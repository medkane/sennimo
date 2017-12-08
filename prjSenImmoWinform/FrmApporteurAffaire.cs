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

namespace prjSenImmoWinform
{
    public partial class FrmApporteurAffaire : Form
    {
        private ApporteurAffaireRepository afRepo;
        private ContratRepository contratRepo;
        private ApporteurAffaire LApporteurAffairesEnCours;
        private bool bModifClient;
        private int leContratIdEnCours;
        private decimal montantFactureEnCours;
        private decimal montantBRSEncours;
        private decimal montantNetApayerEnCours;
        private bool bPrint;

        public FrmApporteurAffaire()
        {
            InitializeComponent();
            try
            {
                afRepo = new ApporteurAffaireRepository();
                contratRepo = new ContratRepository();
                ChargerLesApporteurAffaires(txtNomRecherche.Text);
                VerouillerForm();
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "DC")
                {
                    pCommande.Enabled = false;
                    cmdReglerCommission.Enabled = false;
                    cmdFacturer.Enabled = false;
                    button1.Enabled = false;
                    cmdFermer.Enabled = true;
                }

                if (Tools.Tools.AgentEnCours.Role.CodeRole != "ADM")
                {
                    chkFacturesGenerees.Visible = false;
                }
                tcTypeApporteur.ItemSize = new Size(0, 1);
                tcTypeApporteur.SizeMode = TabSizeMode.Fixed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerLesApporteurAffaires( string nom)
        {
            try
            {
                var listeApporteurAffaires= afRepo.GetAllApporteurAffaires().ToList().Select(af => new
                                                                                                        {
                                                                                                            Id = af.Id,
                                                                                                            Prénom = af.Prenom,
                                                                                                            Nom = af.Nom,
                                                                                                            NomComplet=af.NomComplet,
                                                                                                            Téléphone=af.Mobile1,
                                                                                                            TypeApporteur=af.Type,
                                                                                                            RaisonSocial=af.RaisonSociale,
                                                                                                            NINEA=af.NINEA,
                                                                                                            RCCCM=af.RCCM,
                                                                                                            TelephoneAgence=af.TelephoneAgence,
                                                                                                            AdresseBureau=af.AdresseBureau,
                                                                                                            EmailAgence=af.EmailAgence,
                                                                                                            Gerant=af.NomGerant,
                                                                                                            Taux=af.TauxCommission,
                                                                                                            Contrats = af.Contrats.Count()

                                                                                                       });
                
                if (nom != string.Empty)
                    listeApporteurAffaires = listeApporteurAffaires.Where(af => af.NomComplet.ToLower().Contains(nom.ToLower()));

      
                lvApporteurAffaires.Items.Clear();

                int i = 0;
                foreach (var apporteur in listeApporteurAffaires)
                {
                    ListViewItem lviApporteur = new ListViewItem(apporteur.NomComplet);
                    lviApporteur.SubItems.Add(apporteur.TypeApporteur== TypeApporteur.Particulier? apporteur.Téléphone:apporteur.TelephoneAgence);
                    lviApporteur.SubItems.Add(apporteur.Contrats.ToString());
                    if (apporteur.TypeApporteur == TypeApporteur.Particulier)
                        lviApporteur.ImageIndex = 0;
                    else
                        lviApporteur.ImageIndex = 1;
                    //    lvProspects.Columns.RemoveAt(5);
                    lviApporteur.BackColor = (i % 2 != 0) ? Color.Beige : Color.White;

                    lviApporteur.Tag = apporteur.Id;
                    lvApporteurAffaires.Items.Add(lviApporteur);
                    i++;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void AfficherLApporteurAffaire(ApporteurAffaire apporteur)
        {
            try
            {
                //txtDateSouscription.Text = client.DateCreation.ToShortDateString();
                if (apporteur.Type == TypeApporteur.Particulier)
                {
                    rbParticulier.Checked = true;
                    txtPrenom.Text = apporteur.Prenom;
                    txtNom.Text = apporteur.Nom;
                    txtTelephoneFixe.Text = apporteur.TelephoneFixe;
                    txtTelephoneMobile.Text = apporteur.Mobile1;
                    txtAdresse.Text = apporteur.Adresse;
                    tcTypeApporteur.SelectedIndex = 0;
                }
                else
                {
                    rbAgence.Checked = true;
                    txtRaisonSociale.Text = apporteur.RaisonSociale;
                    txtNINEA.Text = apporteur.NINEA;
                    txtRCCM.Text = apporteur.RCCM;
                    txtAdresseBureau.Text = apporteur.AdresseBureau;
                    txtTelephoneAgence.Text = apporteur.TelephoneAgence;
                    txtEmailAgence.Text = apporteur.EmailAgence;
                    txtNomGerant.Text = apporteur.NomGerant;
                    tcTypeApporteur.SelectedIndex = 1;
                }
                nudTauxCommission.Value = apporteur.TauxCommission;
                AfficherLesContrats(apporteur);
                AfficherLesFactures( apporteur);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void AfficherLesContrats(ApporteurAffaire apporteurAffaire)
        {
            try
            {
                var contratsApporteur = afRepo.GetAllContrats(apporteurAffaire);



                //    foreach (var contrat in contratsApporteur)
                //    {
                //        montantEncaisse = contrat.EncaissementGlobals.Sum(enc => enc.MontantGlobal);
                //        montantCommissionsDues = contrat.FactureCommissions.Sum(fact => fact.MontantAPayer);
                //        montantCommissionsPayees = contrat.FactureCommissions.Where(fact => fact.FactureCommissionGlobale.Payee == true).Sum(fact => fact.MontantAPayer);
                //    }





                dgContrats.DataSource = contratsApporteur.ToList().Select(cont => new
                {
                    Id = cont.Id,
                    Projet=cont.Projet.DenominationProjet,
                    Numéro = cont.NumeroContrat,
                    Client = cont.Client.NomComplet,
                    Type = cont.TypeContrat.CategorieContrat,
                    Lot = cont.TypeContrat.CategorieContrat == CategorieContrat.Réservation ? cont.Lot.NumeroLot : cont.Lot.TypeVilla.CodeType,
                    Prix = cont.PrixFinal,
                    Commission = cont.CommissionApporteur,
                    MontantTotalEncaisse = cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal),
                    MontantDu = cont.FactureCommissions.Sum(fact => fact.MontantAPayer),
                    Payé = cont.FactureCommissions.Where(fact => fact.Payee == true).Sum(fact => fact.MontantAPayer),
                    RestantDu = cont.FactureCommissions.Sum(fact => fact.MontantAPayer) - cont.FactureCommissions.Where(fact => fact.Payee == true).Sum(fact => fact.MontantAPayer),

                }).ToList();
                FormatterGrilleContrats();

                var montantsDus = contratsApporteur.Sum(cont => cont.FactureCommissions.Sum(fact => fact.MontantAPayer));
                lbDues.Text = montantsDus.ToString("### ### ###");
                var montantPayes = contratsApporteur.Sum(cont => cont.FactureCommissions.Where(fact => fact.Payee==true).Sum(fact => fact.MontantAPayer));
                lbVersees.Text = montantPayes.ToString("### ### ###");

                lbRestantes.Text = (montantsDus - montantPayes).ToString("### ### ###");

                txtMontantTotalCommissions.Text = contratsApporteur.Sum(cont => cont.CommissionApporteur).ToString("### ### ###");
                txtMontantTotalcommissionsVersees.Text = montantPayes.ToString("### ### ###");
                txtMontantTotalCommissionsRestantes.Text = (contratsApporteur.Sum(cont => cont.CommissionApporteur) - montantPayes).ToString("### ### ###");
            }

            catch (Exception)
            {

                throw;
            }
        }

        private void FormatterGrilleContrats()
        {
            dgContrats.Columns[0].Width = 0;
            dgContrats.Columns[0].Visible = false;

            dgContrats.Columns[1].Width = 60;

            dgContrats.Columns[2].Width = 70;
            dgContrats.Columns[2].Visible = false;
            dgContrats.Columns[3].Width = 170;
            dgContrats.Columns[4].Width = 80;
           
            dgContrats.Columns[5].Width = 50;

            dgContrats.Columns[6].HeaderText = "Prix de vente";
            dgContrats.Columns[6].Width = 70;
            dgContrats.Columns[6].DefaultCellStyle.Format = "### ### ###";
            dgContrats.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgContrats.Columns[7].HeaderText = "Commissions Totales";
            dgContrats.Columns[7].Width = 70;
            dgContrats.Columns[7].DefaultCellStyle.Format = "### ### ###";
            dgContrats.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgContrats.Columns[8].HeaderText = "Montant Encaissé";
            dgContrats.Columns[8].Width = 70;
            dgContrats.Columns[8].DefaultCellStyle.Format = "### ### ###";
            dgContrats.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgContrats.Columns[9].HeaderText = "Commissions dûes";
            dgContrats.Columns[9].Width = 70;
            dgContrats.Columns[9].DefaultCellStyle.Format = "### ### ###";
            dgContrats.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgContrats.Columns[10].HeaderText = "Commissions payées";
            dgContrats.Columns[10].Width = 70;
            dgContrats.Columns[10].DefaultCellStyle.Format = "### ### ###";
            dgContrats.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgContrats.Columns[11].HeaderText = "Montant restant";
            dgContrats.Columns[11].Width = 70;
            dgContrats.Columns[11].DefaultCellStyle.Format = "### ### ###";
            dgContrats.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void AfficherLesFactures(ApporteurAffaire apporteurAffaire)
        {
            try
            {
                AfficherHistoriqueFacturesGlobales();

                var contratsApporteur = afRepo.GetAllContrats(apporteurAffaire).ToList();
                var lesContratsAFacturer = contratsApporteur.Where(cont => cont.FactureCommissions.Where(fact => fact.FactureGenere == false).Count() > 0);
                if (lesContratsAFacturer.Count() >0)
                {
                    dgFactures.DataSource = lesContratsAFacturer.ToList().Select(cont => new
                    {
                        Id = cont.Id,
                        Projet=cont.Projet.DenominationProjet,
                        Client = cont.Client.NomComplet,

                        Type = cont.TypeContrat.CategorieContrat,

                        Lot = cont.TypeContrat.CategorieContrat == CategorieContrat.Réservation ? cont.Lot.NumeroLot : cont.Lot.TypeVilla.CodeType,
                        Prix = cont.PrixFinal,
                        CumulVerse = cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal),
                       
                        //Commission = cont.CommissionApporteur,
                        CumulPrecedant = cont.FactureCommissions.Where(fact => fact.FactureGenere == true).Sum(fact => fact.EncaissementGlobal.MontantGlobal),
                        BaseDeCalcul = cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) - cont.FactureCommissions.Where(fact => fact.FactureGenere == true).Sum(fact => fact.EncaissementGlobal.MontantGlobal),
                        //MontantDu = cont.FactureCommissions.Sum(fact => fact.MontantAPayer),
                        //Payé = cont.FactureCommissions.Sum(fact => fact.PaiementCommissions.Sum(paie => paie.MontantPaye)),
                        // RestantDu = cont.FactureCommissions.Sum(fact => fact.MontantAPayer) - cont.FactureCommissions.Sum(fact => fact.PaiementCommissions.Sum(paie => paie.MontantPaye)),
                        Commission = (cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) - cont.FactureCommissions.Where(fact => fact.FactureGenere == true).Sum(fact => fact.EncaissementGlobal.MontantGlobal)) * LApporteurAffairesEnCours.TauxCommission / 100,

                    }).ToList();
                    dgFactures.Columns[0].Width = 0;
                    dgFactures.Columns[0].Visible = false;

                    dgFactures.Columns[1].Width = 60;

                    dgFactures.Columns[2].Width = 230;
                    dgFactures.Columns[3].Width = 80;
                    dgFactures.Columns[4].Width = 50;

                    dgFactures.Columns[5].HeaderText = "Prix de vente";
                    dgFactures.Columns[5].Width = 70;
                    dgFactures.Columns[5].DefaultCellStyle.Format = "### ### ###";
                    dgFactures.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgFactures.Columns[6].HeaderText = "Cumul versé";
                    dgFactures.Columns[6].Width = 70;
                    dgFactures.Columns[6].DefaultCellStyle.Format = "### ### ###";
                    dgFactures.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgFactures.Columns[7].HeaderText = "Cumul précédent";
                    dgFactures.Columns[7].Width = 70;
                    dgFactures.Columns[7].DefaultCellStyle.Format = "### ### ###";
                    dgFactures.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgFactures.Columns[8].HeaderText = "Base de calcul";
                    dgFactures.Columns[8].Width = 70;
                    dgFactures.Columns[8].DefaultCellStyle.Format = "### ### ###";
                    dgFactures.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgFactures.Columns[9].HeaderText = "Commission";
                    dgFactures.Columns[9].Width = 70;
                    dgFactures.Columns[9].DefaultCellStyle.Format = "### ### ###";
                    dgFactures.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    //dgContrats.Columns[9].HeaderText = "Commission versée";
                    //dgContrats.Columns[9].Width = 70;
                    //dgContrats.Columns[9].DefaultCellStyle.Format = "### ### ###";
                    //dgContrats.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;



                    //var montantsDus = contratsApporteur.Sum(cont => cont.FactureCommissions.Sum(fact => fact.MontantAPayer));
                    //lbDues.Text = montantsDus.ToString("### ### ###");
                    //var montantPayes = contratsApporteur.Sum(cont => cont.FactureCommissions.Sum(fact => fact.PaiementCommissions.Sum(paie => paie.MontantPaye)));
                    //lbVersees.Text = montantPayes.ToString("### ### ###");

                    //lbRestantes.Text = (montantsDus - montantPayes).ToString("### ### ###");

                    //txtMontantTotalCommissions.Text = contratsApporteur.Sum(cont => cont.CommissionApporteur).ToString("### ### ###");
                    //txtMontantTotalcommissionsVersees.Text = montantPayes.ToString("### ### ###");
                    //txtMontantTotalCommissionsRestantes.Text = (contratsApporteur.Sum(cont => cont.CommissionApporteur) - montantPayes).ToString("### ### ###");

                    // montantFactureEnCours = lesContratsAFacturer.Sum(cont => cont.FactureCommissions.Sum(fact => fact.MontantAPayer - fact.PaiementCommissions.Sum(enc => enc.MontantPaye)));
                    montantFactureEnCours = lesContratsAFacturer.Sum(cont => cont.FactureCommissions.Where(fact => fact.EncaissementGlobal.Contrat.ApporteurID == LApporteurAffairesEnCours.Id
                                                                             && fact.FactureGenere == false).Sum(fact => fact.MontantAPayer));

                    //m = m * LApporteurAffairesEnCours.TauxCommission / 100;

                    //            - cont.FactureCommissions.Where(fact => fact.FactureGenere == true).Sum(fact => fact.EncaissementGlobal.MontantGlobal)
                    //                                                    *LApporteurAffairesEnCours.TauxCommission / 100);

                    montantBRSEncours = montantFactureEnCours * 5 / 100;
                    txtMontantTotalBrut.Text = montantFactureEnCours.ToString("### ### ###");
                    txtTotalBRS.Text = montantBRSEncours.ToString("### ### ###");
                    montantNetApayerEnCours = montantFactureEnCours - montantBRSEncours;
                    txtMontantTotalNet.Text = montantNetApayerEnCours.ToString("### ### ###");
                    button1.Enabled = true;
                    chkFacturesGenerees.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                    chkFacturesGenerees.Enabled = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AfficherHistoriqueFacturesGlobales()
        {
            dgFactureCommissionsEchues.DataSource = afRepo.GetAllFactureCommissionGlobales(LApporteurAffairesEnCours).ToList()
                                                    .Select(factureGlobale => new
                                                    {
                                                        Id = factureGlobale.ID,
                                                        Date = factureGlobale.Date,
                                                        Montant = factureGlobale.MontantAPayer,

                                                        Soldée = factureGlobale.Payee,
                                                        BRS = factureGlobale.MontantAPayer * 5 / 100,
                                                        Net = factureGlobale.MontantAPayer - factureGlobale.MontantAPayer * 5 / 100,
                                                    }).ToList();
            dgFactureCommissionsEchues.Columns[0].Width = 0;
            dgFactureCommissionsEchues.Columns[0].Visible = false;
            dgFactureCommissionsEchues.Columns[1].Width = 70;
            dgFactureCommissionsEchues.Columns[2].HeaderText = "Montant Brut";
            dgFactureCommissionsEchues.Columns[2].Width = 67;
            dgFactureCommissionsEchues.Columns[2].DefaultCellStyle.Format = "### ### ###";
            dgFactureCommissionsEchues.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgFactureCommissionsEchues.Columns[3].Width = 45;
            dgFactureCommissionsEchues.Columns[4].HeaderText = "BRS";
            dgFactureCommissionsEchues.Columns[4].Width = 67;
            dgFactureCommissionsEchues.Columns[4].DefaultCellStyle.Format = "### ### ###";
            dgFactureCommissionsEchues.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgFactureCommissionsEchues.Columns[5].HeaderText = "Montant Net";
            dgFactureCommissionsEchues.Columns[5].Width = 67;
            dgFactureCommissionsEchues.Columns[5].DefaultCellStyle.Format = "### ### ###";
            dgFactureCommissionsEchues.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void dgContrats_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgContrats.SelectedRows.Count > 0)
                {
                    leContratIdEnCours = (int)dgContrats.SelectedRows[0].Cells[0].Value;
                    
                    //var facturesCommissions = afRepo.GetFacturesCommissions(idContrat);

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgContrats_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgContrats.SelectedRows.Count > 0)
                {
                    int idContrat = (int)dgContrats.SelectedRows[0].Cells[0].Value;
                    var facturesCommissions = afRepo.GetFacturesCommissions(idContrat);

                    if (facturesCommissions != null)
                    {
                        dgFacturesCommissions.DataSource = facturesCommissions.ToList().Select(fact => new
                        {
                            Id = fact.ID,
                            Date = fact.Date,
                            Numéro = fact.EncaissementGlobal.NumeroEncaissement,
                            Référence = fact.EncaissementGlobal.ReferencePaiement,
                            Encaissement = fact.EncaissementGlobal.MontantGlobal,
                            Montant = fact.MontantAPayer,
                            MontantVersé = fact.PaiementCommissions.Sum(enc => enc.MontantPaye),
                            RestantDu = fact.MontantAPayer- fact.PaiementCommissions.Sum(enc => enc.MontantPaye)

                        }).ToList();
                        dgFacturesCommissions.Columns[0].Width = 0;
                        dgFacturesCommissions.Columns[0].Visible = false;
                        dgFacturesCommissions.Columns[1].Width = 70;
                        dgFacturesCommissions.Columns[2].Width =110;
                        dgFacturesCommissions.Columns[3].Width = 210;

                        dgFacturesCommissions.Columns[4].HeaderText = "Montant encaissé";
                        dgFacturesCommissions.Columns[4].Width = 70;
                        dgFacturesCommissions.Columns[4].DefaultCellStyle.Format = "### ### ###";
                        dgFacturesCommissions.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dgFacturesCommissions.Columns[5].HeaderText = "Commission dûe";
                        dgFacturesCommissions.Columns[5].Width = 70;
                        dgFacturesCommissions.Columns[5].DefaultCellStyle.Format = "### ### ###";
                        dgFacturesCommissions.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dgFacturesCommissions.Columns[6].HeaderText = "Commission versée";
                        dgFacturesCommissions.Columns[6].Width = 70;
                        dgFacturesCommissions.Columns[6].DefaultCellStyle.Format = "### ### ###";
                        dgFacturesCommissions.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dgFacturesCommissions.Columns[7].HeaderText = "Montant restant";
                        dgFacturesCommissions.Columns[7].Width = 70;
                        dgFacturesCommissions.Columns[7].DefaultCellStyle.Format = "### ### ###";
                        dgFacturesCommissions.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    
                        tcCommissions.SelectedTab = tcCommissions.TabPages[2];
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void lbDebitTotal_Click(object sender, EventArgs e)
        {

        }

        private void cmdReglerCommission_Click(object sender, EventArgs e)
        {
            if (leContratIdEnCours != 0)
            {
                var lesFactures = afRepo.GetFacturesCommissions(leContratIdEnCours).Where(fact => fact.Payee==false).ToList();
                afRepo.EnregistrerLesReglementsCommission(lesFactures);
                //AfficherLesContrats(LApporteurAffairesEnCours);
            }
            else
                MessageBox.Show("Veuillez dabord selectionner le contrat concerné");
        }

        #region CRUD DE L'APPORTEUR D'AFFAIRE



        private void DeverouillerClient()
        {
            txtNom.ReadOnly = false;
            txtPrenom.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtAdresse.ReadOnly = false;
            txtTelephoneMobile.ReadOnly = false;
            txtTelephoneFixe.ReadOnly = false;
            nudTauxCommission.Enabled = true;

            txtRaisonSociale.ReadOnly = false;
            txtNINEA.ReadOnly = false;
            txtRCCM.ReadOnly = false;
            txtAdresseBureau.ReadOnly = false;
            txtTelephoneAgence.ReadOnly = false;
            txtEmailAgence.ReadOnly = false;
            txtNomGerant.ReadOnly = false;

            cmdNouveau.Enabled = false;
            cmdEnregistrer.Enabled = true;
            cmdEditer.Enabled = false;
            cmdSupprimer.Enabled = false;
        }
        private void VerouillerForm()
        {
            txtNom.ReadOnly = true;
            txtPrenom.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtAdresse.ReadOnly = true;
            txtTelephoneMobile.ReadOnly = true;
            txtTelephoneFixe.ReadOnly = true;
            nudTauxCommission.Enabled = false;

            txtRaisonSociale.ReadOnly = true;
            txtNINEA.ReadOnly = true;
            txtRCCM.ReadOnly = true;
            txtAdresseBureau.ReadOnly = true;
            txtTelephoneAgence.ReadOnly = true;
            txtEmailAgence.ReadOnly = true;
            txtNomGerant.ReadOnly = true;


            cmdNouveau.Enabled = true;
            cmdEnregistrer.Enabled = false;
            cmdEditer.Enabled = true;
            cmdSupprimer.Enabled = true;
        }

        private void cmdNouveau_Click(object sender, EventArgs e)
        {
            bModifClient = false;
            EffacerForm();
            DeverouillerClient();
            //cmdEnregistrerVille.Enabled = true;
            txtPrenom.Focus();
            txtMontantTotalCommissionsRestantes.Visible = false;
            txtMontantTotalcommissionsVersees.Visible = false;
            txtMontantTotalCommissions.Visible = false;
            lbMontantTotalCommissionsRestantes.Visible = false;
            lbMontantTotalcommissionsVersees.Visible = false;
            lbMontantTotalCommissions.Visible = false;
        }

        private void EffacerForm()
        {
            txtPrenom.Text = string.Empty;
            txtNom.Text = string.Empty;
            txtAdresse.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelephoneMobile.Text = string.Empty;
            txtTelephoneFixe.Text = string.Empty;

            txtRaisonSociale.Text = string.Empty;
            txtNINEA.Text = string.Empty;
            txtRCCM.Text = string.Empty;
            txtAdresseBureau.Text = string.Empty;
            txtTelephoneAgence.Text = string.Empty;
            txtEmailAgence.Text = string.Empty;
            txtNomGerant.Text = string.Empty;


            txtMontantTotalCommissionsRestantes.Text = string.Empty;
            
            txtMontantTotalcommissionsVersees.Text = string.Empty;
            txtMontantTotalCommissions.Text = string.Empty;

            nudTauxCommission.Value = 0;
            txtMontantTotalNet.Text = string.Empty;
            txtMontantTotalBrut.Text = string.Empty;
            txtTotalBRS.Text = string.Empty;
        }

        private void cmdEditer_Click(object sender, EventArgs e)
        {
            bModifClient = true;
            DeverouillerClient();
            txtPrenom.Focus();
        }

        //private void cmdSupprimerClient_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show(this, "Voulez réellement supprimer ce Client?",
        //            "GesAGRO - Gestion des clients", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
        //    {
        //        db.Clients.Remove(ClientEnCours);
        //        db.SaveChanges();
        //        chargerClients();
        //        EffacerClient();
        //    }

        //}

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbParticulier.Checked && ( txtPrenom.Text == string.Empty || txtNom.Text == string.Empty))
                {
                    MessageBox.Show(this, "Veuillez saisir les prénoms et nom de l'apporteur d'affaire",
                             "Prosopis - Gestion des Apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (rbAgence.Checked && (txtRaisonSociale.Text == string.Empty))
                {
                    MessageBox.Show(this, "Veuillez saisir la raison sociale de l'agence",
                             "Prosopis - Gestion des Apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (nudTauxCommission.Value == 0)
                {
                    MessageBox.Show(this, "Veuillez saisir le taux de commission de l'apporteur d'affaire",
                             "Prosopis - Gestion des Apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!bModifClient)
                {
                    var af = new ApporteurAffaire()
                    {
                        Prenom = txtPrenom.Text,
                        Nom = txtNom.Text,
                        Mobile1 = txtTelephoneMobile.Text,
                        TelephoneFixe = txtTelephoneFixe.Text,
                        Email = txtEmail.Text,
                        Adresse = txtAdresse.Text,
                        TauxCommission = nudTauxCommission.Value,
                        DateCreation = DateTime.Now.Date,
                        Actif = true,
                        Type = rbAgence.Checked ? TypeApporteur.Agence : TypeApporteur.Particulier,
                        RaisonSociale = txtRaisonSociale.Text,
                        NINEA = txtNINEA.Text,
                        RCCM = txtRCCM.Text,
                        TelephoneAgence=txtTelephoneAgence.Text,
                        AdresseBureau=txtAdresseBureau.Text,
                        EmailAgence=txtEmailAgence.Text,
                        NomGerant=txtNomGerant.Text
                    };
                    afRepo.Add(af);
                    MessageBox.Show(this, "L'apporteur d'affaire a été enregistré",
                             "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LApporteurAffairesEnCours.Prenom = txtPrenom.Text;
                    LApporteurAffairesEnCours.Nom = txtNom.Text;
                    LApporteurAffairesEnCours.Mobile1 = txtTelephoneMobile.Text;
                    LApporteurAffairesEnCours.TelephoneFixe = txtTelephoneFixe.Text;
                    LApporteurAffairesEnCours.Email = txtEmail.Text;
                    LApporteurAffairesEnCours.Adresse = txtAdresse.Text;
                    LApporteurAffairesEnCours.TauxCommission = nudTauxCommission.Value;
                    LApporteurAffairesEnCours.Type = rbAgence.Checked ? TypeApporteur.Agence : TypeApporteur.Particulier;
                    LApporteurAffairesEnCours.RaisonSociale = txtRaisonSociale.Text;
                    LApporteurAffairesEnCours.NINEA = txtNINEA.Text;
                    LApporteurAffairesEnCours.RCCM = txtRCCM.Text;
                    LApporteurAffairesEnCours.TelephoneAgence = txtTelephoneAgence.Text;
                    LApporteurAffairesEnCours.AdresseBureau = txtAdresseBureau.Text;
                    LApporteurAffairesEnCours.EmailAgence = txtEmailAgence.Text;
                    LApporteurAffairesEnCours.NomGerant = txtNomGerant.Text;

                    MessageBox.Show(this, "L'apporteur d'affaire a été modifié",
                            "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    afRepo.Save();
                }

                ChargerLesApporteurAffaires(txtNomRecherche.Text);
                EffacerForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,    
                                        "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void dgFactures_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgFactures.SelectedRows.Count > 0)
                {
                    int idContrat = (int)dgFactures.SelectedRows[0].Cells[0].Value;
                    var facturesCommissions = afRepo.GetFacturesCommissions(idContrat);
                    //cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) - cont.FactureCommissions.Where(fact => fact.FactureGenere == true).Sum(fact => fact.EncaissementGlobal.MontantGlobal),
                    if (facturesCommissions != null)
                    {
                        dgFacturesCommissions.DataSource = facturesCommissions.Where(fact => fact.FactureGenere == false).ToList().Select(fact => new
                        {
                            Id = fact.ID,
                            Date = fact.Date,
                            Numéro = fact.EncaissementGlobal.NumeroEncaissement,
                            Référence = fact.EncaissementGlobal.ReferencePaiement,
                            Encaissement = fact.EncaissementGlobal.MontantGlobal,
                            Montant = fact.MontantAPayer,
                            MontantVersé = fact.PaiementCommissions.Sum(enc => enc.MontantPaye),
                            RestantDu = fact.MontantAPayer - fact.PaiementCommissions.Sum(enc => enc.MontantPaye)

                        }).ToList();
                        dgFacturesCommissions.Columns[0].Width = 0;
                        dgFacturesCommissions.Columns[0].Visible = false;
                        dgFacturesCommissions.Columns[1].Width = 70;
                        dgFacturesCommissions.Columns[2].Width = 100;
                        dgFacturesCommissions.Columns[3].Width = 200;

                        dgFacturesCommissions.Columns[4].HeaderText = "Montant encaissé";
                        dgFacturesCommissions.Columns[4].Width = 70;
                        dgFacturesCommissions.Columns[4].DefaultCellStyle.Format = "### ### ###";
                        dgFacturesCommissions.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dgFacturesCommissions.Columns[5].HeaderText = "Commission dûe";
                        dgFacturesCommissions.Columns[5].Width = 70;
                        dgFacturesCommissions.Columns[5].DefaultCellStyle.Format = "### ### ###";
                        dgFacturesCommissions.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        ;

                        dgFacturesCommissions.Columns[6].HeaderText = "Commission versée";
                        dgFacturesCommissions.Columns[6].Width = 70;
                        dgFacturesCommissions.Columns[6].DefaultCellStyle.Format = "### ### ###";
                        dgFacturesCommissions.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dgFacturesCommissions.Columns[7].HeaderText = "Montant restant";
                        dgFacturesCommissions.Columns[7].Width = 70;
                        dgFacturesCommissions.Columns[7].DefaultCellStyle.Format = "### ### ###";
                        dgFacturesCommissions.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        ;
                        ;
                        tcCommissions.SelectedTab = tcCommissions.TabPages[2];
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdFacturer_Click(object sender, EventArgs e)
        {
            //var
            //var contratsApporteur = afRepo.GetAllContrats(LApporteurAffairesEnCours);
            //var lesContratAFacturer = contratsApporteur.Where(cont => cont.FactureCommissions.Where(fact => fact.Payee == false).Count() > 0);

            try
            {
                //Générer une facture globale en cumulant les restants à payer sur chaque facture
                afRepo.GenererFactureGlobale(LApporteurAffairesEnCours);
                //GenererFactureApporteurAffaire(LApporteurAffairesEnCours);
                AfficherHistoriqueFacturesGlobales();
                txtMontantTotalBrut.Text = string.Empty;
                txtTotalBRS.Text = string.Empty;
                txtMontantTotalNet.Text = string.Empty;
                dgFactures.DataSource = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                        "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void GenererFactureApporteurAffaire(ApporteurAffaire lApporteurAffairesEnCours)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                //if (!bPrint)
                //    xlApp.Visible = true;
                //xlWorkBook = xlApp.Workbooks.Add(misValue);
                string dossierTemplates = Tools.Tools.DossierTemplates;
               
                xlWorkBook = xlApp.Workbooks.Open(dossierTemplates+"FactureApporteur.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cells[2, 2] = DateTime.Now.Date;

                xlWorkSheet.Cells[10, 1] = lApporteurAffairesEnCours.NomComplet;

                var contratsApporteur = afRepo.GetAllContrats(lApporteurAffairesEnCours).ToList();
                var lesContratsAFacturer = contratsApporteur.Where(cont => cont.FactureCommissions.Where(fact => fact.FactureGenere == false).Count() > 0);

                //

                //var lesFacturesAFacturer = DB.FactureCommissions.Where(fact => fact.EncaissementGlobal.Contrat.ApporteurID == apporteurAffaires.Id
                //                                                       && fact.FactureGenere == false);
                //lesFacturesAFacturer = lesFacturesAFacturer.Where(fact => fact.Date < System.Data.Entity.DbFunctions.AddMonths(DateTime.Now, -1));
                //if (lesFacturesAFacturer.Count() <= 0)
                //    throw new Exception("Il n'y a pas d'encaissements à facturer pour cet apporteur d'affaires");

                //

                var dateDeReference = DateTime.Now.AddMonths(-1).Date;

                if (lesContratsAFacturer.Count() > 0)
                {
                    var LesContratsFacturesCumules = lesContratsAFacturer.ToList().Select(cont => new
                    {
                        Id = cont.Id,
                        Client = cont.Client.NomComplet,

                        Type = cont.TypeContrat.CategorieContrat,

                        Lot = cont.TypeContrat.CategorieContrat == CategorieContrat.Réservation ? cont.Lot.NumeroLot : cont.Lot.TypeVilla.CodeType,
                        Prix = cont.PrixFinal,
                        CumulVerse = cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal),

                        //Commission = cont.CommissionApporteur,
                        CumulPrecedant = cont.FactureCommissions.Where(fact => fact.FactureGenere == true).Sum(fact => fact.EncaissementGlobal.MontantGlobal),
                        BaseDeCalcul = cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) - cont.FactureCommissions.Where(fact => fact.FactureGenere == true).Sum(fact => fact.EncaissementGlobal.MontantGlobal),
                        //MontantDu = cont.FactureCommissions.Sum(fact => fact.MontantAPayer),
                        //Payé = cont.FactureCommissions.Sum(fact => fact.PaiementCommissions.Sum(paie => paie.MontantPaye)),
                        // RestantDu = cont.FactureCommissions.Sum(fact => fact.MontantAPayer) - cont.FactureCommissions.Sum(fact => fact.PaiementCommissions.Sum(paie => paie.MontantPaye)),
                        Commission = (cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) - cont.FactureCommissions.Where(fact => fact.FactureGenere == true).Sum(fact => fact.EncaissementGlobal.MontantGlobal)) * LApporteurAffairesEnCours.TauxCommission / 100,

                    }).ToList();


                    int numOrdre = 0;
                    int iDepart = 14;// à partir ligne 15
                    Excel.Range range = xlWorkSheet.UsedRange;


                    foreach (var contrat in LesContratsFacturesCumules)
                    {
                        if (contrat.BaseDeCalcul < 0)
                            break;
                        //Excel.Range r = (Excel.Range)xlWorkSheet.Cells[1, 1].EntireRow;
                        //r.Insert(Excel.XlInsertShiftDirection.xlShiftDown);

                        //Client
                        xlWorkSheet.Cells[numOrdre + iDepart, 1] = contrat.Client;
                        xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        //Catégorie contrat
                        xlWorkSheet.Cells[numOrdre + iDepart, 2] = contrat.Type.ToString();
                        xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[numOrdre + iDepart, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        //Lot
                        xlWorkSheet.Cells[numOrdre + iDepart, 3] = contrat.Lot;
                        xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[numOrdre + iDepart, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        //Prix
                        xlWorkSheet.Cells[numOrdre + iDepart, 4] = contrat.Prix;
                        xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[numOrdre + iDepart, 4].NumberFormat = "### ### ###";
                        //Cumul Versé
                        xlWorkSheet.Cells[numOrdre + iDepart, 5] = contrat.CumulVerse;
                        xlWorkSheet.Cells[numOrdre + iDepart, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[numOrdre + iDepart, 5].NumberFormat = "### ### ###";
                        //Cumul Precedent
                        xlWorkSheet.Cells[numOrdre + iDepart, 6] = contrat.CumulPrecedant;
                        xlWorkSheet.Cells[numOrdre + iDepart, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[numOrdre + iDepart, 6].NumberFormat = "### ### ###";
                        //Base de calcul
                        xlWorkSheet.Cells[numOrdre + iDepart, 7] = contrat.BaseDeCalcul;
                        xlWorkSheet.Cells[numOrdre + iDepart, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[numOrdre + iDepart, 7].NumberFormat = "### ### ###";
                        //Taux
                        xlWorkSheet.Cells[numOrdre + iDepart, 8] = LApporteurAffairesEnCours.TauxCommission.ToString("##") + "%";
                        xlWorkSheet.Cells[numOrdre + iDepart, 8].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                        //Commission
                        xlWorkSheet.Cells[numOrdre + iDepart, 9] = contrat.Commission;
                        xlWorkSheet.Cells[numOrdre + iDepart, 9].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[numOrdre + iDepart, 9].NumberFormat = "### ### ###";

                        numOrdre++;
                    }
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = "TOTAL BRUT COMMISSIONS A PAYER";
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Font.Bold = true;
                    xlWorkSheet.Range[xlWorkSheet.Cells[numOrdre + iDepart, 1], xlWorkSheet.Cells[numOrdre + iDepart, 8]].Merge();
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 8].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 9] = LesContratsFacturesCumules.Sum(com => com.Commission);
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].NumberFormat = "### ### ###";
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].Font.Bold = true;

                    numOrdre++;

                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = "BRS";
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Font.Bold = true;
                    xlWorkSheet.Range[xlWorkSheet.Cells[numOrdre + iDepart, 1], xlWorkSheet.Cells[numOrdre + iDepart, 8]].Merge();
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 8].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 9] = LesContratsFacturesCumules.Sum(com => com.Commission) * 5 / 100;
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].NumberFormat = "### ### ###";
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].Font.Bold = true;

                    numOrdre++;

                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = "MONTANT NET A PAYER";
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Font.Bold = true;
                    xlWorkSheet.Range[xlWorkSheet.Cells[numOrdre + iDepart, 1], xlWorkSheet.Cells[numOrdre + iDepart, 8]].Merge();
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 8].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 9] = LesContratsFacturesCumules.Sum(com => com.Commission) - LesContratsFacturesCumules.Sum(com => com.Commission) * 5 / 100;
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].NumberFormat = "### ### ###";
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].Font.Bold = true;

                    numOrdre++;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = " Arrêtée la présente facture à la somme de " + FrenchNumberToWords.convert((long)(LesContratsFacturesCumules.Sum(com => com.Commission) - LesContratsFacturesCumules.Sum(com => com.Commission) * 5 / 100)) + "francs CFA";
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Font.Bold = true;
                    xlWorkSheet.Range[xlWorkSheet.Cells[numOrdre + iDepart, 1], xlWorkSheet.Cells[numOrdre + iDepart, 8]].Merge();

                    numOrdre++; numOrdre++;
                    xlWorkSheet.Cells[numOrdre + iDepart, 9] = "L'APPORTEUR";
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].Font.Bold = true;
                }
                xlApp.Visible = true;
                //System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();
                //saveDlg.InitialDirectory = @"D:\GIIM\Export";
                //saveDlg.Filter = "Excel files (*.xls)|*.xls";
                //saveDlg.FilterIndex = 0;
                //saveDlg.RestoreDirectory = true;
                //saveDlg.Title = "Exporter le tableau de bord vers Excel";
                //if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    string path = saveDlg.FileName;
                //    xlWorkBook.SaveCopyAs(path);
                //    xlWorkBook.Saved = true;
                //    xlWorkBook.Close(true, misValue, misValue);
                //    xlApp.Quit();
                //}

                //xlWorkBook.SaveAs(@"d:\csharp.net-informations.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                //if (bPrint)
                //{
                //    xlWorkBook.Close(false, misValue, misValue);
                //    xlApp.Quit();

                //    releaseObject(xlWorkSheet);
                //    releaseObject(xlWorkBook);
                //    releaseObject(xlApp);
                //}

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dgFactureCommissionsEchues_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgFactureCommissionsEchues.SelectedRows.Count > 0)
                {
                    var lafactureGlobaleId = (int)dgFactureCommissionsEchues.SelectedRows[0].Cells[0].Value;

                    var facturesCommissionsGlobale = afRepo.GetFactureCommissionGlobales(lafactureGlobaleId);


                    var source = from facture in facturesCommissionsGlobale.FactureCommissions
                                 group facture by new
                                 {
                                     
                                     Client = facture.EncaissementGlobal.Contrat.Client.NomComplet,
                                     TypeContrat = facture.EncaissementGlobal.Contrat.TypeContrat,
                                     Lot = facture.EncaissementGlobal.Contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation ? facture.EncaissementGlobal.Contrat.Lot.NumeroLot : facture.EncaissementGlobal.Contrat.Lot.TypeVilla.CodeType,

                                 } into factureContrat
                                 select new
                                 {
                                     
                                     Client = factureContrat.Key.Client,
                                     TyContrat = factureContrat.Key.TypeContrat.LibelleTypeContrat,
                                     Lot = factureContrat.Key.Lot,
                                     MontantEncaisse = factureContrat.Sum(fact => fact.EncaissementGlobal.MontantGlobal),
                                     Commission= factureContrat.Sum(fact => fact.MontantAPayer)

                                 };


                    dgDetailsFactureCommissionGlobale.DataSource = source.ToList();

                    //dgDetailsFactureCommissionGlobale.Columns[0].Width = 0;
                    //dgDetailsFactureCommissionGlobale.Columns[0].Visible = false;
                    dgDetailsFactureCommissionGlobale.Columns[0].Width = 150;
                   
                    dgDetailsFactureCommissionGlobale.Columns[1].Width = 70;
                    dgDetailsFactureCommissionGlobale.Columns[2].Width = 45;
                   

                    dgDetailsFactureCommissionGlobale.Columns[3].HeaderText = "Montant encaissé";
                    dgDetailsFactureCommissionGlobale.Columns[3].Width = 70;
                    dgDetailsFactureCommissionGlobale.Columns[3].DefaultCellStyle.Format = "### ### ###";
                    dgDetailsFactureCommissionGlobale.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    dgDetailsFactureCommissionGlobale.Columns[4].HeaderText = "Commission";
                    dgDetailsFactureCommissionGlobale.Columns[4].Width = 65;
                    dgDetailsFactureCommissionGlobale.Columns[4].DefaultCellStyle.Format = "### ### ###";
                    dgDetailsFactureCommissionGlobale.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dgDetailsFactureCommissionGlobale.DataSource = facturesCommissionsGlobale.FactureCommissions.ToList()
                    //     .Select(facture => new
                    //     {
                    //         Date = facture.EncaissementGlobal.DateEncaissement,
                    //         Client = facture.EncaissementGlobal.Contrat.Client.NomComplet,
                    //         TypeContrat = facture.EncaissementGlobal.Contrat,
                    //         Lot = facture.EncaissementGlobal.Contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation ? facture.EncaissementGlobal.Contrat.Lot.NumeroLot: facture.EncaissementGlobal.Contrat.Lot.TypeVilla.CodeType,
                    //         Numero =facture.EncaissementGlobal.NumeroEncaissement,
                    //         Référence=facture.EncaissementGlobal.ReferencePaiement,
                    //         MontantEncaisse = facture.EncaissementGlobal.MontantGlobal,
                    //         Commission=facture.MontantAPayer,
                    //         Soldé=facture.Payee

                    //     }).ToList();

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void chkVoirListeApporteurs_CheckedChanged(object sender, EventArgs e)
        {
            if(chkVoirListeApporteurs.Checked)
            {
                //chkVoirListeApporteurs.Text = "Voir la liste des apporteurs d'affaires";
                splitContainer1.Panel1Collapsed = false;
                splitContainer1.Panel1.Show();
               
            }
            else
            {
                splitContainer1.Panel1Collapsed = true;
                splitContainer1.Panel1.Hide();
                //chkVoirListeApporteurs.Text = "Cacher la liste des apporteurs d'affaires";
            }
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GenererFactureApporteurAffaire(LApporteurAffairesEnCours);
        }

        private void cmdRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ChargerLesApporteurAffaires(txtNomRecherche.Text);
            }
            catch (Exception ex)
	        {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des Apporteurs d'affaire", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdSupprimer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(this, "Voulez vous réellement supprimer l'Apporteur d'Affaires?",
                   "Prosopis - Gestion des Apporteurs d'affaire", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    afRepo.Delete(LApporteurAffairesEnCours);
                    ChargerLesApporteurAffaires(txtNomRecherche.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des Apporteurs d'affaire", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rbParticulier_CheckedChanged(object sender, EventArgs e)
        {
            if(rbParticulier.Checked)
            {
                tcTypeApporteur.SelectedIndex = 0;
            }
        }

        private void rbAgence_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAgence.Checked)
            {
                tcTypeApporteur.SelectedIndex = 1;
            }
        }

        private void lvApporteurAffaires_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvApporteurAffaires.SelectedItems.Count > 0)
                {
                    EffacerForm();
                    int AAffId = (int)lvApporteurAffaires.SelectedItems[0].Tag;
                    LApporteurAffairesEnCours = afRepo.FindById(AAffId);
                    dgContrats.DataSource = null;
                    dgDetailsFactureCommissionGlobale.DataSource = null;
                    dgFactures.DataSource = null;
                    dgFacturesCommissions.DataSource = null;

                    if (LApporteurAffairesEnCours != null)
                    {
                        AfficherLApporteurAffaire(LApporteurAffairesEnCours);
                        lbMontantTotalCommissionsRestantes.Visible = true;
                        lbMontantTotalcommissionsVersees.Visible = true;
                        lbMontantTotalCommissions.Visible = true;
                        txtMontantTotalCommissionsRestantes.Visible = true;
                        txtMontantTotalcommissionsVersees.Visible = true;
                        txtMontantTotalCommissions.Visible = true;
                        VerouillerForm();
                        //tcCommissions.SelectedTab = tcCommissions.TabPages[0];
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmdFermer_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkFacturesGenerees_CheckedChanged(object sender, EventArgs e)
        {
            if(chkFacturesGenerees.Checked)
            {
                try
                {
                    if (MessageBox.Show(this, "Voulez vous définitivement générer la facture? Attention cette opération est irreversible. ",
                        "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        //Générer une facture globale en cumulant les restants à payer sur chaque facture
                        afRepo.GenererFactureGlobale(LApporteurAffairesEnCours);
                        //GenererFactureApporteurAffaire(LApporteurAffairesEnCours);
                        AfficherHistoriqueFacturesGlobales();
                        txtMontantTotalBrut.Text = string.Empty;
                        txtTotalBRS.Text = string.Empty;
                        txtMontantTotalNet.Text = string.Empty;
                        dgFactures.DataSource = null;
                        chkFacturesGenerees.Checked = false;
                        chkFacturesGenerees.Enabled = false;
                        button1.Enabled = false;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(this, "Erreur:..." + ex.Message,
                                            "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chkFacturesGenerees.Checked = false;
                }
            }
        }
    }
}

