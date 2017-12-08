using prjSenImmoWinform.Models;
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
namespace prjSenImmoWinform
{
    public partial class FrmListeProspects : Form
    {
        private ClientRepository clientRep;
        private ContratRepository contratRep;
        private Client LeClientEnCours;
        private CommercialRepository commercialRep;

        private ColumnHeader SortingColumn = null;

        private Projet LeProjetEncours;

        public bool BChargement { get; private set; }

        public FrmListeProspects()
        {
            //LeProjetEncours = Tools.Tools.ProjetEnCours;
            InitializeComponent();
            cmdAffecterCommercial.Visible = true;
            pCommercial.Visible = false;
            tcDossierProspect.Visible = false;
            pActionProspect.Visible = false;
            cmdFicheNotaire.Visible = false;
            clientRep = new ClientRepository();
            contratRep = new ContratRepository();
            commercialRep = new CommercialRepository();
            ChargerLesProjets();
            ChargerLesProspect();
            ChargerCommerciaux();
            cmbOrigines.DataSource = clientRep.GetAllTypeOrigines().ToList();
            cmbOrigines.DisplayMember = "LibelleTypeOrigine";
            cmbOrigines.SelectedIndex = -1;
            txtPrenomRecherche.Focus();

            dgProspects.ColumnHeadersDefaultCellStyle.BackColor = Color.Lavender;
            dgProspects.EnableHeadersVisualStyles = false;
            dgProspects.ColumnHeadersHeight = 25;
            if (Tools.Tools.AgentEnCours.Role.CodeRole == "MKT" || Tools.Tools.AgentEnCours.Role.CodeRole == "DSK")
            {
                pFiltreGlobal.Visible = false;
                pFiltreMarketing.Visible = true;
            }
            else
            {
                pFiltreGlobal.Visible = true;
                pFiltreMarketing.Visible = false;
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                {
                    chkNonAffecteGlobal.Visible = false;
                    picGris.Visible = false;
                }
            }
            if (Tools.Tools.AgentEnCours.Role.CodeRole == "DC")
            {
                cmdNouveauProspect.Enabled = false;
                cmdFermer.Enabled = true;
            }
            txtPrenomRecherche.Focus();
        }

        public FrmListeProspects(string strInitialisation) : this()
        {
            if (strInitialisation == "NouveauxContrats")
            {
                var listeProspect = clientRep.GetAllProspects().ToList().Where(
                                                                                   pros => pros.Options.Where(op => op.Active == true && op.SeuilContratAtteint == true && op.ContratGenere == false).Count() > 0
                                                                                ).ToList();
                //if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                //{
                //    if(Tools.Tools.AgentEnCours.IsChefEquipe)
                //        listeProspect = listeProspect.Where(pros => pros.Commercial.ChefEquipeId == Tools.Tools.AgentEnCours.Id).ToList();
                //    else
                //        listeProspect = listeProspect.Where(pros => pros.CommercialID == Tools.Tools.AgentEnCours.Id).ToList();
                //}
                //AfficherLaListeDesProspects(listeProspect);
                AfficherLaListeDesProspectsListview(listeProspect);
            }
            else if (strInitialisation == "NouveauxProspects")
            {
                AfficherNouveauxProspects();


            }
            txtPrenomRecherche.Focus();
        }

