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

namespace prjSenImmoWinform
{
    public partial class FrmOption : Form
    {
        private Client leProspectEnCours;
        private Lot leLotEnCours;
        public FrmOption()
        {
            InitializeComponent();
        }
        public FrmOption(Client leProspect, Lot leLot):this()
        {
            try
            {

                leProspectEnCours = leProspect;
                leLotEnCours = leLot;
                AfficherProspect(leProspect);
                Afficherlot(leLot);
                dtpDatePriseOption.Value = DateTime.Today;
                dtpDatePriseOption_ValueChanged(this, new EventArgs());
                pDetailsOptions.Location = new Point(11, 151);
                this.Size = new Size(474, 419);
            }
            catch (Exception ex)
            {
                 MessageBox.Show(this, "Erreur:" + ex.Message,
                          "Prosopis -  Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AfficherProspect(Client prospect)
        {
            txtPrenom.Text = prospect.Prenom;
            txtNom.Text = prospect.Nom;
            txtDateNaissance.Text = prospect.DateDeNaissance.Value.Date.ToShortDateString();
            txtLieuNaissance.Text = prospect.LieuDeNaissance;
            txtAdresse.Text = prospect.Adresse;
            
        }

        private void Afficherlot(Lot lot)
        {
            try
            {
                txtNumeroLot.Text = lot.NumeroLot;
                txtTypeVilla.Text = lot.TypeVilla.NomType;
                txtSuperficieDeBase.Text = lot.TypeVilla.SurfaceDeBase.ToString();
                txtSuperficieReelle.Text = lot.Superficie.ToString();
                txtPosition.Text = lot.PositionLot.ToString();
                txtPrixStandard.Text = lot.TypeVilla.PrixStandard.ToString("0,0", CultureInfo.InvariantCulture);

                txtPrixRevise.Text = lot.PrixRevise.ToString("0,0", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dtpDatePriseOption_ValueChanged(object sender, EventArgs e)
        {
            txtDateFinPriseOption.Text = dtpDatePriseOption.Value.AddDays(30).Date.ToShortDateString();
            txtDureeOption.Text = "30";
        }

        private void txtDureeOption_Validated(object sender, EventArgs e)
        {
            if (txtDureeOption.Text!=string.Empty)
	        {
                try
                {
                    var duree = Int16.Parse(txtDureeOption.Text);
                    txtDateFinPriseOption.Text = dtpDatePriseOption.Value.AddDays(duree).Date.ToShortDateString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Erreur: veuillez saisir la durée sous forme de nombre de jours...:" + ex.Message,
                             "Prosopis -  Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
	        }
        }

        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDateFinPriseOption.Text == string.Empty || txtDureeOption.Text == string.Empty)
                {
                    MessageBox.Show(this, "Erreur: veuillez selectionner la date de début de la mise en option et saisir la durée sous forme de nombre de jours...",
                                 "Prosopis -  Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var duree = Int16.Parse(txtDureeOption.Text);
                var dateFinPriseOption = dtpDatePriseOption.Value.AddDays(duree).Date;
                var leCommercialEnCours = Tools.Tools.db.Commercials.Where(c => c.Id == 1).SingleOrDefault();

                var newOption = new Option()
                {

                    ClientID = leProspectEnCours.ID,

                    LotId = leLotEnCours.ID,

                    DatePriseOption = dtpDatePriseOption.Value.Date,
                    DateFinOption = dateFinPriseOption,

                    CommercialID = leCommercialEnCours.Id


                };
                ClientRepository clientDAL = new ClientRepository();
                clientDAL.AddProspetOption(newOption);

                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:..."+ex,
                                  "Prosopis -  Gestion des options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void FrmOption_Load(object sender, EventArgs e)
        {

        }
    }
}
