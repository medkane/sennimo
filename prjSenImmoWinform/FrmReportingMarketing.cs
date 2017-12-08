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

namespace prjSenImmoWinform
{
    public partial class FrmReportingMarketing : Form
    {
        private SenImmoDataContext db;

        public FrmReportingMarketing()
        {
            InitializeComponent();
            dtpDateDebutMarketing.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDateFinMarketing.Value = DateTime.Now.Date;

            db = new SenImmoDataContext();
        }

        private void cmdReportingMarketing_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime dateDebutTechnique = new DateTime(dtpDateDebutTechnique.Value.Year, dtpDateDebutTechnique.Value.Month, DateTime.DaysInMonth(dtpDateDebutTechnique.Value.Year, dtpDateDebutTechnique.Value.Month));
                //DateTime dateFinTechnique = new DateTime(dtpDateFinTechnique.Value.Year, dtpDateFinTechnique.Value.Month, DateTime.DaysInMonth(dtpDateFinTechnique.Value.Year, dtpDateFinTechnique.Value.Month));

                //int idIlot = 0;
                //if (cmbIlotsTechnique.SelectedItem != null)
                //{
                //    idIlot = ((Ilot)cmbIlotsTechnique.SelectedItem).Id;
                //}
                //else return;

                //int nbClients = db.Clients.Where(Pros => Pros. == idIlot && ea.LotVirtuel == false).Count();
                var query = db.Clients.Where(Pros => Pros.DateAffectationCommercial >= dtpDateDebutMarketing.Value.Date && Pros.DateAffectationCommercial <= dtpDateFinMarketing.Value.Date)
                    .GroupBy(pros => new
                    {
                        pros.Origine
                    }
                    )

                    .Select(n => new
                    {
                        Origine = n.Key.Origine.LibelleTypeOrigine,
                        Nombre= n.Count(),

                    }
                    )
                    .ToList();
                dgReportingMarketing.DataSource = query;

                lvReportingTypeOrigine.Items.Clear();
                foreach (var item in query)
                {
                    ListViewItem lviRepCommercial = new ListViewItem(item.Origine.ToString());
                    lviRepCommercial.SubItems.Add(item.Nombre.ToString("##0"));

                    //lviRepCommercial.SubItems.Add(item.Realisé.ToString());
                    //lviRepCommercial.SubItems.Add((item.Objectif - item.Realisé).ToString());
                    lvReportingTypeOrigine.Items.Add(lviRepCommercial);
                }

                charTypeOrigines.Series.Clear();
                charTypeOrigines.Series.Add("TypeOrigines");
               

                charTypeOrigines.Series["TypeOrigines"].IsValueShownAsLabel = true;


                charTypeOrigines.Series["TypeOrigines"].LegendText = "Origines";

                foreach (var item in query)
                {
                   
                    charTypeOrigines.Series["TypeOrigines"].Points.AddXY(item.Origine, item.Nombre);
                }

                //°°°°°°°°°°°°°°°°°°°°°°°°°°
                var queryCumule = db.Clients.Where(Pros => Pros.DateAffectationCommercial >= dtpDateDebutMarketing.Value.Date && Pros.DateAffectationCommercial <= dtpDateFinMarketing.Value.Date)
                    .GroupBy(pros => new
                    {
                        pros.Origine,
                        pros.DateAffectationCommercial.Value.Year,
                        pros.DateAffectationCommercial.Value.Month,
                    }
                    )

                    .Select(n => new
                    {
                        Origine = n.Key.Origine.LibelleTypeOrigine,
                        Annee = n.Key.Year,
                        Mois=n.Key.Month,
                       
                        Nombre = n.Count(),

                    }
                    )
                    .ToList();

                dgReportingCumuleMarketing.DataSource = queryCumule;
                lvReportingTypeOrigineCumules.Items.Clear();
                foreach (var item in queryCumule)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    ListViewItem lviRepCommercial = new ListViewItem(item.Origine);
                    lviRepCommercial.SubItems.Add(mois + " " + annee);
                    lviRepCommercial.SubItems.Add(item.Nombre.ToString());

                    lvReportingTypeOrigineCumules.Items.Add(lviRepCommercial);
                }
                //dgReportingTechnique.DataSource = query;
                //dgReportingTechnique.Columns[0].HeaderText = "POSTES TRAVAUX";
                //dgReportingTechnique.Columns[0].Width = 250;
                //dgReportingTechnique.Columns[1].HeaderText = " NOMBRE DE VILLA EN FIN " + dateDebutTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                //dgReportingTechnique.Columns[1].Width = 80;
                //dgReportingTechnique.Columns[2].HeaderText = "NOMBRE DE VILLA EN FIN " + dateFinTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                //dgReportingTechnique.Columns[2].Width = 80;
                //dgReportingTechnique.Columns[3].HeaderText = "FIN " + dateDebutTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                //dgReportingTechnique.Columns[3].Width = 80;
                //dgReportingTechnique.Columns[4].HeaderText = "FIN " + dateFinTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                //dgReportingTechnique.Columns[4].Width = 80;
                ////dgReportingTechnique.Columns[3].DefaultCellStyle.Format = "### ### ###";
                ////dgReportingTechnique.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //lbNombreDeVillas.Text = nbVillas.ToString();

                //chartVentes.DataSource = query;
                //chartVentes.Series["Ventes"].Points.Clear();
                //chartVentes.Series["Ventes"].XValueMember = "Type";
                //chartVentes.Series["Ventes"].YValueMembers = "Count";
                //chartVentes.DataBind();

                //chartVentes.Series["Ventes"].YValueMembers = "Count";
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
    }
}
