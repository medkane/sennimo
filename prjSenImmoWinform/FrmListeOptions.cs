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
    public partial class FrmListeOptions : Form
    {
        private ClientRepository clientRep;
        private CommercialRepository commercialRep;
        private IlotRepository ilotRepository;
        private ContratRepository contratRep;
        private Projet LeProjetEncours;
        private TypeConstruction leTypeConstructionEnCours;

        public FrmListeOptions()
        {
            InitializeComponent();
            clientRep = new ClientRepository();
            commercialRep = new CommercialRepository();
            ilotRepository = new IlotRepository();
            contratRep = new ContratRepository();
            ChargerLesProjets();
            AfficherLesOptions();
            ChargerCommerciaux();
            cmbOperation.SelectedItem = "=";
            ChargerTypeVillas();
        }

        private void ChargerLesProjets()
        {
            try
            {
                var lesProjets = contratRep.GetProjets();

                cmbProjets.DataSource = lesProjets.ToList();
                cmbProjets.DisplayMember = "DenominationProjet";
                cmbProjets.ValueMember = "Id";
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" || Tools.Tools.AgentEnCours.Role.CodeRole == "DC")
                {
                    cmbProjets.SelectedValue = Tools.Tools.AgentEnCours.ProjetId;
                    LeProjetEncours = Tools.Tools.AgentEnCours.Projet;
                    cmbProjets.Enabled = false;
                  
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ChargerTypeVillas()
        {
            try               
            {
                if (LeProjetEncours != null)
                { 
                    var typeVillas= ilotRepository.GetTypeVillas().Where(tv => tv.ProjetId==LeProjetEncours.Id).ToList();
                    if (leTypeConstructionEnCours != 0)
                    {
                        typeVillas = typeVillas.Where(tv => tv.TypeConstruction == leTypeConstructionEnCours).ToList();
                    }
                    cmbTypeVillas.DataSource = typeVillas;
                    cmbTypeVillas.DisplayMember = "NomComplet";
                    cmbTypeVillas.ValueMember = "TypeVillaID";
                    cmbTypeVillas.SelectedIndex = -1;
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ChargerCommerciaux()
        {

            try
            {
                var lesCommerciaux = commercialRep.GetCommerciaux();
                //if (Tools.Tools.AgentEnCours.IsChefEquipe)
                //    lesCommerciaux = lesCommerciaux.Where(c => c.ChefEquipeId == Tools.Tools.AgentEnCours.Id);
                if (LeProjetEncours != null)
                {
                    lesCommerciaux = lesCommerciaux.Where(c => c.ProjetId == LeProjetEncours.Id);
                }
                cmbCommercial.DataSource = lesCommerciaux.ToList();
                cmbCommercial.DisplayMember = "NomComplet";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfficherLesOptions()
        {
            ////Liberer les options arrivées à terme
            //if (Tools.Tools.AgentEnCours.Role.CodeRole=="CMC" && !Tools.Tools.AgentEnCours.IsChefEquipe)
            //    if (lvOptions.Columns.Count > 5)
            //        lvOptions.Columns.RemoveAt(5);


            var lesOptions = clientRep.GetAllOptionsProspect().ToList().Where(c => c.TypeContrat.CategorieContrat== CategorieContrat.Réservation).ToList();


            if (txtNomRecherche.Text != string.Empty)
                lesOptions = lesOptions.Where(c => c.Client.Nom.ToLower().StartsWith(txtNomRecherche.Text.ToLower())).ToList();
            if (txtPrenomRecherche.Text != string.Empty)
                lesOptions = lesOptions.Where(c => c.Client.Prenom.ToLower().StartsWith(txtPrenomRecherche.Text.ToLower())).ToList();
            if (txtTelephoneRecherche.Text != string.Empty)
                lesOptions = lesOptions.Where(c => c.Client.Mobile1.StartsWith(txtTelephoneRecherche.Text)).ToList();
            if (txtEmailRecherche.Text != string.Empty)
                lesOptions = lesOptions.Where(c => c.Client.Email.ToLower().StartsWith(txtEmailRecherche.Text.ToLower())).ToList();

            if (cmbProjets.SelectedItem != null)
                lesOptions = lesOptions.Where(c => c.Client.ProjetId ==(int) cmbProjets.SelectedValue).ToList();
            if (cmbTypeVillas.SelectedItem != null)
                lesOptions = lesOptions.Where(c => c.Lot.TypeVillaID == ((TypeVilla)cmbTypeVillas.SelectedItem).TypeVillaId).ToList();

            if(rbConstructionAppartement.Checked)
                lesOptions = lesOptions.Where(opt => opt.TypeVilla.TypeConstruction == TypeConstruction.Appartement).ToList();
            else if (rbConstructionVilla.Checked)
                lesOptions = lesOptions.Where(opt => opt.TypeVilla.TypeConstruction == TypeConstruction.Villa).ToList();

            if (chkCommercial.Checked)
            {
                if (cmbCommercial.SelectedItem != null)
                {
                    Agent leCommercial = (Agent)cmbCommercial.SelectedItem;
                    lesOptions = lesOptions.Where(opt => opt.CommercialID == leCommercial.Id).ToList();
                }
            }

            if (txtLot.Text!=string.Empty)
            {
                lesOptions = lesOptions.Where(opt => opt.Lot.NumeroLot.ToLower()== txtLot.Text.ToLower()).ToList();
            }

            if (txtDelai.Text != string.Empty)
            {
                int nbJours=Int16.Parse(txtDelai.Text);
                switch(cmbOperation.SelectedItem.ToString())
                {
                    case "=":
                        lesOptions = lesOptions.Where(opt => opt.DateFinOption!=null && (opt.DateFinOption.Value.Subtract(DateTime.Now).Days + 1) == nbJours).ToList();
                        break;
                    case "<":
                        lesOptions = lesOptions.Where(opt => opt.DateFinOption != null && (opt.DateFinOption.Value.Date.Subtract(DateTime.Now.Date).Days + 1) < nbJours).ToList();
                        break;
                    case "<=":
                        break;
                    case ">":
                        break;
                    case ">=":
                        break;
                    default:
                        break;

                }
                lesOptions = lesOptions.Where(opt => opt.Lot.NumeroLot.ToLower() == txtLot.Text.ToLower()).ToList();
            }
            lvOptions.Items.Clear();
            var allOptions = lesOptions.ToList();
            foreach (var option in allOptions)
            {
                ListViewItem lviOption = new ListViewItem(option.Lot.NumeroLot);
                lviOption.SubItems.Add(option.Lot.TypeVilla.NomComplet);
                lviOption.SubItems.Add(option.Client.NomComplet);
                lviOption.SubItems.Add(option.DatePriseOption.Value.ToShortDateString());
                if (option.DateFinOption != null)
                {
                    lviOption.SubItems.Add(option.DateFinOption.Value.ToShortDateString());
                    lviOption.SubItems.Add((option.DateFinOption.Value.Subtract(DateTime.Now).Days+1).ToString());
                    lviOption.ImageIndex = 10;
                }
                else
                {
                    lviOption.SubItems.Add("illimité");
                    lviOption.SubItems.Add("");
                    lviOption.ImageIndex = 11;
                }
               
                   
                lviOption.Tag = option;
                //if(! (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" && !Tools.Tools.AgentEnCours.IsChefEquipe))
                    lviOption.SubItems.Add(option.Commercial.NomComplet);

                lvOptions.Items.Add(lviOption);
                //Lever l'option
                //clientRep.LeverOption(option.Id);
                //Informer le commercial

            }
            txtNbOptions.Text = lesOptions.Count().ToString();
        }

        private void cmdRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                AfficherLesOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors de la recherche des options:..." + ex.Message,
                        "Prosopis - Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkCommercial_CheckedChanged(object sender, EventArgs e)
        {
            cmbCommercial.SelectedIndex = -1;
            cmbCommercial.Visible = chkCommercial.Checked;
            //this.Cursor = Cursors.WaitCursor;
            //cmdRechercher_Click(sender, e);
            //this.Cursor = Cursors.Default;
        }

        private void cmbCommercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //cmdRechercher_Click(sender, e);
            //this.Cursor = Cursors.Default;
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtLot_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    cmdRechercher_Click(sender, e);
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void txtDelai_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtDelai_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    cmdRechercher_Click(sender, e);
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void txtPrenomRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    cmdRechercher_Click(sender, e);
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void txtNomRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    cmdRechercher_Click(sender, e);
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void txtTelephoneRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    cmdRechercher_Click(sender, e);
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void txtEmailRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    cmdRechercher_Click(sender, e);
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void lvOptions_DoubleClick(object sender, EventArgs e)
        {
            //if (lvOptions.SelectedItems.Count > 0)
            //{
            //    try
            //    {
            //        this.Cursor = Cursors.WaitCursor;
            //        Client leProspect = ((Option)lvOptions.SelectedItems[0].Tag).Client;

            //        FrmDossierProspect childForm = new FrmDossierProspect(leProspect,true);

            //        childForm.StartPosition = FormStartPosition.CenterScreen;
            //        childForm.ShowDialog();
            //        childForm.WindowState = FormWindowState.Normal;
            //        childForm.WindowState = FormWindowState.Maximized;

            //        AfficherLesOptions();

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(this, "Erreur:..." + ex.Message,
            //             "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    finally
            //    {
            //        this.Cursor = Cursors.Default;
            //    }
            //}
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmListeOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                cmdRechercher_Click(sender, e);
            }
        }

        private void cmbProjets_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbProjets.SelectedItem != null)
            {
                LeProjetEncours = cmbProjets.SelectedItem as Projet;
                rbToutTypeConstruction.Checked = true;
                if (LeProjetEncours.Id == 1)//AKYS
                {
                    rbConstructionVilla.Checked = true;
                    pTypeConstruction.Enabled = false;
                }
                else
                    pTypeConstruction.Enabled = true;


                ChargerCommerciaux();
                ChargerTypeVillas();

            }
            else
            {
                LeProjetEncours = null;
                rbToutTypeConstruction.Checked = true;
                pTypeConstruction.Enabled = false;
            }
        }

        private void rbConstructionVilla_CheckedChanged(object sender, EventArgs e)
        {
            if (rbConstructionVilla.Checked)
            {
                leTypeConstructionEnCours = TypeConstruction.Villa;
            }
           
            ChargerTypeVillas();
        }

        private void rbConstructionAppartement_CheckedChanged(object sender, EventArgs e)
        {
            if(rbConstructionAppartement.Checked)
            {
                leTypeConstructionEnCours = TypeConstruction.Appartement;
            }
            ChargerTypeVillas();
        }

        private void rbToutTypeConstruction_CheckedChanged(object sender, EventArgs e)
        {
            if (rbToutTypeConstruction.Checked)
            {
                leTypeConstructionEnCours = 0;
            }
            ChargerTypeVillas();
        }
    }
}
