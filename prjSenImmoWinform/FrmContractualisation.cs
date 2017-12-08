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
    public partial class FrmContractualisation : Form
    {
        DateTime premiereEcheance = DateTime.Now;
        DateTime derniereEcheance;

        public Lot LeLotChoisi { get; set; }
        public Client LeClientChoisi { get; set; }
        private decimal dPrixDeVente;
        private string strTypeContrat = "Sur échéancier";

        private SenImmoDataContext db;
        private bool bModifContrat;
        private decimal dMontantVerse;
        private decimal dMontantDepotDeGarantie;
        private decimal dMontantAvanceDemarrage;
        private decimal dMontantReliquat;
        private decimal dMontantRemise;
        private decimal dMontantCommission;
        private decimal dMontantFraisDossier;
        private bool bReservation;

        public FrmContractualisation()
        {
            InitializeComponent();
            db = new SenImmoDataContext();
            txtDateContrat.Text = DateTime.Today.ToShortDateString();
            ChargerLesApporteursAffaires();
            cmbTypeEcheanciers.DataSource = Enum.GetValues(typeof(TypeEcheancier));
            cmbTypeEcheanciers.SelectedIndex = -1;
        }


        public FrmContractualisation(Client client ):this()
        {
            LeClientChoisi = client;
            AfficherClient(client);
           
        }

        private void ChargerLesApporteursAffaires()
        {
            cmbApporteurAffaires.DataSource = (from aa in db.ApporteurAffaires
                                        select aa).ToList();
            cmbApporteurAffaires.DisplayMember = "NomComplet";
            cmbApporteurAffaires.ValueMember = "ID";
            cmbApporteurAffaires.SelectedIndex = -1;
        }

        private void AfficherClient(Client client)
        {
            txtPrenom.Text = client.Prenom;
            txtNom.Text = client.Nom;
            txtDateNaissance.Text = client.DateDeNaissance.Value.Date.ToShortDateString();
            txtLieuNaissance.Text = client.LieuDeNaissance;
            txtAdresse.Text = client.Adresse;
            txtNationalite.Text = client.Nationalite;
            txtTypePiece.Text = client.TypePieceIdentite.ToString();
            txtNumeroPiece.Text = client.NumeroPieceIdentification;
            txtDateDelivrance.Text = client.DateDeDelivrancePiece.Value.ToShortDateString();

        }

        private void cmdChoisirLot_Click(object sender, EventArgs e)
        {
            bool choixLot = true;
            FrmLot frmChild = new FrmLot(choixLot);
           
            frmChild.ShowDialog(this);
            if (this.MdiParent!=null && this.MdiParent.GetType() == typeof(FrmMenuGeneral))
            {
                LeLotChoisi = ((FrmMenuGeneral)this.MdiParent).LeLotChoisi;
            }
            
                
            AfficherLot(LeLotChoisi);
            chkRemise.Focus();
        }

        private void AfficherLot(Lot LeLotChoisi)
        {
            if (LeLotChoisi!=null)
            {
                txtIlot.Text = LeLotChoisi.Ilot.NomIlot;
                txtNumeroLot.Text = LeLotChoisi.NumeroLot;
                txtTypeVilla.Text = LeLotChoisi.TypeVilla.CodeType;
                txtSurface.Text = LeLotChoisi.Superficie.ToString();
                txtPosition.Text = LeLotChoisi.PositionLot.ToString();
                txtPrixStandard.Text = LeLotChoisi.TypeVilla.PrixStandard.ToString("### ### ###");
                txtPrixRevise.Text = LeLotChoisi.PrixRevise.ToString("### ### ###");
                txtNbChambre.Text = LeLotChoisi.TypeVilla.Chambre.ToString();
                txtNbChambreAvecSDB.Text = LeLotChoisi.TypeVilla.ChambreAvecSalleDeBain.ToString();
                txtNBSejour.Text = LeLotChoisi.TypeVilla.Salon.ToString();
                txtNbCuisine.Text = LeLotChoisi.TypeVilla.Cuisine.ToString();
                txtNbToilette.Text = LeLotChoisi.TypeVilla.Toilette.ToString();
                txtNbCoursArriere.Text = LeLotChoisi.TypeVilla.CourArriere.ToString();
                txtPrixVente.Text = (LeLotChoisi.PrixRevise != 0) ? LeLotChoisi.PrixRevise.ToString("### ### ###") : LeLotChoisi.TypeVilla.PrixStandard.ToString("### ### ###");
                dPrixDeVente = LeLotChoisi.PrixRevise;
            }
        }

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            if (LeLotChoisi == null || LeClientChoisi == null)
            {
                MessageBox.Show(this, "Veuillez choisir le client et le lot pour ce contrat",
                         "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!bModifContrat)
            {
                DateTime dDateContrat = DateTime.Parse("01/01/1900");
                txtDateContrat.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (txtDateContrat.Text != string.Empty)
                {
                    txtDateContrat.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    dDateContrat = DateTime.Parse(txtDateContrat.Text);
                }
                else
                {
                    return;
                }
                var typeContrat = db.TypeContrats.Where(tc => tc.LibelleTypeContrat == strTypeContrat).SingleOrDefault();
                
                //Vérifier que le montant versé couvre les 30%

                var newContrat = new Contrat()
                {
                   
                    ClientID = LeClientChoisi.ID,
                    LotId = LeLotChoisi.ID,
                    TypeContratID = typeContrat.ID,
                    PrixFinal = Decimal.Parse(dPrixDeVente.ToString()),
                    RemiseAccordee = Decimal.Parse(dMontantRemise.ToString()),
                    CommissionApporteur = Decimal.Parse(dMontantCommission.ToString()),
                    //Genre = txtAdresse.Text,
                    //CommercialID = txtNumeroFixe.Text,
                    MontantVerse = dMontantVerse,
                    DateLivraisonLot = DateTime.Parse(txtDateDelivrance.Text),
                    DateCreationSysteme = DateTime.Today,
                

                };
                //LeLotChoisi.StatutVilla=StatutVilla.Reserve;
                var laVilla = (from vil in db.Lots where vil.ID == LeLotChoisi.ID select vil).FirstOrDefault();
                var leClient = (from cl in db.Clients where cl.ID == LeClientChoisi.ID select cl).FirstOrDefault();
                if (bReservation)
                {
                    laVilla.StatutLot = StatutLot.Reserve;
                    leClient.Actif = true;
                }
                else
                {
                    //laVilla.StatutLot = StatutLot.ReservationEnCours;
                    leClient.Actif = false;
                }
                db.Contrats.Add(newContrat);
                //clientEnCours = newClient;

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
            MessageBox.Show(this, "Le contrat a été enregistré avec succes",
                        "Prosopis -  Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            //chargerClients();
            //EffacerVi();
        }

        private void FrmContractualisation_Load(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtMontantVerse_Validated(object sender, EventArgs e)
        {
                    if (txtMontantVerse.Text != string.Empty)
            {
                try
                {
                    txtMontantVerse.Text = double.Parse(txtMontantVerse.Text).ToString("### ### ###");
                    
                    dMontantFraisDossier = 200000;
                    dMontantDepotDeGarantie = dPrixDeVente * 5 / 100;
                    dMontantAvanceDemarrage = dPrixDeVente * 25 / 100;

                    dMontantVerse = decimal.Parse(txtMontantVerse.Text);
                   
                    decimal DMontantApresFraisDossier ;//= dMontantVerse - 200000;
                    decimal DMontantApresDepotGarantie;// = DMontantApresFraisDossier-(dPrixDeVente * 5 / 100);
                    decimal DMontantApresAvanceDemarrage;// = DMontantApresDepotGarantie - (dPrixDeVente * 25 / 100);

                    if ( dMontantVerse>= dMontantFraisDossier)
                    {
                        txtFraisDeDossier.Text = dMontantFraisDossier.ToString("### ### ###");
                        txtMontantReferenceFraisDeDossier.Text = dMontantFraisDossier.ToString("### ### ###");
                        DMontantApresFraisDossier = dMontantVerse - dMontantFraisDossier;

                        if (DMontantApresFraisDossier >= dMontantDepotDeGarantie)
                        {
                            
                            txtMontantDepotDeGarantie.Text = dMontantDepotDeGarantie.ToString("### ### ###");
                            txtMontantReferenceDepotDeGarantie.Text = dMontantDepotDeGarantie.ToString("### ### ###");
                            DMontantApresDepotGarantie=DMontantApresFraisDossier-dMontantDepotDeGarantie;


                            if (DMontantApresDepotGarantie >= dMontantAvanceDemarrage)
                            {
                                bReservation = true;
                                txtMontantAvance.Text = dMontantAvanceDemarrage.ToString("### ### ###");
                                txtMontantReferenceAvanceDeDemarrage.Text = dMontantAvanceDemarrage.ToString("### ### ###");
                                DMontantApresAvanceDemarrage = DMontantApresDepotGarantie - dMontantAvanceDemarrage;
                                txtReliquat.Text = DMontantApresAvanceDemarrage.ToString("### ### ###");
                            }
                            else
                            {
                                txtMontantAvance.Text = DMontantApresDepotGarantie.ToString("### ### ###");
                                txtMontantReferenceAvanceDeDemarrage.Text = dMontantAvanceDemarrage.ToString("### ### ###");
                                txtReliquat.Text = "0";
                            }
                            
                        }
                        else
                            {
                                txtMontantReferenceDepotDeGarantie.Text = dMontantDepotDeGarantie.ToString("### ### ###"); 
                                txtMontantDepotDeGarantie.Text = DMontantApresFraisDossier.ToString("### ### ###");
                            }

                    }
                    else
                    {

                    }
                    
                    
                    //txtMontantCommission.Text = (dMontantVerse * 5 / 100).ToString();
                    
                    //txtMontantDepotDeGarantie.Text = dMontantDepotDeGarantie.ToString("### ### ###");
                    //txtMontantAvance.Text = dMontantAvanceDemarrage.ToString("### ### ###");
                    //dMontantReliquat = dMontantVerse - (dMontantDepotDeGarantie + dMontantAvanceDemarrage);
                    //txtMontantReliquat.Text = dMontantReliquat.ToString("### ### ###");
                    if (dMontantReliquat == 0) txtMontantReliquat.Text = "0";
                    if (dMontantReliquat>=0) cmdGenererContrat.Visible = true;
                    txtDateDelivrance.Focus();
                }
                catch
                {
                    MessageBox.Show(this, "Vérifier le montant versé saisi",
                        "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void txtTauxRemise_Validated(object sender, EventArgs e)
        {
            if (txtTauxRemise.Text != string.Empty)
            {
                try
                {
                   
                    double dTauxRemise = double.Parse(txtTauxRemise.Text);
                    //txtMontantCommission.Text = (dMontantVerse * 5 / 100).ToString();
                    dMontantRemise = ((decimal)dTauxRemise * LeLotChoisi.PrixRevise / 100);
                    txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
                    dPrixDeVente = (LeLotChoisi.PrixRevise - dMontantRemise);
                    txtPrixVente.Text =dPrixDeVente.ToString("### ### ###");
                    double TauxCommission = Double.Parse(txtTauxCommission.Text);
                    dMontantCommission=dPrixDeVente * (decimal)TauxCommission / 100;
                    txtMontantCommission.Text = dMontantCommission.ToString("### ### ###");
                    
                }
                catch
                {
                    MessageBox.Show(this, "Vérifier le montant versé saisi",
                        "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbApporteurAffaires_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbApporteurAffaires.SelectedItem != null)
            {
                try
                {
                    var AppAff=(ApporteurAffaire)cmbApporteurAffaires.SelectedItem;
                    txtTauxCommission.Text = AppAff.TauxCommission.ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(this, "Erreur dans le chargement de l'apporteur d'affaire:...."+ex.Message,
                       "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tcTypeContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcTypeContrat.SelectedIndex == 0)
            {
                strTypeContrat ="Sur échéancier";
                pVersement.Visible = false;
                txtMontantEcheance.Text = "";
                cmbTypeEcheanciers.SelectedIndex = -1;
            }
            else
            {
                strTypeContrat =  "Sur Avancement Travaux";

               // var etatAvancements =  db.TypeEtatAvancements.Where(u => u.AppelFonds == true).OrderBy(u => u.Pourcentage).ToList();
                dgAvancements.DataSource = (from ea in db.TypeEtatAvancements
                                           where ea.AppelFonds==true
                                           orderby ea.ordre
                                           select new
                                           {
                                               Avancement=ea.Description,
                                               Montant = (dPrixDeVente * (decimal)ea.TauxDecaissement / 100)
                                           }).ToList();
                dgAvancements.Columns[0].Width = 200;
                dgAvancements.Columns[1].DefaultCellStyle.Format = "### ### ###";
                pVersement.Visible = true;
                //List<EtatAvancement> lesEtatsAvancements = new List<EtatAvancement>();
                //foreach (var etat in etatAvancements)
                //{
                //    EtatAvancement etatAvancement = new EtatAvancement
                //    {
                //        Avancement=etat.Description,
                //        MontantDecaissementPrevue =(decimal) (dPrixDeVente * etat.TauxDecaissement / 100),
                //        //TypeEtatAvancementID = etat.ID,
                //        //VillaID = villa.ID
                //    };
                //    lesEtatsAvancements.Add(etatAvancement);
                //    //listeAvancements.Add(new Models.ImmoAvancementViewModel
                //    //{
                //    //    MontantADecaisser = prixVilla * (decimal)etat.TauxDecaissement / 100,
                //    //    Pourcentage = etat.Pourcentage,
                //    //    TauxDecaissement = etat.TauxDecaissement,
                //    //    Avancement = etat.Description
                //    //});
                //}
            }
        }

        private void cmbTypeEcheanciers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SimulerRemboursement();
            //txtMontantEcheance.Focus();
        }

        private void txtMontantRemise_Validated(object sender, EventArgs e)
        {
            if (txtMontantRemise.Text != string.Empty)
            {
                try
                {

                    decimal dMontantRemise = decimal.Parse(txtMontantRemise.Text);
                    txtMontantRemise.Text = dMontantRemise.ToString("### ### ###");
                    
                    dPrixDeVente = (LeLotChoisi.PrixRevise - dMontantRemise);
                    txtPrixVente.Text = dPrixDeVente.ToString("### ### ###");
                    //20 × 100 ÷ 180 
                    double dTauxRemise = (double)(dMontantRemise*100/LeLotChoisi.PrixRevise)  ;
                    txtTauxRemise.Text = dTauxRemise.ToString();
                    //Recalcul de la commission
                    double TauxCommission = double.Parse(txtTauxCommission.Text);
                    dMontantCommission = dPrixDeVente * (decimal)TauxCommission / 100;
                    txtMontantCommission.Text = dMontantCommission.ToString("### ### ###");

                }
                catch
                {
                    MessageBox.Show(this, "Vérifier le montant versé saisi",
                        "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            cmbTypeEcheanciers.Focus();
        }

        private void txtMontantEcheance_Validated(object sender, EventArgs e)
        {
            //if (txtMontantEcheance.Text != string.Empty)
            //    SimulerRemboursement();
            //else
            //{
            //    pVersement.Visible = false;
            //    txtMontantVerse.Text = string.Empty;
            //    txtMontantDepotDeGarantie.Text = string.Empty;
            //    txtMontantAvance.Text = string.Empty;
            //    txtMontantReliquat.Text = string.Empty;
            //}

            //txtMontantVerse.Focus();
        }

        private void SimulerRemboursement()
        {
            if (cmbTypeEcheanciers.SelectedItem != null && txtMontantEcheance.Text != string.Empty)
            {
                try
                {
                    txtMontantEcheance.Text = decimal.Parse(txtMontantEcheance.Text).ToString("### ### ###");
                    var dMontantEcheance = decimal.Parse(txtMontantEcheance.Text);
                    int nbEcheances = 0;

                    switch (cmbTypeEcheanciers.SelectedIndex)
                    {
                        case 0:
                            nbEcheances = (int)Math.Ceiling(dPrixDeVente / dMontantEcheance);
                            derniereEcheance = premiereEcheance.AddMonths(nbEcheances);
                            break;
                        case 1:
                            nbEcheances = (int)Math.Ceiling(dPrixDeVente / dMontantEcheance);
                            derniereEcheance = premiereEcheance.AddMonths(nbEcheances * 3);
                            break;
                        case 2:
                            nbEcheances = (int)Math.Ceiling(dPrixDeVente / dMontantEcheance);
                            derniereEcheance = premiereEcheance.AddMonths(nbEcheances * 6);
                            break;
                        case 3:
                            nbEcheances = (int)Math.Ceiling(dPrixDeVente / dMontantEcheance);
                            derniereEcheance = premiereEcheance.AddMonths(nbEcheances * 12);
                            break;
                        default:
                            break;
                    }
                    txtNombreEcheances.Text = nbEcheances.ToString();
                    txtDateDerniereEcheance.Text = derniereEcheance.ToShortDateString();
                    pVersement.Visible = true;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(this, "Erreur :...." + ex.Message,
                     "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void SimulerRemboursementDelai()
        {
            if (cmbTypeEcheanciers.SelectedItem != null && txtDateDerniereEcheance.Text != string.Empty)
            {
                try
                {
                   
                    derniereEcheance = DateTime.Parse(txtDateDerniereEcheance.Text);

                   
                    
                    int nbEcheances = 0;

                    switch (cmbTypeEcheanciers.SelectedIndex)
                    {
                        case 0:
                           
                            nbEcheances = (derniereEcheance.Year - premiereEcheance.Year) * 12 + derniereEcheance.Month - premiereEcheance.Month;
                           
                            break;
                        case 1:
                            nbEcheances =  ((derniereEcheance.Year - premiereEcheance.Year) * 12 + derniereEcheance.Month - premiereEcheance.Month) / 3;;
                          
                            break;
                        case 2:
                            nbEcheances = ((derniereEcheance.Year - premiereEcheance.Year) * 12 + derniereEcheance.Month - premiereEcheance.Month) / 6;
                            break;
                        case 3:
                            nbEcheances = ((derniereEcheance.Year - premiereEcheance.Year) * 12 + derniereEcheance.Month - premiereEcheance.Month) / 12;
                            break;
                        default:
                            break;
                    }
                    txtNombreEcheances.Text = nbEcheances.ToString();
                    var MontantEcheancePrevu = (int)dPrixDeVente / nbEcheances;
                    var montantDerniereEcheance = (int)(dPrixDeVente - MontantEcheancePrevu * nbEcheances);
                    txtMontantEcheance.Text = MontantEcheancePrevu.ToString();

                    pVersement.Visible = true;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(this, "Erreur :...." + ex.Message,
                     "Prosopis -  Gestion des contrats", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
               

        }
        private void chkRemise_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRemise.Checked)
            {
                pRemise.Visible = true;
                txtTauxRemise.Text = "";
                txtMontantRemise.Text = "";
            }
            else
            {
                pRemise.Visible = false;
                txtTauxRemise.Text = "";
                txtMontantRemise.Text = "";
            }
            txtTauxRemise.Focus();
        }

        private void txtDateDerniereEcheance_Validated(object sender, EventArgs e)
            {
            SimulerRemboursementDelai();
        }

        private void txtTauxRemise_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMontantRemise_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
