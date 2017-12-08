using prjSenImmoWinform.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using prjSenImmoWinform.DAL;

namespace prjSenImmoWinform
{
    public partial class FrmMenuGeneral : Form
    {
        public Lot LeLotChoisi { get; set; }
        ContratRepository contratRep;
        AgentRepository agentRep;
        private Agent UserEnCours;

        public FrmMenuGeneral()
        {
            InitializeComponent();
        }
        public FrmMenuGeneral(Agent agent):this()
        {
           
            try
            {
                //FrmFicheCommercial childForm = new FrmFicheCommercial();
                //childForm.MdiParent = this;
                //childForm.Show();
                //childForm.WindowState = FormWindowState.Maximized;
                contratRep = new ContratRepository();
                ////Verifier le profil de l'utilisateur connecté
                //// S'il s'agit d'un commercial vérifier le nombre de prospects qui lui ont été affectés et qu'il n'a pas encore acceptés
                //// ainsi que le nombre de ses prospects ayant atteint leur seuil de contractualisation
                //var nbProspectsAyantAtteintLeSeuilContratctuels = contratRep.GetOptionsAyantAtteintsSeuilContrat().Count();
                //Tools.Adorner.AddBadgeTo(cmdNouveauxContrats, nbProspectsAyantAtteintLeSeuilContratctuels.ToString(), Color.YellowGreen, Color.WhiteSmoke);
                //Tools.Adorner.AddBadgeTo(cmdNouveauxProspects, "0", Color.Red, Color.White);
                //Tools.Adorner.AddBadgeTo(cmdActivitesCommerciales, "0", Color.YellowGreen, Color.White);
                agentRep = new AgentRepository();
                tcRoleActions.ItemSize = new Size(0, 1);
                tcRoleActions.SizeMode = TabSizeMode.Fixed;
               
                UserEnCours = agent;
                Tools.Tools.AgentEnCours = agent;
                lbRoleUser.Text = UserEnCours.Role.LibelleRole;
                lbNomCompletUser.Text = UserEnCours.NomComplet;
                switch (UserEnCours.Role.CodeRole)
                {
                    case "ADM":
                        tcRoleActions.Visible = false;
                        break;
                    case "CMC":
                        AlertesCommerciales();

                        //FrmAccueilCommercial childForm = new FrmAccueilCommercial();
                        //childForm.MdiParent = this;
                        //childForm.WindowState = FormWindowState.Maximized;
                        //childForm.Show();


                        tcRoleActions.SelectedTab = tcRoleActions.TabPages[0];
                        break;
                    case "TCH":
                        break;
                    case "RCV":
                        tcRoleActions.SelectedTab = tcRoleActions.TabPages[1];
                        AlertesRcouvrementResa();
                        break;
                    case "CSS":
                        break;
                    case "MKT":
                        tcRoleActions.Visible = false;
                        break;
                    default:
                        break;
                }

                DesactiverTousLesMenus();
                ChargerLesMenusAutorises(agent.RoleId);
                Tools.Tools.MDIForm = this;
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void AlertesCommerciales()
        {
            var nbProspectsAyantAtteintLeSeuilContratctuels = contratRep.GetOptionsAyantAtteintsSeuilContrat(UserEnCours.Id).Count();
            var nbNouveauxProspects = contratRep.GetNouveauProspects(UserEnCours.Id).Count();
            var nouvellesActivitesCommerciales= contratRep.GetActivitesCommerciales(UserEnCours.Id, DateTime.Now.Date).Count();

            Tools.Adorner.RemoveBadgeFrom(cmdNouveauxContrats);
            Tools.Adorner.AddBadgeTo(cmdNouveauxContrats, nbProspectsAyantAtteintLeSeuilContratctuels.ToString(), Color.YellowGreen, Color.WhiteSmoke);
            Tools.Adorner.RemoveBadgeFrom(cmdNouveauxProspects);
            Tools.Adorner.AddBadgeTo(cmdNouveauxProspects, nbNouveauxProspects.ToString(), Color.Red, Color.White);
            Tools.Adorner.RemoveBadgeFrom(cmdActivitesCommerciales);
            Tools.Adorner.AddBadgeTo(cmdActivitesCommerciales, nouvellesActivitesCommerciales.ToString(), Color.YellowGreen, Color.White);
        }

        private void AlertesRcouvrementResa()
        {
            var nbAppelsDeFonds = contratRep.GetNewAppelsDeFonds().Count();

            Tools.Adorner.RemoveBadgeFrom(cmdNouveauxAppelsDeFond);
            Tools.Adorner.AddBadgeTo(cmdNouveauxAppelsDeFond, nbAppelsDeFonds.ToString(), Color.YellowGreen, Color.WhiteSmoke);
        }


        private void DesactiverTousLesMenus()
        {
            foreach (var tab in ribbon1.Tabs)
            {
                foreach (var panel in tab.Panels)
                {
                    panel.Visible = false;
                }
                tab.Visible = false;
            }
        }

        private void ChargerLesMenusAutorises(int roleId)
        {
            try
            {
                var menuAutorises = agentRep.GetRoleMenus(roleId);
                foreach (var menu in menuAutorises)
                {
                    foreach (var tab in ribbon1.Tabs)
                    {
                        if (tab.Text == menu.LibelleMenu)
                        {
                            tab.Visible = true;
                            ribbon1.ActiveTab = tab;
                            foreach (var sousMenu in agentRep.GetRoleSousMenus(roleId))
                            {
                                foreach (var pan in tab.Panels.Where(p => p.Text == sousMenu.LibelleSousMenu))
                                    pan.Visible = true;
                            }
                        }
                    }
                }
               // ribbon1.ActiveTab = ribbonTab1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void rbIlot_Click(object sender, EventArgs e)
        {
            FrmIlot childForm = new FrmIlot();
            childForm.MdiParent = this;
            childForm.Show();
            childForm.WindowState = FormWindowState.Normal;
        }

        private void rbVentes_Click(object sender, EventArgs e)
        {
            FrmVente childForm = new FrmVente("Initial");
            childForm.MdiParent = this;
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.Show();
            childForm.WindowState = FormWindowState.Normal;
            
        }

        private void rbListeClients_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmListeClients childForm = new FrmListeClients();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rbRechercherLot_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLot childForm = new FrmLot();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
}

        private void rbProspects_Click(object sender, EventArgs e)
        {
           
        }

        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmEncaissement childForm = new FrmEncaissement();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rbProspects_DoubleClick(object sender, EventArgs e)
        {

        }

        private void FrmMenuGeneral_Load(object sender, EventArgs e)
        {
           

        }

        private void rbListProspects_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmListeProspects childForm = new FrmListeProspects();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
}

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void cmdNouveauxProspects_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmListeProspects childForm = new FrmListeProspects("NouveauxProspects");
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rbLot_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmDetailsIlot childForm = new FrmDetailsIlot("Initial");
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rbRecouvrement_Click(object sender, EventArgs e)
        {
            FrmSelectionRecouvrement childForm = new FrmSelectionRecouvrement();
            //childForm.MdiParent = this;
            
            childForm.WindowState = FormWindowState.Normal;
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.ShowDialog();
        }
        private void cmdNouvelsAppelsDeFond_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void rpProspects_Click(object sender, EventArgs e)
        {
            rbListProspects_Click(sender, e);
        }

        private void rpEncaissement_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            FrmEncaissement childForm = new FrmEncaissement();
            childForm.MdiParent = this;
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;
        }

        private void rbEncaissement_Click(object sender, EventArgs e)
        {
        }

        private void rpTravaux_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            FrmDetailsIlot childForm = new FrmDetailsIlot("Travaux");
            childForm.MdiParent = this;
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;
        }

