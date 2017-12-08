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
using System.Globalization;
using prjSenImmoWinform.DAL;
using System.Transactions;

namespace prjSenImmoWinform
{
    public partial class FrmVente : Form
    {
        private decimal dMontantRemise;
        private decimal dPrixDeVente;
        private decimal dMontantCommission;
        private ApporteurAffaire LApporteurAffaireEnCours;
        private decimal dTauxRemise;
        private TypeContrat leTypeContratEnCours;
        private TypeVilla leTypeVillaDepotEnCours;
        private decimal dMontantPremierVersement;
        private TypeEcheancier leTypeEcheancierEnCours;
        private decimal dMontantApresDepot;
        private Commercial leCommercialEnCours;
        private PositionLot laPositionLotEnCours;
        private bool bInitial;
        private bool bAvenant;
        private Contrat leContratDesiste;
        private bool bDejaClient;

        private Client LeProspect;
        private Option LOption;
        private TypeContrat LeTypeContrat;
        private TypeVilla LeTypeVilla;
        private Lot LeLot;
        private decimal LePrixStandard;
        private PositionLot LaPosition;
        private decimal LaSuperfice;
        private decimal LePrixRevise;
        private decimal LeTauxRemise;
        private decimal LeMontantRemise;
        private decimal LePrixDeVente;

        
        private ApporteurAffaire LApporteurAffaire;
        private decimal LeMontantCommission;

       
        private decimal LeMontantPremierVersement;
        private TypeEcheancier LeTypeEcheancier;
        private decimal LeDepotMinimum;


        private Lot leLotEnCours;
        private Client leProspectEnCours;
        private ContratRepository contratRep;
        private IlotRepository IlotRep;
        private CommercialRepository commercialRep;
        private ClientRepository clientRep;
        private Option LOptionEnCours;
        private List<EncaissementProspect> lesEncaissementsProspect;
        private decimal leNouveauTauxEncaissement;
        private decimal LeMontantEcheance;
        private int LeNombreEcheance;
        private decimal LeMontantDerniereEcheance;
        private List<EcheanceSimule> echeancesSimules;

