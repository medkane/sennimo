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
using prjSenImmoWinform.Tools;

namespace prjSenImmoWinform
{
    public partial class FrmConnexion : Form
    {
        private AgentRepository agentRep;

        public FrmConnexion()
        {
            InitializeComponent();
            agentRep = new AgentRepository();
        }

        private void cmdConnection_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;

                if (txtLogin.Text == string.Empty || txtPassword.Text == string.Empty)
                {
                    MessageBox.Show(this, "Veuillez saisir le login et mot de passe de l'utilisateur",
                            "Prosopis - Gestion des utilisateurs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //valider la connexion
                var login = txtLogin.Text;
                var motDePasse = txtPassword.Text;

                var agent = agentRep.FindByLogin(login);
                if (agent==null)
                {
                    MessageBox.Show(this, "Login incorrect",
                            "Prosopis - Connexion à Prosopis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (agent.MotDePasse == Tools.Tools.Encrypt(motDePasse, "JHzZ3LR4"))
                //if (true)
                {
                    FrmMDI childForm = new FrmMDI(agent);
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Owner = this;
                    childForm.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show(this, "Mot de passe incorrect",
                            "Prosopis - Connexion à Prosopis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis -  Connexion à Prosopis", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                cmdConnection_Click(sender, e);
            }
        }

        private void cmdAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmConnexion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.F2)
                pEnvironnement.Visible = true;
           

        }

        private void FrmConnexion_Load(object sender, EventArgs e)
        {

        }
    }
}