        private void rpApporteurAffaire_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            FrmApporteurAffaire childForm = new FrmApporteurAffaire();
            childForm.MdiParent = this;
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;
        }

        private void rbPUserResume_CheckBoxCheckChanged(object sender, EventArgs e)
        {
            pUserResume.Visible = rbPUserResume.Checked;
        }

        private void cmdNouveauxContrats_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            FrmListeProspects childForm = new FrmListeProspects("NouveauxContrats");
            childForm.MdiParent = this;
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;
        }

        private void rpRechercheLot_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            FrmDetailsIlot childForm = new FrmDetailsIlot("Initial");
            childForm.MdiParent = this;
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;
        }

        private void rpCommission_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            FrmCommissionsApporteurAffaire childForm = new FrmCommissionsApporteurAffaire();
            childForm.MdiParent = this;
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;
        }

        private void rpRemboursement_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            FrmRemboursement childForm = new FrmRemboursement();
            childForm.MdiParent = this;
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;
        }

        private void ribbonPanel2_Click(object sender, EventArgs e)
        {
           
        }

        private void FrmMenuGeneral_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void rpRôles_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmRoleFormAccess childForm = new FrmRoleFormAccess();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rpUsers_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmAgent childForm = new FrmAgent();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((Form)this.Owner).Show();
            this.Close();
        }

        private void rpReportingVentes_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmReportingVentes childForm = new FrmReportingVentes();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rpTypeOrigine_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmTypeOrigine childForm = new FrmTypeOrigine();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des types origine", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ribbonPanel3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmReporting childForm = new FrmReporting();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void ribbonButton4_Click(object sender, EventArgs e)
        {

        }

        private void rpReportingEncaissements_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmReportingEncaissements childForm = new FrmReportingEncaissements();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rpReportingTechnique_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmReportingTechnique childForm = new FrmReportingTechnique();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rpReportingMarketing_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmReportingMarketing childForm = new FrmReportingMarketing();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rpRH_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmObjectivation childForm = new FrmObjectivation();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdDeconnexion_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(this,"Voulez vous réellement vous déconnecter de Prosopis?",
                "Prosopis - Déconnexion de l'application", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void timerAlertes_Tick(object sender, EventArgs e)
        {
            if (UserEnCours.Role.CodeRole == "CMC")
                AlertesCommerciales();
            else
                 if (UserEnCours.Role.CodeRole == "RCV")
                AlertesRcouvrementResa();
        }

        private void rbFermerToutes_Click(object sender, EventArgs e)
        {
            this.MdiChildren.OfType<Form>().ToList().ForEach(x => x.Close());
        }

        private void ribbonPanel1_Click(object sender, EventArgs e)
        {
            this.MdiChildren.OfType<Form>().ToList().ForEach(x => x.WindowState= FormWindowState.Maximized);
        }
    }
}
