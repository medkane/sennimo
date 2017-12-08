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
using System.Globalization;
using prjSenImmoWinform.Models;

namespace prjSenImmoWinform
{
    public partial class FrmDetailsIlot : Form
    {
        Ilot ilotEnCours;
        private IlotRepository ilotRepository;
        private ContratRepository contratRep;
        private Lot leLotEnCours;
        private bool bSaisieEtatAvancement;
        private string p;
        private bool bOptionProspect;
        private bool bSectionLot;
        private Client LeProspectEnCours;
        private bool bInitialLot;
        private IEnumerable<Lot> lesLots;
        private bool bTravaux;
        private Lot LeLotChoisi;
        private TypeConstruction leTypeConstructionEnCours;
       // private Projet leProjetEnCours;
        public Projet leProjetEnCours { get; set; }

        public Form FormAppelant { get; set; }

        public FrmDetailsIlot()
        {
            InitializeComponent();
            ilotRepository = new IlotRepository();
            contratRep = new ContratRepository();
            leProjetEnCours = Tools.Tools.ProjetEnCours;
            if (leProjetEnCours.DenominationProjet == "KERRIA")
            {
                leTypeConstructionEnCours = TypeConstruction.Appartement;
                rbImmeuble.Checked = true;
                pTypeConstruction.Visible = true;
            }
            else
            {
                leTypeConstructionEnCours = TypeConstruction.Villa;
                rbIlot.Checked = true;
                pTypeConstruction.Visible = false;
            }
            ChargerLesProjets();
            ChargerStatuts();
            ChargerPositions();
            //ChargerTypeConstructions();
            ChargerTypeVillas(leProjetEnCours, leTypeConstructionEnCours);
            ChargerNiveauAvancements();
            ChargerLesSuperficieStandards(leProjetEnCours, leTypeConstructionEnCours);
            cmdEditerContrat.Visible = false;
            cmdModifier.Visible = false;
        }

        public FrmDetailsIlot(Projet leProjet) : this()
        {
            txtNumeroIlot.ReadOnly = false;
            //cmbStatuts.Visible = false;
            //lbStatut.Visible = false;
            this.Width = 749;
            splitContainer1.SplitterDistance = 747;
            cmdAjouterEtatAvancementGeneral.Visible = false;
            pHistoriqueTravaux.Visible = false;
            cmdAjouterLot.Visible = false;
            pAjoutEtatAvancementIndividuel.Visible = false;
            bSectionLot = true;
            cmdSelectionnerLot.Visible = true;
            cmdEditerContrat.Visible = false;
            leProjetEnCours = leProjet;
            cmbProjets.SelectedValue = leProjetEnCours.Id;
            cmbProjets.Enabled = false;
        }

        public FrmDetailsIlot(Projet leProjet, TypeConstruction typeConstruction) : this()
        {
            txtNumeroIlot.ReadOnly = false;
            //cmbStatuts.Visible = false;
            //lbStatut.Visible = false;
            this.Width = 749;
            splitContainer1.SplitterDistance = 747;
            cmdAjouterEtatAvancementGeneral.Visible = false;
            pHistoriqueTravaux.Visible = false;
            cmdAjouterLot.Visible = false;
            pAjoutEtatAvancementIndividuel.Visible = false;
            bSectionLot = true;
            cmdSelectionnerLot.Visible = true;
            cmdEditerContrat.Visible = false;
            leProjetEnCours = leProjet;
            cmbProjets.SelectedValue = leProjetEnCours.Id;
            cmbProjets.Enabled = false;
            leTypeConstructionEnCours = typeConstruction;
            if(leProjet.DenominationProjet.Trim().ToUpper()=="KERRIA")
            { 
                if (typeConstruction == TypeConstruction.Appartement)
                    rbImmeuble.Checked = true;
                else
                    rbIlot.Checked = true;
                pTypeConstruction.Enabled = false;
            }

        }

