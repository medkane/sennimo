using prjSenImmoWinform.DAL;
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

namespace prjSenImmoWinform
{
    public partial class FrmMDI : Form
    {
        private bool BFormVisible = true;
        public Lot LeLotChoisi { get; set; }
        ContratRepository contratRep;
        AgentRepository agentRep;
        private Agent UserEnCours;

        public FrmMDI()
        {
            InitializeComponent();
        }

        public FrmMDI(Agent agent):this()
        {

            try
            {
                this.Cursor = Cursors.AppStarting;
                tssHeureConnexion.Text = "Vous êtes connecté(e) depuis "+DateTime.Now.ToShortTimeString();
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
                        tcRoleActions.Visible = true;
                        tcRoleActions.SelectedTab = tcRoleActions.TabPages[2];
                        tsLbServeur.Text = "sur "+ System.Configuration.ConfigurationManager.ConnectionStrings["SenImmoDataContext"].ConnectionString.Split(';')[0].Split('=')[1].Trim();
                        break;
                    case "CMC":
                        AlertesCommerciales();
                        if (UserEnCours.IsChefEquipe)
                            tcRoleActions.Visible = false;
                        else
                            tcRoleActions.SelectedTab = tcRoleActions.TabPages[0];
                        break;
                    case "TCH":
                        tcRoleActions.Visible = false;
                        break;
                    case "TRCV":
                        tcRoleActions.SelectedTab = tcRoleActions.TabPages[1];
                        AlertesRcouvrementResa();
                        break;
                    case "RCV":
                        tcRoleActions.SelectedTab = tcRoleActions.TabPages[2];
                        tcRoleActions.Visible = true;
                        break;
                    case "CRCV":
                        tcRoleActions.Visible = false;
                        break;
                    case "DC":
                        tcRoleActions.Visible = false;
                        break;
                    case "CSS":
                        tcRoleActions.Visible = false;
                        break;
                    case "MKT":
                        tcRoleActions.Visible = false;
                        break;
                    case "ADS":
                        tcRoleActions.Visible = false;
                        break;
                    case "MNG":
                        tcRoleActions.Visible = false;
                        break;
                    case "DSK":
                        tcRoleActions.Visible = false;
                        break;
                    default:
                        break;
                }
                LancerPageAccueil(); 

               
                Tools.Tools.MDIForm = this;

                DesactiverTousLesMenus();
                ChargerLesMenusAutorises(agent.RoleId);
                ChargerLesProjets();
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

        private void ChargerLesProjets()
        {

            try
            {
                var lesProjets = contratRep.GetProjets();
                

                cmbProjets.DataSource = lesProjets.ToList();
                cmbProjets.DisplayMember = "DenominationProjet";
                cmbProjets.ValueMember = "Id";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void AlertesCommerciales()
        {
            try
            {
                var nbProspectsAyantAtteintLeSeuilContratctuels = contratRep.GetOptionsAyantAtteintsSeuilContrat(UserEnCours.Id).Count();
                var nbNouveauxProspects = contratRep.GetNouveauProspects(UserEnCours.Id).Count();
                var nouvellesActivitesCommerciales = contratRep.GetActivitesCommerciales(UserEnCours.Id, DateTime.Now.Date).Count();
                



                Tools.Adorner.RemoveBadgeFrom(cmdNouveauxContrats);
                Tools.Adorner.AddBadgeTo(cmdNouveauxContrats, nbProspectsAyantAtteintLeSeuilContratctuels.ToString(), Color.YellowGreen, Color.WhiteSmoke);
                Tools.Adorner.RemoveBadgeFrom(cmdNouveauxProspects);
                Tools.Adorner.AddBadgeTo(cmdNouveauxProspects, nbNouveauxProspects.ToString(), Color.Red, Color.White);
                Tools.Adorner.RemoveBadgeFrom(cmdActivitesCommerciales);
                Tools.Adorner.AddBadgeTo(cmdActivitesCommerciales, nouvellesActivitesCommerciales.ToString(), Color.YellowGreen, Color.White);
                //verifier les activités commerciales arrviées à terme
             

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void AlertesRcouvrementResa()
        {
            var nbAppelsDeFonds = contratRep.GetNewAppelsDeFonds().Count();

            Tools.Adorner.RemoveBadgeFrom(cmdNouveauxAppelsDeFond);
            Tools.Adorner.AddBadgeTo(cmdNouveauxAppelsDeFond, nbAppelsDeFonds.ToString(), Color.YellowGreen, Color.WhiteSmoke);
        }


        private void lotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmDetailsIlot childForm = new FrmDetailsIlot("Initial");
                childForm.MdiParent = this;  
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
                ;
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

        private void ilôtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmIlot childForm = new FrmIlot();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Ilôts", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
}

        private void prospectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmListeProspects childForm = new FrmListeProspects();
                childForm.MdiParent = this;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
                //childForm.WindowState = FormWindowState.Normal;
                
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

        private void clientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmListeClients childForm = new FrmListeClients();
                childForm.MdiParent = this;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
                //childForm.WindowState = FormWindowState.Normal;
                
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

        private void FrmMDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void accueilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LancerPageAccueil();
        }

        private void LancerPageAccueil()
        {
            try
            {

                this.Cursor = Cursors.AppStarting;
                Form childForm = null;
                if (UserEnCours.Role.CodeRole == "CMC" || UserEnCours.Role.CodeRole == "DC")
                {

                    childForm = new FrmAccueilCommercial();
                }
                if (childForm != null)
                {
                    childForm.MdiParent = this;
                    childForm.Show();
                    childForm.WindowState = FormWindowState.Normal;
                    childForm.WindowState = FormWindowState.Maximized;
                }
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

        private void gestionDesUtilisateursToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cmdDeconnexion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Voulez vous réellement vous déconnecter de Prosopis?",
                "Prosopis - Déconnexion de l'application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void DesactiverTousLesMenus()
        {
            foreach (ToolStripMenuItem menu in mnsGeneral.Items)
            {
                
                foreach (var sousMenu in menu.DropDownItems)
                {
                    if (sousMenu.GetType() == typeof(ToolStripMenuItem))
                    {
                        ((ToolStripMenuItem)sousMenu).Visible = false;
                    }
                    if (sousMenu.GetType() == typeof(ToolStripSeparator))
                    {
                        ((ToolStripSeparator)sousMenu).Visible = false;
                    }
                }
                menu.Visible = false;
            }
        }

        private void ChargerLesMenusAutorises(int roleId)
        {
            try
            {
                var menuAutorises = agentRep.GetRoleMenus(roleId);
                foreach (var menuAutorise in menuAutorises)
                {
                    foreach (ToolStripMenuItem menu in mnsGeneral.Items)
                    {
                        if (menu.Tag.ToString() == menuAutorise.CodeMenu)
                        {

                            menu.Visible = true;
                            //if (menu.Text == "Gestion")
                            //{
                            //    Console.WriteLine();
                            //}
                            foreach (var sousMenuAutorise in agentRep.GetRoleSousMenus(roleId))
                            {
                                //foreach (var pan in tab.Panels.Where(p => p.Text == sousMenu.LibelleSousMenu))
                                //    pan.Visible = true;
                                
                                foreach (var sousMenu in menu.DropDownItems)
                                {
                                    //if(sousMenu.GetType() == typeof(ToolStripSeparator))
                                    //     ((ToolStripSeparator)sousMenu).Visible = true;

                                    if (sousMenu.GetType() == typeof(ToolStripMenuItem))
                                    {
                                        if(((ToolStripMenuItem)sousMenu).Tag.ToString()==sousMenuAutorise.CodeSousMenu)
                                             ((ToolStripMenuItem)sousMenu).Visible = true;
                                    }
                                }
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

        private void encaissementsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmSelectionFormEncaissement childForm = new FrmSelectionFormEncaissement();
                childForm.WindowState = FormWindowState.Normal;
                childForm.StartPosition = FormStartPosition.CenterScreen;
                childForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void timerAlerte_Tick(object sender, EventArgs e)
        {
            try
            {
                if (UserEnCours.Role.CodeRole == "CMC")
                    AlertesCommerciales();
                else
                        if (UserEnCours.Role.CodeRole == "RCV")
                    AlertesRcouvrementResa();
                //timerAlerte.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des alertes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timerActiviteCommerciale_Tick(object sender, EventArgs e)
        {
            try
            {
                if(UserEnCours.Role.CodeRole!="CMC")
                {
                    return;
                }
                var reminder = contratRep.GetActivitesCommercialesATerme(UserEnCours.Id).ToList();

                foreach (var ac in reminder)
                {
                    int idActComm = ac.Id;
                    if(BFormVisible==false)
                    {
                        notifyIcon1.BalloonTipText = "Alerte: " + ac.TypeActivite.ToString() + " pour " + ac.Client.NomComplet + " à " + ac.HeureActivite;
                        notifyIcon1.ShowBalloonTip(2000);
                    }
                    else
                    new FrmActiviteCommerciale(idActComm).Show();
                   
                }
                //timerActiviteCommerciale.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des rappels(activités commerciales)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmMDI_Load(object sender, EventArgs e)
        {

        }

        private void cmdActivitesCommerciales_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FrmMesActivitesCommerciales childForm = new FrmMesActivitesCommerciales();
                childForm.MdiParent = this;
               
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdNouveauxContrats_Click(object sender, EventArgs e)
        {
            if (contratRep.GetOptionsAyantAtteintsSeuilContrat(UserEnCours.Id).Count() >0)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmListeProspects childForm = new FrmListeProspects("NouveauxContrats");
                    childForm.MdiParent = this;
                    
                    childForm.Show();
                    childForm.WindowState = FormWindowState.Maximized;
                    //childForm.WindowState = FormWindowState.Normal;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Erreur:..." + ex.Message,
                           "Prosopis - Gestion des Contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                } 
            }
        }

        private void cmdNouveauxProspects_Click(object sender, EventArgs e)
        {
            try
            {
                if (contratRep.GetNouveauProspects(UserEnCours.Id).Count() >0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmListeProspects childForm = new FrmListeProspects("NouveauxProspects");
                    childForm.MdiParent = this;
                    
                    childForm.Show();
                    childForm.WindowState = FormWindowState.Maximized;
                    //childForm.WindowState = FormWindowState.Normal;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void recouvrementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FrmSelectionRecouvrement childForm = new FrmSelectionRecouvrement();
            //childForm.MdiParent = this;

            childForm.WindowState = FormWindowState.Normal;
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rechercheDeLotsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                FrmDetailsIlot childForm = new FrmDetailsIlot("RechercherLot");
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void remboursementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FrmRemboursement childForm = new FrmRemboursement();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des Prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ventesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmReportingVentes childForm = new FrmReportingVentes();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
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

        private void encaissementsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmReportingEncaissements childForm = new FrmReportingEncaissements();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
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

        private void changementDeMotDePasseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmChangePassword childForm = new FrmChangePassword();
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

        private void paramètrageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void evaluationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void commercialToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void objectifsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmObjectivation childForm = new FrmObjectivation();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
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

        private void deconnexionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Voulez vous réellement vous déconnecter de Prosopis?",
                "Prosopis - Déconnexion de l'application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void typeOrigineToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
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

        private void travauxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.Cursor = Cursors.AppStarting;
                FrmDetailsIlot childForm = new FrmDetailsIlot("Travaux");
                //FrmRepriseDD childForm = new FrmRepriseDD();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
                this.Cursor = Cursors.Default;
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
            //        try
            //        {
            //            this.Cursor = Cursors.WaitCursor;
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(this, "Erreur:..." + ex.Message,
            //                   "Prosopis - Gestion des types origine", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //         }
            //        finally
            //        {
            //            this.Cursor = Cursors.Default;
            //        }
    private void apporteurDaffairesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                this.Cursor = Cursors.WaitCursor;
               
                FrmApporteurAffaire childForm = new FrmApporteurAffaire();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
                this.Cursor = Cursors.Default;
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

        private void commissionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FrmCommissionsApporteurAffaire childForm = new FrmCommissionsApporteurAffaire();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;
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

        private void reportingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void techniquesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FrmReportingTechnique childForm = new FrmReportingTechnique();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
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

        private void marketingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FrmReportingMarketing childForm = new FrmReportingMarketing();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
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

        private void gestionDesRôlesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fermerToutesLesFenêtresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MdiChildren.OfType<Form>().ToList().ForEach(x => x.Close());
        }

        private void panelUtilisateurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pUserResume.Visible =! pUserResume.Visible;
        }

        private void agrandrirToutesLesFenêtresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MdiChildren.OfType<Form>().ToList().ForEach(x => x.WindowState= FormWindowState.Maximized);
        }

        private void reduireToutesLesFenêtresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reduireToutesLesFenêtresToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.MdiChildren.OfType<Form>().ToList().ForEach(x => x.WindowState = FormWindowState.Minimized);
        }

        private void FrmMDI_Resize(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    Hide();
            //    notifyIcon1.Visible = true;
            //}
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Maximized;
            notifyIcon1.Visible = false;
            BFormVisible = true;
        }

        private void mettreEnVeilleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Hide();
            notifyIcon1.Visible = true;
            BFormVisible = false;
        }

        private void cmdGenerationEcheances_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmGenerationEcheances childForm = new FrmGenerationEcheances();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmListeOptions childForm = new FrmListeOptions();
                childForm.MdiParent = this;
                
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
                //childForm.WindowState = FormWindowState.Normal;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void scriptsDadministrationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmAdministrationSI childForm = new FrmAdministrationSI();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void serviceDadministrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmAdminServices childForm = new FrmAdminServices();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void extractionProspectSiteWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmExtractionProspectSiteWeb childForm = new FrmExtractionProspectSiteWeb();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void coopérativesDhabitatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmCooperative childForm = new FrmCooperative();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void financesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gestionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void activitésCommercialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmMesActivitesCommerciales childForm = new FrmMesActivitesCommerciales();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void extractionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmExtraction childForm = new FrmExtraction();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdNouveauxAppelsDeFond_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmRecouvrementResa childForm = new FrmRecouvrementResa("New", (int)cmbProjets.SelectedValue);
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdReprise_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmRepriseDeDonnees childForm = new FrmRepriseDeDonnees();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                FrmTypeContrat childForm = new FrmTypeContrat();
                childForm.MdiParent = this;
                childForm.Show();
                childForm.WindowState = FormWindowState.Normal;
                childForm.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Scripts d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void typeContratToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                FrmTypeContrat childForm = new FrmTypeContrat();
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

        private void cmbProjets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProjets.SelectedItem != null)
            {
                Tools.Tools.ProjetEnCours = (Projet)cmbProjets.SelectedItem;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Form5 childForm = new Form5();
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
    }
}
