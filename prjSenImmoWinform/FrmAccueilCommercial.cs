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
    public partial class FrmAccueilCommercial : Form
    {
        private ClientRepository clientRep;
        private ContratRepository contratRep;
        private CommercialRepository commercialRep;
        private Agent LAgentConcerne;

        public FrmAccueilCommercial()
        {
            InitializeComponent();
            //Tools.Adorner.AddBadgeTo(cmdNouveauxProspects, "12", Color.Red, Color.White);
            //Tools.Adorner.AddBadgeTo(cmdNouvelsAppelsDeFond, "5", Color.SkyBlue, Color.White);
            //Tools.Adorner.AddBadgeTo(cmdActivitesCommerciales, "5", Color.YellowGreen, Color.White);
            clientRep = new ClientRepository();
            contratRep = new ContratRepository();
            commercialRep = new CommercialRepository();
            LAgentConcerne = Tools.Tools.AgentEnCours;

            if (LAgentConcerne.Role.CodeRole == "CMC" || LAgentConcerne.Role.CodeRole == "DC")
            {
                if (LAgentConcerne.IsChefEquipe || LAgentConcerne.Role.CodeRole == "DC")
                {
                    if (LAgentConcerne.Role.CodeRole == "DC")
                        cmbCommerciaux.DataSource = commercialRep.GetAllCommerciaux().Where(comm => comm.ProjetId==Tools.Tools.AgentEnCours.ProjetId).ToList();
                    else
                        cmbCommerciaux.DataSource = commercialRep.GetAllCommerciaux().Where(comm => comm.ProjetId == Tools.Tools.AgentEnCours.ProjetId && comm.ChefEquipeId== Tools.Tools.AgentEnCours.Id).ToList();
                    cmbCommerciaux.DisplayMember = "NomComplet";
                    cmbCommerciaux.SelectedIndex = -1;
                    cmbCommerciaux.Visible = true;
                    lbCommerciaux.Visible = true;
                    cmdRechercher.Visible = true;
                }
                else
                {
                    cmbCommerciaux.Visible = false;
                    lbCommerciaux.Visible = false;
                    cmdRechercher.Visible = false;
                }
            }
            LAgentConcerne = Tools.Tools.AgentEnCours;
            AfficherActivitesCommercialesProspect(LAgentConcerne.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
            ChargerLesContrats(LAgentConcerne);
            ChargerListeProspects(LAgentConcerne);
            //var premierJourDeLAn = new DateTime(DateTime.Now.Year, 1, 1);
            //var today = DateTime.Now;

            var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            dtpDateDebutCalendar.Value = monday;

            var friday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday);
            dtpDateFinCalendar.Value = friday;
            ChargerLesVentes(LAgentConcerne);
        }
        

        private void AfficherActivitesCommercialesProspect(int commercialId, DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                lvActivitesCommerciales.Items.Clear();
                var dateDb = dateDebut.Date;
                var dateFn = dateFin.Date;
                var activitesCommerciales = clientRep.GetActivitesCommerciales(commercialId, dateDb, dateFn).OrderByDescending(act => act.DateActivite).ThenBy(act => act.HeureActivite).ToList();

                if (!Tools.Tools.AgentEnCours.IsChefEquipe)
                    if (lvActivitesCommerciales.Columns.Count > 5)
                        lvActivitesCommerciales.Columns.RemoveAt(5);

                foreach (var ac in activitesCommerciales)
                {
                    ListViewItem lviAc = new ListViewItem(ac.DateActivite.ToShortDateString());
                    lviAc.SubItems.Add(ac.HeureActivite.ToString().Substring(0, 5));
                    lviAc.SubItems.Add(ac.Client.NomComplet);
                    lviAc.SubItems.Add(ac.TypeActivite.ToString());
                    lviAc.SubItems.Add(ac.Commentaire);
                    if (Tools.Tools.AgentEnCours.IsChefEquipe)
                        lviAc.SubItems.Add(ac.Commercial.NomComplet);
                    
                    //lviAc.SubItems.Add(ac.StatutActiviteCommerciale.ToString());

                    if (ac.Priorite == Priorite.Faible)
                        lviAc.ImageIndex = 0;
                    else
                      if (ac.Priorite == Priorite.Moyenne)
                        lviAc.ImageIndex = 1;
                    else
                      if (ac.Priorite == Priorite.Haute)
                        lviAc.ImageIndex = 2;
                    switch (ac.StatutActiviteCommerciale)
                    {
                        case StatutActiviteCommerciale.NonEchue:
                            lviAc.BackColor = Color.White;
                            break;
                        case StatutActiviteCommerciale.Exécutée:
                            lviAc.BackColor = Color.FromArgb(107, 181, 0);
                            break;
                        case StatutActiviteCommerciale.Renvoyée:
                            lviAc.BackColor = Color.Yellow;
                            break;
                        case StatutActiviteCommerciale.Annulée:
                            lviAc.BackColor = Color.LightGray;
                            break;
                        case StatutActiviteCommerciale.EchueNonExecutée:
                            lviAc.BackColor = Color.Salmon;
                            break;
                        default:
                            break;
                    }
                    lviAc.Tag = ac.Id;
                    lvActivitesCommerciales.Items.Add(lviAc);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateDuJour_ValueChanged(object sender, EventArgs e)
        {
            AfficherActivitesCommercialesProspect(LAgentConcerne.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
        }

        private void dtpDateDebutCalendar_ValueChanged(object sender, EventArgs e)
        {
            AfficherActivitesCommercialesProspect(LAgentConcerne.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
        }

        private void ChargerListeProspects(Agent agentConcerne)
        {
            try
            {
                var Clients = clientRep.GetProspects("", "", "", "").ToList();
                if (agentConcerne.Role.CodeRole == "CMC")
                {
                    if (agentConcerne.IsChefEquipe)
                    {
                        Clients = Clients.Where(pros => pros.Projet?.Id == agentConcerne.ProjetId && pros.Commercial != null && pros.Commercial.ChefEquipeId == agentConcerne.Id).ToList();
                    }
                    else
                    {
                        Clients = Clients.Where(pros => pros.Projet?.Id == agentConcerne.ProjetId && pros.CommercialID == agentConcerne.Id).ToList();
                    }
                }
                if (txtNomRecherche.Text != string.Empty)
                    Clients = Clients.Where(c => c.Nom.ToLower().StartsWith(txtNomRecherche.Text.ToLower())).ToList();
                if (txtPrenomRecherche.Text != string.Empty)
                    Clients = Clients.Where(c => c.Prenom.ToLower().StartsWith(txtPrenomRecherche.Text.ToLower())).ToList();

                if (txtTelephoneRecherche.Text != string.Empty)
                    Clients = Clients.Where(c => c.Mobile1.StartsWith(txtTelephoneRecherche.Text)).ToList();

                if (txtEmailRecherche.Text != string.Empty)
                    Clients = Clients.Where(c => c.Email.ToLower().StartsWith(txtEmailRecherche.Text.ToLower())).ToList();
                //.OrderByDescending(pros => pros.DateCreation)
                int i = 1;
                var LesClients = Clients.ToList().OrderByDescending(pros => pros.DateCreation).ToList();
                lvProspects.Items.Clear();
                if (!Tools.Tools.AgentEnCours.IsChefEquipe && Tools.Tools.AgentEnCours.Role.CodeRole=="CMC")
                    if(lvProspects.Columns.Count > 5)
                        lvProspects.Columns.RemoveAt(5);

                foreach (var prospect in LesClients)
                {
                    ListViewItem lviProspect = new ListViewItem(prospect.DateCreation.ToShortDateString());
                    lviProspect.SubItems.Add(prospect.NomComplet);
                    lviProspect.SubItems.Add(prospect.Mobile1);
                    lviProspect.SubItems.Add(prospect.Email);
                    if (prospect.ProspectAffecte == false)
                        lviProspect.ImageIndex = 6;
                    else if (prospect.ProspectEdite)
                        lviProspect.ImageIndex = 8;
                    else
                        lviProspect.ImageIndex = 7;

                    var option = prospect.Options.Where(opt => opt.Active == true).FirstOrDefault();
                    if (option != null)
                    {
                        if (option.SeuilContratAtteint == true && option.ContratGenere == false)
                            lviProspect.ImageIndex = 9;



                        if (option.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                        {
                            lviProspect.SubItems.Add(option.TypeVilla.CodeType + "/" + option.Lot.NumeroLot);
                        }
                        else
                            lviProspect.SubItems.Add(option.TypeVilla.CodeType);
                    }
                    else
                        lviProspect.SubItems.Add("");

                    if (Tools.Tools.AgentEnCours.IsChefEquipe || Tools.Tools.AgentEnCours.Role.CodeRole=="DC")
                    {
                        if (prospect.Commercial != null)
                            lviProspect.SubItems.Add(prospect.Commercial.NomComplet);
                    }

                    //    lvProspects.Columns.RemoveAt(5);
                    lviProspect.BackColor = (i % 2 != 0) ? Color.Beige : Color.White;

                    lviProspect.Tag = prospect;
                    lvProspects.Items.Add(lviProspect);
                    i++;
                }
              

            }
            catch (Exception)
            {

                throw;
            }
        }


        private void ChargerLesContrats( Agent agentConcerne)
        {
            try
            {
                var premierJourDeLAn = new DateTime(DateTime.Now.Year, 1, 1);
                var today = DateTime.Now;

                if (agentConcerne.Role.CodeRole == "CMC" || agentConcerne.Role.CodeRole == "DC")
                {
                    IEnumerable<Contrat> contrats = null;
                    if (agentConcerne.Role.CodeRole == "CMC" && !agentConcerne.IsChefEquipe)
                    {
                        contrats = contratRep.GetContratsCommercial(agentConcerne.Id).Where(vente =>vente.ProjetId==agentConcerne.ProjetId)
                         .Where(vente => vente.Statut == StatutContrat.Actif && vente.DateSouscription.Value >= premierJourDeLAn
                                    && vente.DateSouscription.Value <= today);
                    }
                    else if (agentConcerne.Role.CodeRole == "CMC" && agentConcerne.IsChefEquipe)
                    {
                        contrats = contratRep.GetContratsChefEquipe(agentConcerne.Id)
                         .Where(vente => vente.ProjetId == agentConcerne.ProjetId && vente.Statut == StatutContrat.Actif && vente.DateSouscription.Value>= premierJourDeLAn
                                    && vente.DateSouscription.Value <= today);
                    }
                    else
                    {
                        contrats = contratRep.GetContratsDC(agentConcerne.Id)
                         .Where(vente => vente.ProjetId == agentConcerne.ProjetId && vente.Statut == StatutContrat.Actif && vente.DateSouscription.Value >= premierJourDeLAn
                                    && vente.DateSouscription.Value <= today);
                    }
                    if (txtNomClient.Text != string.Empty)
                        contrats = contrats.Where(c => c.Client.Nom.ToLower().StartsWith(txtNomClient.Text.ToLower()));
                    if (txtPrenomClient.Text != string.Empty)
                        contrats = contrats.Where(c => c.Client.Prenom.ToLower().StartsWith(txtPrenomClient.Text.ToLower()));

                    if (txtTelClient.Text != string.Empty)
                        contrats = contrats.Where(c => c.Client.Mobile1.StartsWith(txtTelClient.Text));

                    if (txtEmailClient.Text != string.Empty)
                        contrats = contrats.Where(c => c.Client.Email.ToLower().StartsWith(txtEmailClient.Text.ToLower()));

                    lvVentes.Items.Clear();
                    int i = 0;

                    if (!Tools.Tools.AgentEnCours.IsChefEquipe)
                        if (lvVentes.Columns.Count > 5)
                            lvVentes.Columns.RemoveAt(5);
                    contrats = contrats.OrderByDescending(cont => cont.DateSouscription).ToList();
                    foreach (var contrat in contrats.ToList())
                    {
                        ListViewItem lviVente = new ListViewItem(contrat.DateSouscription.Value.ToShortDateString());
                        lviVente.SubItems.Add(contrat.Client.NomComplet);
                        lviVente.SubItems.Add(contrat.TypeContrat.CategorieContrat== CategorieContrat.Réservation?"Résa":"Dépôt");
                        lviVente.SubItems.Add(contrat.Lot.TypeVilla.CodeType);
                        lviVente.SubItems.Add(contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation ? contrat.Lot.NumeroLot : "");
                        if (Tools.Tools.AgentEnCours.IsChefEquipe)
                            lviVente.SubItems.Add(contrat.Client.Commercial.NomComplet);
                        if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                            lviVente.ImageIndex = 12;
                        else
                            lviVente.ImageIndex = 13;
                        //else
                        //    lvVentes.Columns.RemoveAt(5);
                        lviVente.Tag = contrat;
                        lviVente.BackColor = (i % 2 != 0) ? Color.Beige : Color.White;
                        lvVentes.Items.Add(lviVente);
                        i++;
                    }

                    lbNbVentes.Text = contrats.Count().ToString().PadLeft(2,'0');
                    aquaGauge1.MaxValue = 60;
                    aquaGauge1.Value = contrats.Count();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void lvActivitesCommerciales_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lvActivitesCommerciales.SelectedItems.Count > 0)
                {
                    int idActComm = (int)lvActivitesCommerciales.SelectedItems[0].Tag;
                    new FrmActiviteCommerciale(idActComm).ShowDialog();
                    AfficherActivitesCommercialesProspect(LAgentConcerne.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "¨Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lvProspects_DoubleClick(object sender, EventArgs e)
        {
            if (lvProspects.SelectedItems.Count > 0)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Client leProspect = (Client)lvProspects.SelectedItems[0].Tag;

                    FrmDossierProspect childForm = new FrmDossierProspect(leProspect);

                    childForm.StartPosition = FormStartPosition.CenterScreen;
                    childForm.ShowDialog();
                    childForm.WindowState = FormWindowState.Normal;
                    childForm.WindowState = FormWindowState.Maximized;

                    ChargerListeProspects(LAgentConcerne);
                    AfficherActivitesCommercialesProspect(LAgentConcerne.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
                    ChargerLesContrats(LAgentConcerne);
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
        }

        private void lvVentes_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (lvVentes.SelectedItems.Count > 0)
                {
                    Contrat leContrat = (Contrat)lvVentes.SelectedItems[0].Tag;
                    if (leContrat != null)
                    {
                        FrmDossierClient frmCli = new FrmDossierClient(leContrat);
                        //frmCli.MdiParent = this.MdiParent;
                        frmCli.ShowDialog();
                    }
                    else
                        MessageBox.Show(this, "Désolé ce contrat n'existe pas dans la base",
                            "Prosopis - Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors du chargement du client:... " + ex.Message,
                        "Prosopis - Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void chkAgrandirPanelProspect_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAgrandirPanelProspect.Checked)
            {
                //chkVoirListeApporteurs.Text = "Voir la liste des apporteurs d'affaires";
                spProspectContrat.Panel2Collapsed = false;
                spProspectContrat.Panel2.Show();
                pRechercheProspects.Visible = false;

            }
            else
            {
                spProspectContrat.Panel2Collapsed = true;
                spProspectContrat.Panel2.Hide();
                pRechercheProspects.Visible = true;
                //chkVoirListeApporteurs.Text = "Cacher la liste des apporteurs d'affaires";
            }
        }

        private void chkAgrandirPanelVentes_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAgrandirPanelVentes.Checked)
            {
                //chkVoirListeApporteurs.Text = "Voir la liste des apporteurs d'affaires";
                spProspectContrat.Panel1Collapsed = false;
                spProspectContrat.Panel1.Show();
                pRechercheClients.Visible = false;

            }
            else
            {
                spProspectContrat.Panel1Collapsed = true;
                spProspectContrat.Panel1.Hide();
                pRechercheClients.Visible = true;
                //chkVoirListeApporteurs.Text = "Cacher la liste des apporteurs d'affaires";
            }
        }

        private void cmdRechercherProspects_Click(object sender, EventArgs e)
        {
            try
            {
               AfficherLaListeDesProspects(LAgentConcerne);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors du chargement de la recherche de prospects:..." + ex.Message,
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }


        private void AfficherLaListeDesProspects(Agent agentConcerne)
        {
            try
            {
                ChargerListeProspects(agentConcerne);
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtPrenomRecherche_Validated(object sender, EventArgs e)
        {
            cmdRechercherProspects_Click(sender, e);
        }

        private void txtNomRecherche_Validated(object sender, EventArgs e)
        {
            cmdRechercherProspects_Click(sender, e);
        }

        private void txtEmailRecherche_Validated(object sender, EventArgs e)
        {
            cmdRechercherProspects_Click(sender, e);
        }

        private void txtTelephoneRecherche_Validated(object sender, EventArgs e)
        {
            cmdRechercherProspects_Click(sender, e);
        }

        private void txtPrenomRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdRechercherProspects_Click(sender, e);
        }

        private void txtNomRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdRechercherProspects_Click(sender, e);
        }

        private void txtEmailRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdRechercherProspects_Click(sender, e);
        }

        private void txtTelephoneRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdRechercherProspects_Click(sender, e);
        }

        private void lvVentes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdRechercherClients_Click(object sender, EventArgs e)
        {
            try
            {
               ChargerLesContrats(LAgentConcerne);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors du chargement de la recherche de clients:... " + ex.Message,
                        "Prosopis - Recherche de clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void ChargerLesClients(IEnumerable<Client> clients)
        {
            try
            {
                if (LAgentConcerne.Role.CodeRole == "CMC")
                {
                    if (LAgentConcerne.IsChefEquipe)
                        clients = clients.ToList().Where(pros => pros.Commercial.ChefEquipeId == LAgentConcerne.Id).ToList();
                    else
                        clients = clients.ToList().Where(pros => pros.CommercialID == LAgentConcerne.Id).ToList();
                }
            }
            catch (Exception )
            {
                throw;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lvActivitesCommerciales_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void spProspectContrat_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbCommerciaux_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cmbCommerciaux.SelectedItem != null)
            {
                LAgentConcerne = (Agent)cmbCommerciaux.SelectedItem;
            }
            else
            {
                LAgentConcerne = Tools.Tools.AgentEnCours;
            }
            try
            {
                AfficherActivitesCommercialesProspect(LAgentConcerne.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
                //var Clients = clientRep.GetProspects("", "", "", "").OrderByDescending(pros => pros.DateCreation).ToList();
                //if (LAgentConcerne.Role.CodeRole == "CMC")
                //{
                //    if (LAgentConcerne.IsChefEquipe)
                //    {
                //        Clients = Clients.ToList().Where(pros => pros.Commercial != null && pros.Commercial.ChefEquipeId == LAgentConcerne.Id).ToList();
                //    }
                //    else
                //    {
                //        Clients = Clients.ToList().Where(pros => pros.CommercialID == LAgentConcerne.Id).ToList();
                //    }
                //}
             
                ChargerLesContrats(LAgentConcerne);

                ChargerListeProspects(LAgentConcerne);
                ChargerLesVentes(LAgentConcerne);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:... " + ex.Message,
                        "Prosopis - Recherche de clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
          }

        private void button1_Click(object sender, EventArgs e)
        {
        //    var a = cmbCommerciaux.SelectedItem;
        //    var b = cmbCommerciaux.SelectedText;
        //    var c = cmbCommerciaux.SelectedValue;
        //    var d = cmbCommerciaux.SelectedIndex;
            cmbCommerciaux_SelectedIndexChanged(sender, e);
        }

        private void txtPrenomClient_Validated(object sender, EventArgs e)
        {
            cmdRechercherClients_Click(sender, e);
        }

        private void txtNomClient_Validated(object sender, EventArgs e)
        {
            cmdRechercherClients_Click(sender, e);
        }

        private void txtEmailClient_Validated(object sender, EventArgs e)
        {
            cmdRechercherClients_Click(sender, e);
        }

        private void txtTelClient_Validated(object sender, EventArgs e)
        {
            cmdRechercherClients_Click(sender, e);
        }

        private void txtPrenomClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdRechercherClients_Click(sender, e);
        }

        private void txtNomClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdRechercherClients_Click(sender, e);
        }

        private void txtEmailClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdRechercherClients_Click(sender, e);
        }

        private void txtTelClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdRechercherClients_Click(sender, e);
        }

        private void FrmAccueilCommercial_Activated(object sender, EventArgs e)
        {
            AfficherActivitesCommercialesProspect(LAgentConcerne.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
            ChargerLesContrats(LAgentConcerne);
            ChargerListeProspects(LAgentConcerne);
        }

        private void lvProspects_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ChargerLesVentes(Agent agent)
        {
            var premierJourDeLAn = new DateTime(DateTime.Now.Year, 1, 1);
            var today = DateTime.Now;

                    var query = contratRep.GetContratsCommercial(agent.Id).Where(vente => vente.DateSouscription.Value >= premierJourDeLAn
                                && vente.DateSouscription.Value <= today).ToList()
                                                       .GroupBy(contrat => new
                                                                    {
                                                                        contrat.TypeContrat
                                                                    }
                                                                )    
                                                       .Select(n => new
                                                                   {
                                                                       Typecontrat = n.Key.TypeContrat.LibelleTypeContrat,
                                                                       Nombre = n.Count(),
                                                                   }
                                                               )
                                                       .ToList().OrderBy(vente => vente.Typecontrat);
            chartVentes.Series.Clear();
            chartVentes.Series.Add("Ventes");


            chartVentes.Series["Ventes"].IsValueShownAsLabel = true;
            //chartVentes.ChartAreas[0].AxisY.label
            //chartVentes.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            //chartVentes.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            //chartVentes.ChartAreas[0].AxisX.LineWidth = 0;
            chartVentes.ChartAreas[0].AxisY.LineWidth = 0;
            //chartVentes.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chartVentes.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            chartVentes.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartVentes.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartVentes.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chartVentes.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            //chartVentes.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chartVentes.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            //chartVentes.ChartAreas[0].AxisX.MinorTickMark.Enabled = false;
            chartVentes.ChartAreas[0].AxisY.MinorTickMark.Enabled = false;



            chartVentes.Series["Ventes"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            //chartVentes.Series["Ventes"].LegendText = "Origines";

            foreach (var item in query)
            {

                chartVentes.Series["Ventes"].Points.AddXY(item.Typecontrat, item.Nombre);
            }
        }

        private void chartVentes_Click(object sender, EventArgs e)
        {

        }

        private void aquaGauge1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void annulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvActivitesCommerciales.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show(this, "Souhaitez vous réellement annuler cette activité commerciale?", "Prosopis -  Suppréssion d'activité commerciale", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        int idActComm = (int)lvActivitesCommerciales.SelectedItems[0].Tag;
                        //var acComm = clientRep.GetActivitesCommercialesById(idActComm);
                        clientRep.DeleteActiviteCommerciale(idActComm);
                        MessageBox.Show(this, "L'activité commerciale a été annulée avec succes",
                                                "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        AfficherActivitesCommercialesProspect(LAgentConcerne.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cloturerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvActivitesCommerciales.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show(this, "Souhaitez vous clôturer cette activité commerciale?", "Prosopis -  Clôture d'activité commerciale", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        int idActComm = (int)lvActivitesCommerciales.SelectedItems[0].Tag;
                        //var acComm = clientRep.GetActivitesCommercialesById(idActComm);
                        clientRep.CloturerActiviteCommerciale(idActComm);
                        MessageBox.Show(this, "L'activité commerciale a été clôturée avec succes",
                                                "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AfficherActivitesCommercialesProspect(LAgentConcerne.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
