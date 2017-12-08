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
using System.Security.Cryptography;
using System.IO;

namespace prjSenImmoWinform
{
    public partial class FrmAgent : Form
    {
        private bool bModifAgent;
        private Agent LAgentEnCours;
        private AgentRepository agentRep;
        private ContratRepository contratRep;
        private Role leRoleEnCours;

        public FrmAgent()
        {
            InitializeComponent();
            agentRep = new AgentRepository();
            contratRep = new ContratRepository();
            cmbRoles.DataSource = agentRep.GetAllRoles().ToList();
            cmbRoles.DisplayMember = "LibelleRole";
            cmbRoles.SelectedIndex = -1;

            cmbChefsEquipes.DataSource = agentRep.GetAllAgents().Where(c =>c.Role.CodeRole=="CMC" && c.IsChefEquipe==true).ToList();
            cmbChefsEquipes.DisplayMember = "NomComplet";
            cmbChefsEquipes.SelectedIndex = -1;
            ChargerLesProjets();
            ChargerLesUtilisateurs();
            VerouillerForm();
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

        #region CRUD DE L'APPORTEUR D'AFFAIRE

        private void ChargerLesUtilisateurs()
        {
            try
            {
                dgUtilisateurs.DataSource = agentRep.GetAllAgents().ToList().Select(af => new
                {
                    Id = af.Id,
                    Prénom = af.Prenom,
                    Nom = af.Nom,
                    Téléphone = af.Mobile1,
                    Email = af.Email,
                    Role = af.Role.LibelleRole

                }).ToList();
               // dgUtilisateurs.Columns.Aut
                //dgApporteurAffaires.Columns[0].Width = 0;
                //dgApporteurAffaires.Columns[0].Visible = false;
                //dgApporteurAffaires.Columns[1].Width = 150;
                //dgApporteurAffaires.Columns[2].Width = 100;
                //dgApporteurAffaires.Columns[3].Width = 80;
                //dgApporteurAffaires.Columns[4].Width = 50;
                //dgApporteurAffaires.Columns[4].DefaultCellStyle.Format = "###";
                //dgApporteurAffaires.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgApporteurAffaires.Columns[5].Width = 50;
                //dgApporteurAffaires.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;




            }
            catch (Exception)
            {

                throw;
            }
        }
        private void EffacerForm()
        {
            txtMatricule.Text = string.Empty;

            txtPrenom.Text = string.Empty;
            txtNom.Text = string.Empty;

            txtEmail.Text = string.Empty;
            txtTelephoneMobile.Text = string.Empty;
            txtTelephoneFixe.Text = string.Empty;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;
            cmbRoles.SelectedIndex = -1;
            cmbChefsEquipes.SelectedIndex = -1;

        }

        private void DeverouillerClient()
        {
            txtMatricule.ReadOnly = false;
            txtNom.ReadOnly = false;
            txtPrenom.ReadOnly = false;
            txtEmail.ReadOnly = false;

            txtTelephoneMobile.ReadOnly = false;
            txtTelephoneFixe.ReadOnly = false;
            txtLogin.ReadOnly = false;
            txtPassword.ReadOnly = false;
            cmbRoles.Enabled = true;
            pChefEquipe.Enabled = true;
            cmbProjets.Enabled = true ;
            cmdNouveau.Enabled = false;
            cmdEnregistrer.Enabled = true;
            cmdEditer.Enabled = false;
            cmdSupprimer.Enabled = false;
        }
        private void VerouillerForm()
        {
            txtMatricule.ReadOnly = true;
            txtNom.ReadOnly = true;
            txtPrenom.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtLogin.ReadOnly = true;
            txtPassword.ReadOnly = true;
            txtTelephoneMobile.ReadOnly = true;
            txtTelephoneFixe.ReadOnly = true;
            cmbRoles.Enabled = false;
            pChefEquipe.Enabled = false;
            cmbProjets.Enabled = false;

            cmdNouveau.Enabled = true;
            cmdEnregistrer.Enabled = false;
            cmdEditer.Enabled = true;
            cmdSupprimer.Enabled = true;
        }

        private void cmdNouveau_Click(object sender, EventArgs e)
        {
            bModifAgent = false;
            EffacerForm();
            DeverouillerClient();
            //cmdEnregistrerVille.Enabled = true;
            txtPrenom.Focus();

        }

      

        private void cmdEditer_Click(object sender, EventArgs e)
        {
            bModifAgent = true;
            DeverouillerClient();
            txtPrenom.Focus();
        }

        private void AfficherUtilisateur(Agent utilisateur)
        {
            try
            {
                txtMatricule.Text = utilisateur.Matricule;
                
                txtPrenom.Text = utilisateur.Prenom;
                txtNom.Text = utilisateur.Nom;
                txtTelephoneFixe.Text = utilisateur.Fixe;
                txtTelephoneMobile.Text = utilisateur.Mobile1;
                txtEmail.Text = utilisateur.Email;
                txtLogin.Text = utilisateur.UserLogin;
                if (utilisateur.MotDePasse != string.Empty)
                    txtPassword.Text = Tools.Tools.Decrypt(utilisateur.MotDePasse,"JHzZ3LR4");
                cmbRoles.SelectedItem = utilisateur.Role;
                if (utilisateur.ChefEquipe != null)
                    cmbChefsEquipes.SelectedItem = utilisateur.ChefEquipe;
                else
                    cmbChefsEquipes.SelectedIndex = -1;
                chkEstChefEquipe.Checked = utilisateur.IsChefEquipe;
                if (utilisateur.Projet != null)
                    cmbProjets.SelectedValue = utilisateur.ProjetId;
                else
                    cmbProjets.SelectedIndex = -1;
            }
            catch (Exception)
            {

                throw;
            }

        }
        //private void cmdSupprimerClient_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show(this, "Voulez réellement supprimer ce Client?",
        //            "GesAGRO - Gestion des clients", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
        //    {
        //        db.Clients.Remove(ClientEnCours);
        //        db.SaveChanges();
        //        chargerClients();
        //        EffacerClient();
        //    }

        //}

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPrenom.Text == string.Empty || txtNom.Text == string.Empty)
                {
                    MessageBox.Show(this, "Veuillez saisir les prénoms et nom de l'utilisateur",
                             "Prosopis - Gestion des utilisateurs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cmbRoles.SelectedItem == null)
                {
                    MessageBox.Show(this, "Veuillez choisir le rôle de l'utilisateur",
                             "Prosopis - Gestion des utilisateurs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if(txtLogin.Text== string.Empty || txtPassword.Text==string.Empty)
                {
                    MessageBox.Show(this, "Veuillez saisir le login et mot de passe de l'utilisateur",
                            "Prosopis - Gestion des utilisateurs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Agent chefEquipe=null;
                if (cmbChefsEquipes.SelectedItem!=null)
                {
                    chefEquipe =(Agent) cmbChefsEquipes.SelectedItem;
                }

                var passwordEncrypted = Tools.Tools.Encrypt(txtPassword.Text,"JHzZ3LR4");
                //MessageBox.Show(passwordEncrypted);
                var role = (Role)cmbRoles.SelectedItem;
                if (!bModifAgent)
                {
                    var agent = new Agent()
                    {
                        Prenom = txtPrenom.Text,
                        Nom = txtNom.Text,
                        Mobile1 = txtTelephoneMobile.Text,
                        Fixe = txtTelephoneFixe.Text,
                        Email = txtEmail.Text,
                        Matricule = txtMatricule.Text,
                        RoleId = role.ID,
                        UserLogin = txtLogin.Text,
                        MotDePasse = passwordEncrypted,
                        IsChefEquipe = chkEstChefEquipe.Checked,
                        ProjetId=(int)cmbProjets.SelectedValue
                    };
                    if (chefEquipe != null)
                        agent.ChefEquipe = chefEquipe;

                    agentRep.Add(agent);
                    MessageBox.Show(this, "L'utilisateur a été enregistré",
                             "Prosopis - Gestion des utilisateurs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
                else
                {
                    LAgentEnCours.Matricule = txtMatricule.Text;
                    LAgentEnCours.Prenom = txtPrenom.Text;
                    LAgentEnCours.Nom = txtNom.Text;
                    LAgentEnCours.Mobile1 = txtTelephoneMobile.Text;
                    LAgentEnCours.Fixe = txtTelephoneFixe.Text;
                    LAgentEnCours.Email = txtEmail.Text;
                    LAgentEnCours.RoleId = role.ID;
                    LAgentEnCours.UserLogin = txtLogin.Text;
                    LAgentEnCours.MotDePasse = passwordEncrypted;
                    if (chefEquipe != null)
                        LAgentEnCours.ChefEquipe = chefEquipe;
                    LAgentEnCours.IsChefEquipe = chkEstChefEquipe.Checked;
                    LAgentEnCours.ProjetId = (int)cmbProjets.SelectedValue;
                    agentRep.SaveChanges();

                    MessageBox.Show(this, "L'utilisateur a été modifié",
                            "Prosopis - Gestion des utilisateurs", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                ChargerLesUtilisateurs();
                EffacerForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                        "Prosopis - Gestion des apporteurs d'affaires", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void dgUtilisateurs_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                if (dgUtilisateurs.SelectedRows.Count > 0)
                {
                    int idAgent = (int)dgUtilisateurs.SelectedRows[0].Cells[0].Value;
                    LAgentEnCours = agentRep.FindById(idAgent);
                    AfficherUtilisateur(LAgentEnCours);
                    VerouillerForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                        "Prosopis - Gestion des utilisateurs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbRoles.SelectedItem!=null)
            {
                leRoleEnCours = (Role)cmbRoles.SelectedItem;
                if (leRoleEnCours.CodeRole == "CMC")
                {
                    pChefEquipe.Visible = true;
                }
                else
                    pChefEquipe.Visible = false;
            }
        }

        private void cmdSupprimer_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show(this,"Voulez vous réellement supprimer cet agent?","Prosopis - Suppression agent", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                {
                    agentRep.Delete(LAgentEnCours);
                    MessageBox.Show(this, "L'agent a été supprimé avec succes",
                                       "Prosopis - Gestion des utilisateurs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ChargerLesUtilisateurs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                        "Prosopis - Gestion des utilisateurs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbChefsEquipes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkEstChefEquipe_CheckedChanged(object sender, EventArgs e)
        {
            cmbChefsEquipes.SelectedIndex = -1;
        }

        //static string Encrypt(string value)
        //{
        //    using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        //    {
        //        UTF8Encoding utf8 = new UTF8Encoding();
        //        byte[] data = md5.ComputeHash(utf8.GetBytes(value));
        //        return Convert.ToBase64String(data);
        //    }
        //}

    }
}
