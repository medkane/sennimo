using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prjSenImmoWinform.DAL;
using prjSenImmoWinform.Models;
namespace prjSenImmoWinform
{
    public partial class FrmAffectationCommercial : Form
    {
        private Client leProspect;
        private Agent leCommercialEnCours;
        private ContratRepository contratRep;
        public CommercialRepository iCommercialRepository { get; set; }
        public Agent LAncienCommercial { get; private set; }
        public string Source { get; set; }
        private SenImmoDataContext db;
        private Client leProspectEnCours;
        private bool BReaffectation = false;
        private Projet leProjetEnCours;

        public FrmAffectationCommercial()
        {
            InitializeComponent();
            dtpDateAffectation.Value = DateTime.Today;
            //iCommercialRepository=new CommercialRepository();
            db = new SenImmoDataContext();
            contratRep = new ContratRepository();
            ChargerLesProjets();
            ChargerCommerciaux();
          

        }

        public FrmAffectationCommercial(ref Client prospect):this()
        {
            try
            {
                leProspect = prospect;

                txtProspect.Text = leProspect.NomComplet;
                ChargerCommerciaux();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public FrmAffectationCommercial(ref Client prospect,bool reaffectation) : this()
        {
            try
            {
                leProspect = prospect;

                txtProspect.Text = leProspect.NomComplet;
                BReaffectation = reaffectation;
                if (reaffectation == true)
                {
                   // Console.Write("OK");
                    var leProjetId = prospect.ProjetId;
                    cmbProjets.SelectedValue = leProjetId;
                    cmbProjets.Enabled = false;
                    if (Tools.Tools.AgentEnCours.Role.CodeRole == "MKT" || Tools.Tools.AgentEnCours.Role.CodeRole == "DSK"
                     || Tools.Tools.AgentEnCours.Role.CodeRole == "ADM")
                    {
                        cmbProjets.Enabled = true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public FrmAffectationCommercial(ref Client prospect, bool reaffectation, string source) : this()
        {
            try
            {
                leProspect = prospect;

                txtProspect.Text = leProspect.NomComplet;
                BReaffectation = reaffectation;
                if(reaffectation==true)
                {
                    //Console.Write("OK");
                    var leProjetId = prospect.ProjetId;
                    cmbProjets.SelectedValue = leProjetId;
                    cmbProjets.Enabled = false;

                    if (Tools.Tools.AgentEnCours.Role.CodeRole == "MKT" || Tools.Tools.AgentEnCours.Role.CodeRole == "DSK"
                     || Tools.Tools.AgentEnCours.Role.CodeRole == "ADM")
                    {
                        cmbProjets.Enabled = true;
                    }
                }
                this.Source = source;
            }
            catch (Exception)
            {

                throw;
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
                cmbProjets.SelectedIndex = -1;
                //leProjetEnCours = cmbProjets.SelectedItem as Projet;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ChargerCommerciaux()
        {
            dgCommerciaux.DataSource = null;
            if (cmbProjets.SelectedIndex ==-1)
            {
                return;
            }

            db.Dispose();
            db = new SenImmoDataContext();
            var RoleCOmmerciale = db.Roles.Where(c => c.CodeRole == "CMC").SingleOrDefault();
            var lesCommerciaux= db.Agents.Where(c => c.RoleId == RoleCOmmerciale.ID && c.IsChefEquipe == false && c.ProjetId==leProjetEnCours.Id).ToList();
            if (Tools.Tools.AgentEnCours.IsChefEquipe)
                lesCommerciaux = lesCommerciaux.Where(c => c.ChefEquipeId == Tools.Tools.AgentEnCours.Id).ToList();
            dgCommerciaux.DataSource = lesCommerciaux.ToList().OrderBy(c =>c.Nom).ThenBy(c => c.Prenom).Select(
                                                                        p => new
                                                                        {
                                                                            ID = p.Id,
                                                                            Prénom = p.Prenom,
                                                                            Nom = p.Nom,
                                                                            nbProspect = p.Clients.Count()
                                                                        }

                                                                        ).ToList();

            //dgCommerciaux.DataSource = iCommercialRepository.GetCommerciaux().Where(cm => cm.IsChefEquipe == false).ToList().Select(
            //                                                     p => new
            //                                                     {
            //                                                         ID = p.Id,
            //                                                         Prénom = p.Prenom,
            //                                                         Nom = p.Nom,
            //                                                         nbProspect = db.Clients.Where(cm => cm.CommercialID==p.Id).Count()??0
            //                                                     }

            //                                                     ).ToList();
            dgCommerciaux.Columns[0].Visible = false;
            dgCommerciaux.Columns[1].Width = 140;
            dgCommerciaux.Columns[2].Width = 100;
            dgCommerciaux.Columns[3].Width = 100;
            dgCommerciaux.Columns[3].HeaderText = "Nb Prospects";



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdAffecter_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                cmdAffecter.Enabled = false;
                if (dgCommerciaux.SelectedRows.Count > 0)
                {
                    var idCommercial = Int16.Parse(dgCommerciaux.SelectedRows[0].Cells[0].Value.ToString());
                    leProspectEnCours = db.Clients.Find(leProspect.ID);
                    LAncienCommercial = leProspectEnCours.Commercial;
                    leCommercialEnCours = db.Agents.Find(idCommercial);

                    leProspectEnCours.CommercialID = leCommercialEnCours.Id;
                    leProspectEnCours.ProjetId = leCommercialEnCours.ProjetId;
                    leProspectEnCours.DateAffectationCommercial = dtpDateAffectation.Value.Date;
                    leProspectEnCours.ProspectAffecte = true;
                    leCommercialEnCours.Clients.Add(leProspectEnCours);
                    db.SaveChanges();
                    MessageBox.Show(this, "Le prospect a été affecté à " + leCommercialEnCours.NomComplet+ ", un mail d'alerte va lui être envoyé",
                          "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (leCommercialEnCours.Email != string.Empty)
                    {
                        if (this.Source==string.Empty || this.Source ==null )
                        { 
                        Tools.Tools.EmailSend(leCommercialEnCours.Email, "", "Affectation de prospect ",
                                @" Bonjour " + "\n\n Le prospect " + leProspectEnCours.NomComplet + " vous a été affecté ce " + DateTime.Now.ToShortDateString() + " à " + DateTime.Now.ToShortTimeString() 
                                + "\n\nVous pouvez le contacter au " +leProspectEnCours.Mobile1 +" ou par mail à "+ leProspectEnCours.Email
                                +"\n\n Cordialement\n\n\n\n"
                                +"************************************************************************************\n"
                                +"PROSOPIS: Logiciel de gestion commerciale de la Cité des AKYS\n"
                                +"************************************************************************************\n");

                        if (leCommercialEnCours.ChefEquipe != null)
                            if (leCommercialEnCours.ChefEquipe.Email != string.Empty)
                                Tools.Tools.EmailSend(leCommercialEnCours.ChefEquipe.Email, "", "Affectation de prospect ",
                                   @" Bonjour " + "\n\n Le prospect " + leProspectEnCours.NomComplet + " a été affecté à votre collaborateur " + leCommercialEnCours.NomComplet + " ce " + DateTime.Now.ToShortDateString() + " à " + DateTime.Now.ToShortTimeString() 
                                + "\n\n Cordialement\n\n"
                                + "************************************************************************************\n"
                                + "PROSOPIS: Logiciel de gestion commerciale de la Cité des AKYS\n"
                                + "************************************************************************************\n");
                        }
                        if (BReaffectation)
                        {
                            if (LAncienCommercial== null || LAncienCommercial.Id==45 )
                            {
                                if (Source == "Client")
                                {
                                    Tools.Tools.EmailSend(leCommercialEnCours.Email, "", "Réaffectation de client ",
                                  @" Bonjour " + "\n\n Le client " + leProspectEnCours.NomComplet + "  a été importé dans votre compte Prosopis dans le cadre de la reprise des données. \n" +
                                  " Merci de vérifier si les informations sur ce client sont exhaustives."

                                  + "\n\n Cordialement\n\n\n\n"
                                  + "************************************************************************************\n"
                                  + "PROSOPIS: Logiciel de gestion commerciale de la Cité des AKYS\n"
                                  + "************************************************************************************\n");

                                }
                                if (Source == "Prospect")
                                {
                                    Tools.Tools.EmailSend(leCommercialEnCours.Email, "", "Réaffectation de prospect ",
                                  @" Bonjour " + "\n\n Le prospect " + leProspectEnCours.NomComplet + " a été importé dans votre compte Prosopis dans le cadre de la reprise des données. \n" +
                                  " Merci de vérifier si les informations sur ce prospect sont exhaustives."

                                  + "\n\n Cordialement\n\n\n\n"
                                  + "************************************************************************************\n"
                                  + "PROSOPIS: Logiciel de gestion commerciale de la Cité des AKYS\n"
                                  + "************************************************************************************\n");

                                }
                            }
                            else
                            { 
                                Tools.Tools.EmailSend(LAncienCommercial.Email, "", "Réaffectation de prospect ",
                                   @" Bonjour " + "\n\n Le prospect " + leProspectEnCours.NomComplet + " qui était précédemmment sous votre gestion, a été réaffecté à "+ leCommercialEnCours.NomComplet + " ce " + DateTime.Now.ToShortDateString() + " à " + DateTime.Now.ToShortTimeString()
                             
                                   + "\n\n Cordialement\n\n\n\n"
                                   + "************************************************************************************\n"
                                   + "PROSOPIS: Logiciel de gestion commerciale de la Cité des AKYS\n"
                                   + "************************************************************************************\n");
                                if (LAncienCommercial.ChefEquipe != null)
                                    if (LAncienCommercial.ChefEquipe.Email != string.Empty)
                                        Tools.Tools.EmailSend(LAncienCommercial.ChefEquipe.Email, "", "Réaffectation de prospect ",
                                           @" Bonjour " + "\n\n Le prospect " + leProspectEnCours.NomComplet + " précédemment sous la gestion de votre collaborateur " + LAncienCommercial.NomComplet + " a été réaffecté à " + leCommercialEnCours.NomComplet + " ce " + DateTime.Now.ToShortDateString() + " à " + DateTime.Now.ToShortTimeString()
                                        + "\n\n Cordialement\n\n"
                                        + "************************************************************************************\n"
                                        + "PROSOPIS: Logiciel de gestion commerciale de la Cité des AKYS\n"
                                        + "************************************************************************************\n");

                            }

                        }
                    }
                    else
                        MessageBox.Show(this, "L'alerte mail n'a pas pu être envoyé vers " + leCommercialEnCours.NomComplet + ", car son adresse e-mail n'est pas correctement configuré",
                           "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                else
                {

                }
                cmdAffecter.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur: "+ ex.Message,
                         "Prosopis - Affectation de prospect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                cmdAffecter.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgCommerciaux_DoubleClick(object sender, EventArgs e)
        {
            cmdAffecter_Click(sender, e);
        }

        private void cmbProjets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProjets.SelectedItem != null)
            {
                leProjetEnCours = (Projet)cmbProjets.SelectedItem;
                ChargerCommerciaux();
            }
        }
    }
}
