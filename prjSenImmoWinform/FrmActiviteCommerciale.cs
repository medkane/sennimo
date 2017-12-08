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
    public partial class FrmActiviteCommerciale : Form
    {
        private Models.Client LeClientEnCours;

        private Models.Client leProspectEnCours;

        private TypeActivite LeTypeActiviteCommerciale;

        private ActiviteCommerciale ActiviteCommercialeEnCours;

        private ClientRepository clientDAL;

        private bool bModif;
        private bool bModifNote;
        private NoteProspect laNoteEnCours;

        public FrmActiviteCommerciale()
        {
            InitializeComponent();
            dtpHeureActivite.CustomFormat = "HH:mm";
            dtpHeureRappel.CustomFormat = "HH:mm";
            cmbTypeActiviteCommerciales.DataSource = Enum.GetValues(typeof(TypeActivite));
            cmbTypeActiviteCommerciales.SelectedIndex = -1;
            chkNePlusRappeler.Visible = false;
            tcRappel.ItemSize = new Size(0, 1);
            tcRappel.SizeMode = TabSizeMode.Fixed;
            clientDAL = new ClientRepository();
            tcDescriptionNotes.TabPages.Remove(tabPage4);

        }

        public FrmActiviteCommerciale(int activiteCommercialeId) : this()
        {
            try
            {
                if (Tools.Tools.AgentEnCours.Role.CodeRole != "CMC")
                {
                    pCommandes.Enabled = false;
                    cmdFermer.Enabled = true;
                    chkNePlusRappeler.Enabled = false;
                }

                ActiviteCommercialeEnCours = clientDAL.GetActivitesCommercialesById(activiteCommercialeId);
                leProspectEnCours = ActiviteCommercialeEnCours.Client;
                AfficherProspect(leProspectEnCours);
                cmbTypeActiviteCommerciales.SelectedItem = ActiviteCommercialeEnCours.TypeActivite;
                dtpDateActivite.Value = ActiviteCommercialeEnCours.DateActivite;
                dtpHeureActivite.Value = ActiviteCommercialeEnCours.DateActivite + ActiviteCommercialeEnCours.HeureActivite;
                txtCommentaire.Text = ActiviteCommercialeEnCours.Commentaire;

                chkNePlusRappeler.Visible = true;
                if (ActiviteCommercialeEnCours.BRappel)
                    chkNePlusRappeler.Checked = false;
                else
                    chkNePlusRappeler.Checked = true;

               // chkNePlusRappeler.Checked = true;

                switch (ActiviteCommercialeEnCours.Priorite)
                {
                    case Priorite.Faible:
                        rbFaible.Checked = true;
                        break;
                    case Priorite.Moyenne:
                        rbMoyenne.Checked = true;
                        break;
                    case Priorite.Haute:
                        rbHaute.Checked = true;
                        break;
                    default:
                        break;
                }
                tcRappel.SelectedIndex = 1;
                if (ActiviteCommercialeEnCours.DateRappel != null && ActiviteCommercialeEnCours.HeureRappel != null)
                {
                    dtpDateRappel.Value = ActiviteCommercialeEnCours.DateRappel.Value;
                    dtpHeureRappel.Value = ActiviteCommercialeEnCours.DateRappel.Value + ActiviteCommercialeEnCours.HeureRappel.Value;

                }
                else
                    tcRappel.Visible = false;
                tcDescriptionNotes.TabPages.Add(tabPage4);
                AfficherNotesProspect(ActiviteCommercialeEnCours.Id);
                VerouillerForm();
                switch (ActiviteCommercialeEnCours.StatutActiviteCommerciale)
                {
                    case StatutActiviteCommerciale.Annulée:
                        chkAnnuler.Checked = true;
                        chkAnnuler.Enabled = false;
                        cmdEditer.Enabled = false;
                        cmdEnregistrer.Enabled = false;
                        break;
                    case StatutActiviteCommerciale.Exécutée:
                        chkCloturer.Checked = true;
                        chkCloturer.Enabled = false;
                        chkAnnuler.Visible = false;
                        cmdEditer.Enabled = false;
                        cmdEnregistrer.Enabled = false;
                        chkNePlusRappeler.Enabled = false;
                        break;
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                     "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public FrmActiviteCommerciale(Models.Client prospect) : this()
        {
            leProspectEnCours = clientDAL.GetClient(prospect.ID);
            AfficherProspect(leProspectEnCours);
            tcRappel.SelectedIndex = 0;
        }

        private void AfficherProspect(Client prospect)
        {
            txtPrenom.Text = prospect.NomComplet;
            txtTelephoneMobile.Text = prospect.Mobile1;
            txtEmail.Text = prospect.Email;
            //txtLieuNaissance.Text = prospect.LieuDeNaissance;
            //txtAdresse.Text = prospect.Adresse;

        }

        private void cmbTypeActiviteCommerciales_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbTypeActiviteCommerciales.SelectedItem != null)
            {
                LeTypeActiviteCommerciale = (TypeActivite)cmbTypeActiviteCommerciales.SelectedItem;
                txtAutreActivite.Text = string.Empty;
                txtAutreActivite.Visible = LeTypeActiviteCommerciale == TypeActivite.Autre ? true : false;
            }
        }

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            if (cmbTypeActiviteCommerciales.SelectedItem == null)
            {
                MessageBox.Show(this, "Veuillez choisir le type d'activité commerciale:",
                     "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (txtDureeOption.Text == string.Empty)
            //{
            //    MessageBox.Show(this, "Erreur: veuillez selectionner la date de début de la mise en option et saisir la durée sous forme de nombre de jours...",
            //                 "Prosopis -  Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //var duree = Int16.Parse(txtDureeOption.Text);
            //var dateFinPriseOption = dtpDatePriseOption.Value.AddDays(duree).Date;
            try
            {
                var leCommercialEnCours = Tools.Tools.AgentEnCours;

                Priorite priorite = 0;
                if (rbFaible.Checked)
                    priorite = Priorite.Faible;
                else
                    if (rbMoyenne.Checked)
                    priorite = Priorite.Moyenne;
                else
                    if (rbHaute.Checked)
                    priorite = Priorite.Haute;

                if (!bModif)
                {


                    var newActiviteCommerciale = new ActiviteCommerciale()
                    {
                        ClientID = leProspectEnCours.ID,
                        DateActivite = dtpDateActivite.Value.Date,
                        HeureActivite = dtpHeureActivite.Value.TimeOfDay,
                        TypeActivite = LeTypeActiviteCommerciale,
                        CommercialID = leCommercialEnCours.Id,
                        AutreTypeActivite = txtAutreActivite.Text,
                        Commentaire = txtCommentaire.Text,
                        Priorite = priorite,
                        StatutActiviteCommerciale = StatutActiviteCommerciale.NonEchue
                    };
                    if (cmbDureeRappel.SelectedItem != null)
                    {
                        var rappel = cmbDureeRappel.SelectedItem;

                        switch (rappel.ToString())
                        {
                            case "5 minutes":
                                newActiviteCommerciale.DateRappel = dtpDateActivite.Value.Date;
                                newActiviteCommerciale.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromMinutes(-5));
                                break;
                            case "10 minutes":
                                newActiviteCommerciale.DateRappel = dtpDateActivite.Value.Date;
                                newActiviteCommerciale.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromMinutes(-10));
                                break;
                            case "15 minutes":
                                newActiviteCommerciale.DateRappel = dtpDateActivite.Value.Date;
                                newActiviteCommerciale.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromMinutes(-15));
                                break;
                            case "30 minutes":
                                newActiviteCommerciale.DateRappel = dtpDateActivite.Value.Date;
                                newActiviteCommerciale.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromMinutes(-30));
                                break;
                            case "45 minutes":
                                newActiviteCommerciale.DateRappel = dtpDateActivite.Value.Date;
                                newActiviteCommerciale.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromMinutes(-45));
                                break;
                            case "1 heure":
                                newActiviteCommerciale.DateRappel = dtpDateActivite.Value.Date;
                                newActiviteCommerciale.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromHours(-1));
                                break;
                            case "2 heures":
                                newActiviteCommerciale.DateRappel = dtpDateActivite.Value.Date;
                                newActiviteCommerciale.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromHours(-2));
                                break;
                            case "1 jour":
                                newActiviteCommerciale.DateRappel = dtpDateActivite.Value.Date.AddDays(-1);
                                newActiviteCommerciale.HeureRappel = dtpHeureActivite.Value.TimeOfDay;
                                break;
                            default:
                                break;
                        }
                        newActiviteCommerciale.BRappel = true;
                    }


                    clientDAL.AddProspetActiviteCommerciale(newActiviteCommerciale);
                    if (Tools.Tools.AgentEnCours.Id == leProspectEnCours.CommercialID && leProspectEnCours.ProspectEdite == false)
                    {
                        leProspectEnCours.ProspectEdite = true;
                        clientDAL.SaveChanges();

                    }
                    this.Close();
                }
                else
                {
                    ActiviteCommercialeEnCours = clientDAL.GetActivitesCommercialesById(ActiviteCommercialeEnCours.Id);
                    ActiviteCommercialeEnCours.ClientID = leProspectEnCours.ID;
                    ActiviteCommercialeEnCours.DateActivite = dtpDateActivite.Value.Date;
                    ActiviteCommercialeEnCours.HeureActivite = dtpHeureActivite.Value.TimeOfDay;
                    ActiviteCommercialeEnCours.TypeActivite = LeTypeActiviteCommerciale;
                    ActiviteCommercialeEnCours.CommercialID = leCommercialEnCours.Id;
                    ActiviteCommercialeEnCours.AutreTypeActivite = txtAutreActivite.Text;
                    ActiviteCommercialeEnCours.Commentaire = txtCommentaire.Text;
                    ActiviteCommercialeEnCours.Priorite = priorite;

                    if (cmbDureeRappel.SelectedItem != null)
                    {
                        var rappel = cmbDureeRappel.SelectedItem;

                        switch (rappel.ToString())
                        {
                            case "5 minutes":
                                ActiviteCommercialeEnCours.DateRappel = dtpDateActivite.Value.Date;
                                ActiviteCommercialeEnCours.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromMinutes(-5));
                                break;
                            case "10 minutes":
                                ActiviteCommercialeEnCours.DateRappel = dtpDateActivite.Value.Date;
                                ActiviteCommercialeEnCours.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromMinutes(-10));
                                break;
                            case "15 minutes":
                                ActiviteCommercialeEnCours.DateRappel = dtpDateActivite.Value.Date;
                                ActiviteCommercialeEnCours.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromMinutes(-15));
                                break;
                            case "30 minutes":
                                ActiviteCommercialeEnCours.DateRappel = dtpDateActivite.Value.Date;
                                ActiviteCommercialeEnCours.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromMinutes(-30));
                                break;
                            case "45 minutes":
                                ActiviteCommercialeEnCours.DateRappel = dtpDateActivite.Value.Date;
                                ActiviteCommercialeEnCours.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromMinutes(-45));
                                break;
                            case "1 heure":
                                ActiviteCommercialeEnCours.DateRappel = dtpDateActivite.Value.Date;
                                ActiviteCommercialeEnCours.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromHours(-1));
                                break;
                            case "2 heures":
                                ActiviteCommercialeEnCours.DateRappel = dtpDateActivite.Value.Date;
                                ActiviteCommercialeEnCours.HeureRappel = dtpHeureActivite.Value.TimeOfDay.Add(TimeSpan.FromHours(-2));
                                break;
                            case "1 jour":
                                ActiviteCommercialeEnCours.DateRappel = dtpDateActivite.Value.Date.AddDays(-1);
                                ActiviteCommercialeEnCours.HeureRappel = dtpHeureActivite.Value.TimeOfDay;
                                break;
                            default:
                                break;
                        }
                        ActiviteCommercialeEnCours.BRappel = !chkNePlusRappeler.Checked;
                    }
                    if (Tools.Tools.AgentEnCours.Id == leProspectEnCours.CommercialID && leProspectEnCours.ProspectEdite == false)
                    {
                        leProspectEnCours.ProspectEdite = true;
                    }
                    clientDAL.SaveChanges();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                     "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAnnuler_Click(object sender, EventArgs e)
        {

        }

        private void cmbDureeRappel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkNePlusRappeler_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNePlusRappeler.Checked)
            {
                var actComm = clientDAL.GetActivitesCommercialesById(ActiviteCommercialeEnCours.Id);
                actComm.BRappel = false;
            }
            else
            {
                var actComm = clientDAL.GetActivitesCommercialesById(ActiviteCommercialeEnCours.Id);
                actComm.BRappel = true;
            }
            clientDAL.SaveChanges();
        }

        private void VerouillerForm()
        {
            cmbTypeActiviteCommerciales.Enabled = false;
            dtpDateActivite.Enabled = false;
            dtpHeureActivite.Enabled = false;
            cmbDureeRappel.Enabled = false;
            dtpDateRappel.Enabled = false;
            dtpHeureRappel.Enabled = false;
            rbFaible.Enabled = false;
            rbMoyenne.Enabled = false;
            rbHaute.Enabled = false;
            txtCommentaire.ReadOnly = true;
            txtNote.ReadOnly = true;
            cmdAjouterNote.Enabled = false;
            cmbDureeRappel.Enabled = false;
            lvNotes.Enabled = false;

            chkAnnuler.Enabled = false;
            chkCloturer.Enabled = false;

            cmdEnregistrer.Enabled = false;
            cmdEditer.Enabled = true;

        }

        private void DeVerouillerForm()
        {
            cmbTypeActiviteCommerciales.Enabled = true;
            dtpDateActivite.Enabled = true;
            dtpHeureActivite.Enabled = true;
            cmbDureeRappel.Enabled = true;
            dtpDateRappel.Enabled = true;
            dtpHeureRappel.Enabled = true;

            rbFaible.Enabled = true;
            rbMoyenne.Enabled = true;
            rbHaute.Enabled = true;
            txtCommentaire.ReadOnly = false;
            txtNote.ReadOnly = false;
            cmdAjouterNote.Enabled = true;
            cmbDureeRappel.Enabled = true;
            lvNotes.Enabled = true;
            chkAnnuler.Enabled = true;
            chkCloturer.Enabled = true;

            cmdEnregistrer.Enabled = true;
            cmdEditer.Enabled = false;
        }

        private void cmdEditer_Click(object sender, EventArgs e)
        {
            bModif = true;
            DeVerouillerForm();
            tcRappel.Visible = true;
            tcRappel.SelectedIndex = 0;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdAjouterNote_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNote.Text != string.Empty)
                {
                    if (!bModifNote)
                        clientDAL.AjouterNote(leProspectEnCours.ID, ActiviteCommercialeEnCours.Id, txtNote.Text);
                    else
                        clientDAL.ModifierNote(laNoteEnCours.ID, txtNote.Text);
                    
                    AfficherNotesProspect(ActiviteCommercialeEnCours.Id);
                    txtNote.Text = string.Empty;
                    bModifNote = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                  "Prosopis - Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherNotesProspect(int activiteId)
        {
            try
            {
                var noteProspects = clientDAL.GetNotesActivite(activiteId).OrderByDescending(note => note.DateDebutTypeOrigine);

                lvNotes.Items.Clear();
                int i = 0;
                foreach (var note in noteProspects.ToList())
                {
                    ListViewItem lviNote = new ListViewItem(note.DateDebutTypeOrigine.Value.ToShortDateString());
                    lviNote.SubItems.Add(note.Comentaire);
                    lviNote.Tag = note;
                    lvNotes.Items.Add(lviNote);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chkAnnuler_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAnnuler.Checked && bModif)
                {
                    if (ActiviteCommercialeEnCours != null)
                    {
                        if (MessageBox.Show(this, "Souhaitez vous réellement annuler cette activité commerciale?", "Prosopis -  Suppréssion d'activité commerciale", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {

                            clientDAL.DeleteActiviteCommerciale(ActiviteCommercialeEnCours.Id);
                            MessageBox.Show(this, "L'activité commerciale a été annulée avec succes",
                                                    "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkCloturer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCloturer.Checked)
                {
                    if (ActiviteCommercialeEnCours != null && ActiviteCommercialeEnCours.StatutActiviteCommerciale != StatutActiviteCommerciale.Exécutée)
                    {
                        if (MessageBox.Show(this, "Souhaitez vous réellement clôturer cette activité commerciale?", "Prosopis -  Suppréssion d'activité commerciale", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {

                            clientDAL.CloturerActiviteCommerciale(ActiviteCommercialeEnCours.Id);
                            MessageBox.Show(this, "L'activité commerciale a été clôturée avec succes",
                                                    "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmActiviteCommerciale_Load(object sender, EventArgs e)
        {

        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (lvNotes.SelectedItems.Count > 0)
                {
                    bModifNote = true;
                    laNoteEnCours = (NoteProspect)lvNotes.SelectedItems[0].Tag;
                    txtNote.Text = laNoteEnCours.Comentaire;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(this, "Souhaitez vous réellement supprimer cette note?", "Prosopis - Gestion des activités commerciales", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    clientDAL.SupprimerNote(laNoteEnCours.ID);
                    AfficherNotesProspect(ActiviteCommercialeEnCours.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void lvNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (lvNotes.SelectedItems.Count > 0)
                {
                    laNoteEnCours = (NoteProspect)lvNotes.SelectedItems[0].Tag;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
