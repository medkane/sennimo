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
    public partial class FrmDetailsRecouvrementResa : Form
    {
        ContratRepository contratRep;
        private Contrat leContratEnCours;
        private bool bModifNote;
        private NoteContrat laNoteEnCours;

        public FrmDetailsRecouvrementResa()
        {
            InitializeComponent();
            contratRep = new ContratRepository();
        }
        public FrmDetailsRecouvrementResa(int contratId):this()
        {
           
            ChargerRecouvrement(contratId);
            AfficherNotesContrat(contratId);
        }


        private void ChargerRecouvrement(int contratId)
        {
            try
            {
                leContratEnCours = contratRep.GetContratById(contratId);
                if (leContratEnCours!=null)
                {
                    //int contratId = (int)lvRecouvrementResa.SelectedItems[0].Tag;
                    txtClient.Text = leContratEnCours.Client.NomComplet;
                    txtCompteTiers.Text = leContratEnCours.Client.CompteTiers;
                    txtLot.Text = leContratEnCours.Lot.NumeroLot;
                    txtTypeVilla.Text = leContratEnCours.Lot.TypeVilla.CodeType;
                    txtPrixDeVente.Text = leContratEnCours.PrixFinal.ToString("### ### ##0");

                    var factures = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true).ToList()
                           .Select(fact => new
                           {
                               id = fact.Id,
                               Libellé = fact.Motif,
                               Taux = fact.EtatAvancement.TypeEtatAvancement.TauxDecaissement.ToString("###") + "%",
                               Montant = fact.Montant,
                               Encaissé = fact.Encaissements.Sum(enc => enc.Montant),
                               Restant = (fact.Montant - fact.Encaissements.Sum(enc => enc.Montant)),
                               Ordre=fact.EtatAvancement.TypeEtatAvancement.ordre
                           }).ToList();
                    lvAppelsDeFonds.Items.Clear();
                    foreach (var facture in factures)
                    {
                        ListViewItem lviContrat = new ListViewItem(facture.Libellé);

                        string taux;
                        //if(leContratEnCours.ProjetId==1)
                        //if (facture.Ordre == 2)
                        //{
                        //    //tc = contratRep.GetTypeContrat contrat.TypeContratID);
                        //    taux = (leContratEnCours.TypeContrat.SeuilSouscription - 5).ToString("###") + "%";
                        //}
                        //else
                        //if (facture.Ordre == 26)
                        //{
                        //    //tc = DB.TypeContrats.Find(contrat.TypeContratID);
                        //    taux = (70 - leContratEnCours.TypeContrat.SeuilSouscription).ToString("###") + "%";

                        //}
                        //else
                        //{
                        //    taux = facture.Taux;
                        //}
                        //else
                            taux = facture.Taux;
                        lviContrat.SubItems.Add(taux);

                        lviContrat.SubItems.Add(facture.Montant.ToString("### ### ##0"));
                        lviContrat.SubItems.Add(facture.Encaissé.ToString("### ### ##0"));
                        lviContrat.SubItems.Add(facture.Restant.ToString("### ### ##0"));
                        lvAppelsDeFonds.Items.Add(lviContrat);
                    }

                    txtTotalFactureClient.Text = factures.Sum(fact => fact.Montant).ToString("### ### ### ##0");
                    txtTotalEncaisseClient.Text = factures.Sum(fact => fact.Encaissé).ToString("### ### ### ##0");
                    txtTotalRestantClient.Text = factures.Sum(fact => fact.Restant).ToString("### ### ### ##0");
                    //    dgHistoriqueAppelDeFonds.DataSource = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier && fact.Active == true && fact.Echue == true).ToList()
                    //       .Select(fact => new
                    //       {
                    //           id = fact.Id,
                    //           Libellé = fact.Motif,
                    //           Taux = fact.EtatAvancement.TypeEtatAvancement.TauxDecaissement.ToString("###") + "%",
                    //           Montant = fact.Montant.ToString("### ### ##0"),
                    //           Encaissé = fact.Encaissements.Sum(enc => enc.Montant).ToString("### ### ##0"),
                    //           Restant = (fact.Montant - fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ##0")
                    //       }).ToList();

                    //dgHistoriqueAppelDeFonds.Columns[0].Width = 0;
                    //dgHistoriqueAppelDeFonds.Columns[0].Visible = false;
                    //dgHistoriqueAppelDeFonds.Columns[1].Width = 150;

                    //dgHistoriqueAppelDeFonds.Columns[2].Width = 50;
                    //dgHistoriqueAppelDeFonds.Columns[2].DefaultCellStyle.Format = "###";
                    //dgHistoriqueAppelDeFonds.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                    //dgHistoriqueAppelDeFonds.Columns[3].Width = 80;
                    //dgHistoriqueAppelDeFonds.Columns[3].DefaultCellStyle.Format = "### ### ##0";
                    //dgHistoriqueAppelDeFonds.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    //dgHistoriqueAppelDeFonds.Columns[4].Width = 80;
                    //dgHistoriqueAppelDeFonds.Columns[4].DefaultCellStyle.Format = "### ### ##0";
                    //dgHistoriqueAppelDeFonds.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    //dgHistoriqueAppelDeFonds.Columns[5].Width = 80;
                    //dgHistoriqueAppelDeFonds.Columns[5].DefaultCellStyle.Format = "### ### ##0";
                    //dgHistoriqueAppelDeFonds.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    var appelDeFonds = contratRep.GetDernierAppelsDeFonds(contratId);
                    if (appelDeFonds != null)
                    {
                        txtDateAppelDeFonds.Text = appelDeFonds.DateEcheanceFacture.Value.ToShortDateString();
                        //txtClient.Text = appelDeFonds.Client.NomComplet;
                        //txtCompteTiers.Text = appelDeFonds.Client.CompteTiers;
                        //txtLot.Text = appelDeFonds.Contrat.Lot.NumeroLot;
                        //txtTypeVilla.Text = appelDeFonds.Contrat.Lot.TypeVilla.CodeType;
                        //txtPrixDeVente.Text = appelDeFonds.Contrat.PrixFinal.ToString("### ### ##0");
                        txtTauxNiveauAppelDeFonds.Text = appelDeFonds.EtatAvancement.TypeEtatAvancement.TauxDecaissement.ToString("###") + "%";
                        txtNiveauAppelDeFond.Text = appelDeFonds.EtatAvancement.TypeEtatAvancement.LibelleCommercial;
                        txtMontantAppelDeFonds.Text = appelDeFonds.Montant.ToString("### ### ##0");
                        txtMontantEncaisséAppelDeFons.Text = appelDeFonds.Encaissements.Sum(enc => enc.Montant).ToString("### ### ##0");
                        txtMontantREstantAppelDeFonds.Text = (appelDeFonds.Montant - appelDeFonds.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ##0");
                        chkCourrierEnvoye.Checked = appelDeFonds.CourrierEnvoye;
                        chkFactureGeneree.Checked = appelDeFonds.FactureGenere;



                    }
                    else
                    {
                        EffacerPanelAppelDeFond();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EffacerPanelAppelDeFond()
        {
            txtDateAppelDeFonds.Text = string.Empty;
            //txtClient.Text = string.Empty;
            //txtCompteTiers.Text = string.Empty;
            //txtLot.Text = string.Empty;
            //txtTypeVilla.Text = string.Empty;
            txtTauxNiveauAppelDeFonds.Text = string.Empty;
            txtNiveauAppelDeFond.Text = string.Empty;
            txtMontantAppelDeFonds.Text = string.Empty;
            txtMontantEncaisséAppelDeFons.Text = string.Empty;
            txtMontantREstantAppelDeFonds.Text = string.Empty;
            //txtPrixDeVente.Text = string.Empty;
            chkFactureGeneree.Checked = false;

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


        private void GenereFactureAppelsDeFond(Object sender)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                if (sender == cmdCourrierFacture)
                    msWord.Visible = false; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                else
                    msWord.Visible = true;

                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template

                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "FactureAppelDeFonds.dotx";

                leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);
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

                    rowTotal.Cells[1].Range.Text = "Montant de l’appel de fonds \"" + appelDeFonds.EtatAvancement.TypeEtatAvancement.LibelleCommercial + "\" exigible le " + DateTime.Now.Date.ToShortDateString();
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

        private void cmdImprimerFactureRelance_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                GenereLettreRelanceResa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                       "Prosopis -  Gestion du recouvrement", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
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
                //msWord.Activate();
                //doc.Activate();
                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;

                //myBookmark = bookmarks["DateFactureRelance"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text =DateTime.Now.Date.ToShortDateString();
                leContratEnCours = contratRep.GetContratById(leContratEnCours.Id);
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

                if(leContratEnCours.Client.Email.Trim()!=string.Empty)
                { 
                    myBookmark = bookmarks["Email"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = leContratEnCours.Client.Email;
                }
                else
                {
                    myBookmark = bookmarks["Email"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "";

                }

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

                        row.Cells[2].Range.Text = appelDeFond.Montant != 0 ? appelDeFond.Montant.ToString("### ### ###") : "0";
                        row.Cells[2].WordWrap = true;
                        row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[2].Range.Bold = 0;


                        row.Cells[3].Range.Text = appelDeFond.Encaissements.Sum(enc => enc.Montant) != 0 ? appelDeFond.Encaissements.Sum(enc => enc.Montant).ToString("### ### ###") : "0";
                        row.Cells[3].WordWrap = true;
                        row.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[3].Range.Bold = 0;

                        row.Cells[4].Range.Text = (appelDeFond.Montant - appelDeFond.Encaissements.Sum(enc => enc.Montant)) != 0 ? (appelDeFond.Montant - appelDeFond.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###") : "0";
                        row.Cells[4].WordWrap = true;
                        row.Cells[4].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[4].Range.Bold = 0;
                    }

                    Microsoft.Office.Interop.Word.Row rowTotal = table.Rows.Add(ref missing);

                    rowTotal.Cells[1].Range.Text = "Total";
                    rowTotal.Cells[1].WordWrap = true;
                    rowTotal.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[1].Range.Bold = 1;

                    rowTotal.Cells[2].Range.Text = lesAppelsDeFonds.Sum(fact => fact.Montant).ToString("### ### ###");
                    rowTotal.Cells[2].WordWrap = true;
                    rowTotal.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[2].Range.Bold = 1;


                    rowTotal.Cells[3].Range.Text = lesAppelsDeFonds.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###");
                    rowTotal.Cells[3].WordWrap = true;
                    rowTotal.Cells[3].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    rowTotal.Cells[3].Range.Bold = 1;

                    rowTotal.Cells[4].Range.Text = (lesAppelsDeFonds.Sum(fact => fact.Montant) - lesAppelsDeFonds.Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
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

        private void chkFactureGeneree_CheckedChanged(object sender, EventArgs e)
        {
            var appelDeFonds = contratRep.GetDernierAppelsDeFonds(leContratEnCours.Id);
            if (appelDeFonds != null)
                contratRep.MAJFactureGeneree(appelDeFonds.Id);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cmdAjouterNote_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtNote.Text != string.Empty)
                {
                    if (!bModifNote)
                        contratRep.AjouterNote(leContratEnCours.Id, txtNote.Text);
                    else
                        contratRep.ModifierNote(laNoteEnCours.ID, txtNote.Text);
                    AfficherNotesContrat(leContratEnCours.Id);
                    txtNote.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                  "Prosopis - Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bModifNote = false;
            }
        }

        private void AfficherNotesContrat(int idContrat)
        {
            try
            {
                var noteProspects = contratRep.GetNotesContrat(idContrat).OrderByDescending(note => note.DateNote);

                lvNotes.Items.Clear();
                int i = 0;
                foreach (var note in noteProspects.ToList())
                {
                    ListViewItem lviNote = new ListViewItem(note.DateNote.Value.ToShortDateString());
                    lviNote.SubItems.Add(note.Note);
                    lviNote.Tag = note;
                    lvNotes.Items.Add(lviNote);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (lvNotes.SelectedItems.Count > 0)
                {
                    bModifNote = true;
                    laNoteEnCours = (NoteContrat)lvNotes.SelectedItems[0].Tag;
                    txtNote.Text = laNoteEnCours.Note;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(this, "Souhaitez vous réellement supprimer cette note?", "Prosopis - Gestion des activités commerciales", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    contratRep.SupprimerNote(laNoteEnCours.ID);
                    AfficherNotesContrat(leContratEnCours.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void lvNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (lvNotes.SelectedItems.Count > 0)
                {
                    laNoteEnCours = (NoteContrat)lvNotes.SelectedItems[0].Tag;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
