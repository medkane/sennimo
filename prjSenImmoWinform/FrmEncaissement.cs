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
using Microsoft.Office.Interop.Word;
using System.Net.Mail;

namespace prjSenImmoWinform
{
    public partial class FrmEncaissement : Form
    {
        private ClientRepository clientRep;
        ContratRepository contratRep;
        Client leClientEncours;
        List<Client> AllClientsInitial;
        private Contrat leContratEnCours;
        private ICollection<Contrat> lesContratTrouves;
        private decimal montantFraisDeDossier;
        private bool bModifEncaissement;
        private List<AppelDeFondSimule> appelsDeFondSimules;
        private bool bRechercheParNumeroLot;

        public EncaissementGlobal EncaissementGlobalEnCours { get; private set; }
        public EncaissementProspect EncaissementProspectEnCours { get; private set; }

        public FrmEncaissement()
        {
            InitializeComponent();
            clientRep = new ClientRepository();
            contratRep = new ContratRepository();
            ChargerModesPaiement();

            InitialisationListClients();

            tcEntete.ItemSize = new Size(0, 1);
            tcEntete.SizeMode = TabSizeMode.Fixed;
            tcEntete.ItemSize = new Size(0, 1);
            tcEntete.SizeMode = TabSizeMode.Fixed;
            tcEntete.SelectedTab = tcEntete.TabPages[2];
            AllClientsInitial = clientRep.GetAllClientsEtProspects().ToList();

            //txtClients.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txtClients.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            //foreach (var item in clientRep.GetAllClients())
            //{
            //    data.Add(item.NomComplet);
            //}
            //txtClients.AutoCompleteCustomSource = data;
            lbCreditTotal.Text = "";
            lbDebitTotal.Text = "";
            lbSoldeTotal.Text = "";

        }

        public FrmEncaissement(int projetId,bool bProjet) 
        {
            InitializeComponent();
            clientRep = new ClientRepository();
            contratRep = new ContratRepository();
            ChargerModesPaiement();

            InitialisationListClients();

            tcEntete.ItemSize = new Size(0, 1);
            tcEntete.SizeMode = TabSizeMode.Fixed;
            tcEntete.ItemSize = new Size(0, 1);
            tcEntete.SizeMode = TabSizeMode.Fixed;
            tcEntete.SelectedTab = tcEntete.TabPages[2];
            AllClientsInitial = clientRep.GetAllClientsEtProspects().Where(clt =>clt.ProjetId==projetId).ToList();
            lbProjet.Text = "Projet: "+contratRep.GetProjet(projetId).DenominationProjet;
            //txtClients.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txtClients.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            //foreach (var item in clientRep.GetAllClients())
            //{
            //    data.Add(item.NomComplet);
            //}
            //txtClients.AutoCompleteCustomSource = data;
            lbCreditTotal.Text = "";
            lbDebitTotal.Text = "";
            lbSoldeTotal.Text = "";
        }

        private void InitialisationListClients()
        {
            //cmbClients.DataSource = clientRep.GetAllClientsEtProspects().ToList();
            //cmbClients.DisplayMember = "NomComplet";
            //cmbClients.ValueMember = "Id";

            //cmbClients.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //cmbClients.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            //foreach (var item in clientRep.GetAllClientsEtProspects())
            //{
            //    data.Add(item.NomComplet);
            //}
            //cmbClients.AutoCompleteCustomSource = data;

            //cmbClients.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            //cmbClients.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;

            //var clients = clientRep.GetAllClientsEtProspects().ToList();

            //Dictionary<int, String> dicClients = new Dictionary<int, string>();

            //foreach (var client in clients)
            //{
            //    dicClients.Add(client.ID, client.NomComplet);
            //}

            //cmbClients.DataSource = new BindingSource(dicClients, null);
            //cmbClients.DisplayMember = "Value";
            //cmbClients.ValueMember = "Key";
        }