        public FrmVente()
        {
            InitializeComponent();
            this.Size = new Size(460, 606);
            try
            {
                contratRep = new ContratRepository();
                IlotRep = new IlotRepository();
                commercialRep = new CommercialRepository();
                clientRep = new ClientRepository();
                //cmbTypeContrats.DataSource = ContratRep.GetTypeContrats().ToList();
                //cmbTypeContrats.DisplayMember = "LibelleTypeContrat";
                //cmbTypeContrats.SelectedIndex = -1;

                //cmbTypeVillaDepot.DataSource = contratRep.GetTypeVillas().ToList();
                //cmbTypeVillaDepot.DisplayMember = "NomComplet";
                //cmbTypeVillaDepot.ValueMember = "TypeVillaId";

                //cmbTypeVillaDepot.SelectedIndex = -1;

                //cmbTypeContrats.DataSource = Enum.GetValues(typeof(CategorieContrat));
                //cmbTypeContrats.SelectedIndex = -1;


                cmbApporteurAffaires.DataSource = contratRep.GetApporteurAffaires().ToList();
                cmbApporteurAffaires.DisplayMember = "NomComplet";
                cmbApporteurAffaires.SelectedIndex = -1;

                cmbModePaiement.DataSource = Enum.GetValues(typeof(ModePaiement));
                cmbModePaiement.SelectedIndex = -1;


                leLotEnCours = null;
                tcTypeContrat.ItemSize = new Size(0, 1);
                tcTypeContrat.SizeMode = TabSizeMode.Fixed;
                tcEncaissement.ItemSize = new Size(0, 1);
                tcEncaissement.SizeMode = TabSizeMode.Fixed;
                tcChoixLot.ItemSize = new Size(0, 1);
                tcChoixLot.SizeMode = TabSizeMode.Fixed;
                laPositionLotEnCours = PositionLot.Standard;
                txtPrixVente.Text = string.Empty;
                dtpDateContrat.Value = DateTime.Now.Date;
                dtpDateLivraisonDepotPrevue.CustomFormat = " "; //An empty SPACE;
                dtpDateLivraisonDepotPrevue.Format = DateTimePickerFormat.Custom;

                dtpDateLivraisonResaPrevue.CustomFormat = " "; //An empty SPACE;
                dtpDateLivraisonResaPrevue.Format = DateTimePickerFormat.Custom;

                dtpDptLivraisonDepot.CustomFormat = " "; //An empty SPACE;
                dtpDptLivraisonDepot.Format = DateTimePickerFormat.Custom;
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:" + ex.Message,
                          "Prosopis -  Gestion des ventes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public FrmVente(string codeString) : this()
        {
            try
            {
                bInitial = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis -  Gestion des ventes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public FrmVente(Client leProspect, bool bDejaClient) : this()
        {
            try
            {
                tcVente.Visible = false;
                LeProspect = clientRep.GetClient(leProspect.ID);
                bInitial = false;
                this.bDejaClient = bDejaClient;
                cmdChoisirClient.Visible = false;

                if (!bDejaClient)
                {
                    tcEncaissement.SelectedTab = tcEncaissement.TabPages[0];
                    if (LeProspect.Options.Where(opt => opt.Active == true).Count() > 0)
                    {
                       
                        LOption = LeProspect.Options.Where(opt => opt.Active == true).FirstOrDefault();//!!!!!!!! seul la première option est prise en compte
                        if (LOption != null)
                        {
                            //Récupération des paramètres de l'option
                            LeTypeContrat = LOption.TypeContrat;
                            LeTypeVilla = LOption.TypeVilla;
                            LeLot = LOption.Lot;
                            LaSuperfice = LOption.Surface;
                            LePrixStandard = LOption.PrixStandard;
                            LePrixDeVente = LOption.PrixDeVente;
                            LePrixRevise= LOption.PrixRevise;
                            LeTauxRemise = LOption.TauxRemiseAccordee;
                            LeMontantRemise = LOption.MontantRemiseAccordee;
                            LaPosition = LOption.PositionLot;
                            //Affichage 
                            txtTypeContrat.Text = LeTypeContrat.LibelleTypeContrat;
                            if (LOption.DateAtteinteSeuil != null)
                            { 
                                dtpDateContrat.Value = LOption.DateAtteinteSeuil.Value;
                                dtpDateContrat.Enabled = false;
                            }
                            else
                            {
                                dtpDateContrat.Value = DateTime.Now;
                                dtpDateContrat.Enabled = true;
                            }

                            if (LeTypeContrat.CategorieContrat == CategorieContrat.Réservation)
                            {
                                txtNumeroLot.Text = LeLot.NumeroLot;
                                txtTypeVilla.Text = LeLot.TypeVilla.CodeType;
                                txtSuperficieDeBase.Text = LeLot.TypeVilla.SurfaceDeBase.ToString("###");
                                txtSuperficieReelle.Text = LaSuperfice.ToString("###");
                                txtPosition.Text = LaPosition.ToString();
                                txtPrixStandard.Text = LePrixStandard.ToString("### ### ###");
                                txtPrixRevise.Text = LePrixRevise.ToString("### ### ###");
                                tcChoixLot.SelectedTab = tcChoixLot.TabPages[1];

                                tcVente.TabPages.Remove(tabPage9);
                            }
                            else
                            {
                                switch (LeTypeContrat.SeuilSouscription)
                                {
                                    case 5:
                                        rbCinqPourcent.Checked = true;
                                        break;
                                    case 15:
                                        rbQuinzePourcent.Checked = true;
                                        break;
                                    default:
                                        break;
                                }
                                txtLeTypeVilla.Text = LeTypeVilla.NomComplet;
                                txtSuperficieDeBaseDepot.Text = LeTypeVilla.SurfaceDeBase.ToString();
                                txtPRixStandardDepot.Text = LePrixStandard.ToString("### ### ###");
                                chkAngleDepot.Checked = LaPosition == PositionLot.Angle ? true : false;
                                txtPrixReviseDepot.Text = LePrixRevise.ToString("### ### ###");
                                txtPrixVente.Text = LePrixDeVente.ToString("### ### ###");
                                tcVente.TabPages.Remove(tabPage3);
                                tcChoixLot.SelectedTab = tcChoixLot.TabPages[0];

                            }
                            if (LeTauxRemise != 0)
                            {
                                chkRemise.Checked = true;
                                txtTauxRemise.Text = LeTauxRemise.ToString();
                                txtMontantRemise.Text = LeMontantRemise.ToString("### ### ###");
                                txtPrixVente.Text = LePrixDeVente.ToString("### ### ###");
                                
                            }
                        }
                    }
                }
                else
                {
                    tcEncaissement.SelectedTab = tcEncaissement.TabPages[1];
                }
                AfficherProspect(LeProspect);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis -  Gestion des ventes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public FrmVente(Client leProspect, bool bDejaClient,Contrat leContratDesiste) : this(leProspect,  bDejaClient)
        {
            bAvenant = true;
            this.leContratDesiste = leContratDesiste;
            dtpDateContrat.Value = leContratDesiste.DateSouscription.Value.Date;
            dtpDateContrat.Enabled = false;

        }

        private void cmdChoixLot_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    FrmDetailsIlot frmDetIlot = new FrmDetailsIlot("SelectionLot");
            //    //frmDetIlot.MdiParent = this.MdiParent;
            //    frmDetIlot.WindowState = FormWindowState.Normal;
            //    frmDetIlot.StartPosition = FormStartPosition.CenterParent;
            //    frmDetIlot.ShowDialog(this);
            //    leLotEnCours = frmDetIlot.GetLotSelectionne();
            //    if (leLotEnCours != null)
            //    {
            //        Afficherlot(leLotEnCours);

            //        txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
            //        if (txtTauxRemise.Text != null)
            //        {
            //            txtTauxRemise.Focus();
            //            txtMontantRemise.Focus();
            //            txtPrixVente.Focus();
            //        }
            //    }
            //    else
            //        MessageBox.Show(this, "Erreur: lors de l'attribution du contrat",
            //        "Prosopis -  Attribution de lots", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    //leLotEnCours = frmDetIlot.GetLotSelectionne();
            //    //if (leLotEnCours != null)
            //    //{
            //    //    // leContratEnCours.Lot = leLotEnCours;
            //    //    if (contratRep.AttribuerLotDepot(leContratEnCours.Id, leLotEnCours.ID))
            //    //        AfficherLeContrat();

            //    //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, "Erreur:" + ex.Message,
            //            "Prosopis -  Gestion des ventes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        private void AfficherProspect(Client prospect)
        {
            txtPrenom.Text = prospect.Prenom;
            txtNom.Text = prospect.Nom;
            txtDateNaissance.Text = prospect.DateDeNaissance!=null? prospect.DateDeNaissance.Value.Date.ToShortDateString():"01/01/1900";
            txtLieuNaissance.Text = prospect.LieuDeNaissance;
            txtAdresse.Text = prospect.Adresse;

            //if (prospect.Cooperative != null)
            //{
            //    txtTauxRemise.Text = prospect.Cooperative.TauxRemise.ToString();
            //    chkRemise.Text = "Remise Coopérative " + prospect.Cooperative.Denomination;
            //    chkRemise.Checked = true;
            //    pRemise.Enabled = false;
            //    //if (leLotEnCours != null)
            //    //{
            //    //    dPrixDeVente = leLotEnCours.PrixRevise;

            //    //    decimal dTauxRemise = prospect.Cooperative.TauxRemise;
            //    //    contratRep.CalculerRemise(ref dPrixDeVente, ref dTauxRemise, ref dMontantRemise);
            //    //    txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
            //    //    txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
            //    //    pRemise.Enabled = true;
            //    //}

            //    //{
            //    //    txtMontantRemise.Text = string.Empty;
            //    //}
            //}


        }

        private void Afficherlot(Lot lot)
        {
            try
            {
                //cmbTypeContrats.SelectedItem = CategorieContrat.Réservation;
                txtNumeroLot.Text = lot.NumeroLot;
                txtTypeVilla.Text = lot.TypeVilla.CodeType;
                txtSuperficieDeBase.Text = lot.TypeVilla.SurfaceDeBase.ToString();
                txtSuperficieReelle.Text = lot.Superficie.ToString();
                txtPosition.Text = lot.PositionLot.ToString();
                txtPrixStandard.Text = lot.TypeVilla.PrixStandard.ToString("### ### ###");

                txtPrixRevise.Text = lot.PrixRevise.ToString("### ### ###");
               
               
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AfficherOptionDepot(Option option)
        {
            try
            {
                cmbTypeContrats.SelectedItem = CategorieContrat.Dépôt;
                switch (option.TypeContrat.SeuilSouscription)
                {
                    case 5:
                        rbCinqPourcent.Checked = true;
                        break;
                    case 15:
                        rbQuinzePourcent.Checked = true;
                        break;
                    default:
                        break;
                }
                cmbTypeVillaDepot.SelectedValue = option.TypeVilla.TypeVillaId;
                txtSuperficieDeBaseDepot.Text = option.TypeVilla.SurfaceDeBase.ToString();
                txtPRixStandardDepot.Text = option.TypeVilla.PrixStandard.ToString("### ### ###");
                chkAngleDepot.Checked = option.PositionLot == PositionLot.Angle ? true : false;
                txtPrixReviseDepot.Text = option.Lot.PrixRevise.ToString("### ### ###");
                txtPrixVente.Text = option.PrixDeVente.ToString("### ### ###");
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void chkRemise_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    //pRemise.Enabled = chkRemise.Checked;
            //    //if (!chkRemise.Checked)
            //    //{
            //    //    //Afficherlot(leLotEnCours);
            //    //    EffacerChoixLot();
                   
            //    //    txtTauxRemise.Text = string.Empty;
            //    //    txtMontantRemise.Text = string.Empty;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, "Erreur:" + ex.Message,
            //           "Prosopis -  Gestion des ventes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void chkApporteur_CheckedChanged(object sender, EventArgs e)
        {
            pApporteur.Enabled = chkApporteur.Checked;

            cmbApporteurAffaires.SelectedIndex = -1;
            txtTauxCommission.Text = string.Empty;

        }

        private void txtTauxRemise_Validated(object sender, EventArgs e)
        {
            //if (txtTauxRemise.Text != string.Empty)
            //{
            //    try
            //    {

            //        //double dTauxRemise = double.Parse(txtTauxRemise.Text);
            //        ////txtMontantCommission.Text = (dMontantVerse * 5 / 100).ToString();
            //        //dMontantRemise = ((decimal)dTauxRemise * leLotEnCours.PrixRevise / 100);
            //        //txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
            //        //dPrixDeVente = (leLotEnCours.PrixRevise - dMontantRemise);
            //        //txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
            //        //if (cmbApporteurAffaires.SelectedItem!=null)
            //        //{
            //        //    double TauxCommission = Double.Parse(txtTauxCommission.Text);
            //        //    dMontantCommission = dPrixDeVente * (decimal)TauxCommission / 100;
            //        //    txtMontantCommission.Text = dMontantCommission.ToString("### ### ###");

            //        //}

            //        dPrixDeVente = leLotEnCours.PrixRevise;

            //        decimal dTauxRemise = decimal.Parse(txtTauxRemise.Text);
            //        contratRep.CalculerRemise(ref dPrixDeVente, ref dTauxRemise, ref dMontantRemise);
            //        txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
            //        txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
            //        if (cmbApporteurAffaires.SelectedItem != null)
            //        {
            //            double TauxCommission = double.Parse(txtTauxCommission.Text);
            //            dMontantCommission = dPrixDeVente * (decimal)TauxCommission / 100;
            //            txtMontantCommission.Text = dMontantCommission.ToString("### ### ###");
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show(this, "Vérifier le montant saisi",
            //            "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void txtMontantRemise_Validated(object sender, EventArgs e)
        {
            //if (txtMontantRemise.Text != string.Empty)
            //{
            //    try
            //    {

            //        dMontantRemise = decimal.Parse(txtMontantRemise.Text);
            //        txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");

            //        dPrixDeVente = (leLotEnCours.PrixRevise - dMontantRemise);
            //        txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
            //        //20 × 100 ÷ 180 
            //        dTauxRemise = (dMontantRemise * 100 / leLotEnCours.PrixRevise);
            //        txtTauxRemise.Text = dTauxRemise.ToString();
            //        //Recalcul de la commission
            //        if (cmbApporteurAffaires.SelectedItem != null)
            //        {
            //            double TauxCommission = double.Parse(txtTauxCommission.Text);
            //            dMontantCommission = dPrixDeVente * (decimal)TauxCommission / 100;
            //            txtMontantCommission.Text = dMontantCommission.ToString("### ### ###");
            //        }

            //    }
            //    catch (Exception ex)
            //    {


            //        MessageBox.Show(this, "Vérifier le taux saisi",
            //            "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void cmbApporteurAffaires_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbApporteurAffaires.SelectedItem != null)
                {
                    LApporteurAffaireEnCours = (ApporteurAffaire)cmbApporteurAffaires.SelectedItem;
                    txtTauxCommission.Text = LApporteurAffaireEnCours.TauxCommission.ToString();
                    dMontantCommission = dPrixDeVente * (decimal)LApporteurAffaireEnCours.TauxCommission / 100;
                    txtMontantCommission.Text = dMontantCommission.ToString("### ### ###");
                }
                else
                {
                    LApporteurAffaireEnCours = null;
                    txtTauxCommission.Text = string.Empty;
                    dMontantCommission = 0;
                    txtMontantCommission.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbTypeContrats_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            //                    "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }


            //    }
            //    else
            //    {
            //        leTypeContratEnCours = null;
            //        tcChoixLot.Enabled = false;
            //    }
        }

        private void EffacerChoixLot()
        {
            txtNumeroLot.Text = string.Empty;
            txtTypeVilla.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtPrixStandard.Text = string.Empty;
            txtPrixRevise.Text = string.Empty;
            txtSuperficieDeBase.Text = string.Empty;
            txtSuperficieReelle.Text = string.Empty;
        }

        private void rbCinqPourcent_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    //pDepot.Visible = true;
            //    //tcChoixLot.SelectedTab = tcChoixLot.TabPages[0];
            //    if (rbCinqPourcent.Checked)
            //    {
            //        tcChoixLot.Enabled = true;
            //        leTypeContratEnCours = contratRep.GetTypeContrat(CategorieContrat.Dépôt, 5);
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        private void rbQuinzePourcent_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (rbQuinzePourcent.Checked)
            //    {
            //        tcChoixLot.Enabled = true;
            //        leTypeContratEnCours = contratRep.GetTypeContrat(CategorieContrat.Dépôt, 15);
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        private void cmbTypeVilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbTypeVillaDepot.SelectedItem != null)
            //{
            //    EffacerChoixTypeVilla();
            //    try
            //    {
            //        leTypeVillaDepotEnCours = (TypeVilla)cmbTypeVillaDepot.SelectedItem;
            //        txtSuperficieDeBaseDepot.Text = leTypeVillaDepotEnCours.SurfaceDeBase.ToString();
            //        txtPRixStandardDepot.Text = leTypeVillaDepotEnCours.PrixStandard.ToString("### ### ###");
            //       // txtPrixReviseDepot.Text = leTypeVillaDepotEnCours.PrixStandard.ToString("### ### ###");
            //        leLotEnCours = IlotRep.GetLotVirtuel(leTypeVillaDepotEnCours, chkAngleDepot.Checked ? PositionLot.Angle : PositionLot.Standard);

            //        if (leLotEnCours!=null)
            //        {
            //            dPrixDeVente = leLotEnCours.PrixRevise;
            //            txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");

            //            if (txtTauxRemise.Text != null)
            //            {
            //                txtTauxRemise.Focus();
            //                txtMontantRemise.Focus();
            //                txtPrixVente.Focus();
            //            } 
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(this, "Erreur:..." + ex.Message,
            //               "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void EffacerChoixTypeVilla()
        {
            txtSuperficieDeBaseDepot.Text = string.Empty;
            txtPRixStandardDepot.Text = string.Empty;
            txtPrixReviseDepot.Text = string.Empty;
            chkAngleDepot.Checked = false;
        }

        private void chkAngleDepot_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (chkAngleDepot.Checked)
            //    {

            //        //    leTypeVillaDepotEnCours =(TypeVilla) cmbTypeVillaDepot.SelectedItem;
            //        //    txtSuperficieDeBaseDepot.Text = leTypeVillaDepotEnCours.SurfaceDeBase.ToString();
            //        //    txtPRixStandardDepot.Text=leTypeVillaDepotEnCours.PrixStandard.ToString("### ### ###");
            //        leLotEnCours = IlotRep.GetLotVirtuel(leTypeVillaDepotEnCours, PositionLot.Angle);
            //        laPositionLotEnCours = PositionLot.Angle;
            //        txtPrixReviseDepot.Text = leLotEnCours.PrixRevise.ToString("### ### ###");


            //    }
            //    else
            //    {
            //        leLotEnCours = IlotRep.GetLotVirtuel(leTypeVillaDepotEnCours, PositionLot.Standard);
            //        laPositionLotEnCours = PositionLot.Standard;
            //        txtPrixReviseDepot.Text = string.Empty;
            //        txtPrixReviseDepot.Text = leLotEnCours.TypeVilla.PrixStandard.ToString("### ### ###");
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(this, "Erreur:..." + ex.Message,
            //               "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void cmdSuivant1_Click(object sender, EventArgs e)
        {
            try
            {

                tcVente.Visible = true;
                tcVente.SelectedTab = tcVente.TabPages[0];
                pVersement.Enabled = true;
                if (!bDejaClient)
                {

                    AfficherEncaissementsProspect(LeProspect.ID);
                    RecalculerLesConditionsDeVente();
                    tcEncaissement.SelectedTab = tcEncaissement.TabPages[0];
                }
                else
                {
                    CalculerLesConditionsDeVente();
                    tcEncaissement.SelectedTab = tcEncaissement.TabPages[1];
                }

                this.Size = new Size(1053, 606);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.CenterToScreen();
                chkFraisDossier.Checked = true;
                cmdSuivant1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                          "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CalculerLesConditionsDeVente()
        {
            try
            {
                txtNouveauPrixDeVente.Text = dPrixDeVente.ToString("### ### ###");
                if (leTypeContratEnCours != null)
                {
                    txtMontantSeuilContrat.Text = leTypeContratEnCours.CategorieContrat == CategorieContrat.Réservation ?
                               (dPrixDeVente * leTypeContratEnCours.SeuilEntreeEnVigueur / 100).ToString("### ### ###") :
                               (dPrixDeVente * leTypeContratEnCours.SeuilSouscription / 100).ToString("### ### ###")
                               ;


                }
                // txtTauxEncaissementProspect.Text= dPrixDeVente/


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RecalculerLesConditionsDeVente()
        {
            try
            {
               txtNouveauPrixDeVente.Text = LePrixDeVente.ToString("### ### ###");
               if (LOption != null)
                {
                    txtMontantSeuilContrat.Text =(LePrixDeVente * LeTypeContrat.SeuilSouscription / 100).ToString("### ### ###") ;
                    // txtTauxEncaissementProspect.Text= dPrixDeVente/
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfficherEncaissementsProspect(int prospectId)
        {
            try
            {
                lesEncaissementsProspect = contratRep.GetEncaissementProspect(prospectId).Where(encProspect => encProspect.FraisDeDossier == false).ToList();
                LeMontantPremierVersement = lesEncaissementsProspect.Sum(encProspect => encProspect.MontantGlobal);
                txtMontantPremierVersement.Text = LeMontantPremierVersement.ToString("### ### ###");
                dgEncaissementsProspect.DataSource = lesEncaissementsProspect.ToList()
                    .Select(encProspect => new
                    {
                        ID = encProspect.ID,
                        Date = encProspect.DateEncaissement.Value.ToShortDateString(),
                        Numéro = encProspect.NumeroEncaissement,
                        Montant = encProspect.MontantGlobal,
                        Mode = encProspect.ModePaiement,
                        Référence = encProspect.ReferencePaiement,
                        Commentaires = encProspect.Commentaire

                    }).ToList();

                dgEncaissementsProspect.Columns[0].Width = 0;
                dgEncaissementsProspect.Columns[0].Visible = false;
                dgEncaissementsProspect.Columns[1].Width = 77;
                dgEncaissementsProspect.Columns[2].Width = 110;
                dgEncaissementsProspect.Columns[3].Width = 80;
                dgEncaissementsProspect.Columns[3].DefaultCellStyle.Format = "### ### ###";
                dgEncaissementsProspect.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgEncaissementsProspect.Columns[4].Width = 80;
                dgEncaissementsProspect.Columns[5].Width = 160;
                dgEncaissementsProspect.Columns[6].Width = 200;
                dgEncaissementsProspect.Columns[6].Visible = false;
                //dgProspects.Columns[3].HeaderText = "Né(e) le";
                //dgProspects.Columns[4].HeaderText = "à";
                //dgEncaissements.Columns[0].Visible = false;
                var fraisDeDossier = contratRep.GetEncaissementProspect(prospectId).Where(encProspect => encProspect.FraisDeDossier == true).FirstOrDefault().MontantGlobal;
                if (fraisDeDossier != 0)
                    chkFraisDossier.Checked = true;

                //txtNouveauPrixDeVente.Text = LePrixDeVente.ToString("### ### ###");
                if (LOption != null)
                {
                    txtMontantSeuilContrat.Text = (LePrixDeVente * LeTypeContrat.SeuilSouscription / 100).ToString("### ### ###");
                    leNouveauTauxEncaissement = (LeMontantPremierVersement / LePrixDeVente * 100);
                    //leNouveauTauxEncaissement = Math.Round(leNouveauTauxEncaissement, 2);
                    txtTauxEncaissementProspect.Text = Math.Round(leNouveauTauxEncaissement, 2).ToString();
                }
                else
                    if (leLotEnCours != null)
                {
                    txtMontantSeuilContrat.Text = (LePrixDeVente * LeTypeContrat.SeuilSouscription / 100).ToString("### ### ###");
                    leNouveauTauxEncaissement = (LeMontantPremierVersement / LePrixDeVente * 100);
                    txtTauxEncaissementProspect.Text = (LeMontantPremierVersement / LePrixDeVente * 100).ToString("### ### ###");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void cmdSuivant2_Click(object sender, EventArgs e)
        {
            try
            {
                cmdSuivant2.Enabled = false;
                //VERIFIER SI LES CONDITIONS SONT TOUJOURS RESPECTES EN CAS DE CHANGEMENT D'OPTION
                if ( leNouveauTauxEncaissement < LeTypeContrat.SeuilSouscription)
                {
                    MessageBox.Show(this, "Erreur: ces conditions de ventes ne respectent pas le seuil minimum de délivrance d'un contrat",
                         "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtPrixVenteDepot.Text = LePrixDeVente.ToString("### ### ###");
                txtMontantPremierVersementDepot.Text = LeMontantPremierVersement.ToString("### ### ###");
                txtDptMontantEncaisse.Text = LeMontantPremierVersement.ToString("### ### ###");
                decimal dDepotDeGarantie;

                //if (chkAutreModeReglement.Checked && txtAutreValeurDepotMinimum.Text!=string.Empty)
                //{
                //    dDepotDeGarantie = decimal.Parse(txtAutreValeurDepotMinimum.Text);
                //}
                //else
                //{
                dDepotDeGarantie = LePrixDeVente * 5 / 100;
                //}

                txtDepotGarantieDepot.Text = dDepotDeGarantie.ToString("### ### ###");
               // txtDptDepotMinimum.Text = dDepotDeGarantie.ToString("### ### ###");
                dMontantApresDepot = LePrixDeVente - dDepotDeGarantie;
               // txtDptMontantAVentiller.Text = (dMontantPremierVersement - dDepotDeGarantie).ToString("### ### ###");
                txtVersmentApresDepot.Text = (LeMontantPremierVersement - dDepotDeGarantie).ToString("### ### ###");
                tcVente.SelectedTab = tcVente.TabPages[1];
                tcTypeContrat.Enabled = true;

                if (LeTypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                {
                    dtpDateDerniereEcheance.CustomFormat = " "; //An empty SPACE;
                    dtpDateDerniereEcheance.Format = DateTimePickerFormat.Custom;
                    dtpDatePremiereEcheance.CustomFormat = " "; //An empty SPACE;
                    dtpDatePremiereEcheance.Format = DateTimePickerFormat.Custom;
                    //dtpDateFinTravaux.CustomFormat = " "; //An empty SPACE;
                    //dtpDateFinTravaux.Format = DateTimePickerFormat.Custom;
                    rbModeReglement.Checked = true;
                    //txtDateDebutEcheancier.Text = "";
                    //txtDateFinEcheancier.Text=""
                    //cmbTypeEcheanciers.DataSource = Enum.GetValues(typeof(TypeEcheancier));
                    //cmbTypeEcheanciers.SelectedIndex = 0;

                    //txtDateFinEcheancier.Text=""
                    cmbDtpPeriodicite.DataSource = Enum.GetValues(typeof(TypeEcheancier));
                    cmbDtpPeriodicite.SelectedIndex = 0;


                    tcTypeContrat.SelectedTab = tcTypeContrat.TabPages[0];
                    txtMontantDerniereEcheance.Text = string.Empty;
                    txtMontantEcheance.Text = string.Empty;
                    txtNombreEcheances.Text = string.Empty;
                  
                    tcVente.SelectedTab = tabPage9;
                }
                else
                {
                    tcTypeContrat.SelectedTab = tcTypeContrat.TabPages[1];
                    SimulerAppelsDeFont(LePrixDeVente, LeMontantPremierVersement, nudRemiseSurFraisDossier.Value,LeTypeContrat.ID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:Vérifier le montant saisi... " + ex.Message,
                          "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SimulerAppelsDeFont(decimal prixDeVente, decimal montantPremierVersement, decimal tauxRemiseFraisDeDossier, int leTypeContratId)
        {
            List<AppelDeFondSimule> appelsDeFondSimules = new List<AppelDeFondSimule>();

            var appelDeFonds = contratRep.GetNiveauxAvancements()
                .Where(na => na.AppelFonds == true
                && na.TypeContratId==leTypeContratId).OrderBy(na => na.ordre);
            var montantAVentiller = montantPremierVersement;
            //EXTRAIRE LES FRAIS DE DOSSIER DANS LE MONTANT GLOBAL

            if (chkFraisDossier.Checked)
            {
                decimal montantFraisDeDossier = 200000;
                if (chkRemiseFraisDeDossierAccordee.Checked)
                    montantFraisDeDossier -= montantFraisDeDossier * tauxRemiseFraisDeDossier / 100;
                appelsDeFondSimules.Add(new AppelDeFondSimule()
                {
                    Niveau = "Frais de dossier",
                    NiveauDecaissement = "0%",
                    Montant = montantFraisDeDossier,
                    Encaissé = montantFraisDeDossier,
                    Restant = 0

                });
                //montantAVentiller -= montantFraisDeDossier;
            }

            foreach (var adf in appelDeFonds)
            {
                if (montantAVentiller > 0)
                {

                    decimal montantRestantNiveau = 0;
                    var resteAEncaisser = prixDeVente * adf.TauxDecaissement / 100;
                    decimal montantAEncaisser = 0;

                    if (montantAVentiller >= resteAEncaisser)
                    {
                        montantAEncaisser = resteAEncaisser;
                        montantRestantNiveau = 0;
                    }
                    else
                    {
                        montantAEncaisser = montantAVentiller;
                        montantRestantNiveau = resteAEncaisser - montantAVentiller;
                    }

                    //    var montantEncaisseNiveau = 0;


                    //appelsDeFondSimules.Add(new AppelDeFondSimule()
                    //{
                    //    Niveau = adf.Description,
                    //    NiveauDecaissement = (adf.TauxDecaissement / 100).ToString("#.#%"),
                    //    Montant = resteAEncaisser,
                    //    Encaissé = montantAEncaisser,
                    //    Restant = montantRestantNiveau

                    //});
                   


                    var af = new AppelDeFondSimule()
                    {
                        Niveau = adf.Description,

                        Montant = resteAEncaisser,
                        Encaissé = montantAEncaisser,
                        Restant = montantRestantNiveau

                    };

                    decimal taux;
                    TypeContrat tc;
                    //if (LeProspect.ProjetId==1)
                    //{
                    //    if (adf.ordre == 2)
                    //    {
                    //        //tc = contratRep.GetTypeContrat contrat.TypeContratID);
                    //        taux = LeTypeContrat.SeuilSouscription - 5;
                    //    }
                    //    else
                    //               if (adf.ordre == 26)
                    //    {
                    //        //tc = DB.TypeContrats.Find(contrat.TypeContratID);
                    //        taux = (70 - LeTypeContrat.SeuilSouscription);

                    //    }
                    //    else
                    //    {
                    //        taux = adf.TauxDecaissement;
                    //    } 
                    //}
                    //else
                    //{
                        taux = adf.TauxDecaissement;
                    //}

                    af.NiveauDecaissement = taux.ToString("###") + "%";

                    appelsDeFondSimules.Add(af);
                    montantAVentiller -= montantAEncaisser;
                }
                else
                {
                    var resteAEncaisser = prixDeVente * adf.TauxDecaissement / 100;
                    var af=new AppelDeFondSimule()
                    {
                        Niveau = adf.Description,
                      
                        Montant = resteAEncaisser,
                        Encaissé = 0,
                        Restant = resteAEncaisser

                    };

                    decimal taux;
                    //TypeContrat tc;
                    //if (LeProspect.ProjetId == 1)
                    //{
                    //    if (adf.ordre == 2)
                    //    {
                    //        //tc = contratRep.GetTypeContrat contrat.TypeContratID);
                    //        taux = LeTypeContrat.SeuilSouscription - 5;
                    //    }
                    //    else
                    //                if (adf.ordre == 26)
                    //    {
                    //        //tc = DB.TypeContrats.Find(contrat.TypeContratID);
                    //        taux = (70 - LeTypeContrat.SeuilSouscription);

                    //    }
                    //    else
                    //    {
                    //        taux = adf.TauxDecaissement;
                    //    } 
                    //}
                    //else
                    //{
                        taux = adf.TauxDecaissement;
                    //}
                    af.NiveauDecaissement = taux.ToString("###") + "%";

                    appelsDeFondSimules.Add(af);

                }

            }

            dgAppelsDeFonds.DataSource = appelsDeFondSimules.ToList();
            dgAppelsDeFonds.Columns[0].Width = 135;
            dgAppelsDeFonds.Columns[1].HeaderText = "Taux";
            dgAppelsDeFonds.Columns[1].Width = 50;
            dgAppelsDeFonds.Columns[1].DefaultCellStyle.Format = "###";
            dgAppelsDeFonds.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgAppelsDeFonds.Columns[2].DefaultCellStyle.Format = "### ### ###";
            dgAppelsDeFonds.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgAppelsDeFonds.Columns[2].Width = 80;
            dgAppelsDeFonds.Columns[3].DefaultCellStyle.Format = "### ### ###";
            dgAppelsDeFonds.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgAppelsDeFonds.Columns[3].Width = 80;
            dgAppelsDeFonds.Columns[4].DefaultCellStyle.Format = "### ### ###";
            dgAppelsDeFonds.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgAppelsDeFonds.Columns[4].Width = 80;


        }

        private void AfficherEcheances(int NbEcheances)
        {
            try
            {

                var MontantEcheancePrevu = (int)dMontantApresDepot / NbEcheances;
                txtMontantEcheance.Text = MontantEcheancePrevu.ToString("### ### ###");
                var montantDerniereTraite = (int)(dMontantApresDepot - MontantEcheancePrevu * NbEcheances);
                if (montantDerniereTraite > 0)
                    NbEcheances++;
                txtNombreEcheances.Text = (NbEcheances - 1).ToString();
                txtMontantDerniereEcheance.Text = montantDerniereTraite.ToString("### ### ###");
                LeDepotMinimum = dPrixDeVente * leTypeContratEnCours.SeuilSouscription / 100;
                LeMontantEcheance = MontantEcheancePrevu;
                LeNombreEcheance = NbEcheances - 1;
                LeMontantDerniereEcheance = montantDerniereTraite;
            }
            catch (Exception)
            {

                throw;
            }


        }



        private void AfficherEcheancesSouhaitees(decimal montantEcheanceSouhaite, decimal montantEcheancePrevu, int nbEcheances)
        {
            try
            {

                if (montantEcheanceSouhaite > montantEcheancePrevu)
                {
                    //model.DateFinPrevue = derniereEcheance;
                    //var MontantEcheancePrevu = decimal.Parse(txtMontantEcheanceSouhaite.Text);
                    //txtMontantEcheance.Text = montantEcheancePrevu.ToString("### ### ###");
                    var nombreEcheances = Math.Ceiling(dMontantApresDepot / montantEcheanceSouhaite);
                    txtNombreEcheancesSouhaitees.Text = (nombreEcheances).ToString();
                    int nbMois = 0;
                    switch (leTypeEcheancierEnCours)
                    {
                        case TypeEcheancier.Mensuel:
                            nbMois = (int)nombreEcheances;
                            break;
                        case TypeEcheancier.Trimestriel:
                            nbMois = (int)nombreEcheances * 3;
                            break;
                        case TypeEcheancier.Semestriel:
                            nbMois = (int)nombreEcheances * 6;
                            break;
                        case TypeEcheancier.Annuel:
                            nbMois = (int)nombreEcheances * 12;
                            break;
                        default:
                            break;
                    }

                    dtpDateDerniereEcheance.Value = dtpDatePremiereEcheance.Value.AddMonths(nbMois);
                    txtMontantDerniereEcheanceSouhaitee.Text = (montantEcheanceSouhaite + (dMontantApresDepot - (montantEcheanceSouhaite * nombreEcheances))).ToString("### ### ###");




                    LeDepotMinimum = dPrixDeVente * leTypeContratEnCours.SeuilSouscription / 100;
                    LeMontantEcheance = montantEcheanceSouhaite;
                    LeNombreEcheance = (int)nombreEcheances;
                    LeMontantDerniereEcheance = (montantEcheanceSouhaite + (dMontantApresDepot - (montantEcheanceSouhaite * nombreEcheances)));
                }
                else
                {
                    var MontantEcheancePrevu = decimal.Parse(txtMontantEcheanceSouhaite.Text);
                    //txtMontantEcheance.Text = MontantEcheancePrevu.ToString("### ### ###");
                    var montantDerniereTraite = (int)(dMontantApresDepot - MontantEcheancePrevu * nbEcheances);
                    if (montantDerniereTraite > 0)
                        nbEcheances++;

                    txtNombreEcheancesSouhaitees.Text = (nbEcheances - 1).ToString();
                    txtMontantDerniereEcheanceSouhaitee.Text = montantDerniereTraite.ToString("### ### ###");

                    LeDepotMinimum = dPrixDeVente * leTypeContratEnCours.SeuilSouscription / 100;
                    LeMontantEcheance = MontantEcheancePrevu;
                    LeNombreEcheance = (int)nbEcheances - 1;
                    LeMontantDerniereEcheance = montantDerniereTraite;


                }
            }
            catch (Exception)
            {

                throw;
            }


        }



        private void AfficherEcheancesDepotMinimumSouhaite(int nbEcheances)
        {
            try
            {
                txtAutreDepotMinimum.Text = dMontantPremierVersement.ToString("### ### ###");
                //txtEcheanceAutreDepotMinimum.Text= dMontantPremierVersement/
                dMontantApresDepot = dPrixDeVente - dMontantPremierVersement;
                int nbEch = 0;
                ContratRepository.CalculNombreEcheances(leTypeEcheancierEnCours, dtpDatePremiereEcheance.Value.Date, dtpDateDerniereEcheance.Value.Date, ref nbEch);
                var MontantEcheancePrevu = (int)dMontantApresDepot / nbEcheances;

                txtEcheanceAutreDepotMinimum.Text = MontantEcheancePrevu.ToString("### ### ###");
                var montantDerniereTraite = (int)(dMontantApresDepot - MontantEcheancePrevu * nbEcheances);
                if (montantDerniereTraite > 0)
                    nbEcheances++;
                txtNombresEcheancesAutreDepotMinimum.Text = (nbEcheances - 1).ToString();
                txtDerniereEcheancesAutreDepotMinimum.Text = montantDerniereTraite.ToString("### ### ###");
                LeDepotMinimum = dMontantPremierVersement;
                LeMontantEcheance = MontantEcheancePrevu;
                LeNombreEcheance = nbEcheances - 1;
                LeMontantDerniereEcheance = montantDerniereTraite;
                // txtMontantDerniereEcheance.Text = montantDerniereTraite.ToString("### ### ###");


                //if (montantEcheanceSouhaite > montantEcheancePrevu)
                //{
                //    //model.DateFinPrevue = derniereEcheance;
                //    //var MontantEcheancePrevu = decimal.Parse(txtMontantEcheanceSouhaite.Text);
                //    //txtMontantEcheance.Text = montantEcheancePrevu.ToString("### ### ###");
                //    var nombreEcheances = Math.Ceiling(dMontantApresDepot / montantEcheanceSouhaite);
                //    txtNombreEcheancesSouhaitees.Text = (nombreEcheances - 1).ToString();
                //    int nbMois = 0;
                //    switch (leTypeEcheancierEnCours)
                //    {
                //        case TypeEcheancier.Mensuel:
                //            nbMois = (int)nombreEcheances;
                //            break;
                //        case TypeEcheancier.Trimestriel:
                //            nbMois = (int)nombreEcheances * 3;
                //            break;
                //        case TypeEcheancier.Semestriel:
                //            nbMois = (int)nombreEcheances * 6;
                //            break;
                //        case TypeEcheancier.Annuel:
                //            nbMois = (int)nombreEcheances * 12;
                //            break;
                //        default:
                //            break;
                //    }

                //    dtpDateDerniereEcheance.Value = dtpDatePremiereEcheance.Value.AddMonths(nbMois);
                //    txtMontantDerniereEcheanceSouhaitee.Text = (montantEcheanceSouhaite + (dMontantApresDepot - (montantEcheanceSouhaite * nombreEcheances))).ToString("### ### ###");
                //    //var montantDerniereTraite = (int)(dMontantApresDepot - montantEcheancePrevu * NbEcheances);
                //    //if (montantDerniereTraite > 0)
                //    //    NbEcheances++;
                //    //txtNombreEcheances.Text = NbEcheances.ToString();
                //    //txtMontantDerniereEcheance.Text = montantDerniereTraite.ToString("### ### ###");
                //}
                //else
                //{
                //    var MontantEcheancePrevu = decimal.Parse(txtMontantEcheanceSouhaite.Text);
                //    //txtMontantEcheance.Text = MontantEcheancePrevu.ToString("### ### ###");
                //    var montantDerniereTraite = (int)(dMontantApresDepot - MontantEcheancePrevu * nbEcheances);
                //    if (montantDerniereTraite > 0)
                //        nbEcheances++;

                //    txtNombreEcheancesSouhaitees.Text = (nbEcheances - 1).ToString();
                //    txtMontantDerniereEcheanceSouhaitee.Text = montantDerniereTraite.ToString("### ### ###");



                //}


            }
            catch (Exception)
            {

                throw;
            }


        }



        private void AfficherDepotMinimumEcheancesSouhaitees(decimal montantEcheanceSouhaite, decimal montantEcheancePrevu, int nbEcheances)
        {
            try
            {

                if (montantEcheanceSouhaite > montantEcheancePrevu)
                {
                    //model.DateFinPrevue = derniereEcheance;
                    //var MontantEcheancePrevu = decimal.Parse(txtMontantEcheanceSouhaite.Text);
                    //txtMontantEcheance.Text = montantEcheancePrevu.ToString("### ### ###");
                    var nombreEcheances = Math.Ceiling((dPrixDeVente - dMontantPremierVersement) / montantEcheanceSouhaite);
                    txtNombreEcheancesSouhaitees.Text = (nombreEcheances - 1).ToString();
                    int nbMois = 0;
                    switch (leTypeEcheancierEnCours)
                    {
                        case TypeEcheancier.Mensuel:
                            nbMois = (int)nombreEcheances;
                            break;
                        case TypeEcheancier.Trimestriel:
                            nbMois = (int)nombreEcheances * 3;
                            break;
                        case TypeEcheancier.Semestriel:
                            nbMois = (int)nombreEcheances * 6;
                            break;
                        case TypeEcheancier.Annuel:
                            nbMois = (int)nombreEcheances * 12;
                            break;
                        default:
                            break;
                    }

                    dtpDateDerniereEcheance.Value = dtpDatePremiereEcheance.Value.AddMonths(nbMois);
                    dtpDateLivraisonDepotPrevue.Value = dtpDatePremiereEcheance.Value.AddMonths(nbMois);
                    nudDureeDepot.Value = (dtpDateDerniereEcheance.Value - dtpDatePremiereEcheance.Value).Days / 365.25m;
                    txtMontantDerniereEcheanceSouhaitee.Text = (montantEcheanceSouhaite + (dMontantApresDepot - (montantEcheanceSouhaite * nombreEcheances))).ToString("### ### ###");
                    txtNombreEcheancesSouhaitees.Text = nombreEcheances.ToString();
                    LeDepotMinimum = dMontantPremierVersement;
                    LeMontantEcheance = montantEcheanceSouhaite;
                    LeNombreEcheance = (int)nombreEcheances;
                    LeMontantDerniereEcheance = (montantEcheanceSouhaite + (dMontantApresDepot - (montantEcheanceSouhaite * nombreEcheances)));

                }
                else
                {
                    var MontantEcheancePrevu = decimal.Parse(txtMontantEcheanceSouhaite.Text);
                    //txtMontantEcheance.Text = MontantEcheancePrevu.ToString("### ### ###");
                    var montantDerniereTraite = (int)(dMontantApresDepot - MontantEcheancePrevu * nbEcheances);
                    if (montantDerniereTraite > 0)
                        nbEcheances++;

                    txtNombreEcheancesSouhaitees.Text = (nbEcheances - 1).ToString();
                    txtMontantDerniereEcheanceSouhaitee.Text = montantDerniereTraite.ToString("### ### ###");



                }
            }
            catch (Exception)
            {

                throw;
            }


        }



        private void cmdSimuler_Click(object sender, EventArgs e)
        {
            //Réinitialiser

            try
            {
                if (dtpDatePremiereEcheance.Text.Trim() == string.Empty)
                {
                    MessageBox.Show(this, "Veuillez d'abord déterminer la date de première et dernière échéance",
                       "Prosopis - Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int nbEch = 0;
                ContratRepository.CalculNombreEcheances(leTypeEcheancierEnCours, dtpDatePremiereEcheance.Value.Date, dtpDateDerniereEcheance.Value.Date, ref nbEch);
                dtpDateDerniereEcheance.Value = dtpDatePremiereEcheance.Value.AddMonths(nbEch).Date;
                if (nbEch <= 0)
                {
                    MessageBox.Show(this, "Veuillez vérifier les dates de première et dernière échéance, le nombre d'échéances calculées est inférieur ou égal à 0",
                        "Prosopis - Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (rbModeReglement.Checked)
                {

                    AfficherEcheances(nbEch);
                    //pVentillationPremierVersementDepot.Visible = true;
                }
                else
                {
                    if (!chkAutreDepotMinimum.Checked && !chkAutreEcheance.Checked)
                    {
                        MessageBox.Show(this, "Veuillez préciser le nouveau mode de règlement!!!!",
                            "Prosopis - Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (chkAutreDepotMinimum.Checked && !chkAutreEcheance.Checked)
                    {
                        dMontantApresDepot = dPrixDeVente - dMontantPremierVersement;
                        AfficherEcheancesDepotMinimumSouhaite(nbEch);
                    }
                    else
                        if (!chkAutreDepotMinimum.Checked && chkAutreEcheance.Checked)
                    {
                        var montantEcheancePrevu = (int)dMontantApresDepot / nbEch;
                        var montantEcheanceSouhaite = decimal.Parse(txtMontantEcheanceSouhaite.Text);
                        AfficherEcheancesSouhaitees(montantEcheanceSouhaite, montantEcheancePrevu, nbEch);
                        //int nbEcheancesAutresMode;
                        //var MontantEcheancePrevu = (int)dMontantApresDepot / nbEcheances;
                        //txtEcheanceAutreDepotMinimum.Text = MontantEcheancePrevu.ToString("### ### ###");
                        //var montantDerniereTraite = (int)(dMontantApresDepot - MontantEcheancePrevu * nbEcheances);
                        //if (montantDerniereTraite > 0)
                        //    nbEcheances++;
                        //txtNombresEcheancesAutreDepotMinimum.Text = (nbEcheances - 1).ToString();
                    }
                    else if (chkAutreDepotMinimum.Checked && chkAutreEcheance.Checked)
                    {
                        var montantEcheancePrevu = (int)dMontantApresDepot / nbEch;
                        var montantEcheanceSouhaite = decimal.Parse(txtMontantEcheanceSouhaite.Text);
                        dMontantApresDepot = dPrixDeVente - dMontantPremierVersement;
                        AfficherDepotMinimumEcheancesSouhaitees(montantEcheanceSouhaite, montantEcheancePrevu, nbEch);
                    }


                    //if(!chkAutreDepotMinimum.Checked && !chkAutreEcheance.Checked)
                    //{
                    //     MessageBox.Show(this, "Veuillez préciser le nouveau mode de règlement!!!!",
                    //         "Prosopis - Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return; 
                    //}
                    //if (chkAutreEcheance.Checked)
                    //{
                    //    var montantEcheancePrevu = (int)dMontantApresDepot / nbEch;
                    //    var montantEcheanceSouhaite = decimal.Parse(txtMontantEcheanceSouhaite.Text);
                    //    AfficherEcheancesSouhaitees(montantEcheanceSouhaite, montantEcheancePrevu, nbEch);
                    //}
                    //else
                    //if (chkAutreDepotMinimum.Checked)
                    //{
                    //    dMontantApresDepot = dPrixDeVente - dMontantPremierVersement;
                    //    AfficherEcheancesDepotMinimumSouhaite(nbEch);
                    //    if (!chkAutreEcheance.Checked)
                    //    {
                    //        //    int nbEcheancesAutresMode;
                    //        //    var MontantEcheancePrevu = (int)dMontantApresDepot / nbEcheances;
                    //        //    txtEcheanceAutreDepotMinimum.Text = MontantEcheancePrevu.ToString("### ### ###");
                    //        //    var montantDerniereTraite = (int)(dMontantApresDepot - MontantEcheancePrevu * nbEcheances);
                    //        //    if (montantDerniereTraite > 0)
                    //        //        nbEcheances++;
                    //        //    txtNombresEcheancesAutreDepotMinimum.Text = (nbEcheances - 1).ToString();
                    //    }
                    //}
                }







                /////
                //var montantEcheancePrevu = (int)dMontantApresDepot / nbEch;
                //var montanEcheanceSouhaite = decimal.Parse(txtMontantEcheanceSouhaite.Text);
                //if (montantEcheancePrevu < montanEcheanceSouhaite)


                //else
                //    AfficherEcheancesSouhaitees(nbEch);

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:... " + ex.Message,
                         "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTypeEcheanciers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTypeEcheanciers.SelectedItem != null)
            {
                leTypeEcheancierEnCours = (TypeEcheancier)cmbTypeEcheanciers.SelectedItem;
            }
        }

        private void cmdEnregistrerVente_Click(object sender, EventArgs e)
        {
            try
            {
                cmdEnregistrerVente.Enabled = false;

                //var referencePaiement = txtReferencePaiement.Text;
                //var commentairesVersement = txtCommentairePaiement.Text;
                //var modePaiement = (ModePaiement)cmbModePaiement.SelectedItem;
                var dateLivraisonPrevue = dtpDateLivraisonDepotPrevue.Value.Date;
                var dateLivraisonResaPrevue = dtpDateLivraisonResaPrevue.Value.Date;
                var dTauxRemiseFraisDeDossier = nudRemiseSurFraisDossier.Value;
                var tauxDeRemiseFraisDeDossier = nudRemiseSurFraisDossier.Value;
                var commercial = Tools.Tools.AgentEnCours;
                var dateContrat = dtpDateContrat.Value.Date;
                //Vérifier l'existence d'apporteur d'affaire sur le contrat
                var idAAffaire = 0;
                if (LeProspect == null)
                {
                    MessageBox.Show(this, "Veuillez d'abord choisir l'acquéreur",
                          "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


               // dtpDateLivraisonResaPrevue.CustomFormat = "dd/MM/yyyy";
                if (LeLot.Ilot.DateDebutLivraison != null && LeLot.Ilot.DateFinLivraison != null)
                {
                    if (dtpDateLivraisonResaPrevue.Value.Date < LeLot.Ilot.DateDebutLivraison || dtpDateLivraisonResaPrevue.Value.Date > LeLot.Ilot.DateFinLivraison)
                    {
                        MessageBox.Show(this, "Cette date n'est pas comprise dans l'intervalle de livraison de l'ilôt qui va du "
                            + LeLot.Ilot.DateDebutLivraison.Value.ToShortDateString() + " au " + LeLot.Ilot.DateFinLivraison.Value.ToShortDateString()
                            , "Prosopis - Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmdEnregistrerVente.Enabled = false;
                        return;
                    }
                    else
                    {
                        cmdEnregistrerVente.Enabled = true;
                    }
                }
                else
                {
                    cmdEnregistrerVente.Enabled = true;
                }


                var encaissementFraisDeDossier = contratRep.GetFraisDeDossierNonDeverse(LeProspect.ID);
                if (encaissementFraisDeDossier == null)
                    throw new Exception("Frais de dossier introuvable");

                if (cmbApporteurAffaires.SelectedItem != null)
                {
                    idAAffaire = LApporteurAffaireEnCours.Id;
                }

                //Enregistrer le contrat
                var lePremierEnCaissement = !bDejaClient ? lesEncaissementsProspect.Where(enc => enc.FraisDeDossier == false).OrderBy(enc => enc.DateEncaissement).FirstOrDefault()
                                                        : new EncaissementProspect
                                                        {
                                                            NumeroEncaissement = "",//"ENPR" + prospect.ID.ToString().PadLeft(4, '0') + dateVersement.Month.ToString().PadLeft(2, '0') + dateVersement.Year.ToString().Substring(2, 2),
                                                            DateEncaissement = dtpDateContrat.Value.Date,
                                                            MontantGlobal = dMontantPremierVersement,
                                                            ProspectId = leProspectEnCours.ID,
                                                            ModePaiement = (ModePaiement)cmbModePaiement.SelectedItem,
                                                            ReferencePaiement = txtReferencePaiement.Text,
                                                            Commentaire = txtCommentairePaiement.Text
                                                        };

                if (LeTypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                {
                    if (dtpDatePremiereEcheance.Text.Trim() == string.Empty || dtpDateDerniereEcheance.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show(this, "Veuillez d'abord déterminer la date de première et dernière échéance",
                           "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int nbEch = 0;
                    if (ContratRepository.CalculNombreEcheances(leTypeEcheancierEnCours, dtpDatePremiereEcheance.Value.Date, dtpDateDerniereEcheance.Value.Date, ref nbEch) <= 0)
                    {
                        MessageBox.Show(this, "Veuillez vérifier les dates de première et dernière échéance, le nombre d'échéances calculées est inférieur ou égal à 0",
                          "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //int contratId=contratRep.AjouterContratDepotBis(leProspectEnCours.ID, commercial.Id, idAAffaire
                    //          , leTypeVillaDepotEnCours, laPositionLotEnCours, dPrixDeVente, dTauxRemise, dMontantRemise, leTypeContratEnCours,
                    //          leTypeEcheancierEnCours, dtpDatePremiereEcheance.Value.Date, dtpDateDerniereEcheance.Value.Date,
                    //          dtpDateContrat.Value.Date,  dateLivraisonPrevue);

                    int contratId = contratRep.AjouterContratDepotTer(leProspectEnCours.ID, leProspectEnCours.CommercialID.Value, idAAffaire
                              , leTypeVillaDepotEnCours, laPositionLotEnCours, dPrixDeVente, dTauxRemise, dMontantRemise, leTypeContratEnCours,
                              leTypeEcheancierEnCours, dtpDatePremiereEcheance.Value.Date, dtpDateDerniereEcheance.Value.Date,
                              dtpDateContrat.Value.Date, dateLivraisonPrevue, LeDepotMinimum, LeMontantEcheance, LeNombreEcheance, LeMontantDerniereEcheance,0);


                    if (contratId != 0)
                    {
                        contratRep.TransfererFraisDeDossier(contratId);
                        //if (!bDejaClient)
                        //{
                        //    foreach (EncaissementProspect autreEncaissementProspect in lesEncaissementsProspect.Where(enc => enc.ID != lePremierEnCaissement.ID && enc.FraisDeDossier==false))
                        //    {
                        //        contratRep.EnregistrerVersement(leLotEnCours.ID, leProspectEnCours.ID, autreEncaissementProspect.DateEncaissement.Value,
                        //            autreEncaissementProspect.MontantGlobal, contratId, autreEncaissementProspect.ModePaiement, autreEncaissementProspect.ReferencePaiement, autreEncaissementProspect.Commentaire);
                        //    }
                        //}
                    }
                    //ENCAISSEMENT FRAIS DE DOSSIER
                }
                else
                {
                    ////////////////// A INCLURE DANS UNE SEULE TRANSACTION
                    //int contratId = contratRep.AjouterContratReservation(leProspectEnCours.ID, commercial.Id, idAAffaire
                    //          , leLotEnCours.ID, leLotEnCours.PrixRevise, dPrixDeVente, dTauxRemise, dMontantRemise, leTypeContratEnCours,
                    //          lePremierEnCaissement.MontantGlobal, lePremierEnCaissement.DateEncaissement.Value,
                    //          lePremierEnCaissement.Commentaire, lePremierEnCaissement.ReferencePaiement, lePremierEnCaissement.ModePaiement, dateLivraisonPrevue,nudRemiseSurFraisDossier.Value);

                    if(dtpDateLivraisonResaPrevue.Value.Date==DateTime.Now.Date)
                    {

                        MessageBox.Show(this, "Veuillez d'abord définir la date de livraison prévue",
                                       "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmdEnregistrerVente.Enabled = true;
                        return;
                    }
                   
                    int contratId = contratRep.AjouterContratReservationBis(LeProspect.ProjetId.Value, LeProspect.ID, LeProspect.CommercialID.Value, idAAffaire, LeLot.ID,
                                                                              LePrixRevise, LePrixDeVente, LeTauxRemise, LeMontantRemise, LeTypeContrat,
                                                                             dateContrat, dateLivraisonResaPrevue, tauxDeRemiseFraisDeDossier,bAvenant, leContratDesiste!=null?leContratDesiste.Id:0);
                    if (contratId != 0)
                    {
                        //TRANSFERER L'ENCAISSEMENT DU FRAIS DE DOSSIER DANS LE COMPTE DU CONTRAT
                        contratRep.TransfererFraisDeDossier(contratId);
                        //if (!bDejaClient)
                        //{
                        //    //foreach (EncaissementProspect autreEncaissementProspect in lesEncaissementsProspect.Where(enc => enc.ID!=lePremierEnCaissement.ID && enc.FraisDeDossier == false))
                        //    //{
                        //    //    contratRep.EnregistrerVersement(leLotEnCours.ID, leProspectEnCours.ID, autreEncaissementProspect.DateEncaissement.Value,
                        //    //        autreEncaissementProspect.MontantGlobal, contratId, autreEncaissementProspect.ModePaiement, autreEncaissementProspect.ReferencePaiement, autreEncaissementProspect.Commentaire);
                        //    //}
                        //}
                    }
                    //////////////////

                }
                //Cloturer l'option.
                var LOption = LeProspect.Options.Where(opt => opt.Active == true).FirstOrDefault();
                if (LOption != null)
                {
                    contratRep.CloturerOption(LOption.Id, LeLot.ID);
                }

                MessageBox.Show(this, "Le contrat a été enregistré avec succes",
                               "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Information);


                if (bInitial)
                {
                    FrmDossierClient childForm = new FrmDossierClient(leProspectEnCours);
                    childForm.MdiParent = this.MdiParent;
                    childForm.Show();
                    childForm.WindowState = FormWindowState.Maximized;
                }
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:... " + ex.Message,
                          "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //FrmListeClients childForm = new FrmListeClients();
            //childForm.MdiParent = this.MdiParent;
            //childForm.Show();

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdChoisirClient_Click(object sender, EventArgs e)
        {



            try
            {
                FrmListeClientsEtProspect frmListProspectClients = new FrmListeClientsEtProspect();
                //frmDetIlot.MdiParent = this.MdiParent;
                frmListProspectClients.WindowState = FormWindowState.Normal;
                frmListProspectClients.StartPosition = FormStartPosition.CenterParent;
                frmListProspectClients.ShowDialog(this);

                leProspectEnCours = frmListProspectClients.GetClientSelectionne();
                if (leProspectEnCours != null)
                {
                    AfficherProspect(leProspectEnCours);
                }
                else
                    MessageBox.Show(this, "Erreur lors de la séléction du Client/Prospect",
                    "Prosopis -  Choix de client ou prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis -  Gestion des ventes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtMontantEcheance_TextChanged(object sender, EventArgs e)
        {

        }

        //private void chkAutreModeReglement_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkAutreModeReglement.Checked)
        //    {
        //        pAutreModeReglement.Visible = true;
        //        cmdSimuler.Text = "Recalculer";
        //    }
        //    else
        //    {
        //        pAutreModeReglement.Visible = false;
        //        txtMontantEcheanceSouhaite.Text = string.Empty;
        //        cmdSimuler.Text = "Calculer";
        //    }
        //}

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dtpDatePremiereEcheance_ValueChanged(object sender, EventArgs e)
        {
            dtpDatePremiereEcheance.CustomFormat = "dd/MM/yyyy";

        }

        private void dtpDateDerniereEcheance_ValueChanged(object sender, EventArgs e)
        {
            dtpDateDerniereEcheance.CustomFormat = "dd/MM/yyyy";
        }

        private void tcVente_Validating(object sender, CancelEventArgs e)
        {

        }

        private void tcVente_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if()
        }

        private void tcVente_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nudDureeDepot_ValueChanged(object sender, EventArgs e)
        {
            if (rbAutreModeReglement.Checked && chkAutreEcheance.Checked && !chkAutreDepotMinimum.Checked)
            {
                return;
            }
            var dateFinEcheance = dtpDatePremiereEcheance.Value.AddMonths((int)nudDureeDepot.Value);
            dtpDateDerniereEcheance.Value = dateFinEcheance;
            dtpDateLivraisonDepotPrevue.Value = dateFinEcheance;

            cmdSimuler_Click(sender, e);
        }

        private void pVentillationPremierVersementDepot_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void label64_Click(object sender, EventArgs e)
        {

        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpDateLivraisonPrevue_ValueChanged(object sender, EventArgs e)
        {
            dtpDateLivraisonDepotPrevue.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateLivraisonResaPrevue_ValueChanged(object sender, EventArgs e)
        {
            dtpDateLivraisonResaPrevue.CustomFormat = "dd/MM/yyyy";
            cmdEnregistrerVente.Enabled = true;
            //    if (LeLot.Ilot.DateDebutLivraison != null && LeLot.Ilot.DateFinLivraison != null)
            //    {
            //        if (dtpDateLivraisonResaPrevue.Value.Date < LeLot.Ilot.DateDebutLivraison || dtpDateLivraisonResaPrevue.Value.Date > LeLot.Ilot.DateFinLivraison)
            //        {
            //            MessageBox.Show(this, "Cette date n'est pas comprise dans l'intervalle de livraison de l'ilôt qui va du "
            //                + LeLot.Ilot.DateDebutLivraison.Value.ToShortDateString() + " au " + LeLot.Ilot.DateFinLivraison.Value.ToShortDateString()
            //                , "Prosopis - Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            cmdEnregistrerVente.Enabled = false;
            //        }
            //        else
            //        {
            //            cmdEnregistrerVente.Enabled = true;
            //        }
            //    }
            //    else
            //    {
            //        cmdEnregistrerVente.Enabled = true;
            //    }
        }

        private void txtNumeroLot_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreEcheances_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMontantPremierVersement_Validated(object sender, EventArgs e)
        {
            try
            {
                if (bDejaClient)
                {
                    dMontantPremierVersement = decimal.Parse(txtMontantPremierVersement.Text);

                    leNouveauTauxEncaissement = (dMontantPremierVersement / dPrixDeVente * 100);
                    txtTauxEncaissementProspect.Text = (dMontantPremierVersement / dPrixDeVente * 100).ToString("### ### ###");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        private void txtMontantVerse_Validated(object sender, EventArgs e)
        {
            txtMontantPremierVersement.Text = txtMontantVerse.Text;
            txtMontantPremierVersement_Validated(sender, e);
        }

        private void txtMontantVerse_TextChanged(object sender, EventArgs e)
        {
            //if (System.Text.RegularExpressions.Regex.IsMatch(txtMontantVerse.Text, "[^0-9]"))
            //{
            //    MessageBox.Show("Les caractères saisis sont incorrects");
            //    txtMontantVerse.Text.Remove(txtMontantVerse.Text.Length - 1);
            //    return;
            //}
            //else
            //    txtMontantVerse.Text = decimal.Parse(txtMontantVerse.Text).ToString("### ### ####");
            decimal a;
            if (!decimal.TryParse(txtMontantVerse.Text, out a))
            {
                // If not int clear textbox text or Undo() last operation
                txtMontantVerse.Clear();
            }
            else
            {
                txtMontantVerse.Text = decimal.Parse(txtMontantVerse.Text).ToString("### ### ###");
                txtMontantVerse.SelectionStart = txtMontantVerse.Text.Length;
            }
        }

        private void txtMontantEcheanceSouhaite_TextChanged(object sender, EventArgs e)
        {
            decimal a;
            if (!decimal.TryParse(txtMontantEcheanceSouhaite.Text, out a))
            {
                // If not int clear textbox text or Undo() last operation
                txtMontantEcheanceSouhaite.Clear();
            }
            else
            {
                txtMontantEcheanceSouhaite.Text = decimal.Parse(txtMontantEcheanceSouhaite.Text).ToString("### ### ###");
                txtMontantEcheanceSouhaite.SelectionStart = txtMontantEcheanceSouhaite.Text.Length;
            }
        }

        private void chkFraisDossier_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFraisDossier.Checked)
            {
                pRemiseSurFraisDossier.Visible = true;
                txtMontantFraisDeDossier.Text = "200000";
            }
            else
            {
                pRemiseSurFraisDossier.Visible = false;
                txtMontantFraisDeDossier.Text = string.Empty;
            }
        }

        private void chkRemiseAccordee_CheckedChanged(object sender, EventArgs e)
        {

            if (chkRemiseFraisDeDossierAccordee.Checked)
            {
                pTauxRemiseAccordee.Visible = true;
            }
            else
            {
                pTauxRemiseAccordee.Visible = false;
                txtMontantFraisDeDossier.Text = "200000";
                nudRemiseSurFraisDossier.Value = 0;
            }
        }

        private void nudRemiseSurFraisDossier_ValueChanged(object sender, EventArgs e)
        {
            var montantFinal = (200000 - (200000 * nudRemiseSurFraisDossier.Value / 100));

            if (montantFinal == 0)
                txtMontantFraisDeDossier.Text = "0";
            else
                txtMontantFraisDeDossier.Text = montantFinal.ToString("### ### ###");
        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label58_Click(object sender, EventArgs e)
        {

        }

        private void txtNombreEcheancesSouhaitees_TextChanged(object sender, EventArgs e)
        {

        }

        private void pAutreModeReglement_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmVente_Load(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void txtNouveauPrixDeVente_TextChanged(object sender, EventArgs e)
        {

        }

        private void pVersement_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rbAutreDepotMinimum_CheckedChanged(object sender, EventArgs e)
        {
            txtAutreDepotMinimum.Text = dMontantPremierVersement.ToString("### ### ###");
            //txtEcheanceAutreDepotMinimum.Text= dMontantPremierVersement/
            dMontantApresDepot = dPrixDeVente - dMontantPremierVersement;
            int nbEch = 0;
            ContratRepository.CalculNombreEcheances(leTypeEcheancierEnCours, dtpDatePremiereEcheance.Value.Date, dtpDateDerniereEcheance.Value.Date, ref nbEch);
            AfficherEcheancesDepotMinimumSouhaite(nbEch);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbAutreModeReglement_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAutreModeReglement.Checked)
            {
                pAutreModeReglement.Visible = true;
                pModeReglement.Enabled = false;
                cmdSimuler.Text = "Recalculer";
            }
            else
            {
                pAutreModeReglement.Visible = false;
                txtMontantEcheanceSouhaite.Text = string.Empty;
                pModeReglement.Enabled = true;
                cmdSimuler.Text = "Calculer";
            }
        }

        private void rbAutreEcheance_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkAutreDepotMinimum_CheckedChanged(object sender, EventArgs e)
        {
            txtAutreDepotMinimum.Text = string.Empty;
            txtEcheanceAutreDepotMinimum.Text = string.Empty;
            txtNombresEcheancesAutreDepotMinimum.Text = string.Empty;
            if (chkAutreDepotMinimum.Checked)
                txtAutreDepotMinimum.Text = dMontantPremierVersement.ToString("### ### ###");
            //if()
            //{

            //}
            ////txtEcheanceAutreDepotMinimum.Text= dMontantPremierVersement/
            //dMontantApresDepot = dPrixDeVente - dMontantPremierVersement;
            //int nbEch = 0;
            //ContratRepository.CalculNombreEcheances(leTypeEcheancierEnCours, dtpDatePremiereEcheance.Value.Date, dtpDateDerniereEcheance.Value.Date, ref nbEch);
            //AfficherEcheancesDepotMinimumSouhaite(nbEch);
        }

        private void chkAutreEcheance_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutreEcheance.Checked && !chkAutreDepotMinimum.Checked)
            {
                txtMontantEcheanceSouhaite.ReadOnly = false;
                txtAutreDepotMinimum.Text = string.Empty;
                txtEcheanceAutreDepotMinimum.Text = string.Empty;
                txtNombresEcheancesAutreDepotMinimum.Text = string.Empty;
            }
            else if (chkAutreEcheance.Checked && chkAutreDepotMinimum.Checked)
            {
                txtMontantEcheanceSouhaite.ReadOnly = false;
            }
            else if (!chkAutreEcheance.Checked)
            {
                txtMontantEcheanceSouhaite.ReadOnly = true;
                txtMontantEcheanceSouhaite.Text = string.Empty;
                txtMontantDerniereEcheanceSouhaitee.Text = string.Empty;
                txtNombreEcheancesSouhaitees.Text = string.Empty;
            }

        }

        private void txtAutreDepotMinimum_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMontantDerniereEcheanceSouhaitee_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTauxRemise_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void xhkDptConsidererSoldeTotal_CheckedChanged(object sender, EventArgs e)
        {

            ////txtDptDepotMinimum.Text = string.Empty;
            //////txtEcheanceAutreDepotMinimum.Text = string.Empty;
            ////txtNombresEcheancesAutreDepotMinimum.Text = string.Empty;
            //var depotMinimum = (dPrixDeVente * leTypeContratEnCours.SeuilSouscription / 100);
            //if (chkDptConsidererSoldeTotal.Checked)
            //{
            //    txtDptDepotMinimum.Text = dMontantPremierVersement.ToString("### ### ###");
            //    txtDptMontantAVentiller.Text = "0";
            //}
            //else
            //{
            //    txtDptDepotMinimum.Text = depotMinimum.ToString("### ### ###");
            //    txtDptMontantAVentiller.Text = (dMontantPremierVersement - depotMinimum).ToString("### ### ###"); ;
            //}
            //EffacerParametresDepot();
            //chkDptEcheanceSouhaite.Checked = false;
            //chkDptEcheanceSouhaite.Enabled = false;
            //cmdSimulerDepot.Enabled = false;
            //chkDptEcheanceSouhaite.Enabled = false;
        }

        private void dtpDptFin_ValueChanged(object sender, EventArgs e)
        {
            if(!chkDptEcheanceSouhaite.Checked)
            { 
                EffacerParametresDepot();
                chkDptEcheanceSouhaite.Checked = false;
                cmdSimulerDepot.Enabled = false;
                chkDptEcheanceSouhaite.Enabled = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime start = dtpDptDebut.Value;
                DateTime end = dtpDptFin.Value;
                if (start.Date == end.Date)
                {
                    MessageBox.Show(this, "Veuillez saisir la date de dernière échéance",
                                    "Prosopis - Gestion des contrats dépots", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //decimal depotMinimum = 0;
                //Calcul du Dépot Initial et du montant à ventiller
                if (rbConsidererSeuilMinimum.Checked)
                {
                    LeDepotMinimum = (LePrixDeVente * LeTypeContrat.SeuilSouscription / 100);
                    txtDptDepotMinimum.Text = LeDepotMinimum.ToString("### ### ##0");
                    txtDptMontantAVentiller.Text = (LeMontantPremierVersement - LeDepotMinimum).ToString("### ### ##0"); 

                }
                else 
                    if(rbConsidererSoldeTotal.Checked)
                    {
                        LeDepotMinimum = LeMontantPremierVersement;
                        txtDptDepotMinimum.Text = LeDepotMinimum.ToString("### ### ##0");
                        txtDptMontantAVentiller.Text = (LeMontantPremierVersement - LeDepotMinimum).ToString("### ### ##0");
                    }
                    else
                    {
                        LeDepotMinimum = decimal.Parse(txtDptDepotInitialSaisi.Text);
                        txtDptDepotMinimum.Text = LeDepotMinimum.ToString("### ### ##0");
                        txtDptMontantAVentiller.Text = (LeMontantPremierVersement - LeDepotMinimum).ToString("### ### ##0");
                        var depotGarantie = (LePrixDeVente * LeTypeContrat.SeuilSouscription / 100);
                        if (LeDepotMinimum > LeMontantPremierVersement)
                        {
                            MessageBox.Show(this, "Le dépot minimum ne peut être supérieur au montant versé qui est de" + LeMontantPremierVersement.ToString("### ### ###"),
                                        "Prosopis - Gestion des contrats dépots", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    if (LeDepotMinimum < depotGarantie)
                        {
                            MessageBox.Show(this, "Le dépot minimum saisi doit être supérieur au seuil exigé de " 
                                + LeTypeContrat.SeuilSouscription+"%"+" du prix de vente de la villa, soit: "+depotGarantie.ToString("### ### ###"),
                                        "Prosopis - Gestion des contrats dépots", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                var diffMonths = (end.Month + end.Year * 12) - (start.Month + start.Year * 12) + 1;
                txtDptDureeDepot.Text = diffMonths.ToString();
                int nbEcheances = ContratRepository.CalculNombreEcheancesDepot(start, end, LeTypeEcheancier);
                txtDptNbEcheances.Text = nbEcheances.ToString();
                var dDptMontantApresDepot = LePrixDeVente - decimal.Parse(txtDptDepotMinimum.Text);
                var MontantEcheancePrevu = (int)dDptMontantApresDepot / nbEcheances;
                txtDptMontantEcheance.Text = MontantEcheancePrevu.ToString("### ### ##0");
                //dtpDptLivraisonDepot.Value = dtpDptFin.Value;
                cmdSimulerDepot.Enabled = true;
                chkDptEcheanceSouhaite.Enabled = true;
                //var montantDerniereTraite = (int)(dMontantApresDepot - MontantEcheancePrevu * NbEcheances);
                //if (montantDerniereTraite > 0)
                //    NbEcheances++;
                //txtNombreEcheances.Text = (NbEcheances - 1).ToString();
                //txtMontantDerniereEcheance.Text = montantDerniereTraite.ToString("### ### ###");
                //LeDepotMinimum = dPrixDeVente * leTypeContratEnCours.SeuilSouscription / 100;
                //LeMontantEcheance = MontantEcheancePrevu;
                //LeNombreEcheance = NbEcheances - 1;
                //LeMontantDerniereEcheance = montantDerniereTraite;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur :..." + ex.Message,
                        "Prosopis - Gestion des contrats dépots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbDtpPeriodicite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDtpPeriodicite.SelectedItem != null)
            {
                LeTypeEcheancier = (TypeEcheancier)cmbDtpPeriodicite.SelectedItem;
                EffacerParametresDepot();
                chkDptEcheanceSouhaite.Checked = false;
                cmdSimulerDepot.Enabled = false;
            }
        }

        private void chkDptEcheanceSouhaite_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDptEcheanceSouhaite.Checked)
            {
                txtDptMontantEcheance.ReadOnly = false;
                txtDptMontantEcheance.Text = string.Empty;

            }
            else
            {
                txtDptMontantEcheance.ReadOnly = true;
                txtDptMontantEcheance.Text = string.Empty;
                cmdSimulerDepot.Enabled = false;
            }
        }
        //txtAutreDepotMinimum.Text = dMontantPremierVersement.ToString("### ### ###");
        //    //txtEcheanceAutreDepotMinimum.Text= dMontantPremierVersement/
        //    dMontantApresDepot = dPrixDeVente - dMontantPremierVersement; 
        //    int nbEch = 0;
        //ContratRepository.CalculNombreEcheances(leTypeEcheancierEnCours, dtpDatePremiereEcheance.Value.Date, dtpDateDerniereEcheance.Value.Date, ref nbEch);
        //    AfficherEcheancesDepotMinimumSouhaite(nbEch);

        private void EffacerParametresDepot()
        {
            txtDptMontantEcheance.Text = string.Empty;
            txtDptNbEcheances.Text = string.Empty;
            txtDptDureeDepot.Text = string.Empty;
            dgEcheances.DataSource = null;
            txtMontantTotalEchancesDepot.Text = string.Empty;
            txtEcartDepot.Text = string.Empty;
            txtCumulEcheanceDepot.Text = string.Empty;


        }

        private void dtpDptDebut_ValueChanged(object sender, EventArgs e)
        {
            EffacerParametresDepot();
            chkDptEcheanceSouhaite.Checked = false;
            cmdSimulerDepot.Enabled = false;
            chkDptEcheanceSouhaite.Enabled = false;
        }


        //private void AjouterContratDepot()
        //{
        //   // var commRep = new CommercialRepository();
        //    var contrat = new Contrat
        //    {
        //        ClientID = leProspectEnCours.ID,
        //        //CommercialID = commRep.leCommercialEnCours.Id,
        //        DateCreationSysteme = DateTime.Now,
        //        PrixRevise = leLotEnCours.PrixRevise,
        //        PrixFinal = dPrixDeVente,
        //       // PrixStandar = leTypeVillaDepotEnCours.PrixStandard,
        //        RemiseAccordee = dMontantRemise,
        //        TypeContratID = leTypeContratEnCours.ID,
        //        TypeEcheancier = leTypeEcheancierEnCours,
        //        LotId = leLotEnCours.ID,
        //        ApporteurID = LApporteurAffaireEnCours.ID,
        //        //DateLivraisonVilla = DateTime.Parse(model.DateLivraisonVilla)
        //    };

        //    leLotEnCours.StatutLot = StatutLot.Option;
        //    ContratRep.Add(contrat);
        //}



        private void SimulerEcheancesDepot(TypeEcheancier typeEcheancier, DateTime datePremiereEcheance, decimal montantEcheance, int nbEcheances
            , decimal montantDerniereEcheance)
        {
            try
            {
                echeancesSimules = null;
                echeancesSimules = new List<EcheanceSimule>();
                int j = 0;
                for (int i = 0; i < nbEcheances; i++)
                {
                    //var echeance = new Echeance()
                    //{

                    //};


                    EcheanceSimule facture = new EcheanceSimule
                    {
                        //Year = TypeFacture.Echeance,
                        //Montant = client.ID,
                        //Date = DateTime.Now.Date,
                        //LibelleEcheance
                        Montant = montantEcheance

                    };

                    //if (i == nbEcheances - 2 && montantDerniereEcheance > 0)
                    //{
                    //    facture.Montant = montantDerniereEcheance;
                    //    derniereEchance = true;
                    //}
                    //else
                    //{
                    //    facture.Montant = montantEcheance;
                    //}

                    switch (typeEcheancier)
                    {
                        case TypeEcheancier.Mensuel:
                            facture.Date = datePremiereEcheance.AddMonths(i * 1);
                            facture.LibelleEcheance = "Echéance Mensuelle de " + String.Format("{0:y}", facture.Date);
                            facture.Year = facture.Date.Year;
                            facture.Month = facture.Date.Month;
                            break;
                        case TypeEcheancier.Trimestriel:
                            facture.Date = datePremiereEcheance.AddMonths(i * 3);
                            facture.LibelleEcheance = "Echéance trimestrielle de " + String.Format("{0:y}", facture.Date);
                            facture.Year = facture.Date.Year;
                            facture.Month = facture.Date.Month;
                            break;
                        case TypeEcheancier.Semestriel:
                            facture.Date = datePremiereEcheance.AddMonths(i * 6);
                            facture.LibelleEcheance = "Echéance semestrielle de " + String.Format("{0:y}", facture.Date);
                            facture.Year = facture.Date.Year;
                            facture.Month = facture.Date.Month;
                            break;
                        case TypeEcheancier.Annuel:
                            facture.Date = datePremiereEcheance.AddMonths(i * 12);
                            facture.LibelleEcheance = "Echéance annuelle de " + String.Format("{0:y}", facture.Date);
                            facture.Year = facture.Date.Year;
                            facture.Month = facture.Date.Month;
                            break;
                        default:
                            break;
                    }

                    echeancesSimules.Add(facture);
                    j = i;

                }
                if (montantDerniereEcheance >0)
                {
                    ////j++;
                    ////EcheanceSimule facture = new EcheanceSimule
                    ////{
                    ////    //Year = TypeFacture.Echeance,
                    ////    //Montant = client.ID,
                    ////    //Date = DateTime.Now.Date,
                    ////    //LibelleEcheance
                    ////    Montant = montantDerniereEcheance

                    ////};
                    //Recupérer la derniere échéance
                    //echeancesSimules[nbEcheances - 1].Montant += montantDerniereEcheance;
                    ////switch (typeEcheancier)
                    ////{
                    ////    case TypeEcheancier.Mensuel:
                    ////        facture.Date = datePremiereEcheance.AddMonths(j * 1);
                    ////        facture.LibelleEcheance = "Echéance Mensuelle de " + String.Format("{0:y}", facture.Date);
                    ////        facture.Year = facture.Date.Year;
                    ////        facture.Month = facture.Date.Month;
                    ////        break;
                    ////    case TypeEcheancier.Trimestriel:
                    ////        facture.Date = datePremiereEcheance.AddMonths(j * 3);
                    ////        facture.LibelleEcheance = "Echéance trimestrielle de " + String.Format("{0:y}", facture.Date);
                    ////        facture.Year = facture.Date.Year;
                    ////        facture.Month = facture.Date.Month;
                    ////        break;
                    ////    case TypeEcheancier.Semestriel:
                    ////        facture.Date = datePremiereEcheance.AddMonths(j * 6);
                    ////        facture.LibelleEcheance = "Echéance semestrielle de " + String.Format("{0:y}", facture.Date);
                    ////        facture.Year = facture.Date.Year;
                    ////        facture.Month = facture.Date.Month;
                    ////        break;
                    ////    case TypeEcheancier.Annuel:
                    ////        facture.Date = datePremiereEcheance.AddMonths(j * 12);
                    ////        facture.LibelleEcheance = "Echéance annuelle de " + String.Format("{0:y}", facture.Date);
                    ////        facture.Year = facture.Date.Year;
                    ////        facture.Month = facture.Date.Month;
                    ////        break;
                    ////    default:
                    ////        break;
                    ////}

                    ////echeancesSimules.Add(facture);
                }

                dgEcheances.DataSource = echeancesSimules.ToList();
                dgEcheances.Columns[0].Width = 0;
                dgEcheances.Columns[0].Visible = false;
                dgEcheances.Columns[1].Width = 0;
                dgEcheances.Columns[1].Visible = false;
                dgEcheances.Columns[2].Width = 80;
                dgEcheances.Columns[3].Width = 250;
                dgEcheances.Columns[4].DefaultCellStyle.Format = "### ### ###";
                dgEcheances.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception)
            {

                throw;
            }




        }

        private void cmdSimulerDepot_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtDptMontantEcheance.Text== string.Empty)
                {
                    MessageBox.Show(this, "Erreur :... veuillez renseigner tous les critères du dépôt",
                       "Prosopis - Gestion des contrats dépots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var typeEcheancier = (TypeEcheancier)cmbDtpPeriodicite.SelectedItem;
                var datePremiereEcheance = dtpDptDebut.Value.Date;
                var montantEcheance = decimal.Parse(txtDptMontantEcheance.Text);
                
                //Recalculer le nombre d'échance
                int nbEcheances = int.Parse(txtDptNbEcheances.Text);
                var montantDerniereEcheance = 0;
                if (chkDptEcheanceSouhaite.Checked)
                {
                    //nbEcheances = Convert.ToInt16(Math.Floor((LePrixDeVente - LeDepotMinimum) / montantEcheance));
                    montantDerniereEcheance = (int)((LePrixDeVente - LeDepotMinimum) - montantEcheance * nbEcheances);
                    DateTime dateDerniereEcheance= dtpDptFin.Value;
                    switch (typeEcheancier)
                    {
                        case TypeEcheancier.Mensuel:
                            dateDerniereEcheance = dtpDatePremiereEcheance.Value.AddMonths(nbEcheances).Date;
                            break;
                        case TypeEcheancier.Trimestriel:
                            dateDerniereEcheance = dtpDatePremiereEcheance.Value.AddMonths(nbEcheances * 3).Date;
                            break;
                        case TypeEcheancier.Semestriel:
                            dateDerniereEcheance = dtpDatePremiereEcheance.Value.AddMonths(nbEcheances * 6).Date;
                            break;
                        case TypeEcheancier.Annuel:
                            dateDerniereEcheance = dtpDatePremiereEcheance.Value.AddMonths(nbEcheances * 12).Date;
                            break;
                        default:
                            break;
                    }
                    //var dateDerniereEcheance = dtpDatePremiereEcheance.Value.AddMonths(nbEcheances).Date;
                    var diffMonths = (dateDerniereEcheance.Month + dateDerniereEcheance.Year * 12) - (datePremiereEcheance.Month + datePremiereEcheance.Year * 12) + 1;
                    dtpDptFin.Value = dateDerniereEcheance;
                    dtpDptLivraisonDepot.Value = dateDerniereEcheance;
                    txtDptNbEcheances.Text = nbEcheances.ToString();
                    txtDptDureeDepot.Text = diffMonths.ToString();
                }

                SimulerEcheancesDepot(typeEcheancier, datePremiereEcheance, montantEcheance, nbEcheances, montantDerniereEcheance);
                decimal total = dgEcheances.Rows.Cast<DataGridViewRow>()
                     .Sum(t => Convert.ToDecimal(t.Cells[4].Value));
                txtMontantTotalEchancesDepot.Text = total.ToString("### ### ##0");
                txtCumulEcheanceDepot.Text=(decimal.Parse(txtDptDepotMinimum.Text ) +total).ToString("### ### ##0");
                txtEcartDepot.Text=(LePrixDeVente- (decimal.Parse(txtDptDepotMinimum.Text) + total)).ToString("### ### ##0");
                //cmdEnregistrer.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur :..." + ex.Message,
                        "Prosopis - Gestion des contrats dépots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgEcheances_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                decimal total = dgEcheances.Rows.Cast<DataGridViewRow>()
                        .Sum(t => Convert.ToDecimal(t.Cells[4].Value));
                txtMontantTotalEchancesDepot.Text = total.ToString("### ### ##0");
                txtMontantTotalEchancesDepot.Text = total.ToString("### ### ##0");
                txtCumulEcheanceDepot.Text = (decimal.Parse(txtDptDepotMinimum.Text) + total).ToString("### ### ##0");
                txtEcartDepot.Text = (LePrixDeVente - (decimal.Parse(txtDptDepotMinimum.Text) + total)).ToString("### ### ##0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur :..." + ex.Message,
                        "Prosopis - Gestion des contrats dépots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if(decimal.Parse(txtEcartDepot.Text) >0)
                {
                    MessageBox.Show(this, "Veuillez d'abord résorber l'écart entre les amortissement et le prix de vente",
                        "Prosopis -  Gestion des contrats dépôts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                cmdEnregistrer.Enabled = false;

                //var referencePaiement = txtReferencePaiement.Text;
                //var commentairesVersement = txtCommentairePaiement.Text;
                //var modePaiement = (ModePaiement)cmbModePaiement.SelectedItem;
                var dateLivraisonPrevue = dtpDptLivraisonDepot.Value.Date;
               
                //var dTauxRemiseFraisDeDossier = nudRemiseSurFraisDossier.Value;
                //var tauxDeRemiseFraisDeDossier = nudRemiseSurFraisDossier.Value;
                var commercial = Tools.Tools.AgentEnCours;
                var dateContrat = dtpDateContrat.Value.Date;
                //Vérifier l'existence d'apporteur d'affaire sur le contrat
                var idAAffaire = 0;
                if (LeProspect == null)
                {
                    MessageBox.Show(this, "Veuillez d'abord choisir l'acquéreur",
                          "Prosopis -  Gestion des contrats dépôts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cmbApporteurAffaires.SelectedItem != null)
                {
                    idAAffaire = LApporteurAffaireEnCours.Id;
                }

                //if (dtpDatePremiereEcheance.Text.Trim() == string.Empty || dtpDateDerniereEcheance.Text.Trim() == string.Empty)
                //{
                //    MessageBox.Show(this, "Veuillez d'abord déterminer la date de première et dernière échéance",
                //        "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                int nbEch = 0;
             
                int contratId = AjouterContratDepotQuatro(LeProspect.ProjetId.Value,LeProspect.ID, LeProspect.CommercialID.Value, idAAffaire
                            , LeTypeVilla, LaPosition, LePrixDeVente, LeTauxRemise, LeMontantRemise, LeTypeContrat,
                            LeTypeEcheancier, dtpDptDebut.Value.Date, dtpDptFin.Value.Date,
                            dtpDateContrat.Value.Date, dtpDptLivraisonDepot.Value.Date, 
                            decimal.Parse(txtDptDepotMinimum.Text), decimal.Parse(txtDptMontantEcheance.Text), Int16.Parse(txtDptNbEcheances.Text), 0,echeancesSimules,Int16.Parse(txtDptDureeDepot.Text));


                if (contratId != 0)
                {
                    contratRep.TransfererFraisDeDossier(contratId);
                }
                //En cas de changement de lot libérer l'ancienne option.
                if (LOption != null)
                {
                    contratRep.CloturerOption(LOption.Id, LeLot.ID);
                }

                MessageBox.Show(this, "Le contrat a été enregistré avec succes",
                               "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Information);


                if (bInitial)
                {
                    FrmDossierClient childForm = new FrmDossierClient(leProspectEnCours);
                    childForm.MdiParent = this.MdiParent;
                    childForm.Show();
                    childForm.WindowState = FormWindowState.Maximized;
                }
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:... " + ex.Message,
                          "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public int AjouterContratDepotQuatro(int projetId, int clientId, int commercialId, int apporteurAffaireId,
           TypeVilla typeVilla, PositionLot position, decimal prixDeVente, decimal tauxRemiseAccordee, decimal MontantRemiseAccordee,
           TypeContrat typeContrat, TypeEcheancier typeEcheancier, DateTime datePremiereEcheance, DateTime dateDerniereEcheance,
          DateTime dateContrat, DateTime dateLivraisonPrevue, decimal montantDepotMinimum, decimal montantEcheance,
          int nbEcheances, decimal montantDerniereEcheance, List<EcheanceSimule> echeancesSimules, int dureeDepot)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    Contrat contrat = null;

                    using (var ctx = new SenImmoDataContext())
                    {
                        #region RECUPERATION DES DIFFERENTS CONSTITUANTS DU CONTRAT

                        Lot lot = IlotRep.GetLotVirtuel(typeVilla, position);
                        var client = ctx.Clients.Find(clientId);

                        ApporteurAffaire apporteur = null;
                        if (apporteurAffaireId != 0)
                        {
                            apporteur = new ApporteurAffaireRepository().FindById(apporteurAffaireId);
                        }

                        //decimal prixDevente = lot.PrixRevise;

                        #endregion

                        #region CALCUL DU MONTANT DE LA REMISE SI ACCORDEE ET DETERMINATION DU PRIX DE VENTE FINAL
                        //prixDeVente += MontantRemiseAccordee;

                        //contratRep.CalculerRemise(ref prixDeVente, ref tauxRemiseAccordee, ref MontantRemiseAccordee);

                        #endregion
                        // VERIFIER SI LE MONTANT DU PREMIER VERSEMENT OU CELUI CUMULE DANS LE COMPTE DU CLIENT A ATTEINT LE SEUIL DE RESERVATION

                        #region CREATION DU CONTRAT

                        // Enregistrer le contrat
                        var dernierNumeroContratDepot = ctx.Parametres.Where(param => param.Nom == "DernierNumeroContratDepot").FirstOrDefault();
                        dernierNumeroContratDepot.valeurInt++;

                        contrat = new Contrat
                        {
                            ProjetId= projetId,
                            NumeroContrat = typeVilla.CodeType + "-" + dernierNumeroContratDepot.valeurInt.ToString().PadLeft(5, '0'),
                            ClientID = client.ID,
                            CommercialID = commercialId,
                            DateCreationSysteme = DateTime.Now.Date,
                            DateSouscription = dateContrat,
                            PrixRevise = lot.PrixRevise,
                            PrixFinal = prixDeVente,
                            // PrixStandar = typeVilla.PrixStandard,
                            RemiseAccordee = MontantRemiseAccordee,
                            TypeContratID = typeContrat.ID,
                            TypeEcheancier = typeEcheancier,
                            LotId = lot.ID,
                            DateLivraisonLot = dateLivraisonPrevue,
                            Souscrit = false,
                            AReserve = false,
                            AttribuerLot = false,
                            LotAttribue = false,
                            Statut = StatutContrat.Actif,
                            DureeDepot=dureeDepot
                        };

                        if (apporteur != null)
                        {
                            contrat.ApporteurID = apporteur.Id;
                            contrat.CommissionApporteur = prixDeVente * apporteur.TauxCommission / 100;
                        }


                        ctx.Contrats.Add(contrat);



                        #endregion

                        #region ENREGISTREMENT FACTURE DEPOT MINIMAL

                        //var montantDepotDeGarantie = prixDevente * typeContrat.SeuilSouscription / 100;


                        Facture factureDepotDeGarantie = new Facture
                        {
                            TypeFacture = TypeFacture.DepotMinimum,
                            ClientId = client.ID,
                            Date = DateTime.Now.Date,
                            DateEcheanceFacture = dateContrat,
                            Motif = "Facture Dépot minimun de " + typeContrat.SeuilSouscription + "%",
                            FacturePayee = false,

                        };
                        factureDepotDeGarantie.Montant = montantDepotMinimum;

                        factureDepotDeGarantie.NumeroFacture = "DG" + contrat.NumeroContrat.ToString().PadLeft(5, '0') + typeEcheancier.ToString().Substring(0, 2).ToUpper()
                                                               + dateContrat.Month.ToString().PadLeft(2, '0') + dateContrat.Year.ToString().Substring(2, 2);
                        contrat.Factures.Add(factureDepotDeGarantie);

                        #endregion

                        #region CALCUL DES ECHEANCES
                        var montantRestantADispacher = prixDeVente - montantDepotMinimum;
                        //var nbEcheances = 0;
                        // Calculer le nombre d'échéances
                        //nbEcheances = CalculNombreEcheances(typeEcheancier, datePremiereEcheance, dateDerniereEcheance, ref nbEcheances);
                        contrat.NbEcheances = nbEcheances;

                        //model.DateFinPrevue = derniereEcheance;
                        //var montantEcheance = (int)montantRestantADispacher / nbEcheances;
                        contrat.MontantEcheance = montantEcheance;
                        //var montantDerniereEcheance = (int)(montantRestantADispacher - montantEcheance * nbEcheances);
                        if (montantDerniereEcheance > 0)
                            nbEcheances++;
                        #endregion

                        #region GENERATION DES ECHEANCES

                        foreach (var ech in  echeancesSimules)
                        {
                            Facture facture = new Facture
                            {
                                TypeFacture = TypeFacture.Echeance,
                                ClientId = client.ID,
                                Date = dateContrat.Date,
                                //DateEcheanceFacture = dateContrat,
                                FacturePayee = false,
                                Montant=ech.Montant,
                                DateEcheanceFacture=ech.Date,
                                Motif=ech.LibelleEcheance
                            };

                            //if (i == nbEcheances - 2 && montantDerniereEcheance > 0)
                            //{
                            //    facture.Montant = montantDerniereEcheance;
                            //    derniereEchance = true;
                            //}
                            //else
                            //{
                            //    facture.Montant = montantEcheance;
                            //}

                            //switch (typeEcheancier)
                            //{
                            //    case TypeEcheancier.Mensuel:
                            //        facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 1);
                            //        facture.Motif = "Echéance mensuelle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                            //        break;
                            //    case TypeEcheancier.Trimestriel:
                            //        facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 3);
                            //        facture.Motif = "Echéance trimestrielle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                            //        break;
                            //    case TypeEcheancier.Semestriel:
                            //        facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 6);
                            //        facture.Motif = "Echéance semestrielle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                            //        break;
                            //    case TypeEcheancier.Annuel:
                            //        facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 12);
                            //        facture.Motif = "Echéance annuelle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                            //        break;
                            //    default:
                            //        break;
                            //}
                            facture.NumeroFacture = contratRep.GenererNumeroFacturesDepot(contrat.NumeroContrat, typeEcheancier, facture.DateEcheanceFacture.Value);
                            contrat.Factures.Add(facture);
                            //if (derniereEchance)
                            //{
                            //    break;
                            //}

                        }
                        ctx.SaveChanges();
                        #endregion

                        #region ENREGISTREMENT DES ENCAISSEMENTS DU CLIENT(PREALABLEMENT ENREGISTRES SUR SON COMPTE PROSPECT)
                        var lesEncaissementsProspects = contratRep.GetEncaissementProspect(client.ID).Where(encProspect => encProspect.FraisDeDossier == false).ToList();
                        foreach (var encaissementProspect in lesEncaissementsProspects)
                        {
                            #region CREER L'ENCAISSEMENT GLOBAL
                            var encaissementGlobal = new EncaissementGlobal()
                            {
                                NumeroEncaissement = encaissementProspect.NumeroEncaissement,
                                DateEncaissement = encaissementProspect.DateEncaissement.Value.Date,
                                MontantGlobal = encaissementProspect.MontantGlobal,
                                ContratId = contrat.Id,
                                ModePaiement = encaissementProspect.ModePaiement,
                                Commentaire = encaissementProspect.Commentaire,
                                ReferencePaiement = encaissementProspect.ReferencePaiement

                            };
                            ctx.EncaissementGlobals.Add(encaissementGlobal);
                            encaissementProspect.Deverse = true;
                            ////Mis à jour des éléments contractuels
                            contrat.MontantVerse += encaissementGlobal.MontantGlobal;
                            #endregion

                            #region ENCAISSEMENT DU DEPOT MINIMUM
                            decimal montantAVentiller = 0;
                            montantAVentiller = encaissementProspect.MontantGlobal;
                            var factureDepotMinimum = contrat.Factures.Where(u => u.TypeFacture == TypeFacture.DepotMinimum).FirstOrDefault();
                            if (factureDepotMinimum.FacturePayee == false)
                            {
                                

                                decimal montantDepotDeGarantieRestant = factureDepotMinimum.Montant - factureDepotMinimum.MontantEncaisse; ;

                                decimal montantDepotDeGarantieAEncaisser = 0;

                                if (montantAVentiller >= montantDepotDeGarantieRestant)
                                {
                                    montantDepotDeGarantieAEncaisser = montantDepotDeGarantieRestant;
                                }
                                else
                                {
                                    montantDepotDeGarantieAEncaisser = montantAVentiller;
                                }


                                Encaissement encaissementDepotDeGarantie = new Encaissement
                                {
                                    Date = encaissementGlobal.DateEncaissement,
                                    ModePaiement = encaissementGlobal.ModePaiement,
                                    Montant = montantDepotDeGarantieAEncaisser,
                                    Commentaire = "Encaissement du dépot minimun de " + typeContrat.SeuilSouscription + "%",
                                    ReferencePaiement = encaissementGlobal.ReferencePaiement,

                                };
                                factureDepotDeGarantie.Encaissements.Add(encaissementDepotDeGarantie);
                                factureDepotDeGarantie.Echue = true;
                                if (montantAVentiller >= montantDepotDeGarantieRestant)
                                {
                                    factureDepotDeGarantie.FacturePayee = true;
                                    factureDepotDeGarantie.Active = true;

                                }
                                encaissementGlobal.Encaissements.Add(encaissementDepotDeGarantie);
                                //Ventiller le versement aprés avoir extrait le dépot de garantie
                                montantAVentiller = montantAVentiller - montantDepotDeGarantieAEncaisser;
                            }
                            #endregion

                            #region ENREGISTRER LA COMMISSION DE L'APPORTEUR D'AFFAIRE SI APPLICABLE
                            if (apporteur != null)
                            {
                                var factureCommission = new FactureCommission
                                {
                                    ContratId = contrat.Id,
                                    EncaissementGlobalId = encaissementGlobal.ID,
                                    Date = encaissementGlobal.DateEncaissement.Value.Date,
                                    MontantAPayer = encaissementGlobal.MontantGlobal * apporteur.TauxCommission / 100,
                                    Motif = "Commission encaissement n° " + encaissementGlobal.NumeroEncaissement + " sur " + encaissementGlobal.ReferencePaiement,
                                    Payee = false
                                };
                                contrat.FactureCommissions.Add(factureCommission);
                            }

                            ctx.SaveChanges();
                            #endregion

                            #region VERIFICATION DU NIVEAU D'ENCAISSEMENT ET MISES A JOUR NECESSAIRES DU CONTRAT

                            var niveauEncaissement = contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) / contrat.PrixFinal * 100;

                            if (niveauEncaissement >= typeContrat.SeuilEntreeEnVigueur && contrat.AReserve == false)
                            {
                                contrat.AReserve = true;
                                //contrat.Lot.StatutLot = StatutLot.Reserve;
                                contrat.AttribuerLot = true;
                                contrat.Client.Type = TypeClient.Client;
                                contrat.DateReservation = dateContrat;
                            }
                            else if (niveauEncaissement >= typeContrat.SeuilSouscription && contrat.Souscrit == false)
                            {
                                contrat.Souscrit = true;
                                //contrat.Lot.StatutLot = StatutLot.ReservationEnCours;
                                contrat.AttribuerLot = false;
                                contrat.Client.Type = TypeClient.ClientEnCours;
                                contrat.DateSouscription = dateContrat;
                            }
                            #endregion

                            #region VENTILLATION DU PAIEMENT SUR LES FACTURES

                            foreach (var fact in contrat.Factures.Where(u => u.FacturePayee == false && u.TypeFacture == TypeFacture.Echeance).OrderBy(u => u.DateEcheanceFacture))
                            {
                                if (montantAVentiller > 0)
                                {
                                    if (fact.Encaissements != null)
                                    {
                                        var totalEncaissement = fact.Encaissements.Sum(u => u.Montant);
                                        var resteAEncaisser = fact.Montant - totalEncaissement;
                                        decimal montantAEncaisser = 0;

                                        if (montantAVentiller >= resteAEncaisser)
                                        {
                                            montantAEncaisser = resteAEncaisser;
                                        }
                                        else
                                        {
                                            montantAEncaisser = montantAVentiller;
                                        }
                                        Encaissement nouvelEncaissement = new Encaissement
                                        {
                                            Date = dateContrat.Date,
                                            ModePaiement = encaissementGlobal.ModePaiement,
                                            Montant = montantAEncaisser,
                                            Commentaire = encaissementGlobal.Commentaire,
                                            ReferencePaiement = encaissementGlobal.ReferencePaiement
                                        };
                                        fact.Encaissements.Add(nouvelEncaissement);

                                        encaissementGlobal.Encaissements.Add(nouvelEncaissement);
                                        if (montantAVentiller >= resteAEncaisser)
                                        {
                                            fact.FacturePayee = true;

                                        }
                                        fact.Active = true;
                                        montantAVentiller -= montantAEncaisser;
                                    }
                                }
                                else
                                    break;
                            }
                            encaissementGlobal.EncaissementLettre = true;
                            //VERIFIER SI LES FACTURES DU CONTRAT SONT TOUTES SOLDEES
                            if (contrat.Factures.Sum(f => f.Montant - f.Encaissements.Sum(enc => enc.Montant)) <= 0)
                            {
                                contrat.ContratSolde = true;
                            }

                            ctx.SaveChanges();
                            #endregion

                        }
                        #endregion

                        ctx.SaveChanges();
                    }
                    scope.Complete();
                    return contrat.Id;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void dtpDateContrat_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rbSaisirDepotInitial_CheckedChanged(object sender, EventArgs e)
        {
            if (rbConsidererDepotInitialSaisi.Checked)
            {
                txtDptDepotInitialSaisi.Text = string.Empty;
                txtDptDepotInitialSaisi.Visible = true;
            }
            else
            {
                txtDptDepotInitialSaisi.Text = string.Empty;
                txtDptDepotInitialSaisi.Visible = false;
            }

            //var depotMinimum = (dPrixDeVente * leTypeContratEnCours.SeuilSouscription / 100);
            //if (rbSaisirDepotInitial.Checked)
            //{
            //    txtDptDepotMinimum.Text = string.Empty;
            //    txtDptDepotMinimum.Enabled =true;
            //    txtDptMontantAVentiller.Text = "0";
            //}
            //else
            //{
            //    txtDptDepotMinimum.Text = depotMinimum.ToString("### ### ###");
            //    txtDptMontantAVentiller.Text = (dMontantPremierVersement - depotMinimum).ToString("### ### ###"); ;
            //}
            //EffacerParametresDepot();
            //chkDptEcheanceSouhaite.Checked = false;
            //chkDptEcheanceSouhaite.Enabled = false;
            //cmdSimulerDepot.Enabled = false;
            //chkDptEcheanceSouhaite.Enabled = false;
        }

        private void txtDptDepotMinimum_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    if (rbSaisirDepotInitial.Checked)
            //    {
            //        var depotMinimum = decimal.Parse(txtDptDepotMinimum.Text);
            //        txtDptMontantAVentiller.Text = (dMontantPremierVersement - depotMinimum).ToString("### ### ###");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, "Erreur:... " + ex.Message,
            //             "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void rbConsidererSoldeTotal_CheckedChanged(object sender, EventArgs e)
        {
            //var depotMinimum = (dPrixDeVente * leTypeContratEnCours.SeuilSouscription / 100);
            //if (rbConsidererSoldeTotal.Checked)
            //{
            //    txtDptDepotMinimum.Text = dMontantPremierVersement.ToString("### ### ###");
            //    txtDptMontantAVentiller.Text = "0";
            //}
            //else
            //{
            //    txtDptDepotMinimum.Text = depotMinimum.ToString("### ### ###");
            //    txtDptMontantAVentiller.Text = (dMontantPremierVersement - depotMinimum).ToString("### ### ###"); ;
            //}
            //EffacerParametresDepot();
            //chkDptEcheanceSouhaite.Checked = false;
            //chkDptEcheanceSouhaite.Enabled = false;
            //cmdSimulerDepot.Enabled = false;
            //chkDptEcheanceSouhaite.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtDptDepotInitialSaisi_TextChanged(object sender, EventArgs e)
        {

            decimal a;
            if (!decimal.TryParse(txtDptDepotInitialSaisi.Text, out a))
            {
                // If not int clear textbox text or Undo() last operation
                txtDptDepotInitialSaisi.Clear();
            }
            else
            {
                txtDptDepotInitialSaisi.Text = decimal.Parse(txtDptDepotInitialSaisi.Text).ToString("### ### ###");
                txtDptDepotInitialSaisi.SelectionStart = txtDptDepotInitialSaisi.Text.Length;
            }
        }

        private void txtDptMontantEcheance_TextChanged(object sender, EventArgs e)
        {
            decimal a;
            if (!decimal.TryParse(txtDptMontantEcheance.Text, out a))
            {
                // If not int clear textbox text or Undo() last operation
                txtDptMontantEcheance.Clear();
            }
            else
            {
                txtDptMontantEcheance.Text = decimal.Parse(txtDptMontantEcheance.Text).ToString("### ### ###");
                txtDptMontantEcheance.SelectionStart = txtDptMontantEcheance.Text.Length;
            }
        }

        private void chkDptNbEcheanceSouhaite_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDptNbEcheanceSouhaite.Checked)
            {
                txtDptNbEcheances.ReadOnly = false;
                txtDptNbEcheances.Text = string.Empty;
            }
           
        }

        private void dtpDptLivraisonDepot_ValueChanged(object sender, EventArgs e)
        {
            dtpDptLivraisonDepot.CustomFormat = "dd/MM/yyyy";
            if(dtpDptLivraisonDepot.Text!=string.Empty)
            cmdEnregistrer.Enabled = true;
        }
    }

    class AppelDeFondSimule
    {
            public string Niveau { get; set; }
            public string NiveauDecaissement { get; set; }
            public decimal Montant { get; set; }
            public decimal Encaissé { get; set; }
            public decimal Restant { get; set; }
            public int Ordre { get; set; }
    }

    public class EcheanceSimule
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime Date { get; set; }
        public string LibelleEcheance { get; set; }
        public decimal Montant { get; set; }


    }


}
