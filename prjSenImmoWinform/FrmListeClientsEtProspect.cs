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
    public partial class FrmListeClientsEtProspect : Form
    {
        ClientRepository clientDAL;
        private Client leClientEnCours;

        public FrmListeClientsEtProspect()
        {
            InitializeComponent();
            clientDAL = new ClientRepository();
            ChargerLesClients();
        }

        private void ChargerLesClients()
        {
            try
            {
                dgClients.DataSource = clientDAL.GetAllClientsEtProspects().ToList().Select(cl => new
                {
                    id = cl.ID,
                    Numéro = cl.ID.ToString().PadLeft(4, '0'),
                    Prénom = cl.Prenom,
                    Nom = cl.Nom,
                    DateNaissance = cl.DateDeNaissance,
                    LieuNaissance = cl.LieuDeNaissance,
                    Téléphone = cl.Mobile1,
                    DateSouscription = cl.DateCreation.Date
                }
                ).ToList();
                dgClients.Columns[0].Width = 0;
                dgClients.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors du chargement des clients:..." + ex.Message,
                         "Prosopis - Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Client GetClientSelectionne()
        {
            try
            {
                return leClientEnCours;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgClients_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void dgClients_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgClients.SelectedRows.Count > 0)
                {
                    if (dgClients.SelectedRows[0].Cells[0].Value != null)
                    {
                        var clientId = dgClients.SelectedRows[0].Cells[0].Value;
                        int idClient = (int)clientId;
                        leClientEnCours = clientDAL.GetClient(idClient);
                        if(leClientEnCours==null)
                        {
                            MessageBox.Show(this, "Erreur:client ou prospect introuvable",
                         "Prosopis Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        else
                        {
                            this.Close();
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis Gestion des lots", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmdRechercherClients_Click(object sender, EventArgs e)
        {
            RechercherClientsOuProspects();
        }

        private void RechercherClientsOuProspects()
        {
            try
            {
                if (txtNom.Text != string.Empty || txtPrenom.Text != string.Empty)
                    dgClients.DataSource = clientDAL.GetClientsProspects(txtNom.Text, txtPrenom.Text).ToList();
                else
                    dgClients.DataSource = clientDAL.GetAllClientsEtProspects().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors du chargement de la recherche de clients:..." + ex.Message,
                        "Prosopis - Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNom_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
                RechercherClientsOuProspects();
        }

        private void txtPrenom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                RechercherClientsOuProspects();
        }
    }
}
