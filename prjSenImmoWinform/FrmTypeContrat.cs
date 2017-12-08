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
    public partial class FrmTypeContrat : Form
    {
        private bool bModif;
        private SenImmoDataContext db;
        private TypeContrat leTypeContratEnCours;
        private ContratRepository contratRep;
        private Projet leProjetEnCours;
        private TypeConstruction leTypeConstructionEnCours;
        private TypeEtatAvancement leTypeAvancementEnCours;
        private bool bModifNA;

        public FrmTypeContrat()
        {
            InitializeComponent();
            db = new SenImmoDataContext();
            contratRep = new ContratRepository();
            ChargerLesProjets();
            ChargerLesTypesContrats();
            Verouiller();
        }

        private void ChargerLesProjets()
        {

            try
            {
                var lesProjets = contratRep.GetProjets();


                cmbProjets.DataSource = lesProjets.ToList();
                cmbProjets.DisplayMember = "DenominationProjet";
                cmbProjets.ValueMember = "Id";
                cmbProjets.SelectedValue = Tools.Tools.ProjetEnCours.Id;
                leProjetEnCours = cmbProjets.SelectedItem as Projet;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Deverouiller()
        {
            txtLibelle.ReadOnly = false;
            txtSeuilReservation.ReadOnly = false;
            txtSeuilSouscription.ReadOnly = false;
            rbDepot.Enabled = true;
            rbReservation.Enabled = true;
            chkActif.Enabled = true;

            cmdNouveau.Enabled = false;
            cmdEnregistrer.Enabled = true;
            cmdEditer.Enabled = false;
            cmdSupprimer.Enabled = false;


        }

        private void Verouiller()
        {
            txtLibelle.ReadOnly = true;
            txtSeuilReservation.ReadOnly = true;
            txtSeuilSouscription.ReadOnly = true;
            rbDepot.Enabled = false;
            rbReservation.Enabled = false;
            chkActif.Enabled = false;

            cmdNouveau.Enabled = true;
            cmdEnregistrer.Enabled = false;
            cmdEditer.Enabled = true;
            cmdEditer.Enabled = true;
        }

        private void cmdNouveau_Click(object sender, EventArgs e)
        {
            bModif = false;
            Effacer();
            Deverouiller();
            rbReservation.Checked = true;
            chkActif.Checked = true;
            txtLibelle.Focus();
        }

        private void Effacer()
        {
            txtLibelle.Text = string.Empty;
            txtSeuilReservation.Text = string.Empty;
            txtSeuilSouscription.Text = string.Empty;
            rbDepot.Checked = false;
            rbReservation.Checked = false;
        }

        private void cmdEditer_Click(object sender, EventArgs e)
        {
            bModif = true;
            Deverouiller();
            txtLibelle.Focus();
        }

        private void cmdSupprimer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(this, "Voulez réellement désactiver ce type de contrat?", "Prosopis - Gestion des types de contrat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //db.TypeContrats.Remove(leTypeContratEnCours);
                    leTypeContratEnCours.Actif = false;
                    db.SaveChanges();
                    ChargerLesTypesContrats();
                    Effacer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des types de contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        public void ChargerLesTypesContrats()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                lvTypeContrats.Items.Clear();
                foreach (var typeContrat in db.TypeContrats.Where(tc => tc.ProjetId == leProjetEnCours.Id && tc.TypeConstruction == leTypeConstructionEnCours).ToList())
                {
                    ListViewItem lviTypeContrat = new ListViewItem(typeContrat.LibelleTypeContrat.ToString());
                    lviTypeContrat.SubItems.Add(typeContrat.CategorieContrat.ToString());
                    lviTypeContrat.SubItems.Add(typeContrat.SeuilSouscription.ToString());
                    lviTypeContrat.SubItems.Add(typeContrat.SeuilEntreeEnVigueur.ToString());
                    if (typeContrat.Actif == true)
                        lviTypeContrat.SubItems.Add("Actif");
                    else
                        lviTypeContrat.SubItems.Add("Désactivé");


                    lviTypeContrat.Tag = typeContrat;
                    lvTypeContrats.Items.Add(lviTypeContrat);
                }

                lvTypeContrats.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des types de contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void rbReservation_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lvTypeContrats_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (lvTypeContrats.SelectedItems.Count > 0)
                {
                    leTypeContratEnCours = lvTypeContrats.SelectedItems[0].Tag as TypeContrat;
                    if (leTypeContratEnCours != null)
                    {
                        cmbProjets.SelectedValue = leTypeContratEnCours.ProjetId;
                        txtLibelle.Text = leTypeContratEnCours.LibelleTypeContrat;
                        txtSeuilSouscription.Text = leTypeContratEnCours.SeuilSouscription.ToString();
                        txtSeuilReservation.Text = leTypeContratEnCours.SeuilEntreeEnVigueur.ToString();
                        if (leTypeContratEnCours.CategorieContrat == CategorieContrat.Réservation)
                            rbReservation.Checked = true;
                        else
                            rbDepot.Checked = true;

                        if (leTypeContratEnCours.Actif == true)
                        {
                            chkActif.Checked = true;
                            cmdSupprimer.Enabled = true;
                            déactiverToolStripMenuItem.Enabled = true;
                            réactiverToolStripMenuItem.Enabled = false;
                        }
                        else
                        {
                            chkActif.Checked = false;
                            cmdSupprimer.Enabled = false;
                            déactiverToolStripMenuItem.Enabled = false;
                            réactiverToolStripMenuItem.Enabled = true;

                        }
                        ChargerLesTypeEtatAvancements();

                        Verouiller();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des types de contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ChargerLesTypeEtatAvancements()
        {
            //Afficher les niveau d'avancement
            EffacerNA();
            decimal totalTaux = 0;
            try
            {
                LvNiveauAvancements.Items.Clear();

                foreach (var na in leTypeContratEnCours.TypeEtatAvancements)
                {
                    ListViewItem lviNA = new ListViewItem(na.ordre.ToString());
                    lviNA.SubItems.Add(na.LibelleCommercial.ToString());
                    lviNA.SubItems.Add(na.TauxDecaissement.ToString("###") + "%");

                    //lviAc.SubItems.Add(ac.StatutActiviteCommerciale.ToString());

                    totalTaux += na.TauxDecaissement;
                    lviNA.Tag = na;
                    LvNiveauAvancements.Items.Add(lviNA);

                }
                txtTauxEncaissementTotal.Text = totalTaux.ToString("###") + "%";
                if (LvNiveauAvancements.Items.Count > 0)
                    LvNiveauAvancements.Items[0].Selected = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            TypeContrat typeContrat = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (!bModif)
                {
                    typeContrat = new TypeContrat();
                    typeContrat.Actif = true;
                    db.TypeContrats.Add(typeContrat);
                }
                else
                {
                    typeContrat = leTypeContratEnCours;
                }
                if (rbDepot.Checked)
                    typeContrat.CategorieContrat = CategorieContrat.Dépôt;
                else
                    typeContrat.CategorieContrat = CategorieContrat.Réservation;

                if (chkActif.Checked)
                    typeContrat.Actif = true;
                else
                    typeContrat.Actif = false;

                typeContrat.LibelleTypeContrat = txtLibelle.Text;
                typeContrat.SeuilSouscription = Int16.Parse(txtSeuilSouscription.Text);
                typeContrat.SeuilEntreeEnVigueur = Int16.Parse(txtSeuilReservation.Text);
                typeContrat.ProjetId = leProjetEnCours.Id;
                typeContrat.TypeConstruction = leTypeConstructionEnCours;


                db.SaveChanges();
                ChargerLesTypesContrats();

                Verouiller();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des types de contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var tc = db.TypeContrats.Find(typeContrat.ID);
                if (tc != null)
                    db.TypeContrats.Remove(tc);

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void déactiverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmdSupprimer_Click(sender, e);
        }

        private void réactiverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(this, "Voulez réellement réactiver ce type de contrat?", "Prosopis - Gestion des types de contrat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //db.TypeContrats.Remove(leTypeContratEnCours);
                    leTypeContratEnCours.Actif = true;
                    db.SaveChanges();
                    ChargerLesTypesContrats();
                    Effacer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des types de contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (MessageBox.Show(this, "Voulez réellement supprimer ce type de contrat?", "Prosopis - Gestion des types de contrat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //db.TypeContrats.Remove(leTypeContratEnCours);
                    db.TypeContrats.Remove(leTypeContratEnCours);
                    db.SaveChanges();
                    ChargerLesTypesContrats();
                    Effacer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des types de contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                leProjetEnCours = (Projet)cmbProjets.SelectedItem;
                if (leProjetEnCours.DenominationProjet.Trim() == "AKYS")
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

                //if (rbIlot.Checked)
                //{
                //    leTypeConstructionEnCours = TypeConstruction.Villa;
                //}
                //else
                //{
                //    leTypeConstructionEnCours = TypeConstruction.Appartement;
                //}

                ChargerLesTypesContrats();

            }
        }

        private void rbIlot_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIlot.Checked)
            {
                leTypeConstructionEnCours = TypeConstruction.Villa;

            }
            else
            {
                leTypeConstructionEnCours = TypeConstruction.Appartement;


            }
            ChargerLesTypesContrats();
        }

        private void FrmTypeContrat_Load(object sender, EventArgs e)
        {

        }

        private void LvNiveauAvancements_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (LvNiveauAvancements.SelectedItems.Count > 0)
                {
                    leTypeAvancementEnCours = LvNiveauAvancements.SelectedItems[0].Tag as TypeEtatAvancement;
                    txtOrdreNA.Text = leTypeAvancementEnCours.ordre.ToString();
                    txtNiveauNA.Text = leTypeAvancementEnCours.LibelleCommercial;
                    txtTauxNA.Text = leTypeAvancementEnCours.TauxDecaissement.ToString("###");

                    VerouillerNA();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des types de contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void VerouillerNA()
        {
            txtOrdreNA.ReadOnly=true;
            txtNiveauNA.ReadOnly = true;
            txtTauxNA.ReadOnly = true;

            cmdNewNA.Enabled = true;
            cmdEnregistrerNA.Enabled = false;
            cmdEditerNA.Enabled = true;
        }
        private void DeverouillerNA()
        {
            txtOrdreNA.ReadOnly = false;
            txtNiveauNA.ReadOnly = false;
            txtTauxNA.ReadOnly = false;

            cmdNewNA.Enabled = false;
            cmdEnregistrerNA.Enabled = true;
            cmdEditerNA.Enabled = false;
           //cmdSupprimer.Enabled = false;
        }

        private void EffacerNA()
        {
            txtOrdreNA.Text = string.Empty;
            txtNiveauNA.Text = string.Empty;
            txtTauxNA.Text = string.Empty;

            //cmdSupprimer.Enabled = false;
        }

        private void cmdNewNA_Click(object sender, EventArgs e)
        {
            EffacerNA();
            DeverouillerNA();
            bModifNA = false;
            txtOrdreNA.Focus();
        }

        private void cmdEditerNA_Click(object sender, EventArgs e)
        {

            bModifNA = true;
            DeverouillerNA();
            txtNiveauNA.Focus();

        }

        private void cmdEnregistrerNA_Click(object sender, EventArgs e)
        {
            TypeEtatAvancement typeEtatAvancement = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (!bModifNA)
                {
                    typeEtatAvancement = new TypeEtatAvancement();
                    db.TypeEtatAvancements.Add(typeEtatAvancement);
                }
                else
                {
                    typeEtatAvancement = leTypeAvancementEnCours;
                }


                typeEtatAvancement.Description = txtNiveauNA.Text;
                typeEtatAvancement.ordre = Int16.Parse(txtOrdreNA.Text);
                typeEtatAvancement.AppelFonds = true;
                typeEtatAvancement.TauxDecaissement = decimal.Parse(txtTauxNA.Text);
                typeEtatAvancement.NiveauTechnique = true;
                typeEtatAvancement.LibelleCommercial = txtNiveauNA.Text;
                typeEtatAvancement.LibelleTechnique = txtNiveauNA.Text;
                typeEtatAvancement.StatutTermine = true;
                typeEtatAvancement.ProjetId = leProjetEnCours.Id;
                typeEtatAvancement.TypeConstruction = leTypeConstructionEnCours;
                typeEtatAvancement.TypeContratId = leTypeContratEnCours.ID;



                db.SaveChanges();
                ChargerLesTypeEtatAvancements(); 

                VerouillerNA();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des types de contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdSupprimerNA_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show(this, "Voulez vous réellement supprimer cet niveau d'avencement?", "Prosopis - Gestion Type Contrat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    db.TypeEtatAvancements.Remove(leTypeAvancementEnCours);
                    db.SaveChanges();
                    MessageBox.Show(this, "Le niveau d'avencement a été supprimé avec succes",
                                       "Prosopis - Gestion Type Contrat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ChargerLesTypeEtatAvancements();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                        "Prosopis - Gestion Type Contrat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
