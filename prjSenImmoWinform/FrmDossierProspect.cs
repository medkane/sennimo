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
using prjSenImmoWinform.DAL;

namespace prjSenImmoWinform
{
    public partial class FrmDossierProspect : Form
    {
        private ClientRepository clientRep;
        private ContratRepository contratRep;

        private Client clientEnCours;
        private Option lOptionEnCours;

        public FrmDossierProspect()
        {
            InitializeComponent();
            clientRep = new ClientRepository();
            contratRep = new ContratRepository();
            cmbTypeStatutProspect.DataSource = clientRep.GetTypeStatutProspects().ToList();
            cmbTypeStatutProspect.DisplayMember = "Libelle";
            cmbTypeStatutProspect.ValueMember = "ID";
            cmbTypeStatutProspect.SelectedIndex = -1;
           

            dtpDateStatut.CustomFormat = " "; //An empty SPACE;
            dtpDateStatut.Format = DateTimePickerFormat.Custom;
            if (Tools.Tools.AgentEnCours.Role.CodeRole == "DC")
            {
                cmdReaffecter.Enabled = false;
                cmdNouvelleNote.Enabled = false;
                cmdRendezVous.Enabled = false;
                cmdSupprimer.Enabled = false;
                cmdVente.Enabled = false;
                cmdAffecterCommercial.Enabled = false;
                cmdNouvelleOption.Enabled = false;
                cmdLeverOption.Enabled = false;
                cmdPromesseVente.Enabled = false;
                cmdNouveau.Enabled = false;
                cmdAjouterStatut.Enabled = false;
                cmdSupprimerActiviteCommerciale.Enabled = false;
                cmdFicheNotaire.Enabled = false;

                cmdFermer.Enabled = true;
            }
            cmdReaffecter.Visible = false;
            txtPrenom.Focus();
        }

        public FrmDossierProspect(Client client)
            : this()
        {

            clientEnCours = clientRep.GetClient(client.ID);
            AfficherInfosProspect(clientEnCours);
            txtIdClient.Text = clientEnCours.ID.ToString();
            txtPrenom.Focus();

        }
        public FrmDossierProspect(Client client, bool BOption)
           : this()
        {

            clientEnCours = clientRep.GetClient(client.ID);
            AfficherInfosProspect(clientEnCours);
            if(BOption)
            {
                tcDetailsDossierClient.SelectedTab = tabPage2;
            }
            txtPrenom.Focus();
        }

        private void AfficherInfosProspect(Client client)
        {
            AfficherProspect(clientEnCours);
            clientEnCours = clientRep.GetClient(client.ID);

            if (clientEnCours.Commercial != null)
            {
                if (clientEnCours.DateAffectationCommercial != null)
                { 
                    lbAffectation.Text = "Affecté(e) le " + clientEnCours.DateAffectationCommercial.Value.ToShortDateString() + " à " + clientEnCours.Commercial.NomComplet;
                }
                else
                    lbAffectation.Text = "Affecté(e) à " + clientEnCours.Commercial.NomComplet;

                if(clientEnCours.Projet!=null)
                    lbProjet.Text = clientEnCours.Projet.DenominationProjet;
                else
                    lbProjet.Text = "Projet non encore défini pour ce prospect";
                //lbDateAffectation.Text = clientEnCours.DateAffectationCommercial.Value.ToString()+ " à ";
                //lbCommercial.Text = clientEnCours.Commercial.NomComplet;
                tcDetailsDossierClient.Enabled = true;
                cmdAffecterCommercial.Visible = false;
                AfficherOptionsProspect(clientEnCours);
                AfficherActivitesCommercialesProspect(clientEnCours);
                AfficherEncaissementProspect(clientEnCours);
                AfficherNotesProspect(clientEnCours);
                AfficherStatutProspects(clientEnCours);
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "MKT" || Tools.Tools.AgentEnCours.Role.CodeRole == "DSK"
                     || Tools.Tools.AgentEnCours.Role.CodeRole == "ADM" || (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" 
                     && Tools.Tools.AgentEnCours.IsChefEquipe==true))
                    cmdReaffecter.Visible = true;
                else
                    cmdReaffecter.Visible = false;
            }
            else
            {
                lbAffectation.Text = "Prospect non encore affecté(e) à un commercial";
                lbProjet.Text = "Projet non encore défini pour ce prospect";
                tcDetailsDossierClient.Enabled = false;
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "MKT" || Tools.Tools.AgentEnCours.Role.CodeRole == "DSK" 
                    || Tools.Tools.AgentEnCours.Role.CodeRole == "ADM")
                    cmdAffecterCommercial.Visible = true;
                else
                    cmdAffecterCommercial.Visible = false;

