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
    public partial class FrmProspect : Form
    {
       // private SenImmoDataContext db;
        private  bool bModifClient;
        private Client clientEnCours;
        private ClientRepository clientDAL;
        private TypeOrigine lOrigineEncours;

        public FrmProspect()
        {
            InitializeComponent();
            //db = new SenImmoDataContext();
            clientDAL = new ClientRepository();
            cmbTypePieces.DataSource = Enum.GetValues(typeof(TypePieceIdentite));
            cmbTypePieces.SelectedIndex = -1;

            cmbOrigines.DataSource = clientDAL.GetAllTypeOrigines().ToList();
            cmbOrigines.DisplayMember = "LibelleTypeOrigine";
            cmbOrigines.SelectedIndex = -1;


            cmbPays.DataSource = clientDAL.GetAllPays().ToList();
            cmbPays.DisplayMember = "CountryName";
            cmbPays.ValueMember = "Id";
            cmbPays.SelectedIndex = -1;



            cmbCooperatives.DataSource = clientDAL.GetAllCooperatives().ToList();
            cmbCooperatives.DisplayMember = "Denomination";
            cmbCooperatives.ValueMember = "Id";
            cmbCooperatives.SelectedIndex = -1;


            dtpDateNaissance.CustomFormat = " "; //An empty SPACE;
            dtpDateNaissance.Format = DateTimePickerFormat.Custom;

            dtpDateDelivrancePiece.CustomFormat = " "; //An empty SPACE;
            dtpDateDelivrancePiece.Format = DateTimePickerFormat.Custom;

            dtpDateNaissanceConjoint.CustomFormat = " "; //An empty SPACE;
            dtpDateNaissanceConjoint.Format = DateTimePickerFormat.Custom;

            dtpDateMariage.CustomFormat = " "; //An empty SPACE;
            dtpDateMariage.Format = DateTimePickerFormat.Custom;

            dtpDateContratMariage.CustomFormat = " "; //An empty SPACE;
            dtpDateContratMariage.Format = DateTimePickerFormat.Custom;
            rbCelibataire.Checked = true;
            pHeaderProspect.Enabled = false;
            pDetailsProspect.Enabled = false;
            //if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
            //{
            //    rbDesk.Enabled = false;
            //    rbPerso.Checked = true;
            //    rbPerso.Enabled = false;
            //    cmbOrigines.Enabled = false;
            //}
            //if (Tools.Tools.AgentEnCours.Role.CodeRole == "DSK" || Tools.Tools.AgentEnCours.Role.CodeRole == "MKT")
            //{
            //    rbDesk.Enabled = true;
            //    rbDesk.Checked = true;
            //    rbPerso.Checked = false;
            //    rbPerso.Enabled = false;
            //    cmbOrigines.Enabled = true;
            //}
            if (Tools.Tools.AgentEnCours.Role.CodeRole == "DC")
            {
                pCommandes.Enabled = false;
                cmdFermer.Enabled = true;
            }

        }



        public FrmProspect(Client client)
            : this()
        {
           
            clientEnCours = clientDAL.GetClient(client.ID);
            AfficherClient(clientEnCours);
            pHeaderProspect.Enabled = true;
            pDetailsProspect.Enabled = true;
            
            VerouillerForm();
           
        }

        public FrmProspect(Cooperative coop)
           : this()
        {
            pHeaderProspect.Enabled = true;
            pDetailsProspect.Enabled = true;
            chkCooperative.Checked = true;
            cmbCooperatives.SelectedValue = coop.Id;
          

        }

        private void cmdNouveau_Click(object sender, EventArgs e)
        {

        }

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPrenom.Text == string.Empty )
                {
                    MessageBox.Show(this, "Veuillez saisir le prénom",
                             "Prosopis -  Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cmbPays.SelectedIndex == -1)
                {
                    MessageBox.Show(this, "Veuillez choisir le pays",
                             "Prosopis -  Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime dDateNaissance = DateTime.Parse("01/01/1900");
                DateTime dDateDelivrancePiece = DateTime.Parse("01/01/1900");
                DateTime dDateNaissanceConjoint = DateTime.Parse("01/01/1900");
                DateTime dDateMariage = DateTime.Parse("01/01/1900");
                DateTime dDateContratMariage = DateTime.Parse("01/01/1900");

                if (dtpDateNaissance.CustomFormat != " ")
                {
                    dDateNaissance = dtpDateNaissance.Value.Date;
                }
                if (dtpDateDelivrancePiece.CustomFormat != " ")
                {
                    dDateDelivrancePiece = dtpDateDelivrancePiece.Value.Date;
                }

                if (rbMarie.Checked)
                {
                    if (dtpDateNaissanceConjoint.CustomFormat != " ")
                    {
                        dDateNaissanceConjoint = dtpDateNaissanceConjoint.Value.Date;
                    }

                    if (dtpDateMariage.CustomFormat != " ")
                    {
                        dDateMariage = dtpDateMariage.Value.Date;
                    }
                    if (dtpDateContratMariage.CustomFormat != " ")
                    {
                        dDateContratMariage = dtpDateContratMariage.Value.Date;
                    }
                }

                //if (txtDateNaissance.Text != string.Empty)
                //{
                //    txtDateNaissance.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                //    dDateNaissance = DateTime.Parse(txtDateNaissance.Text);
                //}
                //if (txtDateDelivrance.Text != string.Empty)
                //{
                //    txtDateDelivrance.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                //    dDelivrancePiece = DateTime.Parse(txtDateDelivrance.Text);
                //}

                //if(dtpDateNaissance.Value)
                //{

                //}
                SituationMatrimoniale sMatrimonial = SituationMatrimoniale.Célibataire;
                if (rbCelibataire.Checked)
                {
                    sMatrimonial = SituationMatrimoniale.Célibataire;
                }
                else
                     if (rbMarie.Checked)
                {
                    sMatrimonial = SituationMatrimoniale.Marié;
                }
                else
                     if (rbVeuf.Checked)
                {
                    sMatrimonial = SituationMatrimoniale.Veuf;
                }
                else
                     if (rbDivorcé.Checked)
                {
                    sMatrimonial = SituationMatrimoniale.Divorcé;
                }
                var regimeMatrimonial = RegimeMatrimoniale.Communautaire;

                if (rbRegimeSeparation.Checked)
                {
                    regimeMatrimonial = RegimeMatrimoniale.Séparation;
                }
                else if (rbRegimeCommunautaire.Checked)
                {
                    regimeMatrimonial = RegimeMatrimoniale.Communautaire;
                }
                else if (rbRegimeAutre.Checked)
                {
                    regimeMatrimonial = RegimeMatrimoniale.Autre;
                }
                Cooperative cooperative = null;
                if (chkCooperative.Checked)
                {
                    cooperative = (Cooperative)cmbCooperatives.SelectedItem;
                }
                //if (Tools.Tools.AgentEnCours.Role.CodeRole != "CMC" )
                //    lOrigineEncours = (TypeOrigine)cmbOrigines.SelectedItem;
                //else
                //    lOrigineEncours = clientDAL.GetTypeOriginePerso();
                if (!bModifClient)
                {
                    var newClient = new Client();
                    newClient.DateCreation = DateTime.Now.Date;
                    clientDAL.AddClient(newClient);
                    #region commentaires
                    //{
                    //    Prenom = txtPrenom.Text,
                    //    Nom = txtNom.Text,
                    //    DateDeNaissance = dDateNaissance.Date,
                    //    LieuDeNaissance = txtLieuNaissance.Text,
                    //    Adresse = txtAdresse.Text,
                    //    Ville=txtVille.Text,
                    //    Pays=txtPays.Text,
                    //    Nationalite = txtNationalite.Text,
                    //    Profession = txtProfession.Text,
                    ////TypePieceIdentite = ((TypePieceIdentite)cmbTypePieces.SelectedItem),
                    //NumeroPieceIdentification = txtNumeroPiece.Text,
                    //    DateDeDelivrancePiece = dDateDelivrancePiece.Date,
                    //    Type = TypeClient.ProspectSansOption,
                    //    Genre = rbHomme.Checked ? Genre.Masculin : Genre.Féminin,
                    //    TelephoneFixe = txtNumeroFixe.Text,
                    //    Mobile1 = txtNumeroMobile.Text,
                    //    DateCreation = DateTime.Now,
                    //    Email = txtEmail.Text,
                    //    Actif = true,
                    //    Origine = lOrigineEncours,
                    //    AutreOrigine = txtAutreOrigine.Text,
                    //    SituationMatrimoniale = sMatrimonial,
                    //};

                    //if(sMatrimonial== SituationMatrimoniale.Marié)
                    //{
                    //    newClient.PrenomConjoint = txtPrenomConjoint.Text;
                    //    newClient.NomConjoint = txtNomConjoint.Text;
                    //    newClient.DateDeNaissanceConjoint = dDateNaissanceConjoint.Date;
                    //    newClient.LieuDeNaissanceConjoint = txtLieuNaissanceConjoint.Text;
                    //    newClient.NationaliteConjoint = txtNationaliteConjoint.Text;
                    //    newClient.DateMariage = dDateMariage.Date;
                    //    newClient.DateContratMariage = dDateContratMariage.Date;
                    //    newClient.RegimeMatrimoniale = regimeMatrimonial;
                    //    newClient.PrenomNotaire = txtPrenomNotaire.Text;
                    //    newClient.NomNotaire=txtNomNotaire.Text;
                    //}

                    //if (cmbTypePieces.SelectedItem != null)
                    //{
                    //    newClient.TypePieceIdentite = ((TypePieceIdentite)cmbTypePieces.SelectedItem);
                    //    newClient.NumeroPieceIdentification = txtNumeroPiece.Text;
                    //    newClient.DateDeDelivrancePiece = dDateDelivrancePiece;
                    //}

                    //if(chkCooperative.Checked)
                    //{
                    //    if(cmbCooperatives.SelectedItem!=null)
                    //    {
                    //        newClient.CooperativeId = ((Cooperative)cmbCooperatives.SelectedItem).Id;
                    //    }
                    //}
                    //clientDAL.AddClient(newClient);
                    #endregion

                    EnregistrerProspect(newClient, dDateNaissance, dDateDelivrancePiece, dDateNaissanceConjoint, dDateMariage, dDateContratMariage, sMatrimonial, regimeMatrimonial);
                    clientEnCours = newClient;

                    MessageBox.Show(this, "Le prospect a été enregistré",
                          "Prosopis -  Gestion des prospect", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    clientEnCours = clientDAL.GetClient(clientEnCours.ID);
                    EnregistrerProspect(clientEnCours, dDateNaissance, dDateDelivrancePiece, dDateNaissanceConjoint, dDateMariage, dDateContratMariage, sMatrimonial, regimeMatrimonial);
                    MessageBox.Show(this, "Le prospect a été modifié avec succes",
                             "Prosopis -  Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if ((Tools.Tools.AgentEnCours.Role.CodeRole != "CMC" 
                    || (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" && Tools.Tools.AgentEnCours.IsChefEquipe))  
                    && clientEnCours.ProspectAffecte==false )////!!!!!!!!!
                {

                    if (MessageBox.Show("Voulez vous affecter ce prospect à un commercial?", "Gestion des prospects", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        clientEnCours = clientDAL.GetClient(clientEnCours.ID);
                        FrmAffectationCommercial frmAffecterComm = new FrmAffectationCommercial(ref clientEnCours);
                        frmAffecterComm.WindowState = FormWindowState.Normal;
                        frmAffecterComm.StartPosition = FormStartPosition.CenterParent;
                        frmAffecterComm.ShowDialog();
                        //clientRep.SaveChanges();


                    } 
                }
                //else
                //{
                //    if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" && clientEnCours.ProspectAffecte == false)
                //    {

                //    }
                //}

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur: ... "+ex.Message,
                            "Prosopis -  Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnregistrerProspect(Client LeClient ,DateTime dDateNaissance, DateTime dDateDelivrancePiece, DateTime dDateNaissanceConjoint, DateTime dDateMariage, DateTime dDateContratMariage, SituationMatrimoniale sMatrimonial, RegimeMatrimoniale regimeMatrimonial)
        {
            SenImmoDataContext db = new SenImmoDataContext();
            var client = db.Clients.Find(LeClient.ID);
            var lOrigine = db.TypeOrigines.Find(lOrigineEncours.TypeOrigineId);

            client.DateSouscription = dtpDateSouscription.Value.Date;
            client.Prenom = txtPrenom.Text;
            client.Nom = txtNom.Text;
            client.DateDeNaissance = dDateNaissance;
            client.LieuDeNaissance = txtLieuNaissance.Text;
            client.Adresse = txtAdresse.Text;
            client.Ville = txtVille.Text;
            client.Pays = txtPays.Text;
            if(cmbPays.SelectedItem!=null)
                client.CountryId =(int) cmbPays.SelectedValue;
            client.Nationalite = txtNationalite.Text;
            client.Profession = txtProfession.Text;
            client.CompteTiers = txtCompteTiers.Text;
            //TypePieceIdentite = ((TypePieceIdentite)cmbTypePieces.SelectedItem),
            client.NumeroPieceIdentification = txtNumeroPiece.Text;
            client.DateDeDelivrancePiece = dDateDelivrancePiece;
            if(!bModifClient)
                client.Type = TypeClient.ProspectSansOption;
            client.Genre = rbHomme.Checked ? Genre.Masculin : Genre.Féminin;
            client.TelephoneFixe = txtNumeroFixe.Text.Replace(" ", string.Empty); ;
            client.Mobile1 = txtNumeroMobile.Text.Replace(" ", string.Empty); 
            client.TelephoneBureau = txtTelBureau.Text.Replace(" ", string.Empty);
            client.Fax = txtFax.Text.Replace(" ", string.Empty); 
            client.CommentaireProspect = txtCommentaireProspect.Text;
            if (!bModifClient)
                client.DateCreation = DateTime.Now;
            client.Email = txtEmail.Text;
            client.Actif = true;
            client.Origine = lOrigine != null? lOrigine : null;
            client.AutreOrigine = txtAutreOrigine.Text;
            client.SituationMatrimoniale = sMatrimonial;
            client.CompteTiers = txtCompteTiers.Text;
            if (sMatrimonial == SituationMatrimoniale.Marié)
            {
                client.PrenomConjoint = txtPrenomConjoint.Text;
                client.NomConjoint = txtNomConjoint.Text;
                client.DateDeNaissanceConjoint = dDateNaissanceConjoint.Date;
                client.LieuDeNaissanceConjoint = txtLieuNaissanceConjoint.Text;
                client.NationaliteConjoint = txtNationaliteConjoint.Text;
                client.DateMariage = dDateMariage.Date;
                client.LieuDeMariage = txtLieuMariage.Text;
                client.DateContratMariage = dDateContratMariage.Date;
                client.RegimeMatrimoniale = regimeMatrimonial;
                client.PrenomNotaire = txtPrenomNotaire.Text;
                client.NomNotaire = txtNomNotaire.Text;
                client.AdresseNotaire = txtAdresseNotaire.Text;
            }
            if (cmbTypePieces.SelectedItem != null)
            {
                client.TypePieceIdentite = ((TypePieceIdentite)cmbTypePieces.SelectedItem);
                client.NumeroPieceIdentification = txtNumeroPiece.Text;
                client.DateDeDelivrancePiece = dDateDelivrancePiece;
            }
            if (chkCooperative.Checked)
            {
                if (cmbCooperatives.SelectedItem != null)
                {
                    var coop = clientDAL.GetCooperative(((Cooperative)cmbCooperatives.SelectedItem).Id);
                    client.CooperativeId = coop.Id;
                    //client.Cooperative = coop;
                }
            }
            else
            {
                client.CooperativeId = null;
                client.Cooperative = null;
            }
            if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" && Tools.Tools.AgentEnCours.IsChefEquipe == false)
            {
                client.CommercialID = Tools.Tools.AgentEnCours.Id;
                client.ProjetId = Tools.Tools.AgentEnCours.ProjetId;
                client.ProspectAffecte = true;
            }
            db.SaveChanges();
        }

        private void AfficherClient(Client client)
        {
            if ((client.Origine!=null))
            {
                if (client.Origine.ClassOrigine == ClassOrigine.Perso)
                {
                    rbPerso.Checked = true;
                }
                else
                {
                    rbDesk.Checked = true;
                    cmbOrigines.SelectedIndex = cmbOrigines.FindStringExact(client.Origine.LibelleTypeOrigine);
                    lOrigineEncours = client.Origine;
                } 
            }

            dtpDateSouscription.Value = client.DateSouscription.Value.Date;
            txtPrenom.Text = client.Prenom;
            txtNom.Text = client.Nom;
            if (client.DateDeNaissance!= null && client.DateDeNaissance.Value.ToShortDateString() != "01/01/1900")
            {
                dtpDateNaissance.Value = client.DateDeNaissance.Value.Date;
            }
            
            txtLieuNaissance.Text = client.LieuDeNaissance;
            txtNationalite.Text = client.Nationalite;
            if (client.Genre == Genre.Masculin)
                rbHomme.Checked = true;
            else
                rbFemme.Checked = true;
            txtNumeroFixe.Text = client.TelephoneFixe;
            txtTelBureau.Text = client.TelephoneBureau;
            txtFax.Text = client.Fax;
            txtNumeroMobile.Text = client.Mobile1;
            cmbTypePieces.SelectedItem = client.TypePieceIdentite;
            txtNumeroPiece.Text = client.NumeroPieceIdentification;
            
            txtCompteTiers.Text = client.CompteTiers;
            if (client.DateDeDelivrancePiece.Value.ToShortDateString() != "01/01/1900")
            {
                dtpDateDelivrancePiece.Value = client.DateDeDelivrancePiece.Value.Date;
            }
            txtCommentaireProspect.Text = client.CommentaireProspect;
            txtEmail.Text = client.Email;
            txtAdresse.Text = client.Adresse;
            txtVille.Text = client.Ville;
            txtPays.Text = client.Pays;
            if(client.CountryId!=null)
                cmbPays.SelectedValue = client.CountryId;
            txtProfession.Text = client.Profession;
            if (client.Cooperative != null)
            {
                chkCooperative.Checked = true;
                cmbCooperatives.SelectedValue = ((Cooperative)client.Cooperative).Id;
            }
            else
            {
                chkCooperative.Checked = false;
                pCooperative.Visible = false;
                cmbCooperatives.SelectedIndex=-1;
            }

            if (client.SituationMatrimoniale == SituationMatrimoniale.Célibataire)
                rbCelibataire.Checked = true;
            else
                if (client.SituationMatrimoniale == SituationMatrimoniale.Marié)
                {
                    rbMarie.Checked = true;
                    txtPrenomConjoint.Text = client.PrenomConjoint;
                    txtNomConjoint.Text = client.NomConjoint;
                    dtpDateNaissanceConjoint.Value = client.DateDeNaissanceConjoint.Value ;
                    txtLieuNaissanceConjoint.Text = client.LieuDeNaissanceConjoint;
                    txtNationaliteConjoint.Text = client.NationaliteConjoint;
                    if (client.DateMariage != null &&  client.DateMariage.Value.ToShortDateString() != "01/01/1900")
                         dtpDateMariage.Value =client.DateMariage.Value;
                    txtLieuMariage.Text = client.LieuDeMariage;
                if (client.DateContratMariage != null && client.DateContratMariage.Value.ToShortDateString() != "01/01/1900")
                    dtpDateContratMariage.Value = client.DateContratMariage.Value;
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Panel2.Show();
                this.Size = new Size(922, 666);
                //pProspect.Size = new Size(880, 580);
                this.CenterToScreen();
                }
                else
                    if (client.SituationMatrimoniale == SituationMatrimoniale.Veuf)
                        rbVeuf.Checked = true;
                    else
                       if (client.SituationMatrimoniale == SituationMatrimoniale.Divorcé)
                        rbDivorcé.Checked = true;

            if (client.RegimeMatrimoniale == RegimeMatrimoniale.Communautaire)
                rbRegimeCommunautaire.Checked = true;
            else
              if (client.RegimeMatrimoniale == RegimeMatrimoniale.Séparation)
                rbRegimeSeparation.Checked = true;
              else
                 if (client.RegimeMatrimoniale == RegimeMatrimoniale.Autre)
                    rbRegimeAutre.Checked = true;
            txtPrenomNotaire.Text = client.PrenomNotaire;
            txtNomNotaire.Text = client.NomNotaire;
            txtAdresseNotaire.Text = client.AdresseNotaire;

            AfficherContratClient(client);
        }

        private void AfficherContratClient(Client client)
        {
            //dgContrats.DataSource = (from cont in db.Contrats
            //                         where cont.ClientID == client.ID
            //                         select new
            //                         {
            //                             Date = cont.DateSignatureContrat,
            //                             TypeVilla = cont.Villa.TypeVilla.CodeType,
            //                             Superficie = cont.Villa.Superficie,
            //                             Numéro = cont.Villa.Numerovilla,
            //                             PrixFinal = cont.PrixFinal,
            //                             Decaissement = cont.TypeContrat.LibelleTypeContrat

            //                         }).ToList();
            //dgContrats.Columns[1].HeaderText = "Type villa";
            //dgContrats.Columns[4].HeaderText = "Prix de vente";
            //dgContrats.Columns[4].DefaultCellStyle.Format = "### ### ###";
            //dgContrats.Columns[5].HeaderText = "Type contrat";
            //dgContrats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void cmdReserver_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void dgContrats_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgContrats_DoubleClick(object sender, EventArgs e)
        {
            FrmDossierClient childForm = new FrmDossierClient();
            childForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cmbOrigines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOrigines.SelectedItem != null)
            {
                lOrigineEncours=(TypeOrigine)cmbOrigines.SelectedItem;
                txtAutreOrigine.Text = string.Empty;
                pHeaderProspect.Enabled = true;
                pDetailsProspect.Enabled = true;
                txtPrenom.Focus();
                // txtAutreOrigine.Visible = lOrigineEncours == TypeOrigine.Autre ? true : false;
            }
        }

        private void FrmProspect_Load(object sender, EventArgs e)
        {
            
        }

        private void rbMarie_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMarie.Checked)
            {
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Panel2.Show();
                this.Size = new Size(922, 666);
                //pProspect.Size = new Size(880, 580);
               
                this.CenterToScreen();
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Hide();
                this.Size = new Size(470, 666);
                //pProspect.Size = new Size(459, 580);
                this.CenterToScreen();
            }
        }

        private void chkCooperative_CheckedChanged(object sender, EventArgs e)
        {
            if(chkCooperative.Checked)
            {
                pCooperative.Visible = true;
                txtTauxRemise.Text = string.Empty;
                cmbCooperatives.SelectedItem = -1;
            }
            else
            {
                pCooperative.Visible = false;
                txtTauxRemise.Text = string.Empty;
                cmbCooperatives.SelectedItem = -1;
            }
        }

        private void cmdSupprimer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show("Voulez vous réellement supprimer ce prospect?", "Gestion des prospects", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    clientDAL.DeleteClient(clientEnCours.ID);
                    MessageBox.Show("Le prospect a été supprimé avec succes ", "Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: "+ex.Message, "Gestion des prospects", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdEditer_Click(object sender, EventArgs e)
        {
            DeverouillerClient();
            bModifClient = true;
            //if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
            //{
            //    rbDesk.Enabled = false;
            //    rbPerso.Enabled = false;
            //    cmbOrigines.Enabled = false;
            //}
        }

        private void DeverouillerClient()
        {
          
            txtNom.ReadOnly = false;
            txtPrenom.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtAdresse.ReadOnly = false;
            txtNumeroMobile.ReadOnly = false;
            txtNumeroFixe.ReadOnly = false;
            txtTelBureau.ReadOnly = false;
            txtFax.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtCommentaireProspect.ReadOnly = false;
            txtLieuNaissance.ReadOnly = false;
            txtNationalite.ReadOnly = false;
            txtNationalite.ReadOnly = false;
            txtNumeroPiece.ReadOnly = false;
            txtCompteTiers.ReadOnly = false;
            txtProfession.ReadOnly = false;
            txtVille.ReadOnly = false;
            txtLieuMariage.ReadOnly = false;
            txtPrenomConjoint.ReadOnly = false;
            txtNomConjoint.ReadOnly = false;
            txtLieuNaissanceConjoint.ReadOnly = false;
            txtNationaliteConjoint.ReadOnly = false;
            txtPrenomNotaire.ReadOnly = false;
            txtNomNotaire.ReadOnly = false;
            txtAdresseNotaire.ReadOnly = false;

            txtPays.ReadOnly = false;
            cmbPays.Enabled = true;
            rbHomme.Enabled = true;
            rbFemme.Enabled = true;
            cmbCooperatives.Enabled = true;
            cmbOrigines.Enabled = true;
            cmbTypePieces.Enabled = true;
            chkCooperative.Enabled = true;
            rbCelibataire.Enabled = true;
            rbDivorcé.Enabled = true;
            rbMarie.Enabled = true;
            rbVeuf.Enabled = true;
            dtpDateNaissance.Enabled = true;
            dtpDateNaissanceConjoint.Enabled = true;
            dtpDateContratMariage.Enabled = true;
            dtpDateMariage.Enabled = true;
            dtpDateDelivrancePiece.Enabled = true;
            rbDesk.Enabled = true;
            rbPerso.Enabled = true;
            if(Tools.Tools.AgentEnCours.Role.CodeRole=="ADM")
            {
                txtCompteTiers.ReadOnly = false;
            }
            //cmdNouveau.Enabled = false;
            cmdEnregistrer.Enabled = true;
            cmdEditer.Enabled = true;
            cmdSupprimer.Enabled = true;
        }

        private void VerouillerForm()
        {
            
            txtNom.ReadOnly = true;
            txtPrenom.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtAdresse.ReadOnly = true;
            txtNumeroMobile.ReadOnly = true;
            txtNumeroFixe.ReadOnly = true;
            txtTelBureau.ReadOnly = true;
            txtFax.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtCommentaireProspect.ReadOnly = true;
            txtLieuNaissance.ReadOnly = true;
            txtNationalite.ReadOnly = true;
            txtNationalite.ReadOnly = true;
            txtNumeroPiece.ReadOnly = true;
            txtCompteTiers.ReadOnly = true;
            txtProfession.ReadOnly = true;
            txtVille.ReadOnly = true;
            txtLieuMariage.ReadOnly = true;
            txtPrenomConjoint.ReadOnly = true;
            txtNomConjoint.ReadOnly = true;
            txtLieuNaissanceConjoint.ReadOnly = true;
            txtNationaliteConjoint.ReadOnly = true;
            txtPrenomNotaire.ReadOnly = true;
            txtNomNotaire.ReadOnly = true;
            txtAdresseNotaire.ReadOnly = true;
            txtPays.ReadOnly = true;
            cmbPays.Enabled = false;
            rbHomme.Enabled = false;
            rbFemme.Enabled = false;
            cmbCooperatives.Enabled = false;
            cmbOrigines.Enabled = false;
            cmbTypePieces.Enabled = false;
            chkCooperative.Enabled = false;
            rbCelibataire.Enabled = false;
            rbDivorcé.Enabled = false;
            rbMarie.Enabled = false;
            rbVeuf.Enabled = false;
            dtpDateNaissance.Enabled = false;
            dtpDateDelivrancePiece.Enabled = false;
            dtpDateNaissanceConjoint.Enabled = false;
            dtpDateContratMariage.Enabled = false;
            dtpDateMariage.Enabled = false;
            rbDesk.Enabled = false;
            rbPerso.Enabled = false;
            txtCompteTiers.ReadOnly = true;
            // cmdNouveau.Enabled = true;
            cmdEnregistrer.Enabled = false;
            cmdEditer.Enabled = true;
            cmdSupprimer.Enabled = true;
        }

        private void cmbCooperatives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbCooperatives.SelectedItem!=null)
            { 
                txtTauxRemise.Text=((Cooperative)cmbCooperatives.SelectedItem).TauxRemise.ToString();
            }
        }

        private void dtpDateNaissance_ValueChanged(object sender, EventArgs e)
        {
            dtpDateNaissance.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateDelivrancePiece_ValueChanged(object sender, EventArgs e)
        {
            dtpDateDelivrancePiece.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateNaissanceConjoint_ValueChanged(object sender, EventArgs e)
        {
            dtpDateNaissanceConjoint.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateContratMariage_ValueChanged(object sender, EventArgs e)
        {
            dtpDateContratMariage.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateMariage_ValueChanged(object sender, EventArgs e)
        {
            dtpDateMariage.CustomFormat = "dd/MM/yyyy";
        }

        private void rbCelibataire_CheckedChanged(object sender, EventArgs e)
        {
            //this.Size = new Size(486, 666);
            //pProspect.Size = new Size(459, 580);
            //this.CenterToScreen();
        }

        private void rbVeuf_CheckedChanged(object sender, EventArgs e)
        {
            //this.Size = new Size(486, 666);
            //pProspect.Size = new Size(459, 580);
            //this.CenterToScreen();
        }

        private void rbDivorcé_CheckedChanged(object sender, EventArgs e)
        {
        //    this.Size = new Size(486, 666);
        //    pProspect.Size = new Size(459, 580);
        //    this.CenterToScreen();
        }

        private void dtpDateNaissance_Enter(object sender, EventArgs e)
        {
            dtpDateNaissance.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateNaissanceConjoint_Enter(object sender, EventArgs e)
        {
            dtpDateNaissanceConjoint.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateMariage_Enter(object sender, EventArgs e)
        {
            dtpDateMariage.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateContratMariage_Enter(object sender, EventArgs e)
        {
            dtpDateContratMariage.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDateDelivrancePiece_Enter(object sender, EventArgs e)
        {
            dtpDateDelivrancePiece.CustomFormat = "dd/MM/yyyy";
        }

        private void rbDesk_CheckedChanged(object sender, EventArgs e)
        {
            cmbOrigines.Visible = rbDesk.Checked;
            cmbOrigines.SelectedIndex = -1;
            pHeaderProspect.Enabled = false;
            pDetailsProspect.Enabled = false;
        }

        private void rbPerso_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPerso.Checked)
            {
                lOrigineEncours = clientDAL.GetTypeOriginePerso();
                pHeaderProspect.Enabled = true;
                pDetailsProspect.Enabled = true;
            }
        }

        private void cmdAffecter_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void txtAdresse_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPays_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtVille_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pHeaderProspect_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pDetailsProspect_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTelBureau_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click_1(object sender, EventArgs e)
        {

        }

        private void txtNationalite_Validated(object sender, EventArgs e)
        {
            if (txtNationalite.Text != string.Empty)
                txtNationalite.Text = Tools.Tools.UppercaseWords(txtNationalite.Text);
            txtNumeroMobile.Focus();
        }

        private void txtPrenom_Validated(object sender, EventArgs e)
        {
            if (txtPrenom.Text != string.Empty)
                txtPrenom.Text = Tools.Tools.UppercaseWords(txtPrenom.Text);
        }

        private void txtNom_Validated(object sender, EventArgs e)
        {
            if (txtNom.Text != string.Empty)
                txtNom.Text = txtNom.Text.ToUpper();
        }

        private void txtVille_Validated(object sender, EventArgs e)
        {
            if (txtVille.Text != string.Empty)
                txtVille.Text = Tools.Tools.UppercaseWords(txtVille.Text);
        }

        private void txtPays_Validated(object sender, EventArgs e)
        {
            if (txtPays.Text != string.Empty)
                txtPays.Text = txtPays.Text.ToUpper();
        }

        private void txtAdresse_Validated(object sender, EventArgs e)
        {
            if (txtAdresse.Text != string.Empty)
                txtAdresse.Text = Tools.Tools.UppercaseWords(txtAdresse.Text);
        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void txtCommentaireProspect_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbPays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPays.SelectedItem != null)
                txtPays.Text = ((Country)cmbPays.SelectedItem).CountryName.ToUpper();
        }
    }
}
