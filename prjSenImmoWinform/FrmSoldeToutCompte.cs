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
    public partial class FrmSoldeToutCompte : Form
    {
        ContratRepository contratRep;
        public FrmSoldeToutCompte()
        {
            InitializeComponent();
        }

        public FrmSoldeToutCompte(int contratId):this()
        {
            try
            {
                contratRep = new ContratRepository();
                var contrat = contratRep.GetContratById(contratId);
                if (contrat!=null)
                {
                    var soldeToutCompte = contratRep.GetSoldeToutCompte(contratId);
                    txtMontantARembourser.Text = soldeToutCompte.MontantARembourser.ToString("### ### ###");
                    txtPrixDeVente.Text = soldeToutCompte.PrixDeVente.ToString("### ### ###");
                    txtMontantTotalEncaisse.Text = soldeToutCompte.MontantTotalEncaisse.ToString("### ### ###");
                    txtTotalCommissionsVersees.Text = soldeToutCompte.MontantTotalCommissionsVersees.ToString("### ### ###");
                    txtMontantDepotGarantie.Text = soldeToutCompte.MontantDepotDeGarantie.ToString("### ### ###");

                    txtPrenom.Text = contrat.Client.Prenom;
                    txtNom.Text = contrat.Client.Nom;
                    txtDateNaissance.Text = contrat.Client.DateDeNaissance.Value.Date.ToShortDateString();
                    txtLieuNaissance.Text = contrat.Client.LieuDeNaissance;

                    txtNumeroLot.Text = contrat.Lot.NumeroLot;
                    txtCodeTypeVilla.Text = contrat.Lot.TypeVilla.CodeType;

                }
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

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}
