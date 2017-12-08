using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace prjSenImmoWinform
{
    public partial class FrmGenerationEcheances : Form
    {
        DateTime dateDebutPeriode;
        DateTime dateFinPeriode;
        public FrmGenerationEcheances()
        {
            InitializeComponent();
            dtpAnnee.Value = DateTime.Now;
            cmbMois.SelectedIndex = DateTime.Now.Month - 1;
        }
       

        private void cmdGenerer_Click(object sender, EventArgs e)
        {

            var dateReference = dtpDateReference.Value;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SenImmoDataContext"].ConnectionString;
                using (SqlConnection cnx = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"
                                UPDATE [AKYSDATABASE].[dbo].Factures 
                                SET [AKYSDATABASE].[dbo].Factures.Echue=1, 
                                    [AKYSDATABASE].[dbo].Factures.Active=1
                                WHERE [AKYSDATABASE].[dbo].Factures.TypeFacture=4 
                                AND [AKYSDATABASE].[dbo].Factures.DateEcheanceFacture  <= CAST('" + dateFinPeriode + @"'  AS DATE);

                                
                        ");
                    //AND[AKYSDATABASE].[dbo].Factures.DateEcheanceFacture >= CAST('" + dateDebutPeriode + @"'  AS DATE)
                    cmd.Connection = cnx;
                    cnx.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Les échéances de la période " + cmbMois.SelectedItem.ToString() + " " + dtpAnnee.Value.Year + " ont été générées avec succes.", "Prosopis - Génération des factures d'échéances", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la génération des échéances: "+ex.Message, "Prosopis - Génération des factures d'échéances", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmdRAZ_Click(object sender, EventArgs e)
        {
           
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["SenImmoDataContext"].ConnectionString;
                using (SqlConnection cnx = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"
                                UPDATE [AKYSDATABASE].[dbo].Factures 
                                SET [AKYSDATABASE].[dbo].Factures.Echue=0,
                                    [AKYSDATABASE].[dbo].Factures.Active=0
                                WHERE [AKYSDATABASE].[dbo].Factures.TypeFacture=4 ; 
                        ");
                    cmd.Connection = cnx;
                    cnx.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Les factures d'échéances ont été supprimées avec succes.", "Prosopis - Génération des factures d'échéances", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la génération des échéances: " + ex.Message, "Prosopis - Génération des factures d'échéances", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmbMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbMois.SelectedItem!=null)
            {
                int mois = cmbMois.SelectedIndex + 1;
                int annee = dtpAnnee.Value.Year;
                dateDebutPeriode = new DateTime(annee, mois, 1);
                dateFinPeriode = new DateTime(annee, mois, DateTime.DaysInMonth(annee, mois));



            }
        }

        private void dtpAnnee_ValueChanged(object sender, EventArgs e)
        {

            if (cmbMois.SelectedItem != null)
            {
                int mois = cmbMois.SelectedIndex + 1;
                int annee = dtpAnnee.Value.Year;
                dateDebutPeriode = new DateTime(annee, mois, 1);
                dateFinPeriode = new DateTime(annee, mois, DateTime.DaysInMonth(annee, mois));



            }
        }
    }
}
