using prjSenImmoWinform.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace prjSenImmoWinform
{
    public partial class FrmReportingEncaissements : Form
    {
        private SenImmoDataContext db;

        public FrmReportingEncaissements()
        {
            InitializeComponent();
            db = new SenImmoDataContext();

            //cmbIlots.DataSource = db.Ilots.ToList();
            //cmbIlots.DisplayMember = "NomIlot";
            //cmbIlots.SelectedIndex = -1;

            dtpDateDebut.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDateFin.Value = DateTime.Now.Date;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer5_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdRecouvrement_Click(object sender, EventArgs e)
        {
            try
            {
                var query = db.Contrats.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Réservation).ToList();

                //if (cmbIlots.SelectedItem != null)
                //{
                //    var ilot = (Ilot)cmbIlots.SelectedItem;
                //    query = query.Where(c => c.Lot.IlotID == ilot.Id).ToList();
                //}
                

                var query2 = query.GroupBy(c => new
                {
                    c.Lot.Ilot.NomIlot,

                })
                .Select(n => new
                {
                    Ilot = n.Key.NomIlot,
                    NbVilla = n.Where(c => c.DateSouscription <= dtpDateFin.Value.Date).Count(),
                    CA = n.Where(c => c.DateSouscription <= dtpDateFin.Value.Date).Sum(c => c.PrixFinal),
                    AEncaisser = n.Sum(c => c.Factures.Where(f => f.Echue == true && f.DateEcheanceFacture <= dtpDateFin.Value.Date).Sum(f => f.Montant)),
                    Encaisse = n.Sum(c => c.EncaissementGlobals.Where(enc => enc.DateEncaissement <= dtpDateFin.Value.Date).Sum(enc => enc.MontantGlobal)),
                    //ARecouvrer = n.Sum(c => c.Factures.Where(f => f.Echue == true).Sum(f => f.Montant))
                    //            - n.Sum(c => c.EncaissementGlobals.Sum(enc => enc.MontantGlobal)),
                    ARec = n.Sum(c => c.Factures.Where(f => f.Echue == true && f.DateEcheanceFacture <= dtpDateFin.Value.Date).Sum(f => f.Montant - f.Encaissements.Where(enc => enc.Date <= dtpDateFin.Value.Date).Sum(enc => enc.Montant)))
                }).ToList();
                dgResultResa.DataSource = null;
                dgResultResa.DataSource = query2;
               
                //
                var queryDepot = db.Contrats.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
               .GroupBy(c => new
               {
                   c.Lot.Ilot.NomIlot,
               })
               .Select(n => new
               {

                   NbVilla = n.Count(),
                   CA = n.Sum(c => c.PrixFinal),
                   DepotInitial = n.Sum(c => c.Factures.Where(f => f.Echue == true && f.TypeFacture == TypeFacture.DepotMinimum && f.DateEcheanceFacture <= dtpDateFin.Value.Date)
                                    .Sum(f => f.Encaissements.Sum(enc => enc.Montant))),
                   Echeances = n.Sum(c => c.Factures.Where(f => f.Echue == true && f.TypeFacture == TypeFacture.Echeance && f.DateEcheanceFacture <= dtpDateFin.Value.Date)
                                    .Sum(f => (decimal?)f.Montant)??0),
                   
                   AEncaisser = n.Sum(c => c.Factures.Where(f => f.Echue == true && f.TypeFacture!=TypeFacture.FraisDossier && f.DateEcheanceFacture <= dtpDateFin.Value.Date)
                                    .Sum(f => f.Montant)),
                   Encaisse = n.Sum(c => c.EncaissementGlobals.Where(enc => enc.DateEncaissement <= dtpDateFin.Value.Date).Sum(enc => enc.MontantGlobal ) - 
                                     c.Factures.Where(f => f.TypeFacture== TypeFacture.FraisDossier && f.DateEcheanceFacture <= dtpDateFin.Value.Date).Sum(f => (decimal?)f.Montant)),
                   //ARecouvrer = n.Sum(c => c.Factures.Where(f => f.Echue == true).Sum(f => f.Montant))
                   //            - n.Sum(c => c.EncaissementGlobals.Sum(enc => enc.MontantGlobal)),
                   ARec = n.Sum(c => c.Factures.Where(f => f.Echue == true && f.DateEcheanceFacture <= dtpDateFin.Value.Date).Sum(f => f.Montant - f.Encaissements.Sum(enc => enc.Montant)))

               }).ToList();
                dgResultDepot.DataSource = null;
                dgResultDepot.DataSource = queryDepot;




                dgResultResa.Columns[0].Width = 70;
                dgResultResa.Columns[1].HeaderText = "NB villas";
                dgResultResa.Columns[1].Width = 80;

                dgResultResa.Columns[2].Width = 90;
                dgResultResa.Columns[2].DefaultCellStyle.Format = "### ### ##0";
                dgResultResa.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgResultResa.Columns[3].HeaderText = "A encaisser";
                dgResultResa.Columns[3].Width = 90;
                dgResultResa.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgResultResa.Columns[3].DefaultCellStyle.Format = "### ### ##0";

                dgResultResa.Columns[4].Width = 90;
                dgResultResa.Columns[4].DefaultCellStyle.Format = "### ### ##0";
                dgResultResa.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgResultResa.Columns[5].HeaderText = "A recouvrer";
                dgResultResa.Columns[5].DefaultCellStyle.Format = "### ### ##0";
                dgResultResa.Columns[5].Width = 90;
                dgResultResa.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                ////////
                dgResultDepot.Columns[0].Width = 80;
                dgResultDepot.Columns[0].HeaderText = "NB villas";

                dgResultDepot.Columns[1].Width = 90;
                dgResultDepot.Columns[1].DefaultCellStyle.Format = "### ### ##0";
                dgResultDepot.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgResultDepot.Columns[2].HeaderText = "Dépôt Initial";
                dgResultDepot.Columns[2].Width = 90;
                dgResultDepot.Columns[2].DefaultCellStyle.Format = "### ### ##0";
                dgResultDepot.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgResultDepot.Columns[3].HeaderText = "Sur échéances";
                dgResultDepot.Columns[3].Width = 90;
                dgResultDepot.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgResultDepot.Columns[3].DefaultCellStyle.Format = "### ### ##0";

                dgResultDepot.Columns[4].HeaderText = "A encaisser";
                dgResultDepot.Columns[4].Width = 90;
                dgResultDepot.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgResultDepot.Columns[4].DefaultCellStyle.Format = "### ### ##0";

                dgResultDepot.Columns[5].HeaderText = "Encaissé";
                dgResultDepot.Columns[5].Width = 90;
                dgResultDepot.Columns[5].DefaultCellStyle.Format = "### ### ##0";
                dgResultDepot.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgResultDepot.Columns[6].HeaderText = "A recouvrer";
                dgResultDepot.Columns[6].DefaultCellStyle.Format = "### ### ##0";
                dgResultDepot.Columns[6].Width = 90;
                dgResultDepot.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //Encaissement par nouvelles ventes et recouvrement

                var queryNouvellesVentesRecouvrement = db.Contrats.ToList();

                //if (cmbIlots.SelectedItem != null)
                //{
                //    var ilot = (Ilot)cmbIlots.SelectedItem;
                //    queryNouvellesVentesRecouvrement = queryNouvellesVentesRecouvrement.Where(c => c.Lot.IlotID == ilot.Id).ToList();
                //}

                var queryNouvellesVentesRecouvrement2 = queryNouvellesVentesRecouvrement.GroupBy(c => new
               {
                   c.DateSouscription.Value.Year,
                   c.DateSouscription.Value.Month
               })
               .Select(n => new
               {
                   Annee = n.Key.Year,
                   Mois = n.Key.Month,
                  
                  
                   NewVentes = n.Sum(c => c.Factures.Where(f => f.Echue == true &&( f.TypeFacture== TypeFacture.DepotMinimum ||
                                                                                    f.TypeFacture == TypeFacture.AvanceDemarrage)
                                                                                 ).Sum(f => f.Montant)),
                   Recouvrements = n.Sum(c => c.Factures.Where(f => f.Echue == true && (f.TypeFacture == TypeFacture.AppelDeFond || f.TypeFacture == TypeFacture.Echeance)
                                                              ).Sum(f => (decimal?)f.Encaissements.Sum(enc =>enc.Montant)))??0,

               }).ToList();
                lvEncaissementNewVentesRecouvrement.Items.Clear();
                foreach (var item in queryNouvellesVentesRecouvrement2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                    lviRepCommercial.SubItems.Add(item.NewVentes.ToString("### ### ##0"));
                    lviRepCommercial.SubItems.Add(item.Recouvrements.ToString("### ### ##0"));
                    lviRepCommercial.SubItems.Add((item.NewVentes + item.Recouvrements).ToString("### ### ##0"));
                    lvEncaissementNewVentesRecouvrement.Items.Add(lviRepCommercial);
                }

                chartEncaissements.Series.Clear();
                chartEncaissements.Series.Add("New");
                chartEncaissements.Series.Add("Reco");

                chartEncaissements.Series["New"].IsValueShownAsLabel = true;
          
                chartEncaissements.Series["Reco"].IsValueShownAsLabel = true;

                chartEncaissements.Series["New"].LegendText = "Nouvelles ventes";
                chartEncaissements.Series["Reco"].LegendText = "Recouvrement";

                foreach (var item in queryNouvellesVentesRecouvrement2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    var periode = mois + " " + annee;
                    chartEncaissements.Series["New"].Points.AddXY(periode, item.NewVentes);
                }
                foreach (var item in queryNouvellesVentesRecouvrement2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    var periode = mois + " " + annee;
                    chartEncaissements.Series["Reco"].Points.AddXY(periode, item.Recouvrements);
                }

               foreach(Series b in chartEncaissements.Series)
                { 
                    foreach (DataPoint c in b.Points)
                    {
                        //Sets just the legend to the dollar values
                        c.Label = c.YValues[0].ToString("### ### ##0");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                     "Prosopis - Gestion des rapports de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void splitContainer5_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
