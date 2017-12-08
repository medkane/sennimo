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

namespace prjSenImmoWinform
{
    public partial class FrmIlot : Form
    {
       
        private Ilot IlotEnCours;
        private TypeConstruction leTypeConstructionEnCours;
        private bool bModifIlot;
        private IlotRepository ilotRepository;
        private ContratRepository contratRep;
        private Projet leProjetEnCours;

        public FrmIlot()
        {
            InitializeComponent();
           
            ilotRepository = new IlotRepository();
            contratRep = new ContratRepository();
            rbImmeuble.Checked = true;
            leProjetEnCours = Tools.Tools.ProjetEnCours;
            if (leProjetEnCours.DenominationProjet == "KERRIA")
            {
                leTypeConstructionEnCours = TypeConstruction.Appartement;
                rbImmeuble.Checked = true;
                //pTypeConstruction.Visible = true;
            }
            else
            {
                leTypeConstructionEnCours = TypeConstruction.Villa;
                rbIlot.Checked = true;
                //pTypeConstruction.Visible = false;
            }
           
            ChargerLesProjets();
            ChargerIlots(leProjetEnCours, leTypeConstructionEnCours);
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
            }
            catch (Exception)
            {

                throw;
            }
        }


        #region GESTION DES IlotS

        private void ChargerIlots(Projet prj,TypeConstruction tc)
        {
            try
            {
                dgIlots.DataSource = ilotRepository.List.Where(ilot =>ilot.ProjetId==prj.Id && ilot.TypeConstruction==tc).ToList().Select(i =>
                                                                    new
                                                                    {
                                                                        ID = i.Id,
                                                                        Nom = i.NomIlot,
                                                                      
                                                                        Ouvert = i.StatutOuverture,
                                                                        NbVilla = i.Lots.Count
                                                                    }
                                                                   ).ToList();
                //DataGridViewComboBoxColumn colBox = new DataGridViewComboBoxColumn();
                //colBox.DataSource = Tools.Tools.db.TypeVillas.ToList();
                //colBox.DisplayMember = "CodeType";  // display category.catName
                //colBox.ValueMember = "TypeVillaId";         // use category.id as the identifier
                //colBox.DataPropertyName = "TypeVillaId"; 
                //dgIlots.Columns.Add(colBox);
                //dgIlots.Columns[0].Width = 0;
                //dgIlots.Columns[1].Width = 60;
                //dgIlots.Columns[2].Width = 120;
                //dgIlots.Columns[3].Width = 80;
                ////dgIlots.Columns[4].Width = 80;
                ////dgIlots.Columns[1].HeaderText = "Type d'Ilot du véhicule";
                //dgIlots.Columns[0].Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgIlots_SelectionChanged(object sender, EventArgs e)
        {
            if (dgIlots.SelectedRows.Count > 0)
            {
                if (dgIlots.SelectedRows[0].Cells[0].Value != null)
                {
                    var ilotVal = dgIlots.SelectedRows[0].Cells[0].Value;
                    int idIlot = (int)ilotVal;
                    IlotEnCours = ilotRepository.FindById(idIlot);

                    AfficherIlot(IlotEnCours);

                    VerouillerIlot();
                }
            }
        }

        private void AfficherIlot(Ilot ilot)
        {
            txtNumeroIlot.Text = ilot.NomIlot;
           
            //txtNbVillas.Text = ilot.Lots.Count().ToString();
            chkOuvert.Checked = ilot.StatutOuverture;
            if (ilot.DateDemarrageTravaux != null)
            {
                dtpDateDemarrageTravaux.CustomFormat = "dd/MM/yyyy";
                dtpDateDemarrageTravaux.Value = ilot.DateDemarrageTravaux.Value.Date;
            }
            else
            {
                dtpDateDemarrageTravaux.CustomFormat = " "; //An empty SPACE;
                dtpDateDemarrageTravaux.Format = DateTimePickerFormat.Custom;
            }
            if (ilot.DateFinTravaux != null)
            {
                dtpDateFinTravaux.CustomFormat = "dd/MM/yyyy";
                dtpDateFinTravaux.Value = ilot.DateFinTravaux.Value.Date;
            }
            else
            {
                dtpDateFinTravaux.CustomFormat = " "; //An empty SPACE;
                dtpDateFinTravaux.Format = DateTimePickerFormat.Custom;
            }
            if (ilot.StatutOuverture)
            {
                dtpOuverture.CustomFormat = "dd/MM/yyyy";
                dtpOuverture.Value = ilot.DateOuverturePrevue.Value;
            }
            else
            {
                dtpOuverture.CustomFormat = " "; //An empty SPACE;
                dtpOuverture.Format = DateTimePickerFormat.Custom;
                //txtTelephoneIlot.Text = IlotEnCours.TelephoneIlot;
                //txtMarqueIlot.Text = IlotEnCours.MarqueIlot;
            }
            if (ilot.DateDebutLivraison != null)
            {
                dtpDateDebutLivraison.CustomFormat = "dd/MM/yyyy";
                dtpDateDebutLivraison.Value = ilot.DateDebutLivraison.Value.Date;
            }
            else
            {
                dtpDateDebutLivraison.CustomFormat = " "; //An empty SPACE;
                dtpDateDebutLivraison.Format = DateTimePickerFormat.Custom;
            }
            if (ilot.DateFinLivraison != null)
            {
                dtpDateFinLivraison.CustomFormat = "dd/MM/yyyy";
                dtpDateFinLivraison.Value = ilot.DateFinLivraison.Value.Date;
            }
            else
            {
                dtpDateFinLivraison.CustomFormat = " "; //An empty SPACE;
                dtpDateFinLivraison.Format = DateTimePickerFormat.Custom;
            }
            txtCommentairesIlot.Text = ilot.Commentaires;
        }

        private void DeverouillerIlot()
        {
            txtNumeroIlot.ReadOnly = false;
            //txtNbVillas.ReadOnly = false;
            txtCommentairesIlot.ReadOnly = false;
          
            chkOuvert.Enabled = true;
            dtpOuverture.Enabled = true;

            cmdNouveauIlot.Enabled = false;
            cmdEnregistrerIlot.Enabled = true;
            cmdEditerIlot.Enabled = false;
            cmdSupprimerIlot.Enabled = false;
        }
        private void VerouillerIlot()
        {
            txtNumeroIlot.ReadOnly = true;
            //txtNbVillas.ReadOnly = true;
            txtCommentairesIlot.ReadOnly = true;
           
            chkOuvert.Enabled = false;
            dtpOuverture.Enabled = false;

            cmdNouveauIlot.Enabled = true;
            cmdEnregistrerIlot.Enabled = false;
            cmdEditerIlot.Enabled = true;
            cmdSupprimerIlot.Enabled = true;
        }

        private void cmdNouveauIlot_Click(object sender, EventArgs e)
        {
            bModifIlot = false;
            EffacerIlot();
            DeverouillerIlot();
            //cmdEnregistrerVille.Enabled = true;
            txtNumeroIlot.Focus();

            dtpOuverture.CustomFormat = " "; //An empty SPACE;
            dtpOuverture.Format = DateTimePickerFormat.Custom;
            dtpDateDemarrageTravaux.CustomFormat = " "; //An empty SPACE;
            dtpDateDemarrageTravaux.Format = DateTimePickerFormat.Custom;
            dtpDateFinTravaux.CustomFormat = " "; //An empty SPACE;
            dtpDateFinTravaux.Format = DateTimePickerFormat.Custom;
            dtpDateDebutLivraison.CustomFormat = " "; //An empty SPACE;
            dtpDateDebutLivraison.Format = DateTimePickerFormat.Custom;
            dtpDateFinLivraison.CustomFormat = " "; //An empty SPACE;
            dtpDateFinLivraison.Format = DateTimePickerFormat.Custom;
        }

        private void EffacerIlot()
        {
            txtNumeroIlot.Text = string.Empty;
            //txtNbVillas.Text = string.Empty;
            txtCommentairesIlot.Text = string.Empty;
           
            chkOuvert.Enabled = false;

            dtpOuverture.CustomFormat = " "; //An empty SPACE;
            dtpOuverture.Format = DateTimePickerFormat.Custom;
            dtpDateDemarrageTravaux.CustomFormat = " "; //An empty SPACE;
            dtpDateDemarrageTravaux.Format = DateTimePickerFormat.Custom;
            dtpDateFinTravaux.CustomFormat = " "; //An empty SPACE;
            dtpDateFinTravaux.Format = DateTimePickerFormat.Custom;
            dtpDateDebutLivraison.CustomFormat = " "; //An empty SPACE;
            dtpDateDebutLivraison.Format = DateTimePickerFormat.Custom;
            dtpDateFinLivraison.CustomFormat = " "; //An empty SPACE;
            dtpDateFinLivraison.Format = DateTimePickerFormat.Custom;



           // dtpOuverture.Value = DateTime.Parse("01/01/1900");
        }

        private void cmdEditerIlot_Click(object sender, EventArgs e)
        {

            bModifIlot = true;
            DeverouillerIlot();
            txtNumeroIlot.Focus();
        }

        private void cmdSupprimerIlot_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Voulez réellement supprimer cet ilôt?",
                    "Prosopis -  Gestion des ilôts", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ilotRepository.Delete(IlotEnCours);
                //db.SaveChanges();
                ChargerIlots(Tools.Tools.ProjetEnCours,leTypeConstructionEnCours);
                EffacerIlot();
            }

        }

