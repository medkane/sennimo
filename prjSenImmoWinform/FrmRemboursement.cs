using prjSenImmoWinform.DAL;
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

namespace prjSenImmoWinform
{
    public partial class FrmRemboursement : Form
    {
        private ClientRepository clientRep;
        private ContratRepository contratRep;
        private Client leClientEncours;
        private IEnumerable<Contrat> lesContratTrouves;
        private Contrat leContratEnCours;
        private SoldeDeToutCompte leSoldeToutCompteEnCours;

        public FrmRemboursement()
        {
            InitializeComponent();
            try
            {
                clientRep = new ClientRepository();
                contratRep = new ContratRepository();


                cmbModePaiement.DataSource = Enum.GetValues(typeof(ModePaiement));

                cmbClients.DataSource = clientRep.GetAllClientsResilie().ToList();
                cmbClients.DisplayMember = "NomComplet";


                cmbClients.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbClients.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                foreach (var item in clientRep.GetAllClientsResilie())
                {
                    data.Add(item.NomComplet);
                }
                cmbClients.AutoCompleteCustomSource = data;
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:..." + ex.Message,
                      "Prosopis Gestion des rembousements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbClients_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (cmbClients.SelectedItem != null)
                {
                    leClientEncours = (Client)cmbClients.SelectedItem;
                    if (leClientEncours != null)
                    {
                        txtDateNaissance.Text = leClientEncours.DateDeNaissance.Value.Date.ToShortDateString();
                        txtLieuNaissance.Text = leClientEncours.LieuDeNaissance;
                        txtAdresse.Text = leClientEncours.Adresse;
                        txtNumeroMobile.Text = leClientEncours.Mobile1;
                        txtNumeroFixe.Text = leClientEncours.TelephoneFixe;
                        txtEmail.Text = leClientEncours.Email;


                        lesContratTrouves = leClientEncours.Contrats.Where(cont => cont.Statut== StatutContrat.Résilié);
                        if (lesContratTrouves.Count() > 0)
                        {
                            lbTypeClient.Text = "Client";
                            if (lesContratTrouves.Count() == 1)
                            {
                                // tcEntete.SelectedTab = tcEntete.TabPages[1];
                                leContratEnCours = lesContratTrouves.FirstOrDefault();
                                leSoldeToutCompteEnCours = contratRep.GetSoldeToutCompte(leContratEnCours.Id);
                                if (leContratEnCours != null)
                                {
                                    AfficherMouvementsCompteRemboursement();
                                }
                            }
                        }

                        
                       
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:... " + ex.Message,
                                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dgCompteRemboursements.Columns[2].Width = 635;
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

        private void cmdEnregistrerPaiement_Click(object sender, EventArgs e)
        {
            try
            {
                var dateVersement = dtpDateRemboursement.Value.Date;
                var montantVersement = decimal.Parse(txtMontantRemboursement.Text);
                var referencePaiement = txtReferencePaiement.Text;
                var commentairesVersement = txtCommentaireRemboursement.Text;
                var modePaiement = (ModePaiement)cmbModePaiement.SelectedItem;

                contratRep.EnregistrerRemboursement(leSoldeToutCompteEnCours.Id, dateVersement, montantVersement, modePaiement, referencePaiement, commentairesVersement);

                MessageBox.Show(this, "Le versement a été enregistré avec succes",
                               "Prosopis Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AfficherMouvementsCompteRemboursement();
                tcCompteClient.SelectedTab = tcCompteClient.TabPages[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:... " + ex.Message,
                                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMontantRemboursement_TextChanged(object sender, EventArgs e)
        {
            decimal a;
            if (!decimal.TryParse(txtMontantRemboursement.Text, out a))
            {
                // If not int clear textbox text or Undo() last operation
                txtMontantRemboursement.Clear();
            }
            else
            {
                txtMontantRemboursement.Text = decimal.Parse(txtMontantRemboursement.Text).ToString("### ### ###");
                txtMontantRemboursement.SelectionStart = txtMontantRemboursement.Text.Length;
            }
        }
    }
}