        public FrmDetailsIlot(string action)
            : this()
        {
            try
            {
                switch (action)
                {
                    case "SelectionLot":
                        txtNumeroIlot.ReadOnly = false;
                        //cmbStatuts.Visible = false;
                        //lbStatut.Visible = false;
                        this.Width = 749;
                        splitContainer1.SplitterDistance = 747;
                        cmdAjouterEtatAvancementGeneral.Visible = false;
                        pHistoriqueTravaux.Visible = false;
                        cmdAjouterLot.Visible = false;
                        pAjoutEtatAvancementIndividuel.Visible = false;
                        bSectionLot = true;
                        cmdSelectionnerLot.Visible = true;
                        cmdEditerContrat.Visible = false;
                        break;
                    case "Initial":
                        txtNumeroIlot.ReadOnly = false;
                        //cmbStatuts.Visible = false;
                        //lbStatut.Visible = false;
                        cmdAjouterEtatAvancementGeneral.Visible = false;
                        cmdAjouterLot.Visible = true;
                        pAjoutEtatAvancementIndividuel.Visible = false;
                        bInitialLot = true;
                        cmdSelectionnerLot.Visible = false;

                        pHistoriqueTravaux.Visible = false;
                        cmdModifier.Visible = true;
                        
                        cmdEditerContrat.Visible = false;

                        break;
                    case "Travaux":
                        txtNumeroIlot.ReadOnly = false;
                        cmbStatuts.Visible = false;
                        lbStatut.Visible = false;
                        cmdAjouterEtatAvancementGeneral.Visible = true;
                        cmdAjouterLot.Visible = false;
                        pAjoutEtatAvancementIndividuel.Visible = false;
                        bTravaux = true;
                        cmdSelectionnerLot.Visible = false;
                        cmdEditerContrat.Visible = false;

                        break;
                    case "RechercherLot":
                        txtNumeroIlot.ReadOnly = false;
                        //cmbStatuts.Visible = false;
                        //lbStatut.Visible = false;
                        cmdAjouterEtatAvancementGeneral.Visible = false;
                        cmdAjouterLot.Visible = false;
                        pAjoutEtatAvancementIndividuel.Visible = false;
                        bInitialLot = true;
                        cmdSelectionnerLot.Visible = false;

                        pHistoriqueTravaux.Visible = false;
                        cmdModifier.Visible = false;
                        cmdEditerContrat.Visible = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:" + ex.Message,
                          "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public FrmDetailsIlot(Ilot ilot):this()
        {
            try
            {

                ilotEnCours = ilot;
                AfficherDetailsIlots(ilotEnCours);
                ChargerLesLots(ilotRepository.GetLots(ilotEnCours.Id).ToList());
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(this, "Erreur:"+ex.Message,
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        public FrmDetailsIlot(Client prospect):this()
        {
            try
            {
                txtNumeroIlot.ReadOnly = false;
                cmbStatuts.Visible = false;
                lbStatut.Visible = false;
                bOptionProspect = true;
                cmdAjouterEtatAvancementGeneral.Visible = false;
                cmdAjouterLot.Visible = false;
                pAjoutEtatAvancementIndividuel.Visible = false;
                LeProspectEnCours = prospect;
               
                pHistoriqueTravaux.Visible = false;
                pActionSurLot.Visible = false;

                bSectionLot = false;
              
                cmdSelectionnerLot.Visible = false;
                cmdEditerContrat.Visible = false;
                ////ilotEnCours = ilot;
                //AfficherDetailsIlots(ilotEnCours);
                //ChargerLesLots(ilotRepository.GetLots(ilotEnCours.Id).ToList());
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:" + ex.Message,
                          "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" || Tools.Tools.AgentEnCours.Role.CodeRole == "DC")
                {
                    cmbProjets.SelectedValue = Tools.Tools.AgentEnCours.ProjetId;
                    //LeProjetEncours = Tools.Tools.AgentEnCours.Projet;
                    cmbProjets.Enabled = false;
             
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ChargerPositions()
        {
            try
            {
                cmbPositions.DataSource = Enum.GetValues(typeof(PositionLot));
                cmbPositions.SelectedIndex = -1;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //private void ChargerTypeConstructions()
        //{
        //    try
        //    {
        //        cmbTypeContructions.DataSource = Enum.GetValues(typeof(TypeConstruction));
        //        cmbTypeContructions.SelectedIndex = -1;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        private void ChargerEtages()
        {
            try
            {
                cmbEtages.DataSource = Enum.GetValues(typeof(NiveauAppartement));
                cmbEtages.SelectedIndex = -1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ChargerStatuts()
        {
            try
            {
                cmbStatuts.DataSource = Enum.GetValues(typeof(StatutLot));
                cmbStatuts.SelectedIndex = -1;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void ChargerNiveauAvancements()
        {
            try
            {
                cmbNiveauxAvancements.DataSource = ilotRepository.GetNiveauxAvancements(leProjetEnCours.Id, leTypeConstructionEnCours).ToList();
                cmbNiveauxAvancements.DisplayMember = "Description";
                cmbNiveauxAvancements.ValueMember = "ID";
                cmbNiveauxAvancements.SelectedIndex = -1;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void ChargerLesSuperficieStandards(Projet prj,TypeConstruction tc)
        {
            try
            {
                cmbSuperficie.DataSource = ilotRepository.GetTypeVillas().Where(tv =>tv.ProjetId==prj.Id && tv.TypeConstruction==tc).Select(tv =>tv.SurfaceDeBase.ToString("###")).Distinct().ToList();
                cmbSuperficie.DisplayMember = "SurfaceDeBase";
              
                cmbSuperficie.SelectedIndex = -1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ChargerTypeVillas(Projet prj,TypeConstruction leTypeConstructionEnCours)
        {
            try
            {
                cmbTypeVillas.DataSource = ilotRepository.GetTypeVillas().Where(tv =>tv.ProjetId==prj.Id && tv.TypeConstruction==leTypeConstructionEnCours).ToList();
                cmbTypeVillas.DisplayMember = "NomComplet";
                cmbTypeVillas.ValueMember = "TypeVillaID";
                cmbTypeVillas.SelectedIndex = -1;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void ChargerTypeImmeuble()
        {
            try
            {
                cmbTypeImmeubles.DataSource = ilotRepository.GetTypeImmeubles().Where(ti => ti.CodeTypeImmeuble.Trim().ToUpper()!="VLA").ToList();
                cmbTypeImmeubles.DisplayMember = "LibelleTypeImmeuble";
                cmbTypeImmeubles.ValueMember = "Id";
                cmbTypeImmeubles.SelectedIndex = -1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ChargerTypeVillasImmeuble(TypeImmeuble ti)
        {
            try
            {
                cmbTypeVillas.DataSource = ilotRepository.GetTypeVillas().Where(tv => tv.TypeImmeubleId==ti.Id).ToList();
                cmbTypeVillas.DisplayMember = "NomComplet";
                cmbTypeVillas.ValueMember = "TypeVillaID";
                cmbTypeVillas.SelectedIndex = -1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ChargerLesLots(List<Lot> lotsRetrouves)
        {
            try
            {
                dgLots.DataSource = lotsRetrouves.Select
                                                        (l => new
                                                        {
                                                            ID = l.ID,
                                                            Numéro = l.NumeroLot,
                                                            Type = l.TypeVilla.NomComplet,
                                                            Supeficie = l.Superficie,
                                                            Position = l.TypeVilla.TypeConstruction == TypeConstruction.Villa ? l.PositionLot.ToString() : l.NiveauAppartement.ToString(),
                                                            Prix = l.PrixRevise != 0 ? l.PrixRevise : l.TypeVilla.PrixStandard,
                                                            Statut = l.StatutLot.ToString(),
                                                            EtatAvancement = l.EtatsAvancements.Where(ea => ea.TypeEtatAvancement.NiveauTechnique == true && ea.Actif == true).Count() > 0 ?
                                                                        l.EtatsAvancements.Where(ea => ea.TypeEtatAvancement.NiveauTechnique == true && ea.Actif == true).OrderByDescending(na => na.TypeEtatAvancement.ordre).
                                                                        FirstOrDefault().
                                                                        TypeEtatAvancement.Description
                                                                        : null,
                                                            Ilot= l.Ilot.NomIlot,
                                                            Livraison =l.Ilot.DateFinLivraison.HasValue?l.Ilot.DateFinLivraison.Value: (DateTime?)null
                                                        }).ToList();
                dgLots.Columns[0].Width = 0;
                dgLots.Columns[0].Visible = false;
                dgLots.Columns[1].Width = 70;
                dgLots.Columns[1].HeaderText = "N° Lot";
                dgLots.Columns[2].Width = 100;
                dgLots.Columns[2].HeaderText = "Type";
                dgLots.Columns[3].Width = 60;
                dgLots.Columns[3].HeaderText = "Superficie";
                //dgLots.Columns[3].DefaultCellStyle.Format = "###,##";
                dgLots.Columns[4].Width = 70;
                if(leTypeConstructionEnCours== TypeConstruction.Villa)
                    dgLots.Columns[4].HeaderText = "Position";
                else if(leTypeConstructionEnCours==TypeConstruction.Appartement)
                    dgLots.Columns[4].HeaderText = "Etage";
                dgLots.Columns[5].Width = 70;
                dgLots.Columns[5].HeaderText = "Prix";
                dgLots.Columns[5].DefaultCellStyle.Format = "### ### ###";
                dgLots.Columns[6].Width = 100;
                dgLots.Columns[6].HeaderText = "Statut";
                dgLots.Columns[7].Width = 160;
                dgLots.Columns[7].HeaderText = "Etat avancement";
                dgLots.Columns[8].Width = 80;
                dgLots.Columns[8].HeaderText = "Ilot/Imm";
                dgLots.Columns[9].Width = 80;
                txtNbLotsTrouves.Text = lotsRetrouves.Count.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AfficherDetailsIlots(Ilot ilot)
        {
            try
            {
                //txtNumeroIlot.Text = ilot.NomIlot;
                txtSuperficie.Text = ilot.Superficie.ToString();
                if (ilot.StatutOuverture)
                {
                    chkOuvert.Checked = true;
                    dtpOuverture.Value = ilot.DateOuvertureEffective.Value;
                }
                else
                {
                    chkOuvert.Checked = false;
                    dtpOuverture.Value = DateTime.Parse("01/01/1900");
                }
                //txtCommentaireIlot.Text=ilot.C
                txtNbTotalLots.Text = ilot.Lots.Where(l =>l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive).Count().ToString();
                txtNbTotalLotsLibres.Text = ilot.Lots.Where(l => l.StatutLot == StatutLot.Libre && l.LotVirtuel== false && l.StatutLot != StatutLot.Desactive).Count().ToString();
                txtNbTotalLotsEnOption.Text = ilot.Lots.Where(l => l.StatutLot == StatutLot.Option && l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive).Count().ToString();
               // txtNbTotalLotsReservationEnCours.Text = ilot.Lots.Where(l => l.StatutLot == StatutLot.ReservationEnCours && l.LotVirtuel == false).Count().ToString();
                txtNbTotalLotsReserves.Text = ilot.Lots.Where(l => l.StatutLot == StatutLot.Reserve && l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive).Count().ToString();
                txtNbTotalLotsVendus.Text = ilot.Lots.Where(l => l.StatutLot == StatutLot.Vendu && l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive).Count().ToString();
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        private void cmdRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                //var queryLots = ilotEnCours.Lots.AsEnumerable();
                this.Cursor = Cursors.WaitCursor;
                int idIlot = 0;
                if (txtNumeroIlot.Text != string.Empty)
                    idIlot = ilotEnCours.Id;


                var queryLots = ilotRepository.GetLotsParTypeConstruction(leProjetEnCours, idIlot, leTypeConstructionEnCours).ToList();
                if(leTypeConstructionEnCours== TypeConstruction.Appartement)
                    if (cmbEtages.SelectedItem != null)
                    {
                        queryLots = queryLots.Where(l => l.NiveauAppartement == ((NiveauAppartement)cmbEtages.SelectedItem)).ToList();

                    }
                if(cmbIlots.SelectedItem != null)
                {
                    queryLots = queryLots.Where(l => l.IlotID==((Ilot)cmbIlots.SelectedItem).Id).ToList();

                }
                //queryLots = queryLots.Where(l => l.TypeVilla.TypeConstruction == leTypeConstructionEnCours);
                if (cmbTypeImmeubles.SelectedItem != null)
                { 
                    queryLots = queryLots.Where(l => l.TypeVilla.TypeImmeubleId == ((TypeImmeuble)cmbTypeImmeubles.SelectedItem).Id).ToList();
                    
                }
                if (txtNumeroLotRecherche.Text != string.Empty)
                    queryLots = queryLots.Where(l => l.NumeroLot.ToLower()== txtNumeroLotRecherche.Text.ToLower()).ToList();
                if (bOptionProspect || bSectionLot)
                {
                    queryLots = queryLots.Where(l => l.StatutLot == StatutLot.Libre).ToList();
                }
                else
                {
                    if (cmbStatuts.SelectedItem != null)
                        queryLots = queryLots.Where(l => l.StatutLot == (StatutLot)cmbStatuts.SelectedItem).ToList(); ;
                }
                 if (cmbPositions.SelectedItem != null)
                    queryLots = queryLots.Where(l => l.PositionLot == (PositionLot)cmbPositions.SelectedItem).ToList(); ;
                if (cmbTypeVillas.SelectedItem != null)
                    queryLots = queryLots.Where(l => l.TypeVillaID == ((TypeVilla)cmbTypeVillas.SelectedItem).TypeVillaId).ToList(); ;

                //if (cmbTypeContructions.SelectedItem != null)
                //    queryLots = queryLots.ToList().Where(l => l.Ilot.TypeConstruction == ((TypeConstruction)cmbTypeContructions.SelectedItem));

                if (cmbSuperficie.SelectedItem != null)
                    queryLots = queryLots.Where(l => l.TypeVilla.SurfaceDeBase == decimal.Parse(cmbSuperficie.Text)).ToList(); ;

                var query=queryLots.ToList();
                ChargerLesLots(query);
                ChargerLesSuperficieStandards(leProjetEnCours, leTypeConstructionEnCours);

                txtNbTotalLots.Text = queryLots.Where(l => l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive).Count().ToString();
                txtNbTotalLotsLibres.Text = queryLots.Where(l => l.StatutLot == StatutLot.Libre && l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive).Count().ToString();
                txtNbTotalLotsEnOption.Text = queryLots.Where(l => l.StatutLot == StatutLot.Option && l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive).Count().ToString();
                // txtNbTotalLotsReservationEnCours.Text = ilot.Lots.Where(l => l.StatutLot == StatutLot.ReservationEnCours && l.LotVirtuel == false).Count().ToString();
                txtNbTotalLotsReserves.Text = queryLots.Where(l => l.StatutLot == StatutLot.Reserve && l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive).Count().ToString();
                txtNbTotalLotsVendus.Text = queryLots.Where(l => l.StatutLot == StatutLot.Vendu && l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive).Count().ToString();
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(this, "Erreur:"+ex.Message,
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtNumeroLotRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdRechercher_Click(sender, new EventArgs());
            }
        }

        private void dgLots_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgLots.SelectedRows.Count > 0)
                {
                    if (dgLots.SelectedRows[0].Cells[0].Value != null)
                    {
                        var lotVal = dgLots.SelectedRows[0].Cells[0].Value;
                        int idLot = (int)lotVal;
                        leLotEnCours = ilotRepository.FindLotById(idLot);

                        Afficherlot(leLotEnCours);
                        if (leLotEnCours.StatutLot == StatutLot.Reserve)
                            cmdModifier.Enabled = false;
                        else
                            cmdModifier.Enabled = true;
                        //cmdEditerContrat.Visible = true;

                        //VerouillerIlot();

                        bSaisieEtatAvancement = false;
                        cmdAjouterEtatAvancementGeneral.Text = "Ajouter états d'avancement";
                        cmdAjouterEtatAvancementGeneral.BackColor = Control.DefaultBackColor;
                        pEtatAvancement.Visible = false;
                        pDateEtatAvancementIndividuel.Visible = true;
                        pAjoutEtatAvancementIndividuel.Visible = false;
                    }
                    else
                    {
                        if(bOptionProspect || bSectionLot)
                        pActionSurLot.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(this, "Erreur:"+ex.Message,
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Afficherlot(Lot lot)
        {
            try
            {
                txtIlot.Text = lot.Ilot.NomIlot;
                txtNumeroLot.Text = lot.NumeroLot;
                txtTypeVilla.Text = lot.TypeVilla.NomType;
                txtSuperficieDeBase.Text = lot.TypeVilla.SurfaceDeBase.ToString();
                txtSuperficieReelle.Text = lot.Superficie.ToString();
                txtPosition.Text = lot.PositionLot.ToString();
                txtPrixStandard.Text = lot.TypeVilla.PrixStandard.ToString("0,0", CultureInfo.InvariantCulture);

                txtPrixRevise.Text = lot.PrixRevise.ToString("0,0", CultureInfo.InvariantCulture);
                txtStatut.Text = lot.StatutLot.ToString();
                txtChambre.Text = lot.TypeVilla.Chambre.ToString();
                txtChambreAvecSDB.Text = lot.TypeVilla.ChambreAvecSalleDeBain.ToString();
                txtSalon.Text = lot.TypeVilla.Salon.ToString();
                txtCuisine.Text = lot.TypeVilla.Cuisine.ToString();
                txtToilette.Text = lot.TypeVilla.Toilette.ToString();
                txtPatio.Text = lot.TypeVilla.Patio.ToString();
                txtCoursArriere.Text = lot.TypeVilla.CourArriere.ToString();
                ChargerNiveauxAvancementLot(lot);
                ChargerEtatAvancementLot(lot);
                pActionSurLot.Visible = bOptionProspect? true:false;
                cmdSelectionnerLot.Visible =bSectionLot? true:false;
            //    if (ilot.StatutOuverture)
            //    {
            //        chkOuvert.Checked = true;
            //        dtpOuverture.Value = ilot.DateOuvertureEffective.Value;
            //    }
            //    else
            //    {
            //        chkOuvert.Checked = false;
            //        dtpOuverture.Value = DateTime.Parse("01/01/1900");
            //    }
            //    txtCommentaireIlot.Text=ilot.C
            //    txtNbTotalLots.Text = ilot.Lots.Count().ToString();
            //    txtNbTotalLotsLibres.Text = ilot.Lots.Where(l => l.StatutLot == StatutLot.Libre).Count().ToString();
            //    txtNbTotalLotsEnOption.Text = ilot.Lots.Where(l => l.StatutLot == StatutLot.Option).Count().ToString();
            //    txtNbTotalLotsReservationEnCours.Text = ilot.Lots.Where(l => l.StatutLot == StatutLot.ReservationEnCours).Count().ToString();
            //    txtNbTotalLotsReserves.Text = ilot.Lots.Where(l => l.StatutLot == StatutLot.Reserve).Count().ToString();
            //    txtNbTotalLotsVendus.Text = ilot.Lots.Where(l => l.StatutLot == StatutLot.Vendu).Count().ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EffacerLot()
        {
            txtIlot.Text =string.Empty;
            txtNumeroLot.Text = string.Empty;
            txtTypeVilla.Text = string.Empty;
            txtSuperficieDeBase.Text = string.Empty;
            txtSuperficieReelle.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtPrixStandard.Text = string.Empty;

            txtPrixRevise.Text = string.Empty;
            txtStatut.Text = string.Empty;
            txtChambre.Text = string.Empty;
            txtChambreAvecSDB.Text = string.Empty;
            txtSalon.Text = string.Empty;
            txtCuisine.Text = string.Empty;
            txtToilette.Text = string.Empty;
            txtPatio.Text = string.Empty;
            txtCoursArriere.Text = string.Empty;
            //ChargerNiveauxAvancementLot(lot);
            //ChargerEtatAvancementLot(lot);
            //pActionSurLot.Visible = bOptionProspect ? true : false;
            //cmdSelectionnerLot.Visible = bSectionLot ? true : false;
        }

        private void ChargerNiveauxAvancementLot(Lot lot)
        {
            try
            {
                if (lot.Contrats.Count == 0)
                {
                    return;
                }
                if (lot.StatutLot == StatutLot.Reserve || lot.StatutLot == StatutLot.Vendu)
                {
                    var leContrat = ilotRepository.GetContratsLot(lot);
                    if (leContrat != null)
                    {
                        var lesDifferentsNiveauAvancements = ilotRepository.GetNiveauxAvancements(leProjetEnCours.Id, leTypeConstructionEnCours, leContrat.TypeContrat);
                        //var lesDifferentsNiveauAvancements = ilotRepository.GetNiveauxAvancements(leProjetEnCours.Id, leTypeConstructionEnCours,leTy);
                        //if (lot.EtatsAvancements.Where(ea => ea.Actif==true).Count() > 0)
                        //{
                        //    lastNiveauAvancement = lot.EtatsAvancements.Where(ea => ea.Actif == true).Max(ea => ea.TypeEtatAvancement.ordre );
                        //}

                        cmbNiveauxAvancements.DataSource = lesDifferentsNiveauAvancements.ToList();
                        //.Where
                        //(na => na.ordre > lastNiveauAvancement).ToList();
                        cmbNiveauxAvancements.DisplayMember = "Description";
                        cmbNiveauxAvancements.ValueMember = "ID";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:"+ex.Message,
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerEtatAvancementLot(Lot lot)
        {
            try
            {
                dgEtatAvancements.DataSource = lot.EtatsAvancements.ToList().Where(ea => ea.TypeEtatAvancement.NiveauTechnique == true && ea.DateSaisieAvancement!=null).Select
                                                                                 (ea => new
                                                                                 {
                                                                                     ID = ea.ID,
                                                                                     Date = ea.DateSaisieAvancement,
                                                                                     Type = ea.TypeEtatAvancement.Description

                                                                                 }).ToList();

                dgEtatAvancements.Columns[0].Width = 0;
                dgEtatAvancements.Columns[1].Width = 80;
                dgEtatAvancements.Columns[0].Visible = false;
                dgEtatAvancements.Columns[2].Width = 350;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:"+ex.Message,
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdRAZ_Click(object sender, EventArgs e)
        {
            txtNumeroLotRecherche.Text = string.Empty;
            cmbTypeVillas.SelectedIndex = -1;
            cmbPositions.SelectedIndex = -1;
            cmbStatuts.SelectedIndex = -1;
            cmbSuperficie.SelectedIndex = -1;
            cmdRechercher_Click(sender, e);
        }

        private void cmdAjouterLot_Click(object sender, EventArgs e)
        {
            try
            {
                if(ilotEnCours==null )
                { 
                    MessageBox.Show(this, "Erreur: veuillez d'abord selectionner l'ilôt concerné",
                        "Prosopis - Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FrmLot frmLot = new FrmLot(ilotEnCours,leTypeConstructionEnCours);
                //frmLot.MdiParent = this.MdiParent;
                frmLot.WindowState = FormWindowState.Normal;
                frmLot.StartPosition = FormStartPosition.CenterParent;
                frmLot.ShowDialog();
                this.ChargerLesLots(ilotRepository.GetLots(ilotEnCours.Id).ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:"+ex.Message,
                         "Prosopis - Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void cmdAjouterEtatAvancementGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                //
               
                if (bSaisieEtatAvancement == false)
                {
                    if (leLotEnCours.Contrats.Count == 0)
                    {
                        MessageBox.Show("Ce lot ne fait pas encore l'objet d'un contrat");
                        return;
                    }
                    bSaisieEtatAvancement = true;
                    cmdAjouterEtatAvancementGeneral.Text = "Arrêter la saisie des états d'avancements";
                    cmdAjouterEtatAvancementGeneral.BackColor = Color.LimeGreen;
                    pEtatAvancement.Visible = true;
                    pDateEtatAvancementIndividuel.Visible = true;
                    pAjoutEtatAvancementIndividuel.Visible = true;
                    splitContainer1.Panel2Collapsed = false;
                    splitContainer1.Panel2.Show();

                }
                else
                {
                    bSaisieEtatAvancement = false;
                    cmdAjouterEtatAvancementGeneral.Text = "Ajouter états d'avancement";
                    cmdAjouterEtatAvancementGeneral.BackColor = Control.DefaultBackColor;
                    pEtatAvancement.Visible = false;
                    pDateEtatAvancementIndividuel.Visible = true;
                    pAjoutEtatAvancementIndividuel.Visible = false;
                    splitContainer1.Panel2Collapsed = true;
                    splitContainer1.Panel2.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void cmdAjouterEtatAvencementIndividuel_Click(object sender, EventArgs e)
        {
            try
            {
                if (bSaisieEtatAvancement)
                {
                    AjouterEtatAvancement();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                        "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjouterEtatAvancement()
        {
            try
            {
                if (dgLots.SelectedRows.Count>0)
                {
                    int rowEncours = dgLots.SelectedRows[0].Index;
                    StatutEtatAvancement statut;
                    if (cmbNiveauxAvancements.SelectedItem != null)
                    {
                       
                        var niveauAvancement = (TypeEtatAvancement)cmbNiveauxAvancements.SelectedItem;
                        ilotRepository.AddEtatAvancement(leLotEnCours, niveauAvancement, dtpDateEtatAvancementIndividuel.Value.Date,txtCommentaires.Text);
                        ChargerEtatAvancementLot(leLotEnCours);
                        ChargerNiveauxAvancementLot(leLotEnCours);
                        if(ilotEnCours!=null)
                            ChargerLesLots(ilotRepository.GetLots(ilotEnCours.Id).ToList());
                        dgLots.Rows[0].Selected = false;
                        dgLots.CurrentCell = dgLots.Rows[rowEncours].Cells[1];
                        dgLots.Rows[rowEncours].Selected = true; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                      "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpDateEtatAvancementIlot_ValueChanged(object sender, EventArgs e)
        {
            dtpDateEtatAvancementIndividuel.Value = dtpDateEtatAvancementIlot.Value;
            
        }

        private void dgLots_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (bSaisieEtatAvancement)
                    {
                        AjouterEtatAvancement();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:"+ex.Message,
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNumeroIlot_Validated(object sender, EventArgs e)
        {
            if (!bOptionProspect && !bSectionLot && !bInitialLot && !bTravaux)
            {
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (txtNumeroIlot.Text != string.Empty)
                {
                    if (txtNumeroIlot.Text.Trim().ToLower()!="%")
                    {

                        var strNumeroIlot = txtNumeroIlot.Text;
                        var ilot = ilotRepository.FindIlotByName(strNumeroIlot,leProjetEnCours ,leTypeConstructionEnCours);
                        if (ilot != null)
                        {
                            ilotEnCours = ilot;
                            AfficherDetailsIlots(ilotEnCours);
                            lesLots = ilotRepository.GetLots(ilotEnCours.Id);

                            if (bSectionLot || bOptionProspect)
                                lesLots = lesLots.Where(l => l.StatutLot == StatutLot.Libre);

                            ChargerLesLots(lesLots.ToList()); 
                        }
                    }
                    else
                    {
                        EffacerForm();
                        ChargerLesLots(ilotRepository.GetAllLots().ToList());
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        public void EffacerForm()
        {
            txtNumeroIlot.Text = string.Empty;
            txtSuperficie.Text = string.Empty;
            //if (ilot.StatutOuverture)
            //{
            //    chkOuvert.Checked = true;
            //    dtpOuverture.Value = ilot.DateOuvertureEffective.Value;
            //}
            //else
            //{
            //    chkOuvert.Checked = false;
            //    dtpOuverture.Value = DateTime.Parse("01/01/1900");
            //}
            //txtCommentaireIlot.Text=ilot.C
            txtNbTotalLots.Text = string.Empty;
            txtNbTotalLotsLibres.Text = string.Empty;
            txtNbTotalLotsEnOption.Text = string.Empty;
            txtNbTotalLotsReserves.Text = string.Empty;
            txtNbTotalLotsVendus.Text = string.Empty;
            txtNbLotsTrouves.Text = string.Empty;
            dgLots.DataSource = null;
        }
        private void cmdPrendreEnOption_Click(object sender, EventArgs e)
        {
            if (bOptionProspect)
            {
                FrmOption frmOpt = new FrmOption(LeProspectEnCours,leLotEnCours);
                //frmOpt.MdiParent = this.MdiParent;
                frmOpt.StartPosition = FormStartPosition.CenterParent;
                frmOpt.WindowState = FormWindowState.Normal;
                frmOpt.ShowDialog();
                this.Close();
            }
        }

        private void cmdSelectionnerLot_Click(object sender, EventArgs e)
        {
           LeLotChoisi= leLotEnCours;
            this.Close();
        }

        public Lot GetLotSelectionne()
        {
            try
            {
                return LeLotChoisi;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtNumeroLot_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrixRevise_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrixStandard_TextChanged(object sender, EventArgs e)
        {

        }

        private void pDateEtatAvancementIndividuel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pAjoutEtatAvancementIndividuel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void txtCoursArriere_TextChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void txtPatio_TextChanged(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void txtToilette_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtCuisine_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void txtSalon_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void txtChambreAvecSDB_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void txtChambre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void txtStatut_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dgEtatAvancements_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSuperficieDeBase_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPosition_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtSuperficieReelle_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtTypeVilla_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void txtNbLotsTrouves_TextChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void dgLots_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmDetailsIlot_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel2.Hide();
        }

        private void cmbNiveauxAvancements_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdEditerContrat_Click(object sender, EventArgs e)
        {
            if(dgLots.SelectedRows.Count > 0)
            try
            {
                if ( leLotEnCours.StatutLot == StatutLot.Reserve || leLotEnCours.StatutLot == StatutLot.Vendu)
                {
                    var leContrat = ilotRepository.GetContratsLot(leLotEnCours);
                    if(leContrat!=null)
                    {
                            this.Cursor = Cursors.WaitCursor;
                        FrmDossierClient childForm = new FrmDossierClient(leContrat);
                        childForm.MdiParent = this.MdiParent;
                        childForm.Show();
                        childForm.WindowState = FormWindowState.Maximized;
                            this.Cursor = Cursors.Default;
                        }
                }
                else
                {
                    MessageBox.Show(this, "Désolé ce lot n'est pas lié à un contrat!",
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(this, "Veuillez d'abord sélectionner le contrat à afficher",
                        "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dgLots_DoubleClick(object sender, EventArgs e)
        {
            //if(bSectionLot)
            //cmdSelectionnerLot_Click(sender, e);
        }

        private void cmdSupprimer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Voulez vous réellement supprimer cet état d'avancement", "Gestion des travaux", MessageBoxButtons.YesNo, MessageBoxIcon.Question )== DialogResult.Yes)
            {

                try
                {
                    if (dgEtatAvancements.SelectedRows.Count > 0)
                    {
                        int etatAvancementId = (int)dgEtatAvancements.SelectedRows[0].Cells[0].Value;
                        ilotRepository.DeteteEtatAvancement(etatAvancementId);
                       // leLotEnCours = ilotRepository.FindLotById(idLot);

                        Afficherlot(leLotEnCours);
                        //ChargerLesLots(ilotRepository.GetLots(ilotEnCours.Id).ToList());
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(this, "Erreur:" + ex.Message,
                             "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //dgEtatAvancements.
                //ilotRepository.AddEtatAvancement(leLotEnCours, niveauAvancement, dtpDateEtatAvancementIndividuel.Value.Date, txtCommentaires.Text);
            }
        }

        private void cmdModifier_Click(object sender, EventArgs e)
        {
            if (leLotEnCours != null)
            {
                this.Cursor = Cursors.WaitCursor;
                FrmLot childForm = new FrmLot(leLotEnCours);
                //childForm.MdiParent = this.MdiParent;
                childForm.WindowState = FormWindowState.Normal;
                childForm.ShowDialog();
                cmdRechercher_Click( sender,  e);
                this.Cursor = Cursors.Default;
            }
        
        }

        private void txtNumeroIlot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                 txtNumeroIlot_Validated( sender,  e);
        }

        private void cmbSuperficie_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbStatuts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rbIlot_CheckedChanged(object sender, EventArgs e)
        {

            if (rbIlot.Checked)
            {
                leTypeConstructionEnCours = TypeConstruction.Villa;
                EffacerForm();
                EffacerLot();
                txtNumeroIlot.Text = "";
                pTypeImmeuble.Visible = false;
                //txtNumeroIlot_Validated(sender, e);  
                pEtage.Visible = false;
                ChargerTypeVillas(leProjetEnCours,leTypeConstructionEnCours);
                ChargerLesSuperficieStandards(leProjetEnCours, leTypeConstructionEnCours);
                cmbTypeImmeubles.SelectedIndex = -1;
                cmbTypeVillas.Enabled = true;
            }
            else
            {
                leTypeConstructionEnCours = TypeConstruction.Appartement;
                EffacerForm();
                EffacerLot();
                txtNumeroIlot.Text = "";
                pTypeImmeuble.Visible = true;
                ChargerTypeImmeuble();
                pEtage.Visible = true;
                ChargerEtages();
                // txtNumeroIlot_Validated(sender, e);
                ChargerTypeVillas(leProjetEnCours, leTypeConstructionEnCours);
                cmbTypeVillas.Enabled = true;

            }
            cmbIlots.SelectedIndex = -1;
            bSaisieEtatAvancement = false;
            cmdAjouterEtatAvancementGeneral.Text = "Ajouter états d'avancement";
            cmdAjouterEtatAvancementGeneral.BackColor = Control.DefaultBackColor;
            pEtatAvancement.Visible = false;
            pDateEtatAvancementIndividuel.Visible = true;
            pAjoutEtatAvancementIndividuel.Visible = false;

        }

        private void cmbProjets_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cmbProjets.SelectedItem != null)
                {
                    leProjetEnCours = (Projet)cmbProjets.SelectedItem;
                    if(leProjetEnCours==null)return;

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

                    ChargerLesIlots(leProjetEnCours.Id);

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
                bSaisieEtatAvancement = false;
                cmdAjouterEtatAvancementGeneral.Text = "Ajouter états d'avancement";
                cmdAjouterEtatAvancementGeneral.BackColor = Control.DefaultBackColor;
                pEtatAvancement.Visible = false;
                pDateEtatAvancementIndividuel.Visible = true;
                pAjoutEtatAvancementIndividuel.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                             "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerLesIlots(int idProjet)
        {
            try
            {
                var lesIlots = contratRep.GetIlots(idProjet);

                cmbIlots.DataSource = lesIlots.ToList();
                cmbIlots.DisplayMember = "NomIlot";
                cmbIlots.ValueMember = "Id";
                cmbIlots.SelectedIndex = -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void rbImmeuble_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbTypeImmeubles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTypeImmeubles.SelectedItem != null)
            {
                ChargerTypeVillasImmeuble((TypeImmeuble)cmbTypeImmeubles.SelectedItem);
                
                    cmbTypeVillas.Enabled = true;
            }
            else
            {
                cmbTypeVillas.SelectedIndex = -1;
                cmbTypeVillas.Enabled = false;
                if (leTypeConstructionEnCours == TypeConstruction.Villa)
                    cmbTypeVillas.Enabled = true;
            }
        }

        private void txtNumeroIlot_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbIlots_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bOptionProspect && !bSectionLot && !bInitialLot && !bTravaux)
            {
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                EffacerForm();
                EffacerLot();
                //if (txtNumeroIlot.Text != string.Empty)
                //{
                //    if (txtNumeroIlot.Text.Trim().ToLower() != "%")
                //    {

                //var strNumeroIlot = txtNumeroIlot.Text;
                //var ilot = ilotRepository.FindIlotByName(strNumeroIlot, leProjetEnCours, leTypeConstructionEnCours);
                if (cmbIlots.SelectedItem!=null)
                {
                    var ilot = (Ilot)cmbIlots.SelectedItem;
                    if (ilot != null)
                    {
                        ilotEnCours = ilot;
                        //AfficherDetailsIlots(ilotEnCours);
                        lesLots = ilotRepository.GetLots(ilotEnCours.Id);

                        if (bSectionLot || bOptionProspect)
                            lesLots = lesLots.Where(l => l.StatutLot == StatutLot.Libre);

                       // ChargerLesLots(lesLots.ToList());
                    } 
                }
                    //}
                    //else
                    //{
                    //    EffacerForm();
                    //    ChargerLesLots(ilotRepository.GetAllLots().ToList());
                    //}
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis -  Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
