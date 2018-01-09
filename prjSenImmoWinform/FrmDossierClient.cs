using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prjSenImmoWinform.DAL;
using prjSenImmoWinform.Models;
using Excel = Microsoft.Office.Interop.Excel;
using System.Transactions;

namespace prjSenImmoWinform
{
    public partial class FrmDossierClient : Form
    {
        private ClientRepository clientRep;
        ContratRepository contratRep;
        Client leClientEncours;
        IlotRepository ilotRep;
        private Contrat leContratEnCours;
        private bool bNouveauEncaissement;
        private bool bRecharger = false;
        private int iNiveauAppelDeFondActuel;
        private CategorieContrat precedentTypeContrat= CategorieContrat.Dépôt;

        public Lot leLotEnCours { get; set; }

        public FrmDossierClient()
        {
            try
            {
                InitializeComponent();
                clientRep = new ClientRepository();
                ilotRep = new IlotRepository();
                tcDetailsDossierClient.TabPages.Remove(tabPage4);
                if (Tools.Tools.AgentEnCours.Role.CodeRole != "ADM")
                {
                    cmdDesistement.Enabled = false;
                }
                if (Tools.Tools.AgentEnCours.Role.CodeRole =="ADM")
                {
                    cmdAvenant.Visible = true;
                }
                else
                    cmdAvenant.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur: ..." + ex.Message,
                        "Prosopis - Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public FrmDossierClient(Client client):this()
        {

            try
            {
                contratRep = new ContratRepository();
                leClientEncours = clientRep.GetClient(client.ID);
                AffichierClient(leClientEncours);
                ChargerLesContrats(client);

                cmbContrats.DisplayMember = "NumeroLot";

                txtIdClient.Text = leClientEncours.ID.ToString();
                txtContratId.Text = leContratEnCours.Id.ToString();
                txtNumeroDossier.Text = leContratEnCours.NumeroContrat;

            }
            catch (Exception ex )
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis - Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public FrmDossierClient(Contrat contratClient) : this(contratClient.Client)
        {

            try
            {
                cmbContrats.SelectedItem = contratRep.GetContratById(contratClient.Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ChargerLesContrats(Client client)
        {
            cmbContrats.DataSource = contratRep.GetContratsClient(client).OrderBy(cont =>cont.Statut).ToList();
            cmbContrats.DisplayMember = "NumeroContrat";
            //    .Select(c => new MiniContrat
            //{
            //    ID = c.Id,
            //    NumeroLot = c.Lot.NumeroLot
            //}).ToList();
        }

        private void AffichierClient(Client client)
        {

            txtDateSouscription.Text = client.DateSouscription.Value.ToShortDateString();
           // txtOrigine.Text = client.Origine != TypeOrigine.Autre ? client.Origine.ToString() : client.AutreOrigine;
            txtPrenom.Text = client.Prenom;
            txtNom.Text = client.Nom;
            txtDateNaissance.Text = client.DateDeNaissance != null ? client.DateDeNaissance.Value.Date.ToShortDateString():"" ;
            txtLieuNaissance.Text = client.LieuDeNaissance;
            txtAdresse.Text = client.Adresse;
            lbCommercial.Text = client.Commercial.NomComplet;
            lbProjet.Text = client.Projet.DenominationProjet;
            Genre sexe = client.Genre;
            if (sexe == Genre.Masculin)
                rbHomme.Checked = true;
            else
                rbFemme.Checked = true;

            txtNationalite.Text = client.Nationalite;
            txtNumeroFixe.Text = client.TelephoneFixe;
            txtNumeroMobile.Text = client.Mobile1;
            txtCompteTiers.Text = client.CompteTiers;

            if (client.TypePieceIdentite != 0)
            {
                txtTypePiece.Text = client.TypePieceIdentite.ToString();
                txtNumeroPiece.Text = client.NumeroPieceIdentification;
                txtDateDelivrance.Text = client.DateDeDelivrancePiece.Value.ToShortDateString();
            }
            else
            {
                txtTypePiece.Text = string.Empty;
                txtNumeroPiece.Text = string.Empty;
                txtDateDelivrance.Text = string.Empty;
            }
            txtEmail.Text = client.Email;
            txtDateSouscription.Text = client.DateSouscription.Value.ToShortDateString();
            txtStatutClient.Text = client.Type.ToString();
            txtOrigine.Text = client.Origine.LibelleTypeOrigine;
            if (Tools.Tools.AgentEnCours.Role.CodeRole == "ADM")
                cmdReaffecter.Visible = true;
            else
                cmdReaffecter.Visible = false;
            //if (client.Commercial != null)
            //{
            //    cmdAffecterCommercial.Visible = false;
            //    pCommercial.Visible = true;
            //    txtCommercial.Text = client.Commercial.NomComplet;
            //    AfficherOptionsProspect(client);
            //    AfficherActivitesCommercialesProspect(client);
            //    AfficherEncaissementProspect(client);
            //    tcDossierProspect.Visible = true;
            //    pActionProspect.Visible = true;
            //    cmdFicheNotaire.Visible = true;
            //}
            //else
            //{
            //    cmdAffecterCommercial.Visible = true;
            //    pCommercial.Visible = false;
            //    tcDossierProspect.Visible = false;
            //    pActionProspect.Visible = false;
            //    cmdFicheNotaire.Visible = false;
            //}

        }

        private void cmbContrats_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AfficherLeContrat();
                txtContratId.Text = leContratEnCours.Id.ToString();
                txtNumeroDossier.Text = leContratEnCours.NumeroContrat;
                if (leContratEnCours.Statut == StatutContrat.Résilié || leContratEnCours.Statut == StatutContrat.Désisté)
                {
                    pProjet.BackColor = Color.Silver;
                    pDetailsProjets.BackColor = Color.Silver;
                    pTitreContrat.BackColor = Color.Silver;
                    lbStatutContrat.Visible = true;
                    lbStatutContrat.Text = "AVENANT SUR CONTRAT".ToUpper();
                    if (leContratEnCours.Statut == StatutContrat.Résilié)
                    {
                        if (tcDetailsDossierClient.TabPages.Count == 3)
                        tcDetailsDossierClient.TabPages.Add(tabPage4);
                        lbStatutContrat.Text = "CONTRAT Résilié".ToUpper();
                        
                        AfficherMouvementsCompteRemboursement();
                        tcDetailsDossierClient.SelectedTab = tabPage4;
                    }
                    cmdDesistement.Visible = false;
                    cmdAvenant.Visible = false;
                    cmdImprimerContratDepot.Visible = false;
                    cmdImprimerContratReservation.Visible = false;
                    cmdAttribuerLot.Visible = false;
                }
                else
                {
                    pProjet.BackColor = Color.FromArgb(193, 205, 193);
                    pDetailsProjets.BackColor = Color.FromArgb(219, 230, 224);
                    pTitreContrat.BackColor = Color.FromArgb(193, 205, 193);
                    if (tcDetailsDossierClient.TabPages.Count == 4)
                        tcDetailsDossierClient.TabPages.Remove(tabPage4);
                    if (Tools.Tools.AgentEnCours.Role.CodeRole == "ADM")
                    {
                        cmdDesistement.Visible = true;
                    }
                    
                }
                if (leClientEncours.Type == TypeClient.Résilié)
                {
                    pClient.BackColor = Color.Silver;

                }
                else
                    pClient.BackColor = Color.FromArgb(248, 248, 248);
                rbEcheancesEchuesNonSoldees.Checked = true;
                rbTotalEcheances.Checked = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis - Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AfficherMouvementsCompteRemboursement()
        {
            var mouvementsComptables = contratRep.GetCompteRemboursements(leContratEnCours.Id);
            dgCompteRemboursements.DataSource = mouvementsComptables.Select(mvt => new
            {
                Date = mvt.DateOp.ToShortDateString(),
                NumeroPiece = mvt.NumeroPiece,
                LibelleOp = mvt.LibelleOp,
                Debit = mvt.Debit,
                Credit = mvt.Credit,
                Solde = mvt.Solde,
                TypeMouvement = mvt.TypeOp,
                ID = mvt.Id

            }).ToList();
            FormatterGrilleMouvement();
            decimal debitTotal = mouvementsComptables.Sum(mvt => mvt.Debit);
            decimal creditTotal = mouvementsComptables.Sum(mvt => mvt.Credit);
            decimal soldeTotal = creditTotal - debitTotal;

            lbDebitTotal.Text = debitTotal.ToString("### ### ###");
            lbCreditTotal.Text = creditTotal.ToString("### ### ###");
            lbSoldeTotal.Text = soldeTotal.ToString("### ### ###");
        }

        private void FormatterGrilleMouvement()
        {
            dgCompteRemboursements.Columns[0].Width = 80;
            dgCompteRemboursements.Columns[1].Width = 130;
            dgCompteRemboursements.Columns[2].Width = 360;
            dgCompteRemboursements.Columns[3].Width = 110;
            dgCompteRemboursements.Columns[3].DefaultCellStyle.Format = "### ### ###";
            dgCompteRemboursements.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgCompteRemboursements.Columns[4].Width = 110;
            dgCompteRemboursements.Columns[4].DefaultCellStyle.Format = "### ### ###";
            dgCompteRemboursements.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgCompteRemboursements.Columns[5].Width = 110;
            dgCompteRemboursements.Columns[5].DefaultCellStyle.Format = "### ### ###";
            dgCompteRemboursements.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgCompteRemboursements.Columns[6].Width = 0;
            dgCompteRemboursements.Columns[6].Visible = false;
            dgCompteRemboursements.Columns[7].Width = 0;
            dgCompteRemboursements.Columns[7].Visible = false;


        }

        private void AfficherLeContrat()
        {
            try
            {
                int iNiveauApelDeFond=0;
                if (cmbContrats.SelectedItem != null)
                {
                    //var miniContrat = (MiniContrat)cmbContrats.SelectedItem;

                    //leContratEnCours = contratRep.FindById(miniContrat.ID);
                    leContratEnCours = (Contrat)cmbContrats.SelectedItem;
                    leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);
                    EffacerContrat();
                    lbStatutContrat.Visible = false;
                    tcDetailsDossierClient.SelectedTab = tcDetailsDossierClient.TabPages[1];
                    if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                    {

                        txtNumeroLot.Text = leContratEnCours.TypeContrat.CategorieContrat== CategorieContrat.Réservation? leContratEnCours.Lot.NumeroLot:"";
                        txtNumeroLot.BackColor = Color.WhiteSmoke;
                        txtSuperficieReelle.BackColor = Color.WhiteSmoke;
                        txtPrixRevise.BackColor = Color.WhiteSmoke;

                        lbTypeContrat.Text = "RESERVATION";
                        lbEcheancier.Visible = false;
                        lbLbalEcheancier.Visible = false;
                        gbEcheancier.Visible = false;
                        lbSeuilEntreeEnVigueur.Text = leContratEnCours.TypeContrat.SeuilEntreeEnVigueur.ToString() + "%";
                        lbSeuilSousciption.Text = leContratEnCours.TypeContrat.SeuilSouscription.ToString() + "%";
                        txtDureeDepot.Visible = false;
                        lbDureeDepot.Visible = false;
                        txtDureeDepot.Text = "";
                        cmdTableauAmortissements.Visible = false;
                        lbTableauAmortissement.Visible = false;
                        txtSuperficieReelle.Text = leContratEnCours.Lot.Superficie.ToString("###");
                        txtPrixRevise.Text = leContratEnCours.PrixRevise.ToString("### ### ###");
                        pEcheances.Visible = false;
                        pAfficherConditionDepot.Visible = false;
                        spcFactures.Location = new Point(3, 6);
                        if (precedentTypeContrat == CategorieContrat.Dépôt)
                            spcFactures.Height += 35;
                        precedentTypeContrat = CategorieContrat.Réservation;

                        AfficherFacturesClient(leContratEnCours.Id, "");
                        leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);
                        var lesEtatsAvancementsActifs = leContratEnCours.Lot.EtatsAvancements.Where(ea => ea.Actif == true);
                        int iNiveauAvancement;

                        if (lesEtatsAvancementsActifs.Count() > 0)
                        {
                            iNiveauAvancement = lesEtatsAvancementsActifs.Max(ea => ea.TypeEtatAvancement.ordre);

                            if (leContratEnCours.Lot.EtatsAvancements.Where(ea => ea.Actif == true && ea.TypeEtatAvancement.AppelFonds == true).Count() > 0)
                            {
                                iNiveauApelDeFond = leContratEnCours.Lot.EtatsAvancements.Where(ea => ea.Actif == true && ea.TypeEtatAvancement.AppelFonds == true).Max(ea => ea.TypeEtatAvancement.ordre);
                                
                            }
                            else
                                iNiveauApelDeFond = 0;
                        }
                        else
                        {
                            iNiveauAvancement = 2;
                        }
                        var niveauAvancementEnCours = ilotRep.GetNiveauAvancement(iNiveauAvancement);


                        if (niveauAvancementEnCours != null)
                        {
                            txtNiveauAvancementResume.Text = niveauAvancementEnCours.Description;
                            if (iNiveauApelDeFond != 0)
                            {
                                int ligne = 0;
                                if (iNiveauApelDeFond == 26)
                                { 
                                    ligne = 2;
                                    if (dgFactures.Rows.Count >= ligne)
                                        dgFactures.Rows[ligne].DefaultCellStyle.BackColor = Color.PaleGreen;
                                }
                                else
                                if (iNiveauApelDeFond == 46)
                                {
                                    ligne = 3;
                                    if (dgFactures.Rows.Count >= ligne)
                                        dgFactures.Rows[ligne].DefaultCellStyle.BackColor = Color.PaleGreen;
                                }
                                else
                                if (iNiveauApelDeFond == 56)
                                {
                                    ligne = 4;
                                    if (dgFactures.Rows.Count >= ligne)
                                        dgFactures.Rows[ligne].DefaultCellStyle.BackColor = Color.PaleGreen;
                                }
                            }
                        }
                        ChargerEtatAvancementLot(leContratEnCours.Lot);
                        cmdImprimerContratDepot.Visible = false;
                        chkContratDepotValide.Visible = false;
                        if(leContratEnCours.ContratValide==false && Tools.Tools.AgentEnCours.IsChefEquipe )
                        {
                            chkContratValide.Visible = true;
                        }
                        else
                        {
                            chkContratValide.Visible = false;
                            if (leContratEnCours.ContratValide == true)
                            {
                                chkContratValide.Visible = true;
                            }
                        }
                    }
                    else//Contrat Dépot
                    {
                        txtNumeroLot.BackColor = Color.LightGray;
                        txtSuperficieReelle.BackColor = Color.LightGray;
                        txtPrixRevise.BackColor = Color.LightGray;
                        lbTypeContrat.Text = "DEPOT";
                        lbEcheancier.Visible = true;
                        lbEcheancier.Text = leContratEnCours.TypeEcheancier.ToString();
                        lbLbalEcheancier.Visible = true;
                        gbEcheancier.Visible = true;
                        lbSeuilEntreeEnVigueur.Text = leContratEnCours.TypeContrat.SeuilEntreeEnVigueur.ToString() + "%";
                        lbSeuilSousciption.Text = leContratEnCours.TypeContrat.SeuilSouscription.ToString() + "%";
                        pEcheances.Visible = true;
                        pAfficherConditionDepot.Visible = true;
                        spcFactures.Location = new Point(3, 45);
                        if (precedentTypeContrat == CategorieContrat.Réservation)
                            spcFactures.Height -= 35;
                        precedentTypeContrat = CategorieContrat.Dépôt;
                        txtDureeDepot.Visible = true;
                        lbDureeDepot.Visible = true;
                        txtDureeDepot.Text = leContratEnCours.DureeDepot.ToString() + " mois";
                        cmdTableauAmortissements.Visible = true;
                        lbTableauAmortissement.Visible = true;

                        Tools.Adorner.AddBadgeTo(rbTotalEcheances, contratRep.GetNombreTotalFactures(leContratEnCours.Id).ToString(), Color.SkyBlue, Color.White);
                        Tools.Adorner.AddBadgeTo(rbEcheancesEchuesNonSoldees, contratRep.GetNombreFacturesEchuesNonSoldees(leContratEnCours.Id).ToString(), Color.Red, Color.White);
                        Tools.Adorner.AddBadgeTo(rbEcheancesSoldees, contratRep.GetNombreFacturesSoldees(leContratEnCours.Id).ToString(), Color.YellowGreen, Color.White);
                        Tools.Adorner.AddBadgeTo(rbEcheancesNonSoldees, contratRep.GetNombreFacturesNonSoldees(leContratEnCours.Id).ToString(), Color.Yellow, Color.Black);
                        rbTotalEcheances.Checked = true;
                        if (leContratEnCours.LotAttribue)
                        {
                            leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);
                            txtNumeroLot.Text = leContratEnCours.Lot.NumeroLot;
                            txtNumeroLot.BackColor = Color.WhiteSmoke;
                            txtSuperficieReelle.BackColor = Color.WhiteSmoke;
                            txtPrixRevise.BackColor = Color.WhiteSmoke;

                            txtNumeroLot.Text = leContratEnCours.Lot.NumeroLot;
                            txtSuperficieReelle.Text = leContratEnCours.Lot.Superficie.ToString("###");
                            txtPrixRevise.Text = leContratEnCours.PrixRevise.ToString("### ### ###");
                            ChargerEtatAvancementLot(leContratEnCours.Lot);
                            //AfficherFacturesClient(leContratEnCours.Id, "");
                            var lesEtatsAvancementsActifs = leContratEnCours.Lot.EtatsAvancements.Where(ea => ea.Actif == true);
                            int iNiveauAvancement;

                            if (lesEtatsAvancementsActifs.Count() > 0)
                            {
                                iNiveauAvancement = lesEtatsAvancementsActifs.Max(ea => ea.TypeEtatAvancement.ordre);
                            }
                            else
                            {
                                iNiveauAvancement = 2;
                            }
                            var niveauAvancementEnCours = ilotRep.GetNiveauAvancement(iNiveauAvancement);

                            if (niveauAvancementEnCours != null)
                            {
                                txtNiveauAvancementResume.Text = niveauAvancementEnCours.Description;
                                // dgFactures.Rows[iNiveauApelDeFond - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                            }
                        }
                        else
                        {
                            chkContratValide.Visible = false;
                        }
                    }
                    leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);
                    txtCodeTypeVilla.Text = leContratEnCours.Lot.TypeVilla.CodeType;
                    txtNomTypeVilla.Text = leContratEnCours.Lot.TypeVilla.NomType;
                    txtPrixStandard.Text = leContratEnCours.Lot.TypeVilla.PrixStandard.ToString("### ### ###");
                    txtSuperficieStandard.Text = leContratEnCours.Lot.TypeVilla.SurfaceDeBase.ToString("###");
                    txtPosition.Text = leContratEnCours.Lot.PositionLot.ToString();

                    txtDateLivraison.Text = leContratEnCours.DateLivraisonLot != null ? leContratEnCours.DateLivraisonLot.Value.ToShortDateString() : "";
                   

                    if (leContratEnCours.Apporteur != null)
                    { 
                        txtApporteurAffaire.Text = leContratEnCours.Apporteur.NomComplet;
                        txtTauxCommission.Text = leContratEnCours.Apporteur.TauxCommission.ToString("##");
                        txtMontantCommission.Text = leContratEnCours.CommissionApporteur.ToString("### ### ###");
                    }

                    txtPrixVente.Text = leContratEnCours.PrixFinal.ToString("### ### ###");
                    txtTauxRemise.Text= (leContratEnCours.RemiseAccordee/ leContratEnCours.PrixRevise*100).ToString("###.##");
                    txtMontantRemise.Text = leContratEnCours.RemiseAccordee.ToString("### ### ###");
                    txtPrixDeVenteResume.Text = leContratEnCours.PrixFinal.ToString("### ### ###");
                  
                    var montantEncaisse = leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal);
                    txtMontantTotalEncaisseResume.Text = montantEncaisse.ToString("### ### ###");
                    txtMontantRestantResume.Text  =(leContratEnCours.PrixFinal- montantEncaisse).ToString("### ### ###");
                    txtNiveauEncaissement.Text= (montantEncaisse / leContratEnCours.PrixFinal*100 ).ToString("###.#");
                    if (leContratEnCours.Souscrit)
                    {
                       
                        txtDateSouscriptionContrat.Text = leContratEnCours.DateSouscription != null ? leContratEnCours.DateSouscription.Value.ToShortDateString() : "";
                        if(leContratEnCours.TypeContrat.CategorieContrat==CategorieContrat.Dépôt)
                        {
                            if (!leContratEnCours.ContratDepotValide)
                            {
                                cmdImprimerContratDepot.Visible = true;
                                chkContratDepotValide.Checked = false;
                                chkContratDepotValide.Enabled = true;
                                if ( Tools.Tools.AgentEnCours.IsChefEquipe)
                                {
                                    chkContratDepotValide.Visible = true;
                                }
                                else
                                {
                                    chkContratDepotValide.Visible = false;
                                    if (leContratEnCours.ContratDepotValide == true)
                                    {
                                        chkContratDepotValide.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                cmdImprimerContratDepot.Visible = false;

                                chkContratDepotValide.Checked = true;
                                chkContratDepotValide.Enabled = false;
                            }
                            //if (!leContratEnCours.ContratValide)
                            //{
                            //    cmdImprimerContratDepot.Visible = true;
                            //    chkContratValide.Checked = false;
                            //    chkContratValide.Enabled = true;
                            //}
                            //else
                            //{
                            //    cmdImprimerContratDepot.Visible = false;
                                
                            //    chkContratValide.Checked = true;
                            //    chkContratValide.Enabled = false;
                            //}
                        }
                        else
                        {
                           
                        }
                        cmdImprimerContratReservation.Visible = false;
                        cmdAttribuerLot.Visible = false;
                    }
                    
                    if (leContratEnCours.AReserve)
                    {
                        //chkContratValide.Visible = true;
                        txtDateReservationContrat.Text = leContratEnCours.DateReservation != null ? leContratEnCours.DateReservation.Value.ToShortDateString() : "";
                       
                        if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                        {
                            cmdImprimerContratReservation.Visible = true;
                            cmdImprimerContratReservation.Location = new Point(pTitreContrat.Width-144, cmdImprimerContratReservation.Location.Y);
                            cmdAttribuerLot.Visible = false;

                            if (!leContratEnCours.ContratValide)
                            {
                                cmdImprimerContratReservation.Visible = true;
                                chkContratValide.Checked = false;
                                chkContratValide.Enabled = true;
                            }
                            else
                            {
                                cmdImprimerContratReservation.Visible = false;
                                chkContratValide.Checked = true;
                                chkContratValide.Enabled = false;
                            }
                        }
                        else
                        {
                            if(leContratEnCours.LotAttribue==false)
                            { 
                                cmdImprimerContratReservation.Visible = false;
                                chkContratValide.Visible = false;
                                cmdAttribuerLot.Visible = true;
                                cmdAttribuerLot.Location = new Point(pTitreContrat.Width - 250, cmdAttribuerLot.Location.Y);
                            }
                            else
                            {
                                cmdImprimerContratReservation.Visible = true;
                                chkContratValide.Visible = true;
                                cmdAttribuerLot.Visible = false;
                                cmdImprimerContratReservation.Location = new Point(pTitreContrat.Width - 275, cmdImprimerContratReservation.Location.Y);
                                if (!leContratEnCours.ContratValide)
                                {
                                    cmdImprimerContratReservation.Visible = true;
                                   
                                    chkContratValide.Checked = false;
                                    chkContratValide.Enabled = true;
                                }
                                else
                                {
                                    cmdImprimerContratReservation.Visible = false;
                                    chkContratValide.Checked = true;
                                    chkContratValide.Enabled = false;
                                }
                            }
                        }
                    }

                    txtDateReservationContrat.Text = leContratEnCours.DateReservation!=null? leContratEnCours.DateReservation.Value.ToShortDateString():"";
                    //AfficherEncaissementClient(leClientEncours, leContratEnCours);
                    //AfficherFacturesClient(leContratEnCours.Id,"");
                    
                    AfficherEncaissementsGlobauxClient(leContratEnCours.Id);
                   
                    ////if (iNiveauApelDeFond > 0)
                    ////{
                    ////    int gridRow = 0;
                    ////    if (iNiveauApelDeFond == 26)
                    ////        gridRow = 3;
                    ////    else
                    ////        if (iNiveauApelDeFond == 46)
                    ////        gridRow = 4;
                    ////    else
                    ////        if (iNiveauApelDeFond == 56)
                    ////        gridRow = 5;

                    ////    iNiveauAppelDeFondActuel = iNiveauApelDeFond;
                    ////    if (leContratEnCours.Statut == StatutContrat.Résilié)
                    ////        dgFactures.Rows[gridRow].DefaultCellStyle.BackColor = Color.Silver;
                    ////    else
                    ////        dgFactures.Rows[gridRow].DefaultCellStyle.BackColor = Color.YellowGreen;

                    ////}
                    if (leContratEnCours.ContratSolde)
                        cmdAttestationSoldeToutCompte.Visible = true;
                    else
                        cmdAttestationSoldeToutCompte.Visible = false;
                    //if (leContratEnCours.LotAttribue)
                    //{
                    //    if (leContratEnCours.Lot.PrixRevise!=leContratEnCours.PrixRevise)
                    //    {
                    //        cmdImprimerContratReservation.Visible = false;
                    //        lbChangerLot.Text = "Le prix du lot choisit ("+leContratEnCours.Lot.PrixRevise.ToString("### ### ###")+") est différent de celui du contrat";
                    //        lbChangerLot.Visible = true;
                    //        cmdAttribuerLot.Left= pTitreContrat.Width-245;
                    //        lbChangerLot.Left = pTitreContrat.Width - 558;
                    //        cmdAttribuerLot.Text = "Changer le lot";
                    //        cmdAttribuerLot.Visible = true;

                    //    }
                    //}
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

      

        private void EffacerContrat()
        {


            txtNumeroLot.Text = string.Empty;
            lbTypeContrat.Text = string.Empty;
            txtSuperficieReelle.Text = string.Empty;
            txtPrixRevise.Text = string.Empty;
            txtNumeroLot.BackColor = Color.LightGray;
            txtSuperficieReelle.BackColor = Color.LightGray;
            txtPrixRevise.BackColor = Color.LightGray;
            lbTypeContrat.Text = string.Empty;
            txtCodeTypeVilla.Text = string.Empty;
            txtNomTypeVilla.Text = string.Empty;
            txtPrixStandard.Text =  string.Empty;
            txtSuperficieStandard.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtMontantCommission.Text = string.Empty;
            txtDateLivraison.Text = string.Empty;
            txtApporteurAffaire.Text = string.Empty;
            txtTauxCommission.Text = string.Empty;
            txtPrixVente.Text = string.Empty;
            txtMontantRemise.Text = string.Empty;
        }

        private void ChargerEtatAvancementLot(Lot lot)
        {
            try
            {
                dgEtatAvancements.DataSource = lot.EtatsAvancements.ToList().Where(ea => ea.TypeEtatAvancement.NiveauTechnique == true && ea.Actif==true).Select
                                                                                 (ea => new
                                                                                 {
                                                                                     ID = ea.ID,
                                                                                     Date = ea.DateSaisieAvancement,
                                                                                     Type = ea.TypeEtatAvancement.Description,
                                                                                     Commentaire=ea.Commentaire

                                                                                 }).ToList();
                dgEtatAvancements.Columns[0].Width = 0;
                dgEtatAvancements.Columns[0].Visible = false;
                dgEtatAvancements.Columns[1].Width = 80;
                dgEtatAvancements.Columns[2].Width = 200;
                dgEtatAvancements.Columns[3].Width = 400;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis - Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmDossierClient_Load(object sender, EventArgs e)
        {
            cmbContrats_SelectedIndexChanged(sender, e);
        }

        private void cmdNouveauContrat_Click(object sender, EventArgs e)
        {
            FrmVente frmVD = new FrmVente(leClientEncours,true);
            frmVD.WindowState = FormWindowState.Normal;
            frmVD.StartPosition = FormStartPosition.CenterParent;
            frmVD.ShowDialog();
            ChargerLesContrats(leClientEncours);
            //leClientEncours.Type = TypeClient.Client;
            //ClientDAL.SaveChanges();
            // this.leClientEncours();
        }

        private void AfficherEncaissementClient(int EncaissementGlobalId)
        {
            try
            {
                var encaissements = clientRep.GetEncaissementsClient(EncaissementGlobalId) ;
                dgEncaissements.DataSource = encaissements.ToList().Select(enc => new
                {
                    ID = enc.ID,
                    Date = enc.Date.Value.ToShortDateString(),
                    Motif = enc.Facture.Motif,
                    Montant = enc.Montant

                }).ToList();
                dgEncaissements.Columns[0].Width = 0;
                dgEncaissements.Columns[1].Width = 80;
                dgEncaissements.Columns[2].Width = 250;
                dgEncaissements.Columns[3].Width = 80;
                dgEncaissements.Columns[3].DefaultCellStyle.Format = "### ### ###";
                dgEncaissements.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //dgProspects.Columns[3].HeaderText = "Né(e) le";
                //dgProspects.Columns[4].HeaderText = "à";
                dgEncaissements.Columns[0].Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfficherEncaissementsGlobauxClient( int contratId)
        {
            try
            {
                var encaissements = clientRep.GetEncaissementsGlobauxClient(contratId).ToList();
                dgEncaissementsGlobals.DataSource = encaissements.ToList()
                    .Select(encG => new
                    {
                        ID = encG.ID,
                        Date = encG.DateEncaissement.Value.ToShortDateString(),
                        Montant = encG.MontantGlobal,
                        Mode = encG.ModePaiement,
                        Référence=encG.ReferencePaiement,
                        Commentaires=encG.Commentaire

                     }).ToList();
                
                dgEncaissementsGlobals.Columns[0].Width = 0;
                dgEncaissementsGlobals.Columns[0].Visible = false;
                dgEncaissementsGlobals.Columns[1].Width = 80;
                dgEncaissementsGlobals.Columns[2].Width = 80;
                dgEncaissementsGlobals.Columns[2].DefaultCellStyle.Format = "### ### ###";
                dgEncaissementsGlobals.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgEncaissementsGlobals.Columns[3].Width = 80;
                dgEncaissementsGlobals.Columns[4].Width = 170;
                dgEncaissementsGlobals.Columns[5].Width = 400;

                //dgProspects.Columns[3].HeaderText = "Né(e) le";
                //dgProspects.Columns[4].HeaderText = "à";
                //dgEncaissements.Columns[0].Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfficherFacturesClient(int contratId,string etatFacture)
        {
            var contrat = contratRep.GetContratById(contratId);
            try
            {
                var factures = contratRep.GetEcheancesClient(contratId, etatFacture).OrderBy(fact =>fact.DateEcheanceFacture).ToList();
                var Listfactures = factures.ToList()
                    .Select(fact => new
                    {
                        ID = fact.Id,
                        //Numéro=fact.NumeroFacture,
                        Date = fact.DateEcheanceFacture != null ? fact.DateEcheanceFacture.Value.ToShortDateString() : "",
                        Numéro = fact.NumeroFacture,
                        Motif = fact.Motif,
                        Taux = fact.Contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation && fact.TypeFacture != TypeFacture.FraisDossier ? fact.EtatAvancement.TypeEtatAvancement.TauxDecaissement  : 0,
                        Montant = fact.Montant,
                        Encaissé = fact.MontantEncaisse,
                        Restant = fact.Montant - fact.MontantEncaisse,
                        Soldée = fact.FacturePayee,
                        Ordre = fact.Contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation && fact.TypeFacture != TypeFacture.FraisDossier ? fact.EtatAvancement.TypeEtatAvancement.ordre : 0,

                    }).ToList();
                if(contrat.TypeContrat.CategorieContrat== CategorieContrat.Réservation)
                {
                    Listfactures = Listfactures.OrderBy(fact => fact.Ordre).ToList();
                }
                lvFactures.Items.Clear();
                if(contrat.TypeContrat.CategorieContrat== CategorieContrat.Dépôt)
                {
                    lvFactures.Columns[4].Width = 0;
                }
                foreach (var item in Listfactures)
                {
                    ListViewItem lviFacture = new ListViewItem(item.ID.ToString());
                    lviFacture.SubItems.Add(item.Date);
                    lviFacture.SubItems.Add(item.Numéro);
                    lviFacture.SubItems.Add(item.Motif);


                    decimal taux;
                    TypeContrat tc;
                    //if (contrat.ProjetId==1)
                    //{
                    //    if (item.Ordre == 2)
                    //    {
                    //        //tc = contratRep.GetTypeContrat contrat.TypeContratID);
                    //        taux = contrat.TypeContrat.SeuilSouscription - 5;
                    //    }
                    //    else
                    //               if (item.Ordre == 26)
                    //    {
                    //        //tc = DB.TypeContrats.Find(contrat.TypeContratID);
                    //        taux = (70 - contrat.TypeContrat.SeuilSouscription);

                    //    }
                    //    else
                    //    {
                    //        taux = item.Taux;
                    //    } 
                    //}
                    //else
                    //{
                        taux = item.Taux;
                    //}

                    lviFacture.SubItems.Add(taux!=0?taux.ToString("###")+"%":"");
                    lviFacture.SubItems.Add(item.Montant.ToString("### ### ###"));
                    lviFacture.SubItems.Add(item.Encaissé.ToString("### ### ##0"));
                    lviFacture.SubItems.Add(item.Restant.ToString("### ### ##0"));
                    //lviFacture.SubItems.Add(item.Soldée.ToString());
                    //lviFacture.SubItems.Add(item.Ordre.ToString());
                    lviFacture.Checked = item.Soldée;
                    lviFacture.Tag = item.ID;
                    lvFactures.Items.Add(lviFacture);
                }
                dgFactures.DataSource = Listfactures;
                dgFactures.Columns[0].Width = 0;
                dgFactures.Columns[0].Visible = false;
                dgFactures.Columns[1].Width = 80;
                dgFactures.Columns[2].Width = 120;
                dgFactures.Columns[3].Width = 215;
                dgFactures.Columns[4].Width = 50;
               
                dgFactures.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgFactures.Columns[5].Width = 80;
                dgFactures.Columns[5].DefaultCellStyle.Format = "### ### ###";
                dgFactures.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                
                dgFactures.Columns[6].Width = 80;
                dgFactures.Columns[6].DefaultCellStyle.Format = "### ### ###";
                dgFactures.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgFactures.Columns[7].Width = 80;
                dgFactures.Columns[7].DefaultCellStyle.Format = "### ### ###";
                dgFactures.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgFactures.Columns[8].Width = 50;
                dgFactures.Columns[9].Visible = false;
                if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                {
                    dgFactures.Columns[4].Width = 0;
                    dgFactures.Columns[4].Visible = false;
                  
                }
                else
                {
                    dgFactures.Columns[4].Width = 50;
                    dgFactures.Columns[4].Visible = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgEncaissementsGlobals_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgEncaissementsGlobals.SelectedRows.Count > 0)
                {
                    int encaissementGlobalId = (int)dgEncaissementsGlobals.SelectedRows[0].Cells[0].Value;
                    AfficherEncaissementClient(encaissementGlobalId);
                  
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgEncaissementsGlobals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tcDetailsDossierClient_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //if (e.TabPage == tcDetailsDossierClient.TabPages[0])
            //    e.Cancel = true;
        }

        private void cmdNouvelEncaissement_Click(object sender, EventArgs e)
        {

        }

       
      

       

      
        private void cmbModePaiement_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       


         private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void txtMontantRestantResume_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void dgFactures_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgFactures.SelectedRows.Count > 0)
                {
                    int factureId = (int)dgFactures.SelectedRows[0].Cells[0].Value;
                    var facture = contratRep.GetFactureById(factureId);
                    dgRepartitionEncaissementsFacture.DataSource = facture.Encaissements.ToList().Select
                        (enc => new
                        {
                            Id=enc.ID,
                            Date = enc.Date.Value.Date,
                            Montant=enc.Montant,
                            Référence=enc.ReferencePaiement

                        }).ToList();

                    dgRepartitionEncaissementsFacture.Columns[0].Width = 0;
                    dgRepartitionEncaissementsFacture.Columns[0].Visible = false;
                    dgRepartitionEncaissementsFacture.Columns[1].Width = 80;
                    dgRepartitionEncaissementsFacture.Columns[2].Width = 80;
                    dgRepartitionEncaissementsFacture.Columns[2].DefaultCellStyle.Format = "### ### ###";
                    dgRepartitionEncaissementsFacture.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    
                    dgRepartitionEncaissementsFacture.Columns[3].Width = 250;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      




        private void cmdTotalEcheances_Click(object sender, EventArgs e)
        {
            try
            {
                AfficherFacturesClient(leContratEnCours.Id, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                       "Prosopis - Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdEcheancesSoldees_Click(object sender, EventArgs e)
        {
            try
            {
                AfficherFacturesClient(leContratEnCours.Id, "soldees");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                       "Prosopis - Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdEcheancesEchuesNonSoldees_Click(object sender, EventArgs e)
        {
            try
            {
                AfficherFacturesClient(leContratEnCours.Id, "EchuesNonSoldees");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                       "Prosopis - Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdEcheancesNonSoldees_Click(object sender, EventArgs e)
        {
            try
            {
                AfficherFacturesClient(leContratEnCours.Id, "nonSoldees");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                       "Prosopis - Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgFactures_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gbEcheancier_Enter(object sender, EventArgs e)
        {

        }

        private void cmdAttribuerLot_Click(object sender, EventArgs e)
        {

            try
            {
                FrmDetailsIlot frmDetIlot = new FrmDetailsIlot("SelectionLot");
                //frmDetIlot.MdiParent = this.MdiParent;
                frmDetIlot.WindowState = FormWindowState.Normal;
                frmDetIlot.StartPosition = FormStartPosition.CenterParent;
                frmDetIlot.ShowDialog(this);
                leLotEnCours = frmDetIlot.GetLotSelectionne();
                if (leLotEnCours != null)
                {
                   // leContratEnCours.Lot = leLotEnCours;
                    if(contratRep.AttribuerLotDepot(leContratEnCours.Id, leLotEnCours.ID))
                        AfficherLeContrat();
                    else
                        MessageBox.Show(this, "Erreur: lors de l'attribution du contrat",
                        "Prosopis - Attribution de lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis - Attribution de lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCommentairePaiement_TextChanged(object sender, EventArgs e)
        {

        }

        private void timerGrid_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    timerGrid.Enabled = false;
            //    if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation && iNiveauAppelDeFondActuel > 0)
            //        dgFactures.Rows[iNiveauAppelDeFondActuel - 1].DefaultCellStyle.BackColor = Color.YellowGreen;
            //}
            //catch (Exception ex)
            //{
                
            //    MessageBox.Show(this, "Erreur lors de l'affichage de la grille des appels de fond" + ex.Message,
            //            "Prosopis -  Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void rbTotalEcheances_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(rbTotalEcheances.Checked)
                    AfficherFacturesClient(leContratEnCours.Id, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                       "Prosopis -  Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbEcheancesEchuesNonSoldees_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if(rbEcheancesEchuesNonSoldees.Checked)
                    AfficherFacturesClient(leContratEnCours.Id, "EchuesNonSoldees");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                       "Prosopis -  Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbEcheancesSoldees_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(rbEcheancesSoldees.Checked)
                    AfficherFacturesClient(leContratEnCours.Id, "soldees");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                       "Prosopis -  Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbEcheancesNonSoldees_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(rbEcheancesNonSoldees.Checked)
                    AfficherFacturesClient(leContratEnCours.Id, "nonSoldees");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                       "Prosopis -  Gestion du dossier client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdDesistement_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "Souhaitez vous réellement résilier ce contrat?", "Prosopis -  Gestion du dossier client - Résiliation de contrat", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (MessageBox.Show(this, "Merci de confirmer!!!",
                            "Prosopis - Edition avenant", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        var montantRemboursement = contratRep.ResilierContrat(leContratEnCours.Id, DateTime.Now.Date);

                        //MessageBox.Show(this, "Le contrat a été résilié avec un remboursement de " + montantRemboursement.ToString("### ### ###"),
                        //                       "Prosopis -  Résiliation de contrat client ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ////Visualiser le soldeToutCompte

                        FrmSoldeToutCompte frmStc = new FrmSoldeToutCompte(leContratEnCours.Id);
                        frmStc.ShowDialog();
                        this.ChargerLesContrats(leClientEncours);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                      "Prosopis -   Résiliation de contrat client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgRepartitionEncaissementsFacture_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdImprimerContratReservation_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                {
                    if(leContratEnCours.ProjetId==1)
                    { 
                        GenererContratReservationResa();
                        GenererFicheDemandeContratReservation();
                    }
                    else
                    {
                        GenererContratReservationResaKerria();
                        GenererFicheDemandeContratReservationKerria();
                    }
                }
                else
                {
                    GenererConversionDepotContratReservationResa();
                    GenererFicheDemandeContratTransitionDepotReservation();
                }
                //FicheDemandeContratTransitionDepotResa
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                     "Prosopis -   Edition de contrat de réservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void GenererContratReservationResa()
        {
            try
            {
                leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);

                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates.Trim();
                object templateName = dossierTemplates+"ContratReservationAvecApporteurAffaires.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                msWord.Activate();
                doc.Activate();

                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;

                myBookmark = bookmarks["NomCompletClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


                myBookmark = bookmarks["AdjectifNaissanceClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "né" : "née";

                if (leClientEncours.DateDeNaissance != null)
                {
                    myBookmark = bookmarks["DateNaissance"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.DateDeNaissance.Value.Date.ToShortDateString();
                }

                myBookmark = bookmarks["LieuNaissance"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.LieuDeNaissance;

                myBookmark = bookmarks["AdresseClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;

                myBookmark = bookmarks["Nationalite"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Nationalite;

                myBookmark = bookmarks["SujetClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "il" : "elle";

                myBookmark = bookmarks["ArticlePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.TypePieceIdentite == TypePieceIdentite.CNI ? "de la" : "du";

                myBookmark = bookmarks["TypePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.TypePieceIdentite == TypePieceIdentite.CNI ? "Carte nationale d'identité" : "passeport";

                myBookmark = bookmarks["NumeroPieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NumeroPieceIdentification;

                myBookmark = bookmarks["DatePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.DateDeDelivrancePiece.HasValue ? leClientEncours.DateDeDelivrancePiece.Value.ToShortDateString() : "";

                myBookmark = bookmarks["NomTypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType;

                myBookmark = bookmarks["SemestreLivraison"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = Tools.Tools.GetSemestre( leContratEnCours.DateLivraisonLot.Value.Month) +" " + leContratEnCours.DateLivraisonLot.Value.Year;

                myBookmark = bookmarks["SuperficieStandard"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");

                myBookmark = bookmarks["NumeroLot"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                myBookmark = bookmarks["CodeTypeVilla"];
                bookmarkRange = myBookmark.Range;
                if(leContratEnCours.Lot.PositionLot!= PositionLot.Angle)
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType;
                else
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType+"(angle)";


                myBookmark = bookmarks["SuperficieReelle"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");

                myBookmark = bookmarks["NombrePieces"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.ImageVilla;


                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                myBookmark = bookmarks["PrixDeVenteEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)leContratEnCours.PrixFinal);


                //////////////////////////////////////////////////////////////////////////
                Microsoft.Office.Interop.Word.Tables tables = doc.Tables;
                if (tables.Count > 0)
                {
                    //Get the first table in the document
                    Microsoft.Office.Interop.Word.Table table = tables[1];
                    foreach (var typeEA in leContratEnCours.TypeContrat.TypeEtatAvancements.OrderBy(tea => tea.ordre))
                    {

                        Microsoft.Office.Interop.Word.Row row = table.Rows.Add(ref missing);
                        row.Cells[1].Range.Text = typeEA.LibelleCommercial;
                        row.Cells[1].WordWrap = true;
                        row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[1].Range.Bold = 0;




                        row.Cells[2].Range.Text = typeEA.TauxDecaissement.ToString("###") + "%";
                        row.Cells[2].WordWrap = true;
                        row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[2].Range.Bold = 0;



                    }
                }

                //////////////////////////////////////////////////////////////////////////

                //myBookmark = bookmarks["SeuilReservation"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.TypeContrat.SeuilSouscription.ToString();

                //myBookmark = bookmarks["SeuilAchevementGrosOeuvre"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = (70 - leContratEnCours.TypeContrat.SeuilSouscription).ToString();

                myBookmark = bookmarks["MontantReservation"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ###") + " Francs CFA"; ;

                var depotDeGarantie = (leContratEnCours.PrixFinal * 5 / 100);
                myBookmark = bookmarks["MontantDepotDeGarantie"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = depotDeGarantie.ToString("### ### ###");

                myBookmark = bookmarks["MontantDepotDeGarantieEnLettre"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)(depotDeGarantie));

                var accompte = (leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) - depotDeGarantie);
                myBookmark = bookmarks["AccomptePrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = accompte.ToString("### ### ###");

                myBookmark = bookmarks["AccomptePrixDeVenteEnLettre"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)(accompte));

                if(leContratEnCours.ContratSolde==true)
                {
                    myBookmark = bookmarks["AccompteSolde"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "au solde";
                }


                //myBookmark = bookmarks["PrixDeVenteEnLettres"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)leContratEnCours.PrixFinal * 30 / 100);



                //if (leContratEnCours.Apporteur == null)
                //{
                //    myBookmark = bookmarks["ClauseApporteurAffaire"];
                //    bookmarkRange = myBookmark.Range;
                //    bookmarkRange.Text = string.Empty;
                //}

                myBookmark = bookmarks["NomCompletbasDePage"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;

                myBookmark = bookmarks["NumeroDossierBasDePage"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.NumeroContrat;

                myBookmark = bookmarks["NumeroLotBasDePage"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                myBookmark = bookmarks["DateImpression"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Day + " " + String.Format("{0:y}", DateTime.Now); ;

                //// Attribuer le nom
                //object fileName = @"Mon nouveau document.doc";
                ////// Sauver le document
                ////doc.SaveAs(ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing);
                //// Fermer le document
                //doc.Close(ref missing, ref missing, ref missing);


                //// Fermeture de word
                //msWord.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GenererFicheDemandeContratReservation()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "FicheDemandeContrat.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                msWord.Activate();
                doc.Activate();
                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;


                myBookmark = bookmarks["DateDuJour"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Day + " " + String.Format("{0:y}", DateTime.Now);


                myBookmark = bookmarks["Commercial"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Commercial.NomComplet;

                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;

                if(leContratEnCours.Apporteur!=null)
                { 
                    myBookmark = bookmarks["ApporteurAffaire"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Apporteur.NomComplet;
                }

             

                if (leClientEncours.DateDeNaissance != null)
                {
                    myBookmark = bookmarks["DateEtLieuNaissance"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.DateDeNaissance.Value.ToShortDateString() + " à " + leClientEncours.LieuDeNaissance;
                }

                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;

                myBookmark = bookmarks["Profession"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Profession;

                myBookmark = bookmarks["Telephone"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Mobile1;

                myBookmark = bookmarks["Email"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Email;

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType;
                if (leContratEnCours.Lot.PositionLot != PositionLot.Angle)
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType;
                else
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType + "(angle)";

                if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                {
                    myBookmark = bookmarks["NumeroLot"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;
                }

                if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                {

                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");
                }
                else
                {
                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.SurfaceDeBase.ToString("###");
                }
                myBookmark = bookmarks["DateLivraison"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.DateLivraisonLot.Value.ToShortDateString();

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.PrixRevise.ToString("### ### ###")+ " Francs CFA";

                myBookmark = bookmarks["MontantRemise"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.RemiseAccordee.ToString("### ### ##0") + " Francs CFA";

                myBookmark = bookmarks["PrixDeVenteApresRemise"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###") + " Francs CFA";

                myBookmark = bookmarks["TypeContrat"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeContrat.LibelleTypeContrat;

                var encFraisDeDossier = contratRep.GetEncaissementProspect(leClientEncours.ID).Where(enc => enc.ProspectId == leClientEncours.ID && enc.FraisDeDossier == true).FirstOrDefault();
                    //leClientEncours.EncaissementProspects.Where(enc => enc.ProspectId == leClientEncours.ID && enc.FraisDeDossier == true).FirstOrDefault();
                if (encFraisDeDossier != null)
                {
                    myBookmark = bookmarks["FraisDeDossier"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = encFraisDeDossier.MontantGlobal.ToString("### ### ##0") + " Francs CFA"; 
                    //leContratEnCours.Factures.Where(fact => fact.TypeFacture == TypeFacture.FraisDossier).FirstOrDefault().Montant.ToString("### ### ##0") + " Francs CFA"; ;
                }
                //if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                //{
                    myBookmark = bookmarks["MontantVerse"];
                    bookmarkRange = myBookmark.Range;
                    //bookmarkRange.Text = (leContratEnCours.PrixFinal * leContratEnCours.TypeContrat.SeuilEntreeEnVigueur / 100).ToString("### ### ###") + " Francs CFA";
                    bookmarkRange.Text= leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ##0") + " Francs CFA"; 
                //}
                //else
                //{
                //    myBookmark = bookmarks["MontantSeuilMinimumDepot"];
                //    bookmarkRange = myBookmark.Range;
                //    bookmarkRange.Text = leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ##0") + " Francs CFA";
                //}

               

                //// Attribuer le nom
                //object fileName = @"Mon nouveau document.doc";
                ////// Sauver le document
                ////doc.SaveAs(ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing);
                //// Fermer le document
                //doc.Close(ref missing, ref missing, ref missing);


                //// Fermeture de word
                //msWord.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void GenererFicheDemandeContratReservationKerria()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "FicheDemandeContratKerria.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                msWord.Activate();
                doc.Activate();
                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;


                myBookmark = bookmarks["DateDuJour"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Day + " " + String.Format("{0:y}", DateTime.Now);


                myBookmark = bookmarks["Commercial"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Commercial.NomComplet;

                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;

                if (leContratEnCours.Apporteur != null)
                {
                    myBookmark = bookmarks["ApporteurAffaire"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Apporteur.NomComplet;
                }



                if (leClientEncours.DateDeNaissance != null)
                {
                    myBookmark = bookmarks["DateEtLieuNaissance"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.DateDeNaissance.Value.ToShortDateString() + " à " + leClientEncours.LieuDeNaissance;
                }

                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;


                myBookmark = bookmarks["TypeConstruction"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.TypeConstruction.ToString();


                myBookmark = bookmarks["Profession"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Profession;

                myBookmark = bookmarks["Telephone"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Mobile1;

                myBookmark = bookmarks["Email"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Email;

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomComplet;
                if (leContratEnCours.Lot.PositionLot != PositionLot.Angle)
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomComplet;
                else
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomComplet + "(angle)";

                if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                {
                    myBookmark = bookmarks["NumeroLot"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;
                }

                if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                {

                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");
                }
                else
                {
                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.SurfaceDeBase.ToString("###");
                }
                myBookmark = bookmarks["DateLivraison"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.DateLivraisonLot.Value.ToShortDateString();

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.PrixRevise.ToString("### ### ###") + " Francs CFA";

                myBookmark = bookmarks["MontantRemise"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.RemiseAccordee.ToString("### ### ##0") + " Francs CFA";

                myBookmark = bookmarks["PrixDeVenteApresRemise"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###") + " Francs CFA";

                myBookmark = bookmarks["TypeContrat"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeContrat.LibelleTypeContrat;

                var encFraisDeDossier = contratRep.GetEncaissementProspect(leClientEncours.ID).Where(enc => enc.ProspectId == leClientEncours.ID && enc.FraisDeDossier == true).FirstOrDefault();
                //leClientEncours.EncaissementProspects.Where(enc => enc.ProspectId == leClientEncours.ID && enc.FraisDeDossier == true).FirstOrDefault();
                if (encFraisDeDossier != null)
                {
                    myBookmark = bookmarks["FraisDeDossier"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = encFraisDeDossier.MontantGlobal.ToString("### ### ##0") + " Francs CFA";
                    //leContratEnCours.Factures.Where(fact => fact.TypeFacture == TypeFacture.FraisDossier).FirstOrDefault().Montant.ToString("### ### ##0") + " Francs CFA"; ;
                }
                //if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                //{
                myBookmark = bookmarks["MontantVerse"];
                bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = (leContratEnCours.PrixFinal * leContratEnCours.TypeContrat.SeuilEntreeEnVigueur / 100).ToString("### ### ###") + " Francs CFA";
                bookmarkRange.Text = leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ##0") + " Francs CFA";
                //}
                //else
                //{
                //    myBookmark = bookmarks["MontantSeuilMinimumDepot"];
                //    bookmarkRange = myBookmark.Range;
                //    bookmarkRange.Text = leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ##0") + " Francs CFA";
                //}



                //// Attribuer le nom
                //object fileName = @"Mon nouveau document.doc";
                ////// Sauver le document
                ////doc.SaveAs(ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing);
                //// Fermer le document
                //doc.Close(ref missing, ref missing, ref missing);


                //// Fermeture de word
                //msWord.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GenererFicheDemandeContratTransitionDepotReservation()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "FicheDemandeContratTransitionDepotResa.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                msWord.Activate();
                doc.Activate();
                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;


                myBookmark = bookmarks["DateDuJour"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Day + " " + String.Format("{0:y}", DateTime.Now);


                myBookmark = bookmarks["Commercial"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Commercial.NomComplet;

                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;

                //if (leContratEnCours.Apporteur != null)
                //{
                myBookmark = bookmarks["SurfaceDeBase"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");
                //}



                //if (leClientEncours.DateDeNaissance != null)
                //{
                //    myBookmark = bookmarks["DateEtLieuNaissance"];
                //    bookmarkRange = myBookmark.Range;
                //    bookmarkRange.Text = leClientEncours.DateDeNaissance.Value.ToShortDateString() + " à " + leClientEncours.LieuDeNaissance;
                //}

                //myBookmark = bookmarks["Adresse"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leClientEncours.Adresse;

                //myBookmark = bookmarks["Profession"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leClientEncours.Profession;

                //myBookmark = bookmarks["Telephone"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leClientEncours.Mobile1;

                ////myBookmark = bookmarks["Email"];
                ////bookmarkRange = myBookmark.Range;
                ////bookmarkRange.Text = leClientEncours.Email;

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                if (leContratEnCours.Lot.PositionLot != PositionLot.Angle)
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType;
                else
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType + "(angle)";


                if (leContratEnCours.Lot!=null)
                {
                    myBookmark = bookmarks["NumeroLot"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");
                }

               
                myBookmark = bookmarks["DateDeLivraison"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.DateLivraisonLot.Value.ToShortDateString();

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###") + " Francs CFA";

                myBookmark = bookmarks["MontantEcheance"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.MontantEcheance.Value.ToString("### ### ##0") + " Francs CFA";


                myBookmark = bookmarks["MontantEcheance2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.MontantEcheance.Value.ToString("### ### ##0") + " Francs CFA";

                myBookmark = bookmarks["Periodicite"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeEcheancier.Value.ToString();

                myBookmark = bookmarks["Periodicite2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeEcheancier.Value.ToString();

                //myBookmark = bookmarks["TypeContrat"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.TypeContrat.LibelleTypeContrat;


                myBookmark = bookmarks["MontantTotalVerse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ###") + " Francs CFA"; ;

                //if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                //{
                //    myBookmark = bookmarks["MontantSeuilMinimumResa"];
                //    bookmarkRange = myBookmark.Range;
                //    bookmarkRange.Text = (leContratEnCours.PrixFinal * leContratEnCours.TypeContrat.SeuilEntreeEnVigueur / 100).ToString("### ### ###") + " Francs CFA";
                //}
                //else
                //{
                //    myBookmark = bookmarks["MontantSeuilMinimumDepot"];
                //    bookmarkRange = myBookmark.Range;
                //    bookmarkRange.Text = (leContratEnCours.PrixFinal * leContratEnCours.TypeContrat.SeuilEntreeEnVigueur / 100).ToString("### ### ###") + " Francs CFA";
                //}



                //// Attribuer le nom
                //object fileName = @"Mon nouveau document.doc";
                ////// Sauver le document
                ////doc.SaveAs(ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing);
                //// Fermer le document
                //doc.Close(ref missing, ref missing, ref missing);


                //// Fermeture de word
                //msWord.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void GenererContratReservationResaKerria()
        {
            try
            {
                leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);

                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates.Trim();
                object templateName = dossierTemplates + "ContratReservationKerriaAppartVilla.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                msWord.Activate();
                doc.Activate();

                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;

                myBookmark = bookmarks["Titre1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "Monsieur" : "Madame";

                myBookmark = bookmarks["NomCompletClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


                myBookmark = bookmarks["AdjectifNaissanceClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "né" : "née";

                if (leClientEncours.DateDeNaissance != null)
                {
                    myBookmark = bookmarks["DateNaissance"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.DateDeNaissance.Value.Date.ToShortDateString();
                }

                myBookmark = bookmarks["LieuNaissance"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.LieuDeNaissance;

                myBookmark = bookmarks["AdresseClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;

                myBookmark = bookmarks["Nationalite"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Nationalite;

                myBookmark = bookmarks["SujetClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "il" : "elle";

                myBookmark = bookmarks["ArticlePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.TypePieceIdentite == TypePieceIdentite.CNI ? "de la" : "du";

                myBookmark = bookmarks["TypePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.TypePieceIdentite == TypePieceIdentite.CNI ? "Carte nationale d'identité" : "passeport";

                myBookmark = bookmarks["NumeroPieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NumeroPieceIdentification;

                myBookmark = bookmarks["DatePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.DateDeDelivrancePiece.HasValue ? leClientEncours.DateDeDelivrancePiece.Value.ToShortDateString() : "";

                //myBookmark = bookmarks["NomTypeVilla"];
                //bookmarkRange = myBookmark.Range;
                //if (leContratEnCours.TypeContrat.TypeConstruction == TypeConstruction.Appartement)
                //    bookmarkRange.Text = leContratEnCours.Lot.Ilot.NomIlot;
                //else
                //    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType;


                myBookmark = bookmarks["SemestreLivraison"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = Tools.Tools.GetSemestre(leContratEnCours.DateLivraisonLot.Value.Month) + " " + leContratEnCours.DateLivraisonLot.Value.Year;

                //myBookmark = bookmarks["SuperficieStandard"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");

                //myBookmark = bookmarks["NumeroLot"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                myBookmark = bookmarks["CodeTypeVilla"];
                bookmarkRange = myBookmark.Range;
                if (leContratEnCours.Lot.PositionLot != PositionLot.Angle)
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType;
                else
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType + "(angle)";

                
                if (leContratEnCours.TypeContrat.TypeConstruction== TypeConstruction.Appartement)
                {
                    
                    myBookmark = bookmarks["BienReserveAppart"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "Le bien réservé se situera dans le bâtiment dénommé "+ leContratEnCours.Lot.Ilot.NomIlot + ", et consistera en un appartement portant le numéro "+ leContratEnCours.Lot.NumeroLot + " de l’état descriptif de division et dont les principales caractéristiques seront les suivantes :";

                    //myBookmark = bookmarks["TypeConstruction"];
                    //bookmarkRange = myBookmark.Range;
                    //bookmarkRange.Text = "un Appartement";

                    myBookmark = bookmarks["TypeConstruction2"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "l'appartement";

                    myBookmark = bookmarks["TypeConstruction3"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "l'appartement";

                    myBookmark = bookmarks["TypeConstructionBasDePage"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "Appartement";

                    myBookmark = bookmarks["ClausePartiesCommunes"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "Et avec une proportion de tantièmes des parties communes générales.";

                    myBookmark = bookmarks["ImmeubleBasDePage"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType;
                }
                else
                {
                    myBookmark = bookmarks["BienReserveAppart"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "Le bien réservé consistera en une villa "+ leContratEnCours.Lot.TypeVilla.NomType+", portant le numéro "+leContratEnCours.Lot.NumeroLot+" de l’état descriptif de division et dont les principales caractéristiques seront les suivantes :";

                    //myBookmark = bookmarks["TypeConstruction"];
                    //bookmarkRange = myBookmark.Range;
                    //bookmarkRange.Text = "une Villa";

                    myBookmark = bookmarks["TypeConstruction2"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "la villa";

                    myBookmark = bookmarks["TypeConstruction3"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "la villa";

                    myBookmark = bookmarks["TypeConstructionBasDePage"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "Villa";

                    myBookmark = bookmarks["ClausePartiesCommunes"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "";

                    myBookmark = bookmarks["ImmeubleBasDePage"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType;
                }

                myBookmark = bookmarks["SuperficieReelle"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");

                myBookmark = bookmarks["NombrePieces"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.ImageVilla;


                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                myBookmark = bookmarks["PrixDeVenteEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)leContratEnCours.PrixFinal);

                //////////////////////////////////////////////////////////////////////////
                Microsoft.Office.Interop.Word.Tables tables = doc.Tables;
                if (tables.Count > 0)
                {
                    //Get the first table in the document
                    Microsoft.Office.Interop.Word.Table table = tables[1];
                    foreach (var typeEA in leContratEnCours.TypeContrat.TypeEtatAvancements.OrderBy(tea => tea.ordre))
                    {

                        Microsoft.Office.Interop.Word.Row row = table.Rows.Add(ref missing);
                        row.Cells[1].Range.Text = typeEA.LibelleCommercial;
                        row.Cells[1].WordWrap = true;
                        row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[1].Range.Bold = 0;




                        row.Cells[2].Range.Text = typeEA.TauxDecaissement.ToString("###") + "%";
                        row.Cells[2].WordWrap = true;
                        row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[2].Range.Bold = 0;



                    }
                }

                //////////////////////////////////////////////////////////////////////////

                //myBookmark = bookmarks["SeuilReservation"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.TypeContrat.SeuilSouscription.ToString();

                //myBookmark = bookmarks["SeuilAchevementGrosOeuvre"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = (70 - leContratEnCours.TypeContrat.SeuilSouscription).ToString();

                myBookmark = bookmarks["MontantReservation"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ###") + " Francs CFA"; ;

                var depotDeGarantie = (leContratEnCours.PrixFinal * 5 / 100);
                myBookmark = bookmarks["MontantDepotDeGarantie"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = depotDeGarantie.ToString("### ### ###");

                myBookmark = bookmarks["MontantDepotDeGarantieEnLettre"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)(depotDeGarantie));

                var accompte = (leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) - depotDeGarantie);
                myBookmark = bookmarks["AccomptePrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = accompte.ToString("### ### ###");

                myBookmark = bookmarks["AccomptePrixDeVenteEnLettre"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)(accompte));

                if (leContratEnCours.ContratSolde == true)
                {
                    myBookmark = bookmarks["AccompteSolde"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "au solde";
                }


                //myBookmark = bookmarks["PrixDeVenteEnLettres"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)leContratEnCours.PrixFinal * 30 / 100);



                //if (leContratEnCours.Apporteur == null)
                //{
                //    myBookmark = bookmarks["ClauseApporteurAffaire"];
                //    bookmarkRange = myBookmark.Range;
                //    bookmarkRange.Text = string.Empty;
                //}

                myBookmark = bookmarks["NomCompletbasDePage"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;

                myBookmark = bookmarks["TypeVillaBasDePage"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType;

              

                //myBookmark = bookmarks["NumeroDossierBasDePage"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.NumeroContrat;

                myBookmark = bookmarks["NumeroLotBasDePage"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                myBookmark = bookmarks["DateImpression"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Day + " " + String.Format("{0:y}", DateTime.Now); ;

                //// Attribuer le nom
                //object fileName = @"Mon nouveau document.doc";
                ////// Sauver le document
                ////doc.SaveAs(ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing);
                //// Fermer le document
                //doc.Close(ref missing, ref missing, ref missing);


                //// Fermeture de word
                //msWord.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void GenererContratDepot()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates+"ContratDepot2.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                msWord.Activate();
                doc.Activate();

                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;


                myBookmark = bookmarks["NomTypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType.ToUpper();

                myBookmark = bookmarks["NomCompletClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


                myBookmark = bookmarks["AdjectifNaissanceClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "né" : "née";

                myBookmark = bookmarks["DateNaissance"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.DateDeNaissance.Value.Date.ToShortDateString();

                myBookmark = bookmarks["LieuNaissance"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.LieuDeNaissance;

                myBookmark = bookmarks["AdresseClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;

                myBookmark = bookmarks["Nationalite"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Nationalite;

                myBookmark = bookmarks["SeuilSouscription"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeContrat.SeuilSouscription.ToString();

                myBookmark = bookmarks["SujetClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "il" : "elle";

                myBookmark = bookmarks["ArticlePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.TypePieceIdentite == TypePieceIdentite.CNI ? "de la" : "du";

                myBookmark = bookmarks["TypePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.TypePieceIdentite == TypePieceIdentite.CNI ? "Carte nationale d'identité" : "passeport";

                myBookmark = bookmarks["NumeroPieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NumeroPieceIdentification;

                myBookmark = bookmarks["DatePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.DateDeDelivrancePiece.HasValue ? leClientEncours.DateDeDelivrancePiece.Value.ToShortDateString() : "";

                myBookmark = bookmarks["SeuilEntreeEnVigueur"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeContrat.SeuilEntreeEnVigueur.ToString();

                myBookmark = bookmarks["CodeTypeVilla"];
                bookmarkRange = myBookmark.Range;
                if (leContratEnCours.Lot.PositionLot != PositionLot.Angle)
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType;
                else
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType + "(angle)";


                //myBookmark = bookmarks["NumeroLot"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                //myBookmark = bookmarks["CodeTypeVilla"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType;


                //myBookmark = bookmarks["SuperficieReelle"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");


                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                myBookmark = bookmarks["PrixDeVenteEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)leContratEnCours.PrixFinal);


                //myBookmark = bookmarks["TypeEcheancier"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.TypeEcheancier.Value.ToString();

                //myBookmark = bookmarks["TypeEcheancier2"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.TypeEcheancier.Value.ToString();

                //myBookmark = bookmarks["TypeEcheancier3"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.TypeEcheancier.Value.ToString();

                myBookmark = bookmarks["NombreEcheances"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.NbEcheances.ToString();


                var nombreDeMois = 0;
                switch (leContratEnCours.TypeEcheancier)
                {
                    case TypeEcheancier.Mensuel:
                        nombreDeMois =1;
                        break;
                    case TypeEcheancier.Trimestriel:
                        nombreDeMois = 3;
                        break;
                    case TypeEcheancier.Semestriel:
                        nombreDeMois = 6;
                        break;
                    case TypeEcheancier.Annuel:
                        nombreDeMois = 12;
                        break;
                    default:
                        break;
                }


                myBookmark = bookmarks["NombreAnnees"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = (leContratEnCours.DureeDepot).ToString(); ;

                myBookmark = bookmarks["Periodicite1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeEcheancier.Value.ToString().ToLower()+"s";

                myBookmark = bookmarks["Periodicite2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeEcheancier.Value.ToString().ToLower() + "s";

                myBookmark = bookmarks["Periodicite3"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeEcheancier.Value.ToString().ToLower() + "s";

                myBookmark = bookmarks["Periodicite4"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeEcheancier.Value.ToString().ToLower() + "s";

                string NomTypeEcheance = "";
                switch (leContratEnCours.TypeEcheancier)
                {
                    case TypeEcheancier.Mensuel:
                        NomTypeEcheance = "mois";
                        break;
                    case TypeEcheancier.Trimestriel:
                        NomTypeEcheance = "trimestres";
                        break;
                    case TypeEcheancier.Semestriel:
                        NomTypeEcheance = "semestres";
                        break;
                    case TypeEcheancier.Annuel:
                        NomTypeEcheance = "années";
                        break;
                    default:
                        break;
                }
                myBookmark = bookmarks["PeriodiciteNom"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = NomTypeEcheance;

                myBookmark = bookmarks["PeriodiciteNom2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = NomTypeEcheance;

               

                myBookmark = bookmarks["SeuilEntreeEnVigueur2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeContrat.SeuilEntreeEnVigueur.ToString();

                var montantSeuil = leContratEnCours.PrixFinal * leContratEnCours.TypeContrat.SeuilEntreeEnVigueur / 100;
                myBookmark = bookmarks["MontantSeuilReservation"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = montantSeuil.ToString("### ### ###");


                myBookmark = bookmarks["MontantSeuilReservationEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)montantSeuil);



                myBookmark = bookmarks["NumeroDossierBasDePage"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.NumeroContrat;


                myBookmark = bookmarks["NomCompletbasDePage"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;

                myBookmark = bookmarks["DateEdition"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Day + " " + String.Format("{0:y}", DateTime.Now); 


                //if (leContratEnCours.Apporteur == null)
                //{
                //    myBookmark = bookmarks["ClauseApporteurAffaire"];
                //    bookmarkRange = myBookmark.Range;
                //    bookmarkRange.Text = string.Empty;
                //}




                //// Attribuer le nom
                //object fileName = @"Mon nouveau document.doc";
                ////// Sauver le document
                ////doc.SaveAs(ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing);
                //// Fermer le document
                //doc.Close(ref missing, ref missing, ref missing);


                //// Fermeture de word
                //msWord.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {

                throw;
            }
        }



        private void GenererConversionDepotContratReservationResa()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates+"ConversionDepotEnContratReservation.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                msWord.Activate();
                doc.Activate();
                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;

                myBookmark = bookmarks["NomCompletClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


                myBookmark = bookmarks["AdjectifNaissanceClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "né" : "née";

                if (leClientEncours.DateDeNaissance!=null)
                {
                    myBookmark = bookmarks["DateNaissance"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.DateDeNaissance.Value.Date.ToShortDateString(); 
                }

                myBookmark = bookmarks["LieuNaissance"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.LieuDeNaissance;

                myBookmark = bookmarks["AdresseClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;

                myBookmark = bookmarks["Nationalite"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Nationalite;

                myBookmark = bookmarks["SujetClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "il" : "elle";

                myBookmark = bookmarks["ArticlePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.TypePieceIdentite == TypePieceIdentite.CNI ? "de la" : "du";

                myBookmark = bookmarks["TypePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.TypePieceIdentite == TypePieceIdentite.CNI ? "Carte nationale d'identité" : "passeport";

                myBookmark = bookmarks["NumeroPieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NumeroPieceIdentification;

                myBookmark = bookmarks["DatePieceIdentification"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.DateDeDelivrancePiece.HasValue ? leClientEncours.DateDeDelivrancePiece.Value.ToShortDateString() : "";





                myBookmark = bookmarks["DateSouscriptionContratDepot"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.DateSouscription.Value.ToShortDateString();


                myBookmark = bookmarks["SeuilReservation1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeContrat.SeuilEntreeEnVigueur.ToString();


                myBookmark = bookmarks["NomTypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType;


                myBookmark = bookmarks["SuperficieStandard"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.SurfaceDeBase.ToString("###");

                myBookmark = bookmarks["NumeroLot"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                myBookmark = bookmarks["CodeTypeVilla"];
                bookmarkRange = myBookmark.Range;
                if (leContratEnCours.Lot.PositionLot != PositionLot.Angle)
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType;
                else
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType + "(angle)";


                myBookmark = bookmarks["SuperficieReelle"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");


                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                myBookmark = bookmarks["PrixDeVenteEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)leContratEnCours.PrixFinal);

                if (leContratEnCours.Apporteur == null)
                {
                    myBookmark = bookmarks["ClauseApporteurAffaire"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = string.Empty;
                }

                myBookmark = bookmarks["SeuilReservation"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.TypeContrat.SeuilEntreeEnVigueur.ToString();

                myBookmark = bookmarks["PourcentageRestantApresReservation"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = (100-leContratEnCours.TypeContrat.SeuilEntreeEnVigueur).ToString();

                var montantVerse = leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal);
                myBookmark = bookmarks["MontantPremierVersement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = montantVerse.ToString("### ### ###");


                myBookmark = bookmarks["MontantDepotDeGarantie"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = (leContratEnCours.PrixFinal*5/100).ToString("### ### ###");


                myBookmark = bookmarks["MontantAccompteSurPrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text =( montantVerse - (leContratEnCours.PrixFinal * 5 / 100)).ToString("### ### ###");


                myBookmark = bookmarks["DateContrat"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Date.ToShortDateString();

                myBookmark = bookmarks["NomCompletbasDePage"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;

                myBookmark = bookmarks["NumeroDossierBasDePage"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.NumeroContrat;

                myBookmark = bookmarks["NumeroLotBasDePage"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;


                myBookmark = bookmarks["NombrePieces"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.ImageVilla!=null? leContratEnCours.Lot.TypeVilla.ImageVilla:"";


                myBookmark = bookmarks["PourcentageRestantDroitReservation"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = (leContratEnCours.TypeContrat.SeuilEntreeEnVigueur - 5) + "%";

                myBookmark = bookmarks["SemestreLivraison"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = Tools.Tools.GetSemestre(leContratEnCours.DateLivraisonLot.Value.Month) + " " + leContratEnCours.DateLivraisonLot.Value.Year;

                //// Attribuer le nom
                //object fileName = @"Mon nouveau document.doc";
                ////// Sauver le document
                ////doc.SaveAs(ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing);
                //// Fermer le document
                //doc.Close(ref missing, ref missing, ref missing);


                //// Fermeture de word
                //msWord.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {

                throw;
            }
        }





        private void cmdImprimerContratDepot_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                {
                    GenererContratDepot();
                    GenererFicheDemandeContratReservation();
                }
                //else
                //GenererContratReservationDepot();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                     "Prosopis - Edition de contrat de dépôt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProspect childForm = new FrmProspect(leClientEncours);
                //childForm.MdiParent = this.MdiParent;
                childForm.StartPosition = FormStartPosition.CenterScreen;
               
                childForm.ShowDialog();
                childForm.WindowState = FormWindowState.Normal;
                leClientEncours = clientRep.GetClient(leClientEncours.ID);
                AffichierClient(leClientEncours);


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void chkContratValide_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chkContratValide.Checked && !leContratEnCours.ContratValide)
            {
                if (!Tools.Tools.AgentEnCours.IsChefEquipe)
                {
                    //MessageBox.Show(this, "Désolé seuls les chefs d'équipe ont le droit ",
                    //    "Enregistrement de la validation du contrat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if(MessageBox.Show(this,"Voulez vous réellement valider ce contrat de réservation? Attention cette opération ne pourra plus être annulée par la suite.",
                    "Enregistrement de la validation du contrat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes)
                {
                    contratRep.ValiderContrat(leContratEnCours.Id);
                    AfficherLeContrat();
                }
            }
        }

        private void cmdAttestationSoldeToutCompte_Click(object sender, EventArgs e)
        {
            try
            {
                if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                    GenererAttestationSoldeToutCompte();
                else
                    GenererAttestationSoldeToutCompte();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                     "Prosopis -  Edition de contrat de réservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void GenererAttestationSoldeToutCompte()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "AttestationSoldeToutCompte.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);

                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;

                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;



                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;

                myBookmark = bookmarks["Ville"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Pays;

              


              

                myBookmark = bookmarks["NumeroLot"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

               
                myBookmark = bookmarks["Superficie"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");


                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                myBookmark = bookmarks["PrixDeVenteEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)leContratEnCours.PrixFinal);

               
                myBookmark = bookmarks["DateSoldeToutCompte"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Date.ToShortDateString();

                //// Attribuer le nom
                //object fileName = @"Mon nouveau document.doc";
                ////// Sauver le document
                ////doc.SaveAs(ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing);
                //// Fermer le document
                //doc.Close(ref missing, ref missing, ref missing);


                //// Fermeture de word
                //msWord.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dtpDateVersement_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkContratDepotValide_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chkContratDepotValide.Checked && !leContratEnCours.ContratDepotValide)
            {
                if (!Tools.Tools.AgentEnCours.IsChefEquipe)
                {
                    //MessageBox.Show(this, "Désolé seuls les chefs d'équipe ont le droit ",
                    //    "Enregistrement de la validation du contrat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    chkContratDepotValide.Checked = false;
                    return;
                }
                if (MessageBox.Show(this, "Voulez vous réellement valider le contrat de dépot? Attention cette opération ne pourra plus être annulée par la suite.",
                    "Enregistrement de la validation du contrat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    contratRep.ValiderContratDepot(leContratEnCours.Id);
                    AfficherLeContrat();
                }
            }
        }

        private void cmdTableauAmortissements_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                cmdTableauAmortissements.Enabled = false;
                lbTableauAmortissement.Enabled = false;
                GenererTableauAmortissement();
                cmdTableauAmortissements.Enabled = true;
                lbTableauAmortissement.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                     "Prosopis - Génération du tableau d'amortissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void GenererTableauAmortissement()
        {
            try
            {

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                //if (!bPrint)
                xlApp.Visible = false;
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                string dossierTemplates = Tools.Tools.DossierTemplates;
               // xlWorkBook.Activate();
                xlWorkBook = xlApp.Workbooks.Open(dossierTemplates + "TableauAmortissementsBis.xlsx", 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                //excelApp.Workbooks.Open(workbookPath,                                                 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows                       , "",  true, false, 0, true, false, false);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                leClientEncours = clientRep.GetClient(leClientEncours.ID);
                leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);

                xlApp.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
                xlWorkSheet.Cells[2, 2] = leClientEncours.NomComplet;
                xlWorkSheet.Cells[3, 2] = leContratEnCours.Lot.TypeVilla.NomType;
                xlWorkSheet.Cells[4, 2] = leContratEnCours.PrixFinal.ToString("### ### ###");
                xlWorkSheet.Cells[5, 2] = leContratEnCours.DureeDepot;

                //xlWorkSheet.Cells[8, 3] = leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal);
                xlWorkSheet.Cells[8, 3] = leContratEnCours.Factures.Where(fact => fact.TypeFacture == TypeFacture.DepotMinimum).First().Montant;
                xlWorkSheet.Cells[8, 3].Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = 3d;
                xlWorkSheet.Cells[8, 3].Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 3d;
                xlWorkSheet.Cells[8, 3].Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                xlWorkSheet.Cells[8, 3].Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;
                var lesAnnees = leContratEnCours.Factures.GroupBy(fact => fact.DateEcheanceFacture.Value.Year).Select(fact => fact.First().DateEcheanceFacture.Value.Year);
                int i = 0;
               
                foreach (var annee in lesAnnees)
                {
                    xlWorkSheet.Cells[7, 3+i] = annee;
                    for(int j=7; j <21;j++ )
                    {
                        xlWorkSheet.Cells[j, 3 + i].Activate();
                        xlWorkSheet.Cells[j, 3 + i].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[j, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = 3d;
                        xlWorkSheet.Cells[j, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 3d;
                        xlWorkSheet.Cells[j, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                        xlWorkSheet.Cells[j, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;

                    }

                    var lesMois = leContratEnCours.Factures.Where(fact =>fact.TypeFacture== TypeFacture.Echeance && fact.DateEcheanceFacture.Value.Year == annee).OrderBy(fact => fact.DateEcheanceFacture.Value.Month);
                   
                    foreach (var mois in lesMois)
                    {
                        xlWorkSheet.Cells[mois.DateEcheanceFacture.Value.Month + 8, 3 + i].Activate();
                        xlWorkSheet.Cells[mois.DateEcheanceFacture.Value.Month +8, 3 + i] = mois.Montant;
                        //xlWorkSheet.Cells[mois.DateEcheanceFacture.Value.Month + 8, 3 + i].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        //xlWorkSheet.Cells[mois.DateEcheanceFacture.Value.Month + 8, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = 3d;
                        //xlWorkSheet.Cells[mois.DateEcheanceFacture.Value.Month + 8, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 3d;
                        //xlWorkSheet.Cells[mois.DateEcheanceFacture.Value.Month + 8, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 3d;
                        //xlWorkSheet.Cells[mois.DateEcheanceFacture.Value.Month + 8, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;
                        //j++;
                    }
                    xlWorkSheet.Cells[22, 3 + i].Activate();
                    xlWorkSheet.Cells[22, 3 + i].Formula ="=Sum(" + xlWorkSheet.Cells[9, 3 + i].Address + ":" + xlWorkSheet.Cells[20, 3 + i].Address + ")";
                    xlWorkSheet.Cells[22, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[22, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                    xlWorkSheet.Cells[22, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = 3d;
                    xlWorkSheet.Cells[22, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 3d;
                    xlWorkSheet.Cells[21, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[21, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                    xlWorkSheet.Cells[21, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = 3d;
                    xlWorkSheet.Cells[21, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 3d;

                    if (i == 0)
                    {
                        xlWorkSheet.Cells[24, 3 + i].Formula = "=" + xlWorkSheet.Cells[8, 3 + i].Address + "+" + xlWorkSheet.Cells[22, 3 + i].Address ;
                    }
                    else
                    {
                        xlWorkSheet.Cells[24, 3 + i].Formula = "=" + xlWorkSheet.Cells[24, 3 + i-1].Address + "+" + xlWorkSheet.Cells[22, 3 + i].Address;
                    }
                    xlWorkSheet.Cells[24, 3 + i].Activate();
                    xlWorkSheet.Cells[24, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[24, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[24, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[24, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = 3d;
                    xlWorkSheet.Cells[24, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 3d;
                    xlWorkSheet.Cells[24, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 3d;

                    xlWorkSheet.Cells[23, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[23, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                    xlWorkSheet.Cells[23, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = 3d;
                    xlWorkSheet.Cells[23, 3 + i].Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 3d;

                 
                    i++;
                }
                var signature = "Je soussigné, " + leContratEnCours.Client.NomComplet;
                if (leContratEnCours.Client.TypePieceIdentite!=null)
                {
                    signature+= ", titulaire";
                    if (leContratEnCours.Client.TypePieceIdentite.Value == TypePieceIdentite.CNI)
                        signature += " de la carte nationale d'identité ";
                    else
                        signature += " du passport" ;

                    signature += " N° " + leContratEnCours.Client.NumeroPieceIdentification + " du " + leContratEnCours.Client.DateDeDelivrancePiece.Value.ToShortDateString();
                }
                signature+= ", m'engage à respecter cet échéancier et à respecter le contrat de dépôt.";

                xlWorkSheet.Cells[26, 1] = signature;

                xlApp.Visible = true;
            }
        catch (Exception)
        {

            throw;
        }
     }

        private void lbTableauAmortissement_Click(object sender, EventArgs e)
        {
            cmdTableauAmortissements_Click(sender, e);
        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void txtCompteTiers_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbCommercial_Click(object sender, EventArgs e)
        {

        }

        private void cmdReaffecter_Click(object sender, EventArgs e)
        {
            FrmAffectationCommercial frmAffecterComm = new FrmAffectationCommercial(ref leClientEncours, true, "Client");
            frmAffecterComm.WindowState = FormWindowState.Normal;
            frmAffecterComm.StartPosition = FormStartPosition.CenterParent;
            frmAffecterComm.ShowDialog();
            AffichierClient(leClientEncours);
        }

        private void cmdAvenant_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(this, "Voulez vous réellement éditer un avenant sur ce contrat " + leContratEnCours.NumeroContrat + "?",
                   "Prosopis - Edition avenant", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (MessageBox.Show(this, "Merci de confirmer!!!" ,
                         "Prosopis - Edition avenant", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        using (var scope = new TransactionScope())
                        {
                            using (var db = new SenImmoDataContext())
                            {
                                // Rectrouver le contrat et le client
                                var contrat = db.Contrats.Where(cont => cont.Id == leContratEnCours.Id).SingleOrDefault();
                                var client = db.Clients.Find(contrat.ClientID);

                                //Transferer l'ensemble des encaissements clients dans le compte prospect
                                foreach (var encG in contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD"))
                                {
                                    //Vérifier si l'encaissement n'est pas déjà présent dans le compte prospect
                                    if(!client.EncaissementProspects.Any(encPros => encPros.DateEncaissement==encG.DateEncaissement && encPros.MontantGlobal==encG.MontantGlobal))
                                    {
                                        var versement = new EncaissementProspect()
                                        {
                                            NumeroEncaissement = "ENPR" + client.ID.ToString().PadLeft(4, '0') + encG.DateEncaissement.Value.Month.ToString().PadLeft(2, '0') + encG.DateEncaissement.Value.Year.ToString().Substring(2, 2),
                                            DateEncaissement = encG.DateEncaissement.Value.Date,
                                            MontantGlobal = encG.MontantGlobal,
                                            ProspectId = client.ID,
                                            ModePaiement = encG.ModePaiement,
                                            ReferencePaiement = encG.ReferencePaiement,
                                            Commentaire = encG.Commentaire+"Transfert avenant sur le contrat"+ contrat.NumeroContrat
                                        };

                                        db.EncaissementProspects.Add(versement);
                                    }
                                   
                                }
                                //S'assurer que toutes les options antécédemment prises par ce client sont désactivées
                                var optionsProspects = db.Options.Where(opt => opt.ClientID == client.ID && opt.Active==true);
                                if(optionsProspects.Count() >0)
                                {
                                    foreach (Option option in optionsProspects)
                                    {
                                        option.Active = false;
                                    }
                                }

                                //Reinitialiser tous les encaissements prospect
                                foreach (EncaissementProspect encP in client.EncaissementProspects)
                                {
                                    encP.Deverse = false;
                                    encP.AtteinteSeuil = false;
                                }

                                // Mettre le statut du contrat à "Désisté pour avenant"
                                contrat.Statut = StatutContrat.Désisté;

                                //Remettre le lot à libre
                                if (contrat.Lot != null)
                                    contrat.Lot.StatutLot = StatutLot.Libre;

                                // Mettre le statut du client à "Desisté"
                                // vérifier si le client a d'autres contrat, auquel cas laisser son statut par rapport à l'autre contrat, sinon flaguer le client comme résilié
                                //if (contrat.Client.Contrats.Where(cont => cont.Statut == StatutContrat.Actif).Count() <= 0)
                                client.Type = TypeClient.ProspectSansOption;


                                db.SaveChanges();
                            }
                            scope.Complete();
                        }
                        MessageBox.Show(this, "Le contrat "+leContratEnCours.NumeroContrat+" a été résilié et le solde transféré dans le compte prospect de "+leClientEncours.NomComplet
                            , "Prosopis - Edition avenant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (MessageBox.Show(this, "Voulez vous configurer les paramètres de la nouvelle option en vue de définir l'avenant au contrat ?",
                                     "Prosopis - Edition avenant", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {

                            FrmOptionProspect frmOption = new FrmOptionProspect(leClientEncours);
                            frmOption.WindowState = FormWindowState.Normal;
                            frmOption.StartPosition = FormStartPosition.CenterParent;
                            frmOption.ShowDialog();

                            if (MessageBox.Show(this, "Voulez vous créer la vente relative à l'avenant au contrat ?",
                                    "Prosopis - Edition avenant", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                FrmVente frmVD = new FrmVente(leClientEncours, false,leContratEnCours);
                                frmVD.WindowState = FormWindowState.Normal;
                                frmVD.StartPosition = FormStartPosition.CenterParent;
                                frmVD.ShowDialog();

                                //AffichierClient(leClientEncours);
                                //ChargerLesContrats(leClientEncours);

                                //cmbContrats.DisplayMember = "NumeroLot";
                                this.Close();
                            }
                            else
                                this.Close();
                        }
                        else
                            this.Close();



                        //txtTypeContrat.Text = string.Empty;
                        //txtClient.Text = string.Empty;
                        //txtTotalEncaisse.Text = string.Empty;
                        //txtTypeVilla.Text = string.Empty;
                        //txtLot.Text = string.Empty;
                        //txtPrixDeVente.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FrmDossierClient_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.F10)
            {
                txtIdClient.Visible = true;
                txtContratId.Visible = true;
                txtNumeroDossier.Visible = true;
            }

            if (e.KeyCode == Keys.F11)
            {
                txtIdClient.Visible = false;
                txtContratId.Visible = false;
                txtNumeroDossier.Visible = false;
            }
        }

        private void lvFactures_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvFactures.SelectedItems.Count > 0)
                {
                    int factureId = (int)lvFactures.SelectedItems[0].Tag ;
                    var facture = contratRep.GetFactureById(factureId);
                    dgRepartitionEncaissementsFacture.DataSource = facture.Encaissements.ToList().Select
                        (enc => new
                        {
                            Id = enc.ID,
                            Date = enc.Date.Value.Date,
                           
                            Lettré = enc.Montant,
                            Référence = enc.ReferencePaiement,
                            Encaissé = enc.EncaissementGlobal.MontantGlobal,

                        }).ToList();

                    dgRepartitionEncaissementsFacture.Columns[0].Width = 0;
                    dgRepartitionEncaissementsFacture.Columns[0].Visible = false;
                    dgRepartitionEncaissementsFacture.Columns[1].Width = 80;
                    dgRepartitionEncaissementsFacture.Columns[2].Width = 80;

                    dgRepartitionEncaissementsFacture.Columns[2].DefaultCellStyle.Format = "### ### ###";
                    dgRepartitionEncaissementsFacture.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgRepartitionEncaissementsFacture.Columns[4].Width = 80;
                    dgRepartitionEncaissementsFacture.Columns[4].DefaultCellStyle.Format = "### ### ###";
                    dgRepartitionEncaissementsFacture.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgRepartitionEncaissementsFacture.Columns[3].Width = 250;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtContratId_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class MiniContrat
    {
        public int ID { get; set; }
        public string NumeroLot { get; set; }
        
    }
}