        public FrmEncaissement(int contratId):this()
        {
            var contrat = contratRep.GetContratById(contratId);
            //foreach (var item in cmbClients.Items)
            //{
            //    if (((Client)item).ID == contrat.ClientID)
            //        cmbClients.SelectedItem = item;
            //}
            leClientEncours = clientRep.GetClient(contrat.ClientID);
            SelectionnerClient();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmdChoisirClient_Click(object sender, EventArgs e)
        {
            try
            {
                tcEntete.SelectedTab = tcEntete.TabPages[0];
                pCompteClient.Visible = false;
                cmdListerContrat.Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:..." + ex.Message,
                      "Prosopis Comptabilité client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherCompteClient(int contratId)
        {

            try
            {
                var mouvementsComptables = contratRep.GetOperationsContrat(contratId).ToList();
                //DgVentillation.Visible = false;
                dgCompteClient.DataSource = mouvementsComptables.ToList().Select(mvt => new
                {
                    Date = mvt.DateOp.ToShortDateString(),
                    NumeroPiece = mvt.NumeroPiece,
                    LibelleOp = mvt.LibelleOp,
                    Débit = mvt.Debit,
                    Crédit = mvt.Credit,
                    Solde = mvt.Solde,
                    TypeMouvement = mvt.TypeOp,
                    ID=mvt.Id

                }).ToList();
                decimal debitTotal = mouvementsComptables.Sum(mvt => mvt.Debit);
                decimal creditTotal = mouvementsComptables.Sum(mvt => mvt.Credit);
                decimal soldeTotal =creditTotal- debitTotal ;

                lbDebitTotal.Text = debitTotal.ToString("### ### ###");
                lbCreditTotal.Text = creditTotal.ToString("### ### ###");
                lbSoldeTotal.Text = soldeTotal.ToString("### ### ###");
                FormatterGrilleMouvement();
                //dgCompteClient.Height = tcCompteClient.TabPages[0].Height - 7;
                //DgVentillation.Visible = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FormatterGrilleMouvement()
        {
            dgCompteClient.Columns[0].Width = 80;
            dgCompteClient.Columns[1].Width = 130;
            dgCompteClient.Columns[0].HeaderText = "Date";
            dgCompteClient.Columns[1].HeaderText = "Numéro pièce";
            dgCompteClient.Columns[2].Width = 565;
            dgCompteClient.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgCompteClient.Columns[2].HeaderText = "Libellé";
            dgCompteClient.Columns[3].Width = 110;
            dgCompteClient.Columns[3].DefaultCellStyle.Format = "### ### ###";
            dgCompteClient.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgCompteClient.Columns[4].Width = 110;
            dgCompteClient.Columns[4].DefaultCellStyle.Format = "### ### ###";
            dgCompteClient.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgCompteClient.Columns[5].Width = 110;
            dgCompteClient.Columns[5].DefaultCellStyle.Format = "### ### ###";
            dgCompteClient.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgCompteClient.Columns[6].Width = 0;
            dgCompteClient.Columns[6].Visible = false;
            dgCompteClient.Columns[7].Width = 0;
            dgCompteClient.Columns[7].Visible = false;


        }

        private void cmdEnregistrerVersement_Click(object sender, EventArgs e)
        {
            if (!bModifEncaissement)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    if (leClientEncours.Type == TypeClient.ProspectSansOption || leClientEncours.Type == TypeClient.ProspectAvecOptionResa || leClientEncours.Type == TypeClient.ProspectAvecOptionDepot)
                    {
                        int idClient = leClientEncours.ID;
                        EnregistrerVersmentProspectEtMAJ();
                        EffacerPaiement();
                        leClientEncours = clientRep.GetClient(idClient);
                        if (leClientEncours.Options.Where(opt => opt.Active == true).Count() > 0)
                        {
                            var option = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault();
                            if (option.SeuilContratAtteint && option.ContratGenere == false && option.Commercial.Email != string.Empty)//et que ????
                            {
                                Tools.Tools.EmailSend(option.Commercial.Email, "", "Génération du Contrat",
                              @" Bonjour " + "\n\n Le prospect " + option.Client.NomComplet + " a atteint le seuil d'encaissement minimum pour bénéficier d'un contrat \n\n Cordialement");

                                if (option.CommercialID != option.Client.CommercialID)
                                    Tools.Tools.EmailSend(option.Client.Commercial.Email, "", "Génération du Contrat",
                             @" Bonjour " + "\n\n Le prospect " + option.Client.NomComplet + " a atteint le seuil d'encaissement minimum pour bénéficier d'un contrat \n\n Cordialement");

                                MessageBox.Show(this, "Un mail d'alerte a été envoyé à " + leClientEncours.Commercial.NomComplet +
                                    " pour l'informer de l'atteinte du seuil de dépot minimum de son prospect",
                          "Prosopis - Encaissements prospect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        EnregistrerVersmentEtMAJ();
                        EffacerPaiement();

                    }
                }
                catch (SmtpException ex)
                {
                    MessageBox.Show(this, "Erreur lors de l'envoi de mail d'alerte au commercial " + leClientEncours.Commercial.NomComplet + ". " + ex.Message,
                           "Prosopis - Encaissements prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Erreur Enregistrement versement" + ex.Message,
                            "Prosopis - Encaissements prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else // en cas de modificaton (mal foutu dans l'urgence à reprendre proprement)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (leClientEncours.Type == TypeClient.Client || leClientEncours.Type == TypeClient.ClientEnCours)
                    {

                        var dateVersement = dtpDateVersement.Value.Date;
                  
                        var referencePaiement = txtReferencePaiement.Text;
                        var commentairesVersement = txtCommentairePaiement.Text;
                        var modePaiement = (ModePaiement)cmbModePaiement.SelectedItem;
                        var numeroPiece = txtNumeroPiece.Text;
                         contratRep.UpdateEncaissement(EncaissementGlobalEnCours.ID, dateVersement, modePaiement, referencePaiement, 
                             commentairesVersement,numeroPiece);
                        MessageBox.Show(this, "L'encaissement a été modifié avec succes",
                             "Prosopis - Modification encaissements client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AfficherCompteClient(leContratEnCours.Id);
                        AfficherEncaissementsGlobauxClient(leContratEnCours.Id);
                    }
                    else
                    {
                        var dateVersement = dtpDateVersement.Value.Date;
                        var referencePaiement = txtReferencePaiement.Text;
                        var commentairesVersement = txtCommentairePaiement.Text;
                        var modePaiement = (ModePaiement)cmbModePaiement.SelectedItem;
                        var numeroPiece = txtNumeroPiece.Text;
                        contratRep.UpdateEncaissementProspect(EncaissementProspectEnCours.ID, dateVersement, modePaiement, referencePaiement,
                            commentairesVersement, numeroPiece);
                        MessageBox.Show(this, "L'encaissement a été modifié avec succes",
                             "Prosopis - Modification encaissements client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AfficherCompteProspect(leClientEncours);
                        AfficherEncaissementsProspect(leClientEncours.ID);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Erreur: "  + ex.Message,
                           "Prosopis - Modification encaissements client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            
        }

        private void EnregistrerVersmentEtMAJ()
        {
            try
            {
                EnregistrerLeVersement();
                AfficherCompteClient(leContratEnCours.Id);
                AfficherEncaissementsGlobauxClient(leContratEnCours.Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void EnregistrerVersmentProspectEtMAJ()
        {
            try
            {
                EnregistrerLeVersementProspect();
                AfficherCompteProspect(leClientEncours);
                AfficherEncaissementsProspect(leClientEncours.ID);
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void EnregistrerLeVersementProspect()
        {
            try
            {
                var dateVersement = dtpDateVersement.Value.Date;
                var montantVersement = decimal.Parse(txtMontantVerse.Text);
                var referencePaiement = txtReferencePaiement.Text;
                var commentairesVersement = txtCommentairePaiement.Text;
                var modePaiement = (ModePaiement)cmbModePaiement.SelectedItem;



                if (chkFraisDossier.Checked)
                {
                    
                    contratRep.EnregistrerFraisDeDossierProspect(leClientEncours.ID, dateVersement,
                        montantFraisDeDossier, modePaiement, referencePaiement, commentairesVersement);
                    montantVersement -= montantFraisDeDossier;
                }


                if(montantVersement >0)
                    contratRep.EnregistrerVersementProspect( leClientEncours.ID, dateVersement, montantVersement, modePaiement, referencePaiement, commentairesVersement);

                //GenererAttestationVersementContratDepot(montantVersement, referencePaiement, dateVersement, encaissementId);

                MessageBox.Show(this, "Le versement a été enregistré avec succes",
                               "Prosopis - Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //AfficherLeContrat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:... " + ex.Message,
                                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnregistrerLeVersement()
        {
            try
            {
                var dateVersement = dtpDateVersement.Value.Date;
                var montantVersement = decimal.Parse(txtMontantVerse.Text);
                var referencePaiement = txtReferencePaiement.Text;
                var commentairesVersement = txtCommentairePaiement.Text;
                var modePaiement = (ModePaiement)cmbModePaiement.SelectedItem;
                leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);
                int encaissementId=contratRep.EnregistrerVersementBis(leContratEnCours.Lot.ID, leClientEncours.ID, dateVersement, montantVersement, leContratEnCours.Id, modePaiement, referencePaiement, commentairesVersement);
                //if(leContratEnCours.TypeContrat.CategorieContrat== CategorieContrat.Réservation)
                //    GenererAttestationVersementContratResa(montantVersement, referencePaiement, dateVersement, encaissementId);
                //else
                //    GenererAttestationVersementContratDepot(montantVersement, referencePaiement, dateVersement, encaissementId);

                //foreach (var encaissement in clientRep.GetEncaissementsClient(encaissementId).ToList())
                //{
                //    txtVentillation.AppendText(encaissement.Montant + " pour " + encaissement.Facture.Motif);
                //}

                //AfficherFacturesClient(leContratEnCours.Id, "");
                MessageBox.Show(this, "Le versement a été enregistré avec succes",
                               "Prosopis Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //AfficherLeContrat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:... " + ex.Message,
                                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenererAttestationVersementContratResa(decimal montantEncaisse, string referencePaiement, DateTime dateVersement, int encaissementId)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;

                leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);

                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "AttestationVersementContratResa.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                //msWord.Activate();
                //doc.Activate();
                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;


                myBookmark = bookmarks["Titre1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "Monsieur" : "Madame";
                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;
                myBookmark = bookmarks["Ville"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = " - " +leClientEncours.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Pays;

                

                myBookmark = bookmarks["ReferencePaiement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text =referencePaiement;

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text =montantEncaisse.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisseEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)montantEncaisse);

                var stringDetailsEncaissements="";
                foreach (var encaissement in clientRep.GetEncaissementsClient(encaissementId).OrderBy(enc => enc.Facture.EtatAvancement.TypeEtatAvancement.ordre).ToList())
                {
                    stringDetailsEncaissements += "-  "+ encaissement.Montant.ToString("### ### ###").Trim() + " FCFA correspondant à ";

                    if (encaissement.Facture.Montant - encaissement.Facture.Encaissements.Sum(enc => enc.Montant) <=0)
                    {
                        if (encaissement.Montant == encaissement.Facture.Montant)
                            stringDetailsEncaissements += "l'appel de fond ";
                        else
                            stringDetailsEncaissements += "un reliquat sur l'appel de fond ";
                    }
                    else

                        stringDetailsEncaissements += "un acompte sur l'appel de fond ";

                    stringDetailsEncaissements += encaissement.Facture.EtatAvancement.TypeEtatAvancement.LibelleCommercial+".";
                    stringDetailsEncaissements+= (char)11;
                }

                myBookmark = bookmarks["PremierEncaissement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = stringDetailsEncaissements;

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType.ToUpper()+" ";

                myBookmark = bookmarks["NumeroLot"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                if(leContratEnCours.Lot.PositionLot== PositionLot.Angle)
                {
                    myBookmark = bookmarks["Position"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "(angle)";
                }
                else
                {
                    myBookmark = bookmarks["Position"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "";
                }

                myBookmark = bookmarks["Superficie"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");

                var factures = contratRep.GetEcheancesClient(leContratEnCours.Id, "").Where(f =>f.Active==true && f.TypeFacture!= TypeFacture.FraisDossier).ToList();

                Microsoft.Office.Interop.Word.Tables tables = doc.Tables;
                if (tables.Count > 0)
                {
                    //Get the first table in the document
                    Microsoft.Office.Interop.Word.Table table = tables[1];
                    foreach (var facture in factures.OrderBy(f => f.EtatAvancement.TypeEtatAvancement.ordre))
                    { 
                       
                        Microsoft.Office.Interop.Word.Row row = table.Rows.Add(ref missing);
                        row.Cells[1].Range.Text = facture.Motif;
                        row.Cells[1].WordWrap = true;
                        row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[1].Range.Bold = 0;

                        decimal taux;
                        TypeContrat tc;
                        //if (facture.EtatAvancement.TypeEtatAvancement.ordre == 2)
                        //{
                        //    //tc = contratRep.GetTypeContrat contrat.TypeContratID);
                        //    taux = leContratEnCours.TypeContrat.SeuilSouscription - 5;
                        //}
                        //else
                        //if (facture.EtatAvancement.TypeEtatAvancement.ordre == 26)
                        //{
                        //    //tc = DB.TypeContrats.Find(contrat.TypeContratID);
                        //    taux = (70 - leContratEnCours.TypeContrat.SeuilSouscription);

                        //}
                        //else
                        //{
                            taux = facture.EtatAvancement.TypeEtatAvancement.TauxDecaissement;
                        //}


                        row.Cells[2].Range.Text = taux.ToString("###")+"%";
                        row.Cells[2].WordWrap = true;
                        row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[2].Range.Bold = 0;


                        row.Cells[3].Range.Text = facture.Montant.ToString("### ### ###");
                        row.Cells[3].WordWrap = true;
                        row.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[3].Range.Bold = 0;

                        row.Cells[4].Range.Text = facture.Encaissements.Sum(enc => enc.Montant)!=0? facture.Encaissements.Sum(enc => enc.Montant).ToString("### ### ###"):"0";
                        row.Cells[4].WordWrap = true;
                        row.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[4].Range.Bold =0;

                        row.Cells[5].Range.Text = (facture.Montant - facture.Encaissements.Sum(enc => enc.Montant))!=0?(facture.Montant - facture.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###"):"0";
                        row.Cells[5].WordWrap = true;
                        row.Cells[5].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[5].Range.Bold = 0;
                    }

                    Microsoft.Office.Interop.Word.Row rowTotal = table.Rows.Add(ref missing);

                    rowTotal.Cells[1].Range.Text = "Total";
                    rowTotal.Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    rowTotal.Cells[1].WordWrap = true;
                    rowTotal.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[1].Range.Bold = 1;
                    rowTotal.Cells[1].Merge(rowTotal.Cells[2]);

                    rowTotal.Cells[2].Range.Text = factures.Sum(fact => fact.Montant).ToString("### ### ###");
                    rowTotal.Cells[2].WordWrap = true;
                    rowTotal.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[2].Range.Bold = 1;

                    rowTotal.Cells[3].Range.Text = factures.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###");
                    rowTotal.Cells[3].WordWrap = true;
                    rowTotal.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[3].Range.Bold = 1;

                    rowTotal.Cells[4].Range.Text = (factures.Sum(fact => fact.Montant) - factures.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
                    rowTotal.Cells[4].WordWrap = true;
                    rowTotal.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[4].Range.Bold = 1;
                }


                myBookmark = bookmarks["DateEncaissement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = dateVersement.Day+ " "+ String.Format("{0:y}", dateVersement); //dateVersement.ToShortDateString();
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
                msWord.Visible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GenererAttestationVersementContratResaKerria(decimal montantEncaisse, string referencePaiement, DateTime dateVersement, int encaissementId)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;

                leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);
                leClientEncours = clientRep.GetClient(leClientEncours.ID);

                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName;
                if (leContratEnCours.TypeContrat.TypeConstruction== TypeConstruction.Appartement)
                    templateName = dossierTemplates + "AttestationVersementContratResaKerriaAppart.dotx";
                else
                    templateName = dossierTemplates + "AttestationVersementContratResaKerriaVilla.dotx";

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
                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;
                myBookmark = bookmarks["Ville"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = " - " + leClientEncours.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Pays;



                myBookmark = bookmarks["ReferencePaiement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = referencePaiement;

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = montantEncaisse.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisseEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)montantEncaisse);

                //myBookmark = bookmarks["MontantTotalVerse"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leClientEncours.EncaissementProspects.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ###");

                var stringDetailsEncaissements = "";
                foreach (var encaissement in clientRep.GetEncaissementsClient(encaissementId).OrderBy(enc => enc.Facture.EtatAvancement.TypeEtatAvancement.ordre).ToList())
                {
                    stringDetailsEncaissements += "-  " + encaissement.Montant.ToString("### ### ###").Trim() + " FCFA correspondant à ";

                    if (encaissement.Facture.Montant - encaissement.Facture.Encaissements.Sum(enc => enc.Montant) <= 0)
                    {
                        if (encaissement.Montant == encaissement.Facture.Montant)
                            stringDetailsEncaissements += "l'appel de fond ";
                        else
                            stringDetailsEncaissements += "un reliquat sur l'appel de fond ";
                    }
                    else

                        stringDetailsEncaissements += "un acompte sur l'appel de fond ";

                    stringDetailsEncaissements += encaissement.Facture.EtatAvancement.TypeEtatAvancement.LibelleCommercial + ".";
                    stringDetailsEncaissements += (char)11;
                }

                myBookmark = bookmarks["PremierEncaissement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = stringDetailsEncaissements;

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType.ToUpper() + " ";

                myBookmark = bookmarks["Immeuble"];
                bookmarkRange = myBookmark.Range;
                if (leContratEnCours.TypeContrat.TypeConstruction == TypeConstruction.Appartement)
                    bookmarkRange.Text = leContratEnCours.Lot.Ilot.NomIlot.ToUpper() + " ";
                else
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType.ToUpper() + " ";

                myBookmark = bookmarks["NumeroLot"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.NumeroLot;

                //if (leContratEnCours.Lot.PositionLot == PositionLot.Angle)
                //{
                //    myBookmark = bookmarks["Position"];
                //    bookmarkRange = myBookmark.Range;
                //    bookmarkRange.Text = "(angle)";
                //}
                //else
                //{
                //    myBookmark = bookmarks["Position"];
                //    bookmarkRange = myBookmark.Range;
                //    bookmarkRange.Text = "";
                //}
                if (leContratEnCours.TypeContrat.TypeConstruction == TypeConstruction.Villa)
                { 
                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###");
                }

                var factures = contratRep.GetEcheancesClient(leContratEnCours.Id, "").Where(f => f.Active == true && f.TypeFacture != TypeFacture.FraisDossier).ToList();

                Microsoft.Office.Interop.Word.Tables tables = doc.Tables;
                if (tables.Count > 0)
                {
                    //Get the first table in the document
                    Microsoft.Office.Interop.Word.Table table = tables[1];
                    foreach (var facture in factures.OrderBy(f => f.EtatAvancement.TypeEtatAvancement.ordre))
                    {

                        Microsoft.Office.Interop.Word.Row row = table.Rows.Add(ref missing);
                        row.Cells[1].Range.Text = facture.Motif;
                        row.Cells[1].WordWrap = true;
                        row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[1].Range.Bold = 0;

                        decimal taux;
                        TypeContrat tc;
                        //if (facture.EtatAvancement.TypeEtatAvancement.ordre == 2)
                        //{
                        //    //tc = contratRep.GetTypeContrat contrat.TypeContratID);
                        //    taux = leContratEnCours.TypeContrat.SeuilSouscription - 5;
                        //}
                        //else
                        //if (facture.EtatAvancement.TypeEtatAvancement.ordre == 26)
                        //{
                        //    //tc = DB.TypeContrats.Find(contrat.TypeContratID);
                        //    taux = (70 - leContratEnCours.TypeContrat.SeuilSouscription);

                        //}
                        //else
                        //{
                            taux = facture.EtatAvancement.TypeEtatAvancement.TauxDecaissement;
                        //}


                        row.Cells[2].Range.Text = taux.ToString("###") + "%";
                        row.Cells[2].WordWrap = true;
                        row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[2].Range.Bold = 0;


                        row.Cells[3].Range.Text = facture.Montant.ToString("### ### ###");
                        row.Cells[3].WordWrap = true;
                        row.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[3].Range.Bold = 0;

                        row.Cells[4].Range.Text = facture.Encaissements.Sum(enc => enc.Montant) != 0 ? facture.Encaissements.Sum(enc => enc.Montant).ToString("### ### ###") : "0";
                        row.Cells[4].WordWrap = true;
                        row.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[4].Range.Bold = 0;

                        row.Cells[5].Range.Text = (facture.Montant - facture.Encaissements.Sum(enc => enc.Montant)) != 0 ? (facture.Montant - facture.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###") : "0";
                        row.Cells[5].WordWrap = true;
                        row.Cells[5].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[5].Range.Bold = 0;
                    }

                    Microsoft.Office.Interop.Word.Row rowTotal = table.Rows.Add(ref missing);

                    rowTotal.Cells[1].Range.Text = "Total";
                    rowTotal.Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    rowTotal.Cells[1].WordWrap = true;
                    rowTotal.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[1].Range.Bold = 1;
                    rowTotal.Cells[1].Merge(rowTotal.Cells[2]);

                    rowTotal.Cells[2].Range.Text = factures.Sum(fact => fact.Montant).ToString("### ### ###");
                    rowTotal.Cells[2].WordWrap = true;
                    rowTotal.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[2].Range.Bold = 1;

                    rowTotal.Cells[3].Range.Text = factures.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###");
                    rowTotal.Cells[3].WordWrap = true;
                    rowTotal.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[3].Range.Bold = 1;

                    rowTotal.Cells[4].Range.Text = (factures.Sum(fact => fact.Montant) - factures.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
                    rowTotal.Cells[4].WordWrap = true;
                    rowTotal.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[4].Range.Bold = 1;
                }


                myBookmark = bookmarks["DateEncaissement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = dateVersement.Day + " " + String.Format("{0:y}", dateVersement); //dateVersement.ToShortDateString();
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
                msWord.Visible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GenererAttestationVersementContratDepot(decimal montantEncaisse, string referencePaiement, DateTime dateVersement, int encaissementId)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = false; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;

                leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);
                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "AttestationVersementContratDepot.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);

                //msWord.Activate();
                //doc.Activate();

                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;


                myBookmark = bookmarks["Titre1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "Monsieur" : "Madame";
                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;
                myBookmark = bookmarks["Ville"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = " - " + leClientEncours.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Pays;



                myBookmark = bookmarks["ReferencePaiement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = referencePaiement;

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.PrixFinal.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = montantEncaisse.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisseEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)montantEncaisse);

                var stringDetailsEncaissements = "";
                var lesEncaissements = clientRep.GetEncaissementsClient(encaissementId).ToList();
                foreach (var encaissement in lesEncaissements)
                {
                    stringDetailsEncaissements += "-  " + encaissement.Montant.ToString("### ### ###")+ " FCFA correspondant à ";

                    if (encaissement.Facture.Montant - encaissement.Facture.Encaissements.Sum(enc => enc.Montant) <= 0)
                    {
                        if (encaissement.Montant == encaissement.Facture.Montant)
                            stringDetailsEncaissements += "l'";
                        else
                            stringDetailsEncaissements += "un reliquat sur l'";
                    }
                    else

                        stringDetailsEncaissements += "un acompte sur l'";

                    stringDetailsEncaissements += encaissement.Facture.Motif + ".";
                    stringDetailsEncaissements += (char)11;
                }

                myBookmark = bookmarks["PremierEncaissement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = stringDetailsEncaissements;

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType;

                myBookmark = bookmarks["CodeTypeVilla"];
                bookmarkRange = myBookmark.Range;
                if(leContratEnCours.LotAttribue)
                {
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType+ " N° "+ leContratEnCours.Lot.NumeroLot;
                }
                else
                {
                    bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType;
                }
                

                myBookmark = bookmarks["Superficie"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.SurfaceDeBase.ToString("###");

                if (leContratEnCours.Lot.PositionLot == PositionLot.Angle)
                {
                    myBookmark = bookmarks["PositionAngle"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "(angle)";
                }
                else
                {
                    myBookmark = bookmarks["PositionAngle"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "";
                }

                //var factures = clientRep.GetEcheancesClient(leContratEnCours.Id, "").Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).ToList();

                //var mouvementsComptables=contratRep.GetOperationsContrat(leContratEnCours.Id).ToList();
                //decimal debitTotal = mouvementsComptables.Sum(mvt => mvt.Debit);
                //decimal creditTotal = mouvementsComptables.Sum(mvt => mvt.Credit);
                //decimal montantSolde = creditTotal - debitTotal;


                var factures = clientRep.GetEcheancesClient(leContratEnCours.Id, "").Where(f => f.Echue == true && f.TypeFacture != TypeFacture.FraisDossier).ToList();
                var encaissements = contratRep.GetEncaissementGlobals(leContratEnCours.Id);
                myBookmark = bookmarks["Solde"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = encaissements.Sum(enc => enc.Montant).ToString("### ### ###");

                myBookmark = bookmarks["DateEncaissement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = dateVersement.Day + " " + String.Format("{0:y}", dateVersement);

                ////Microsoft.Office.Interop.Word.Tables tables = doc.Tables;
                ////if (tables.Count > 0)
                ////{
                ////    //Get the first table in the document
                ////    Microsoft.Office.Interop.Word.Table table = tables[1];
                ////    var lesAppelsDeFonds = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true);
                ////    //foreach (var appelDeFond in lesAppelsDeFonds)
                ////    //{

                ////    var depotMinimum = leContratEnCours.Factures.Where(fact => fact.TypeFacture == TypeFacture.DepotMinimum).FirstOrDefault();
                ////    //txtDepotMinimum.Text = depotMinimum.Montant.ToString("### ### ###");


                ////    //txtMontantDepotMinimumEncaisse.Text = depotMinimum.Encaissements.Sum(enc => enc.Montant).ToString("### ### ###");


                ////    //txtMontantDepotMinimumRestant.Text = (depotMinimum.Montant - depotMinimum.Encaissements.Sum(enc => enc.Montant)).ToString();



                ////    Microsoft.Office.Interop.Word.Row row = table.Rows.Add(ref missing);

                ////    row.Cells[1].Range.Text = "Dépôt minimum " + leContratEnCours.TypeContrat.SeuilSouscription + "%";
                ////    row.Cells[1].WordWrap = true;
                ////    row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[1].Range.Bold = 0;

                ////    row.Cells[2].Range.Text = depotMinimum.Montant.ToString("### ### ###"); ;
                ////    row.Cells[2].WordWrap = true;
                ////    row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[2].Range.Bold = 0;

                ////    row.Cells[3].Range.Text = depotMinimum.Encaissements.Sum(enc => enc.Montant).ToString("### ### ###");
                ////    row.Cells[3].WordWrap = true;
                ////    row.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[3].Range.Bold = 0;


                ////    row.Cells[4].Range.Text = (depotMinimum.Montant - depotMinimum.Encaissements.Sum(enc => enc.Montant)) == 0 ? "0" : (depotMinimum.Montant - depotMinimum.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###");
                ////    row.Cells[4].WordWrap = true;
                ////    row.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[4].Range.Bold = 0;


                ////    row = table.Rows.Add(ref missing);

                ////    row.Cells[1].Range.Text = "Echéances";
                ////    row.Cells[1].WordWrap = true;
                ////    row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[1].Range.Bold = 0;

                ////    row.Cells[2].Range.Text = leContratEnCours.Factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant).ToString("### ### ###");
                ////    row.Cells[2].WordWrap = true;
                ////    row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[2].Range.Bold = 0;

                ////    row.Cells[3].Range.Text = leContratEnCours.Factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###"); ;
                ////    row.Cells[3].WordWrap = true;
                ////    row.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[3].Range.Bold = 0;


                ////    row.Cells[4].Range.Text = (leContratEnCours.Factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant) -
                ////                                leContratEnCours.Factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
                ////    row.Cells[4].WordWrap = true;
                ////    row.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[4].Range.Bold = 0;

                ////    row = table.Rows.Add(ref missing);

                ////    row.Cells[1].Range.Text = "TOTAL";
                ////    row.Cells[1].WordWrap = true;
                ////    row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[1].Range.Bold = 0;

                ////    row.Cells[2].Range.Text = leContratEnCours.Factures.Where(fact => fact.Echue == true && fact.TypeFacture != TypeFacture.FraisDossier).Sum(fact => fact.Montant).ToString("### ### ###");
                ////    row.Cells[2].WordWrap = true;
                ////    row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[2].Range.Bold = 0;

                ////    row.Cells[3].Range.Text = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###"); ;
                ////    row.Cells[3].WordWrap = true;
                ////    row.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[3].Range.Bold = 0;


                ////    row.Cells[4].Range.Text = (leContratEnCours.Factures.Where(fact => fact.Echue == true && fact.TypeFacture != TypeFacture.FraisDossier).Sum(fact => fact.Montant) -
                ////                                leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
                ////    row.Cells[4].WordWrap = true;
                ////    row.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    row.Cells[4].Range.Bold = 0;


                ////    //Microsoft.Office.Interop.Word.Row rowTotal = table.Rows.Add(ref missing);

                ////    //rowTotal.Cells[1].Range.Text = "Total";
                ////    //rowTotal.Cells[1].WordWrap = true;
                ////    //rowTotal.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    //rowTotal.Cells[1].Range.Bold = 1;

                ////    //rowTotal.Cells[2].Range.Text = lesAppelsDeFonds.Sum(fact => fact.Montant).ToString("### ### ###");
                ////    //rowTotal.Cells[2].WordWrap = true;
                ////    //rowTotal.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    //rowTotal.Cells[2].Range.Bold = 1;


                ////    //rowTotal.Cells[3].Range.Text = lesAppelsDeFonds.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###");
                ////    //rowTotal.Cells[3].WordWrap = true;
                ////    //rowTotal.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    //rowTotal.Cells[3].Range.Bold = 1;

                ////    //rowTotal.Cells[4].Range.Text = (lesAppelsDeFonds.Sum(fact => fact.Montant) - lesAppelsDeFonds.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
                ////    //rowTotal.Cells[4].WordWrap = true;
                ////    //rowTotal.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                ////    //rowTotal.Cells[4].Range.Bold = 1;
                ////}
                msWord.Visible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void GenererAttestationVersementProspectKerria(decimal montantEncaisse, string referencePaiement, DateTime dateVersement, int encaissementId)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;

                leClientEncours = clientRep.GetClient(leClientEncours.ID);

                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName;
                if (leClientEncours.ProjetId==1)
                    templateName = dossierTemplates + "AttestationVersementProspect.dotx";
                else
                    //if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.TypeConstruction== TypeConstruction.Appartement)
                        templateName = dossierTemplates + "AttestationVersementProspectKerria.dotx";
                    //else
                    //{
                    //    templateName = dossierTemplates + "AttestationVersementProspectKerriaVilla.dotx";
                    //}



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
                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;
                myBookmark = bookmarks["Ville"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = " - " + leClientEncours.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Pays;

                if(leClientEncours.ProjetId == 2)
                { 
                    if(leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.TypeConstruction==TypeConstruction.Appartement)
                    {
                        myBookmark = bookmarks["Immeuble"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = " à la résidence "+leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.Ilot.NomIlot.ToUpper().Trim() + " ";

                        myBookmark = bookmarks["TypeConstruction1"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "un Appartement";

                        myBookmark = bookmarks["TypeConstruction2"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "l'Appartement";


                    }
                    else
                    {
                        myBookmark = bookmarks["Immeuble"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "";

                        myBookmark = bookmarks["TypeConstruction1"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "une Villa";

                        myBookmark = bookmarks["TypeConstruction2"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "la Villa";
                    }
                }

                myBookmark = bookmarks["ReferencePaiement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = referencePaiement;

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = montantEncaisse.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisseEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)montantEncaisse);

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.CodeType.ToUpper().Trim() + " ";

                //myBookmark = bookmarks["Immeuble"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.Ilot.NomIlot.ToUpper().Trim() + " ";

                myBookmark = bookmarks["MontantTotalVerse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.EncaissementProspects.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ###");

                if (leClientEncours.ProjetId==1)
                {
                    if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                    {
                        if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().PositionLot == PositionLot.Angle)
                        {
                            myBookmark = bookmarks["PositionNumeroLot"];
                            bookmarkRange = myBookmark.Range;
                            bookmarkRange.Text = "(angle)";
                        }
                        else
                        {
                            myBookmark = bookmarks["PositionNumeroLot"];
                            bookmarkRange = myBookmark.Range;
                            bookmarkRange.Text = "";
                        }
                        myBookmark = bookmarks["Superficie"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.SurfaceDeBase.ToString("###");
                    }
                    else
                    {

                        if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.PositionLot == PositionLot.Angle)
                        {
                            myBookmark = bookmarks["PositionNumeroLot"];
                            bookmarkRange = myBookmark.Range;
                            bookmarkRange.Text = "(" + leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.NumeroLot + " angle)";
                        }
                        else
                        {
                            myBookmark = bookmarks["PositionNumeroLot"];
                            bookmarkRange = myBookmark.Range;
                            bookmarkRange.Text = "(" + leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault()?.Lot.NumeroLot + ")";
                        }

                        myBookmark = bookmarks["Superficie"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault()?.Lot.Superficie.ToString("###");
                    }

                }
                else
                {
                    if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().PositionLot == PositionLot.Angle)
                    {
                        myBookmark = bookmarks["PositionAngle"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "(angle)";
                    }
                    else
                    {
                        myBookmark = bookmarks["PositionAngle"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "";
                    }
                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault()?.Lot.Superficie.ToString("###");

                    myBookmark = bookmarks["NumeroLot"];
                    bookmarkRange = myBookmark.Range;
                    if(leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.TypeVilla.TypeConstruction== TypeConstruction.Appartement)
                        bookmarkRange.Text ="N° "+ leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault()?.Lot.NumeroLot;
                    else
                        bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault()?.Lot.TypeVilla.NomType+ " N° " + leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault()?.Lot.NumeroLot;
                }

                myBookmark = bookmarks["DateEncaissement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = dateVersement.Day + " " + String.Format("{0:y}", dateVersement); //dateVersement.ToShortDateString();
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
                //msWord.Visible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void GenererRecuFraisDeGestion(decimal montantEncaisse, string referencePaiement, DateTime dateVersement, int encaissementId)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;

                //leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);
                leClientEncours = clientRep.GetClient(leClientEncours.ID);

                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName;
                if (leClientEncours.ProjetId==1)
                    templateName = dossierTemplates + "RecuFraisDeGestionAKYS.dotx";
                else
                    templateName = dossierTemplates + "RecuFraisDeGestionKerria.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                //msWord.Activate();
                //doc.Activate();

                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;


                myBookmark = bookmarks["Titre"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "Monsieur" : "Madame";
                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


              


                myBookmark = bookmarks["ReferencePaiement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = referencePaiement;

              
                myBookmark = bookmarks["MontantEncaisse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = montantEncaisse.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisseEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)montantEncaisse);


             

               // myBookmark = bookmarks["TypeVilla"];
               // bookmarkRange = myBookmark.Range;
               // if(leClientEncours.Type== TypeClient.ProspectAvecOptionDepot || leClientEncours.Type == TypeClient.ProspectAvecOptionResa)
               //     bookmarkRange.Text = leClientEncours.Options.FirstOrDefault().TypeVilla.CodeType.ToUpper() + " ";
               // else if (leClientEncours.Type == TypeClient.Client || leClientEncours.Type == TypeClient.ClientEnCours)
               //     bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.CodeType.ToUpper() + " ";

               // myBookmark = bookmarks["NomTypeVilla"];
               // bookmarkRange = myBookmark.Range;
               // //bookmarkRange.Text = leClientEncours.Options.FirstOrDefault().TypeVilla.NomType.ToUpper() + " ";
               // if (leClientEncours.Type == TypeClient.ProspectAvecOptionDepot || leClientEncours.Type == TypeClient.ProspectAvecOptionResa)
               //     bookmarkRange.Text = leClientEncours.Options.FirstOrDefault().TypeVilla.NomType.ToUpper() + " ";
               // else if (leClientEncours.Type == TypeClient.Client || leClientEncours.Type == TypeClient.ClientEnCours)
               //     bookmarkRange.Text = leContratEnCours.Lot.TypeVilla.NomType.ToUpper() + " ";

               // myBookmark = bookmarks["Superficie"];
               // bookmarkRange = myBookmark.Range;
               //// bookmarkRange.Text = leClientEncours.Options.FirstOrDefault().TypeVilla.SurfaceDeBase.ToString("###");
               // if (leClientEncours.Type == TypeClient.ProspectAvecOptionDepot || leClientEncours.Type == TypeClient.ProspectAvecOptionResa)
               //     bookmarkRange.Text = leClientEncours.Options.FirstOrDefault().TypeVilla.SurfaceDeBase.ToString("###");
               // else if (leClientEncours.Type == TypeClient.Client || leClientEncours.Type == TypeClient.ClientEnCours)
               //     bookmarkRange.Text = leContratEnCours.Lot.Superficie.ToString("###"); 

                myBookmark = bookmarks["DateEncaissement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = dateVersement.Day + " " + String.Format("{0:y}", dateVersement); //dateVersement.ToShortDateString();
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
                //msWord.Visible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }



        private void GenererAttestationVersementProspectSeuilContratResa(decimal montantEncaisse, string referencePaiement, DateTime dateVersement, int encaissementId)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;

                leClientEncours = clientRep.GetClient(leClientEncours.ID);

                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "AttestationVersementProspectAtteinteSeuilContratResa.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                //msWord.Activate();
                //doc.Activate();

                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;

                leClientEncours = clientRep.GetClient(leClientEncours.ID);

                myBookmark = bookmarks["Titre1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "Monsieur" : "Madame";
                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;
                myBookmark = bookmarks["Ville"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = " - " + leClientEncours.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Pays;



                myBookmark = bookmarks["ReferencePaiement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = referencePaiement;

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = montantEncaisse.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisseEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)montantEncaisse);



                var stringDetailsEncaissements = "";
                foreach (var encaissement in appelsDeFondSimules.OrderBy(adf => adf.Ordre).ToList())
                {
                    if (encaissement.Encaissé >0)
                    {
                        stringDetailsEncaissements += "-  " + encaissement.Encaissé.ToString("### ### ###").Trim() + " FCFA correspondant à ";

                        if (encaissement.Montant - encaissement.Encaissé <= 0)
                        {
                            if (encaissement.Montant == encaissement.Encaissé)
                                stringDetailsEncaissements += "l'appel de fond ";
                            else
                                stringDetailsEncaissements += "un reliquat sur l'appel de fond ";
                        }
                        else

                            stringDetailsEncaissements += "un acompte sur l'appel de fond ";

                        stringDetailsEncaissements += encaissement.Niveau + ".";
                        stringDetailsEncaissements += (char)11; 
                    }
                }


                myBookmark = bookmarks["Ventillation"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = stringDetailsEncaissements;

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.NomType.ToUpper() + " ";

                if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                {
                    if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().PositionLot == PositionLot.Angle)
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "(angle)";
                    }
                    else
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "";
                    }
                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.SurfaceDeBase.ToString("###");
                }
                else
                {

                    if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.PositionLot == PositionLot.Angle)
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "(" + leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.NumeroLot + " angle)";
                    }
                    else
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "(" + leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.NumeroLot + ")";
                    }

                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.Superficie.ToString("###");
                }

                myBookmark = bookmarks["DateEncaissement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = dateVersement.Day + " " + String.Format("{0:y}", dateVersement); //dateVersement.ToShortDateString();
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
                //msWord.Visible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void GenererAttestationVersementProspectSeuilContratResaKerria(decimal montantEncaisse, string referencePaiement, DateTime dateVersement, int encaissementId)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;

                leClientEncours = clientRep.GetClient(leClientEncours.ID);

                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "AttestationVersementProspectAtteinteSeuilContratResaKerria.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                msWord.Activate();
                doc.Activate();

                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;

                leClientEncours = clientRep.GetClient(leClientEncours.ID);

                myBookmark = bookmarks["Titre1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "Monsieur" : "Madame";
                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;
                myBookmark = bookmarks["Ville"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = " - " + leClientEncours.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Pays;



                myBookmark = bookmarks["ReferencePaiement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = referencePaiement;

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = montantEncaisse.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisseEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)montantEncaisse);



                var stringDetailsEncaissements = "";
                foreach (var encaissement in appelsDeFondSimules.OrderBy(adf => adf.Ordre).ToList())
                {
                    if (encaissement.Encaissé > 0)
                    {
                        stringDetailsEncaissements += "-  " + encaissement.Encaissé.ToString("### ### ###").Trim() + " FCFA correspondant à ";

                        if (encaissement.Montant - encaissement.Encaissé <= 0)
                        {
                            if (encaissement.Montant == encaissement.Encaissé)
                                stringDetailsEncaissements += "l'appel de fond ";
                            else
                                stringDetailsEncaissements += "un reliquat sur l'appel de fond ";
                        }
                        else

                            stringDetailsEncaissements += "un acompte sur l'appel de fond ";

                        stringDetailsEncaissements += encaissement.Niveau + ".";
                        stringDetailsEncaissements += (char)11;
                    }
                }


                myBookmark = bookmarks["Ventillation"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = stringDetailsEncaissements;

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.NomType.ToUpper();


                myBookmark = bookmarks["CodeType"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.CodeType.ToUpper() + " ";

                if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.TypeConstruction == TypeConstruction.Appartement)
                {
                    myBookmark = bookmarks["TypeConstruction1"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "de l'Appartement";
                    myBookmark = bookmarks["TypeConstruction2"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "de l'Appartement";
                }
                else
                {
                    myBookmark = bookmarks["TypeConstruction1"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "de la villa";
                    myBookmark = bookmarks["TypeConstruction2"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "de la villa";
                }

                myBookmark = bookmarks["MontantTotalVerse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.EncaissementProspects.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ###");


                if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                {
                    if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().PositionLot == PositionLot.Angle)
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "(angle)";
                    }
                    else
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "";
                    }
                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.SurfaceDeBase.ToString("###");
                }
                else
                {

                    if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.PositionLot == PositionLot.Angle)
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "(" + leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.NumeroLot + " angle)";
                    }
                    else
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "(" + leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.NumeroLot + ")";
                    }

                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.Superficie.ToString("###");
                }

                myBookmark = bookmarks["DateEncaissement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = dateVersement.Day + " " + String.Format("{0:y}", dateVersement); //dateVersement.ToShortDateString();
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
                //msWord.Visible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void txtContrat_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtContrat.Text!=string.Empty)
            //    {

            //        int contratId = Int16.Parse(txtContrat.Text);
            //        leContratEnCours = contratRep.GetContratById(contratId);
            //        if (leContratEnCours != null)
            //        {
            //            leClientEncours = leContratEnCours.Client;
            //            cmbClients.Text = leClientEncours.NomComplet;
            //            if (leContratEnCours.Lot != null)
            //                cmbLots.Text = leContratEnCours.Lot.NumeroLot;
            //            AfficherFacturesClient(contratId);
            //            AfficherEncaissementsGlobauxClient(contratId);
            //        } 
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(this, "Erreur:... " + ex.Message,
            //                          "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void AfficherEncaissementsGlobauxClient(int contratId)
        {
            try
            {
                chkFraisDossier.Checked = false;
                lbCasPremierEncaissement.Text = "";
                pFraisDeDossier.Visible = false;

                var encaissements = clientRep.GetEncaissementsGlobauxClient(contratId).ToList();
                dgEncaissementsGlobals.DataSource = encaissements.ToList()
                    .Select(encG => new
                    {
                        ID = encG.ID,
                        Date = encG.DateEncaissement.Value.ToShortDateString(),
                        Numéro = encG.NumeroEncaissement,
                        Montant = encG.MontantGlobal,
                        Mode = encG.ModePaiement,
                        Référence = encG.ReferencePaiement,
                        Commentaires = encG.Commentaire

                    }).ToList();
                FormatterGrilleEncaissements();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FormatterGrilleEncaissements()
        {
            dgEncaissementsGlobals.Columns[0].Width = 50;
            dgEncaissementsGlobals.Columns[0].Visible = false;
            dgEncaissementsGlobals.Columns[1].Width = 80;
            dgEncaissementsGlobals.Columns[2].Width = 110;
            dgEncaissementsGlobals.Columns[3].Width = 83;
            dgEncaissementsGlobals.Columns[3].DefaultCellStyle.Format = "### ### ###";
            dgEncaissementsGlobals.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgEncaissementsGlobals.Columns[4].Width = 80;
            dgEncaissementsGlobals.Columns[5].Width = 252;
            dgEncaissementsGlobals.Columns[6].Width = 540;

            //dgProspects.Columns[3].HeaderText = "Né(e) le";
            //dgProspects.Columns[4].HeaderText = "à";
            //dgEncaissements.Columns[0].Visible = false;
        }

        private void AfficherEncaissementsProspect(int prospectId)
        {
            try
            {
                pFraisDeDossier.Visible = true;
           

                var encaissements = clientRep.GetEncaissementsProspect(prospectId).ToList();
                //Si le prospect n'a pas d'encaissements alors il s'agit du premier dans quel cas, proposer la déduction des frais de dossier
                if(encaissements.Where(enc =>enc.FraisDeDossier==true).Count() <=0)
                {
                    chkFraisDossier.Visible = true;
                    chkFraisDossier.Checked = true;
                    montantFraisDeDossier = 200000;
                    lbCasPremierEncaissement.Text = "Les frais de dossier de 200 000 FCFA seront déduits de ce premier encaissement ";

                }
                else
                {
                    chkFraisDossier.Checked = false;
                    chkFraisDossier.Visible = false;
                    lbCasPremierEncaissement.Text = "";
                }



                dgEncaissementsGlobals.DataSource = encaissements.ToList()
                    .Select(encProspect => new
                    {
                        ID = encProspect.ID,
                        Date = encProspect.DateEncaissement.Value.ToShortDateString(),
                        Numéro = encProspect.NumeroEncaissement,
                        Montant = encProspect.MontantGlobal,
                        Mode = encProspect.ModePaiement,
                        Référence = encProspect.ReferencePaiement,
                        Commentaires = encProspect.Commentaire

                    }).ToList();

                FormatterGrilleEncaissements();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgEncaissementsGlobals_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                if (leClientEncours.Type == TypeClient.Client || leClientEncours.Type == TypeClient.ClientEnCours)
                {
                    EncaissementGlobalEnCours = null;
                    if (dgEncaissementsGlobals.SelectedRows.Count > 0)
                    {
                        int encaissementGlobalId = (int)dgEncaissementsGlobals.SelectedRows[0].Cells[0].Value;

                        AfficherEncaissementClient(encaissementGlobalId);

                        EncaissementGlobalEnCours = clientRep.GetEncaissementsGlobal(encaissementGlobalId);
                        EffacerPaiement();
                        //VerouillerPaiement();
                        if (EncaissementGlobalEnCours != null)
                            cmdImprimerAttestation.Visible = true;

                        if (!(EncaissementGlobalEnCours.NumeroEncaissement.Substring(0, 4) == "ENFD"))
                        {
                            dtpDateVersement.Value = EncaissementGlobalEnCours.DateEncaissement.Value;
                            dtpDateVersement.Enabled = false;
                            txtNumeroPiece.Text = EncaissementGlobalEnCours.NumeroEncaissement;
                            txtNumeroPiece.ReadOnly = true;
                            txtMontantVerse.Text = EncaissementGlobalEnCours.MontantGlobal.ToString("### ### ###");
                            txtMontantVerse.ReadOnly = true;
                            txtReferencePaiement.Text = EncaissementGlobalEnCours.ReferencePaiement;
                            txtReferencePaiement.ReadOnly = true;
                            txtCommentairePaiement.Text = EncaissementGlobalEnCours.Commentaire;
                            txtCommentairePaiement.ReadOnly = true;
                            cmbModePaiement.SelectedItem = EncaissementGlobalEnCours.ModePaiement;
                            cmbModePaiement.Enabled = false;
                            cmdEnregistrerVersement.Enabled = false;
                        }
                    }
                    else
                        cmdImprimerAttestation.Visible = false; 
                }
                else
                {
                    EncaissementProspectEnCours = null;
                    if (dgEncaissementsGlobals.SelectedRows.Count > 0)
                    {
                        int encaissementProspectId = (int)dgEncaissementsGlobals.SelectedRows[0].Cells[0].Value;

                        EncaissementProspectEnCours = clientRep.GetEncaissementProspect(encaissementProspectId);
                        EffacerPaiement();
                        //VerouillerPaiement();
                        if (!(EncaissementProspectEnCours.NumeroEncaissement.Substring(0, 4) == "ENFD"))
                        {
                            dtpDateVersement.Value = EncaissementProspectEnCours.DateEncaissement.Value;
                            dtpDateVersement.Enabled = false;
                            txtNumeroPiece.Text = EncaissementProspectEnCours.NumeroEncaissement;
                            txtNumeroPiece.ReadOnly = true;
                            txtMontantVerse.Text = EncaissementProspectEnCours.MontantGlobal.ToString("### ### ###");
                            txtMontantVerse.ReadOnly = true;
                            txtReferencePaiement.Text = EncaissementProspectEnCours.ReferencePaiement;
                            txtReferencePaiement.ReadOnly = true;
                            txtCommentairePaiement.Text = EncaissementProspectEnCours.Commentaire;
                            txtCommentairePaiement.ReadOnly = true;
                            cmbModePaiement.SelectedItem = EncaissementProspectEnCours.ModePaiement;
                            cmbModePaiement.Enabled = false;
                            cmdEnregistrerVersement.Enabled = false;
                        }
                        if (EncaissementProspectEnCours != null)
                            cmdImprimerAttestation.Visible = true;
                    }
                    else
                        cmdImprimerAttestation.Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherEncaissementClient(int EncaissementGlobalId)
        {
            try
            {
                if (leClientEncours.Type== TypeClient.Client || leClientEncours.Type== TypeClient.ClientEnCours)
                {
                    var encaissements = clientRep.GetEncaissementsClient(EncaissementGlobalId).OrderBy(e =>e.Date);
                    if (encaissements.Count() > 0)
                    {
                        dgEncaissements.DataSource = encaissements.ToList().Select(enc => new
                        {
                            ID = enc.ID,
                            Date = enc.Date.Value.ToShortDateString(),
                            Motif = enc.Facture.Motif,
                            Montant = enc.Montant
                        }).ToList();
                        dgEncaissements.Columns[0].Width = 0;
                        dgEncaissements.Columns[1].Width = 80;
                        dgEncaissements.Columns[2].Width = 180;
                        dgEncaissements.Columns[3].Width = 80;
                        dgEncaissements.Columns[3].DefaultCellStyle.Format = "### ### ###";
                        dgEncaissements.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        //dgProspects.Columns[3].HeaderText = "Né(e) le";
                        //dgProspects.Columns[4].HeaderText = "à";
                        dgEncaissements.Columns[0].Visible = false; 
                    }
                }
                else
                    dgEncaissements.DataSource = null;
            }
            catch (Exception)
            {

                throw;
            }
        }
      
        public void EffacerPaiement()
        {

            dtpDateVersement.Value = DateTime.Now.Date;
            dtpDateVersement.Enabled = false;
            txtNumeroPiece.Text = string.Empty;
            txtNumeroPiece.ReadOnly = true;
            txtMontantVerse.Text = string.Empty;
            txtMontantVerse.ReadOnly = true;
            txtReferencePaiement.Text = string.Empty;
            txtReferencePaiement.ReadOnly = true;
            txtCommentairePaiement.Text = string.Empty;
            txtCommentairePaiement.ReadOnly = true;
            cmbModePaiement.SelectedIndex = -1;
            cmbModePaiement.Enabled = false;
            cmdEnregistrerVersement.Enabled = false;
        }

        public void ActiverPaiement()
        {

            dtpDateVersement.Value = DateTime.Now.Date;
            dtpDateVersement.Enabled = true;
            txtMontantVerse.Text = string.Empty;
            txtMontantVerse.ReadOnly = false;
            txtNumeroPiece.Text = string.Empty;
            txtNumeroPiece.ReadOnly = true;
            txtReferencePaiement.Text = string.Empty;
            txtReferencePaiement.ReadOnly = false;
            txtCommentairePaiement.Text = string.Empty;
            txtCommentairePaiement.ReadOnly = false;
            cmbModePaiement.SelectedIndex = -1;
            cmbModePaiement.Enabled = true;
            cmdEnregistrerVersement.Enabled = true;
        }
        private void txtMontantPremierVersement_Validated(object sender, EventArgs e)
        {
            if (txtMontantVerse.Text.Trim() == string.Empty || txtMontantVerse.Text.Trim() =="0" )
            {
                
                txtMontantVerse.Text = "0";
                return;
            }
            try
            {
                txtMontantVerse.Text = decimal.Parse(txtMontantVerse.Text).ToString("### ### ###");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur: Vérifier le montant saisi... " + ex.Message,
                                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerModesPaiement()
        {
            cmbModePaiement.DataSource = Enum.GetValues(typeof(ModePaiement));
            //cmbModePaiement.SelectedIndex = -1;
        }

        private void txtCommentairePaiement_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmdEnregistrerVersement_Click(sender, e);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur Enregistrement versement" + ex.Message,
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EffacerForm();
            //EffacerPaiement();
            //dgEncaissements.DataSource = null;

            //try
            //{
            //    if (cmbClients.SelectedItem != null)
            //    {
            //        var client = (Client)cmbClients.SelectedItem;
            //        leClientEncours = clientRep.GetClient(client.ID);
            //        if (leClientEncours != null)
            //        {
            //            txtCommercial.Text = leClientEncours.Commercial!=null?leClientEncours.Commercial.NomComplet:"";
                       
            //            txtAdresse.Text = leClientEncours.Adresse;
            //            txtNumeroMobile.Text = leClientEncours.Mobile1;
            //            txtNumeroFixe.Text = leClientEncours.TelephoneFixe;

            //            lesContratTrouves = leClientEncours.Contrats.Where(c =>c.Statut==StatutContrat.Actif).ToList();
            //            if (lesContratTrouves.Count() > 0 )
            //            {
            //                lbTypeClient.Text = "Client";
            //                if (lesContratTrouves.Count() == 1)
            //                {
            //                    tcEntete.SelectedTab = tcEntete.TabPages[1];
            //                    leContratEnCours = lesContratTrouves.FirstOrDefault();
            //                    if (leContratEnCours != null)
            //                    {
            //                        AfficherContrat(leContratEnCours);
            //                        cmdDossierClient.Visible = true;
            //                    }
            //                    dgComptesClientTrouves.DataSource = null;
            //                    cmdListerContrat.Visible = false;
            //                }
            //                else
            //                {
            //                    dgComptesClientTrouves.DataSource = lesContratTrouves.ToList().Select(
            //                                                cont => new
            //                                                {
            //                                                    ID = cont.Id,
            //                                                    TypeContrat=cont.TypeContrat.LibelleTypeContrat,
            //                                                    Numéro = cont.NumeroContrat,
            //                                                    Typevilla = cont.Lot.TypeVilla.CodeType,
            //                                                    PrixDeVente = cont.PrixFinal

            //                                                }).ToList();
            //                    dgComptesClientTrouves.Columns[0].Width = 0;
            //                    dgComptesClientTrouves.Columns[0].Visible = false;

            //                    dgComptesClientTrouves.Columns[1].Width = 100;
            //                    dgComptesClientTrouves.Columns[2].Width = 150;
            //                    dgComptesClientTrouves.Columns[3].Width = 50;
            //                    dgComptesClientTrouves.Columns[4].Width = 80;
            //                    dgComptesClientTrouves.Columns[4].DefaultCellStyle.Format = "### ### ###";
            //                    dgComptesClientTrouves.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //                    tcEntete.SelectedTab = tcEntete.TabPages[0];
            //                    pCompteClient.Visible = false;
            //                    //cmdListerContrat.Visible = true;
            //                    //pGridContrats.Visible = true;
            //                }
            //            }
            //            else //Le client est prospect
            //            {
            //                AfficherCompteProspect(leClientEncours);
            //                AfficherEncaissementsProspect(leClientEncours.ID);
            //                tcEntete.SelectedTab = tcEntete.TabPages[2];
            //                cmdDossierClient.Visible = false;
                           
            //                pCompteClient.Visible = true;
            //                lbTypeClient.Text = "Prospect";
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    //MessageBox.Show(this, "Erreur:... " + ex.Message,
            //    //                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        private void EffacerForm()
        {
            txtDateSouscriptionProspect.Text = string.Empty;

            txtTypeContratProspect.Text = string.Empty;
            txtTypeVillaProspect.Text = string.Empty;
            txtPositionLotProspect.Text = string.Empty;
            txtLotProspect.Text = string.Empty;
            txtPrixDeVenteProspect.Text = string.Empty;
            txtMontantEncaisseProspect.Text = string.Empty;
            txtTauxEncaissementProspect.Text = string.Empty;
            chkContratGenere.Checked = false;
            chkSeuilContratAtteint.Checked = false;
            lbDebitTotal.Text = string.Empty;
            lbCreditTotal.Text = string.Empty;
            lbSoldeTotal.Text = string.Empty;
        }

        private void AfficherCompteProspect(Client leClientEncours)
        {
            try
            {
                var mouvementsComptables = contratRep.GetMouvementsProspect(leClientEncours.ID).ToList();
                var option = contratRep.GetOptionProspect(leClientEncours.ID);
                if (option!=null)
                {
                    txtDateSouscriptionProspect.Text = option.DatePriseOption.Value.ToShortDateString();
                    txtTypeContratProspect.Text = option.TypeContrat.LibelleTypeContrat;
                    txtTypeVillaProspect.Text = option.TypeVilla.CodeType;
                    txtPositionLotProspect.Text = option.PositionLot.ToString();
                    txtLotProspect.Text = option.Lot != null && option.TypeContrat.CategorieContrat == CategorieContrat.Réservation  ? option.Lot.NumeroLot : "";
                    txtPrixDeVenteProspect.Text = option.PrixDeVente.ToString("### ### ###");
                    var encaissementsProspect = contratRep.GetEncaissementProspect(leClientEncours.ID).Where(enc => enc.FraisDeDossier==false).Sum(enc => enc.MontantGlobal);
                    txtMontantEncaisseProspect.Text = encaissementsProspect.ToString("### ### ###");

                    txtMontantSeuil.Text = option.TypeContrat.CategorieContrat == CategorieContrat.Réservation ?
                            (option.PrixDeVente * option.TypeContrat.SeuilEntreeEnVigueur / 100).ToString("### ### ###") :
                            (option.PrixDeVente * option.TypeContrat.SeuilSouscription / 100).ToString("### ### ###")
                            ;

                    txtTauxEncaissementProspect.Text =(encaissementsProspect/option.PrixDeVente*100).ToString("###.#");
                    chkContratGenere.Checked = option.ContratGenere;
                    chkSeuilContratAtteint.Checked = option.SeuilContratAtteint;
                    
                }
                //var optionProspect=contratRep.get
                //DgVentillation.Visible = false;

                dgCompteClient.DataSource = mouvementsComptables.ToList().Select(mvt => new
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
                decimal debitTotal = mouvementsComptables.Sum(mvt => mvt.Debit);
                decimal creditTotal = mouvementsComptables.Sum(mvt => mvt.Credit);
                decimal soldeTotal = creditTotal - debitTotal;

                lbDebitTotal.Text = debitTotal.ToString("### ### ###");
                lbCreditTotal.Text = creditTotal.ToString("### ### ###");
                lbSoldeTotal.Text = soldeTotal.ToString("### ### ###");
                FormatterGrilleMouvement();
                dgCompteClient.Height = tcCompteClient.TabPages[0].Height - 7;
                DgVentillation.Visible = false;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfficherContrat(Contrat leContrat)
        {
            leContrat = contratRep.GetContratById(leContrat.Id);
            txtContrat.Text = leContrat.NumeroContrat;
            txtTypeContrat.Text = leContrat.TypeContrat.LibelleTypeContrat;

            txtPrixVente.Text = leContrat.PrixFinal.ToString("### ### ###");

            if (leContrat.Lot != null)
            {
                if (leContrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation || (leContrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && leContrat.LotAttribue == true))
                {
                    txtLot.Text = leContrat.Lot.NumeroLot;

                }
                else
                    txtLot.Text = string.Empty;

                txtTypeVilla.Text = leContrat.Lot.TypeVilla.CodeType;
                txtTypeContrat.Text = leContrat.TypeContrat.LibelleTypeContrat;
                //var montantEncaisse = contratRep.GetVersementsClient(leContrat.Id).Sum(vers => vers.MontantGlobal);
                //var montantRestant = leContrat.PrixFinal - montantEncaisse;






                var montantEncaisse = leContrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal);
                txtMontantTotalEncaisse.Text = montantEncaisse.ToString("### ### ###");
                txtMontantTotalRestant.Text = (leContratEnCours.PrixFinal - montantEncaisse).ToString("### ### ###");
                txtNiveauEncaissement.Text = (montantEncaisse / leContratEnCours.PrixFinal * 100).ToString("###.#");







                //txtMontantTotalEncaisse.Text = montantEncaisse.ToString("### ### ###");
                //txtMontantTotalRestant.Text = montantRestant.ToString("### ### ###");
                ////.Factures.Sum(f => f.Encaissements.Sum(e => e.Montant)).ToString("### ### ###");
                ;            }
            AfficherCompteClient(leContrat.Id);
            AfficherEncaissementsGlobauxClient(leContrat.Id);
            pCompteClient.Visible = true;
        }

        private void dgComptesClientTrouves_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgComptesClientTrouves.SelectedRows.Count > 0)
                {
                    int contratId = (int)dgComptesClientTrouves.SelectedRows[0].Cells[0].Value;
                    leContratEnCours = contratRep.GetContratById(contratId);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:... " + ex.Message,
                                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgComptesClientTrouves_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgComptesClientTrouves.SelectedRows.Count > 0)
                {
                    int contratId = (int)dgComptesClientTrouves.SelectedRows[0].Cells[0].Value;
                    leContratEnCours = contratRep.GetContratById(contratId);
                    if (leContratEnCours != null)
                    {
                        AfficherContrat(leContratEnCours);
                        //txtContrat.Text = leContratEnCours.NumeroContrat;
                        //if (leContratEnCours.Lot != null)
                        //    cmbLots.Text = leContratEnCours.Lot.NumeroLot;
                        //AfficherCompteClient(leContratEnCours.Id);
                        //AfficherEncaissementsGlobauxClient(leContratEnCours.Id);
                        tcEntete.SelectedTab = tcEntete.TabPages[1];
                        pCompteClient.Visible = true;
                        cmdDossierClient.Visible = true;
                        Tools.Adorner.AddBadgeTo(cmdListerContrat,leClientEncours.Contrats.Count.ToString(), Color.Orange, Color.WhiteSmoke);
                        cmdListerContrat.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:... " + ex.Message,
                                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtContrat_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DgVentillation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //999; 327
            //999; 160
        }

        private void dgCompteClient_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgCompteClient.SelectedRows.Count > 0)
                {
                    if(leClientEncours.Type==  TypeClient.Client || leClientEncours.Type== TypeClient.ClientEnCours)
                    { 
                        int opId = (int)dgCompteClient.SelectedRows[0].Cells[7].Value;
                        string opType= dgCompteClient.SelectedRows[0].Cells[6].Value.ToString();
                        if (opType == "E")
                            AfficherEncaissementVentiles(opId);
                        else
                            if (opType == "F")
                            AfficherEncaissementVentilesFactures(opId);


                        dgCompteClient.Height = tcCompteClient.TabPages[0].Height - 130;
                        DgVentillation.Location = new System.Drawing.Point(DgVentillation.Location.X, dgCompteClient.Height + 10);
                        DgVentillation.Visible = true;
                    }

                    //DgVentillation.Location = new Point(DgVentillation.Location.X, dgCompteClient.Height - 5);
                    //EffacerPaiement();
                    //VerouillerPaiement();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }

        private void dgCompteClient_SelectionChanged(object sender, EventArgs e)
        {
            //dgCompteClient.Height = tcCompteClient.TabPages[0].Height - 7;
            //DgVentillation.Visible = false;
        }

        private void AfficherEncaissementVentiles(int EncaissementGlobalId)
        {
            try
            {
                var encaissements = clientRep.GetEncaissementsClient(EncaissementGlobalId);
                DgVentillation.DataSource = encaissements.ToList().Select(enc => new
                {
                    ID = enc.ID,
                    Date = enc.Date.Value.ToShortDateString(),
                    Motif = enc.Facture.Motif,
                    Montant = enc.Montant

                }).ToList();
                DgVentillation.Columns[0].Width = 0;
                DgVentillation.Columns[1].Width = 80;
                DgVentillation.Columns[2].Width = 250;
                DgVentillation.Columns[3].Width = 80;
                DgVentillation.Columns[3].DefaultCellStyle.Format = "### ### ###";
                DgVentillation.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //dgProspects.Columns[3].HeaderText = "Né(e) le";
                //dgProspects.Columns[4].HeaderText = "à";
                DgVentillation.Columns[0].Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfficherEncaissementVentilesFactures(int FactureId)
        {
            try
            {
                var facture = contratRep.GetFactureById(FactureId);
                if (facture!=null)
                {
                    DgVentillation.DataSource = facture.Encaissements.ToList().Select(enc => new
                    {
                        ID = enc.ID,
                        Date = enc.Date.Value.ToShortDateString(),
                        NuméroEncaissement = enc.EncaissementGlobal.NumeroEncaissement,
                        Référence = enc.EncaissementGlobal.ReferencePaiement,
                        Total = enc.EncaissementGlobal.MontantGlobal,
                        Lettrage = enc.Montant,
                    }).ToList();
                    DgVentillation.Columns[0].Width = 0;
                    DgVentillation.Columns[0].Visible = false;
                    DgVentillation.Columns[1].Width = 80;
                    DgVentillation.Columns[2].Width = 110;
                    DgVentillation.Columns[3].Width = 300;
                    DgVentillation.Columns[4].Width = 80;
                    DgVentillation.Columns[4].DefaultCellStyle.Format = "### ### ###";
                    DgVentillation.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DgVentillation.Columns[5].Width = 80;
                    DgVentillation.Columns[5].DefaultCellStyle.Format = "### ### ###";
                    DgVentillation.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DgVentillation.Columns[0].Visible = false; 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmdDossierClient_Click(object sender, EventArgs e)
        {
            FrmDossierClient childForm = new FrmDossierClient(leContratEnCours);
            childForm.MdiParent = this.MdiParent;
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
        }

        private void dgComptesClientTrouves_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgComptesClientTrouves_DoubleClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

       


        private void txtMontantVerse_TextChanged(object sender, EventArgs e)
        {
            decimal a;
            if (!decimal.TryParse(txtMontantVerse.Text, out a))
            {
                // If not int clear textbox text or Undo() last operation
                txtMontantVerse.Clear();
            }
            else
            {
                txtMontantVerse.Text = decimal.Parse(txtMontantVerse.Text).ToString("### ### ###");
                txtMontantVerse.SelectionStart = txtMontantVerse.Text.Length;
            }
        }

        private void nudRemiseSurFraisDossier_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkFraisDossier_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFraisDossier.Checked)
            {
                pRemiseSurFraisDossier.Visible = true;
                txtMontantFraisDeDossier.Text = "200000";
                chkRemiseFraisDeDossierAccordee.Checked = false;
                montantFraisDeDossier = 200000;
            }
            else
            {
                pRemiseSurFraisDossier.Visible = false;
                txtMontantFraisDeDossier.Text = string.Empty;
                montantFraisDeDossier = 0;
            }
        }

        private void chkRemiseFraisDeDossierAccordee_CheckedChanged(object sender, EventArgs e)
        {

            if (chkRemiseFraisDeDossierAccordee.Checked)
            {
                pTauxRemiseAccordee.Visible = true;
            }
            else
            {
                pTauxRemiseAccordee.Visible = false;
                txtMontantFraisDeDossier.Text = "200000";
                nudRemiseSurFraisDossier.Value = 0;
            }
        }

        private void nudRemiseSurFraisDossier_ValueChanged_1(object sender, EventArgs e)
        {

            montantFraisDeDossier = (200000 - (200000 * nudRemiseSurFraisDossier.Value / 100));

            if (montantFraisDeDossier == 0)
                txtMontantFraisDeDossier.Text = "0";
            else
                txtMontantFraisDeDossier.Text = montantFraisDeDossier.ToString("### ### ###");
        }



        private void SimulerAppelsDeFont(decimal prixDeVente, decimal montantPremierVersement, decimal tauxRemiseFraisDeDossier)
        {
            List<AppelDeFondSimule> appelsDeFondSimules = new List<AppelDeFondSimule>();

            var appelDeFonds = contratRep.GetNiveauxAvancements().Where(na => na.AppelFonds == true).OrderBy(na => na.ordre);
            var montantAVentiller = montantPremierVersement;
            //EXTRAIRE LES FRAIS DE DOSSIER DANS LE MONTANT GLOBAL

            if (chkFraisDossier.Checked)
            {
                decimal montantFraisDeDossier = 200000;
                if (chkRemiseFraisDeDossierAccordee.Checked)
                    montantFraisDeDossier -= montantFraisDeDossier * tauxRemiseFraisDeDossier / 100;
                appelsDeFondSimules.Add(new AppelDeFondSimule()
                {
                    Niveau = "Frais de dossier",
                    NiveauDecaissement = "0%",
                    Montant = montantFraisDeDossier,
                    Encaissé = montantFraisDeDossier,
                    Restant = 0

                });
                montantAVentiller -= montantFraisDeDossier;
            }

            foreach (var adf in appelDeFonds)
            {
                if (montantAVentiller > 0)
                {

                    decimal montantRestantNiveau = 0;
                    var resteAEncaisser = prixDeVente * adf.TauxDecaissement / 100;
                    decimal montantAEncaisser = 0;

                    if (montantAVentiller >= resteAEncaisser)
                    {
                        montantAEncaisser = resteAEncaisser;
                        montantRestantNiveau = 0;
                    }
                    else
                    {
                        montantAEncaisser = montantAVentiller;
                        montantRestantNiveau = resteAEncaisser - montantAVentiller;
                    }

                    //    var montantEncaisseNiveau = 0;


                    appelsDeFondSimules.Add(new AppelDeFondSimule()
                    {
                        Niveau = adf.LibelleCommercial,
                        NiveauDecaissement = (adf.TauxDecaissement / 100).ToString("#.#%"),
                        Montant = resteAEncaisser,
                        Encaissé = montantAEncaisser,
                        Restant = montantRestantNiveau

                    });
                    montantAVentiller -= montantAEncaisser;
                }
                else
                {
                    var resteAEncaisser = prixDeVente * adf.TauxDecaissement / 100;
                    appelsDeFondSimules.Add(new AppelDeFondSimule()
                    {
                        Niveau = adf.Description,
                        NiveauDecaissement = (adf.TauxDecaissement / 100).ToString("#.#%"),
                        Montant = resteAEncaisser,
                        Encaissé = 0,
                        Restant = resteAEncaisser

                    });
                }

            }

            //dgAppelsDeFonds.DataSource = appelsDeFondSimules.ToList();
            //dgAppelsDeFonds.Columns[0].Width = 135;
            //dgAppelsDeFonds.Columns[1].HeaderText = "Taux";
            //dgAppelsDeFonds.Columns[1].Width = 50;
            //dgAppelsDeFonds.Columns[1].DefaultCellStyle.Format = "###";
            //dgAppelsDeFonds.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgAppelsDeFonds.Columns[2].DefaultCellStyle.Format = "### ### ###";
            //dgAppelsDeFonds.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgAppelsDeFonds.Columns[2].Width = 80;
            //dgAppelsDeFonds.Columns[3].DefaultCellStyle.Format = "### ### ###";
            //dgAppelsDeFonds.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgAppelsDeFonds.Columns[3].Width = 80;
            //dgAppelsDeFonds.Columns[4].DefaultCellStyle.Format = "### ### ###";
            //dgAppelsDeFonds.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgAppelsDeFonds.Columns[4].Width = 80;


        }

        private void tcCompteClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcCompteClient.SelectedIndex != 0)
                panel2.Visible = false;
            else
                panel2.Visible = true;
        }

        private void cmdImprimerAttestation_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (leClientEncours.ProjetId==1)
                {
                    if (leClientEncours.Type == TypeClient.Client || leClientEncours.Type == TypeClient.ClientEnCours)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        //Recuperer l'encaissement en cours

                        if (EncaissementGlobalEnCours.NumeroEncaissement.Substring(0, 4) == "ENFD")
                            GenererRecuFraisDeGestion(EncaissementGlobalEnCours.MontantGlobal, EncaissementGlobalEnCours.ReferencePaiement, EncaissementGlobalEnCours.DateEncaissement.Value, EncaissementGlobalEnCours.ID);

                        else if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                            GenererAttestationVersementContratResa(EncaissementGlobalEnCours.MontantGlobal, EncaissementGlobalEnCours.ReferencePaiement, EncaissementGlobalEnCours.DateEncaissement.Value, EncaissementGlobalEnCours.ID);
                        else
                            GenererAttestationVersementContratDepot(EncaissementGlobalEnCours.MontantGlobal, EncaissementGlobalEnCours.ReferencePaiement, EncaissementGlobalEnCours.DateEncaissement.Value, EncaissementGlobalEnCours.ID);
                    }
                    else
                    {
                        EncaissementProspectEnCours = contratRep.GetEncaissementProspectById(EncaissementProspectEnCours.ID);
                        if (EncaissementProspectEnCours.Facture != null)
                        {
                            if (EncaissementProspectEnCours.Facture.TypeFacture == TypeFacture.FraisDossier)
                                GenererRecuFraisDeGestion(EncaissementProspectEnCours.MontantGlobal, EncaissementProspectEnCours.ReferencePaiement, EncaissementProspectEnCours.DateEncaissement.Value, EncaissementProspectEnCours.ID);
                        }
                        else
                        {
                            if (leClientEncours.Type == TypeClient.ProspectAvecOptionResa && EncaissementProspectEnCours.AtteinteSeuil)
                            {
                                var option = contratRep.GetOptionProspect(leClientEncours.ID);
                                SimulerAppelsDeFond(option,EncaissementProspectEnCours.Prospect.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente,
                                    EncaissementProspectEnCours.Prospect.EncaissementProspects.Where(enc => enc.FraisDeDossier == false).Sum(enc => enc.MontantGlobal), 0);
                                GenererAttestationVersementProspectSeuilContratResa(EncaissementProspectEnCours.MontantGlobal, EncaissementProspectEnCours.ReferencePaiement, EncaissementProspectEnCours.DateEncaissement.Value, EncaissementProspectEnCours.ID);
                            }
                            else
                                GenererAttestationVersementProspect(EncaissementProspectEnCours.MontantGlobal, EncaissementProspectEnCours.ReferencePaiement, EncaissementProspectEnCours.DateEncaissement.Value, EncaissementProspectEnCours.ID);
                        }
                    } 
                }
                else
                {
                    if (leClientEncours.Type == TypeClient.Client || leClientEncours.Type == TypeClient.ClientEnCours)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        //Recuperer l'encaissement en cours

                        if (EncaissementGlobalEnCours.NumeroEncaissement.Substring(0, 4) == "ENFD")
                            GenererRecuFraisDeGestion(EncaissementGlobalEnCours.MontantGlobal, EncaissementGlobalEnCours.ReferencePaiement, EncaissementGlobalEnCours.DateEncaissement.Value, EncaissementGlobalEnCours.ID);

                        else if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                            if(leContratEnCours.TypeContrat.TypeConstruction== TypeConstruction.Appartement)
                            {
                                ///MessageBox.Show("Appartement");
                                GenererAttestationVersementContratResaKerria(EncaissementGlobalEnCours.MontantGlobal, EncaissementGlobalEnCours.ReferencePaiement, EncaissementGlobalEnCours.DateEncaissement.Value, EncaissementGlobalEnCours.ID);
                            }
                            else if(leContratEnCours.TypeContrat.TypeConstruction == TypeConstruction.Villa)
                            {
                                GenererAttestationVersementContratResaKerria(EncaissementGlobalEnCours.MontantGlobal, EncaissementGlobalEnCours.ReferencePaiement, EncaissementGlobalEnCours.DateEncaissement.Value, EncaissementGlobalEnCours.ID);
                                //MessageBox.Show("Villa");
                            }

                               
                            //GenererAttestationVersementContratResaKerria(EncaissementGlobalEnCours.MontantGlobal, EncaissementGlobalEnCours.ReferencePaiement, EncaissementGlobalEnCours.DateEncaissement.Value, EncaissementGlobalEnCours.ID);
                    }
                    else
                    {
                        EncaissementProspectEnCours = contratRep.GetEncaissementProspectById(EncaissementProspectEnCours.ID);
                        if (EncaissementProspectEnCours.Facture != null)
                        {
                            if (EncaissementProspectEnCours.Facture.TypeFacture == TypeFacture.FraisDossier)
                                GenererRecuFraisDeGestion(EncaissementProspectEnCours.MontantGlobal, EncaissementProspectEnCours.ReferencePaiement, EncaissementProspectEnCours.DateEncaissement.Value, EncaissementProspectEnCours.ID);
                        }
                        else
                        {
                            if (leClientEncours.Type == TypeClient.ProspectAvecOptionResa && EncaissementProspectEnCours.AtteinteSeuil)
                            {
                                var option = contratRep.GetOptionProspect(leClientEncours.ID);
                                SimulerAppelsDeFond(option, EncaissementProspectEnCours.Prospect.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente,
                                    EncaissementProspectEnCours.Prospect.EncaissementProspects.Where(enc => enc.FraisDeDossier == false).Sum(enc => enc.MontantGlobal), 0);
                                GenererAttestationVersementProspectSeuilContratResaKerria(EncaissementProspectEnCours.MontantGlobal, EncaissementProspectEnCours.ReferencePaiement, EncaissementProspectEnCours.DateEncaissement.Value, EncaissementProspectEnCours.ID);
                            }
                            else
                                GenererAttestationVersementProspectKerria(EncaissementProspectEnCours.MontantGlobal, EncaissementProspectEnCours.ReferencePaiement, EncaissementProspectEnCours.DateEncaissement.Value, EncaissementProspectEnCours.ID);
                        }

                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { 
                this.Cursor = Cursors.Default;
            }
        }



        private void GenererAttestationVersementProspect(decimal montantEncaisse, string referencePaiement, DateTime dateVersement, int encaissementId)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;

                leClientEncours = clientRep.GetClient(leClientEncours.ID);

                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "AttestationVersementProspect.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                //msWord.Activate();
                //doc.Activate();

                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;


                myBookmark = bookmarks["Titre1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Genre == Genre.Masculin ? "Monsieur" : "Madame";
                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.NomComplet;


                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Adresse;
                myBookmark = bookmarks["Ville"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = " - " + leClientEncours.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Pays;



                myBookmark = bookmarks["ReferencePaiement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = referencePaiement;

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = montantEncaisse.ToString("### ### ###");

                myBookmark = bookmarks["MontantEncaisseEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)montantEncaisse);

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.NomType.ToUpper() + " ";

                if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                {
                    if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().PositionLot == PositionLot.Angle)
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "(angle)";
                    }
                    else
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "";
                    }
                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVilla.SurfaceDeBase.ToString("###");
                }
                else
                {

                    if (leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.PositionLot == PositionLot.Angle)
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "(" + leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.NumeroLot + " angle)";
                    }
                    else
                    {
                        myBookmark = bookmarks["PositionNumeroLot"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = "(" + leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.NumeroLot + ")";
                    }

                    myBookmark = bookmarks["Superficie"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leClientEncours.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.Superficie.ToString("###");
                }

                myBookmark = bookmarks["DateEncaissement"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = dateVersement.Day + " " + String.Format("{0:y}", dateVersement); //dateVersement.ToShortDateString();
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
                //msWord.Visible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void cmdNouveauEncaissement_Click(object sender, EventArgs e)
        {
            ActiverPaiement();
            txtMontantVerse.Focus();
            bModifEncaissement = false;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void annulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this,"Voulez vous réellement annuler cet encaissement?", "Annulation d'encaissements", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (dgEncaissementsGlobals.SelectedRows.Count > 0)
                    {
                        //Supprimer un encaissement global
                        //EncaissementGlobalEnCours
                        // Si c'est un encaissement prospect
                        //Vérifier si l'encaissement est lettre au frais de dossier
                        //si oui supprimer la facture frais de dossier 
                        // Supprimer les encaissements prospects concernés
                        //Si Encaissement client
                        // Retrouver tous les encaissements concernés
                        // Soustraire les montants sur les factures lettrées
                        // supprimer les encaissements
                        //supprimer l'encaissement global 

                        if (leClientEncours.Type == TypeClient.Client || leClientEncours.Type == TypeClient.ClientEnCours)
                        {
                            int encaissementGlobalId = (int)dgEncaissementsGlobals.SelectedRows[0].Cells[0].Value;
                            EncaissementGlobalEnCours = clientRep.GetEncaissementsGlobal(encaissementGlobalId);

                            DateTime maxEncDate = contratRep.GetMaxEncaissementGlobal(leContratEnCours.Id);

                           if (EncaissementGlobalEnCours.DateEncaissement==maxEncDate)
                            {
                                contratRep.SupprimerEncaissementGlobal(EncaissementGlobalEnCours.ID);
                                MessageBox.Show(this, "L'encaissement a été supprimé avec succes",
                                     "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AfficherCompteClient(leContratEnCours.Id);
                                AfficherEncaissementsGlobauxClient(leContratEnCours.Id); 
                            }
                           else
                            {
                                MessageBox.Show(this, "Désolé, il est impossible d'annuler cet encaissement, car il n'est pas le dernier.",
                                     "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            int encaissementProspectId = (int)dgEncaissementsGlobals.SelectedRows[0].Cells[0].Value;
                            EncaissementProspectEnCours = clientRep.GetEncaissementProspect(encaissementProspectId);

                            contratRep.SupprimerEncaissementProspect(EncaissementProspectEnCours.ID);
                            MessageBox.Show(this, "L'encaissement a été supprimé avec succes",
                                     "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            AfficherCompteProspect(leClientEncours);
                            AfficherEncaissementsProspect(leClientEncours.ID);
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int encaissementGlobalId = (int)dgEncaissementsGlobals.SelectedRows[0].Cells[0].Value;
                EncaissementGlobalEnCours = clientRep.GetEncaissementsGlobal(encaissementGlobalId);

                if (leClientEncours.Type == TypeClient.Client || leClientEncours.Type == TypeClient.ClientEnCours)
                {
                    if (!(EncaissementGlobalEnCours.NumeroEncaissement.Substring(0, 4) == "ENFD"))
                    {
                        ActiverPanelPaiement();
                    }
                }
                else
                {
                    int encaissementProspectId = (int)dgEncaissementsGlobals.SelectedRows[0].Cells[0].Value;
                    EncaissementProspectEnCours = clientRep.GetEncaissementProspect(encaissementProspectId);
                    if (!(EncaissementProspectEnCours.NumeroEncaissement.Substring(0, 4) == "ENFD"))
                    {
                        ActiverPanelPaiement();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                       "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActiverPanelPaiement()
        {
            dtpDateVersement.Enabled = true;
            txtMontantVerse.ReadOnly = true;
            txtNumeroPiece.ReadOnly = false;
            txtReferencePaiement.ReadOnly = false;
            txtCommentairePaiement.ReadOnly = false;
            cmbModePaiement.Enabled = true;
            cmdEnregistrerVersement.Enabled = true;
            bModifEncaissement = true;
        }


        private void SimulerAppelsDeFond(Option option,decimal prixDeVente, decimal montantPremierVersement, decimal tauxRemiseFraisDeDossier)
        {
            appelsDeFondSimules = new List<AppelDeFondSimule>();

            var appelDeFonds = contratRep.GetNiveauxAvancements().Where(na =>na.TypeContratId==option.TypeContratId && na.AppelFonds == true).OrderBy(na => na.ordre);
            var montantAVentiller = montantPremierVersement;
            //EXTRAIRE LES FRAIS DE DOSSIER DANS LE MONTANT GLOBAL

            if (chkFraisDossier.Checked)
            {
                decimal montantFraisDeDossier = 200000;
                if (chkRemiseFraisDeDossierAccordee.Checked)
                    montantFraisDeDossier -= montantFraisDeDossier * tauxRemiseFraisDeDossier / 100;
                appelsDeFondSimules.Add(new AppelDeFondSimule()
                {
                    Niveau = "Frais de dossier",
                    NiveauDecaissement = "0%",
                    Montant = montantFraisDeDossier,
                    Encaissé = montantFraisDeDossier,
                    Restant = 0

                });
                montantAVentiller -= montantFraisDeDossier;
            }

            foreach (var adf in appelDeFonds)
            {
                if (montantAVentiller > 0)
                {

                    decimal montantRestantNiveau = 0;
                    var resteAEncaisser = prixDeVente * adf.TauxDecaissement / 100;
                    decimal montantAEncaisser = 0;

                    if (montantAVentiller >= resteAEncaisser)
                    {
                        montantAEncaisser = resteAEncaisser;
                        montantRestantNiveau = 0;
                    }
                    else
                    {
                        montantAEncaisser = montantAVentiller;
                        montantRestantNiveau = resteAEncaisser - montantAVentiller;
                    }

                    //    var montantEncaisseNiveau = 0;


                    appelsDeFondSimules.Add(new AppelDeFondSimule()
                    {
                        Niveau = adf.LibelleCommercial,
                        NiveauDecaissement = (adf.TauxDecaissement / 100).ToString("#.#%"),
                        Montant = resteAEncaisser,
                        Encaissé = montantAEncaisser,
                        Restant = montantRestantNiveau,
                        Ordre=adf.ordre

                    });
                    montantAVentiller -= montantAEncaisser;
                }
                else
                {
                    var resteAEncaisser = prixDeVente * adf.TauxDecaissement / 100;
                    appelsDeFondSimules.Add(new AppelDeFondSimule()
                    {
                        Niveau = adf.LibelleCommercial,
                        NiveauDecaissement = (adf.TauxDecaissement / 100).ToString("#.#%"),
                        Montant = resteAEncaisser,
                        Encaissé = 0,
                        Restant = resteAEncaisser,
                        Ordre = adf.ordre

                    });
                }

            }

            //////dgEncaissements.DataSource = appelsDeFondSimules.ToList();
            //////dgEncaissements.Columns[0].Width = 135;
            //////dgEncaissements.Columns[1].HeaderText = "Taux";
            //////dgEncaissements.Columns[1].Width = 50;
            //////dgEncaissements.Columns[1].DefaultCellStyle.Format = "###";
            //////dgEncaissements.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //////dgEncaissements.Columns[2].DefaultCellStyle.Format = "### ### ###";
            //////dgEncaissements.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //////dgEncaissements.Columns[2].Width = 80;
            //////dgEncaissements.Columns[3].DefaultCellStyle.Format = "### ### ###";
            //////dgEncaissements.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //////dgEncaissements.Columns[3].Width = 80;
            //////dgEncaissements.Columns[4].DefaultCellStyle.Format = "### ### ###";
            //////dgEncaissements.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //////dgEncaissements.Columns[4].Width = 80;


        }

        private void txtNumeroLotRecherche_Validated(object sender, EventArgs e)
        {
            bRechercheParNumeroLot = true;
            EffacerForm();
            EffacerPaiement();
            dgEncaissements.DataSource = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                //var client = (Client)cmbClients.SelectedItem;
                //leClientEncours = clientRep.GetClient(client.ID);

                //if (leClientEncours != null)
                //{
                //    txtCommercial.Text = leClientEncours.Commercial != null ? leClientEncours.Commercial.NomComplet : "";

                //    txtAdresse.Text = leClientEncours.Adresse;
                //    txtNumeroMobile.Text = leClientEncours.Mobile1;
                //    txtNumeroFixe.Text = leClientEncours.TelephoneFixe;
                Lot leLot = new IlotRepository().FindLotByNumero(txtNumeroLotRecherche.Text.Trim());
                if(leLot!=null)
                { 
                    var leContratTrouve = contratRep.GetContratsLot(leLot);
                    if (leContratTrouve != null)
                    {
                        lbTypeClient.Text = "Client";
                        //var client = (Client)cmbClients.SelectedItem;
                        leClientEncours = leContratTrouve.Client;
                        leContratEnCours = leContratTrouve;

                        if (leClientEncours != null)
                        {
                            //foreach (var item in cmbClients.Items)
                            //{
                            //    if(((Client)item).ID== leClientEncours.ID)
                            //        cmbClients.SelectedItem= item;
                            //}
                           // =cmbClients.Items
                            txtCommercial.Text = leClientEncours.Commercial != null ? leClientEncours.Commercial.NomComplet : "";

                            txtAdresse.Text = leClientEncours.Adresse;
                            txtNumeroMobile.Text = leClientEncours.Mobile1;
                            txtNumeroFixe.Text = leClientEncours.TelephoneFixe;
                            txtNomCompletClient.Text = leClientEncours.NomComplet;
                            txtNumeroDossierRecherche.Text = leContratEnCours.NumeroContrat;
                        }
                        else
                            return;

                        tcEntete.SelectedTab = tcEntete.TabPages[1];
                       
                        if (leContratEnCours != null)
                        {
                            AfficherContrat(leContratTrouve);
                            cmdDossierClient.Visible = true;
                        }
                        dgComptesClientTrouves.DataSource = null;
                        cmdListerContrat.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:... " + ex.Message,
                                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtNumeroDossierRecherche_Validated(object sender, EventArgs e)
        {

            //EffacerForm();
            //EffacerPaiement();
            //dgEncaissements.DataSource = null;

            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    //var client = (Client)cmbClients.SelectedItem;
            //    //leClientEncours = clientRep.GetClient(client.ID);

            //    //if (leClientEncours != null)
            //    //{
            //    //    txtCommercial.Text = leClientEncours.Commercial != null ? leClientEncours.Commercial.NomComplet : "";

            //    //    txtAdresse.Text = leClientEncours.Adresse;
            //    //    txtNumeroMobile.Text = leClientEncours.Mobile1;
            //    //    txtNumeroFixe.Text = leClientEncours.TelephoneFixe;
            //    Contrat leContratTrouve = contratRep.GetContratByNumero(txtNumeroDossierRecherche.Text.Trim());
            //    if (leContratTrouve != null)
            //    {
            //        leClientEncours = leContratTrouve.Client;
            //        if (leClientEncours != null)
            //        {
            //            lbTypeClient.Text = "Client";
            //            //var client = (Client)cmbClients.SelectedItem;
            //            leClientEncours = leContratTrouve.Client;

            //            if (leClientEncours != null)
            //            {
            //                //foreach (var item in cmbClients.Items)
            //                //{
            //                //    if (((Client)item).ID == leClientEncours.ID)
            //                //        cmbClients.SelectedItem = item;
            //                //}
            //                // =cmbClients.Items
            //                txtCommercial.Text = leClientEncours.Commercial != null ? leClientEncours.Commercial.NomComplet : "";

            //                txtAdresse.Text = leClientEncours.Adresse;
            //                txtNumeroMobile.Text = leClientEncours.Mobile1;
            //                txtNumeroFixe.Text = leClientEncours.TelephoneFixe;
            //                txtNomCompletClient.Text = leClientEncours.NomComplet;
            //            }
            //            else
            //                return;

            //            tcEntete.SelectedTab = tcEntete.TabPages[1];

            //            if (leContratEnCours != null)
            //            {
            //                AfficherContrat(leContratTrouve);
            //                cmdDossierClient.Visible = true;
            //            }
            //            dgComptesClientTrouves.DataSource = null;
            //            cmdListerContrat.Visible = false;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(this, "Erreur:... " + ex.Message,
            //                          "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void lettrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (leClientEncours.Type == TypeClient.Client || leClientEncours.Type == TypeClient.ClientEnCours)
                {
                    EncaissementGlobalEnCours = null;
                    if (dgEncaissementsGlobals.SelectedRows.Count > 0)
                    {
                        int encaissementGlobalId = (int)dgEncaissementsGlobals.SelectedRows[0].Cells[0].Value;
                        contratRep.LettrerEncaissement(encaissementGlobalId, leContratEnCours.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:... " + ex.Message,
                                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtNomComplet_TextChanged(object sender, EventArgs e)
        {
            var AllClients = AllClientsInitial;
            if (txtNomCompletClient.Text.Length >= 3 && !bRechercheParNumeroLot)
            {
                lvFiltreClients.Items.Clear();
                lvFiltreClients.Visible = true;
                lvFiltreClients.BringToFront();
                AllClients = AllClients.Where(client => client.NomComplet.ToLower().Contains(txtNomCompletClient.Text.ToLower())).OrderBy(cl=>cl.Nom).ThenBy(cl => cl.Prenom).ToList();
                foreach (var client in AllClients)
                {
                    ListViewItem lviClient = new ListViewItem(client.NomComplet.Trim());
                    lviClient.Tag = client;
                    lvFiltreClients.Items.Add(lviClient);
                }
            }
            else
            {
                lvFiltreClients.Visible = false;
            }
            bRechercheParNumeroLot = false;
        }

        private void lvFiltreClients_DoubleClick(object sender, EventArgs e)
        {
            if(lvFiltreClients.SelectedItems.Count >0)
            {

                leClientEncours = clientRep.GetClient((lvFiltreClients.SelectedItems[0].Tag as Client).ID);
                SelectionnerClient();


            }
        }
        private void SelectionnerClient()
        {
            txtNomCompletClient.Text = leClientEncours.NomComplet;
            lvFiltreClients.Visible = false;
            txtNumeroLotRecherche.Text = string.Empty;
            txtNumeroDossierRecherche.Text = string.Empty;
            EffacerForm();
            EffacerPaiement();
            dgEncaissements.DataSource = null;

            try
            {
                //if (cmbClients.SelectedItem != null)
                //{
                //    var client = (Client)cmbClients.SelectedItem;
                //    leClientEncours = clientRep.GetClient(client.ID);
                if (leClientEncours != null)
                {
                    txtCommercial.Text = leClientEncours.Commercial != null ? leClientEncours.Commercial.NomComplet : "";

                    txtAdresse.Text = leClientEncours.Adresse;
                    txtNumeroMobile.Text = leClientEncours.Mobile1;
                    txtNumeroFixe.Text = leClientEncours.TelephoneFixe;

                    lesContratTrouves = leClientEncours.Contrats.Where(c => c.Statut == StatutContrat.Actif).ToList();
                    if (lesContratTrouves.Count() > 0)
                    {
                        lbTypeClient.Text = "Client";
                        if (lesContratTrouves.Count() == 1)
                        {
                            tcEntete.SelectedTab = tcEntete.TabPages[1];
                            leContratEnCours = lesContratTrouves.FirstOrDefault();
                            if (leContratEnCours != null)
                            {
                                txtNumeroDossierRecherche.Text = leContratEnCours.NumeroContrat;
                                if (leContratEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                                {
                                    txtNumeroLotRecherche.Text = leContratEnCours.Lot.NumeroLot;
                                }
                                AfficherContrat(leContratEnCours);
                                cmdDossierClient.Visible = true;
                            }
                            dgComptesClientTrouves.DataSource = null;
                            cmdListerContrat.Visible = false;
                        }
                        else
                        {
                            dgComptesClientTrouves.DataSource = lesContratTrouves.ToList().Select(
                                                        cont => new
                                                        {
                                                            ID = cont.Id,
                                                            TypeContrat = cont.TypeContrat.LibelleTypeContrat,
                                                            Numéro = cont.NumeroContrat,
                                                            Typevilla = cont.Lot.TypeVilla.CodeType,
                                                            PrixDeVente = cont.PrixFinal

                                                        }).ToList();
                            dgComptesClientTrouves.Columns[0].Width = 0;
                            dgComptesClientTrouves.Columns[0].Visible = false;

                            dgComptesClientTrouves.Columns[1].Width = 100;
                            dgComptesClientTrouves.Columns[2].Width = 150;
                            dgComptesClientTrouves.Columns[3].Width = 50;
                            dgComptesClientTrouves.Columns[4].Width = 80;
                            dgComptesClientTrouves.Columns[4].DefaultCellStyle.Format = "### ### ###";
                            dgComptesClientTrouves.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            tcEntete.SelectedTab = tcEntete.TabPages[0];
                            pCompteClient.Visible = false;
                            //cmdListerContrat.Visible = true;
                            //pGridContrats.Visible = true;
                        }
                    }
                    else //Le client est prospect
                    {
                        AfficherCompteProspect(leClientEncours);
                        AfficherEncaissementsProspect(leClientEncours.ID);
                        tcEntete.SelectedTab = tcEntete.TabPages[2];
                        cmdDossierClient.Visible = false;

                        pCompteClient.Visible = true;
                        lbTypeClient.Text = "Prospect";
                    }
                }
                //}
            }
            catch (Exception ex)
            {

                //MessageBox.Show(this, "Erreur:... " + ex.Message,
                //                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNumeroDossierRecherche_TextChanged(object sender, EventArgs e)
        {
        }

        private void dgComptesClientTrouves_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lvFiltreClients_Validated(object sender, EventArgs e)
        {
            lvFiltreClients.Visible = false;
        }

        private void lvFiltreClients_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                lvFiltreClients_DoubleClick(sender, e);
            }
            else
                if(e.KeyCode== Keys.Escape)
                {
                    lvFiltreClients.Visible = false;
                }
        }

        private void txtNomCompletClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                lvFiltreClients.Visible = false;
            }
        }

        private void cmsEncaissement_Opened(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (leClientEncours.Type == TypeClient.Client || leClientEncours.Type == TypeClient.ClientEnCours)
                {
                    EncaissementGlobalEnCours = null;
                    if (dgEncaissementsGlobals.SelectedRows.Count > 0)
                    {
                        int encaissementGlobalId = (int)dgEncaissementsGlobals.SelectedRows[0].Cells[0].Value;
                        var encaissement=contratRep.GetEncaissementGlobalById(encaissementGlobalId);
                        if(encaissement!=null)
                        {
                            if(encaissement.EncaissementLettre==true)
                            {
                                lettrerToolStripMenuItem.Enabled = false;
                            }
                        }
                        else
                        {
                            lettrerToolStripMenuItem.Enabled = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:... " + ex.Message,
                                      "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
