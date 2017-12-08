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
    public partial class FrmCooperative : Form
    {
        private bool bModif;
        Cooperative laCooperativeEnCours;

        public FrmCooperative()
        {
            InitializeComponent();
            ChargerLesCooperatives();
            if (Tools.Tools.AgentEnCours.Role.CodeRole == "DC")
            {
                pCommandes.Enabled = false;
                cmdFermer.Enabled = true;
            }
        }

        private void FrmCooperative_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTelephoneMobile_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtTelephoneFixe_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAdresse_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdNouveau_Click(object sender, EventArgs e)
        {
            EffacerForm();
            bModif = false;
            DeverouillerClient();
        }

        private void EffacerForm()
        {
            txtDenomination.Text = string.Empty;
            txtTelephoneMobile.Text = string.Empty;
            txtAdresse.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelephoneMobile.Text = string.Empty;
            txtTelephoneFixe.Text = string.Empty;
            nudTauxCommission.Value = 0;

        }
        private void VerouillerForm()
        {
            txtDenomination.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtAdresse.ReadOnly = true;
            txtTelephoneMobile.ReadOnly = true;
            txtTelephoneFixe.ReadOnly = true;
            nudTauxCommission.Enabled = false;


            cmdNouveau.Enabled = true;
            cmdEnregistrer.Enabled = false;
            cmdEditer.Enabled = true;
            cmdSupprimer.Enabled = true;
        }

        private void DeverouillerClient()
        {
            txtDenomination.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtAdresse.ReadOnly = false;
            txtTelephoneMobile.ReadOnly = false;
            txtTelephoneFixe.ReadOnly = false;
            nudTauxCommission.Enabled = true;


            cmdNouveau.Enabled = false;
            cmdEnregistrer.Enabled = true;
            cmdEditer.Enabled = false;
            cmdSupprimer.Enabled = false;
        }

        private void cmdEditer_Click(object sender, EventArgs e)
        {
            bModif = true;
            DeverouillerClient();
        }

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDenomination.Text==string.Empty)
                {
                    MessageBox.Show(this, "Veuillez saisir la dénomination de la coopérative",
                             "Prosopis - Gestion des coopératives d'habitat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
                //if (nudTauxCommission.Value == 0)
                //{
                //    MessageBox.Show(this, "Veuillez saisir le taux de commission accordée à la coopérative",
                //             "Prosopis - Gestion des coopératives d'habitat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                using (var db = new SenImmoDataContext())
                {
                    if (!bModif)
                    {
                        laCooperativeEnCours = new Cooperative() ;
                    }
                    else
                    {
                        laCooperativeEnCours = db.Cooperatives.Find(laCooperativeEnCours.Id);
                    }
                    laCooperativeEnCours.DateSouscription = dtpDateSouscription.Value.Date;
                    laCooperativeEnCours.Denomination = txtDenomination.Text;
                    laCooperativeEnCours.Adresse = txtAdresse.Text;
                    laCooperativeEnCours.Email = txtEmail.Text;
                    laCooperativeEnCours.Mobile = txtTelephoneMobile.Text;
                    laCooperativeEnCours.Fixe = txtTelephoneFixe.Text;
                    laCooperativeEnCours.TauxRemise = nudTauxCommission.Value;
                    laCooperativeEnCours.AgentID = Tools.Tools.AgentEnCours.Id;
                    if (!bModif)
                    {
                        db.Cooperatives.Add(laCooperativeEnCours);
                    }
                    db.SaveChanges();
                }
                if (!bModif)
                {
                   
                    MessageBox.Show(this, "La coopérative a été enregistrée avec succes",
                                       "Prosopis - Gestion des coopératives d'habitat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EffacerForm();
                }
                else
                {
                    
                    MessageBox.Show(this, "La coopérative a été modifiée avec succes",
                                       "Prosopis - Gestion des coopératives d'habitat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    VerouillerForm();
                }

                ChargerLesCooperatives();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                        "Prosopis - Gestion des coopératives d'habitat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerLesCooperatives()
        {
            try
            {
                using (var db= new SenImmoDataContext())
                {
                    var cooperatives = db.Cooperatives.ToList();
                    lvCooperatives.Items.Clear();
                    foreach (var coop in cooperatives)
                    {
                        ListViewItem lviCooperative = new ListViewItem(String.Format("{0:dd/MM/yyyy}", coop.DateSouscription));
                        lviCooperative.SubItems.Add(coop.Denomination);
                        lviCooperative.SubItems.Add(coop.Mobile);
                        lviCooperative.SubItems.Add(coop.Mobile);
                        lviCooperative.SubItems.Add(coop.Email);
                        lviCooperative.SubItems.Add(coop.Adresse);
                        lviCooperative.SubItems.Add(coop.TauxRemise.ToString("###")+"%");
                        if(coop.Agent!=null)
                            lviCooperative.SubItems.Add(coop.Agent.NomComplet);
                        else
                            lviCooperative.SubItems.Add("");
                        lviCooperative.Tag = coop;
                        lvCooperatives.Items.Add(lviCooperative);
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                        "Prosopis - Gestion des coopératives d'habitat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvCooperatives_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvCooperatives.SelectedItems.Count > 0)
                {
                    var coop = lvCooperatives.SelectedItems[0].Tag as Cooperative;
                    dtpDateSouscription.Value = coop.DateSouscription.HasValue == true ? coop.DateSouscription.Value.Date : DateTime.Parse("01/01/1900");
                    txtDenomination.Text = coop.Denomination;
                    txtTelephoneFixe.Text = coop.Fixe;
                    txtTelephoneMobile.Text = coop.Mobile;
                    txtEmail.Text = coop.Email;
                    txtAdresse.Text = coop.Adresse;
                    nudTauxCommission.Value = coop.TauxRemise;
                    laCooperativeEnCours = coop;

                    ChargerLesMembres(coop.Id);
                }
                VerouillerForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                        "Prosopis - Gestion des coopératives d'habitat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerLesMembres(int id)
        {
            try
            {
                using (var db = new SenImmoDataContext())
                {
                    var membres = db.Clients.Where(membre =>membre.CooperativeId==id).ToList();
                    lvMembres.Items.Clear();
                    foreach (var client in membres)
                    {
                        ListViewItem lviMembre = new ListViewItem(String.Format("{0:dd/MM/yyyy}", client.DateSouscription));
                        lviMembre.SubItems.Add(client.NomComplet);
                        lviMembre.SubItems.Add(client.Mobile1);
                        if (client.Type == TypeClient.ProspectSansOption || client.Type == TypeClient.ProspectAvecOptionDepot || client.Type == TypeClient.ProspectAvecOptionResa)
                        {
                            lviMembre.SubItems.Add("Prospect");
                            if (client.Type == TypeClient.ProspectAvecOptionDepot || client.Type == TypeClient.ProspectAvecOptionResa)
                            {
                                lviMembre.SubItems.Add(client.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeContrat.CategorieContrat.ToString());

                                lviMembre.SubItems.Add(client.Options.Where(opt =>opt.Active==true).FirstOrDefault().TypeVilla.NomComplet);
                                if (client.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                                    lviMembre.SubItems.Add(client.Options.Where(opt => opt.Active == true).FirstOrDefault().Lot.NumeroLot);
                                else
                                    lviMembre.SubItems.Add("");
                               lviMembre.SubItems.Add(client.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente.ToString("### ### ###"));
                            }
                            else
                            {
                                lviMembre.SubItems.Add("");
                                lviMembre.SubItems.Add("");
                                lviMembre.SubItems.Add("");
                                lviMembre.SubItems.Add("");
                            }
                        }
                        else
                        { 
                            lviMembre.SubItems.Add("Client");
                            lviMembre.SubItems.Add(client.Contrats.FirstOrDefault().TypeContrat.CategorieContrat.ToString());
                            lviMembre.SubItems.Add(client.Contrats.FirstOrDefault().Lot.TypeVilla.NomComplet);
                            if(client.Contrats.FirstOrDefault().TypeContrat.CategorieContrat== CategorieContrat.Réservation)
                            {
                                lviMembre.SubItems.Add(client.Contrats.FirstOrDefault().Lot.NumeroLot);
                            }
                            else
                                lviMembre.SubItems.Add("");
                            lviMembre.SubItems.Add(client.Contrats.FirstOrDefault().PrixFinal.ToString("### ### ###"));
                        }
                        //lviMembre.SubItems.Add(client.Adresse);
                        //lviMembre.SubItems.Add(client.TauxRemise.ToString());
                        lviMembre.Tag = client;
                        lvMembres.Items.Add(lviMembre);
                    }
                    txtNbAdherents.Text = membres.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                        "Prosopis - Gestion des coopératives d'habitat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdSupprimer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(this, "Voulez vous réellement supprimer cette coopérative?",
                                            "Prosopis - Gestion des coopératives d'habitat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (var db = new SenImmoDataContext())
                    {
                        db.Cooperatives.Remove( db.Cooperatives.Find(laCooperativeEnCours.Id));
                        db.SaveChanges();
                        MessageBox.Show(this, "La coopérative a été supprimée avec succes",
                                                "Prosopis - Gestion des coopératives d'habitat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ChargerLesCooperatives();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des coopératives d'habitat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdAjouterMembre_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProspect frmPros = new FrmProspect(laCooperativeEnCours);
                frmPros.StartPosition = FormStartPosition.CenterScreen;
                frmPros.ShowDialog();
                //ChargerLesProspect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                     "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtDenomination_TextChanged(object sender, EventArgs e)
        {

        }

        private void lvMembres_DoubleClick(object sender, EventArgs e)
        {
            if (lvMembres.SelectedItems.Count > 0)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Client client = (Client)lvMembres.SelectedItems[0].Tag;
                    if (client.Type == TypeClient.ProspectSansOption || client.Type == TypeClient.ProspectAvecOptionDepot || client.Type == TypeClient.ProspectAvecOptionResa)
                    {
                        FrmDossierProspect childForm = new FrmDossierProspect(client);
                        childForm.MdiParent = this.MdiParent;
                        // childForm.StartPosition = FormStartPosition.CenterScreen;

                        childForm.WindowState = FormWindowState.Normal;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    else
                    {
                        FrmDossierClient childForm = new FrmDossierClient(client);
                        childForm.MdiParent = this.MdiParent;
                        // childForm.StartPosition = FormStartPosition.CenterScreen;

                        childForm.WindowState = FormWindowState.Normal;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    //cmdRechercher_Click(sender, e);
                    //AfficherLaListeDesProspects(clientRep.GetProspects(txtNomRecherche.Text, txtPrenomRecherche.Text, txtTelephoneRecherche.Text, txtEmailRecherche.Text).OrderByDescending(pros => pros.DateCreation));
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
    }
}
