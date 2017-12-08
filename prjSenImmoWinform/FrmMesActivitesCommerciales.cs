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
    public partial class FrmMesActivitesCommerciales : Form
    {
        private ClientRepository clientRep;
        private CommercialRepository commercialRep;
        private ContratRepository contratRep;
        private Projet leProjetEnCours;

        public FrmMesActivitesCommerciales()
        {
            InitializeComponent();
            clientRep = new ClientRepository();
            contratRep = new ContratRepository();
            dtpDateDebutCalendar.Value = DateTime.Now;
            dtpDateFinCalendar.Value = DateTime.Now;
            ChargerLesProjets();
            commercialRep = new CommercialRepository();
            if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" && Tools.Tools.AgentEnCours.IsChefEquipe == false)
            {
                chkCommercial.Visible = false;
            }
            else
                ChargerCommerciaux();


            if (Tools.Tools.AgentEnCours.Role.CodeRole=="CMC")
                AfficherActivitesCommercialesProspect(Tools.Tools.AgentEnCours.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
            else
            {
                var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                dtpDateDebutCalendar.Value = monday;

                var saturday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Saturday);
                dtpDateFinCalendar.Value = saturday;
                AfficherActivitesCommercialesProspect(dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
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
                // cmbProjets.SelectedValue = leProjetEnCours;
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" || Tools.Tools.AgentEnCours.Role.CodeRole == "DC")
                {
                    cmbProjets.SelectedValue = Tools.Tools.AgentEnCours.ProjetId;
                    //LeProjetEncours = Tools.Tools.AgentEnCours.Projet;
                    cmbProjets.Enabled = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void lvActivitesCommerciales_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void AfficherActivitesCommercialesProspect(int commercialId, DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                lvActivitesCommerciales.Items.Clear();
                var dateDb = dateDebut.Date;
                var dateFn = dateFin.Date;
                var activitesCommerciales = clientRep.GetActivitesCommerciales(commercialId, dateDb, dateFn).OrderByDescending(act => act.DateActivite).ThenBy(act => act.HeureActivite).ToList();
                if (Tools.Tools.AgentEnCours.Id!=commercialId && lvActivitesCommerciales.Columns.Count <= 5)
                {
                    lvActivitesCommerciales.Columns.Add("Comerciale");
                    lvActivitesCommerciales.Columns[5].Width = 150;
                }
                if(chkExecutee.Checked)
                {
                    activitesCommerciales = activitesCommerciales.Where(act => act.StatutActiviteCommerciale == StatutActiviteCommerciale.Exécutée).ToList();
                }
                if (chkNonEchue.Checked)
                {
                    activitesCommerciales = activitesCommerciales.Where(act => act.StatutActiviteCommerciale == StatutActiviteCommerciale.NonEchue).ToList();
                }
                if (chkEchueNonExecutee.Checked)
                {
                    activitesCommerciales = activitesCommerciales.Where(act => act.StatutActiviteCommerciale == StatutActiviteCommerciale.EchueNonExecutée).ToList();
                }
                if (chkAnnulee.Checked)
                {
                    activitesCommerciales = activitesCommerciales.Where(act => act.StatutActiviteCommerciale == StatutActiviteCommerciale.Annulée).ToList();
                }
                foreach (var ac in activitesCommerciales)
                {
                    ListViewItem lviAc = new ListViewItem(ac.DateActivite.ToShortDateString());
                    lviAc.SubItems.Add(ac.HeureActivite.ToString().Substring(0, 5));
                    lviAc.SubItems.Add(ac.Client.NomComplet);
                    lviAc.SubItems.Add(ac.TypeActivite.ToString());
                    lviAc.SubItems.Add(ac.Commentaire);
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
                    if (Tools.Tools.AgentEnCours.Id != commercialId && lvActivitesCommerciales.Columns.Count >= 5)
                    {
                        lviAc.SubItems.Add(ac.Commercial.NomComplet);
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



        private void AfficherActivitesCommercialesProspect( DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                lvActivitesCommerciales.Items.Clear();
                var dateDb = dateDebut.Date;
                var dateFn = dateFin.Date;
                var activitesCommerciales = clientRep.GetActivitesCommerciales(dateDb, dateFn).OrderByDescending(act => act.DateActivite).ThenBy(act => act.HeureActivite).ToList();

                if (cmbProjets.SelectedItem != null)
                {
                    var projetId = (int)cmbProjets.SelectedValue;
                    activitesCommerciales = activitesCommerciales.Where(act => act.Commercial.ProjetId == projetId).ToList();
                }


                if (cmbCommercial.SelectedItem != null)
                {
                    var commercialId = (int)cmbCommercial.SelectedValue;
                    activitesCommerciales = activitesCommerciales.Where(act => act.CommercialID== commercialId).ToList();
                }

                if (lvActivitesCommerciales.Columns.Count<=5)
                { 
                    lvActivitesCommerciales.Columns.Add("Comerciale");
                    lvActivitesCommerciales.Columns[5].Width = 150;
                }



                if (chkExecutee.Checked)
                {
                    activitesCommerciales = activitesCommerciales.Where(act => act.StatutActiviteCommerciale == StatutActiviteCommerciale.Exécutée).ToList();
                }
                if (chkNonEchue.Checked)
                {
                    activitesCommerciales = activitesCommerciales.Where(act => act.StatutActiviteCommerciale == StatutActiviteCommerciale.NonEchue).ToList();
                }
                if (chkEchueNonExecutee.Checked)
                {
                    activitesCommerciales = activitesCommerciales.Where(act => act.StatutActiviteCommerciale == StatutActiviteCommerciale.EchueNonExecutée).ToList();
                }
                if (chkAnnulee.Checked)
                {
                    activitesCommerciales = activitesCommerciales.Where(act => act.StatutActiviteCommerciale == StatutActiviteCommerciale.Annulée).ToList();
                }
                foreach (var ac in activitesCommerciales)
                {
                    ListViewItem lviAc = new ListViewItem(ac.DateActivite.ToShortDateString());
                    lviAc.SubItems.Add(ac.HeureActivite.ToString().Substring(0, 5));
                    lviAc.SubItems.Add(ac.Client.NomComplet);
                    lviAc.SubItems.Add(ac.TypeActivite.ToString());
                    lviAc.SubItems.Add(ac.Commentaire);
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
                    lviAc.SubItems.Add(ac.Commercial.NomComplet);
                    lviAc.Tag = ac.Id;

                    lvActivitesCommerciales.Items.Add(lviAc);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpDateDebutCalendar_ValueChanged(object sender, EventArgs e)
        {
            //if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
            //    AfficherActivitesCommercialesProspect(Tools.Tools.AgentEnCours.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
            //else
            //    AfficherActivitesCommercialesProspect(dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
        }

        private void dtpDateFinCalendar_ValueChanged(object sender, EventArgs e)
        {
            //if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
            //    AfficherActivitesCommercialesProspect(Tools.Tools.AgentEnCours.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
            //else
            //    AfficherActivitesCommercialesProspect(dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
        }

        private void lvActivitesCommerciales_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lvActivitesCommerciales.SelectedItems.Count > 0)
                {
                    int idActComm = (int)lvActivitesCommerciales.SelectedItems[0].Tag;
                    new FrmActiviteCommerciale(idActComm).ShowDialog();
                    //if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                    //    AfficherActivitesCommercialesProspect(Tools.Tools.AgentEnCours.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
                    //else
                    //    AfficherActivitesCommercialesProspect(dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmActivite_Opening(object sender, CancelEventArgs e)
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
                        if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                            AfficherActivitesCommercialesProspect(Tools.Tools.AgentEnCours.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
                        else
                            AfficherActivitesCommercialesProspect(dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
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
                        if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                            AfficherActivitesCommercialesProspect(Tools.Tools.AgentEnCours.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
                        else
                            AfficherActivitesCommercialesProspect(dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void FrmMesActivitesCommerciales_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void ChargerCommerciaux()
        {

            try
            {
                var lesCommerciaux = commercialRep.GetAllCommerciaux();
                if (Tools.Tools.AgentEnCours.IsChefEquipe)
                    lesCommerciaux = lesCommerciaux.Where(c => c.ChefEquipeId == Tools.Tools.AgentEnCours.Id);

                if(cmbProjets.SelectedItem!=null)
                {
                    lesCommerciaux = lesCommerciaux.Where(c => c.ProjetId == leProjetEnCours.Id);
                }
                cmbCommercial.DataSource = lesCommerciaux.ToList();
                cmbCommercial.DisplayMember = "NomComplet";
                cmbCommercial.ValueMember = "Id";
                cmbCommercial.SelectedIndex = -1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void chkCommercial_CheckedChanged(object sender, EventArgs e)
        {
            if(chkCommercial.Checked)
             ChargerCommerciaux();
            cmbCommercial.SelectedIndex = -1;
            cmbCommercial.Visible = chkCommercial.Checked;


            //this.Cursor = Cursors.WaitCursor;
            //if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
            //    AfficherActivitesCommercialesProspect(Tools.Tools.AgentEnCours.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
            //else
            //    AfficherActivitesCommercialesProspect(dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
            //this.Cursor = Cursors.Default;
        }

        private void cmbCommercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCommercial.SelectedItem != null)
                {
                    // commercial = commercialRep.FindById(cmbCommercial.SelectedValue);
                    //this.Cursor = Cursors.WaitCursor;
                    ////if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                    //AfficherActivitesCommercialesProspect((cmbCommercial.SelectedItem as Agent).Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
                    ////else
                    ////    AfficherActivitesCommercialesProspect(dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
                    //this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                AfficherActivitesCommercialesProspect(Tools.Tools.AgentEnCours.Id, dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
            else
                AfficherActivitesCommercialesProspect(dtpDateDebutCalendar.Value, dtpDateFinCalendar.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkNonEchue_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (chkNonEchue.Checked)
            {

                chkAnnulee.Checked = false;
                chkEchueNonExecutee.Checked = false;
                chkExecutee.Checked = false;

            }
           // cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void chkExecutee_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (chkExecutee.Checked)
            {

                chkAnnulee.Checked = false;
                chkEchueNonExecutee.Checked = false;
                chkNonEchue.Checked = false;

            }
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void chkAnnulee_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (chkAnnulee.Checked)
            {

                chkExecutee.Checked = false;
                chkEchueNonExecutee.Checked = false;
                chkNonEchue.Checked = false;

            }
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void chkEchueNonExecutee_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (chkEchueNonExecutee.Checked)
            {

                chkExecutee.Checked = false;
                chkAnnulee.Checked = false;
                chkNonEchue.Checked = false;

            }
            //cmdRechercher_Click(sender, e);
            this.Cursor = Cursors.Default;
        }

        private void cmbProjets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProjets.SelectedItem != null)
            {
                chkCommercial.Checked = false;
            }
            else
                leProjetEnCours = null;
            leProjetEnCours = cmbProjets.SelectedItem as Projet;
        }

        private void cmbProjets_Validated(object sender, EventArgs e)
        {
            if (cmbProjets.SelectedItem == null)
            {
                chkCommercial.Checked = false;
            }
            else
                leProjetEnCours = null;
            leProjetEnCours = cmbProjets.SelectedItem as Projet;
        }
    }
}
