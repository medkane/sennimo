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
using prjSenImmoWinform.Models;

namespace prjSenImmoWinform
{
    public partial class FrmRoleFormAccess : Form
    {
        private AgentRepository agentRep;
        private Role leRoleEnCours;

        public FrmRoleFormAccess()
        {
            InitializeComponent();
            agentRep = new AgentRepository();
            cmbRoles.DataSource = agentRep.GetAllRoles().ToList();
            cmbRoles.DisplayMember = "LibelleRole";
            cmbRoles.SelectedIndex = -1;

            cmbMenus.DataSource = agentRep.GetAllMenus().ToList();
            cmbMenus.DisplayMember = "LibelleMenu";
            cmbMenus.SelectedIndex = -1;

         
        }

        private void cmbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            leRoleEnCours = (Role)cmbRoles.SelectedItem;
            if (leRoleEnCours != null)
                ChargerLesMenusDuRole();
        }

        private void ChargerLesMenusDuRole()
        {
            dgMenu.DataSource = leRoleEnCours.Menus.ToList();
        }

        private void dgMenu_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgMenu.SelectedRows.Count > 0 && leRoleEnCours!=null)
                {
                    int menuId=(int)dgMenu.SelectedRows[0].Cells[0].Value;
                    dgSousMenu.DataSource = agentRep.GetRoleMenuSousMenus(leRoleEnCours.ID ,menuId).ToList();

                    cmbSousMenus.DataSource = agentRep.GetMeuSousMenus(menuId).ToList();
                    cmbSousMenus.DisplayMember = "LibelleSousMenu";
                    cmbSousMenus.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis -  Gestion des rôles", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdAjouterMenu_Click(object sender, EventArgs e)
        {
            var menu = (Models.Menu)cmbMenus.SelectedItem;
            if(menu!=null)
            {
                agentRep.AddRoleMenu(leRoleEnCours.ID, menu.ID);
                cmbRoles_SelectedIndexChanged(sender, e);
            }
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgMenu.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show(this, "Voulez vous réellement supprimer ce menu?",
                         "Prosopis -  Gestion des rôles", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                    {
                        int menuId = (int)dgMenu.SelectedRows[0].Cells[0].Value;
                        agentRep.DeleteRoleMenu(leRoleEnCours.ID, menuId);
                        ChargerLesMenusDuRole(); 
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void cmdAjouterSousMenu_Click(object sender, EventArgs e)
        {
            try
            {
                var sousMenu = (Models.SousMenu)cmbSousMenus.SelectedItem;
                if (sousMenu != null)
                {
                    agentRep.AddRoleSousMenus(leRoleEnCours.ID, sousMenu.ID);
                    dgMenu_SelectionChanged(sender, e);
                }
            }
            catch (Exception ex)
            {

            MessageBox.Show(this, "Erreur:" + ex.Message,
                     "Prosopis -  Gestion des rôles", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void supprimerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgMenu.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show(this, "Voulez vous réellement supprimer ce sous-menu?",
                         "Prosopis -  Gestion des rôles", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        int sousMenuId = (int)dgSousMenu.SelectedRows[0].Cells[0].Value;
                        agentRep.DeleteRoleSousMenu(leRoleEnCours.ID, sousMenuId);
                        dgMenu_SelectionChanged(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