        private void cmdEnregistrerIlot_Click(object sender, EventArgs e)
        {
             if (txtNumeroIlot.Text == string.Empty)
            {
                MessageBox.Show(this, "Veuillez saisir lae nom de l'ilôt",
                         "Prosopis -  Gestion des ilôts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!bModifIlot)
            {
                var newIlot = new Ilot()
                {
                    NomIlot = txtNumeroIlot.Text,
                    TypeConstruction = leTypeConstructionEnCours,
                    StatutOuverture = false,
                    DateDemarrageTravaux = dtpDateDemarrageTravaux.Value.Date,
                    DateFinTravaux = dtpDateFinTravaux.Value.Date,
                    DateOuverturePrevue = dtpOuverture.Value.Date,
                    DateDebutLivraison = dtpDateDebutLivraison.Value.Date,
                    DateFinLivraison = dtpDateFinLivraison.Value.Date,
                    ProjetId = leProjetEnCours.Id,
                    Commentaires=txtCommentairesIlot.Text
                };
                ilotRepository.Add(newIlot);
                MessageBox.Show(this, "L'ilôt a été enregistré",
                         "Prosopis -  Gestion des ilôts", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                IlotEnCours.NomIlot = txtNumeroIlot.Text;
              
                if (chkOuvert.Checked)
                {
                    IlotEnCours.StatutOuverture = true;
                    IlotEnCours.DateOuvertureEffective = dtpOuverture.Value;
                   
                }
                else
                    IlotEnCours.StatutOuverture = false;

                IlotEnCours.DateFinTravaux = dtpDateFinTravaux.Value.Date;
                IlotEnCours.DateOuverturePrevue = dtpOuverture.Value.Date;
                IlotEnCours.TypeConstruction = leTypeConstructionEnCours;
                IlotEnCours.DateDebutLivraison = dtpDateDebutLivraison.Value.Date;
                IlotEnCours.DateFinLivraison = dtpDateFinLivraison.Value.Date;
                IlotEnCours.ProjetId = leProjetEnCours.Id;
                IlotEnCours.Commentaires = txtCommentairesIlot.Text;
                ilotRepository.Update(IlotEnCours);
                MessageBox.Show(this, "L'ilôt a été modifié",
                        "Prosopis -  Gestion des ilôts", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //db.SaveChanges();
            ChargerIlots(leProjetEnCours,leTypeConstructionEnCours);
            EffacerIlot();
        }
        

        #endregion

        private void dgIlots_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void détailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailsIlot frmDetIlot = new FrmDetailsIlot();
            frmDetIlot.Show();
        }

        private void dgIlots_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{

            //    FrmDetailsIlot frmDetIlot = new FrmDetailsIlot(IlotEnCours);
            //    frmDetIlot.MdiParent = this.MdiParent;
            //    frmDetIlot.Show();
            //    frmDetIlot.WindowState = FormWindowState.Maximized;
            //}
            //catch (Exception ex)
            //{
                
            //     MessageBox.Show(this, "Erreur:"+ex.Message,
            //             "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void dtpDateDemarrageTravaux_ValueChanged(object sender, EventArgs e)
        {
            dtpDateDemarrageTravaux.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateFinTravaux_ValueChanged(object sender, EventArgs e)
        {
            dtpDateFinTravaux.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpOuverture_ValueChanged(object sender, EventArgs e)
        {
            dtpOuverture.CustomFormat = "dd/MM/yyyy";
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpDateDebutLivraison_ValueChanged(object sender, EventArgs e)
        {
            dtpDateDebutLivraison.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateFinLivraison_ValueChanged(object sender, EventArgs e)
        {
            dtpDateFinLivraison.CustomFormat = "dd/MM/yyyy";
        }

        private void rbIlot_CheckedChanged(object sender, EventArgs e)
        {
            if(rbIlot.Checked)
            {
                leTypeConstructionEnCours = TypeConstruction.Villa;
                
            }
            else
            {
                leTypeConstructionEnCours = TypeConstruction.Appartement;
            }
            EffacerIlot();
            ChargerIlots(leProjetEnCours,leTypeConstructionEnCours);
        }

        private void rbImmeuble_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbProjets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProjets.SelectedItem != null)
            {
                leProjetEnCours = (Projet)cmbProjets.SelectedItem;
                if(leProjetEnCours.DenominationProjet.Trim()=="AKYS")
                { 
                    rbIlot.Checked = true;
                    rbImmeuble.Checked = false;
                    pTypeConstruction.Visible = false;
                }
                else
                { 
                    rbIlot.Checked = false;
                    rbImmeuble.Checked = true;
                    pTypeConstruction.Visible = true;
                }
                EffacerIlot();
                //if (rbIlot.Checked)
                //{
                //    leTypeConstructionEnCours = TypeConstruction.Villa;
                //}
                //else
                //{
                //    leTypeConstructionEnCours = TypeConstruction.Appartement;
                //}

                //ChargerIlots(leProjetEnCours, leTypeConstructionEnCours);

            }
        }
    }
}