                //lbCommercial.Text = "";
                //lbDateAffectation.Text = "";
            }
            clientEnCours = clientRep.GetClient(client.ID);
            if (clientEnCours.StatutProspects.Count > 0)
            {
                lbStatut.Visible = true;
                lbEtiquetteStatut.Visible = true;
                lbStatut.Text = clientEnCours.StatutProspects.Where(stat => stat.DateStatut == clientEnCours.StatutProspects.Max(statut => statut.DateStatut)).FirstOrDefault().TypeStatutProspect.Libelle;
            }
            else
            {
                lbStatut.Visible = false;
                lbEtiquetteStatut.Visible = false;
            }
        }

        private void AfficherProspect(Client leClient)
        {
            var client = clientRep.GetClient(leClient.ID);
            txtDateCreation.Text = client.DateCreation.ToShortDateString();
            txtDateSouscription.Text = client.DateSouscription.Value.ToShortDateString();
           
            //txtOrigine.Text = client.Origine != TypeOrigine.Autre ? client.Origine.ToString() : client.AutreOrigine;
            txtPrenom.Text = client.Prenom;
            txtNom.Text = client.Nom;
            txtDateNaissance.Text = client.DateDeNaissance != null ? client.DateDeNaissance.Value.Date.ToShortDateString() : "";
            txtLieuNaissance.Text = client.LieuDeNaissance;
            txtAdresse.Text = client.Adresse;
            txtCompteTiers.Text = client.CompteTiers;
            cmdVente.Visible = false;
            Genre sexe = client.Genre;
            if (sexe == Genre.Masculin)
                rbHomme.Checked = true;
            else
                rbFemme.Checked = true;

            txtNationalite.Text = client.Nationalite;
            txtNumeroFixe.Text = client.TelephoneFixe;
            txtNumeroMobile.Text = client.Mobile1;
            txtOrigine.Text = client.Origine != null ? client.Origine.LibelleTypeOrigine : "";

            if (client.TypePieceIdentite != 0 && client.TypePieceIdentite!=null)
            {
                txtTypePiece.Text = client.TypePieceIdentite.ToString();
                txtNumeroPiece.Text = client.NumeroPieceIdentification;
                txtDateDelivrance.Text = client.DateDeDelivrancePiece.Value.ToShortDateString();
            }
            else
            {
                txtTypePiece.Text = string.Empty;
                txtNumeroPiece.Text = string.Empty;
                txtDateDelivrance.Text = string.Empty;
            }
            txtEmail.Text = client.Email;
           
            txtCommentaireProspect.Text = client.CommentaireProspect;

            //if (client.Commercial != null)
            //{
            //    //    cmdAffecterCommercial.Visible = false;
            //    //    pCommercial.Visible = true;
            //    lbCommercial.Text = client.Commercial.NomComplet;
            //    //    AfficherOptionsProspect(client);
            //    //    AfficherActivitesCommercialesProspect(client);
            //    //    AfficherEncaissementProspect(client);
            //    //    tcDossierProspect.Visible = true;
            //    //    pActionProspect.Visible = true;
            //    //    cmdFicheNotaire.Visible = true;
            //}
            //else
            //    {
            //        lbCommercial.Text = "";
            //        //    cmdAffecterCommercial.Visible = true;
            //        //    pCommercial.Visible = false;
            //        //    tcDossierProspect.Visible = false;
            //        //    pActionProspect.Visible = false;
            //        //    cmdFicheNotaire.Visible = false;
            //        //    dgActivitesCommerciales.DataSource = null;
            //        //    dgOptions.DataSource = null;
            //        //    dgEncaissements.DataSource = null;
            //    }
        }


        private void AfficherOptionsProspect(Client prospect)
        {
            //lbTypeProspect.Text = prospect.Type.ToString();
            txtDateSouscriptionProspect.Text = string.Empty;
            txtTypeContratProspect.Text = string.Empty;
            txtTypeVillaProspect.Text = string.Empty;
            txtPositionLotProspect.Text = string.Empty;
            txtLotProspect.Text = string.Empty;
            txtPrixDeVenteProspect.Text = string.Empty;
            txtTauxEncaissementProspect.Text = string.Empty;
            txtMontantEncaisseProspect.Text = string.Empty;
            chkSeuilContratAtteint.Checked = false;
            txtMontantSeuilContrat.Text = string.Empty;
            txtSuperficie.Text = string.Empty;
            txtMontantEncaisseProspect.Text = string.Empty;
            txtPrixRevise.Text = string.Empty;
            txtTauxRemise.Text = string.Empty;
            txtMontantRemise.Text = string.Empty;
            txtDureeOption.Text = string.Empty;
            txtNbJoursRestantOption.Text = string.Empty;
            //cmdVente.Visible = false;
            try
            {
                lOptionEnCours = clientRep.GetOptionsProspect(prospect).FirstOrDefault();
                var encaissementsProspect = contratRep.GetEncaissementProspect(prospect.ID).Where(enc => enc.FraisDeDossier == false).Sum(enc => enc.MontantGlobal);
               // 
                if (lOptionEnCours != null)
                {
                    txtDateSouscriptionProspect.Text = lOptionEnCours.DatePriseOption.Value.ToShortDateString();
                    txtTypeContratProspect.Text = lOptionEnCours.TypeContrat.LibelleTypeContrat;
                    txtTypeVillaProspect.Text = lOptionEnCours.TypeVilla.CodeType;
                    txtPositionLotProspect.Text = lOptionEnCours.PositionLot.ToString();
                    txtLotProspect.Text = lOptionEnCours.Lot != null && lOptionEnCours.TypeContrat.CategorieContrat==CategorieContrat.Réservation ? lOptionEnCours.Lot.NumeroLot : "";
                    //txtSuperficie.Text= option.Lot != null ? option.Lot.Superficie.ToString("###") : option.TypeVilla.SurfaceDeBase.ToString("###");
                    //txtPrixRevise.Text = option.Lot.PrixRevise.ToString("### ### ###");
                    txtSuperficie.Text = lOptionEnCours.Surface.ToString("###");
                    txtPrixRevise.Text = lOptionEnCours.PrixRevise.ToString("### ### ###");
                    txtTauxRemise.Text = lOptionEnCours.TauxRemiseAccordee.ToString("##0.##") + "%";
                    txtMontantRemise.Text = lOptionEnCours.MontantRemiseAccordee.ToString("### ### ##0").Trim();
                    txtPrixDeVenteProspect.Text = lOptionEnCours.PrixDeVente.ToString("### ### ###");
                    txtMontantEncaisseProspect.Text = encaissementsProspect.ToString("### ### ##0").Trim();
                    txtTauxEncaissementProspect.Text = (encaissementsProspect / lOptionEnCours.PrixDeVente * 100).ToString("##0.##") + "%";

                    if (lOptionEnCours.DateFinOption != null)
                    {
                       txtDureeOption.Text= (lOptionEnCours.DateFinOption.Value.Subtract(lOptionEnCours.DatePriseOption.Value).Days + 1).ToString()+" jrs";
                       txtNbJoursRestantOption.Text = (lOptionEnCours.DateFinOption.Value.Subtract(DateTime.Now).Days + 1).ToString() + " jrs";
                        txtNbJoursRestantOption.Visible = true;
                        lbJ.Visible = true;
                    }
                    else
                    {
                        txtDureeOption.Text = "Illimitée";
                        txtNbJoursRestantOption.Text = string.Empty;
                        txtNbJoursRestantOption.Visible = false;
                        lbJ.Visible = false;

                    }

                    txtMontantSeuilContrat.Text = lOptionEnCours.TypeContrat.CategorieContrat == CategorieContrat.Réservation ?
                            (lOptionEnCours.PrixDeVente * lOptionEnCours.TypeContrat.SeuilEntreeEnVigueur / 100).ToString("### ### ###") :
                            (lOptionEnCours.PrixDeVente * lOptionEnCours.TypeContrat.SeuilSouscription / 100).ToString("### ### ###")
                            ;
                    chkSeuilContratAtteint.Checked = lOptionEnCours.SeuilContratAtteint;
                    if (lOptionEnCours.SeuilContratAtteint && lOptionEnCours.ContratGenere == false)
                    {
                        cmdVente.Visible = true;
                        lbContratGenere.Visible = false;
                    }
                    else
                    {
                        if (lOptionEnCours.ContratGenere == true)
                        {
                            cmdVente.Visible = true;
                            cmdVente.Enabled = false;
                            lbContratGenere.Visible = true;
                            lbContratGenere.Text = "Contrat déjà généré pour cette option".ToUpper();
                        }
                        else
                        {
                            cmdVente.Visible = false;
                            lbContratGenere.Visible = false;
                        }
                    }


                }
                //chkContratGenere.Checked = option.ContratGenere;
                //chkSeuilContratAtteint.Checked = option.SeuilContratAtteint;
                var options = clientRep.GetOptionsProspect(prospect);
                dgOptions.DataSource = options.ToList().Select(opttion => new
                {
                    ID = opttion.Id,
                    Option = opttion.TypeContrat.LibelleTypeContrat,
                    Lot = opttion.Lot != null && lOptionEnCours.TypeContrat.CategorieContrat==CategorieContrat.Réservation ? opttion.Lot.NumeroLot : "",
                    Type = opttion.TypeVilla.CodeType,
                    Position = opttion.PositionLot,
                    Prix = opttion.PrixDeVente,
                    DateOption = opttion.DatePriseOption,
                    DateFinOption = opttion.DateFinOption,
                }
                                                   ).ToList();
                dgOptions.Columns[0].Width = 0;
                dgOptions.Columns[1].Width = 80;
                dgOptions.Columns[2].Width = 50;
                dgOptions.Columns[3].Width = 60;
                dgOptions.Columns[4].Width = 80;
                dgOptions.Columns[5].Width = 70;
                dgOptions.Columns[5].DefaultCellStyle.Format = "### ### ###";
                dgOptions.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgOptions.Columns[6].Width = 80;
                dgOptions.Columns[7].Width = 80;
                //dgProspects.Columns[7].Width = 170;
                //dgProspects.Columns[3].HeaderText = "Né(e) le";
                //dgProspects.Columns[4].HeaderText = "à";
                dgOptions.Columns[0].Visible = false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void AfficherActivitesCommercialesProspect(Client client)
        {
            try
            {
                var activitesCommerciales = clientRep.GetActivitesCommercialesProspect(client).OrderByDescending(act => act.DateActivite).ThenByDescending(act => act.HeureActivite);
                dgActivitesCommerciales.DataSource = activitesCommerciales.ToList().Select(act => new
                {
                    ID = act.Id,
                    Date = act.DateActivite,
                    Heure = act.HeureActivite.ToString().Substring(0, 5),
                    Activité = act.TypeActivite != TypeActivite.Autre ? act.TypeActivite.ToString() : act.AutreTypeActivite,
                    
                  
                    Détails = act.Commentaire




                }
                                                   ).ToList();
                dgActivitesCommerciales.Columns[0].Width = 0;
                dgActivitesCommerciales.Columns[1].Width = 80;
                dgActivitesCommerciales.Columns[2].Width = 50;
                dgActivitesCommerciales.Columns[3].Width = 120;
                dgActivitesCommerciales.Columns[3].DefaultCellStyle.Format = "HH:mm";
                dgActivitesCommerciales.Columns[4].Width = 350;
                //dgActivitesCommerciales.Columns[5].Width = 100;
                //dgActivitesCommerciales.Columns[6].Width = 100;
                //dgProspects.Columns[7].Width = 170;
                //dgProspects.Columns[3].HeaderText = "Né(e) le";
                //dgProspects.Columns[4].HeaderText = "à";
                dgActivitesCommerciales.Columns[0].Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void AfficherEncaissementProspect(Client prospect)
        {
            try
            {
                var encaissements = clientRep.GetEncaissementsProspect(prospect.ID);
                dgEncaissementsdgEncaissements.DataSource = encaissements.ToList().Select(enc => new
                {
                    ID = enc.ID,
                    Date = enc.DateEncaissement,
                    Numéro = enc.NumeroEncaissement,
                    Référence = enc.ReferencePaiement,
                    Commentaire=enc.Commentaire,
                    Montant = enc.MontantGlobal

                }
                                                   ).ToList();
                dgEncaissementsdgEncaissements.Columns[0].Width = 0;
                dgEncaissementsdgEncaissements.Columns[1].Width = 80;
                dgEncaissementsdgEncaissements.Columns[2].Width = 100;
                dgEncaissementsdgEncaissements.Columns[3].Width = 230;
                dgEncaissementsdgEncaissements.Columns[4].Width = 330;
                dgEncaissementsdgEncaissements.Columns[5].Width = 80;
                dgEncaissementsdgEncaissements.Columns[5].DefaultCellStyle.Format = "### ### ###";
                dgEncaissementsdgEncaissements.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //dgProspects.Columns[3].HeaderText = "Né(e) le";
                //dgProspects.Columns[4].HeaderText = "à";
                dgEncaissementsdgEncaissements.Columns[0].Visible = false;
                lbSolde.Text = encaissements.Sum(enc => enc.MontantGlobal).ToString("### ### ###");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfficherNotesProspect(Client prospect)
        {
            try
            {
              var noteProspects = clientRep.GetNotesProspect(prospect.ID).OrderByDescending(note => note.DateDebutTypeOrigine);
                  
                    lvNotes.Items.Clear();
                    int i = 0;
                    foreach (var note in noteProspects.ToList())
                    {
                        ListViewItem lviNote = new ListViewItem(note.DateDebutTypeOrigine.Value.ToShortDateString());
                        lviNote.SubItems.Add(note.Comentaire);
                        lvNotes.Items.Add(lviNote);
                    }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void AfficherStatutProspects(Client prospect)
        {
            try
            {
                var statutsProspects = clientRep.GetStatutsProspect(prospect.ID).OrderByDescending(statut => statut.DateStatut);

                lvStatutProspects.Items.Clear();
                foreach (var statut in statutsProspects.ToList())
                {
                    ListViewItem lviStatut = new ListViewItem(statut.DateStatut.ToShortDateString());
                    lviStatut.SubItems.Add(statut.TypeStatutProspect.Libelle);
                    lviStatut.SubItems.Add(statut.Motif);
                    lviStatut.Tag = statut;
                    lvStatutProspects.Items.Add(lviStatut);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtOrigine_TextChanged(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgOptions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pProjet_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdEditerProspect_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FrmProspect childForm = new FrmProspect(clientEnCours);

                childForm.StartPosition = FormStartPosition.CenterScreen;
                childForm.ShowDialog();
                childForm.WindowState = FormWindowState.Normal;
                if(clientRep.GetClient(clientEnCours.ID)!=null)
                AfficherProspect(clientRep.GetClient(clientEnCours.ID));

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Gestion des Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdFicheNotaire_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                GenererFicheNotaire();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                     "Prosopis - Edition de la fiche notaire", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void GenererFicheNotaire()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                //object templateName = @"D:\Med\Projet Immociel\prjWinform\prjSenImmoWinform\prjSenImmoWinform\bin\Debug\modeles\FicheNotaire.dotx";
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "FicheNotaire.dotx";

                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);

                msWord.Activate();
                doc.Activate();
                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;


                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = clientEnCours.NomComplet;

                if (clientEnCours.DateDeNaissance != null)
                {
                    myBookmark = bookmarks["DateEtLieuDeNaissance"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = clientEnCours.DateDeNaissance.Value.Date.ToShortDateString() + " à " + clientEnCours.LieuDeNaissance;
                }

                myBookmark = bookmarks["Nationalite"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = clientEnCours.Nationalite;

                myBookmark = bookmarks["Profession"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = clientEnCours.Profession;
                object oCheckbox = null;
                if (clientEnCours.SituationMatrimoniale == SituationMatrimoniale.Célibataire)
                {
                    oCheckbox = "CaseACocher1";
                }
                else if (clientEnCours.SituationMatrimoniale == SituationMatrimoniale.Marié)
                {
                    oCheckbox = "CaseACocher2";
                }
                else if (clientEnCours.SituationMatrimoniale == SituationMatrimoniale.Veuf)
                {
                    oCheckbox = "CaseACocher3";
                }
                else if (clientEnCours.SituationMatrimoniale == SituationMatrimoniale.Divorcé)
                {
                    oCheckbox = "CaseACocher4";
                }

                doc.FormFields.get_Item(ref oCheckbox).CheckBox.Value = true;

                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = clientEnCours.Adresse;

                myBookmark = bookmarks["Email"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = clientEnCours.Email;

                myBookmark = bookmarks["TelBureau"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = clientEnCours.TelephoneFixe;

                myBookmark = bookmarks["telPortable"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = clientEnCours.Mobile1;

                //myBookmark = bookmarks["telDomicile"];
                //bookmarkRange = myBookmark.Range;
                //bookmarkRange.Text = LeClientEnCours.T;

                myBookmark = bookmarks["Fax"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = clientEnCours.Fax;

                if (clientEnCours.SituationMatrimoniale == SituationMatrimoniale.Marié)
                {

                    myBookmark = bookmarks["NomCompletConjoint"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = clientEnCours.PrenomConjoint;

                    if (clientEnCours.DateDeNaissanceConjoint != null)
                    {
                        myBookmark = bookmarks["DateNaissanceConjoint"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = clientEnCours.DateDeNaissanceConjoint.Value.Date.ToShortDateString();
                    }

                    myBookmark = bookmarks["ProfessionConjoint"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = clientEnCours.ProfessionConjoint;


                    myBookmark = bookmarks["NationaliteConjoint"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = clientEnCours.NationaliteConjoint;

                    if (clientEnCours.DateMariage != null)
                    {
                        myBookmark = bookmarks["DateEtLieuMariage"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = clientEnCours.DateMariage.Value.ToShortDateString() + " à " + clientEnCours.LieuDeMariage;
                    }

                    if (clientEnCours.DateContratMariage != null)
                    {
                        myBookmark = bookmarks["DateContratMariage"];
                        bookmarkRange = myBookmark.Range;
                        bookmarkRange.Text = clientEnCours.DateContratMariage.Value.ToShortDateString();
                    }
                    object oCheckboxRegime = null;
                    if (clientEnCours.RegimeMatrimoniale == RegimeMatrimoniale.Séparation)
                    {
                        oCheckboxRegime = "CaseACocher5";
                    }
                    else if (clientEnCours.RegimeMatrimoniale == RegimeMatrimoniale.Communautaire)
                    {
                        oCheckboxRegime = "CaseACocher6";
                    }
                    else if (clientEnCours.RegimeMatrimoniale == RegimeMatrimoniale.Autre)
                    {
                        oCheckboxRegime = "CaseACocher7";
                    }

                    doc.FormFields.get_Item(ref oCheckboxRegime).CheckBox.Value = true;

                    myBookmark = bookmarks["NomEtResidenceNotaire"];
                    bookmarkRange = myBookmark.Range;
                    bookmarkRange.Text = "Me " + clientEnCours.PrenomNotaire + " " + clientEnCours.NomNotaire + " demeurant à " + clientEnCours.AdresseNotaire;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void cmdRendezVous_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FrmActiviteCommerciale childForm = new FrmActiviteCommerciale(clientEnCours);
                childForm.StartPosition = FormStartPosition.CenterScreen;
                childForm.ShowDialog();
                AfficherActivitesCommercialesProspect(clientEnCours);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dgActivitesCommerciales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgActivitesCommerciales_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgActivitesCommerciales.SelectedRows.Count > 0)
                {
                    int idActComm = (int)dgActivitesCommerciales.SelectedRows[0].Cells[0].Value;
                    new FrmActiviteCommerciale(idActComm).ShowDialog();
                    AfficherActivitesCommercialesProspect(clientEnCours);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "¨Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdNouvelleOption_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //VERIFIER SI LE PROSPECT N'A PAS DEJA PRIS UNE OPTION
                var option = contratRep.GetOptionProspect(clientEnCours.ID);
                if (option != null)
                {
                    MessageBox.Show(this, "Désolé ce prospect a déjà souscrit à une option:...",
                        "Prosopis - Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FrmOptionProspect frmDetIlot = new FrmOptionProspect(clientEnCours);
                frmDetIlot.StartPosition = FormStartPosition.CenterScreen;
             
                frmDetIlot.ShowDialog();
                AfficherOptionsProspect(clientEnCours);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Gestion des options du prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdLeverOption_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(this, "Voulez vous réellement vous lever cette option?",
               "Prosopis - Lever d'option", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                try
                {
                    if (dgOptions.SelectedRows.Count > 0)
                    {
                        int idOption = (int)dgOptions.SelectedRows[0].Cells[0].Value;
                        clientRep.LeverOption(idOption);
                        AfficherOptionsProspect(clientEnCours);
                        cmdLeverOption.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Erreur:..." + ex.Message,
                      "Prosopis - Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgOptions_SelectionChanged(object sender, EventArgs e)
        {

            if (dgOptions.SelectedRows.Count > 0)
            {
                cmdLeverOption.Visible = true;
                if(lOptionEnCours.TypeContrat.CategorieContrat== CategorieContrat.Réservation)
                    cmdPromesseVente.Visible = true;
            }
            else
            { 
                cmdLeverOption.Visible = false;
                cmdPromesseVente.Visible = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdSupprimerActiviteCommerciale_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show(this, "Voulez vous réellement annuler cette activité commerciale?",
              "Prosopis - Gestion des activités commerciales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    if (dgActivitesCommerciales.SelectedRows.Count > 0)
                    {
                        int idActComm = (int)dgActivitesCommerciales.SelectedRows[0].Cells[0].Value;
                        clientRep.DeleteActiviteCommerciale(idActComm);
                        AfficherActivitesCommercialesProspect(clientEnCours);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "¨Prosopis - Gestion des activités commerciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdNouvelleNote_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNote.Text!=string.Empty)
                {
              
                    clientRep.AjouterNote(clientEnCours.ID,txtNote.Text);
                    AfficherNotesProspect(clientEnCours);
                    txtNote.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                  "Prosopis - Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdVente_Click(object sender, EventArgs e)
        {
            FrmVente frmVD = new FrmVente(clientEnCours, false);
            frmVD.WindowState = FormWindowState.Normal;
            frmVD.StartPosition = FormStartPosition.CenterParent;
            frmVD.ShowDialog();
            cmdVente.Visible = false;
            //EffacerForm();
            //dgActivitesCommerciales.DataSource = null;
            //dgEncaissements.DataSource = null;
            //dgOptions.DataSource = null;
            //this.ChargerLesProspect();

        }

        private void FrmDossierProspect_Load(object sender, EventArgs e)
        {

        }

        private void lvNotes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      

        private void cmdAffecterCommercial_Click(object sender, EventArgs e)
        {
            FrmAffectationCommercial frmAffecterComm = new FrmAffectationCommercial(ref clientEnCours);
            frmAffecterComm.WindowState = FormWindowState.Normal;
            frmAffecterComm.StartPosition = FormStartPosition.CenterParent;
            frmAffecterComm.ShowDialog();
            AfficherInfosProspect(clientEnCours);
        }

        private void cmdReaffecter_Click(object sender, EventArgs e)
        {
            FrmAffectationCommercial frmAffecterComm;
            clientEnCours = clientRep.GetClient(clientEnCours.ID);
            if (clientEnCours.Commercial!= null && clientEnCours.Commercial.Id==45)
                frmAffecterComm = new FrmAffectationCommercial(ref clientEnCours, true, "Prospect");
            else
                frmAffecterComm = new FrmAffectationCommercial(ref clientEnCours, true);
            frmAffecterComm.WindowState = FormWindowState.Normal;
            frmAffecterComm.StartPosition = FormStartPosition.CenterParent;
            frmAffecterComm.ShowDialog();
            AfficherInfosProspect(clientEnCours);
        }

        private void txtDateSouscriptionProspect_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTypeContratProspect_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTypeVillaProspect_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLotProspect_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrixRevise_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTauxRemise_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrixDeVenteProspect_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMontantSeuilContrat_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTauxEncaissementProspect_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDureeOption_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDateCreation_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void cmdAjouterStatut_Click(object sender, EventArgs e)
        {
            try
            {
                // if(dtpDateStatut.Value)
                if (cmbTypeStatutProspect.SelectedIndex != -1)
                {
                    TypeStatutProspect tsp = cmbTypeStatutProspect.SelectedItem as TypeStatutProspect;
                    StatutProspect stp = new StatutProspect()
                    {
                        DateStatut = dtpDateStatut.Value.Date,
                        ProspectId = clientEnCours.ID,
                        TypeStatutProspectId = tsp.ID,
                        Motif = txtMotif.Text

                    };
                    clientRep.AddStatutsProspect(stp);
                }
                EffacerStatutProspect();
                AfficherInfosProspect(clientEnCours);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "¨Prosopis - Gestion des stauts du prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpDateStatut_ValueChanged(object sender, EventArgs e)
        {
            dtpDateStatut.CustomFormat = "dd/MM/yyyy";
        }

        private void lvStatutProspects_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvStatutProspects.SelectedItems.Count > 0)
                {
                    StatutProspect stp = lvStatutProspects.SelectedItems[0].Tag as  StatutProspect;
                    AfficherStatutProspect(stp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "¨Prosopis - Gestion des stauts du prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherStatutProspect(StatutProspect stp)
        {
            if(stp!=null)
            {
                dtpDateStatut.Value = stp.DateStatut;
                dtpDateStatut.Enabled = false;
                cmbTypeStatutProspect.SelectedValue = stp.TypeStatutProspect.ID;
                cmbTypeStatutProspect.Enabled = false;
                txtMotif.Text = stp.Motif;
                txtMotif.ReadOnly = true;
            }
        }

        private void cmdSupprimer_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "Voulez vous réellement supprimer ce statut du prospect?", "Prosopis - Gestion des prospect", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (lvStatutProspects.SelectedItems.Count > 0)
                    {
                        StatutProspect stp = lvStatutProspects.SelectedItems[0].Tag as StatutProspect;
                        clientRep.DeleteStatutsProspect(stp);
                        EffacerStatutProspect();
                        AfficherInfosProspect(clientEnCours);
                    }
                    else
                    {
                        MessageBox.Show(this, "Erreur: veuillez d'abord selection le statut à supprimer",
                      "¨Prosopis - Gestion des statuts du prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "¨Prosopis - Gestion des statuts du prospect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void cmdNouveau_Click(object sender, EventArgs e)
        {
            EffacerStatutProspect();
        }
        private void EffacerStatutProspect()
        {

            dtpDateStatut.CustomFormat = " "; //An empty SPACE;
            dtpDateStatut.Format = DateTimePickerFormat.Custom;
            dtpDateStatut.Enabled = true;
            cmbTypeStatutProspect.SelectedIndex = -1;
            cmbTypeStatutProspect.Enabled = true;
            txtMotif.Text = string.Empty;
            txtMotif.ReadOnly = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void pClient_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmDossierProspect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                txtIdClient.Visible = true;
            }

            if (e.KeyCode == Keys.F11)
            {
                txtIdClient.Visible = false;
            }
        }

     

        private void cmdPromesseVente_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var option = clientRep.GetOptionsProspect(clientEnCours).FirstOrDefault();

                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                string template = "";
                if (clientEnCours.ProjetId == 1)
                    template = "PROMESSEDEVENTEAKYS.dotx";
                else
                    if(option.TypeVilla.TypeConstruction== TypeConstruction.Villa)
                        template = "PROMESSEDEVENTEKERRIAVILLA.dotx";
                    else
                        template = "PROMESSEDEVENTEKERRIAAPPART.dotx";

                object templateName = dossierTemplates + template;


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
                msWord.Activate();
                doc.Activate();
                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;
                
                myBookmark = bookmarks["Titre1"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = option.Client.Genre == Genre.Masculin ? "Monsieur" : "Madame";
                myBookmark = bookmarks["NomComplet"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = option.Client.NomComplet;
                
                myBookmark = bookmarks["Adresse"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = option.Client.Adresse;
                myBookmark = bookmarks["Ville"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = " - " + option.Client.Ville;

                myBookmark = bookmarks["Pays"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = option.Client.Pays;

                myBookmark = bookmarks["PrixDeVente"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = option.PrixDeVente.ToString("### ### ###");
                              
                myBookmark = bookmarks["PrixDeVenteEnLettres"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = DAL.FrenchNumberToWords.convert((long)option.PrixDeVente);

                myBookmark = bookmarks["NomTypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = option.Lot.TypeVilla.NomType.ToUpper() + " ";

                myBookmark = bookmarks["TypeVilla"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = option.Lot.TypeVilla.CodeType.ToUpper() + " ";

                myBookmark = bookmarks["Superficie"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = option.Surface.ToString("###");
                
                Microsoft.Office.Interop.Word.Tables tables = doc.Tables;
                if (tables.Count > 0)
                {
                    //Get the first table in the document
                    Microsoft.Office.Interop.Word.Table table = tables[1];
                    foreach (var niveauAvancement in option.TypeContrat.TypeEtatAvancements)
                    {
                        Microsoft.Office.Interop.Word.Row row = table.Rows.Add(ref missing);
                        row.Cells[1].Range.Text = niveauAvancement.LibelleCommercial;
                        row.Cells[1].WordWrap = true;
                        row.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[1].Range.Bold = 0;

                        row.Cells[2].Range.Text = niveauAvancement.TauxDecaissement.ToString("###")+"%";
                        row.Cells[2].WordWrap = false;
                        row.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                        row.Cells[2].Range.Bold = 1;

                    }
                    Microsoft.Office.Interop.Word.Row row2 = table.Rows.Add(ref missing);
                    row2.Cells[1].Range.Text ="TOTAL";
                    row2.Cells[1].WordWrap = true;
                    row2.Cells[1].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row2.Cells[1].Range.Bold = 1;

                    row2.Cells[2].Range.Text =  "100%";
                    row2.Cells[2].WordWrap = false;
                    row2.Cells[2].Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    row2.Cells[2].Range.Bold = 1;

                }


                myBookmark = bookmarks["DateOption"];
                bookmarkRange = myBookmark.Range;
                bookmarkRange.Text = option.DatePriseOption.Value.Day + " " + String.Format("{0:y}", option.DatePriseOption.Value); //dateVersement.ToShortDateString();
                //// Attribuer le nom
                //object fileName = @"Mon nouveau document.doc";
                ////// Sauver le document
                ////doc.SaveAs(ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing);
                //// Fermer le document
                //doc.Close(ref missing, ref missing, ref missing);


                //// Fermeture de word
                //msWord.Quit(ref missing, ref missing, ref missing);
                msWord.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:..." + ex.Message,
                        "¨Prosopis - Edition Promesse de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
