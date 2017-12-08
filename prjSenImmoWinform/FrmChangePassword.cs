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
    public partial class FrmChangePassword : Form
    {
        private AgentRepository agentRep;

        public FrmChangePassword()
        {
            InitializeComponent();
            agentRep = new AgentRepository();
        }

        private void cmdValider_Click(object sender, EventArgs e)
        {
            try
            {
                //Vérifier que l'ancien password saisi est le bon
                var agent = agentRep.FindById(Tools.Tools.AgentEnCours.Id);
                var motDePasse = txtOldPassword.Text;



                if (agent.MotDePasse != Tools.Tools.Encrypt(motDePasse, "JHzZ3LR4"))
                {
                    MessageBox.Show(this, "Ancien mot de passe incorrect",
                           "Prosopis - Changement de mot de passe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Comparer le nouveau password et la confirmation
                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show(this, "Confirmation du mot de passe incorrect",
                        "Prosopis - Changement de mot de passe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Enregistrer le nouveau password

                var passwordEncrypted = Tools.Tools.Encrypt(txtNewPassword.Text, "JHzZ3LR4");

                agent.MotDePasse = passwordEncrypted;

                agentRep.SaveChanges();

                MessageBox.Show(this, "Votre mot de passe a été modifié avec succes",
                       "Prosopis - Changement de mot de passe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur: ... "+ex.Message,
                       "Prosopis - Changement de mot de passe", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void chkVoir_CheckedChanged(object sender, EventArgs e)
        {
            if(chkVoir.Checked)
            {
                txtOldPassword.PasswordChar = '\0';
                txtNewPassword.PasswordChar = '\0';
                txtConfirmPassword.PasswordChar = '\0';
            }
            else
            { 
                txtOldPassword.PasswordChar = '*';
                txtNewPassword.PasswordChar = '*';
                txtConfirmPassword.PasswordChar = '*';
            }
        }

        private void cmdAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
    