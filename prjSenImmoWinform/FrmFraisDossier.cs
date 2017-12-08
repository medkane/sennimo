using prjSenImmoWinform.DAL;
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
    public partial class FrmFraisDossier : Form
    {
        private Client leProspectEnCours;
        FactureRepository fr;
        public FrmFraisDossier()
        {
            InitializeComponent();
            fr = new FactureRepository();
        }

        public FrmFraisDossier(Client prospect ):this()
        {
            leProspectEnCours = prospect;
            AfficherProspect(prospect);
            txtMontantEncaissement.Text = "200000";
        }
        private void AfficherProspect(Client prospect)
        {
            txtPrenom.Text = prospect.Prenom;
            txtNom.Text = prospect.Nom;
            txtDateNaissance.Text = prospect.DateDeNaissance.Value.Date.ToShortDateString();
            txtLieuNaissance.Text = prospect.LieuDeNaissance;
            txtAdresse.Text = prospect.Adresse;

        }
        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                var montant = decimal.Parse(txtMontantEncaissement.Text);
                var newFacture = new FactureProspect()
                {
                    Motif = "Frais de dossier",
                    Date = dtpDateEncaissement.Value.Date,
                    Montant = montant,
                    TypeFacture = TypeFacture.FraisDossier,
                    ProspectId = leProspectEnCours.ID,

                };
                var newEncaissement = new EncaissementProspect()
                {
                    DateEncaissement = dtpDateEncaissement.Value.Date,
                    MontantGlobal = montant,
                    ProspectId = leProspectEnCours.ID,
                };
                newFacture.EncaissementsProspects.Add(newEncaissement);
                fr.Add(newFacture);

                this.Close();


            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void txtMontantEncaissement_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtMontantEncaissement.Text))
        //    {
        //        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
        //        int valueBefore = Int32.Parse(txtMontantEncaissement.Text, System.Globalization.NumberStyles.AllowThousands);
        //        txtMontantEncaissement.Text = String.Format(culture, "{0:N0}", valueBefore);
        //        txtMontantEncaissement.Select(txtMontantEncaissement.Text.Length, 0);
        //    } 
        //}
    }
}
