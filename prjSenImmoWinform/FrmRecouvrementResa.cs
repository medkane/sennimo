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
using System.Net.Mail;
using Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;


namespace prjSenImmoWinform
{
    public partial class FrmRecouvrementResa : Form
    {
        ContratRepository contratRep;
        private int leProjetId;
        private IlotRepository ilotRepository;
        private Contrat leContratEnCours;
        ClientRepository clientRep;

        IEnumerable<Contrat> contratsResa;

        public FrmRecouvrementResa(int projetId)
        {
            InitializeComponent();
            try
            {
                contratRep = new ContratRepository();
                ilotRepository = new IlotRepository();
                clientRep = new ClientRepository();
                ChargerNiveauAvancements();
                ChargerIlots();
                leProjetId = projetId;
                //dtpDateFinPeriode.Value = DateTime.Today.AddDays(0 - DateTime.Today.Day).Date;
                //dtpDateDebutPeriode.Value = dtpDateFinPeriode.Value.AddDays(1 - dtpDateFinPeriode.Value.Day).Date;

                var lesCLients = clientRep.GetAllClients(false).ToList().Where(cli => cli.ProjetId == projetId && cli.Contrats.Where(cont => cont.TypeContrat.CategorieContrat == CategorieContrat.Réservation && cont.Statut== StatutContrat.Actif).Count() > 0).ToList();
                cmbClients.DataSource = lesCLients;
                cmbClients.DisplayMember = "NomComplet";
                cmbClients.ValueMember = "ID";

                cmbClients.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbClients.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                foreach (var item in lesCLients)
                {
                    data.Add(item.NomComplet);
                }
                cmbClients.AutoCompleteCustomSource = data;
                cmbClients.SelectedIndex = -1;
                AfficherNiveauContratClient(leProjetId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public FrmRecouvrementResa(string sType,int projetId):this(projetId)
        {
            InitializeComponent();
            try
            {
                contratRep = new ContratRepository();
                ilotRepository = new IlotRepository();
                clientRep = new ClientRepository();
                ChargerNiveauAvancements();
                ChargerIlots();
                //dtpDateFinPeriode.Value = DateTime.Today.AddDays(0 - DateTime.Today.Day).Date;
                //dtpDateDebutPeriode.Value = dtpDateFinPeriode.Value.AddDays(1 - dtpDateFinPeriode.Value.Day).Date;

                var lesCLients = clientRep.GetAllClients(false).ToList().Where(cli => cli.Contrats.Where(cont => cont.TypeContrat.CategorieContrat == CategorieContrat.Réservation && cont.Statut == StatutContrat.Actif).Count() > 0).ToList();
                cmbClients.DataSource = lesCLients;
                cmbClients.DisplayMember = "NomComplet";
                cmbClients.ValueMember = "ID";

                cmbClients.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbClients.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                foreach (var item in lesCLients)
                {
                    data.Add(item.NomComplet);
                }
                cmbClients.AutoCompleteCustomSource = data;
                cmbClients.SelectedIndex = -1;
                AfficherNiveauContratClient(leProjetId);
            }
            catch (Exception)
            {
                throw;
            }   
        }
        private void ChargerNiveauAvancements()
        {
            try
            {
                cmbNiveauAvancements.DataSource = ilotRepository.GetNiveauxAvancementsAppelsDeFond().ToList();
                //cmbNiveauAvancements.DataSource = ilotRepository.GetNiveauxAvancements(leProjetId).ToList();
                cmbNiveauAvancements.DisplayMember = "LibelleCommercial";
                cmbNiveauAvancements.ValueMember = "ID";
                cmbNiveauAvancements.SelectedIndex = -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ChargerIlots()
        {
            try
            {
                cmbIlots.DataSource = ilotRepository.List.ToList();
                cmbIlots.DisplayMember = "NomIlot";
                cmbIlots.ValueMember = "ID";
                cmbIlots.SelectedIndex = -1;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void AfficherFacturesClient()
        {
            try
            {
                var factures = contratRep.GetNewAppelsDeFonds().ToList();

                TypeEtatAvancement niveauAvancement = null;
                Ilot ilot = null;

                //Filtrer par niveau d'appels de fond
                if (cmbNiveauAvancements.SelectedItem != null)
                {
                    niveauAvancement = (TypeEtatAvancement)cmbNiveauAvancements.SelectedItem;
                }
                if (niveauAvancement != null)
                    factures = factures.Where(fact => fact.EtatAvancement.TypeEtatAvancementID == niveauAvancement.ID).ToList();

                //Filtrer par Ilot
                if (cmbIlots.SelectedItem != null)
                {
                    ilot = (Ilot)cmbIlots.SelectedItem;
                }
                if (ilot != null)
                    factures = factures.Where(fact => fact.Contrat.Lot.IlotID == ilot.Id).ToList();

                //Filtrer selon l'etat du solde de la facture
                if (rbImpayes.Checked)
                    factures = factures.Where(fact => fact.FacturePayee == false).ToList();
                if (rbSoldes.Checked)
                    factures = factures.Where(fact => fact.FacturePayee == true).ToList();

                dgAppelsFond.DataSource = factures.ToList()
                    .Select(fact => new
                    {
                        ID = fact.Id,
                        //Numéro=fact.NumeroFacture,
                        Date = fact.DateEcheanceFacture != null ? fact.DateEcheanceFacture.Value.ToShortDateString() : "",
                        Client = fact.Client.NomComplet,
                        Lot = fact.Contrat.Lot.NumeroLot,
                        Numéro = fact.NumeroFacture,
                        Motif = fact.Motif,
                        Taux = fact.Contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation ? fact.EtatAvancement.TypeEtatAvancement.TauxDecaissement.ToString("###") + "%" : "",
                        Montant = fact.Montant,
                        Encaissé = fact.MontantEncaisse,
                        Restant = fact.Montant - fact.MontantEncaisse,
                        Soldée = fact.FacturePayee
                    }).ToList();

                FormatterGrilleAppelsFond();
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void AfficherNiveauContratClient(int projetId)
        {

            try
            {
                lvRecouvrementResa.Items.Clear();

                TypeEtatAvancement niveauAvancement = null;
                Ilot ilot = null;
                contratsResa = contratRep.GetContratsResa().Where(cont =>cont.ProjetId==projetId &&  cont.Statut== StatutContrat.Actif);//.Where(cont => cont.Factures.Where(f => f.EtatAvancement.TypeEtatAvancement.NiveauTechnique == true).Count() >0);

                //Filtrer par niveau d'appels de fond
                if (cmbNiveauAvancements.SelectedItem != null)
                {
                    niveauAvancement = (TypeEtatAvancement)cmbNiveauAvancements.SelectedItem;
                }
                if (niveauAvancement != null)
                    contratsResa = contratsResa.Where(cont => cont.Factures.Where(f => f.Active == true && f.Echue == true
                   && f.TypeFacture == TypeFacture.AppelDeFond && f.EtatAvancement.TypeEtatAvancement.NiveauTechnique == true).Max(fact => fact.EtatAvancement.TypeEtatAvancementID) == niveauAvancement.ID).ToList();

                //Filtrer par Ilot
                if (cmbIlots.SelectedItem != null)
                {
                    ilot = (Ilot)cmbIlots.SelectedItem;
                }
                if (ilot != null)
                    contratsResa = contratsResa.Where(cont => cont.Lot.IlotID == ilot.Id).ToList();

                ////Filtrer selon l'etat du solde de la facture
                //if (rbImpayes.Checked)
                //    factures = factures.Where(fact => fact.FacturePayee == false).ToList();
                //if (rbSoldes.Checked)
                //    factures = factures.Where(fact => fact.FacturePayee == true).ToList();

                if (cmbClients.SelectedItem != null)
                {
                    Client client = cmbClients.SelectedItem as Client;
                    contratsResa = contratsResa.Where(cont => cont.ClientID == client.ID).ToList();
                }
               
                if (rbImpayes.Checked)
                    contratsResa = contratsResa.Where(cont => cont.Factures.Where(fact => fact.TypeFacture == TypeFacture.AppelDeFond && fact.Echue == true
                        && fact.FacturePayee == false).Count() > 0).ToList();
                if (rbSoldes.Checked)
                    contratsResa = contratsResa.Where(cont => cont.Factures.Where(fact => fact.TypeFacture == TypeFacture.AppelDeFond && fact.Echue == true
                        && fact.FacturePayee == false).Count() <= 0).ToList();
                DateTime dateReference = DateTime.Parse("01/01/1900");
                var contr = contratsResa.ToList();
                foreach (Contrat    contrat in contratsResa.ToList())
                {
                    int nbImpaye = contrat.Factures.Where(fact => fact.TypeFacture == TypeFacture.AppelDeFond && fact.Echue == true
                        && fact.FacturePayee == false).Count();
                    ListViewItem lviContrat = new ListViewItem(contrat.Client.NomComplet);
                    lviContrat.SubItems.Add(contrat.DateSouscription.Value.ToShortDateString());
                    lviContrat.SubItems.Add(contrat.NumeroContrat);
                    lviContrat.SubItems.Add(contrat.Lot.NumeroLot);
                    lviContrat.SubItems.Add(contrat.Lot.TypeVilla.CodeType);
                    lviContrat.SubItems.Add(contrat.PrixFinal.ToString("### ### ##0"));
                    lviContrat.SubItems.Add(contrat.Factures.Where(f => f.Active == true && f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Max(f => f.EtatAvancement.DateSaisieAvancement.HasValue? f.EtatAvancement.DateSaisieAvancement.Value: dateReference).ToShortDateString());
                    lviContrat.SubItems.Add(contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.EtatAvancement.TypeEtatAvancement.TauxDecaissement).ToString("###") + "%");
                    lviContrat.SubItems.Add(contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant).ToString("### ### ##0"));
                    lviContrat.SubItems.Add(contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ##0"));
                    //lviContrat.SubItems.Add((contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant) - contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ###"));
                    lviContrat.SubItems.Add((contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant) - contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ##0"));
                    lviContrat.Tag = contrat.Id;
                    if (nbImpaye > 0)
                        lviContrat.ImageIndex = 0;
                    else
                        lviContrat.ImageIndex = 1;
                    lvRecouvrementResa.Items.Add(lviContrat);
                }
                txtNbNombreContrats.Text = contratsResa.Count().ToString();
                txtCATotal.Text = contratsResa.Sum(contrat => contrat.PrixFinal).ToString("### ### ### ##0");
                txtTotalCumulé.Text = contratsResa.Sum(contrat => contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant)).ToString("### ### ### ##0");
                txtTotalEncaissé.Text= contratsResa.Sum(contrat => contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ### ##0");
                txtTotalRestant.Text = contratsResa.Sum(contrat => (contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant) - contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Encaissements.Sum(enc => enc.Montant)))).ToString("### ### ### ##0");
                //dgAppelsFond.DataSource = contratsResa.ToList().Select(cont => new
                //{
                //    Id = cont.Id,
                //    Lot = cont.Lot.NumeroLot,
                //    Type = cont.Lot.TypeVilla.CodeType,
                //    Client = cont.Client.NomComplet,
                //    PrixVente = cont.PrixFinal,
                //    SeuilCumule = cont.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.EtatAvancement.TypeEtatAvancement.TauxDecaissement).ToString("###") + "%",
                //    MontantCumule = cont.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant),
                //    MontantEncaisse = cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal),
                //    MontantRestant= cont.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant) - cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)
                //}).ToList();


                //TypeEtatAvancement niveauAvancement = null;
                //Ilot ilot = null;

                ////Filtrer par niveau d'appels de fond
                //if (cmbNiveauAvancements.SelectedItem != null)
                //{
                //    niveauAvancement = (TypeEtatAvancement)cmbNiveauAvancements.SelectedItem;
                //}
                //if (niveauAvancement != null)
                //    factures = factures.Where(fact => fact.EtatAvancement.TypeEtatAvancementID == niveauAvancement.ID).ToList();

                ////Filtrer par Ilot
                //if (cmbIlots.SelectedItem != null)
                //{
                //    ilot = (Ilot)cmbIlots.SelectedItem;
                //}
                //if (ilot != null)
                //    factures = factures.Where(fact => fact.Contrat.Lot.IlotID == ilot.Id).ToList();

                ////Filtrer selon l'etat du solde de la facture
                //if (rbImpayes.Checked)
                //    factures = factures.Where(fact => fact.FacturePayee == false).ToList();
                //if (rbSoldes.Checked)
                //    factures = factures.Where(fact => fact.FacturePayee == true).ToList();

                //dgAppelsFond.DataSource = factures.ToList()
                //    .Select(fact => new
                //    {
                //        ID = fact.Id,
                //        //Numéro=fact.NumeroFacture,
                //        Date = fact.DateEcheanceFacture != null ? fact.DateEcheanceFacture.Value.ToShortDateString() : "",
                //        Client = fact.Client.NomComplet,
                //        Lot = fact.Contrat.Lot.NumeroLot,
                //        Numéro = fact.NumeroFacture,
                //        Motif = fact.Motif,
                //        Taux = fact.Contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation ? fact.EtatAvancement.TypeEtatAvancement.TauxDecaissement.ToString("###") + "%" : "",
                //        Montant = fact.Montant,
                //        Encaissé = fact.MontantEncaisse,
                //        Restant = fact.Montant - fact.MontantEncaisse,
                //        Soldée = fact.FacturePayee
                //    }).ToList();

                //FormatterGrilleAppelsFond();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FormatterGrilleAppelsFond()
        {
            dgAppelsFond.Columns[0].Width = 0;
            dgAppelsFond.Columns[0].Visible = false;
            dgAppelsFond.Columns[1].Width = 60;


            dgAppelsFond.Columns[2].Width = 60;
            dgAppelsFond.Columns[3].Width = 200;
            // dgAppelsFond.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgAppelsFond.Columns[4].Width = 80;
            dgAppelsFond.Columns[4].HeaderText = "Prix de vente";
            dgAppelsFond.Columns[4].DefaultCellStyle.Format = "### ### ###";
            dgAppelsFond.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgAppelsFond.Columns[5].Width = 50;
            dgAppelsFond.Columns[5].HeaderText = "Taux Cumulé";
            dgAppelsFond.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgAppelsFond.Columns[6].Width = 80;
            dgAppelsFond.Columns[6].HeaderText = "Montant Cumulé";
            dgAppelsFond.Columns[6].DefaultCellStyle.Format = "### ### ###";
            dgAppelsFond.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgAppelsFond.Columns[7].Width = 80;
            dgAppelsFond.Columns[7].HeaderText = "Montant encaissé";
            dgAppelsFond.Columns[7].DefaultCellStyle.Format = "### ### ###";
            dgAppelsFond.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgAppelsFond.Columns[8].Width = 80;
            dgAppelsFond.Columns[8].HeaderText = "Montant restant";
            dgAppelsFond.Columns[8].DefaultCellStyle.Format = "### ### ###";
            dgAppelsFond.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdRechercherAppelsDeFond_Click(object sender, EventArgs e)
        {
            try
            {
                //AfficherFacturesClient();
                AfficherNiveauContratClient(leProjetId);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgAppelsFond_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                EffacerPanelAppelDeFond();
                dgHistoriqueAppelDeFonds.DataSource = null;
                if (dgAppelsFond.SelectedRows.Count > 0)
                {
                    int contratId = (int)dgAppelsFond.SelectedRows[0].Cells[0].Value;
                    leContratEnCours=contratRep.GetContratById(contratId);
                    var appelDeFonds = contratRep.GetDernierAppelsDeFonds(contratId);
                    if (appelDeFonds != null)
                    {
                        txtDateAppelDeFonds.Text = appelDeFonds.DateEcheanceFacture.Value.ToShortDateString();
                        txtClient.Text = appelDeFonds.Client.NomComplet;
                        txtLot.Text = appelDeFonds.Contrat.Lot.NumeroLot;
                        txtTypeVilla.Text = appelDeFonds.Contrat.Lot.TypeVilla.CodeType;
                        txtPrixDeVente.Text = appelDeFonds.Contrat.PrixFinal.ToString("### ### ##0");
                        txtTauxNiveauAppelDeFonds.Text = appelDeFonds.EtatAvancement.TypeEtatAvancement.TauxDecaissement.ToString("###") + "%";
                        txtNiveauAppelDeFond.Text = appelDeFonds.EtatAvancement.TypeEtatAvancement.Description;
                        txtMontantAppelDeFonds.Text = appelDeFonds.Montant.ToString("### ### ##0");
                        txtMontantEncaisséAppelDeFons.Text = appelDeFonds.Encaissements.Sum(enc => enc.Montant).ToString("### ### ##0");
                        txtMontantREstantAppelDeFonds.Text = (appelDeFonds.Montant - appelDeFonds.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ##0");
                        chkCourrierEnvoye.Checked = appelDeFonds.CourrierEnvoye;
                        chkFactureGeneree.Checked = appelDeFonds.FactureGenere;


                        dgHistoriqueAppelDeFonds.DataSource = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true).ToList()
                            .Select(fact => new
                            {
                                id = fact.Id,
                                Libellé = fact.Motif,
                                Taux = fact.EtatAvancement.TypeEtatAvancement.TauxDecaissement.ToString("###")+"%",
                                Montant = fact.Montant.ToString("### ### ##0"),
                                Encaissé=fact.Encaissements.Sum(enc => enc.Montant).ToString("### ### ##0"),
                                Restant= (fact.Montant- fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ##0")
                            }).ToList();

                        dgHistoriqueAppelDeFonds.Columns[0].Width = 0;
                        dgHistoriqueAppelDeFonds.Columns[0].Visible = false;
                        dgHistoriqueAppelDeFonds.Columns[1].Width = 150;

                        dgHistoriqueAppelDeFonds.Columns[2].Width = 50;
                        dgHistoriqueAppelDeFonds.Columns[2].DefaultCellStyle.Format = "###";
                        dgHistoriqueAppelDeFonds.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                        dgHistoriqueAppelDeFonds.Columns[3].Width = 80;
                        dgHistoriqueAppelDeFonds.Columns[3].DefaultCellStyle.Format = "### ### ##0";
                        dgHistoriqueAppelDeFonds.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                       
                        dgHistoriqueAppelDeFonds.Columns[4].Width = 80;
                        dgHistoriqueAppelDeFonds.Columns[4].DefaultCellStyle.Format = "### ### ##0";
                        dgHistoriqueAppelDeFonds.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dgHistoriqueAppelDeFonds.Columns[5].Width = 80;
                        dgHistoriqueAppelDeFonds.Columns[5].DefaultCellStyle.Format = "### ### ##0";
                        dgHistoriqueAppelDeFonds.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    else
                    {
                        EffacerPanelAppelDeFond();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis -  Gestion du recouvrement", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EffacerPanelAppelDeFond()
        {
            txtDateAppelDeFonds.Text = string.Empty;
            txtClient.Text = string.Empty;
            txtLot.Text = string.Empty;
            txtTypeVilla.Text = string.Empty;
            txtTauxNiveauAppelDeFonds.Text = string.Empty;
            txtNiveauAppelDeFond.Text = string.Empty;
            txtMontantAppelDeFonds.Text = string.Empty;
            txtMontantEncaisséAppelDeFons.Text = string.Empty;
            txtMontantREstantAppelDeFonds.Text = string.Empty;
            txtPrixDeVente.Text = string.Empty;
            chkFactureGeneree.Checked =false;

        }

        private void cmdImprimerFactureRelance_Click(object sender, EventArgs e)
        {
            try
            {
                GenereLettreRelanceResa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                       "Prosopis -  Gestion du recouvrement", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenereLettreRelanceResa()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "LettreDeRelanceResa1.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                msWord.Activate();
                doc.Activate();
                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;

                //myBookmark = bookmarks["DateFactureRelance"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text =DateTime.Now.Date.ToShortDateString();
                string titre = string.Empty;
                if (leContratEnCours.Client.Genre == Genre.Masculin)
                    titre = "Monsieur";
                else
                    titre = "Madame";

                myBookmark = bookmarks["Titre1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = titre;

                myBookmark = bookmarks["Titre2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = titre;

                myBookmark = bookmarks["Titre3"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = titre;

                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.NomComplet;

                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.Adresse;

                myBookmark = bookmarks["Ville"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.Pays;

                myBookmark = bookmarks["DateFacture"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Date.ToShortDateString();

                myBookmark = bookmarks["NumeroLot"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType;

                myBookmark = bookmarks["Commercial"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Commercial.NomComplet;

                myBookmark = bookmarks["NumeroLot2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                myBookmark = bookmarks["MontantTotalEncaisse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ###");

                myBookmark = bookmarks["MontantTotalImpaye"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = (leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true).Sum(fact => fact.Montant) -
                                     leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ###");

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                myBookmark = bookmarks["MontantTotalImpaye2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = (leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true).Sum(fact => fact.Montant) -
                                     leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ###");


                //    adoc.Activate();

                Microsoft.Office.Interop.Word.Tables tables = doc.Tables;
                if (tables.Count > 0)
                {
                    //Get the first table in the document
                    Microsoft.Office.Interop.Word.Table table = tables[1];
                    var lesAppelsDeFonds = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true);
                    foreach (var appelDeFond in lesAppelsDeFonds)
                    {
                        Microsoft.Office.Interop.Word.Row row = table.Rows.Add(ref missing);

                        row.Cells[1].Range.Text = appelDeFond.Motif;
                        row.Cells[1].WordWrap = true;
                        row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[1].Range.Bold = 0;

                        row.Cells[2].Range.Text = appelDeFond.Montant!=0?appelDeFond.Montant.ToString("### ### ###"):"0";
                        row.Cells[2].WordWrap = true;
                        row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[2].Range.Bold = 0;
                      

                        row.Cells[3].Range.Text = appelDeFond.Encaissements.Sum(enc => enc.Montant)!=0? appelDeFond.Encaissements.Sum(enc => enc.Montant).ToString("### ### ###"):"0";
                        row.Cells[3].WordWrap = true;
                        row.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[3].Range.Bold = 0;

                        row.Cells[4].Range.Text = (appelDeFond.Montant - appelDeFond.Encaissements.Sum(enc => enc.Montant)) != 0 ? (appelDeFond.Montant - appelDeFond.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###"):"0" ;
                        row.Cells[4].WordWrap = true;
                        row.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[4].Range.Bold = 0;
                    }

                    Microsoft.Office.Interop.Word.Row rowTotal = table.Rows.Add(ref missing);

                    rowTotal.Cells[1].Range.Text ="Total";
                    rowTotal.Cells[1].WordWrap = true;
                    rowTotal.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[1].Range.Bold = 1;

                    rowTotal.Cells[2].Range.Text = lesAppelsDeFonds.Sum(fact => fact.Montant).ToString("### ### ###") ;
                    rowTotal.Cells[2].WordWrap = true;
                    rowTotal.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[2].Range.Bold = 1;


                    rowTotal.Cells[3].Range.Text = lesAppelsDeFonds.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###") ;
                    rowTotal.Cells[3].WordWrap = true;
                    rowTotal.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[3].Range.Bold = 1;

                    rowTotal.Cells[4].Range.Text =( lesAppelsDeFonds.Sum(fact => fact.Montant) - lesAppelsDeFonds.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
                    rowTotal.Cells[4].WordWrap = true;
                    rowTotal.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[4].Range.Bold = 1;
                }

                myBookmark = bookmarks["DateEcheance"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Date.AddDays(35).Date.ToShortDateString();
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GenereFactureAppelsDeFond(Object sender)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                if(sender== cmdCourrierFacture)
                     msWord.Visible = false; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                else
                    msWord.Visible = true;

                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template

                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "FactureAppelDeFonds.dotx";

                var appelDeFonds = contratRep.GetDernierAppelsDeFonds(leContratEnCours.Id);
                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                msWord.Activate();
                doc.Activate();
                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;

                //myBookmark = bookmarks["DateFactureRelance"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text =DateTime.Now.Date.ToShortDateString();
                string titre = string.Empty;
                if (leContratEnCours.Client.Genre == Genre.Masculin)
                    titre = "Monsieur";
                else
                    titre = "Madame";

                myBookmark = bookmarks["Titre1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = titre;

                myBookmark = bookmarks["Titre2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = titre;

                myBookmark = bookmarks["Titre3"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = titre;

                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.NomComplet;

                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.Adresse;

                myBookmark = bookmarks["Ville"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.Pays;

                myBookmark = bookmarks["DateFacture"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Date.ToShortDateString();

                myBookmark = bookmarks["NiveauAppelDeFond1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = appelDeFonds.EtatAvancement.TypeEtatAvancement.LibelleCommercial;

                myBookmark = bookmarks["NiveauAppelDeFond2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = appelDeFonds.EtatAvancement.TypeEtatAvancement.LibelleCommercial;

                myBookmark = bookmarks["NomTypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType;

                myBookmark = bookmarks["CodeTypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType;


                myBookmark = bookmarks["Commercial"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Commercial.NomComplet;

                //myBookmark = bookmarks["NumeroLot2"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                //myBookmark = bookmarks["MontantTotalEncaisse"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ###");

                //myBookmark = bookmarks["MontantTotalImpaye"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = (leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true).Sum(fact => fact.Montant) -
                //                     leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ###");

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                //myBookmark = bookmarks["MontantTotalImpaye2"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = (leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true).Sum(fact => fact.Montant) -
                //                     leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ###");


                //    adoc.Activate();
              
                Microsoft.Office.Interop.Word.Tables tables = doc.Tables;
                if (tables.Count > 0)
                {
                    //Get the first table in the document
                    Microsoft.Office.Interop.Word.Table table = tables[1];
                  
                    Microsoft.Office.Interop.Word.Row rowTotal = table.Rows.Add(ref missing);

                    rowTotal.Cells[1].Range.Text = "Montant de l’appel de fonds \""+appelDeFonds.EtatAvancement.TypeEtatAvancement.LibelleCommercial+"\" exigible le "+ DateTime.Now.Date.ToShortDateString();
                    rowTotal.Cells[1].WordWrap = true;
                    rowTotal.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[1].Range.Bold = 1;

                    rowTotal.Cells[2].Range.Text = appelDeFonds.Montant.ToString("### ### ##0");
                    rowTotal.Cells[2].WordWrap = true;
                    rowTotal.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[2].Range.Bold = 1;


                    rowTotal.Cells[3].Range.Text = appelDeFonds.Encaissements.Sum(enc => enc.Montant).ToString("### ### ##0");
                    rowTotal.Cells[3].WordWrap = true;
                    rowTotal.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[3].Range.Bold = 1;

                    rowTotal.Cells[4].Range.Text = (appelDeFonds.Montant - appelDeFonds.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ##0");
                    rowTotal.Cells[4].WordWrap = true;
                    rowTotal.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[4].Range.Bold = 1;
                }


                myBookmark = bookmarks["MontantAppelDeFond"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = (appelDeFonds.Montant - appelDeFonds.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ##0");

                //if (sender == cmdCourrierFacture)
                //{
                //    // Attribuer le nom
                //    object fileName = @"D:\" + appelDeFonds.EtatAvancement.TypeEtatAvancement.LibelleCommercial + " - "
                //                      + leContratEnCours.Client.NomComplet + ".docx";
                //    // Sauver le document
                //    doc.SaveAs(ref fileName, ref missing, ref missing, ref missing, ref missing,
                //                ref missing, ref missing, ref missing, ref missing, ref missing,
                //                ref missing, ref missing, ref missing, ref missing, ref missing,
                //                ref missing);
                //    //Fermer le document
                //    doc.Close(ref missing, ref missing, ref missing);


                //    // Fermeture de word
                //    msWord.Quit(ref missing, ref missing, ref missing);

                //    email_send(leContratEnCours.Client.Email, fileName.ToString(), "Facture appel de fond " + appelDeFonds.EtatAvancement.TypeEtatAvancement.LibelleCommercial,
                //        "Bonne reception");

                //    contratRep.MAJFactureCourrierEnvoye(appelDeFonds.Id);
                //    MessageBox.Show("Courrier envoyé avec succes...");

                //}
                //else if (sender == cmdFacture)
                //{
                //    contratRep.MAJFactureGeneree(appelDeFonds.Id);
                //}

                // doc.ExportAsFixedFormat(@"D:\Med\Projet Immociel\documents\Donnees inputs\test2.pdf", WdExportFormat.wdExportFormatPDF);
                //doc = appWord.Documents.Open(FileNameTextBox.Text);
                //System.Diagnostics.Process.Start(templateName.ToString());

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

        private void cmdFacture_Click(object sender, EventArgs e)
        {
            try
            { 
                GenereFactureAppelsDeFond(sender);
                //FactureGenere=true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                       "Prosopis -  Gestion du recouvrement", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdCourrierFacture_Click(object sender, EventArgs e)
        {
            try
            {
                GenereFactureAppelsDeFond(sender);
                //CourrierEnvoye=true;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis -  Gestion du recouvrement", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void email_send(string destinataire,string FichierPath, string sujet, string body)
        {
            //string dossierTemplates = Tools.Tools.DossierTemplates;
            //string templateName = dossierTemplates + "FactureAppelDeFonds.dotx";

            var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("kmedtech@gmail.com", "JHzZ1LR2");

            try
            {
             
                // Create instance of message
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                // Add receiver
                message.To.Add(destinataire);

                // Set sender
                // In this case the same as the username
                message.From = new System.Net.Mail.MailAddress("kmedtech@gmail.com");

                // Set subject
                message.Subject = sujet;

                // Set body of message
                message.Body = body;

                message.Attachments.Add(new Attachment(FichierPath));

                // Send the message
                client.Send(message);

                // Clean up
                message = null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkFactureGeneree_CheckedChanged(object sender, EventArgs e)
        {
            var appelDeFonds = contratRep.GetDernierAppelsDeFonds(leContratEnCours.Id);
            if(appelDeFonds!=null)
            contratRep.MAJFactureGeneree(appelDeFonds.Id);
        }

        private void cmbIlots_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FrmRecouvrementResa_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgAppelsFond_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lvRecouvrementResa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (lvRecouvrementResa.SelectedItems.Count > 0)
            //    {
            //        int contratId = (int)lvRecouvrementResa.SelectedItems[0].Tag;
            //        leContratEnCours = contratRep.GetContratById(contratId);

            //        var factures = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true).ToList()
            //               .Select(fact => new
            //               {
            //                   id = fact.Id,
            //                   Libellé = fact.Motif,
            //                   Taux = fact.EtatAvancement.TypeEtatAvancement.TauxDecaissement.ToString("###") + "%",
            //                   Montant = fact.Montant,
            //                   Encaissé = fact.Encaissements.Sum(enc => enc.Montant),
            //                   Restant = (fact.Montant - fact.Encaissements.Sum(enc => enc.Montant))
            //               }).ToList();
            //        lvAppelsDeFonds.Items.Clear();
            //        foreach (var facture in factures)
            //        {
            //            ListViewItem lviContrat = new ListViewItem(facture.Libellé);
            //            lviContrat.SubItems.Add(facture.Taux);
            //            lviContrat.SubItems.Add(facture.Montant.ToString("### ### ##0"));
            //            lviContrat.SubItems.Add(facture.Encaissé.ToString("### ### ##0"));
            //            lviContrat.SubItems.Add(facture.Restant.ToString("### ### ##0"));
            //            lvAppelsDeFonds.Items.Add(lviContrat);
            //        }

            //        txtTotalFactureClient.Text = factures.Sum(fact => fact.Montant).ToString("### ### ### ##0");
            //        txtTotalEncaisseClient.Text = factures.Sum(fact => fact.Encaissé).ToString("### ### ### ##0");
            //        txtTotalRestantClient.Text = factures.Sum(fact => fact.Restant).ToString("### ### ### ##0");
            //        //    dgHistoriqueAppelDeFonds.DataSource = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true).ToList()
            //        //       .Select(fact => new
            //        //       {
            //        //           id = fact.Id,
            //        //           Libellé = fact.Motif,
            //        //           Taux = fact.EtatAvancement.TypeEtatAvancement.TauxDecaissement.ToString("###") + "%",
            //        //           Montant = fact.Montant.ToString("### ### ##0"),
            //        //           Encaissé = fact.Encaissements.Sum(enc => enc.Montant).ToString("### ### ##0"),
            //        //           Restant = (fact.Montant - fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ##0")
            //        //       }).ToList();

            //        //dgHistoriqueAppelDeFonds.Columns[0].Width = 0;
            //        //dgHistoriqueAppelDeFonds.Columns[0].Visible = false;
            //        //dgHistoriqueAppelDeFonds.Columns[1].Width = 150;

            //        //dgHistoriqueAppelDeFonds.Columns[2].Width = 50;
            //        //dgHistoriqueAppelDeFonds.Columns[2].DefaultCellStyle.Format = "###";
            //        //dgHistoriqueAppelDeFonds.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            //        //dgHistoriqueAppelDeFonds.Columns[3].Width = 80;
            //        //dgHistoriqueAppelDeFonds.Columns[3].DefaultCellStyle.Format = "### ### ##0";
            //        //dgHistoriqueAppelDeFonds.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            //        //dgHistoriqueAppelDeFonds.Columns[4].Width = 80;
            //        //dgHistoriqueAppelDeFonds.Columns[4].DefaultCellStyle.Format = "### ### ##0";
            //        //dgHistoriqueAppelDeFonds.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //        //dgHistoriqueAppelDeFonds.Columns[5].Width = 80;
            //        //dgHistoriqueAppelDeFonds.Columns[5].DefaultCellStyle.Format = "### ### ##0";
            //        //dgHistoriqueAppelDeFonds.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //        var appelDeFonds = contratRep.GetDernierAppelsDeFonds(contratId);
            //        if (appelDeFonds != null)
            //        {
            //            txtDateAppelDeFonds.Text = appelDeFonds.DateEcheanceFacture.Value.ToShortDateString();
            //            txtClient.Text = appelDeFonds.Client.NomComplet;
            //            txtLot.Text = appelDeFonds.Contrat.Lot.NumeroLot;
            //            txtTypeVilla.Text = appelDeFonds.Contrat.Lot.TypeVilla.CodeType;
            //            txtPrixDeVente.Text = appelDeFonds.Contrat.PrixFinal.ToString("### ### ##0");
            //            txtTauxNiveauAppelDeFonds.Text = appelDeFonds.EtatAvancement.TypeEtatAvancement.TauxDecaissement.ToString("###") + "%";
            //            txtNiveauAppelDeFond.Text = appelDeFonds.EtatAvancement.TypeEtatAvancement.Description;
            //            txtMontantAppelDeFonds.Text = appelDeFonds.Montant.ToString("### ### ##0");
            //            txtMontantEncaisséAppelDeFons.Text = appelDeFonds.Encaissements.Sum(enc => enc.Montant).ToString("### ### ##0");
            //            txtMontantREstantAppelDeFonds.Text = (appelDeFonds.Montant - appelDeFonds.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ##0");
            //            chkCourrierEnvoye.Checked = appelDeFonds.CourrierEnvoye;
            //            chkFactureGeneree.Checked = appelDeFonds.FactureGenere;


                       
            //        }
            //        else
            //        {
            //            EffacerPanelAppelDeFond();
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, "Erreur:..." + ex.Message,
            //           "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void txtTotalCumulé_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTotalEncaissé_TextChanged(object sender, EventArgs e)
        {

        }

        private void lvRecouvrementResa_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lvRecouvrementResa.SelectedItems.Count > 0)
                {
                    int contratId = (int)lvRecouvrementResa.SelectedItems[0].Tag;
                    this.Cursor = Cursors.AppStarting;
                    FrmDetailsRecouvrementResa childForm = new FrmDetailsRecouvrementResa(contratId);
                    //childForm.MdiParent = this;
                    childForm.StartPosition = FormStartPosition.CenterParent;
                    childForm.ShowDialog();
                    //childForm.WindowState = FormWindowState.Normal;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion du recouvrement résa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dossierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvRecouvrementResa.SelectedItems.Count > 0)
                {
                    int contratId = (int)lvRecouvrementResa.SelectedItems[0].Tag;
                    leContratEnCours = contratRep.GetContratById(contratId);
                    FrmDossierClient childForm = new FrmDossierClient(leContratEnCours);
                    //childForm.MdiParent = this.MdiParent;
                    childForm.StartPosition =  FormStartPosition.CenterScreen;
                    childForm.ShowDialog();
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion du recouvrement résa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void encaissementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvRecouvrementResa.SelectedItems.Count > 0)
                {
                    int contratId = (int)lvRecouvrementResa.SelectedItems[0].Tag;
                    FrmEncaissement childForm = new FrmEncaissement(contratId);
                    //childForm.MdiParent = this.MdiParent;
                    childForm.StartPosition = FormStartPosition.CenterScreen;
                    childForm.ShowDialog();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion du recouvrement résa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdExporter_Click(object sender, EventArgs e)
        {
            ExporterListeRecouvrementResa();
        }

        private void ExporterListeRecouvrementResa()
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
                xlApp.Visible = false;
                //xlWorkBook = xlApp.Workbooks.Add(misValue);
                string dossierTemplates = Tools.Tools.DossierTemplates;

                xlWorkBook = xlApp.Workbooks.Open(dossierTemplates + "RecouvrementResa.xltx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                int numOrdre = 0;
                int iDepart = 8;// à partir ligne 15
                DateTime dateReference = DateTime.Parse("01/01/1900");

                foreach (var contrat in contratsResa)
                {
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = numOrdre + 1;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 2] = contrat.Client.NomComplet;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    xlWorkSheet.Cells[numOrdre + iDepart, 3] = contrat.DateSouscription.Value.ToShortDateString();
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 4] = contrat.NumeroContrat;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                    xlWorkSheet.Cells[numOrdre + iDepart, 5] = contrat.Lot.NumeroLot;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                    xlWorkSheet.Cells[numOrdre + iDepart, 6] = contrat.Lot.TypeVilla.NomComplet;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 7] = contrat.PrixFinal.ToString("### ### ###");
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 8] = contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Max(f => f.EtatAvancement.DateSaisieAvancement.HasValue ? f.EtatAvancement.DateSaisieAvancement.Value : dateReference).ToShortDateString();
                    xlWorkSheet.Cells[numOrdre + iDepart, 8].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 8].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 9] = contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.EtatAvancement.TypeEtatAvancement.TauxDecaissement).ToString("###") + "%";
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 10] = contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant).ToString("### ### ##0");
                    xlWorkSheet.Cells[numOrdre + iDepart, 10].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 10].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 11] = contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ##0");
                    xlWorkSheet.Cells[numOrdre + iDepart, 11].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 11].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 12] = (contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant) - contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ##0");
                    xlWorkSheet.Cells[numOrdre + iDepart, 12].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 12].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    //lviContrat.SubItems.Add(contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Max(f => f.EtatAvancement.DateSaisieAvancement.HasValue ? f.EtatAvancement.DateSaisieAvancement.Value : dateReference).ToShortDateString());
                    //lviContrat.SubItems.Add(contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.EtatAvancement.TypeEtatAvancement.TauxDecaissement).ToString("###") + "%");
                    //lviContrat.SubItems.Add(contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant).ToString("### ### ##0"));
                    //lviContrat.SubItems.Add(contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ##0"));
                    ////lviContrat.SubItems.Add((contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant) - contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ###"));
                    //lviContrat.SubItems.Add((contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Montant) - contrat.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ##0"));

                    numOrdre++;
                }

                xlApp.Visible = true;

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
    }
}
