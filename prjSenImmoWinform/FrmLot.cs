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
    public partial class FrmLot : Form
    {
        //F2A  150m2 : 13.490.000 FCFA
        //F3    150m2 : 16.990.000 FCFA
        //F4A 150m2 : 18.990.000 FCFA
        //F4B 200m2 : 24.990.000 FCFA
        //F5A 150m2 : 29.990.000 FCFA
        //F5B 150m2 : 39.990.000 FCFA
        private IlotRepository ilotRepository;
        private Lot lotEnCours;
        private bool bModif;

        private bool bChoixLot;
        private TypeVilla leTypeVillaEnCours;
        private decimal prixRevise;
        private PositionLot position;
        private decimal superficieReelle;
        private Ilot LIlotEnCours;
        private Ilot ilotEnCours;
        private TypeConstruction leTypeConstructionEnCours;

        public FrmLot()
        {
            InitializeComponent();
            ilotRepository=new IlotRepository();

            try
            {
                ChargerTypeVillas();
                ChargerIlots();
                ChargerPositions();
                txtNumeroLot.Text = string.Empty;
                txtNomCommercial.Text = string.Empty;
                txtPrixRevise.Text = string.Empty;
                txtPrixStandard.Text = string.Empty;
                txtSuperficieReelle.Text = string.Empty;
                txtSuperficieStandard.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public FrmLot(bool choixLot):this()
        {
            bChoixLot = true;

        }

        public FrmLot(Ilot ilotEnCours):this()
        {
            // TODO: Complete member initialization
            ChargerTypeVillas();
            ChargerIlots();

            this.ilotEnCours = ilotEnCours;
            cmbIlots.SelectedItem = ilotEnCours;
            cmbIlots.SelectedValue = ilotEnCours.Id;
            cmbIlots_Validated(this, new EventArgs());
          
        }
        public FrmLot(Ilot ilotEnCours, TypeConstruction tc) : this()
        {
            this.ilotEnCours = ilotRepository.FindIlotByName(ilotEnCours.NomIlot, ilotEnCours.TypeConstruction);
            // TODO: Complete member initialization
            ChargerTypeVillas(this.ilotEnCours.Projet,tc);
            ChargerIlots(tc);
            leTypeConstructionEnCours = tc;
            
            cmbIlots.SelectedItem = ilotEnCours;
            cmbIlots.SelectedValue = ilotEnCours.Id;
            cmbIlots_Validated(this, new EventArgs());

            txtProjet.Text = this.ilotEnCours.Projet.DenominationProjet;

            if (tc== TypeConstruction.Appartement)
            {
                pEtage.Visible = true;
                pPosition.Visible = false;
                pEtage.Location = pPosition.Location;
                cmbEtages.DataSource = Enum.GetValues(typeof(NiveauAppartement));
                cmbEtages.SelectedIndex =- 1;
            }
            else
            {
                pEtage.Visible = false;
                pPosition.Visible = true;
                //pEtage.Location = pPosition.Location;
                //cmbEtages.DataSource = Enum.GetValues(typeof(NiveauAppartement));
                //cmbEtages.SelectedIndex = 1;

            }

        }
        public FrmLot(Lot lot) : this()
        {
            // TODO: Complete member initialization
            txtProjet.Text = lot.Ilot.Projet.DenominationProjet;
            lotEnCours = ilotRepository.FindLotById(lot.ID);
            txtNumeroLot.Text = lotEnCours.NumeroLot;
            LIlotEnCours= lotEnCours.Ilot;
            cmbIlots.SelectedItem = lotEnCours.Ilot;
            cmbIlots.SelectedValue = lotEnCours.Ilot.Id;
            cmbTypeVillas.SelectedItem = lotEnCours.TypeVilla;
            cmbTypeVillas.SelectedValue = lotEnCours.TypeVilla.TypeVillaId;
            txtSuperficieReelle.Text = lotEnCours.Superficie.ToString("###");
            txtPrixRevise.Text = lotEnCours.PrixRevise.ToString("### ### ###");
            cmbPositions.SelectedItem=lotEnCours.PositionLot;
            bModif = true;

        }
        private void ChargerIlots()
        {
            cmbIlots.DataSource = ilotRepository.List.ToList();
            cmbIlots.DisplayMember = "NomIlot";
            cmbIlots.ValueMember = "ID";
            cmbIlots.SelectedIndex = -1;
        }
        private void ChargerIlots(TypeConstruction tc)
        {
            cmbIlots.DataSource = ilotRepository.List.ToList().Where(ilot => ilot.TypeConstruction == tc).ToList();
            cmbIlots.DisplayMember = "NomIlot";
            cmbIlots.ValueMember = "ID";
            cmbIlots.SelectedIndex = -1;
        }
        private void ChargerPositions()
        {
            cmbPositions.DataSource = Enum.GetValues(typeof(PositionLot));
            cmbPositions.SelectedIndex = 1;
        }

        private void ChargerTypeVillas()
        {
            cmbTypeVillas.DataSource = ilotRepository.GetTypeVillas().ToList();
            cmbTypeVillas.DisplayMember = "CodeType";
            cmbTypeVillas.ValueMember = "TypeVillaID";
            cmbTypeVillas.SelectedIndex = -1;
        }

        private void ChargerTypeVillas(TypeConstruction tc)
        {
            cmbTypeVillas.DataSource = ilotRepository.GetTypeVillas().Where(tv => tv.TypeConstruction==tc).ToList();
            cmbTypeVillas.DisplayMember = "CodeType";
            cmbTypeVillas.ValueMember = "TypeVillaID";
            cmbTypeVillas.SelectedIndex = -1;
        }
        private void ChargerTypeVillas(Projet prj,TypeConstruction tc)
        {
            cmbTypeVillas.DataSource = ilotRepository.GetTypeVillas().Where(tv =>tv.ProjetId== prj.Id &&  tv.TypeConstruction == tc).ToList();
            cmbTypeVillas.DisplayMember = "CodeType";
            cmbTypeVillas.ValueMember = "TypeVillaID";
            cmbTypeVillas.SelectedIndex = -1;
        }
        private void ChargerTypeVillas(Projet prj, TypeConstruction tc, TypeImmeuble ti)
        {
            cmbTypeVillas.DataSource = ilotRepository.GetTypeVillas().Where(tv => tv.ProjetId == prj.Id && tv.TypeConstruction == tc && tv.TypeImmeuble.Id==ti.Id).ToList();
            cmbTypeVillas.DisplayMember = "CodeType";
            cmbTypeVillas.ValueMember = "TypeVillaID";
            cmbTypeVillas.SelectedIndex = -1;
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            //Controle de saisie
            if(txtNumeroLot.Text==string.Empty)
            {
                MessageBox.Show(this, "Veuillez compléter la saisie",
                        "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var numeroLot = txtNumeroLot.Text;
                var lot = ilotRepository.FindLotByNumero(numeroLot);
                if (!bModif && lot!=null)
                {
                    MessageBox.Show(this, "Désolé ce numéro de lot est déja enregistré",
                            "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if(Tools.Tools.ProjetEnCours.DenominationProjet == "KERRIA")
                {
                    //    superficieReelle
                    //    prixRevise
                    //    position
                        
                    //superficieReelle = decimal.Parse(txtSuperficieReelle.Text);
                    //position = (PositionLot)cmbPositions.SelectedItem;

                }

                if (!bModif)
                {
                    var newLot = new Lot();
                    //{
                    //    NumeroLot = numeroLot,
                    //    Superficie = superficieReelle,
                    //    PositionLot = position,
                    //    PrixRevise = prixRevise,
                    //    StatutLot = StatutLot.Libre,
                    //    TypeVillaID = leTypeVillaEnCours.TypeVillaId,
                    //    TypeVilla = leTypeVillaEnCours,
                    //    IlotID = LIlotEnCours.Id,
                    //    Ilot = LIlotEnCours,
                    //    LotVirtuel = false
                    //};
                    newLot.NumeroLot = numeroLot;
                    newLot.Superficie = superficieReelle;

                    if (leTypeConstructionEnCours == TypeConstruction.Villa)
                        newLot.PositionLot = position;
                    else
                        newLot.NiveauAppartement = (NiveauAppartement)cmbEtages.SelectedItem;

                    newLot.PrixRevise = prixRevise;

                    newLot.TypeVillaID = leTypeVillaEnCours.TypeVillaId;
                    newLot.TypeVilla = leTypeVillaEnCours;
                    newLot.IlotID = LIlotEnCours.Id;
                    newLot.Ilot = LIlotEnCours;
                    newLot.LotVirtuel = false;
                    ilotRepository.AddLot(newLot,leTypeVillaEnCours.ProjetId.Value);
                    ilotRepository.SaveChanges();
                }
                else
                {
                    if (lotEnCours.StatutLot!= StatutLot.Reserve)
                    {

                        lotEnCours.NumeroLot = numeroLot;
                        lotEnCours.Superficie = superficieReelle;
                        lotEnCours.PositionLot = position;
                        lotEnCours.PrixRevise = prixRevise;

                        lotEnCours.TypeVillaID = leTypeVillaEnCours.TypeVillaId;
                        lotEnCours.TypeVilla = leTypeVillaEnCours;
                        lotEnCours.IlotID = LIlotEnCours.Id;
                        lotEnCours.Ilot = LIlotEnCours;
                        lotEnCours.LotVirtuel = false;
                        ilotRepository.SaveChanges(); 
                    }
                    else
                    {
                        MessageBox.Show(this, "Désolé ce lot n'est pas modifiable car déja réservé",
                            "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                      "Prosopis - Gestion des Lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbIlots_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(cmbIlots.SelectedItem!=null)
            //    LIlotEnCours = (Ilot)cmbIlots.SelectedItem;
                //ChargerTypeVillas(Projet prj, TypeConstruction tc, TypeImmeuble ti)
               // ChargerTypeVillas(LIlotEnCours.Projet, leTypeConstructionEnCours, LIlotEnCours.TypeImmeuble);
        }

        private void cmbTypeVillas_SelectedIndexChanged(object sender, EventArgs e)
        {
           try 
	        {
                txtNomCommercial.Text = string.Empty;
                txtPrixStandard.Text = string.Empty;
                txtSuperficieStandard.Text = string.Empty;
                txtSuperficieReelle.Text = string.Empty;
                txtPrixRevise.Text = string.Empty;
                cmbPositions.SelectedIndex = -1;
                cmbEtages.SelectedIndex = -1;
                if (cmbTypeVillas.SelectedItem != null)
                    {
                        leTypeVillaEnCours = (TypeVilla)cmbTypeVillas.SelectedItem;
                        txtNomCommercial.Text = leTypeVillaEnCours.NomType;
                        txtPrixStandard.Text = leTypeVillaEnCours.PrixStandard.ToString("### ### ###");
                        txtSuperficieStandard.Text = leTypeVillaEnCours.SurfaceDeBase.ToString();
                        txtSuperficieReelle.Text = leTypeVillaEnCours.SurfaceDeBase.ToString("###");
                        txtSuperficieReelle.Focus();
                        if(Tools.Tools.ProjetEnCours.DenominationProjet == "KERRIA" && leTypeVillaEnCours.CodeType.ToLower()=="commerce")
                        {
                            pEtage.Visible = false;
                            pPosition.Visible = true;
                        }
                        else 
                        if (Tools.Tools.ProjetEnCours.DenominationProjet == "KERRIA" && leTypeConstructionEnCours== TypeConstruction.Appartement && leTypeVillaEnCours.CodeType.ToLower() != "commerce")
                        {
                            pEtage.Visible = true;
                            pPosition.Visible = false;
                        }
                    //if (Tools.Tools.Projet != "KERRIA")
                    CalculerPrixRevise();
                    }
	        }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                      "Prosopis - Gestion des Lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Tools.Tools.Projet != "KERRIA")
                CalculerPrixRevise();
        }

        private void CalculerPrixRevise()
        {
            decimal tauxMajoration;

            try
            {
                if (Tools.Tools.ProjetEnCours.DenominationProjet == "KERRIA")
                {
                    tauxMajoration = 15;
                }
                else
                {
                    tauxMajoration = 10;
                }

                if (cmbPositions.SelectedItem != null && cmbTypeVillas.SelectedItem != null
                       && txtSuperficieReelle.Text != string.Empty)
                {
                    var prixStandard = leTypeVillaEnCours.PrixStandard;
                    var superficieStandard = leTypeVillaEnCours.SurfaceDeBase;
                    superficieReelle = decimal.Parse(txtSuperficieReelle.Text);
                    position = (PositionLot)cmbPositions.SelectedItem;
                    decimal differencePrix = 0;
                        if (Tools.Tools.ProjetEnCours.DenominationProjet != "KERRIA")
                    {
                        var differenceSurface = superficieReelle - superficieStandard;
                        differencePrix = differenceSurface * 40000;
                    }
                    else
                    {

                    }

                    prixRevise = prixStandard + differencePrix;

                     if (position == PositionLot.Angle)
                        prixRevise += prixRevise * tauxMajoration / 100;

                  
                    txtPrixRevise.Text = prixRevise.ToString("### ### ###");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cmbIlots_Validated(object sender, EventArgs e)
        {
            try
            {
                if (cmbIlots.SelectedItem != null)
                {
                    LIlotEnCours = (Ilot)cmbIlots.SelectedItem;
                    if(Tools.Tools.ProjetEnCours.DenominationProjet != "KERRIA")
                    { 
                        txtNumeroLot.Text = LIlotEnCours.NomIlot;
                    }
                    txtNumeroLot.Focus();
                    txtNumeroLot.SelectionStart = txtNumeroLot.Text.Length;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                                      "Prosopis - Gestion des Lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSuperficieReelle_TextChanged(object sender, EventArgs e)
        {
            //if(Tools.Tools.Projet!="KERRIA")
                CalculerPrixRevise();
        }

        private void FrmLot_Load(object sender, EventArgs e)
        {

        }

        private void cmdNouvelEtatAvancement_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdNouveau_Click(object sender, EventArgs e)
        {

        }

        private void cmdEditer_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPrixRevise_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtNumeroLot_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdSupprimer_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(this, "Voulez vous réellement supprimer le lot " + lotEnCours.NumeroLot +"? ","Prosopis - Suppression lot", MessageBoxButtons.YesNo, MessageBoxIcon.Question )== DialogResult.Yes)
            {
                try
                {
                    if(lotEnCours.StatutLot!= StatutLot.Libre)
                    {
                        MessageBox.Show(this, "Désolé le lot " + lotEnCours.NumeroLot + " n'est pas libre!!!, vous ne pourrait le supprimer.",
                        "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    ilotRepository.DeleteLot(lotEnCours.ID);
                    MessageBox.Show(this, "Le lot "+ lotEnCours.NumeroLot + " a été supprimé avec succes",
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Erreur:" + ex.Message,
                                          "Prosopis - Gestion des Lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }





        //private void RechercherVillas()
        //{
        //    var villaQuery = db.Lots.Where(v => v.ID!=null);
        //    if(cmbIlots.SelectedItem!=null)
        //    {
        //        var idIlot=((Ilot)cmbIlots.SelectedItem).Id;
        //        villaQuery=villaQuery.Where(v => v.IlotID == idIlot);
        //    }
        //    if (cmbTypeVillas.SelectedItem != null)
        //    {
        //        var idTypeVilla = ((TypeVilla)cmbTypeVillas.SelectedItem).TypeVillaId;
        //        villaQuery = villaQuery.Where(v => v.TypeVillaID == idTypeVilla);
        //    }
        //    if (cmbPositions.SelectedItem != null)
        //    {
        //        var position = (PositionLot)cmbPositions.SelectedItem;
        //        villaQuery = villaQuery.Where(v => v.PositionLot == position);
        //    }
        //    if (cmbStatuts.SelectedItem != null)
        //    {
        //        var statut = (StatutLot)cmbStatuts.SelectedItem;
        //        villaQuery = villaQuery.Where(v => v.StatutLot == statut);
        //    }
        //    if(txtNumeroVilla.Text!=string.Empty)
        //        villaQuery = villaQuery.Where(v => v.NumeroLot == txtNumeroVilla.Text);



        //        dgVillas.DataSource = (from vl in villaQuery

        //                            select new
        //                            {
        //                                ID = vl.ID,
        //                                Ilôt = vl.Ilot.NomIlot,
        //                                Numéro = vl.NumeroLot,
        //                                Type = vl.TypeVilla.CodeType,
        //                                Surface = vl.Superficie,
        //                                Position = vl.PositionLot,
        //                                Prix = vl.PrixRevise,
        //                                Statut = vl.StatutLot
        //                            }).ToList();
        //        dgVillas.Columns[0].Width = 0;
        //        dgVillas.Columns[1].Width = 60;
        //        dgVillas.Columns[2].Width = 60;
        //        dgVillas.Columns[3].Width = 80;
        //        dgVillas.Columns[4].Width = 80;
        //        dgVillas.Columns[5].Width = 80;
        //        dgVillas.Columns[6].Width = 80;
        //        dgVillas.Columns[7].Width = 80;
        //        //dgIlots.Columns[1].HeaderText = "Type d'Ilot du véhicule";
        //        dgVillas.Columns[0].Visible = false;

        //}


    }
}
