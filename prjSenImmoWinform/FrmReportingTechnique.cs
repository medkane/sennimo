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
    public partial class FrmReportingTechnique : Form
    {
        private SenImmoDataContext db;

        public FrmReportingTechnique()
        {
            InitializeComponent();
            db = new SenImmoDataContext();
          

           

            cmbIlotsTechnique.DataSource = db.Ilots.ToList();
            cmbIlotsTechnique.DisplayMember = "NomIlot";
            //cmbIlotsTechnique.SelectedIndex = -1;

           
        }

        private void cmdReportingtechnique_Click(object sender, EventArgs e)
        {

        }

        private void cmdReportingtechnique_Click_1(object sender, EventArgs e)
        {
            try
            {
                DateTime dateDebutTechnique = new DateTime(dtpDateDebutTechnique.Value.Year, dtpDateDebutTechnique.Value.Month, DateTime.DaysInMonth(dtpDateDebutTechnique.Value.Year, dtpDateDebutTechnique.Value.Month));
                DateTime dateFinTechnique = new DateTime(dtpDateFinTechnique.Value.Year, dtpDateFinTechnique.Value.Month, DateTime.DaysInMonth(dtpDateFinTechnique.Value.Year, dtpDateFinTechnique.Value.Month));
                ////Indicateur par Ilot
                //var query = db.EtatAvancements.Where(ea => ea.Lot.IlotID == 1 && ea.TypeEtatAvancement.StatutTermine == true)
                //    .GroupBy(eal => new
                //    {
                //        eal.TypeEtatAvancement
                //    }
                //    )
                //    .OrderBy(ea => ea.Key.TypeEtatAvancement.ordre)
                //    .Select(n => new
                //    {
                //        Avancement = n.Key.TypeEtatAvancement.LibelleTechnique,
                //        Nombre = n.Count(ea => ea.Actif == true),
                //        NombreTotal = db.Lots.Where(ea => ea.IlotID == 1 && ea.LotVirtuel == false).Count(),
                //        NiveauRealisation = ((double)n.Count(ea => ea.Actif == true) / db.Lots.Where(ea => ea.IlotID == 1 && ea.LotVirtuel == false).Count()*100)+"%"
                //    }
                //    )
                //    .ToList();

                //dgResult.DataSource = query;
                int idIlot = 0;
                if (cmbIlotsTechnique.SelectedItem != null)
                {
                    idIlot = ((Ilot)cmbIlotsTechnique.SelectedItem).Id;
                }
                else return;

                int nbVillas = db.Lots.Where(ea => ea.IlotID == idIlot && ea.LotVirtuel == false).Count();
                var query = db.EtatAvancements.Where(ea => ea.Lot.IlotID == idIlot && ea.TypeEtatAvancement.StatutTermine == true)
                    .GroupBy(eal => new
                    {
                        eal.TypeEtatAvancement
                    }
                    )
                    .OrderBy(ea => ea.Key.TypeEtatAvancement.ordre)
                    .Select(n => new
                    {
                        Avancement = n.Key.TypeEtatAvancement.LibelleTechnique,
                        Nombre1 = n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateDebutTechnique.Date),
                        Nombre2 = n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateFinTechnique.Date),
                        NiveauRealisation1 = ((double)n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateDebutTechnique.Date) / nbVillas * 100) + "%",
                        NiveauRealisation2 = ((double)n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateFinTechnique.Date) / nbVillas * 100) + "%",
                        DYN = (((double)n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateFinTechnique.Date) / nbVillas * 100) - ((double)n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateDebutTechnique.Date) / nbVillas * 100)) + "%",
                        Commentaires = n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateFinTechnique.Date).ToString() + "/" + nbVillas
                    }
                    )
                    .ToList();

                dgReportingTechnique.DataSource = query;
                dgReportingTechnique.Columns[0].HeaderText = "POSTES TRAVAUX";
                dgReportingTechnique.Columns[0].Width = 250;
                dgReportingTechnique.Columns[1].HeaderText = " NOMBRE DE VILLA EN FIN " + dateDebutTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                dgReportingTechnique.Columns[1].Width = 80;
                dgReportingTechnique.Columns[2].HeaderText = "NOMBRE DE VILLA EN FIN " + dateFinTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                dgReportingTechnique.Columns[2].Width = 80;
                dgReportingTechnique.Columns[3].HeaderText = "FIN " + dateDebutTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                dgReportingTechnique.Columns[3].Width = 80;
                dgReportingTechnique.Columns[4].HeaderText = "FIN " + dateFinTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                dgReportingTechnique.Columns[4].Width = 80;
                //dgReportingTechnique.Columns[3].DefaultCellStyle.Format = "### ### ###";
                //dgReportingTechnique.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                lbNombreDeVillas.Text = nbVillas.ToString();

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
    }
}
