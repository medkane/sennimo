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
using prjSenImmoWinform.DAL;

namespace prjSenImmoWinform
{
    public partial class FrmDetailsRecouvrementDepot : Form
    {

        ContratRepository contratRep;
        private Contrat leContratEnCours;
        private bool bModifNote;
        private NoteContrat laNoteEnCours;

        public FrmDetailsRecouvrementDepot()
        {
            InitializeComponent();
            contratRep = new ContratRepository();
        }
        public FrmDetailsRecouvrementDepot(int contratId):this()
        {

            ChargerRecouvrement(contratId);
            AfficherNotesContrat(contratId);
        }

        public void ChargerRecouvrement(int contratId)
        {
            try
            {
                    leContratEnCours = contratRep.GetContratById(contratId);
                    var factures = leContratEnCours.Factures.Where(fact => fact.TypeFacture != TypeFacture.FraisDossier).ToList();

                    if (factures != null)
                    {
                        txtClient.Text = leContratEnCours.Client.NomComplet;
                        txtTypeVilla.Text = leContratEnCours.Lot.TypeVilla.CodeType;
                        txtPrixDeVente.Text = leContratEnCours.PrixFinal.ToString("### ### ###");
                        txtPeriodicite.Text = leContratEnCours.TypeEcheancier.Value.ToString();
                        txtTypeContrat.Text = leContratEnCours.TypeContrat.LibelleTypeContrat;
                        txtDureeDepot.Text = leContratEnCours.DureeDepot.ToString();
                        txtNbEcheances.Text = leContratEnCours.NbEcheances.Value.ToString();
                        txtMontantEcheance.Text = leContratEnCours.MontantEcheance.Value.ToString("### ### ###");
                        var depotMinimum = factures.Where(fact => fact.TypeFacture == TypeFacture.DepotMinimum).FirstOrDefault();
                        txtDepotMinimum.Text = depotMinimum.Montant.ToString("### ### ###");

                        txtMontantDepotMinimumEncaisse.Text = depotMinimum.Encaissements.Sum(enc => enc.Montant).ToString("### ### ###");

                        txtMontantDepotMinimumRestant.Text = (depotMinimum.Montant - depotMinimum.Encaissements.Sum(enc => enc.Montant)).ToString();

                        txtNbEcheancesEchues.Text = factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Count().ToString();
                        txtMontantEcheancesDu.Text = factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant).ToString("### ### ###");

                        txtNbEcheancesSoldees.Text = factures.Where(fact => fact.FacturePayee == true && fact.TypeFacture == TypeFacture.Echeance).Count().ToString();
                        txtMontantEcheancesEncaisse.Text = factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###"); ;
                        //factures.Where(fact => fact.Echue == true && && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant).ToString("### ### ###");

                        txtNbEcheancesNonSoldees.Text = factures.Where(fact => fact.Echue == true && fact.FacturePayee == false && fact.TypeFacture == TypeFacture.Echeance).Count().ToString();
                        txtMontantEcheancesRestant.Text = (factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Montant) -
                            factures.Where(fact => fact.TypeFacture == TypeFacture.Echeance).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");

                        txtMontantTotalDu.Text = factures.Where(fact => fact.Echue == true).Sum(fact => fact.Montant).ToString("### ### ###");
                        txtMontantTotalEncaisse.Text = factures.Where(fact => fact.Echue == true).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###");
                        txtMontantTotalRestant.Text = (factures.Where(fact => fact.Echue == true).Sum(fact => fact.Montant) - factures.Where(fact => fact.Echue == true).Sum(fact => fact.Encaissements.Sum(enc => enc.Montant))).ToString("### ### ###");
                        var echeancesEchues = factures.Where(fact => fact.Echue == true && fact.TypeFacture == TypeFacture.Echeance).ToList();
                        listView1.Items.Clear();
                        foreach (var echeance in echeancesEchues)
                        {
                        ListViewItem lviEchance = new ListViewItem(Tools.Tools.UppercaseWords( String.Format("{0:y}", echeance.DateEcheanceFacture.Value)));
                            lviEchance.SubItems.Add(echeance.DateEcheanceFacture.Value.ToShortDateString());
                            lviEchance.SubItems.Add(echeance.Montant.ToString("### ### ###"));
                            lviEchance.SubItems.Add(echeance.MontantEncaisse.ToString("### ### ###"));
                            lviEchance.SubItems.Add((echeance.Montant - echeance.MontantEncaisse).ToString("### ### ###"));
                            listView1.Items.Add(lviEchance);
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
                this.Cursor = Cursors.WaitCursor;
                GenererFactureRElanceDepot();
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
                bookmarkRange.Text = leContratEnCours.Client.Adresse + " - " + leContratEnCours.Client.Ville;

                if (leContratEnCours.Client.Email.Trim() != string.Empty)
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

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = leContratEnCours.Client.Pays;

                myBookmark = bookmarks["DateEdition"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Date.ToShortDateString();

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

                    row.Cells[1].Range.Text = "Dépôt minimum " + leContratEnCours.TypeContrat.SeuilSouscription + "%";
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


                    row.Cells[4].Range.Text = (depotMinimum.Montant - depotMinimum.Encaissements.Sum(enc => enc.Montant)) == 0 ? "0" : (depotMinimum.Montant - depotMinimum.Encaissements.Sum(enc => enc.Montant)).ToString("### ### ###");
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

                    row.Cells[2].Range.Text = leContratEnCours.Factures.Where(fact => fact.Echue == true && fact.TypeFacture != TypeFacture.FraisDossier).Sum(fact => fact.Montant).ToString("### ### ###");
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

                myBookmark = bookmarks["DateEcheance"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DateTime.Now.Date.AddDays(35).Date.ToShortDateString();

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

        private void cmdAjouterNote_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtNote.Text != string.Empty)
                {
                    if(!bModifNote)
                        contratRep.AjouterNote(leContratEnCours.Id,  txtNote.Text);
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
                var noteContrats = contratRep.GetNotesContrat(idContrat).OrderByDescending(note => note.DateNote);

                lvNotes.Items.Clear();
                int i = 0;
                foreach (var note in noteContrats.ToList())
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
