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
    public partial class FrmRecouvrementDepot : Form
    {
        ContratRepository contratRep;
        ClientRepository clientRep;

        private Contrat leContratEnCours;
        private int leProjetId;
        private IEnumerable<Contrat> contratsDepot;

        public FrmRecouvrementDepot(int projetId)
        {
            InitializeComponent();
            contratRep = new ContratRepository();
            clientRep = new ClientRepository();
            leProjetId = projetId;
            cmbTypeEcheancuer.DataSource = Enum.GetValues(typeof(TypeEcheancier));
            cmbTypeEcheancuer.SelectedIndex = -1;
            //dtpDateFinPeriode.Value = DateTime.Today.AddDays(0 - DateTime.Today.Day).Date;
            //dtpDateDebutPeriode.Value = dtpDateFinPeriode.Value.AddDays(1 - dtpDateFinPeriode.Value.Day).Date;

            var lesCLients = clientRep.GetAllClients(false).ToList().Where(cli =>cli.ProjetId== leProjetId && cli.Contrats.Where(cont => cont.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && cont.Statut== StatutContrat.Actif ).Count() > 0).ToList();
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
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmdRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                AfficherFacturesClient(leProjetId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis -  Gestion du recouvrement", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherFacturesClient(int projetId)
        {
            try
            {
                contratsDepot = contratRep.GetContratsDepot().Where(c =>c.ProjetId==projetId && c.Statut == StatutContrat.Actif);
                TypeEcheancier typeEcheancier = 0;
                if(cmbTypeEcheancuer.SelectedItem!=null)
                {
                    typeEcheancier = (TypeEcheancier)cmbTypeEcheancuer.SelectedItem;
                    contratsDepot = contratsDepot.Where(c => c.TypeEcheancier == typeEcheancier);
                }
               
                if(cmbClients.SelectedItem!=null)
                {
                    Client client = cmbClients.SelectedItem as Client;
                    contratsDepot = contratsDepot.Where(cont => cont.ClientID == client.ID).ToList();
                }

                ////Filtrer selon l'etat du solde de la facture
                if (rbImpayes.Checked)
                    contratsDepot = contratsDepot.Where(cont => cont.Factures.Where(fact => fact.TypeFacture== TypeFacture.Echeance && fact.Echue==true && fact.FacturePayee==false).Count() >0).ToList();
                if (rbSoldes.Checked)
                    contratsDepot = contratsDepot.Where(cont => cont.Factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance && fact.Echue == true && fact.FacturePayee == false).Count() <= 0).ToList();

                if (txtDuree.Text != string.Empty)
                {
                    var nbEcheances = Int16.Parse(txtDuree.Text);
                    //DateTime dateReference = DateTime.Now.;

                    contratsDepot = contratsDepot.Where(c => c.Factures.Where(f => f.TypeFacture == TypeFacture.Echeance && f.Echue == true && f.FacturePayee == false).Count() >= nbEcheances);
                }
                //dgFacturesDepots.DataSource = contratsDepot.ToList()
                //    .Select(cont => new
                //    {
                //        //ID = fact.Id,
                //        ////Numéro=fact.NumeroFacture,
                //        //Date = fact.DateEcheanceFacture != null ? fact.DateEcheanceFacture.Value.ToShortDateString() : "",
                //        //Client = fact.Client.NomComplet,
                //        //Contrat = fact.Contrat.NumeroContrat,
                //        //Numéro = fact.NumeroFacture,
                //        //Motif = fact.Motif,
                //        //Taux = fact.Contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation ? fact.EtatAvancement.TypeEtatAvancement.TauxDecaissement.ToString("###") + "%" : "",
                //        //Montant = fact.Montant,
                //        //Encaissé = fact.MontantEncaisse,
                //        //Restant = fact.Montant - fact.MontantEncaisse,
                //        //Soldée = fact.FacturePayee
                //        Id = cont.Id,
                //        //Lot = cont.Lot.NumeroLot,
                //        Type = cont.Lot.TypeVilla.CodeType,
                //        Client = cont.Client.NomComplet,
                //        PrixVente = cont.PrixFinal.ToString("### ### ###"),
                //        NombreEcheances = cont.NbEcheances,
                //        MontantEcheance = cont.MontantEcheance,
                //        NBEcheanceEchues = cont.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance)).Count(),
                //        NBEcheanceSoldees = cont.Factures.Where(f => f.TypeFacture == TypeFacture.Echeance && f.FacturePayee == true).Count(),
                //        NBEcheanceEchuesNonSoldees = cont.Factures.Where(f => f.TypeFacture == TypeFacture.Echeance && f.Echue == true && f.FacturePayee == false).Count(),
                //        // SeuilCumule = cont.Factures.Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).Sum(f => f.EtatAvancement.TypeEtatAvancement.TauxDecaissement).ToString("###") + "%",

                //        Echu = cont.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance || f.TypeFacture == TypeFacture.DepotMinimum)).Sum(f => f.Montant).ToString("### ### ###"),
                //        Encaissé = cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ###"),
                //        Restant= (cont.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance || f.TypeFacture == TypeFacture.DepotMinimum)).Sum(f => f.Montant) -
                //        cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ###")
                //    }).ToList();

                //lvContratsDepot.Columns[1].
                lvContratsDepot.Items.Clear();
                foreach (var contrat in contratsDepot.ToList())
                {
                    int nbImpaye = contrat.Factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance && fact.Echue == true
                        && fact.FacturePayee == false).Count();

                    ListViewItem lviContrat = new ListViewItem(contrat.NumeroContrat);
                    lviContrat.SubItems.Add(contrat.Client.NomComplet);
                    lviContrat.SubItems.Add(contrat.DateSouscription.Value.ToShortDateString());
                    lviContrat.SubItems.Add(contrat.Lot.TypeVilla.CodeType);
                    //lviContrat.SubItems.Add(contrat.Lot.PositionLot.ToString());
                    lviContrat.SubItems.Add(contrat.PrixFinal.ToString("### ### ###"));
                   
                    //lviContrat.SubItems.Add(contrat.DureeDepot.ToString());
                    lviContrat.SubItems.Add(contrat.NbEcheances.Value.ToString());
                    lviContrat.SubItems.Add(contrat.TypeEcheancier.Value.ToString());
                    lviContrat.SubItems.Add(contrat.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance)).Count().ToString());
                    lviContrat.SubItems.Add(contrat.Factures.Where(f => f.TypeFacture == TypeFacture.Echeance && f.Echue == true && f.FacturePayee == true).Count().ToString());
                    lviContrat.SubItems.Add(contrat.Factures.Where(f => f.TypeFacture == TypeFacture.Echeance && f.Echue == true && f.FacturePayee == false).Count().ToString());

                    lviContrat.SubItems.Add(contrat.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance || f.TypeFacture == TypeFacture.DepotMinimum)).Sum(f => f.Montant).ToString("### ### ##0"));
                    lviContrat.SubItems.Add(contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ##0"));
                    lviContrat.SubItems.Add((contrat.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance || f.TypeFacture == TypeFacture.DepotMinimum)).Sum(f => f.Montant)
                                                                   - contrat.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance || f.TypeFacture == TypeFacture.DepotMinimum))
                                                                                          .Sum(f => f.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ##0"));
                    if (nbImpaye > 0)
                        lviContrat.ImageIndex = 0;
                    else
                        lviContrat.ImageIndex = 1;
                    lviContrat.Tag = contrat.Id;
                    lvContratsDepot.Items.Add(lviContrat);
                }
                txtNbNombreContrats.Text = contratsDepot.Count().ToString();
                txtChiffreAffaires.Text = contratsDepot.Sum(contrat => contrat.PrixFinal).ToString("### ### ### ##0");
                txtTotalCumulé.Text = contratsDepot.Sum(contrat => contrat.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance || f.TypeFacture == TypeFacture.DepotMinimum)).Sum(f => f.Montant)).ToString("### ### ### ##0");
                txtTotalEncaissé.Text= contratsDepot.Sum(contrat => contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ### ##0");
                txtTotalRestant.Text = contratsDepot.Sum(contrat => (contrat.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance || f.TypeFacture == TypeFacture.DepotMinimum)).Sum(f => f.Montant) 
                                                                   - contrat.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance || f.TypeFacture == TypeFacture.DepotMinimum)).Sum(f => f.Encaissements.Sum(enc => enc.Montant)))).ToString("### ### ### ##0");

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FormatterGrilleAppelsFond()
        {
            dgFacturesDepots.Columns[0].Width = 0;
            dgFacturesDepots.Columns[0].Visible = false;
            dgFacturesDepots.Columns[1].Width = 80;


            dgFacturesDepots.Columns[2].Width = 120;
            dgFacturesDepots.Columns[3].Width = 80;
            dgFacturesDepots.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgFacturesDepots.Columns[4].Width = 130;

            dgFacturesDepots.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgFacturesDepots.Columns[5].Width = 220;
            dgFacturesDepots.Columns[5].DefaultCellStyle.Format = "### ### ###";
            dgFacturesDepots.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgFacturesDepots.Columns[6].Width = 50;
            dgFacturesDepots.Columns[6].DefaultCellStyle.Format = "### ### ###";
            dgFacturesDepots.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgFacturesDepots.Columns[6].Visible = false;


            dgFacturesDepots.Columns[7].Width = 80;
            dgFacturesDepots.Columns[7].DefaultCellStyle.Format = "### ### ###";
            dgFacturesDepots.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgFacturesDepots.Columns[8].Width = 80;
            dgFacturesDepots.Columns[8].DefaultCellStyle.Format = "### ### ###";
            dgFacturesDepots.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgFacturesDepots.Columns[9].Width = 80;
            dgFacturesDepots.Columns[9].DefaultCellStyle.Format = "### ### ###";
            dgFacturesDepots.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgFacturesDepots.Columns[10].Width = 50;
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgFacturesDepots_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgFacturesDepots.SelectedRows.Count > 0)
                {
                    int contratId = (int)dgFacturesDepots.SelectedRows[0].Cells[0].Value;
                    leContratEnCours = contratRep.GetContratById(contratId);
                    var factures = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier).ToList();

                    if (factures != null)
                    {
                       
                        txtClient.Text = leContratEnCours.Client.NomComplet;
                        
                        txtTypeVilla.Text = leContratEnCours.Lot.TypeVilla.CodeType;
                        txtPrixDeVente.Text= leContratEnCours.PrixFinal.ToString("### ### ###");
                        txtPeriodicite.Text = leContratEnCours.TypeEcheancier.Value.ToString();
                        txtTypeContrat.Text = leContratEnCours.TypeContrat.LibelleTypeContrat;
                        txtDureeDepot.Text = leContratEnCours.DureeDepot.ToString();
                        txtNbEcheances.Text = leContratEnCours.NbEcheances.Value.ToString();
                        txtMontantEcheance.Text= leContratEnCours.MontantEcheance.Value.ToString("### ### ###");
                        var depotMinimum = factures.Where(fact => fact.TypeFacture == TypeFacture.DepotMinimum).FirstOrDefault();
                        txtDepotMinimum.Text = depotMinimum.Montant.ToString("### ### ###");

                       
                        txtMontantDepotMinimumEncaisse.Text = depotMinimum.Encaissements.Sum(enc => enc.Montant).ToString("### ### ###");

                      
                        txtMontantDepotMinimumRestant.Text = (depotMinimum.Montant - depotMinimum.Encaissements.Sum(enc => enc.Montant)).ToString();

                        txtNbEcheancesEchues.Text = factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Count().ToString();
                        txtMontantEcheancesDu.Text = factures.Where(fact => fact.Echue == true && fact.TypeFacture== TypeFacture.Echeance).Sum(fact => fact.Montant).ToString("### ### ###");

                        txtNbEcheancesSoldees.Text = factures.Where(fact => fact.FacturePayee==true && fact.TypeFacture == TypeFacture.Echeance).Count().ToString();
                        txtMontantEcheancesEncaisse.Text = factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###"); ;



                            //factures.Where(fact => fact.Echue == true && && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant).ToString("### ### ###");



                        txtNbEcheancesNonSoldees.Text = factures.Where(fact => fact.Echue == true && fact.FacturePayee == false && fact.TypeFacture == TypeFacture.Echeance).Count().ToString();
                        txtMontantEcheancesRestant.Text =( factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant) -
                            factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");


                        txtMontantTotalDu.Text = factures.Where(fact => fact.Echue == true).Sum(fact => fact.Montant).ToString("### ### ###");
                        txtMontantTotalEncaisse.Text = factures.Where(fact => fact.Echue == true).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###");
                        txtMontantTotalRestant.Text = (factures.Where(fact => fact.Echue == true).Sum(fact => fact.Montant) - factures.Where(fact => fact.Echue == true).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
                        var echeancesEchues = factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).ToList();
                        listView1.Items.Clear();
                        foreach (var echeance in echeancesEchues)
                        {
                            ListViewItem lviEchance = new ListViewItem(echeance.Motif);
                            lviEchance.SubItems.Add(echeance.DateEcheanceFacture.Value.ToShortDateString());
                            lviEchance.SubItems.Add(echeance.Montant.ToString("### ### ###"));
                            lviEchance.SubItems.Add(echeance.MontantEncaisse.ToString("### ### ###"));
                            lviEchance.SubItems.Add((echeance.Montant - echeance.MontantEncaisse).ToString("### ### ###"));
                            listView1.Items.Add(lviEchance);
                                
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis -  Gestion du recouvrement", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdImprimerFactureRelance_Click(object sender, EventArgs e)
        {
            try
            {
                GenererFactureRElanceDepot();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                       "Prosopis -  Gestion du recouvrement", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenererFactureRElanceDepot()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "FactureRelanceDepot.dotx";


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

                //myBookmark = bookmarks["Titre2"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = titre;

                //myBookmark = bookmarks["Titre3"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = titre;

                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.NomComplet;

                myBookmark = bookmarks["AdresseClient"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.Adresse +" - "+ leContratEnCours.Client.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.Pays;

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType;

                myBookmark = bookmarks["TypeVilla2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType;

                myBookmark = bookmarks["Commercial"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Commercial.NomComplet;

                var nbEchDues = leContratEnCours.Factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Count();
                myBookmark = bookmarks["NombreEcheances"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = nbEchDues.ToString();

                myBookmark = bookmarks["MontantEcheance"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.MontantEcheance.Value.ToString("### ### ###");

                myBookmark = bookmarks["MontantTotalEcheances"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = (leContratEnCours.MontantEcheance.Value * nbEchDues).ToString("### ### ###");

                //myBookmark = bookmarks["PrixDeVente"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                myBookmark = bookmarks["MontantTotalImpaye"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = (leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true).Sum(fact => fact.Montant) -
                                     leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ###");

                myBookmark = bookmarks["MontantTotalImpaye2"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = (leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true).Sum(fact => fact.Montant) -
                                     leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ###");


                myBookmark = bookmarks["MontantEncaisse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = (leContratEnCours.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal)).ToString("### ### ###");


                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                //    adoc.Activate();

                Microsoft.Office.Interop.Word.Tables tables = doc.Tables;
                if (tables.Count > 0)
                {
                    //Get the first table in the document
                    Microsoft.Office.Interop.Word.Table table = tables[1];
                    var lesAppelsDeFonds = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true);
                    //foreach (var appelDeFond in lesAppelsDeFonds)
                    //{

                    var depotMinimum = leContratEnCours.Factures.Where(fact => fact.TypeFacture == TypeFacture.DepotMinimum).FirstOrDefault();
                    //txtDepotMinimum.Text = depotMinimum.Montant.ToString("### ### ###");


                    //txtMontantDepotMinimumEncaisse.Text = depotMinimum.Encaissements.Sum(enc => enc.Montant).ToString("### ### ###");


                    //txtMontantDepotMinimumRestant.Text = (depotMinimum.Montant - depotMinimum.Encaissements.Sum(enc => enc.Montant)).ToString();



                    Microsoft.Office.Interop.Word.Row row = table.Rows.Add(ref missing);

                    row.Cells[1].Range.Text = "Dépôt minimum "+leContratEnCours.TypeContrat.SeuilSouscription+"%";
                    row.Cells[1].WordWrap = true;
                    row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[1].Range.Bold = 0;

                    row.Cells[2].Range.Text = depotMinimum.Montant.ToString("### ### ###"); ;
                    row.Cells[2].WordWrap = true;
                    row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[2].Range.Bold = 0;

                    row.Cells[3].Range.Text = depotMinimum.Encaissements.Sum(enc => enc.Montant).ToString("### ### ###");
                    row.Cells[3].WordWrap = true;
                    row.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[3].Range.Bold = 0;


                    row.Cells[4].Range.Text = (depotMinimum.Montant - depotMinimum.Encaissements.Sum(enc => enc.Montant)) == 0?"0":(depotMinimum.Montant - depotMinimum.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###");
                    row.Cells[4].WordWrap = true;
                    row.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[4].Range.Bold = 0;


                    row = table.Rows.Add(ref missing);

                    row.Cells[1].Range.Text = "Echéances";
                    row.Cells[1].WordWrap = true;
                    row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[1].Range.Bold = 0;

                    row.Cells[2].Range.Text = leContratEnCours.Factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant).ToString("### ### ###");
                    row.Cells[2].WordWrap = true;
                    row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[2].Range.Bold = 0;

                    row.Cells[3].Range.Text = leContratEnCours.Factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###"); ;
                    row.Cells[3].WordWrap = true;
                    row.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[3].Range.Bold = 0;


                    row.Cells[4].Range.Text = (leContratEnCours.Factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant) -
                                                leContratEnCours.Factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
                    row.Cells[4].WordWrap = true;
                    row.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[4].Range.Bold = 0;

                    row = table.Rows.Add(ref missing);

                    row.Cells[1].Range.Text = "TOTAL";
                    row.Cells[1].WordWrap = true;
                    row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[1].Range.Bold = 0;
                  
                    row.Cells[2].Range.Text = leContratEnCours.Factures.Where(fact => fact.Echue == true && fact.TypeFacture!= TypeFacture.FraisDossier).Sum(fact => fact.Montant).ToString("### ### ###");
                    row.Cells[2].WordWrap = true;
                    row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[2].Range.Bold = 0;

                    row.Cells[3].Range.Text = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###"); ;
                    row.Cells[3].WordWrap = true;
                    row.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[3].Range.Bold = 0;


                    row.Cells[4].Range.Text = (leContratEnCours.Factures.Where(fact => fact.Echue == true && fact.TypeFacture != TypeFacture.FraisDossier).Sum(fact => fact.Montant) -
                                                leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
                    row.Cells[4].WordWrap = true;
                    row.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row.Cells[4].Range.Bold = 0;


                    //Microsoft.Office.Interop.Word.Row rowTotal = table.Rows.Add(ref missing);

                    //rowTotal.Cells[1].Range.Text = "Total";
                    //rowTotal.Cells[1].WordWrap = true;
                    //rowTotal.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    //rowTotal.Cells[1].Range.Bold = 1;

                    //rowTotal.Cells[2].Range.Text = lesAppelsDeFonds.Sum(fact => fact.Montant).ToString("### ### ###");
                    //rowTotal.Cells[2].WordWrap = true;
                    //rowTotal.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    //rowTotal.Cells[2].Range.Bold = 1;


                    //rowTotal.Cells[3].Range.Text = lesAppelsDeFonds.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###");
                    //rowTotal.Cells[3].WordWrap = true;
                    //rowTotal.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    //rowTotal.Cells[3].Range.Bold = 1;

                    //rowTotal.Cells[4].Range.Text = (lesAppelsDeFonds.Sum(fact => fact.Montant) - lesAppelsDeFonds.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
                    //rowTotal.Cells[4].WordWrap = true;
                    //rowTotal.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    //rowTotal.Cells[4].Range.Bold = 1;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void rbSoldes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgEcheances_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdRechercher_Click(sender, e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbImpayes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbImpayes.Checked)
            {
                txtDuree.Text = string.Empty;
                pRetard.Visible = true;
            }
            else
            {
                txtDuree.Text = string.Empty;
                pRetard.Visible = false;
            }

        }

        private void lvContratsDepot_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    try
        //    {
        //        if (lvContratsDepot.SelectedItems.Count > 0)
        //        {
        //            var contrat = lvContratsDepot.SelectedItems[0].Tag as Contrat;
        //            leContratEnCours = contratRep.GetContratById(contrat.Id);
        //            var factures = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier).ToList();

        //            if (factures != null)
        //            {
        //                txtClient.Text = leContratEnCours.Client.NomComplet;
        //                txtTypeVilla.Text = leContratEnCours.Lot.TypeVilla.CodeType;
        //                txtPrixDeVente.Text = leContratEnCours.PrixFinal.ToString("### ### ###");
        //                txtPeriodicite.Text = leContratEnCours.TypeEcheancier.Value.ToString();
        //                txtTypeContrat.Text = leContratEnCours.TypeContrat.LibelleTypeContrat;
        //                txtDureeDepot.Text = leContratEnCours.DureeDepot.ToString();
        //                txtNbEcheances.Text = leContratEnCours.NbEcheances.Value.ToString();
        //                txtMontantEcheance.Text = leContratEnCours.MontantEcheance.Value.ToString("### ### ###");
        //                var depotMinimum = factures.Where(fact => fact.TypeFacture == TypeFacture.DepotMinimum).FirstOrDefault();
        //                txtDepotMinimum.Text = depotMinimum.Montant.ToString("### ### ###");

        //                txtMontantDepotMinimumEncaisse.Text = depotMinimum.Encaissements.Sum(enc => enc.Montant).ToString("### ### ###");

        //                txtMontantDepotMinimumRestant.Text = (depotMinimum.Montant - depotMinimum.Encaissements.Sum(enc => enc.Montant)).ToString();

        //                txtNbEcheancesEchues.Text = factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Count().ToString();
        //                txtMontantEcheancesDu.Text = factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant).ToString("### ### ###");

        //                txtNbEcheancesSoldees.Text = factures.Where(fact => fact.FacturePayee == true && fact.TypeFacture == TypeFacture.Echeance).Count().ToString();
        //                txtMontantEcheancesEncaisse.Text = factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###"); ;
        //                //factures.Where(fact => fact.Echue == true && && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant).ToString("### ### ###");

        //                txtNbEcheancesNonSoldees.Text = factures.Where(fact => fact.Echue == true && fact.FacturePayee == false && fact.TypeFacture == TypeFacture.Echeance).Count().ToString();
        //                txtMontantEcheancesRestant.Text = (factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant) -
        //                    factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");

        //                txtMontantTotalDu.Text = factures.Where(fact => fact.Echue == true).Sum(fact => fact.Montant).ToString("### ### ###");
        //                txtMontantTotalEncaisse.Text = factures.Where(fact => fact.Echue == true).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###");
        //                txtMontantTotalRestant.Text = (factures.Where(fact => fact.Echue == true).Sum(fact => fact.Montant) - factures.Where(fact => fact.Echue == true).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
        //                var echeancesEchues = factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).ToList();
        //                listView1.Items.Clear();
        //                foreach (var echeance in echeancesEchues)
        //                {
        //                    ListViewItem lviEchance = new ListViewItem(echeance.Motif);
        //                    lviEchance.SubItems.Add(echeance.DateEcheanceFacture.Value.ToShortDateString());
        //                    lviEchance.SubItems.Add(echeance.Montant.ToString("### ### ###"));
        //                    lviEchance.SubItems.Add(echeance.MontantEncaisse.ToString("### ### ###"));
        //                    lviEchance.SubItems.Add((echeance.Montant - echeance.MontantEncaisse).ToString("### ### ###"));
        //                    listView1.Items.Add(lviEchance);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, "Erreur:" + ex.Message,
        //                "Prosopis -  Gestion du recouvrement", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        }

        private void lvContratsDepot_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lvContratsDepot.SelectedItems.Count > 0)
                {
                    int contratId = (int)lvContratsDepot.SelectedItems[0].Tag;
                    this.Cursor = Cursors.AppStarting;
                    FrmDetailsRecouvrementDepot childForm = new FrmDetailsRecouvrementDepot(contratId);
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
                if (lvContratsDepot.SelectedItems.Count > 0)
                {
                    int contratId = (int)lvContratsDepot.SelectedItems[0].Tag;
                    leContratEnCours = contratRep.GetContratById(contratId);
                    FrmDossierClient childForm = new FrmDossierClient(leContratEnCours);
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

        private void encaissementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvContratsDepot.SelectedItems.Count > 0)
                {
                    int contratId = (int)lvContratsDepot.SelectedItems[0].Tag;
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
            ExporterListeRecouvrementDepot();
        }

        private void ExporterListeRecouvrementDepot()
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

                xlWorkBook = xlApp.Workbooks.Open(dossierTemplates + "RecouvrementDepot.xltx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                int numOrdre = 0;
                int iDepart = 8;// à partir ligne 15
                DateTime dateReference = DateTime.Parse("01/01/1900");

                foreach (var contrat in contratsDepot)
                {
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = contrat.NumeroContrat;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 2] = contrat.Client.NomComplet;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    xlWorkSheet.Cells[numOrdre + iDepart, 3] = contrat.DateSouscription.Value.ToShortDateString();
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 4] = contrat.Lot.TypeVilla.CodeType;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 5] = contrat.PrixFinal;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    
                    xlWorkSheet.Cells[numOrdre + iDepart, 6] = contrat.NbEcheances.Value.ToString();
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 7] = contrat.TypeEcheancier.Value.ToString();
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 8] = contrat.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance)).Count().ToString();
                    xlWorkSheet.Cells[numOrdre + iDepart, 8].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 8].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 9] = contrat.Factures.Where(f => f.TypeFacture == TypeFacture.Echeance && f.Echue == true && f.FacturePayee == true).Count().ToString();
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 9].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 10] = contrat.Factures.Where(f => f.TypeFacture == TypeFacture.Echeance && f.Echue == true && f.FacturePayee == false).Count().ToString();
                    xlWorkSheet.Cells[numOrdre + iDepart, 10].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 10].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 11] = contrat.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance || f.TypeFacture == TypeFacture.DepotMinimum)).Sum(f => f.Montant);
                    xlWorkSheet.Cells[numOrdre + iDepart, 11].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 11].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    xlWorkSheet.Cells[numOrdre + iDepart, 12] = contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal);
                    xlWorkSheet.Cells[numOrdre + iDepart, 12].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 12].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    xlWorkSheet.Cells[numOrdre + iDepart, 13] = (contrat.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance || f.TypeFacture == TypeFacture.DepotMinimum)).Sum(f => f.Montant)
                                                                   - contrat.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.Echeance || f.TypeFacture == TypeFacture.DepotMinimum))
                                                                                          .Sum(f => f.Encaissements.Sum(enc => enc.Montant)));
                    xlWorkSheet.Cells[numOrdre + iDepart, 13].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 13].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

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
    }
}
