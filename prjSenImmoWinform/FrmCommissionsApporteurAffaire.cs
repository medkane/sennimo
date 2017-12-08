using prjSenImmoWinform.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prjSenImmoWinform.Models;


namespace prjSenImmoWinform
{
    public partial class FrmCommissionsApporteurAffaire : Form
    {
        private ApporteurAffaireRepository afRepo;
        private ContratRepository contratRepo;
        private ApporteurAffaire LApporteurAffairesEnCours;
        private decimal montantBrutFactureCommissionCumulees=0;
        private decimal montantBRSFactureCommissionCumulees=0;
        private decimal montantNetFactureCommissionCumulees=0;

        private List<FactureCommissionGlobale> lesFacturesGobalesPayees ;

        public FrmCommissionsApporteurAffaire()
        {
            InitializeComponent();

            afRepo = new ApporteurAffaireRepository();
            contratRepo = new ContratRepository();
            cmbModePaiement.DataSource = Enum.GetValues(typeof(ModePaiement));
            lesFacturesGobalesPayees = new List<FactureCommissionGlobale>();
        }

        private void ChargerLesAporteursAffaires()
        {
            try
            {
                cmbApporteurAffaires.DataSource = afRepo.GetAllApporteurAffaires().ToList();
                cmbApporteurAffaires.DisplayMember = "NomComplet";
              

                cmbApporteurAffaires.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbApporteurAffaires.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                foreach (var item in afRepo.GetAllApporteurAffaires())
                {
                    data.Add(item.NomComplet);
                }
                cmbApporteurAffaires.AutoCompleteCustomSource = data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cmbApporteurAffaires_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Réinitialiser dles données spécifiques à l'Apporteur d'affaire
                lesFacturesGobalesPayees.Clear();
                dgFactureCommissionsEchues.Rows.Clear();
                montantBrutFactureCommissionCumulees = 0;
                montantBRSFactureCommissionCumulees = 0;
                montantNetFactureCommissionCumulees = 0;

                if (cmbApporteurAffaires.SelectedItem!=null)
                {

                    LApporteurAffairesEnCours =(ApporteurAffaire) cmbApporteurAffaires.SelectedItem;

                    if (LApporteurAffairesEnCours != null)
                    {
                        AfficherApporteurAffaire(LApporteurAffairesEnCours);
                        lbMontantTotalCommissionsRestantes.Visible = true;
                        lbMontantTotalcommissionsVersees.Visible = true;
                        lbMontantTotalCommissions.Visible = true;
                        txtMontantTotalCommissionsRestantes.Visible = true;
                        txtMontantTotalcommissionsVersees.Visible = true;
                        txtMontantTotalCommissions.Visible = true;

                        ChargerMouvementsCompteApporteurAffaire();
                        AfficherHistoriqueFacturesGlobales();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerMouvementsCompteApporteurAffaire()
        {
            try
            {
                dgCompteApporteurAffaires.DataSource = afRepo.GetOperationsApporteurAffaires(LApporteurAffairesEnCours.Id).ToList().Select
                    (mvt => new
                    {
                        Date = mvt.DateOp.ToShortDateString(),
                        NumeroPiece = mvt.NumeroPiece,
                        LibelleOp = mvt.LibelleOp,
                        Debit = mvt.Debit,
                        Credit = mvt.Credit-mvt.Credit*5/100,
                        Solde = mvt.TypeOp=="F"?mvt.Solde - mvt.Solde * 5 / 100: mvt.Solde,
                        TypeMouvement = mvt.TypeOp,
                        ID = mvt.Id
                    }).ToList();
                dgCompteApporteurAffaires.Columns[0].Width = 80;
                dgCompteApporteurAffaires.Columns[1].Width = 130;
                dgCompteApporteurAffaires.Columns[2].Width = 560;
                dgCompteApporteurAffaires.Columns[3].Width = 110;
                dgCompteApporteurAffaires.Columns[3].DefaultCellStyle.Format = "### ### ###";
                dgCompteApporteurAffaires.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCompteApporteurAffaires.Columns[4].Width = 110;
                dgCompteApporteurAffaires.Columns[4].DefaultCellStyle.Format = "### ### ###";
                dgCompteApporteurAffaires.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCompteApporteurAffaires.Columns[5].Width = 110;
                dgCompteApporteurAffaires.Columns[5].DefaultCellStyle.Format = "### ### ###";
                dgCompteApporteurAffaires.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCompteApporteurAffaires.Columns[6].Width = 0;
                dgCompteApporteurAffaires.Columns[6].Visible = false;
                dgCompteApporteurAffaires.Columns[7].Width = 0;
                dgCompteApporteurAffaires.Columns[7].Visible = false;



            }
            catch (Exception)
            {

                throw;
            }

        }

        private void AfficherApporteurAffaire(ApporteurAffaire apporteur)
        {
            try
            {
                //txtDateSouscription.Text = client.DateCreation.ToShortDateString();
                //txtPrenom.Text = apporteur.Prenom;
                //txtNom.Text = apporteur.Nom;
                txtTelephoneFixe.Text = apporteur.TelephoneFixe;
                txtTelephoneMobile.Text = apporteur.Mobile1;
                txtAdresse.Text = apporteur.Adresse;
                nudTauxCommission.Value = apporteur.TauxCommission;
                //AfficherLesContrats(apporteur);
                //AfficherLesFactures(apporteur);
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void AfficherHistoriqueFacturesGlobales()
        {
            var factureCommissionsGlobalesImpayees = afRepo.GetAllFactureCommissionGlobales(LApporteurAffairesEnCours).Where(fact => fact.Payee==false).ToList()
                                                    .Select(factureGlobale => new
                                                    {
                                                        Id = factureGlobale.ID,
                                                        Date = factureGlobale.Date,
                                                        Montant = factureGlobale.MontantAPayer,

                                                        //Soldée = factureGlobale.Payee,
                                                        BRS = factureGlobale.MontantAPayer * 5 / 100,
                                                        Net = factureGlobale.MontantAPayer - factureGlobale.MontantAPayer * 5 / 100,
                                                        
                                                    }).ToList();

            //dgFactureCommissionsEchues.Columns[0].Width = 0;
            //dgFactureCommissionsEchues.Columns[0].Visible = false;
            //dgFactureCommissionsEchues.Columns[1].Width = 70;
            //dgFactureCommissionsEchues.Columns[2].HeaderText = "Montant Brut";
            //dgFactureCommissionsEchues.Columns[2].Width = 67;
            //dgFactureCommissionsEchues.Columns[2].DefaultCellStyle.Format = "### ### ###";
            //dgFactureCommissionsEchues.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ////dgFactureCommissionsEchues.Columns[3].Width = 45;
            //dgFactureCommissionsEchues.Columns[3].HeaderText = "BRS";
            //dgFactureCommissionsEchues.Columns[3].Width = 67;
            //dgFactureCommissionsEchues.Columns[3].DefaultCellStyle.Format = "### ### ###";
            //dgFactureCommissionsEchues.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgFactureCommissionsEchues.Columns[4].HeaderText = "Montant Net";
            //dgFactureCommissionsEchues.Columns[4].Width = 67;
            //dgFactureCommissionsEchues.Columns[4].DefaultCellStyle.Format = "### ### ###";
            //dgFactureCommissionsEchues.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (var item in factureCommissionsGlobalesImpayees)
            {



                dgFactureCommissionsEchues.Rows.Add(item.Id, item.Date.Value.ToShortDateString(), item.Montant.ToString("### ### ###"),item.BRS.ToString("### ### ###"), item.Net.ToString("### ### ###"), false);
                //dgPalettesFrigoReception.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
                
            }


           

            //DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            //checkBoxColumn.HeaderText = "Payer";
            //checkBoxColumn.Width = 30;
            //checkBoxColumn.Name = "checkBoxColumn";
            //dgFactureCommissionsEchues.Columns.Insert(5, checkBoxColumn);
        }

        private void cmdEnregistrerPaiement_Click(object sender, EventArgs e)
        {
            try
            {
                if (LApporteurAffairesEnCours!=null)
                {
                    EnregistrerPaiementApporteur();
                }
                txtMontantPaiement.Text = string.Empty;
                txtReferencePaiement.Text = string.Empty;
                txtCommentairePaiement.Text = string.Empty;
                cmbModePaiement.SelectedIndex=-1;

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur Enregistrement paiement commission:... " + ex.Message,
                        "Prosopis - Paiement des commissions", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnregistrerPaiementApporteur()
        {
            try
            {
                var dateVersement = dtpDateVersement.Value.Date;
                var montantVersement = decimal.Parse(txtMontantPaiement.Text);
                var referencePaiement = txtReferencePaiement.Text;
                var commentairesVersement = txtCommentairePaiement.Text;
                var modePaiement = (ModePaiement)cmbModePaiement.SelectedItem;

               afRepo.EnregistrerPaiementCommission(LApporteurAffairesEnCours.Id, lesFacturesGobalesPayees, dateVersement, montantVersement, modePaiement, referencePaiement, commentairesVersement);

                MessageBox.Show(this, "Le paiement de la commission a été enregistré avec succes",
                               "Prosopis Paiement des commissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //AfficherLeContrat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:... " + ex.Message,
                                      "Prosopis -  Paiement des commissions", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmCommissionsApporteurAffaire_Load(object sender, EventArgs e)
        {
          
                ChargerLesAporteursAffaires();
        }

        private void dgFactureCommissionsEchues_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            dgFactureCommissionsEchues.CommitEdit(DataGridViewDataErrorContexts.Commit);


           
        }

        private void dgFactureCommissionsEchues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {

                    if (dgFactureCommissionsEchues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "True")
                    {
                        //DataGridViewRow row = dgPalettesFrigoReception.Rows[e.RowIndex];
                        //string numeroPalette = dgPalettesFrigoReception.Rows[e.RowIndex].Cells[0].Value.ToString();
                        ////rechercher la paletteFinie
                        //var palette = (from pal in db.PaletteProductions
                        //               where pal.CodePaletteProduction == numeroPalette
                        //               && pal.Chargement.LotRecolte.CodeLot == leLotEnCours.CodeLot
                        //               select pal).FirstOrDefault();

                        //if (palette != null)
                        //{
                        //    VerserPaletteProduction(palette);
                        //    row.DefaultCellStyle.BackColor = Color.LimeGreen;
                        //    row.ReadOnly = true;
                        //}
                        var factureGlobaleId=Int16.Parse(dgFactureCommissionsEchues.Rows[e.RowIndex].Cells[0].Value.ToString());
                        var factureGlobale = afRepo.GetFactureCommissionGlobales(factureGlobaleId);
                        lesFacturesGobalesPayees.Add(factureGlobale);
                        montantBrutFactureCommissionCumulees += decimal.Parse(dgFactureCommissionsEchues.Rows[e.RowIndex].Cells[2].Value.ToString());
                        montantBRSFactureCommissionCumulees += decimal.Parse(dgFactureCommissionsEchues.Rows[e.RowIndex].Cells[3].Value.ToString());
                        montantNetFactureCommissionCumulees += decimal.Parse(dgFactureCommissionsEchues.Rows[e.RowIndex].Cells[4].Value.ToString());
                        txtMontantPaiement.Text = montantNetFactureCommissionCumulees.ToString("### ### ###");
                    }
                    else
                    {
                        MessageBox.Show("NOK");
                        var factureGlobaleId = Int16.Parse(dgFactureCommissionsEchues.Rows[e.RowIndex].Cells[0].Value.ToString());
                        var factureGlobale = afRepo.GetFactureCommissionGlobales(factureGlobaleId);
                        lesFacturesGobalesPayees.Remove(factureGlobale);
                        montantBrutFactureCommissionCumulees -= decimal.Parse(dgFactureCommissionsEchues.Rows[e.RowIndex].Cells[2].Value.ToString());
                        montantBRSFactureCommissionCumulees -= decimal.Parse(dgFactureCommissionsEchues.Rows[e.RowIndex].Cells[3].Value.ToString());
                        montantNetFactureCommissionCumulees -= decimal.Parse(dgFactureCommissionsEchues.Rows[e.RowIndex].Cells[4].Value.ToString());
                        txtMontantPaiement.Text = montantNetFactureCommissionCumulees.ToString("### ### ###");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur: ..." + ex.Message, "Paiement des commissions apporteur d'affaires ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCommentairePaiement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdEnregistrerPaiement_Click(sender, e);
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