        private void ChargerLesProjets()
        {

            try
            {
                var lesProjets = contratRep.GetProjets();


                cmbProjets.DataSource = lesProjets.ToList();
                cmbProjets.DisplayMember = "DenominationProjet";
                cmbProjets.ValueMember = "Id";
                // cmbProjets.SelectedValue = leProjetEnCours;
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" || Tools.Tools.AgentEnCours.Role.CodeRole == "DC")
                {
                    cmbProjets.SelectedValue = Tools.Tools.AgentEnCours.ProjetId;
                    LeProjetEncours = Tools.Tools.AgentEnCours.Projet;
                    cmbProjets.Enabled = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void AfficherNouveauxProspects()
        {
            var listeProspect = clientRep.GetAllProspects().ToList();
            if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
            {
                if (Tools.Tools.AgentEnCours.IsChefEquipe)
                    listeProspect = listeProspect.Where(pros => pros.Commercial != null && pros.Commercial.ChefEquipeId == Tools.Tools.AgentEnCours.Id && pros.ProspectEdite == false).ToList();
                else
                    listeProspect = listeProspect.Where(pros => pros.CommercialID == Tools.Tools.AgentEnCours.Id && pros.ProspectEdite == false).ToList();

            }
            // AfficherLaListeDesProspects(listeProspect);
            AfficherLaListeDesProspectsListview(listeProspect);
        }

        private void ChargerLesProspect()
        {
            try
            {
                var listeProspect = clientRep.GetAllProspects().ToList();
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                {

                    if (Tools.Tools.AgentEnCours.IsChefEquipe)
                        listeProspect = listeProspect.Where(pros => pros.Commercial != null && pros.Commercial.ChefEquipeId == Tools.Tools.AgentEnCours.Id).ToList();
                    else
                    {
                        listeProspect = listeProspect.Where(pros => pros.CommercialID == Tools.Tools.AgentEnCours.Id).ToList();
                        chkCommercial.Visible = false;
                    }
                }

                //AfficherLaListeDesProspects(listeProspect);
                AfficherLaListeDesProspectsListview(listeProspect);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors du chargement des prospects:..." + ex.Message,
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgClients_SelectionChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (dgProspects.SelectedRows.Count > 0)
            //    {
            //        int idClient = (int)dgProspects.SelectedRows[0].Cells[0].Value;
            //        LeClientEnCours = clientRep.GetClient(idClient);

            //        if (LeClientEnCours != null)
            //            AfficherProspect(LeClientEnCours);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, "Erreur:..." + ex.Message,
            //            "Senimo - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        private void AfficherProspect(Client client)
        {
            txtDateSouscription.Text = client.DateCreation.ToShortDateString();
            //txtOrigine.Text = client.Origine != TypeOrigine.Autre ? client.Origine.ToString() : client.AutreOrigine;
            txtPrenom.Text = client.Prenom;
            txtNom.Text = client.Nom;
            txtDateNaissance.Text = client.DateDeNaissance != null ? client.DateDeNaissance.Value.Date.ToShortDateString() : "";
            txtLieuNaissance.Text = client.LieuDeNaissance;
            txtAdresse.Text = client.Adresse;
            Genre sexe = client.Genre;
            if (sexe == Genre.Masculin)
                rbHomme.Checked = true;
            else
                rbFemme.Checked = true;

            txtNationalite.Text = client.Nationalite;
            txtNumeroFixe.Text = client.TelephoneFixe;
            txtNumeroMobile.Text = client.Mobile1;
            txtOrigine.Text = client.Origine != null ? client.Origine.LibelleTypeOrigine : "";

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
            txtDateSouscription.Text = client.DateCreation.ToShortDateString();

            if (client.Commercial != null)
            {
                cmdAffecterCommercial.Visible = false;
                pCommercial.Visible = true;
                txtCommercial.Text = client.Commercial.NomComplet;
                AfficherOptionsProspect(client);
                AfficherActivitesCommercialesProspect(client);
                AfficherEncaissementProspect(client);
                tcDossierProspect.Visible = true;
                pActionProspect.Visible = true;
                cmdFicheNotaire.Visible = true;
            }
            else
            {
                cmdAffecterCommercial.Visible = true;
                pCommercial.Visible = false;
                tcDossierProspect.Visible = false;
                pActionProspect.Visible = false;
                cmdFicheNotaire.Visible = false;
                dgActivitesCommerciales.DataSource = null;
                dgOptions.DataSource = null;
                dgEncaissements.DataSource = null;
            }


        }

        private void AfficherOptionsProspect(Client prospect)
        {
            lbTypeProspect.Text = prospect.Type.ToString();
            txtDateSouscriptionProspect.Text = string.Empty;
            txtTypeContratProspect.Text = string.Empty;
            txtTypeVillaProspect.Text = string.Empty;
            txtPositionLotProspect.Text = string.Empty;
            txtLotProspect.Text = string.Empty;
            txtPrixDeVenteProspect.Text = string.Empty;
            txtTauxEncaissementProspect.Text = string.Empty;
            txtMontantEncaisseProspect.Text = string.Empty;
            chkSeuilContratAtteint.Checked = false;
            txtMontantSeuilContrat.Text = string.Empty;
            cmdVente.Visible = false;
            try
            {
                var option = clientRep.GetOptionsProspect(prospect).FirstOrDefault();
                var encaissementsProspect = contratRep.GetEncaissementProspect(prospect.ID).Where(enc => enc.FraisDeDossier == false).Sum(enc => enc.MontantGlobal);
                txtMontantEncaisseProspect.Text = encaissementsProspect.ToString("### ### ###");
                if (option != null)
                {
                    txtDateSouscriptionProspect.Text = option.DatePriseOption.Value.ToShortDateString();
                    txtTypeContratProspect.Text = option.TypeContrat.LibelleTypeContrat;
                    txtTypeVillaProspect.Text = option.TypeVilla.CodeType;
                    txtPositionLotProspect.Text = option.PositionLot.ToString();
                    txtLotProspect.Text = option.Lot != null ? option.Lot.NumeroLot : "";
                    txtPrixDeVenteProspect.Text = option.PrixDeVente.ToString("### ### ###");
                    txtTauxEncaissementProspect.Text = (encaissementsProspect / option.PrixDeVente * 100).ToString("###.#");

                    txtMontantSeuilContrat.Text = option.TypeContrat.CategorieContrat == CategorieContrat.Réservation ?
                            (option.PrixDeVente * option.TypeContrat.SeuilEntreeEnVigueur / 100).ToString("### ### ###") :
                            (option.PrixDeVente * option.TypeContrat.SeuilSouscription / 100).ToString("### ### ###")
                            ;
                    chkSeuilContratAtteint.Checked = option.SeuilContratAtteint;
                    if (option.SeuilContratAtteint)
                        cmdVente.Visible = true;
                    else
                        cmdVente.Visible = false;

                }


                var options = clientRep.GetOptionsProspect(prospect);
                dgOptions.DataSource = options.ToList().Select(opttion => new
                {
                    ID = opttion.Id,
                    Option = option.TypeContrat.LibelleTypeContrat,
                    Lot = opttion.Lot != null ? opttion.Lot.NumeroLot : "",
                    Type = opttion.TypeVilla.CodeType,
                    Position = opttion.Lot != null ? opttion.Lot.PositionLot : opttion.PositionLot,
                    Prix = opttion.PrixDeVente,
                    DateOption = opttion.DatePriseOption,
                    DateFinOption = opttion.DateFinOption,



                }
                                                   ).ToList();
                dgOptions.Columns[0].Width = 0;
                dgOptions.Columns[1].Width = 80;
                dgOptions.Columns[2].Width = 50;
                dgOptions.Columns[3].Width = 60;
                dgOptions.Columns[4].Width = 80;
                dgOptions.Columns[5].Width = 70;
                dgOptions.Columns[5].DefaultCellStyle.Format = "### ### ###";
                dgOptions.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgOptions.Columns[6].Width = 80;
                dgOptions.Columns[7].Width = 80;
                //dgProspects.Columns[7].Width = 170;
                //dgProspects.Columns[3].HeaderText = "Né(e) le";
                //dgProspects.Columns[4].HeaderText = "à";
                dgOptions.Columns[0].Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfficherActivitesCommercialesProspect(Client client)
        {
            try
            {
                var activitesCommerciales = clientRep.GetActivitesCommercialesProspect(client).OrderBy(act => act.DateActivite).ThenBy(act => act.HeureActivite);
                dgActivitesCommerciales.DataSource = activitesCommerciales.ToList().Select(act => new
                {
                    ID = act.Id,
                    Activité = act.TypeActivite != TypeActivite.Autre ? act.TypeActivite.ToString() : act.AutreTypeActivite,
                    Date = act.DateActivite,
                    Heure = act.HeureActivite.ToString().Substring(0, 5),
                    Détails = act.Commentaire




                }
                                                   ).ToList();
                dgActivitesCommerciales.Columns[0].Width = 0;
                dgActivitesCommerciales.Columns[1].Width = 120;
                dgActivitesCommerciales.Columns[2].Width = 100;
                dgActivitesCommerciales.Columns[3].Width = 80;
                dgActivitesCommerciales.Columns[3].DefaultCellStyle.Format = "HH:mm";
                dgActivitesCommerciales.Columns[4].Width = 200;
                //dgActivitesCommerciales.Columns[5].Width = 100;
                //dgActivitesCommerciales.Columns[6].Width = 100;
                //dgProspects.Columns[7].Width = 170;
                //dgProspects.Columns[3].HeaderText = "Né(e) le";
                //dgProspects.Columns[4].HeaderText = "à";
                dgActivitesCommerciales.Columns[0].Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfficherEncaissementProspect(Client prospect)
        {
            try
            {
                var encaissements = clientRep.GetEncaissementsProspect(prospect.ID);
                dgEncaissements.DataSource = encaissements.ToList().Select(enc => new
                {
                    ID = enc.ID,
                    Date = enc.DateEncaissement,
                    Numéro = enc.NumeroEncaissement,
                    Motif = enc.ReferencePaiement,
                    Montant = enc.MontantGlobal

                }
                                                   ).ToList();
                dgEncaissements.Columns[0].Width = 0;
                dgEncaissements.Columns[1].Width = 80;
                dgEncaissements.Columns[2].Width = 100;
                dgEncaissements.Columns[3].Width = 250;
                dgEncaissements.Columns[4].Width = 80;
                dgEncaissements.Columns[4].DefaultCellStyle.Format = "### ### ###";

                //dgProspects.Columns[3].HeaderText = "Né(e) le";
                //dgProspects.Columns[4].HeaderText = "à";
                dgEncaissements.Columns[0].Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void dgClients_DoubleClick(object sender, EventArgs e)
        {
            if (dgProspects.SelectedRows.Count > 0)
            {
                //int idClient = (int)dgClients.SelectedRows[0].Cells[0].Value;
                //var client = db.Clients.Where(c => c.ID == idClient).SingleOrDefault();
                //FrmProspect frmCli = new FrmProspect(client);
                //frmCli.Show();
            }
        }

        private void cmdNouveauClient_Click(object sender, EventArgs e)
        {
            FrmProspect frmCli = new FrmProspect();
            frmCli.Show();
        }

        private void cmdNouveauProspect_Click(object sender, EventArgs e)
        {
            try
            {

                FrmProspect frmPros = new FrmProspect();
                frmPros.StartPosition = FormStartPosition.CenterScreen;
                frmPros.ShowDialog();

                ChargerLesProspect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                     "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtNomRecherche_Validated(object sender, EventArgs e)
        {
            //cmdRechercher_Click(sender, e);
        }

        private void cmdRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                BChargement = true;
                //AfficherLaListeDesProspects(clientRep.GetProspects(txtNomRecherche.Text, txtPrenomRecherche.Text, txtTelephoneRecherche.Text, txtEmailRecherche.Text));
                AfficherLaListeDesProspectsListview(clientRep.GetProspects(txtNomRecherche.Text, txtPrenomRecherche.Text, txtTelephoneRecherche.Text, txtEmailRecherche.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors du chargement de la recherche de prospects:..." + ex.Message,
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtNomRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //cmdRechercher_Click(sender, e);
            }
        }

        private void txtPrenomRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //cmdRechercher_Click(sender, e);
            }
        }

        private void AfficherLaListeDesProspects(IEnumerable<Client> Clients)
        {
            try
            {
                Clients = Clients.OrderBy(pros => pros.DateSouscription).ToList();
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                {
                    if (Tools.Tools.AgentEnCours.IsChefEquipe)
                        Clients = Clients.ToList().Where(pros => pros.Commercial != null && pros.Commercial.ChefEquipeId == Tools.Tools.AgentEnCours.Id).ToList();
                    else
                        Clients = Clients.ToList().Where(pros => pros.CommercialID == Tools.Tools.AgentEnCours.Id).ToList();
                }



                if (chkCommercial.Checked)
                {
                    if (cmbCommercial.SelectedItem != null)
                    {
                        Agent leCommercial = (Agent)cmbCommercial.SelectedItem;



                        Clients = Clients.Where(pros => pros.CommercialID == leCommercial.Id).ToList();


                    }
                }
                dgProspects.DataSource = Clients.OrderBy(p => p.Nom).OrderBy(p => p.Prenom).Select
                                                   (p => new
                                                   {
                                                       ID = p.ID,
                                                       Prénom = p.Prenom,
                                                       Nom = p.Nom,
                                                       DateNaissance = (p.DateDeNaissance != null && p.DateDeNaissance.Value.ToShortDateString() != "01/01/1900") ? p.DateDeNaissance.Value.ToShortDateString() : "",
                                                       LieuNaissance = p.LieuDeNaissance,
                                                       Adresse = p.Adresse,
                                                       Mobile = p.Mobile1,
                                                       Email = p.Email,
                                                       Compte = p.CompteTiers


                                                   }
                                                   ).ToList();
                dgProspects.Columns[0].Width = 0;
                dgProspects.Columns[1].Width = 250;
                dgProspects.Columns[2].Width = 100;
                dgProspects.Columns[3].Width = 80;
                dgProspects.Columns[4].Width = 132;
                dgProspects.Columns[5].Width = 200;
                dgProspects.Columns[6].Width = 80;
                dgProspects.Columns[7].Width = 170;
                dgProspects.Columns[3].HeaderText = "Né(e) le";
                dgProspects.Columns[4].HeaderText = "à";
                dgProspects.Columns[0].Visible = false;
                txtNbProspects.Text = Clients.Count().ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfficherLaListeDesProspectsListview(IEnumerable<Client> Clients)
        {
            try
            {
                Clients = Clients.ToList();
                if (cmbProjets.SelectedItem != null)
                {
                    Projet leProjet = (Projet)cmbProjets.SelectedItem;
                    if (leProjet != null)
                    { 
                        Clients = Clients.Where(pros => pros.ProjetId == leProjet.Id).ToList();
                        if(leProjet.Id==2)
                        {
                            if (rbConstructionVilla.Checked)
                                Clients = Clients.Where(pros => pros.Options.Where(opt => opt.Active == true).FirstOrDefault()?.TypeContrat.TypeConstruction == TypeConstruction.Villa);
                            else
                               if (rbConstructionAppartement.Checked)
                                Clients = Clients.Where(pros => pros.Options.Where(opt => opt.Active == true).FirstOrDefault()?.TypeContrat.TypeConstruction == TypeConstruction.Appartement);
                        }
                    }

                }
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                {
                    if (Tools.Tools.AgentEnCours.IsChefEquipe)
                        Clients = Clients.ToList().Where(pros => pros.Commercial != null && pros.Commercial.ChefEquipeId == Tools.Tools.AgentEnCours.Id).ToList();
                    else
                        Clients = Clients.ToList().Where(pros => pros.CommercialID == Tools.Tools.AgentEnCours.Id).ToList();
                }
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "MKT" || Tools.Tools.AgentEnCours.Role.CodeRole == "DSK")
                {
                    lvProspects.Columns[5].Text = "Origine";
                }
                if (chkCommercial.Checked)
                {
                    if (cmbCommercial.SelectedItem != null)
                    {
                        Agent leCommercial = (Agent)cmbCommercial.SelectedItem;
                        if (leCommercial.IsChefEquipe == false)
                            Clients = Clients.Where(pros => pros.CommercialID == leCommercial.Id).ToList();
                        else
                            Clients = Clients.ToList().Where(pros => pros.Commercial != null && pros.Commercial.ChefEquipeId == leCommercial.Id).ToList();
                    }
                }


               

                if (cmbOrigines.SelectedItem != null)
                    {
                        TypeOrigine lOrigine = (TypeOrigine)cmbOrigines.SelectedItem;
                        if (lOrigine!=null)
                            Clients = Clients.Where(pros => pros.Origine.TypeOrigineId == lOrigine.TypeOrigineId).ToList();
                    }

                if (Tools.Tools.AgentEnCours.Role.CodeRole == "MKT" || Tools.Tools.AgentEnCours.Role.CodeRole == "DSK")
                {
                    if (chkAffecte.Checked)
                    {
                        Clients = Clients.Where(pros => pros.ProspectAffecte == true);
                    }
                    if (chkNonAffecte.Checked)
                    {
                        Clients = Clients.Where(pros => pros.ProspectAffecte == false);
                    }
                }
                else
                {

                    if (chkNonAffecteGlobal.Checked)
                    {
                        Clients = Clients.Where(pros => pros.ProspectAffecte == false);
                    }
                    if (chkNonTraitesGlobal.Checked)
                    {
                        Clients = Clients.Where(pros => pros.ProspectAffecte == true && pros.ProspectEdite == false);
                    }
                    if (chkTraitesGlobal.Checked)
                    {
                        Clients = Clients.Where(pros => pros.ProspectAffecte == true && pros.ProspectEdite == true);
                    }
                    if (chkSeuilAtteintGlobal.Checked)
                    {
                        Clients = Clients.Where(pros => pros.Options != null && pros.Options.Where(opt => opt.Active == true).Count() > 0
                                                        && pros.Options.Where(opt => opt.Active == true).FirstOrDefault()
                                                        .SeuilContratAtteint == true && pros.Options.Where(opt => opt.Active == true).FirstOrDefault()
                                                        .ContratGenere == false);
                    }
                }
                if (rbOrigineDesk.Checked)
                {
                    Clients = Clients.Where(pros => pros.Origine.ClassOrigine == ClassOrigine.Desk);
                }
                else
                    if (rbOriginePerso.Checked)
                {
                    Clients = Clients.Where(pros => pros.Origine.ClassOrigine == ClassOrigine.Perso);
                }
                if (rbOptionDepot.Checked)
                {

                    Clients = Clients.Where(pros => pros.Options != null && pros.Options.Where(opt => opt.Active == true).Count() > 0
                                                    && pros.Options.Where(opt => opt.Active == true).FirstOrDefault()
                                                     .TypeContrat.CategorieContrat == CategorieContrat.Dépôt);
                }
                else
                    if (rbOptionResa.Checked)
                    {

                        Clients = Clients.Where(pros => pros.Options != null && pros.Options.Where(opt => opt.Active == true).Count() > 0
                                                        && pros.Options.Where(opt => opt.Active == true).FirstOrDefault()
                                                         .TypeContrat.CategorieContrat == CategorieContrat.Réservation);
                    }
                if(txtCompteTiers.Text!=string.Empty)
                {
                    var compteTiers = txtCompteTiers.Text;
                    Clients = Clients.Where(pros => pros.CompteTiers!=null &&  pros.CompteTiers.ToUpper().Trim() == compteTiers.ToUpper().Trim());
                }
                int i = 1;
                lvProspects.Items.Clear();

                Clients = Clients.OrderByDescending(pros => pros.DateCreation).ToList();

                foreach (var prospect in Clients)
                {
                    ListViewItem lviProspect = new ListViewItem(String.Format("{0:dd/MM/yyyy HH:mm:ss}", prospect.DateCreation));
                    lviProspect.SubItems.Add(prospect.DateSouscription.Value.ToShortDateString());
                    lviProspect.SubItems.Add(prospect.NomComplet);
                    lviProspect.SubItems.Add(prospect.Mobile1);
                    lviProspect.SubItems.Add(prospect.Email);
                    lviProspect.SubItems.Add(prospect.Adresse);
                    if (Tools.Tools.AgentEnCours.Role.CodeRole != "MKT" && Tools.Tools.AgentEnCours.Role.CodeRole != "DSK")
                    {
                        if (prospect.ProspectAffecte == false)
                            lviProspect.ImageIndex = 6;
                        else if (prospect.ProspectEdite)
                            lviProspect.ImageIndex = 8;
                        else
                            lviProspect.ImageIndex = 7;
                    }
                    else
                    {
                        if (prospect.ProspectAffecte == false)
                            lviProspect.ImageIndex = 6;
                        else
                            lviProspect.ImageIndex = 8;
                    }

                    var option = prospect.Options.Where(opt => opt.Active == true).FirstOrDefault();

                    if (Tools.Tools.AgentEnCours.Role.CodeRole == "MKT" || Tools.Tools.AgentEnCours.Role.CodeRole == "DSK")
                    {
                        lviProspect.SubItems.Add(prospect.Origine.LibelleTypeOrigine);
                    }
                    else
                        if (option != null)
                    {
                        if (option.SeuilContratAtteint == true && option.ContratGenere == false)
                            if (Tools.Tools.AgentEnCours.Role.CodeRole != "MKT" && Tools.Tools.AgentEnCours.Role.CodeRole != "DSK")
                                lviProspect.ImageIndex = 9;


                        if (option.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                        {
                            lviProspect.SubItems.Add(option.TypeVilla.CodeType + "/" + option.Lot.NumeroLot);
                        }
                        else
                            lviProspect.SubItems.Add(option.TypeVilla.CodeType);
                    }
                    else
                        lviProspect.SubItems.Add("");


                    //if (!(Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" && !Tools.Tools.AgentEnCours.IsChefEquipe))
                    //{
                        if (prospect.Commercial != null)
                            lviProspect.SubItems.Add(prospect.Commercial.NomComplet);
                        else
                            lviProspect.SubItems.Add("");
                    //}
                    //else
                    //{
                    //    lviProspect.SubItems.Add("");
                    //}
                    if(prospect.Projet!=null)
                    lviProspect.SubItems.Add(prospect.Projet.DenominationProjet);
                    else
                        lviProspect.SubItems.Add("");
                    lviProspect.BackColor = (i % 2 != 0) ? Color.Beige : Color.White;

                    lviProspect.Tag = prospect;
                    
                    lvProspects.Items.Add(lviProspect);
                    i++;
                }
                txtNbProspects.Text = Clients.Count().ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cmdPrendreOption_Click(object sender, EventArgs e)
        {
            try
            {
                //VERIFIER SI LE PROSPECT N'A PAS DEJA PRIS UNE OPTION
                var option = contratRep.GetOptionProspect(LeClientEnCours.ID);
                if (option != null)
                {
                    MessageBox.Show(this, "Désolé ce prospect a déjà souscrit à une option:...",
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FrmOptionProspect frmDetIlot = new FrmOptionProspect(LeClientEnCours);
                //frmDetIlot.MdiParent = this.MdiParent;
                frmDetIlot.WindowState = FormWindowState.Normal;
                frmDetIlot.StartPosition = FormStartPosition.CenterParent;
                frmDetIlot.ShowDialog();
                //Mettre à jours la liste des options
                AfficherOptionsProspect(LeClientEnCours);
                tcDossierProspect.SelectedTab = tcDossierProspect.TabPages[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdAffecterCommercial_Click(object sender, EventArgs e)
        {
            try
            {

                FrmAffectationCommercial frmAffecterComm = new FrmAffectationCommercial(ref LeClientEnCours);
                frmAffecterComm.WindowState = FormWindowState.Normal;
                frmAffecterComm.StartPosition = FormStartPosition.CenterParent;
                frmAffecterComm.ShowDialog();
                clientRep.SaveChanges();

                ChargerLesProspect();
                //AfficherOptionsProspect(LeClientEnCours);
                //tcDossierProspect.SelectedTab = tcDossierProspect.TabPages[2]; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                      "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdRendezVous_Click(object sender, EventArgs e)
        {
            FrmActiviteCommerciale frmActiviteComm = new FrmActiviteCommerciale(LeClientEnCours);
            frmActiviteComm.WindowState = FormWindowState.Normal;
            frmActiviteComm.StartPosition = FormStartPosition.CenterParent;
            frmActiviteComm.ShowDialog();
            AfficherActivitesCommercialesProspect(LeClientEnCours);
            tcDossierProspect.SelectedTab = tcDossierProspect.TabPages[0];

        }

        private void dgOptions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdFraisDossier_Click(object sender, EventArgs e)
        {
            FrmFraisDossier frmFD = new FrmFraisDossier(LeClientEnCours);
            frmFD.WindowState = FormWindowState.Normal;
            frmFD.StartPosition = FormStartPosition.CenterParent;
            frmFD.ShowDialog();
            AfficherEncaissementProspect(LeClientEnCours);
            tcDossierProspect.SelectedTab = tcDossierProspect.TabPages[2];

        }

        private void cmdVente_Click(object sender, EventArgs e)
        {
            FrmVente frmVD = new FrmVente(LeClientEnCours, false);
            frmVD.WindowState = FormWindowState.Normal;
            frmVD.StartPosition = FormStartPosition.CenterParent;
            frmVD.ShowDialog();

            EffacerForm();
            dgActivitesCommerciales.DataSource = null;
            dgEncaissements.DataSource = null;
            dgOptions.DataSource = null;
            this.ChargerLesProspect();



        }

        private void pActionProspect_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTauxEncaissementProspect_TextChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void txtLotProspect_TextChanged(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void txtPrixDeVenteProspect_TextChanged(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void txtMontantEncaisseProspect_TextChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void cmdFicheNotaire_Click(object sender, EventArgs e)
        {
            try
            {
                GenererFicheNotaire();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenererFicheNotaire()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                //object templateName = @"D:\Med\Projet Immociel\prjWinform\prjSenImmoWinform\prjSenImmoWinform\bin\Debug\modeles\FicheNotaire.dotx";
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "FicheNotaire.dotx";

                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);

                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;


                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = LeClientEnCours.NomComplet;

                if (LeClientEnCours.DateDeNaissance != null)
                {
                    myBookmark = bookmarks["DateEtLieuDeNaissance"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = LeClientEnCours.DateDeNaissance.Value.Date.ToShortDateString() + " à " + LeClientEnCours.LieuDeNaissance;
                }

                myBookmark = bookmarks["Nationalite"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = LeClientEnCours.Nationalite;

                myBookmark = bookmarks["Profession"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = LeClientEnCours.Profession;
                object oCheckbox = null;
                if (LeClientEnCours.SituationMatrimoniale == SituationMatrimoniale.Célibataire)
                {
                    oCheckbox = "CaseACocher1";
                }
                else if (LeClientEnCours.SituationMatrimoniale == SituationMatrimoniale.Marié)
                {
                    oCheckbox = "CaseACocher2";
                }
                else if (LeClientEnCours.SituationMatrimoniale == SituationMatrimoniale.Veuf)
                {
                    oCheckbox = "CaseACocher3";
                }
                else if (LeClientEnCours.SituationMatrimoniale == SituationMatrimoniale.Divorcé)
                {
                    oCheckbox = "CaseACocher4";
                }

                doc.FormFields.get_Item(ref oCheckbox).CheckBox.Value = true;

                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = LeClientEnCours.Adresse;

                myBookmark = bookmarks["Email"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = LeClientEnCours.Email;

                myBookmark = bookmarks["TelBureau"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = LeClientEnCours.TelephoneFixe;

                myBookmark = bookmarks["telPortable"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = LeClientEnCours.Mobile1;

                //myBookmark = bookmarks["telDomicile"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = LeClientEnCours.T;

                myBookmark = bookmarks["Fax"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = LeClientEnCours.Fax;

                if (LeClientEnCours.SituationMatrimoniale == SituationMatrimoniale.Marié)
                {

                    myBookmark = bookmarks["NomCompletConjoint"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = LeClientEnCours.PrenomConjoint;

                    if (LeClientEnCours.DateDeNaissanceConjoint != null)
                    {
                        myBookmark = bookmarks["DateNaissanceConjoint"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = LeClientEnCours.DateDeNaissanceConjoint.Value.Date.ToShortDateString();
                    }

                    myBookmark = bookmarks["ProfessionConjoint"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = LeClientEnCours.ProfessionConjoint;


                    myBookmark = bookmarks["NationaliteConjoint"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = LeClientEnCours.NationaliteConjoint;

                    if (LeClientEnCours.DateMariage != null)
                    {
                        myBookmark = bookmarks["DateEtLieuMariage"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = LeClientEnCours.DateMariage.Value.ToShortDateString() + " à " + LeClientEnCours.LieuDeMariage;
                    }

                    if (LeClientEnCours.DateContratMariage != null)
                    {
                        myBookmark = bookmarks["DateContratMariage"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = LeClientEnCours.DateContratMariage.Value.ToShortDateString();
                    }
                    object oCheckboxRegime = null;
                    if (LeClientEnCours.RegimeMatrimoniale == RegimeMatrimoniale.Séparation)
                    {
                        oCheckboxRegime = "CaseACocher5";
                    }
                    else if (LeClientEnCours.RegimeMatrimoniale == RegimeMatrimoniale.Communautaire)
                    {
                        oCheckboxRegime = "CaseACocher6";
                    }
                    else if (LeClientEnCours.RegimeMatrimoniale == RegimeMatrimoniale.Autre)
                    {
                        oCheckboxRegime = "CaseACocher7";
                    }

                    doc.FormFields.get_Item(ref oCheckboxRegime).CheckBox.Value = true;

                    myBookmark = bookmarks["NomEtResidenceNotaire"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "Me " + LeClientEnCours.PrenomNotaire + " " + LeClientEnCours.NomNotaire + " demeurant à " + LeClientEnCours.AdresseNotaire;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmdEditer_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProspect childForm = new FrmProspect(LeClientEnCours);

                childForm.StartPosition = FormStartPosition.CenterScreen;
                childForm.ShowDialog();
                childForm.WindowState = FormWindowState.Normal;

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

        public void EffacerForm()
        {
            txtDateSouscriptionProspect.Text = string.Empty;
            txtTypeContratProspect.Text = string.Empty;
            txtTypeVillaProspect.Text = string.Empty;
            txtPositionLotProspect.Text = string.Empty;
            txtLotProspect.Text = string.Empty;
            txtPrixDeVenteProspect.Text = string.Empty;
            txtTauxEncaissementProspect.Text = string.Empty;
            txtMontantEncaisseProspect.Text = string.Empty;
            chkSeuilContratAtteint.Checked = false;
            txtMontantSeuilContrat.Text = string.Empty;

            txtDateSouscriptionProspect.Text = string.Empty;
            txtTypeContratProspect.Text = string.Empty;
            txtTypeVillaProspect.Text = string.Empty;
            txtPositionLotProspect.Text = string.Empty;
            txtLotProspect.Text = string.Empty;
            txtPrixDeVenteProspect.Text = string.Empty;
            txtTauxEncaissementProspect.Text = string.Empty;

            txtMontantSeuilContrat.Text = string.Empty;
            chkSeuilContratAtteint.Checked = false;
            cmdVente.Visible = false;
        }

        private void tcDossierProspect_SelectedIndexChanged(object sender, EventArgs e)
        {
            AfficherCmdLeverOption();
        }

        private void AfficherCmdLeverOption()
        {
            if (tcDossierProspect.SelectedIndex == 1 && dgOptions.SelectedRows.Count > 0)
            {
                cmdLeverOption.Visible = true;
            }
            else
                cmdLeverOption.Visible = false;
        }

        private void cmdLeverOption_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(this, "Voulez vous réellement vous lever cette option?",
                "Prosopis - Lever d'option", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {

                try
                {
                    if (dgOptions.SelectedRows.Count > 0)
                    {
                        int idOption = (int)dgOptions.SelectedRows[0].Cells[0].Value;
                        clientRep.LeverOption(idOption);
                        AfficherOptionsProspect(LeClientEnCours);
                        cmdLeverOption.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Erreur:..." + ex.Message,
                      "Prosopis - Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgOptions_SelectionChanged(object sender, EventArgs e)
        {
            AfficherCmdLeverOption();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgProspects_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dgProspects.SelectedRows.Count > 0)
                {
                    int idClient = (int)dgProspects.SelectedRows[0].Cells[0].Value;
                    LeClientEnCours = clientRep.GetClient(idClient);

                    FrmDossierProspect childForm = new FrmDossierProspect(LeClientEnCours);
                    childForm.MdiParent = this.MdiParent;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }

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

        private void ChargerCommerciaux()
        {

            var lesCommerciaux = commercialRep.GetAllCommerciaux();
            if (Tools.Tools.AgentEnCours.IsChefEquipe)
                lesCommerciaux = lesCommerciaux.Where(c => c.ChefEquipeId == Tools.Tools.AgentEnCours.Id);
            if(LeProjetEncours!=null)
            {
                lesCommerciaux = lesCommerciaux.Where(c => c.ProjetId == LeProjetEncours.Id);
            }
            cmbCommercial.DataSource = lesCommerciaux.ToList();
            cmbCommercial.DisplayMember = "NomComplet";
            cmbCommercial.SelectedIndex = -1;
        }

        private void cmbCommercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (chkCommercial.Checked)
            //{
            //    if (cmbCommercial.SelectedItem != null)
            //    {
            //        Agent leCommercial = (Agent)cmbCommercial.SelectedItem;

            //        try
            //        {
            //            var listeProspect = clientRep.GetAllProspects().ToList();
            //            listeProspect = listeProspect.Where(pros => pros.CommercialID == leCommercial.Id).ToList();
            //            AfficherLaListeDesProspects(listeProspect);

            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(this, "Erreur lors du chargement des prospects:..." + ex.Message,
            //                    "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
            this.Cursor = Cursors.WaitCursor;
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void chkCommercial_CheckedChanged(object sender, EventArgs e)
        {
            cmbCommercial.SelectedIndex = -1;
            cmbCommercial.Visible = chkCommercial.Checked;
            this.Cursor = Cursors.WaitCursor;
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }



        private void lvProspects_DoubleClick(object sender, EventArgs e)
        {
            if (lvProspects.SelectedItems.Count > 0)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Client leProspect = (Client)lvProspects.SelectedItems[0].Tag;

                    FrmDossierProspect childForm = new FrmDossierProspect(leProspect);
                    childForm.MdiParent = this.MdiParent;
                    // childForm.StartPosition = FormStartPosition.CenterScreen;

                    childForm.WindowState = FormWindowState.Normal;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                    //cmdRechercher_Click(sender, e);
                    //AfficherLaListeDesProspects(clientRep.GetProspects(txtNomRecherche.Text, txtPrenomRecherche.Text, txtTelephoneRecherche.Text, txtEmailRecherche.Text).OrderByDescending(pros => pros.DateCreation));
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
        }

        private void txtPrenomRecherche_Validated(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void txtTelephoneRecherche_Validated(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void txtEmailRecherche_Validated(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void txtPrenomRecherche_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Cursor = Cursors.WaitCursor;
                //cmdRechercher_Click(sender, e);
                this.Cursor = Cursors.Default;
            }
        }

        private void txtTelephoneRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Cursor = Cursors.WaitCursor;
                //cmdRechercher_Click(sender, e);
                this.Cursor = Cursors.Default;
            }
        }

        private void txtEmailRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Cursor = Cursors.WaitCursor;
                //cmdRechercher_Click(sender, e);
                this.Cursor = Cursors.Default;
            }
        }

        private void chkAffecte_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAffecte.Checked)
            {
                chkNonAffecte.Checked = false;

            }
            this.Cursor = Cursors.WaitCursor;
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void chkNonAffecte_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNonAffecte.Checked)
            {
                chkAffecte.Checked = false;

            }
            this.Cursor = Cursors.WaitCursor;
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void lvProspects_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkNonAffecteGlobal_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (chkNonAffecteGlobal.Checked)
            {

                chkNonTraitesGlobal.Checked = false;
                chkTraitesGlobal.Checked = false;
                chkSeuilAtteintGlobal.Checked = false;

            }
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void chkNonTraitesGlobal_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (chkNonTraitesGlobal.Checked)
            {

                chkNonAffecteGlobal.Checked = false;
                chkTraitesGlobal.Checked = false;
                chkSeuilAtteintGlobal.Checked = false;

            }
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void chkTraitesGlobal_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (chkTraitesGlobal.Checked)
            {

                chkNonAffecteGlobal.Checked = false;
                chkNonTraitesGlobal.Checked = false;
                chkSeuilAtteintGlobal.Checked = false;

            }
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void chkSeuilAtteintGlobal_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (chkSeuilAtteintGlobal.Checked)
            {

                chkNonAffecteGlobal.Checked = false;
                chkTraitesGlobal.Checked = false;
                chkNonTraitesGlobal.Checked = false;

            }
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void rbOptionDepot_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOptionDepot.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                //cmdRechercher_Click(sender, e);
                this.Cursor = Cursors.Default;
            }
        }

        private void rbOptionResa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOptionResa.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                //cmdRechercher_Click(sender, e);
                this.Cursor = Cursors.Default;
            }
        }

        private void rbToutTypeOption_CheckedChanged(object sender, EventArgs e)
        {
            if (rbToutTypeOption.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                //cmdRechercher_Click(sender, e);
                this.Cursor = Cursors.Default;
            }
        }

        private void rbToutOrigine_CheckedChanged(object sender, EventArgs e)
        {
            if (rbToutOrigine.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                //cmdRechercher_Click(sender, e);
                this.Cursor = Cursors.Default;
            }
        }

        private void rbOriginePerso_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOriginePerso.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                //cmdRechercher_Click(sender, e);
                this.Cursor = Cursors.Default;
            }
        }

        private void rbOrigineDesk_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOrigineDesk.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                //cmdRechercher_Click(sender, e);
                pSource.Visible = true;
                this.Cursor = Cursors.Default;
            }
            else
            {
                pSource.Visible = false;
                cmbOrigines.SelectedIndex = -1;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmListeProspects_Activated(object sender, EventArgs e)
        {
            //cmdRechercher_Click(sender, e);
        }

        private void lvProspects_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ////if (BChargement == true) return;
           
            //// Get the new sorting column.
            //ColumnHeader new_sorting_column = lvProspects.Columns[e.Column];

            //// Figure out the new sorting order.
            //System.Windows.Forms.SortOrder sort_order;
            //if (SortingColumn == null)
            //{
            //    // New column. Sort ascending.
            //    sort_order = SortOrder.Descending;
            //}
            //else
            //{
            //    // See if this is the same column.
            //    if (new_sorting_column == SortingColumn)
            //    {
            //        // Same column. Switch the sort order.
            //        if (SortingColumn.Text.StartsWith("> "))
            //        {
            //            sort_order = SortOrder.Descending;
            //        }
            //        else
            //        {
            //            sort_order = SortOrder.Ascending;
            //        }
            //    }
            //    else
            //    {
            //        // New column. Sort ascending.
            //        sort_order = SortOrder.Ascending;
            //    }

            //    // Remove the old sort indicator.
            //    SortingColumn.Text = SortingColumn.Text.Substring(2);
            //}

            //// Display the new sort order.
            //SortingColumn = new_sorting_column;
            //if (sort_order == SortOrder.Ascending)
            //{
            //    SortingColumn.Text = "> " + SortingColumn.Text;
            //}
            //else
            //{
            //    SortingColumn.Text = "< " + SortingColumn.Text;
            //}

            //// Create a comparer.
            //lvProspects.ListViewItemSorter =
            //    new ListViewComparer(e.Column, sort_order);

            //// Sort.
            //lvProspects.Sort();
        }

        private void FrmListeProspects_Load(object sender, EventArgs e)
        {
            lvProspects_ColumnClick(sender, new ColumnClickEventArgs(0));
            txtPrenomRecherche.Focus();
        }

        private void cmbOrigines_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmdRechercher_Click(sender,e);
        }

        private void txtPrenomRecherche_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmListeProspects_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.F1)
            {
                cmdRechercher_Click(sender, e);
            }
        }

        private void cmbProjets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbProjets.SelectedItem!=null)
            {
                LeProjetEncours = cmbProjets.SelectedItem as Projet;
                rbToutTypeConstruction.Checked = true;
                if (LeProjetEncours.Id  == 1)//AKYS
                {
                    rbConstructionVilla.Checked = true;
                    pTypeConstruction.Enabled = false;
                }
                else
                    pTypeConstruction.Enabled = true;

                
                ChargerCommerciaux();

            }else
            {
                LeProjetEncours = null;
                rbToutTypeConstruction.Checked = true;
                pTypeConstruction.Enabled = false;
            }


        }

        private void cmbProjets_Validated(object sender, EventArgs e)
        {
            if (cmbProjets.SelectedItem == null)
            { 
                rbToutTypeConstruction.Checked = true;
                pTypeConstruction.Enabled = false;
                LeProjetEncours = null;
                ChargerCommerciaux();
            }
        }
    }

    public class ListViewComparer : System.Collections.IComparer
    {
       
        private int ColumnNumber;
        private SortOrder SortOrder;

        public ListViewComparer(int column_number,
            SortOrder sort_order)
        {
            ColumnNumber = column_number;
            SortOrder = sort_order;
           
        }

        // Compare two ListViewItems.
        public int Compare(object object_x, object object_y)
        {
            
            // Get the objects as ListViewItems.
            ListViewItem item_x = object_x as ListViewItem;
            ListViewItem item_y = object_y as ListViewItem;

            // Get the corresponding sub-item values.
            string string_x;
            if (item_x.SubItems.Count <= ColumnNumber)
            {
                string_x = "";
            }
            else
            {
                string_x = item_x.SubItems[ColumnNumber].Text;
            }

            string string_y;
            if (item_y.SubItems.Count <= ColumnNumber)
            {
                string_y = "";
            }
            else
            {
                string_y = item_y.SubItems[ColumnNumber].Text;
            }

            // Compare them.
            int result;
            double double_x, double_y;
            if (double.TryParse(string_x, out double_x) &&
                double.TryParse(string_y, out double_y))
            {
                // Treat as a number.
                result = double_x.CompareTo(double_y);
            }
            else
            {
                DateTime date_x, date_y;
                if (DateTime.TryParse(string_x, out date_x) &&
                    DateTime.TryParse(string_y, out date_y))
                {
                    // Treat as a date.
                    result = date_x.CompareTo(date_y);
                }
                else
                {
                    // Treat as a string.
                    result = string_x.CompareTo(string_y);
                }
            }

            // Return the correct result depending on whether
            // we're sorting ascending or descending.
            if (SortOrder == SortOrder.Ascending)
            {
                return result;
            }
            else
            {
                return -result;
            }
        }
    }
}
