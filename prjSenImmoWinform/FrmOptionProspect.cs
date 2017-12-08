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
    public partial class FrmOptionProspect : Form
    {
        private ContratRepository contratRep;
        private IlotRepository IlotRep;
        private ClientRepository clientRep;
        private TypeContrat leTypeContratEnCours;
        private TypeVilla leTypeVillaDepotEnCours;
        private Client leProspectEnCours;
        private Lot leLotEnCours;
        private decimal dPrixDeVente;
        private PositionLot laPositionLotEnCours;
        private decimal dMontantRemise;
        private decimal dTauxRemise;
        private decimal dPrixRevise;
        private decimal laSuperficieReelleEnCours;
        private TypeConstruction leTypeConstructiontEnCours;

        private Projet LeProjetEnCours;

        public FrmOptionProspect()
        {

            InitializeComponent();
            contratRep = new ContratRepository();
            IlotRep = new IlotRepository();
            clientRep = new ClientRepository();
            //cmbTypeContrats.DataSource = Enum.GetValues(typeof(CategorieContrat));
            //cmbTypeContrats.SelectedIndex = -1;


            ChargerLesProjets();

            dtpDatePriseOption.CustomFormat = " "; //An empty SPACE;
            dtpDatePriseOption.Format = DateTimePickerFormat.Custom;

            dtpDateFinOption.CustomFormat = " "; //An empty SPACE;
            dtpDateFinOption.Format = DateTimePickerFormat.Custom;
           
        }

        public FrmOptionProspect(Client prospect) : this()
        {
            leProspectEnCours = clientRep.GetClient(prospect.ID);
            cmbProjets.SelectedValue = leProspectEnCours.ProjetId;
            cmbProjets.Enabled = false;
            tcTypeContrat.Enabled = false;
            if (leProspectEnCours.Projet.DenominationProjet == "AKYS")
            { 
                pTypeConstruction.Visible = false;
                txtTypeConstructionResa.Visible = false;
                tcTypeContrat.Enabled = true;
                leTypeConstructiontEnCours = TypeConstruction.Villa;
            }
           
           
            cmbTypeContratResa.DataSource = contratRep.GetTypeContrats().Where(tc =>tc.Actif==true && tc.ProjetId== leProspectEnCours.ProjetId && tc.CategorieContrat == CategorieContrat.Réservation).ToList();
            cmbTypeContratResa.DisplayMember = "LibelleTypeContrat";
            cmbTypeContratResa.SelectedIndex = -1;

            cmbTypeContratDepot.DataSource = contratRep.GetTypeContrats().Where(tc => tc.Actif == true && tc.ProjetId == leProspectEnCours.ProjetId && tc.CategorieContrat == CategorieContrat.Dépôt).ToList();
            cmbTypeContratDepot.DisplayMember = "LibelleTypeContrat";
            cmbTypeContratDepot.SelectedIndex = -1;
            
            cmbTypeVillaDepot.DataSource = contratRep.GetTypeVillas().Where(tc => tc.ProjetId == leProspectEnCours.ProjetId).ToList();
            cmbTypeVillaDepot.DisplayMember = "NomComplet";
            cmbTypeVillaDepot.SelectedIndex = -1;

            laPositionLotEnCours = PositionLot.Standard;
            leTypeContratEnCours = contratRep.GetTypeContrat(CategorieContrat.Réservation, 5);

         
            AfficherProspect(leProspectEnCours);
            cmdEnregistrer.Enabled = false;
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
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void AfficherProspect(Client prospect)
        {
            txtPrenom.Text = prospect.Prenom;
            txtNom.Text = prospect.Nom;
            txtAdresse.Text = prospect.Adresse;
            txtTelephoneMobile.Text = prospect.Mobile1;
            txtEmail.Text = prospect.Email;
            if (prospect.Cooperative != null)
            {
                chkRemise.Checked = true;
                chkRemise.Text = "Remise coopérative " + prospect.Cooperative.Denomination;
                txtTauxRemise.Text = prospect.Cooperative.TauxRemise.ToString();
            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void EffacerChoixTypeVilla()
        {
            txtSuperficieDeBaseDepot.Text = string.Empty;
            txtPRixStandardDepot.Text = string.Empty;
            txtPrixReviseDepot.Text = string.Empty;
            txtPrixVente.Text = string.Empty;
            chkAngleDepot.Checked = false;
            //cmbTypeVillaDepot.SelectedIndex = -1;
        }

        private void EffacerChoixLot()
        {
            txtNumeroLot.Text = string.Empty;
            txtTypeVilla.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtPrixStandard.Text = string.Empty;
            txtPrixRevise.Text = string.Empty;
            txtPrixVente.Text = string.Empty;
            txtSuperficieDeBase.Text = string.Empty;
            txtSuperficieReelle.Text = string.Empty;
        }

        //private void cmbTypeContrats_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    EffacerChoixTypeVilla();
        //    EffacerChoixLot();
        //    leLotEnCours = null;




        //    cmbTypeVillaDepot.SelectedIndex = -1;
        //    if (cmbTypeContrats.SelectedItem != null)
        //    {
        //        try
        //        {
        //            var catContrat = (CategorieContrat)cmbTypeContrats.SelectedItem;

        //            if (catContrat == CategorieContrat.Réservation)
        //            {
        //                pDepot.Visible = false;
        //                tcChoixLot.SelectedTab = tcChoixLot.TabPages[1];
        //                tcChoixLot.Enabled = true;
        //                leTypeContratEnCours = contratRep.GetTypeContrat(CategorieContrat.Réservation, 5);
        //            }
        //            else
        //            {
        //                pDepot.Visible = true;
        //                tcChoixLot.SelectedTab = tcChoixLot.TabPages[0];
        //                tcChoixLot.Enabled = false;
        //                rbCinqPourcent.Checked = false;
        //                rbQuinzePourcent.Checked = false;
        //            }
        //            //leTypeContratEnCours = (CategorieContrat)cmbTypeContrats.SelectedItem;
        //        }
        //        catch (Exception ex)
        //        {

        //            MessageBox.Show(this, "Erreur:..." + ex.Message,
        //                    "Prosopis -  Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }


        //    }
        //    else
        //    {
        //        leTypeContratEnCours = null;
        //        tcChoixLot.Enabled = false;
        //    }
        //}

        private void rbCinqPourcent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //pDepot.Visible = true;
                //tcChoixLot.SelectedTab = tcChoixLot.TabPages[0];
                if (rbCinqPourcent.Checked)
                {
                    //tcChoixLot.Enabled = true;
                    leTypeContratEnCours = contratRep.GetTypeContrat(CategorieContrat.Dépôt, 5);
                    pParametreDepot.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis -  Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTypeVillaDepot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTypeVillaDepot.SelectedItem != null)
            {
                EffacerChoixTypeVilla();
                try
                {
                    leTypeVillaDepotEnCours = (TypeVilla)cmbTypeVillaDepot.SelectedItem;
                    txtSuperficieDeBaseDepot.Text = leTypeVillaDepotEnCours.SurfaceDeBase.ToString();
                    txtPRixStandardDepot.Text = leTypeVillaDepotEnCours.PrixStandard.ToString("### ### ###");
                    leLotEnCours = IlotRep.GetLotVirtuel(leTypeVillaDepotEnCours, PositionLot.Standard);

                    //if (leLotEnCours!=null)
                    //{
                    //    dPrixDeVente = leLotEnCours.PrixRevise;
                    //    txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
                    //    if (txtTauxRemise.Text != null)
                    //    {
                    //        txtTauxRemise.Focus();
                    //        txtMontantRemise.Focus();
                    //        txtPrixVente.Focus();
                    //    } 
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Erreur:..." + ex.Message,
                           "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Afficherlot(Lot lot)
        {
            try
            {
                //cmbTypeContrats.SelectedItem = CategorieContrat.Réservation;
                txtNumeroLot.Text = lot.NumeroLot;
                txtTypeVilla.Text = lot.TypeVilla.NomType;
                txtSuperficieDeBase.Text = lot.TypeVilla.SurfaceDeBase.ToString();
                txtSuperficieReelle.Text = lot.Superficie.ToString();
                txtPosition.Text = lot.PositionLot.ToString();
                txtPrixStandard.Text = lot.TypeVilla.PrixStandard.ToString("### ### ###");
                txtTypeConstructionResa.Text = lot.TypeVilla.TypeConstruction.ToString();
                txtPrixRevise.Text = lot.PrixRevise.ToString("### ### ###");
                //txtPrixVente.Text = lot.PrixRevise.ToString("### ### ###");
                //dPrixDeVente = lot.PrixRevise != 0 ? lot.PrixRevise : lot.TypeVilla.PrixStandard;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmdChoixLot_Click(object sender, EventArgs e)
        {
            try
            {
                string strProjet = leProspectEnCours.Projet.DenominationProjet;
                //FrmDetailsIlot frmDetIlot = new FrmDetailsIlot("SelectionLot");
                FrmDetailsIlot frmDetIlot = new FrmDetailsIlot(leProspectEnCours.Projet,leTypeConstructiontEnCours);
                //frmDetIlot.MdiParent = this.MdiParent;
                frmDetIlot.WindowState = FormWindowState.Normal;
                frmDetIlot.StartPosition = FormStartPosition.CenterParent;
                frmDetIlot.leProjetEnCours = leProspectEnCours.Projet;
                frmDetIlot.ShowDialog(this);
                leLotEnCours = frmDetIlot.GetLotSelectionne();
                if (leLotEnCours != null)
                {
                    Afficherlot(leLotEnCours);

                    //dPrixDeVente = leLotEnCours.PrixRevise;
                    //txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
                    //if (txtTauxRemise.Text != null)
                    //{
                    //    txtTauxRemise.Focus();
                    //    txtMontantRemise.Focus();
                    txtPrixVente.Text=string.Empty;
                    //}
                }
                //else
                //    MessageBox.Show(this, "Erreur: lors de l'attribution du contrat",
                //    "Prosopis -  Attribution de lots", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis - Gestion des ventes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                CategorieContrat categorieContrat = 0;
                if (tcTypeContrat.SelectedIndex == 1)
                    categorieContrat = CategorieContrat.Dépôt;
                else
                    categorieContrat = CategorieContrat.Réservation;
                if (categorieContrat != null)
                {

                    switch (categorieContrat)
                    {
                        case CategorieContrat.Dépôt:
                            contratRep.AddOptionProspectDepot(leProspectEnCours.ID, leTypeContratEnCours.ID, leLotEnCours.TypeVillaID, leLotEnCours.TypeVilla.PrixStandard,
                                dPrixRevise,laPositionLotEnCours,laSuperficieReelleEnCours, leProspectEnCours.CommercialID.Value, dPrixDeVente, chkLimitationDuree.Checked, 
                                dtpDatePriseOption.Value.Date, dtpDateFinOption.Value.Date, leLotEnCours.ID, dMontantRemise, dTauxRemise);
                            break;
                        case CategorieContrat.Réservation:
                            contratRep.AddOptionProspectResa(leProspectEnCours.ID, leTypeContratEnCours.ID, leLotEnCours.TypeVillaID,leLotEnCours.TypeVilla.PrixStandard,dPrixRevise,
                                leLotEnCours.PositionLot, leLotEnCours.Superficie, leLotEnCours.ID, leProspectEnCours.CommercialID.Value, dPrixDeVente, chkLimitationDuree.Checked, 
                                dtpDatePriseOption.Value.Date, dtpDateFinOption.Value.Date, dMontantRemise, dTauxRemise);
                            break;
                        default:
                            break;
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                       "Prosopis - Gestion des Options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCommentaires_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dtpDateFinOption_ValueChanged(object sender, EventArgs e)
        {
            dtpDateFinOption.CustomFormat = "dd/MM/yyyy";
        }

        private void txtDureeOption_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dtpDatePriseOption_ValueChanged(object sender, EventArgs e)
        {
            dtpDatePriseOption.CustomFormat = "dd/MM/yyyy";
            dtpDateFinOption.Value = dtpDatePriseOption.Value.AddMonths(1);
            txtDureeOption.Text = (dtpDateFinOption.Value.Date - dtpDatePriseOption.Value.Date).Days.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tcChoixLot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkLimitationDuree_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLimitationDuree.Checked)
            {
                pLimitationDuree.Enabled = true;
            }
            else
            {
                pLimitationDuree.Enabled = false;
                txtDureeOption.Text = string.Empty;
                dtpDatePriseOption.CustomFormat = " "; //An empty SPACE;
                dtpDatePriseOption.Format = DateTimePickerFormat.Custom;
                dtpDateFinOption.CustomFormat = " "; //An empty SPACE;
                dtpDateFinOption.Format = DateTimePickerFormat.Custom;
            }
        }

        private void chkAngleDepot_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (chkAngleDepot.Checked)
            //    {
            //        laPositionLotEnCours = PositionLot.Angle;
            //        leLotEnCours = IlotRep.GetLotVirtuel(leTypeVillaDepotEnCours, laPositionLotEnCours);
            //        if (txtSurfaceReelle.Text != string.Empty)
            //        {
            //            laSuperficieReelleEnCours = decimal.Parse(txtSurfaceReelle.Text);
            //        }
            //        else
            //        {
            //            laSuperficieReelleEnCours = leLotEnCours.TypeVilla.SurfaceDeBase;
            //        }
            //        dPrixRevise = CalculerPrixRevise(leLotEnCours.TypeVilla, laPositionLotEnCours, laSuperficieReelleEnCours);
            //        txtPrixStandard.Text= leLotEnCours.TypeVilla.PrixStandard.ToString("### ### ###");
            //        txtPrixReviseDepot.Text = dPrixRevise.ToString("### ### ###");
            //        txtPrixVente.Text = dPrixRevise.ToString("### ### ###");
            //        dPrixDeVente = dPrixRevise;
            //        if(txtTauxRemise.Text!=string.Empty)
            //        {

            //            dTauxRemise = decimal.Parse(txtTauxRemise.Text);
            //            dMontantRemise = 0;
            //            contratRep.CalculerRemise(ref dPrixDeVente, ref dTauxRemise, ref dMontantRemise);
            //            txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
            //            txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
            //        }


            //    }
            //    else
            //    {
            //        laPositionLotEnCours = PositionLot.Standard;
            //        leLotEnCours = IlotRep.GetLotVirtuel(leTypeVillaDepotEnCours, laPositionLotEnCours);
                    
            //        if (txtSurfaceReelle.Text != string.Empty)
            //        {
            //            laSuperficieReelleEnCours = decimal.Parse(txtSurfaceReelle.Text);
            //        }
            //        else
            //        {
            //            laSuperficieReelleEnCours = leLotEnCours.TypeVilla.SurfaceDeBase;
            //        }
            //        dPrixRevise = CalculerPrixRevise(leLotEnCours.TypeVilla, laPositionLotEnCours, laSuperficieReelleEnCours);
            //        txtPrixStandard.Text = leLotEnCours.TypeVilla.PrixStandard.ToString("### ### ###");
            //        txtPrixReviseDepot.Text = dPrixRevise.ToString("### ### ###");
            //        txtPrixVente.Text = dPrixRevise.ToString("### ### ###");
            //        dPrixDeVente = dPrixRevise;
            //        if (txtTauxRemise.Text != string.Empty)
            //        {

            //            dTauxRemise = decimal.Parse(txtTauxRemise.Text);
            //            dMontantRemise = 0;
            //            contratRep.CalculerRemise(ref dPrixDeVente, ref dTauxRemise, ref dMontantRemise);
            //            txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
            //            txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(this, "Erreur:..." + ex.Message,
            //               "Prosopis -  Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void txtPrixReviseDepot_TextChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void txtPRixStandardDepot_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDureeOption_Validated(object sender, EventArgs e)
        {
            try
            {

                dtpDateFinOption.Value = dtpDatePriseOption.Value.AddDays(Int16.Parse(txtDureeOption.Text));
                //txtDureeOption.Text = (dtpDateFinOption.Value.Date - dtpDatePriseOption.Value.Date).Days.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                           "Prosopis -  Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbQuinzePourcent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //pDepot.Visible = true;
                //tcChoixLot.SelectedTab = tcChoixLot.TabPages[0];
                if (rbQuinzePourcent.Checked)
                {
                    //tcChoixLot.Enabled = true;
                    leTypeContratEnCours = contratRep.GetTypeContrat(CategorieContrat.Dépôt, 15);
                    pParametreDepot.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis -  Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void txtPrixRevise_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtSuperficieReelle_TextChanged(object sender, EventArgs e)
        {

        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void txtTypeVilla_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPosition_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtSuperficieDeBase_TextChanged(object sender, EventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void txtPrixStandard_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumeroLot_TextChanged(object sender, EventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void txtPrixVente_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EffacerChoixTypeVilla();
            EffacerChoixLot();
            leLotEnCours = null;
            pParametreDepot.Enabled = false;
            if (tcTypeContrat.SelectedIndex == 0)
            {
                //pDepot.Visible = false;
                //tcChoixLot.SelectedTab = tcChoixLot.TabPages[1];
                //tcChoixLot.Enabled = true;
                leTypeContratEnCours = contratRep.GetTypeContrat(CategorieContrat.Réservation, 5);
            }
            else
            {
                ////pDepot.Visible = true;
                //tcChoixLot.SelectedTab = tcChoixLot.TabPages[0];
                //tcChoixLot.Enabled = false;
                rbCinqPourcent.Checked = false;
                rbQuinzePourcent.Checked = false;

            }
        }

        private void FrmOptionProspect_Load(object sender, EventArgs e)
        {

        }

        private void chkRemise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pRemise.Enabled = chkRemise.Checked;
                if (chkRemise.Checked)
                {
                    //        //Afficherlot(leLotEnCours);
                    //        //EffacerChoixLot();
                    //        if (leTypeContratEnCours.CategorieContrat == CategorieContrat.Dépôt)
                    //        {
                    //            txtTauxRemise.Text = string.Empty;
                    //            txtMontantRemise.Text = string.Empty;
                    //            leLotEnCours = IlotRep.GetLotVirtuel(leTypeVillaDepotEnCours, PositionLot.Angle);

                    //            if (txtSurfaceReelle.Text != string.Empty)
                    //            {
                    //                laSuperficieReelleEnCours = decimal.Parse(txtSurfaceReelle.Text);
                    //            }
                    //            else
                    //            {
                    //                laSuperficieReelleEnCours = leLotEnCours.TypeVilla.SurfaceDeBase;
                    //            }
                    //            dPrixRevise = CalculerPrixRevise(leLotEnCours.TypeVilla, leLotEnCours.PositionLot, laSuperficieReelleEnCours);
                    //            txtPrixStandard.Text = leLotEnCours.TypeVilla.PrixStandard.ToString("### ### ###");
                    //            txtPrixReviseDepot.Text = dPrixRevise.ToString("### ### ###");
                    //            txtPrixVente.Text = dPrixRevise.ToString("### ### ###");
                    //            dPrixDeVente = dPrixRevise;
                    //            dTauxRemise = 0;
                    //            dMontantRemise = 0;
                    //        }
                    txtTauxRemise.Text = string.Empty;
                    txtMontantRemise.Text = string.Empty;
                }
                else
                {
                    //        if (leTypeContratEnCours.CategorieContrat == CategorieContrat.Dépôt)
                    //        {
                    //            leLotEnCours = IlotRep.GetLotVirtuel(leTypeVillaDepotEnCours, PositionLot.Angle);
                    //            laPositionLotEnCours = PositionLot.Angle;
                    //            if (txtSurfaceReelle.Text != string.Empty)
                    //            {
                    //                laSuperficieReelleEnCours = decimal.Parse(txtSurfaceReelle.Text);
                    //            }
                    //            else
                    //            {
                    //                laSuperficieReelleEnCours = leLotEnCours.TypeVilla.SurfaceDeBase;
                    //            }
                    //            dPrixRevise = CalculerPrixRevise(leLotEnCours.TypeVilla, leLotEnCours.PositionLot, laSuperficieReelleEnCours);
                    //            txtPrixStandard.Text = leLotEnCours.TypeVilla.PrixStandard.ToString("### ### ###");
                    //            txtPrixReviseDepot.Text = dPrixRevise.ToString("### ### ###");
                    //            txtPrixVente.Text = dPrixRevise.ToString("### ### ###");
                    //            dPrixDeVente = dPrixRevise;
                    //            if (txtTauxRemise.Text != string.Empty)
                    //            {

                    //                dTauxRemise = decimal.Parse(txtTauxRemise.Text);
                    //                dMontantRemise = 0;
                    //                contratRep.CalculerRemise(ref dPrixDeVente, ref dTauxRemise, ref dMontantRemise);
                    //                txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
                    //                txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
                    //            }
                    //        }
                    txtTauxRemise.Text = string.Empty;
                    txtMontantRemise.Text = string.Empty;
                }
            }
                    catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                       "Prosopis -  Gestion des ventes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMontantRemise_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void txtMontantRemise_Validated(object sender, EventArgs e)
        {

            //if (txtMontantRemise.Text != string.Empty)
            //{
            //    try
            //    {
            //        dPrixDeVente = leLotEnCours.PrixRevise;

            //        dTauxRemise = 0;
            //        dMontantRemise = decimal.Parse(txtMontantRemise.Text);
            //        contratRep.CalculerRemise(ref dPrixDeVente, ref dTauxRemise, ref dMontantRemise);
            //        txtTauxRemise.Text = dTauxRemise.ToString("###.##");
            //        txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");

            //    }
            //    catch
            //    {
            //        MessageBox.Show(this, "Vérifier le montant saisi",
            //            "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    txtMontantRemise.Text = string.Empty;
            //    //txtPrixVente.Text = string.Empty;
            //}
        }

        private void txtTauxRemise_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTauxRemise_Validated(object sender, EventArgs e)
        {
            //if (txtTauxRemise.Text != string.Empty)
            //{
            //    try
            //    {
            //        dPrixDeVente = leLotEnCours.PrixRevise;

            //        dTauxRemise = decimal.Parse(txtTauxRemise.Text);
            //        dMontantRemise = 0;
            //        //contratRep.CalculerRemise(ref dPrixDeVente, ref dTauxRemise, ref dMontantRemise);
            //        //txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
            //        //txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");

            //    }
            //    catch
            //    {
            //        MessageBox.Show(this, "Vérifier le montant saisi",
            //            "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    txtMontantRemise.Text = string.Empty;
            //    //txtPrixVente.Text = string.Empty;
            //}
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtSurfaceReelle.Text!=string.Empty)
            //    {

            //        leLotEnCours = IlotRep.GetLotVirtuel(leTypeVillaDepotEnCours, laPositionLotEnCours);
            //        //laPositionLotEnCours = PositionLot.Angle;
            //        txtPrixReviseDepot.Text = leLotEnCours.PrixRevise.ToString("### ### ###");
            //        txtPrixVente.Text = leLotEnCours.PrixRevise.ToString("### ### ###");
            //        laSuperficieReelleEnCours = decimal.Parse(txtSurfaceReelle.Text);
            //        //var prixStandard = leTypeVillaEnCours.PrixStandard;
            //        //var superficieStandard = leTypeVillaEnCours.SurfaceDeBase;
            //        //superficieReelle = decimal.Parse(txtSuperficieReelle.Text);
            //        //position = (PositionLot)cmbPositions.SelectedItem;


            //        //var differenceSurface = superficieReelle - superficieStandard;
            //        //var differencePrix = differenceSurface * 40000;
            //        //prixRevise = prixStandard + differencePrix;

            //        //if (position == PositionLot.Angle)
            //        //    prixRevise += prixRevise * 10 / 100;

            //        dPrixRevise = CalculerPrixRevise(leLotEnCours.TypeVilla, leLotEnCours.PositionLot, laSuperficieReelleEnCours);
            //        txtPrixReviseDepot.Text = dPrixRevise.ToString("### ### ###");
            //        txtPrixVente.Text = dPrixRevise.ToString("### ### ###");
            //        dPrixDeVente = dPrixRevise;
            //        if (txtTauxRemise.Text != string.Empty)
            //        {

            //            dTauxRemise = decimal.Parse(txtTauxRemise.Text);
            //            dMontantRemise = 0;
            //            contratRep.CalculerRemise(ref dPrixDeVente, ref dTauxRemise, ref dMontantRemise);
            //            txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
            //            txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
            //        }
            //    }
            //}
            
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, "Erreur:" + ex.Message,
            //           "Prosopis - Gestion des Options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
}


        private decimal CalculerPrixRevise(TypeVilla leTypeVillaEnCours, PositionLot position, decimal superficieReelle)
        {
            try
            {
                decimal prixRevise = 0;
                //var prixStandard = leTypeVillaEnCours.PrixStandard;
                var prixStandard = dPrixRevise;
                var superficieStandard = leTypeVillaEnCours.SurfaceDeBase;

                var differenceSurface = superficieReelle - superficieStandard;
                var differencePrix = differenceSurface * 40000;
                prixRevise = prixStandard + differencePrix;
                if (position == PositionLot.Angle)
                    prixRevise += prixRevise * 10 / 100;
                return prixRevise;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtTauxRemise_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void cmdCalculerPrixDeVente_Click(object sender, EventArgs e)
        {
            try
            {
                dPrixRevise = 0;
                dPrixDeVente = 0;
                dTauxRemise = 0;
                dMontantRemise = 0;
                //S'il s'agit d'un contrat dépot
                if (leTypeContratEnCours.CategorieContrat == CategorieContrat.Dépôt)
                {
                    leTypeVillaDepotEnCours = (TypeVilla)cmbTypeVillaDepot.SelectedItem;
                    laPositionLotEnCours = PositionLot.Standard;
                    if(leTypeConstructiontEnCours == TypeConstruction.Villa)
                        leLotEnCours = IlotRep.GetLotVirtuel(leTypeVillaDepotEnCours, laPositionLotEnCours);
                    else
                        leLotEnCours = IlotRep.GetLotVirtuel(leTypeVillaDepotEnCours, laPositionLotEnCours);

                    dPrixRevise = leLotEnCours.PrixRevise;
                    if(txtPrixdeVenteRectifieDepot.Text!=string.Empty)
                    {
                        dPrixRevise = decimal.Parse(txtPrixdeVenteRectifieDepot.Text);
                    }
                    dPrixDeVente = dPrixRevise;
                    laSuperficieReelleEnCours = leLotEnCours.TypeVilla.SurfaceDeBase;
                    if (txtSurfaceReelle.Text != string.Empty && chkAngleDepot.Checked)
                    {
                        laPositionLotEnCours = PositionLot.Angle;
                        laSuperficieReelleEnCours = decimal.Parse(txtSurfaceReelle.Text);
                        dPrixRevise = CalculerPrixRevise(leLotEnCours.TypeVilla, laPositionLotEnCours, laSuperficieReelleEnCours);
                        dPrixDeVente = dPrixRevise;
                    }
                    else
                       //Si angle
                       if (chkAngleDepot.Checked)
                        {
                            laPositionLotEnCours = PositionLot.Angle;
                            dPrixRevise = CalculerPrixRevise(leLotEnCours.TypeVilla, laPositionLotEnCours, leLotEnCours.TypeVilla.SurfaceDeBase);
                            dPrixDeVente = dPrixRevise;
                        }

                        //si la surface est révisée
                       else if (txtSurfaceReelle.Text != string.Empty)
                        {
                            laSuperficieReelleEnCours = decimal.Parse(txtSurfaceReelle.Text);
                            dPrixRevise = CalculerPrixRevise(leLotEnCours.TypeVilla, laPositionLotEnCours, laSuperficieReelleEnCours);
                            dPrixDeVente = dPrixRevise;
                        }
                   

                    //si Remise
                    if (chkRemise.Checked)
                    {
                        //contratRep.CalculerRemise(ref dPrixDeVente, ref dTauxRemise, ref dMontantRemise);
                        if (txtTauxRemise.Text != string.Empty)
                        {
                            dTauxRemise = decimal.Parse(txtTauxRemise.Text);
                            dMontantRemise =  (dPrixDeVente * dTauxRemise / 100);
                            dPrixDeVente = dPrixDeVente - dMontantRemise;
                        }
                        else if (txtMontantRemise.Text != string.Empty)
                        {
                            dMontantRemise = decimal.Parse(txtMontantRemise.Text);
                            dTauxRemise = (dMontantRemise / dPrixDeVente) * 100;
                            dPrixDeVente = dPrixDeVente - dMontantRemise;
                        }
                    }
                    txtPrixReviseDepot.Text = dPrixRevise.ToString("### ### ###");
                    txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
                    txtTauxRemise.Text = dTauxRemise.ToString("##0");
                    txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
                }
                else
                {
                    dPrixRevise = leLotEnCours.PrixRevise;
                    if (txtPrixdeVenteRectifie.Text != string.Empty)
                    {
                        dPrixRevise = decimal.Parse(txtPrixdeVenteRectifie.Text);
                    }
                    dPrixDeVente = dPrixRevise;
                    if (chkRemise.Checked)
                    {
                        //dTauxRemise = decimal.Parse(txtTauxRemise.Text);
                        dMontantRemise = 0;
                        //contratRep.CalculerRemise(ref dPrixDeVente, ref dTauxRemise, ref dMontantRemise);
                        if (txtTauxRemise.Text != string.Empty)
                        {
                            dTauxRemise = decimal.Parse(txtTauxRemise.Text);
                            dMontantRemise = (dPrixDeVente * dTauxRemise / 100);
                            dPrixDeVente = dPrixDeVente - dMontantRemise;
                        }
                        else if (txtMontantRemise.Text != string.Empty)
                        {
                            dMontantRemise = decimal.Parse(txtMontantRemise.Text);
                            dTauxRemise = (dMontantRemise/dPrixDeVente) * 100;
                            dPrixDeVente = dPrixDeVente - dMontantRemise;
                        }
                    }
                    txtPrixReviseDepot.Text = dPrixRevise.ToString("### ### ###");
                    txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
                    txtTauxRemise.Text = dTauxRemise.ToString("###.##");
                    txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
                }
                cmdEnregistrer.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                      "Prosopis - Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTypeContratResa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbTypeContratResa.SelectedItem!=null)
            {
                leTypeContratEnCours = cmbTypeContratResa.SelectedItem as TypeContrat;
                pParametreResa.Enabled = true;
            }
            else
            {
                pParametreResa.Enabled = false;
            }
        }

        private void cmbTypeContratDepot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTypeContratDepot.SelectedItem != null)
            {

                leTypeContratEnCours = cmbTypeContratDepot.SelectedItem as TypeContrat;
                pParametreDepot.Enabled = true;
            }
            else
            {
                pParametreDepot.Enabled = false;
            }
        }

        private void rbImmeuble_CheckedChanged(object sender, EventArgs e)
        {
            if(rbImmeuble.Checked)
            {
                leTypeConstructiontEnCours = TypeConstruction.Appartement;
                cmbTypeContratDepot.DataSource = contratRep.GetTypeContrats().Where(tc => tc.Actif == true 
                && tc.ProjetId == leProspectEnCours.ProjetId && tc.CategorieContrat == CategorieContrat.Dépôt
                && tc.TypeConstruction==leTypeConstructiontEnCours).ToList();
                cmbTypeContratDepot.DisplayMember = "LibelleTypeContrat";
                cmbTypeContratDepot.SelectedIndex = -1;

                cmbTypeContratResa.DataSource = contratRep.GetTypeContrats().Where(tc => tc.Actif == true
                && tc.ProjetId == leProspectEnCours.ProjetId && tc.CategorieContrat == CategorieContrat.Réservation
                 && tc.TypeConstruction == leTypeConstructiontEnCours).ToList();
                cmbTypeContratResa.DisplayMember = "LibelleTypeContrat";
                cmbTypeContratResa.SelectedIndex = -1;

                cmbTypeContratDepot.Enabled = true;
                cmbTypeContratDepot.SelectedIndex = -1;
                cmbTypeVillaDepot.DataSource = contratRep.GetTypeVillas().Where(tc => tc.ProjetId == leProspectEnCours.ProjetId 
                                                                                     && tc.TypeConstruction==TypeConstruction.Appartement)
                                                                                     .ToList();
                cmbTypeVillaDepot.DisplayMember = "NomComplet";
                cmbTypeVillaDepot.SelectedIndex = -1;
                EffacerChoixTypeVilla();
                lbPosition.Visible = false;
                chkAngleDepot.Visible = false;
                tcTypeContrat.Enabled = true;
            }
        }

        private void rbIlot_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIlot.Checked)
            {
                leTypeConstructiontEnCours = TypeConstruction.Villa;
                cmbTypeContratDepot.Enabled = true;
                cmbTypeContratDepot.SelectedIndex = -1;
                cmbTypeContratDepot.DataSource = contratRep.GetTypeContrats().Where(tc => tc.Actif == true
                && tc.ProjetId == leProspectEnCours.ProjetId && tc.CategorieContrat == CategorieContrat.Dépôt
                && tc.TypeConstruction == leTypeConstructiontEnCours).ToList();
                cmbTypeContratDepot.DisplayMember = "LibelleTypeContrat";
                cmbTypeContratDepot.SelectedIndex = -1;

                cmbTypeContratResa.DataSource = contratRep.GetTypeContrats().Where(tc => tc.Actif == true
               && tc.ProjetId == leProspectEnCours.ProjetId && tc.CategorieContrat == CategorieContrat.Réservation
                && tc.TypeConstruction == leTypeConstructiontEnCours).ToList();
                cmbTypeContratResa.DisplayMember = "LibelleTypeContrat";
                cmbTypeContratResa.SelectedIndex = -1;

                cmbTypeVillaDepot.DataSource = contratRep.GetTypeVillas().Where(tc => tc.ProjetId == leProspectEnCours.ProjetId
                                                                                     && tc.TypeConstruction == TypeConstruction.Villa)
                                                                                     .ToList();
                cmbTypeVillaDepot.DisplayMember = "NomComplet";
                cmbTypeVillaDepot.SelectedIndex = -1;
                
                EffacerChoixTypeVilla();
                lbPosition.Visible = true;
                chkAngleDepot.Visible = true;
                tcTypeContrat.Enabled = true;
            }
        }

        private void cmbProjets_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void rbAppartement_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbVilla_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
