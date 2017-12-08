using prjSenImmoWinform.DAL;
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
    public partial class FrmSelectionRecouvrement : Form
    {
        private ContratRepository contratRep;

        public FrmSelectionRecouvrement()
        {
            InitializeComponent();
            contratRep = new ContratRepository();
            ChargerLesProjets();
        }

        private void ChargerLesProjets()
        {

            try
            {
                var lesProjets = contratRep.GetProjets();


                cmbProjets.DataSource = lesProjets.ToList();
                cmbProjets.DisplayMember = "DenominationProjet";
                cmbProjets.ValueMember = "Id";
                cmbProjets.SelectedValue = Tools.Tools.ProjetEnCours.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (rbDepot.Checked)
                {
                    FrmRecouvrementDepot childForm = new FrmRecouvrementDepot((int)cmbProjets.SelectedValue);
                    childForm.MdiParent = Tools.Tools.MDIForm;
                    childForm.Show();
                    childForm.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    FrmRecouvrementResa childForm = new FrmRecouvrementResa((int)cmbProjets.SelectedValue);
                    childForm.MdiParent = Tools.Tools.MDIForm;
                    childForm.Show();
                    childForm.WindowState = FormWindowState.Maximized;
                }
                this.Close();
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

        private void cmdAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
