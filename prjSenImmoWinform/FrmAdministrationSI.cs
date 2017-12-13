using prjSenImmoWinform.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace prjSenImmoWinform
{
    public partial class FrmAdministrationSI : Form
    {
        Contrat leContratEnCours;
        List<Lot> ListeDesLotsALiberer;
        Boolean bImporterLot = false;
        public FrmAdministrationSI()
        {
            InitializeComponent();
            ListeDesLotsALiberer = new List<Lot>();
        }

        private void cmdRechercherContrat_Click(object sender, EventArgs e)
        {
            if (txtNumeroDossier.Text == string.Empty)
            {
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (var scope = new TransactionScope())
                {
                    Contrat contrat = null;

                    using (var db = new SenImmoDataContext())
                    {
                        contrat = db.Contrats.Where(cont => cont.NumeroContrat == txtNumeroDossier.Text
                         && (cont.Statut == StatutContrat.Actif || cont.Statut == StatutContrat.Cloturé)).SingleOrDefault();
                        //contrat = db.Contrats.Find(2003);
                        if (contrat!=null)
                        { 
                            txtTypeContrat.Text = contrat.TypeContrat.LibelleTypeContrat;
                            txtClient.Text = contrat.Client.NomComplet;
                            txtTotalEncaisse.Text = contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ###");
                            txtTypeVilla.Text = contrat.Lot.TypeVilla.NomComplet;
                            txtLot.Text = contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation ? contrat.Lot.NumeroLot : contrat.LotAttribue==true?contrat.Lot.NumeroLot:"";
                            txtPrixDeVente.Text=contrat.PrixFinal.ToString("### ### ###");
                            leContratEnCours = contrat;
                            cmdSupprimerContrat.Enabled = true;
                            if(contrat.TypeContrat.CategorieContrat== CategorieContrat.Dépôt)
                            { 
                                cmdChangementLot.Visible = true;
                                lbNouveauLot.Visible = true;
                                txtNouveauLot.Visible = true;
                            }
                            else
                            {
                                cmdChangementLot.Visible = false;
                                lbNouveauLot.Visible = false;
                                txtNouveauLot.Visible = false;
                            }
                        }
                    }
                    scope.Complete();

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

        

     
        private void cmdSupprimerContrat_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(this, "Voulez vous réellement supprimer le contrat " + leContratEnCours.NumeroContrat + "?",
                   "Prosopis - Scripts d'administration", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (MessageBox.Show(this, "Confirmer vous vraiment vouloir supprimer le contrat " + leContratEnCours.NumeroContrat + "?",
                         "Prosopis - Scripts d'administration", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        using (var scope = new TransactionScope())
                        {
                            using (var db = new SenImmoDataContext())
                            {
                                // Rectrouver le contrat et le client
                                var contrat = db.Contrats.Where(cont => cont.NumeroContrat == txtNumeroDossier.Text && cont.Statut == StatutContrat.Actif).SingleOrDefault();
                                //var contrat = db.Contrats.Find(2003);
                                var client = db.Clients.Find(contrat.ClientID);
                                //Suppression des encaissements
                                List<int> listeEncaissementGlobalIDs = new List<int>();
                                foreach (var encG in contrat.EncaissementGlobals)
                                {
                                    listeEncaissementGlobalIDs.Add(encG.ID);
                                }
                                var encaissements = db.Encaissements.Where(enc => listeEncaissementGlobalIDs.Contains(enc.EncaissementGlobalId.Value));


                                db.Encaissements.RemoveRange(encaissements);


                                //Transferer les encaissements post contrat dans le compte prospect

                                //var encaissementGlobal = new EncaissementGlobal()
                                //{
                                //    NumeroEncaissement = encaissementProspect.NumeroEncaissement,
                                //    DateEncaissement = encaissementProspect.DateEncaissement.Value.Date,
                                //    MontantGlobal = encaissementProspect.MontantGlobal,
                                //    ContratId = contrat.Id,
                                //    ModePaiement = encaissementProspect.ModePaiement,
                                //    Commentaire = encaissementProspect.Commentaire,
                                //    ReferencePaiement = encaissementProspect.ReferencePaiement

                                //};
                                foreach (var encG in contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD"))
                                {
                                    var encProspectCorrespondant = db.EncaissementProspects.FirstOrDefault(encPros => encPros.NumeroEncaissement == encG.NumeroEncaissement
                                                                   && encPros.DateEncaissement == encG.DateEncaissement
                                                                   && encPros.MontantGlobal == encPros.MontantGlobal);
                                    if (encProspectCorrespondant == null)
                                    {
                                        //Créer un encaissement prospect
                                        var encaissementATransferer = new EncaissementProspect()
                                        {
                                            NumeroEncaissement = encG.NumeroEncaissement,
                                            DateEncaissement = encG.DateEncaissement,
                                            MontantGlobal = encG.MontantGlobal,
                                            ProspectId = encG.Contrat.ClientID,
                                            ModePaiement = encG.ModePaiement,
                                            ReferencePaiement = encG.ReferencePaiement,
                                            Commentaire = encG.Commentaire
                                        };
                                        db.EncaissementProspects.Add(encaissementATransferer);
                                    }
                                }

                                //Suppression des encaissements globaux
                                db.EncaissementGlobals.RemoveRange(contrat.EncaissementGlobals);

                                //Suppression des factures
                                db.Factures.RemoveRange(contrat.Factures);

                                //Suppression des factures commissions apportteur d'affaire
                                db.FactureCommissions.RemoveRange(contrat.FactureCommissions);

                                //Mise à jour du statut du client à prospect avec option depot
                                if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                                {
                                    client.Type = TypeClient.ProspectAvecOptionDepot;
                                }
                                else
                                {
                                    client.Type = TypeClient.ProspectAvecOptionResa;
                                    client.Options.Where(opt => opt.Active == true).SingleOrDefault().Lot.StatutLot = StatutLot.Option;
                                }


                                //Mettre à jour l'option pour redéclencher le seuil d'encaissement
                                client.Options.Where(opt => opt.Active == true).SingleOrDefault().ContratGenere = false;
                                //client.Options.Where(opt => opt.Active == true).SingleOrDefault().SeuilContratAtteint = false;
                                //Libération frais de dossier
                                foreach (var encPros in client.EncaissementProspects)
                                {
                                    encPros.Deverse = false;
                                }
                                //libérer le lot en question
                                //if(contrat.TypeContrat.CategorieContrat== CategorieContrat.Réservation
                                //    || (contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation && contrat.LotAttribue == true))
                                //{

                                //}
                                if (contrat.Lot != null)
                                    contrat.Lot.StatutLot = StatutLot.Libre;
                                db.Contrats.Remove(contrat);
                                db.SaveChanges();
                            }
                            scope.Complete();
                        }
                        MessageBox.Show(this, "Opération terminée avec succes!!!", "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        EffacerFormContrat();
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

        private void EffacerFormContrat()
        {
            txtTypeContrat.Text = string.Empty;
            txtClient.Text = string.Empty;
            txtTotalEncaisse.Text = string.Empty;
            txtTypeVilla.Text = string.Empty;
            txtLot.Text = string.Empty;
            txtPrixDeVente.Text = string.Empty;
            txtNouveauLot.Text = string.Empty;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdAjouter_Click(object sender, EventArgs e)
        {
            if (!bImporterLot)
            {
                if (txtNumeroLotLiberation.Text == string.Empty)
                    return;
                Lot lot;
                using (var db = new SenImmoDataContext())
                {
                    lot = db.Lots.Where(lt => lt.NumeroLot == txtNumeroLotLiberation.Text).FirstOrDefault();
                }
                if(lot==null)
                {
                    MessageBox.Show(this, "Erreur:... le lot " + txtNumeroLotLiberation.Text + " n'existe pas dans la base!!!", "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ListeDesLotsALiberer.Add(lot);
                txtNumeroLotLiberation.Text = string.Empty;
                lvListLotsAliberer.Items.Clear();
                ChargerLesLots();
            }
            else
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    lvListLotsAliberer.Items.Clear();
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
                    xlWorkBook = xlApp.Workbooks.Open( txtNumeroLotLiberation.Text, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                    //excelApp.Workbooks.Open(workbookPath,                                                 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows                       , "",  true, false, 0, true, false, false);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                   
                    Excel.Range myRange = xlWorkSheet.get_Range("A:A", Type.Missing);
                    using (var db = new SenImmoDataContext())
                    {
                        //Charger les lots dans une liste
                        foreach (Excel.Range r in myRange)
                        {
                            if (r.Text != string.Empty)
                            {
                                string numeroLot = r.Text;
                                var lot = db.Lots.Where(ltLot => ltLot.NumeroLot== numeroLot).FirstOrDefault();
                                if (lot != null)
                                {
                                    ListeDesLotsALiberer.Add(lot);
                                }
                                else
                                {
                                    MessageBox.Show(this, "Erreur:... le lot " + numeroLot + " n'existe pas dans la base!!!", "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                                break;
                        }
                        //Afficher la liste des lots dans la listview
                        lvListLotsAliberer.Items.Clear();
                        foreach (Lot item in ListeDesLotsALiberer)
                        {
                            var lot = db.Lots.Find(item.ID);
                            if (lot != null)
                            {
                                var lviLot=new ListViewItem(lot.NumeroLot);
                                lviLot.SubItems.Add(lot.StatutLot.ToString());
                                lvListLotsAliberer.Items.Add(lviLot);
                            }
                            else
                            {
                                MessageBox.Show(this, "Erreur:... le lot " + lot + " n'existe pas dans la base!!!", "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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

            cmdSupprimer.Enabled = true;
            cmdLibererLot.Enabled = true;
            cmdReserverLot.Enabled = true;
        }

        private void ChargerLesLots()
        {
            lvListLotsAliberer.Items.Clear();
            foreach (Lot item in ListeDesLotsALiberer)
            {
                var lviLot = new ListViewItem(item.NumeroLot);
                lviLot.SubItems.Add(item.StatutLot.ToString());
                lvListLotsAliberer.Items.Add(lviLot);
            }
        }

        private void lvListLotsAliberer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdParcourir_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                DialogResult result = ofdFichierSource.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    this.Cursor = Cursors.WaitCursor;
                    string file = ofdFichierSource.FileName;
                    this.Cursor = Cursors.WaitCursor;
                    txtNumeroLotLiberation.Text = file;
                    cmdAjouterLot.Enabled = true;
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

        private void rbParLot_CheckedChanged(object sender, EventArgs e)
        {
            if (rbParLot.Checked)
            {
                bImporterLot = false;
                cmdParcourir.Enabled = false;
                txtNumeroLotLiberation.ReadOnly = false;
                txtNumeroLotLiberation.Text = string.Empty;
            }
        }

        private void rbParImportation_CheckedChanged(object sender, EventArgs e)
        {
            if (rbParImportation.Checked)
            {
                bImporterLot = true;
                cmdParcourir.Enabled = true;
                txtNumeroLotLiberation.ReadOnly = true;
                txtNumeroLotLiberation.Text = string.Empty;
                cmdAjouterLot.Enabled = false;
            }
        }

        private void cmdSupprimer_Click(object sender, EventArgs e)
        {
            ListeDesLotsALiberer.Clear();
            lvListLotsAliberer.Items.Clear();
            cmdSupprimer.Enabled = false;
        }

        private void txtNumeroLotLiberation_TextChanged(object sender, EventArgs e)
        {
            if(txtNumeroLotLiberation.Text!=string.Empty)
            {
                cmdAjouterLot.Enabled = true;
            }
            else
                cmdAjouterLot.Enabled = true;
        }

        private void cmdReserver_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ChangerStatutLots(StatutLot.Reserve);
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

        private void ChangerStatutLots(StatutLot statut)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var db = new SenImmoDataContext())
                    {
                        foreach (Lot item in ListeDesLotsALiberer)
                        {
                            var lot = db.Lots.Find(item.ID);
                            if (lot != null)
                                lot.StatutLot = statut;
                            else
                            {
                                MessageBox.Show(this, "Erreur:... le lot " + lot + " n'existe pas dans la base!!!", "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        db.SaveChanges();
                        ChargerLesLots();
                    }
                    scope.Complete();
                }
                MessageBox.Show(this, "Opération terminée avec succes!!!", "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public class LotMono
        {
            public string NumeroLot { get; set; }
            public StatutLot Statut { get; set; }
        }

        private void cmdLibererLot_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ChangerStatutLots(StatutLot.Libre);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdChangementLot_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(this, "Voulez vous réellement modifier le lot attribué à ce contrat de dépôt" + leContratEnCours.NumeroContrat + "?",
                   "Prosopis - Scripts d'administration", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    using (var scope = new TransactionScope())
                    {
                        Contrat contrat = null;

                        using (var db = new SenImmoDataContext())
                        {
                            contrat = db.Contrats.Where(cont => cont.NumeroContrat == txtNumeroDossier.Text
                             && (cont.Statut == StatutContrat.Actif || cont.Statut == StatutContrat.Cloturé)).SingleOrDefault();
                            //contrat = db.Contrats.Find(2003);
                            if (contrat != null)
                            {
                                var lot = db.Lots.Where(lt => lt.NumeroLot == txtNouveauLot.Text).FirstOrDefault();
                                if (lot != null)
                                {
                                    if (lot.StatutLot != StatutLot.Libre)
                                        MessageBox.Show(this, "Erreur: ce lot n'est pas libre!!!",
                                                   "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    else
                                    {
                                        var ancienLot = db.Lots.Find(contrat.LotId);
                                        contrat.LotId = lot.ID;
                                        lot.StatutLot = StatutLot.Reserve;
                                        ancienLot.StatutLot = StatutLot.Libre;
                                        db.SaveChanges();
                                        MessageBox.Show(this, "Le lot a été changé avec succes",
                                                    "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        EffacerFormContrat();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(this, "Erreur: ce lot n'existe pas!!!",
                                                    "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                //txtClient.Text = contrat.Client.NomComplet;
                                //txtTotalEncaisse.Text = contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ###");
                                //txtTypeVilla.Text = contrat.Lot.TypeVilla.NomComplet;
                                //txtLot.Text = contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation ? contrat.Lot.NumeroLot : "";
                                //txtPrixDeVente.Text = contrat.PrixFinal.ToString("### ### ###");
                                //leContratEnCours = contrat;
                                //cmdSupprimerContrat.Enabled = true;
                                //if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                                //    cmdChangementLot.Visible = true;
                                //else
                                //    cmdChangementLot.Visible = false;
                            }
                        }
                        scope.Complete();

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
    }
}

