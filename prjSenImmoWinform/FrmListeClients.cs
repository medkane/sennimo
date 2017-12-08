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
    public partial class FrmListeClients : Form
    {
        private ClientRepository clientDAL;
        private CommercialRepository commercialRep;
        private ContratRepository contratRep;
        private ColumnHeader SortingColumn = null;
        private Projet LeProjetEncours;

        public FrmListeClients()
        {
            InitializeComponent();
            clientDAL = new ClientRepository();
            contratRep = new ContratRepository();
            var listeClients = clientDAL.GetAllClients(false);
            //if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
            //{
            //    if (Tools.Tools.AgentEnCours.IsChefEquipe)
            //        listeClients = listeClients.Where(pros => pros.Commercial.ChefEquipeId == Tools.Tools.AgentEnCours.Id).ToList();
            //    else
            //        listeClients = listeClients.Where(pros => pros.CommercialID == Tools.Tools.AgentEnCours.Id).ToList();
            //}
            commercialRep = new CommercialRepository();
            ChargerLesProjets();
            ChargerLesClients(listeClients);
            ChargerCommerciaux();
            cmbOrigines.DataSource = clientDAL.GetAllTypeOrigines().ToList();
            cmbOrigines.DisplayMember = "LibelleTypeOrigine";
            cmbOrigines.SelectedIndex = -1;
        }
        private void ChargerLesProjets()
        {

            try
            {
                var lesProjets = contratRep.GetProjets();


                cmbProjets.DataSource = lesProjets.ToList();
                cmbProjets.DisplayMember = "DenominationProjet";
                cmbProjets.ValueMember = "Id";
                // cmbProjets.SelectedValue = leProjetEnCours;
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" || Tools.Tools.AgentEnCours.Role.CodeRole == "DC")
                {
                    cmbProjets.SelectedValue = Tools.Tools.AgentEnCours.ProjetId;
                    LeProjetEncours = Tools.Tools.AgentEnCours.Projet;
                    cmbProjets.Enabled = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void ChargerLesClients(IEnumerable<Client> clients)
        {
            try
            {
                if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                {
                    if (Tools.Tools.AgentEnCours.IsChefEquipe)
                        clients = clients.ToList().Where(pros => pros.Commercial != null && pros.Commercial.ChefEquipeId == Tools.Tools.AgentEnCours.Id).ToList();
                    else
                    {
                        clients = clients.ToList().Where(pros => pros.CommercialID == Tools.Tools.AgentEnCours.Id).ToList();
                        chkCommercial.Visible = false;

                    }
                }
                if (cmbProjets.SelectedItem != null)
                {
                    Projet leProjet = (Projet)cmbProjets.SelectedItem;
                    if (leProjet != null)
                    {
                        clients = clients.Where(pros => pros.ProjetId == leProjet.Id).ToList();
                        if (leProjet.Id == 2)
                        {
                            if (rbConstructionVilla.Checked)
                                clients = clients.Where(pros => pros.Options.Where(opt => opt.Active == true).FirstOrDefault()?.TypeContrat.TypeConstruction == TypeConstruction.Villa);
                            else
                               if (rbConstructionAppartement.Checked)
                                clients = clients.Where(pros => pros.Options.Where(opt => opt.Active == true).FirstOrDefault()?.TypeContrat.TypeConstruction == TypeConstruction.Appartement);
                        }
                    }
                }

                //clients = clients.Where(client =>client.Contrats.Where(cont => cont.Statut == StatutContrat.Actif));

                if (rbOptionDepot.Checked)
                {

                    clients = clients.Where(client => client.Contrats.Where(cont => cont.Statut ==  StatutContrat.Actif).FirstOrDefault()
                                                     .TypeContrat.CategorieContrat == CategorieContrat.Dépôt);
                }
                else
                if (rbOptionResa.Checked)
                {
                    clients = clients.Where(client => client.Contrats.Where(cont => cont.Statut == StatutContrat.Actif).FirstOrDefault()
                                                                         .TypeContrat.CategorieContrat == CategorieContrat.Réservation);
                }
                if (rbOrigineDesk.Checked)
                {
                    clients = clients.ToList().Where(client => client.Origine.ClassOrigine == ClassOrigine.Desk).ToList();
                }
                else
                   if (rbOriginePerso.Checked)
                {
                    clients = clients.ToList().Where(client => client.Origine.ClassOrigine == ClassOrigine.Perso).ToList();
                }

                if (rbSoldes.Checked)
                {

                    clients = clients.Where(client => client.Contrats.Where(cont => cont.Statut == StatutContrat.Actif).FirstOrDefault()
                                                     .ContratSolde == true);
                }
                else
               if (rbEnCours.Checked)
                {
                    clients = clients.Where(client => client.Contrats.Where(cont => cont.Statut == StatutContrat.Actif).FirstOrDefault()
                                                                         .ContratSolde == false);
                }

                if (chkCommercial.Checked)
                {
                    if (cmbCommercial.SelectedItem != null)
                    {
                        Agent leCommercial = (Agent)cmbCommercial.SelectedItem;
                        if (leCommercial.IsChefEquipe == false)
                            clients = clients.Where(client => client.CommercialID == leCommercial.Id).ToList();
                        else
                            clients = clients.ToList().Where(client => client.Commercial!=null && client.Commercial.ChefEquipeId == leCommercial.Id).ToList();
                    }
                }

                if (cmbOrigines.SelectedItem != null)
                {
                    TypeOrigine lOrigine = (TypeOrigine)cmbOrigines.SelectedItem;
                    if (lOrigine != null)
                        clients = clients.ToList().Where(client => client.Origine!=null && client.Origine.TypeOrigineId == lOrigine.TypeOrigineId).ToList();
                }

                if (txtCompteTiers.Text != string.Empty)
                {
                    clients = clients.Where(client => client.CompteTiers != null && client.CompteTiers.ToUpper().Trim() == txtCompteTiers.Text.ToUpper().Trim());
                }

                if(chkAvenant.Checked)
                {
                    clients = clients.Where(client => client.Contrats.Any(cont => cont.Statut == StatutContrat.Désisté)).ToList();
                }

                if(txtNumeroDossier.Text != string.Empty)
                {
                    clients = clients.ToList().Where(client => client.Contrats!=null && client.Contrats.Any(cont => cont.NumeroContrat.Trim().ToUpper() == txtNumeroDossier.Text.Trim().ToUpper()
                        )).ToList();
                }
                lvClients.Items.Clear();
                int i = 1;
                foreach (var client in clients.ToList())
                {
                    try
                    {
                        ListViewItem lviClient = new ListViewItem(client.ID.ToString().PadLeft(4, '0'));
                        lviClient.SubItems.Add(client.NomComplet);
                        lviClient.SubItems.Add(client.Mobile1);
                        lviClient.SubItems.Add(client.Email);
                        lviClient.SubItems.Add(client.Adresse);
                        lviClient.SubItems.Add(client.DateSouscription.Value.ToShortDateString());
                        //lviClient.SubItems.Add(client.Contrats.FirstOrDefault(c => c.Statut == StatutContrat.Actif).DateReservation.Value.ToShortDateString());
                        lviClient.SubItems.Add(client.Contrats.Count.ToString());
                        if(client.Type!=TypeClient.Résilié)
                        { 
                            lviClient.SubItems.Add(client.Contrats.FirstOrDefault(c =>c.Statut== StatutContrat.Actif).TypeContrat.LibelleTypeContrat);
                            lviClient.SubItems.Add(client.Contrats.FirstOrDefault(c => c.Statut == StatutContrat.Actif).Lot.TypeVilla.CodeType);
                            lviClient.SubItems.Add(client.Contrats.FirstOrDefault(c => c.Statut == StatutContrat.Actif).TypeContrat.CategorieContrat == CategorieContrat.Réservation ? client.Contrats.FirstOrDefault(c => c.Statut == StatutContrat.Actif).Lot.NumeroLot : "");
                            lviClient.ImageIndex = client.Contrats.FirstOrDefault(c => c.Statut == StatutContrat.Actif).TypeContrat.CategorieContrat == CategorieContrat.Réservation ? 1 : 0;
                        }
                        else
                        {
                            lviClient.SubItems.Add(client.Contrats.FirstOrDefault(c => c.Statut == StatutContrat.Résilié).TypeContrat.LibelleTypeContrat);
                            lviClient.SubItems.Add(client.Contrats.FirstOrDefault(c => c.Statut == StatutContrat.Résilié).Lot.TypeVilla.CodeType);
                            lviClient.SubItems.Add(client.Contrats.FirstOrDefault(c => c.Statut == StatutContrat.Résilié).TypeContrat.CategorieContrat == CategorieContrat.Réservation ? client.Contrats.FirstOrDefault(c => c.Statut == StatutContrat.Résilié).Lot.NumeroLot : "");
                            lviClient.ImageIndex = client.Contrats.FirstOrDefault(c => c.Statut == StatutContrat.Résilié).TypeContrat.CategorieContrat == CategorieContrat.Réservation ? 1 : 0;
                        }
                        lviClient.Tag = client;


                        
                        if (!(Tools.Tools.AgentEnCours.Role.CodeRole == "CMC" && !Tools.Tools.AgentEnCours.IsChefEquipe))
                        {
                            if (client.Commercial != null)
                                lviClient.SubItems.Add(client.Commercial.NomComplet);
                        }
                        //lviClient.BackColor = (i % 2 != 0) ? Color.Honeydew : Color.White;
                        if (client.Type != TypeClient.Résilié)
                            if (client.Contrats.FirstOrDefault(c => c.Statut == StatutContrat.Actif).ContratSolde == true)
                                lviClient.BackColor = Color.LightGray;

                        i++;
                        lvClients.Items.Add(lviClient);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Erreur sur le client:" + client.NomComplet+ "..."+ ex.Message, "Prosopis - Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                        
                    }
                }
                txtNbProspects.Text = clients.Count().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors du chargement des clients:..."+ex.Message,
                         "Prosopis - Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgClients_SelectionChanged(object sender, EventArgs e)
        {

           
        }

        private void dgClients_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dgClients.SelectedRows.Count > 0)
                {
                    int idClient = (int)dgClients.SelectedRows[0].Cells[0].Value;
                    var client = clientDAL.GetClient(idClient);
                    if (client != null)
                    {
                        FrmDossierClient frmCli = new FrmDossierClient(client);
                        frmCli.MdiParent = this.MdiParent;
                        frmCli.Show();
                    }
                    else
                        MessageBox.Show(this, "Désolé ce client n'existe pas dans la base",
                            "Prosopis - Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors du chargement du client:... " + ex.Message,
                        "Prosopis - Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdNouveauClient_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdRechercherClients_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                RechercherClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors du chargement de la recherche de clients:..." + ex.Message,
                        "Prosopis - Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void RechercherClients()
        {
            try
            {
                if (txtNom.Text != string.Empty || txtPrenom.Text != string.Empty || txtTelMobile.Text != string.Empty 
                    || txtEmail.Text != string.Empty || chkResilie.Checked|| txtNumeroLot.Text!=string.Empty)

                    ChargerLesClients(clientDAL.GetClients(txtNom.Text, txtPrenom.Text,txtTelMobile.Text, txtEmail.Text, chkResilie.Checked,txtNumeroLot.Text));
                   
                else
                    ChargerLesClients(clientDAL.GetAllClients(chkResilie.Checked));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void txtPrenom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Cursor = Cursors.WaitCursor;
                //RechercherClients();
                this.Cursor = Cursors.Default;
            }
        }

        private void txtNom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Cursor = Cursors.WaitCursor;
                // RechercherClients();
                this.Cursor = Cursors.Default;
            }
        }

        private void dgClients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrenom_Validated(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            // RechercherClients();
            this.Cursor = Cursors.Default;
        }

        private void txtNom_Validated(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //RechercherClients();
            this.Cursor = Cursors.Default;
        }

        private void txtTelMobile_Validated(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //RechercherClients();
            this.Cursor = Cursors.Default;
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //RechercherClients();
            this.Cursor = Cursors.Default;
        }

        private void txtTelMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Cursor = Cursors.WaitCursor;
                //RechercherClients();
                this.Cursor = Cursors.Default;
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Cursor = Cursors.WaitCursor;
                //RechercherClients();
                this.Cursor = Cursors.Default;
            }
        }

        private void FrmListeClients_Load(object sender, EventArgs e)
        {
            lvClients_ColumnClick(sender, new ColumnClickEventArgs(0));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lvClients_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvClients_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (lvClients.SelectedItems.Count > 0)
                {

                    var client = (Client)lvClients.SelectedItems[0].Tag;
                    if (client != null)
                    {
                        FrmDossierClient frmCli = new FrmDossierClient(client);
                        frmCli.MdiParent = this.MdiParent;
                        frmCli.Show();
                    }
                    else
                        MessageBox.Show(this, "Désolé ce client n'existe pas dans la base",
                            "Prosopis - Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur lors du chargement du client:... " + ex.Message,
                        "Prosopis - Gestion des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rbOptionDepot_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOptionDepot.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                // RechercherClients();
                this.Cursor = Cursors.Default;
            }
        }

        private void rbOptionResa_CheckedChanged(object sender, EventArgs e)
        {

            if (rbOptionResa.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                //RechercherClients();
                this.Cursor = Cursors.Default;
            }
        }

        private void rbToutTypeOption_CheckedChanged(object sender, EventArgs e)
        {
            if (rbToutTypeOption.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                //RechercherClients();
                this.Cursor = Cursors.Default;
            }
        }

        private void chkCommercial_CheckedChanged(object sender, EventArgs e)
        {
            cmbCommercial.SelectedIndex = -1;
            cmbCommercial.Visible = chkCommercial.Checked;

            this.Cursor = Cursors.WaitCursor;
            //RechercherClients();
            this.Cursor = Cursors.Default;
        }
        private void ChargerCommerciaux()
        {

            var lesCommerciaux = commercialRep.GetAllCommerciaux();
            if (Tools.Tools.AgentEnCours.IsChefEquipe)
                lesCommerciaux = lesCommerciaux.Where(c => c.ChefEquipeId == Tools.Tools.AgentEnCours.Id);

            cmbCommercial.DataSource = lesCommerciaux.ToList();
            cmbCommercial.DisplayMember = "NomComplet";
        }

        private void cmbCommercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //RechercherClients();
            this.Cursor = Cursors.Default;
        }

        private void txtNbProspects_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkResilie_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkResilie.Checked)
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    //RechercherClients();
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void rbOriginePerso_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOriginePerso.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                //RechercherClients();
                this.Cursor = Cursors.Default;
            }
        }

        private void rbOrigineDesk_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOrigineDesk.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                //RechercherClients();
                pSource.Visible = true;
                this.Cursor = Cursors.Default;
            }
            else
            {
                pSource.Visible = false;
                cmbOrigines.SelectedIndex = -1;
            }
        }

        private void rbToutOrigine_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbToutOrigine.Checked)
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    RechercherClients();
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void cmbOrigines_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmdRechercherClients_Click(sender, e);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmListeClients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                cmdRechercherClients_Click(sender, e);
            }
        }

        private void lvClients_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvClients.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Descending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn.Text = SortingColumn.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn.Text = "> " + SortingColumn.Text;
            }
            else
            {
                SortingColumn.Text = "< " + SortingColumn.Text;
            }

            // Create a comparer.
            lvClients.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvClients.Sort();
        }

        private void txtNumeroLot_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cmbProjets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProjets.SelectedItem != null)
            {
                LeProjetEncours = cmbProjets.SelectedItem as Projet;
                rbToutTypeConstruction.Checked = true;
                if (LeProjetEncours.Id == 1)//AKYS
                {
                    rbConstructionVilla.Checked = true;
                    pTypeConstruction.Enabled = false;
                }
                else
                    pTypeConstruction.Enabled = true;


                ChargerCommerciaux();

            }
            else
            {
                LeProjetEncours = null;
                rbToutTypeConstruction.Checked = true;
                pTypeConstruction.Enabled = false;
            }
        }
    }
}
