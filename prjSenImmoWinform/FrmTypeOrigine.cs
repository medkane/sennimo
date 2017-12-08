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
    public partial class FrmTypeOrigine : Form
    {
        private ClientRepository clientDAL;
        private TypeOrigine leTypeOrigineEnCours;
        private bool bModifTypeOption;
        private ContratRepository contratRep;
        private Projet leProjetEnCours;

        public FrmTypeOrigine()
        {
            InitializeComponent();
            clientDAL = new ClientRepository();
            contratRep = new ContratRepository();
            ChargerLesProjets();
            ChargerLesTypesOptions();
            dtpDateDebut.CustomFormat = " "; //An empty SPACE;
            dtpDateDebut.Format = DateTimePickerFormat.Custom;
            dtpDateFin.CustomFormat = " "; //An empty SPACE;
            dtpDateFin.Format = DateTimePickerFormat.Custom;
        }

        private void ChargerLesProjets()
        {

            try
            {
                var lesProjets = contratRep.GetProjets();


                cmbProjets.DataSource = lesProjets.ToList();
                cmbProjets.DisplayMember = "DenominationProjet";
                cmbProjets.ValueMember = "Id";
                cmbProjets.SelectedValue = 1;
                leProjetEnCours = cmbProjets.SelectedItem as Projet;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ChargerLesTypesOptions()
        {
            dgTypeOrigineProspects.DataSource = clientDAL.GetRealAllTypeOrigines().Where(to =>to.ProjetId== leProjetEnCours.Id).ToList().Select(to => new
            {
                Id = to.TypeOrigineId,
                Libellé = to.LibelleTypeOrigine,
                Commentaire = to.CommentaireTypeOrigine
            }).ToList();
        }

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bModifTypeOption)
                {
                    var typeOrigine = new TypeOrigine()
                    {
                        ClassOrigine = ClassOrigine.Desk,
                        LibelleTypeOrigine = txtLibelleTypeOrigine.Text,
                        CommentaireTypeOrigine = txtDescriptionTypeOrigine.Text,
                        DateDebutTypeOrigine = dtpDateDebut.Value.Date,
                        DateFinTypeOrigine = dtpDateFin.Value.Date,
                        ProjetId = leProjetEnCours.Id,
                        BActif = chkActif.Checked
                    };
                    if(chkLimiteDansLeTemps.Checked)
                    {
                        typeOrigine.BLimiteDansLeTemps = true;
                        typeOrigine.DateDebutTypeOrigine = dtpDateDebut.Value.Date;
                        typeOrigine.DateFinTypeOrigine = dtpDateFin.Value.Date;
                    }
                    else
                    {
                        typeOrigine.BLimiteDansLeTemps = false;
                    }
                    clientDAL.AddTypeOrigine(typeOrigine);
                }
                else
                {
                    leTypeOrigineEnCours.ClassOrigine = ClassOrigine.Desk;
                    leTypeOrigineEnCours.LibelleTypeOrigine = txtLibelleTypeOrigine.Text;
                    leTypeOrigineEnCours.CommentaireTypeOrigine = txtDescriptionTypeOrigine.Text;
                    leTypeOrigineEnCours.DateDebutTypeOrigine = dtpDateDebut.Value.Date;
                    leTypeOrigineEnCours.DateFinTypeOrigine = dtpDateFin.Value.Date;
                    leTypeOrigineEnCours.BActif = chkActif.Checked;
                    leTypeOrigineEnCours.ProjetId = leProjetEnCours.Id;

                    if (chkLimiteDansLeTemps.Checked)
                    {
                        leTypeOrigineEnCours.BLimiteDansLeTemps = true;
                        leTypeOrigineEnCours.DateDebutTypeOrigine = dtpDateDebut.Value.Date;
                        leTypeOrigineEnCours.DateFinTypeOrigine = dtpDateFin.Value.Date;
                    }
                    else
                    {
                        leTypeOrigineEnCours.BLimiteDansLeTemps = false;
                    }
                    clientDAL.SaveChanges();
                }
                EffacerForm();
                ChargerLesTypesOptions();
                MessageBox.Show(this, "Le type d'origine des prospects a été enregistré avec succes",
                    "Prosopis - Gestion des types d'origine des prospects", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                     "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkLimiteDansLeTemps_CheckedChanged(object sender, EventArgs e)
        {
            if(chkLimiteDansLeTemps.Checked)
            {
                pLimiteDansLeTemps.Visible = true;
            }
            else
                pLimiteDansLeTemps.Visible = false;
        }

        private void cmdAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EffacerForm()
        {
            txtDescriptionTypeOrigine.Text = string.Empty;
            txtLibelleTypeOrigine.Text = string.Empty;
            dtpDateDebut.CustomFormat = " "; //An empty SPACE;
            dtpDateDebut.Format = DateTimePickerFormat.Custom;
            dtpDateFin.CustomFormat = " "; //An empty SPACE;
            dtpDateFin.Format = DateTimePickerFormat.Custom;
            chkActif.Checked = false;
            chkLimiteDansLeTemps.Checked = false;
        }
        private void DeverouillerForm()
        {
            txtDescriptionTypeOrigine.ReadOnly = false;
            txtLibelleTypeOrigine.ReadOnly = false;

            dtpDateDebut.Enabled = true;
            dtpDateFin.Enabled = true;
            chkActif.Enabled = true;
            chkLimiteDansLeTemps.Enabled = true;
        }

        private void VerouillerForm()
        {
            txtDescriptionTypeOrigine.ReadOnly = true;
            txtLibelleTypeOrigine.ReadOnly = true;
            dtpDateDebut.Enabled = false;
            dtpDateFin.Enabled = false;
            chkActif.Enabled = false;
            chkLimiteDansLeTemps.Enabled = false;
        }

        private void dgTypeOrigineProspects_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgTypeOrigineProspects.SelectedRows.Count > 0)
                {
                    int idTypeOrigine = (int)dgTypeOrigineProspects.SelectedRows[0].Cells[0].Value;
                    leTypeOrigineEnCours = clientDAL.GetTypeOrigine(idTypeOrigine);
                    txtLibelleTypeOrigine.Text = leTypeOrigineEnCours.LibelleTypeOrigine;
                    txtDescriptionTypeOrigine.Text = leTypeOrigineEnCours.CommentaireTypeOrigine;
                    //dtpDateDebut.Value
                    if (leTypeOrigineEnCours.BLimiteDansLeTemps)
                    {
                        chkLimiteDansLeTemps.Checked = true;
                        dtpDateDebut.Value = leTypeOrigineEnCours.DateDebutTypeOrigine.Value.Date;
                        dtpDateFin.Value = leTypeOrigineEnCours.DateFinTypeOrigine.Value.Date;
                    }
                    else
                        chkLimiteDansLeTemps.Checked = false;

                    chkActif.Checked = leTypeOrigineEnCours.BActif;
                    VerouillerForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdNouveau_Click(object sender, EventArgs e)
        {
            bModifTypeOption= false;
            EffacerForm();
            DeverouillerForm();
        }

        private void cmdEditer_Click(object sender, EventArgs e)
        {
            bModifTypeOption = true;
            DeverouillerForm();
        }

        private void dtpDateDebut_ValueChanged(object sender, EventArgs e)
        {
            dtpDateDebut.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateFin_ValueChanged(object sender, EventArgs e)
        {
            dtpDateFin.CustomFormat = "dd/MM/yyyy";
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbProjets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProjets.SelectedItem != null)
            {
                leProjetEnCours = (Projet)cmbProjets.SelectedItem;
                ChargerLesTypesOptions(); 
            }
        }
    }
}
        