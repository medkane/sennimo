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
    public partial class FrmVilla : Form
    {
        private SenImmoDataContext db;
        private Lot villaEnCours;

        public FrmVilla()
        {
            InitializeComponent();
            db = new SenImmoDataContext();
            ChargerLesTypeVilla();
            ChargerLesIlots();
            cmbPositions.DataSource = Enum.GetValues(typeof(PositionLot));
            cmbPositions.SelectedIndex = -1;
            cmbStatuts.DataSource = Enum.GetValues(typeof(StatutLot));
            cmbStatuts.SelectedIndex = -1;
            ChargerLesTypesEtatAvancements();
        }
        public FrmVilla(Lot villa):this()
        {
            bAfficherVilla = true;
            villaEnCours=villa;
            
        }

        private void ChargerLesTypeVilla()
        {
            cmbTypeVillas.DataSource = (from tvl in db.TypeVillas
                                        select tvl).ToList();
            cmbTypeVillas.DisplayMember = "CodeType";
            cmbTypeVillas.ValueMember = "TypeVillaId";
            cmbTypeVillas.SelectedIndex = -1;
        }


        private void ChargerLesIlots()
        {
            cmbIlots.DataSource = (from ilt in db.Ilots
                                   select ilt).ToList();
            cmbIlots.DisplayMember = "NomIlot";
            cmbIlots.ValueMember = "IlotId";
            cmbIlots.SelectedIndex = -1;
        }

        private void ChargerLesTypesEtatAvancements()
        {
            cmbTypeEtatAvancement.DataSource = (from tea in db.TypeEtatAvancements
                                                select tea).ToList();
            cmbTypeEtatAvancement.DisplayMember = "Description";
            cmbTypeEtatAvancement.ValueMember = "ID";
            cmbTypeEtatAvancement.SelectedIndex = -1;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmbTypeVillas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTypeVillas.SelectedIndex >= 0)
            {
                var typeVilla = (TypeVilla)cmbTypeVillas.SelectedItem;
                txtSurfaceStandard.Text = typeVilla.SurfaceDeBase.ToString();
                txtClasseVilla.Text = typeVilla.ClasseVilla.ToString();
                //txtPrixStandard.Text = typeVilla.Pri.ToString();
                txtNbChambre.Text = typeVilla.Chambre.ToString();
                txtNbChambreAvecSDB.Text = typeVilla.ChambreAvecSalleDeBain.ToString();
                txtSalon.Text = typeVilla.Salon.ToString();
                txtCuisine.Text = typeVilla.Cuisine.ToString();
                txtPatio.Text = typeVilla.Patio.ToString();
                txtCoursArriere.Text = typeVilla.CourArriere.ToString();
                txtToilette.Text = typeVilla.Toilette.ToString();
                txtPrixRevise.Text = (txtPrixRevise.Text == string.Empty || txtPrixRevise.Text == txtPrixStandard.Text) ? typeVilla.PrixStandard.ToString() : txtPrixRevise.Text;
                txtPrixStandard.Text = typeVilla.PrixStandard.ToString();
            }
        }

        private void cmdEnregistrerVilla_Click(object sender, EventArgs e)
        {
            if (txtNumeroVilla.Text == string.Empty)
            {
                MessageBox.Show(this, "Veuillez saisir le numéro de la villa",
                         "Prosopis -  Gestion des villas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!bModifVilla)
            {
                db.Lots.Add(new Lot()
                {
                    NumeroLot = txtNumeroVilla.Text,
                    IlotID = ((Ilot)cmbIlots.SelectedItem).Id,
                    Ilot = ((Ilot)cmbIlots.SelectedItem),
                    PositionLot = ((PositionLot)cmbPositions.SelectedItem),
                    StatutLot = ((StatutLot)cmbStatuts.SelectedItem),
                    Superficie = decimal.Parse(txtSuperficieReelle.Text),
                    TypeVillaID=((TypeVilla)cmbTypeVillas.SelectedItem).TypeVillaId,
                    PrixRevise = decimal.Parse(txtPrixRevise.Text)
                });
                MessageBox.Show(this, "La villa a été enregistrée avec succes",
                         "Prosopis -  Gestion des villas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //ClientEnCours.RaisonSocialClient = txtRaisonSocialClient.Text;
                //ClientEnCours.TelephoneClient = txtTelephoneClient.Text;
                //ClientEnCours.EmailClient = txtEmailClient.Text;
                //ClientEnCours.AdresseClient = txtAdresseClient.Text;
                //ClientEnCours.MarqueClient = txtMarqueClient.Text;
                //MessageBox.Show(this, "Le client a été modifié",
                //        "GesAGRO - Gestion des Sites", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            db.SaveChanges();
            //chargerClients();
            //EffacerVi();
        }
        private void AfficherVilla(Lot vl)
        {
            txtNumeroVilla.Text = vl.NumeroLot.ToString();
            txtPrixRevise.Text = vl.PrixRevise.ToString();
            txtSuperficieReelle.Text = vl.Superficie.ToString();
            cmbTypeVillas.SelectedItem = vl.TypeVilla;
            cmbIlots.SelectedItem = vl.Ilot;
            cmbPositions.SelectedItem = vl.PositionLot;
            cmbStatuts.SelectedItem = vl.StatutLot;
            ChargerEtatAvancementsVilla(vl.ID);
        }


        public bool bModifVilla { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (villaEnCours!=null)
            {
                villaEnCours = (from vil in db.Lots
                                where vil.NumeroLot == villaEnCours.NumeroLot
                                select vil).First();
                AfficherVilla(villaEnCours); 
            }
        }

        public bool bAfficherVilla { get; set; }

        private void FrmVilla_Load(object sender, EventArgs e)
        {
            //if(bAfficherVilla)
            //    AfficherVilla(villaEnCours);
            button1_Click(sender, e);
            //txtCoursArriere.Text = string.Empty;
            //txtCuisine.Text = string.Empty;
            //txtDateSaisieEtatAvancement.Text = string.Empty;
            //txtNbChambre.Text = string.Empty;
            //txtNbChambreAvecSDB.Text = string.Empty;
            //txtNumeroVilla.Text = string.Empty;
            //txtPatio.Text = string.Empty;
            //txtPrixRevise.Text = string.Empty;
            //txtPrixStandard.Text = string.Empty;
            //txtSalon.Text = string.Empty;
            //txtSuperficieReelle.Text = string.Empty;
            //txtSurfaceStandard.Text = string.Empty;
            //txtToilette.Text = string.Empty;
            //txtClasseVilla.Text = string.Empty;
        }

        private void cmdNouvelEtatAvancement_Click(object sender, EventArgs e)
        {
            
            if (pEtatAvancement.Visible)
            {
                cmdNouvelEtatAvancement.Text = "Nouvel état d'avancement";
                pEtatAvancement.Visible = false;
            }
            else
            {
                cmdNouvelEtatAvancement.Text = "Annuler";
                txtDateSaisieEtatAvancement.Text = DateTime.Today.ToShortDateString();
                pEtatAvancement.Visible = true;
            }
        }

        private void cmdAjouterEtatAvancement_Click(object sender, EventArgs e)
        {
             txtDateSaisieEtatAvancement.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (txtDateSaisieEtatAvancement.Text!=string.Empty)
	        {
                txtDateSaisieEtatAvancement.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                DateTime dDateEtatAvancement=DateTime.Parse(txtDateSaisieEtatAvancement.Text);
                int typeEtatAvId=((TypeEtatAvancement)cmbTypeEtatAvancement.SelectedItem).ID;
                 int villaId=villaEnCours.ID;
		        pEtatAvancement.Visible = false;
                    
                    db.EtatAvancements.Add(
                        new EtatAvancement()
                        {
                            DateSaisieAvancement=dDateEtatAvancement,
                            TypeEtatAvancementID=typeEtatAvId,
                            LotId = villaId
                        });
                    db.SaveChanges();
                    ChargerEtatAvancementsVilla(villaId);
	        }

        }

        private void ChargerEtatAvancementsVilla(int villaId)
        {
            //dgEtatAvancements.DataSource = db.EtatAvancements.Where(ea => ea.VillaID == villaId).ToList();
            dgEtatAvancements.DataSource = (from ea in db.EtatAvancements
                                            where ea.LotId == villaId
                                            select new
                                            {
                                                Date = ea.DateSaisieAvancement,
                                                Avancement = ea.TypeEtatAvancement.Description,
                                                Taux = ea.TypeEtatAvancement.TauxDecaissement
                                            }).ToList();
            //dgContrats.Columns[1].HeaderText = "Avancement";
            //dgContrats.Columns[1].HeaderText = "Taux";
            dgEtatAvancements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void cmbStatuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatuts.SelectedIndex != 0 && cmbStatuts.SelectedIndex != -1)
                cmdVoirContrat.Visible = true;
            else
                cmdVoirContrat.Visible = false;
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
