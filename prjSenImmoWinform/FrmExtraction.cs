using prjSenImmoWinform.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
namespace prjSenImmoWinform
{
    public partial class FrmExtraction : Form
    {
        private List<string> champs;
        public FrmExtraction()
        {
            InitializeComponent();
            champs = new List<string>();
            this.lvResult.Columns.Add("Numero");
            this.lvResult.Columns[0].Width = 0;
            dtpDebut.CustomFormat = " "; //An empty SPACE;
            dtpDebut.Format = DateTimePickerFormat.Custom;
            dtpFin.CustomFormat = " "; //An empty SPACE;
            dtpFin.Format = DateTimePickerFormat.Custom;
        }

        private void PopulateTreeView()
        {
            try
            {
            }
            catch (Exception)
            {

            }
        }

        private void listView2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string s = e.Item.ToString();
            DoDragDrop(s, DragDropEffects.Copy | DragDropEffects.Move);
          
        }
            
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
          
           
            //string typestring = "Type";
            //string s = e.Data.GetData(typestring.GetType()).ToString();
            //string orig_string = s;
            //s = s.Substring(s.IndexOf(":") + 1).Trim();
            //s = s.Substring(1, s.Length - 2);
            
            foreach (ListViewItem item  in lvFields.SelectedItems)
            {
                champs.Add(item.Text);
                this.lvResult.Columns.Add(item.Text);
                item.BackColor = Color.LightGray;
            }
            lvResult.Focus();
            cmdExtraire.Enabled = true;
            //lvResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

          

            //IEnumerator enumerator = listView2.Items.GetEnumerator();
            //int whichIdx = -1;
            //int idx = 0;
            //while (enumerator.MoveNext())
            //{
            //    string s2 = enumerator.Current.ToString();
            //    if (s2.Equals(orig_string))
            //    {
            //        whichIdx = idx;
            //        break;
            //    }
            //    idx++;
            //}
            //this.listView1.Items.RemoveAt(whichIdx);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void cmdExtraire_Click(object sender, EventArgs e)
        {
            ExtraireNew();
        }

        private void Extraire()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (var db = new SenImmoDataContext())
                {
                    if (rbClients.Checked)
                    {
                        var contrats = db.Contrats.ToList();
                        if (rbAKYS.Checked)
                        {
                            contrats = contrats.Where(cont => cont.ProjetId == 1).ToList();
                        }
                        else if (rbKERRIA.Checked)
                        {
                            contrats = contrats.Where(cont => cont.ProjetId == 2).ToList();
                        }
                        if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                        {
                            if (Tools.Tools.AgentEnCours.IsChefEquipe == false)
                                contrats = contrats.Where(cont => cont.Client.CommercialID == Tools.Tools.AgentEnCours.Id).ToList();
                            else
                                contrats = contrats.Where(cont => cont.Client.Commercial != null && cont.Client.Commercial.ChefEquipeId == Tools.Tools.AgentEnCours.Id).ToList();
                        }

                        #region Application des critères de filtre

                        foreach (ListViewItem item in lvCriteres.Items)
                        {

                            //string critere = cmbCriteres.SelectedItem.ToString();
                            string critere = item.Text;
                            switch (critere.ToLower())
                            {
                                case "origine":
                                    ClassOrigine typeOrigine = (ClassOrigine)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.Origine.ClassOrigine == typeOrigine).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.Origine.ClassOrigine != typeOrigine).ToList();
                                    break;
                                case "source":
                                    TypeOrigine origine = (TypeOrigine)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.Origine.TypeOrigineId == origine.TypeOrigineId).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.Origine.TypeOrigineId != origine.TypeOrigineId).ToList();
                                    break;
                                case "type villa":
                                    TypeVilla typeVilla = (TypeVilla)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Lot.TypeVillaID == typeVilla.TypeVillaId).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Lot.TypeVillaID != typeVilla.TypeVillaId).ToList();
                                    break;
                                case "type contrat":
                                    TypeContrat typeContrat = (TypeContrat)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.TypeContratID == typeContrat.ID).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.TypeContratID != typeContrat.ID).ToList();
                                    break;
                                case "commercial":
                                    Agent commercial = (Agent)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.CommercialID == commercial.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.CommercialID != commercial.Id).ToList();
                                    break;
                                case "chef d'équipe":
                                    Agent chefEquipe = (Agent)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Commercial.ChefEquipeId == chefEquipe.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Commercial.ChefEquipeId != chefEquipe.Id).ToList();
                                    break;
                                case "nature apporteur":
                                    TypeApporteur typeApp = (TypeApporteur)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Type == typeApp).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Type != typeApp).ToList();
                                    break;
                                case "apporteur d'affaires":
                                    ApporteurAffaire appAff = (ApporteurAffaire)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Id == appAff.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Id != appAff.Id).ToList();
                                    break;
                                case "coopérative":
                                    Cooperative coop = (Cooperative)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.Cooperative != null && c.Client.CooperativeId == coop.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.Cooperative != null && c.Client.CooperativeId != coop.Id).ToList();
                                    break;
                                case "pays":
                                    string pays = (string)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.Pays.ToLower() == pays.ToLower()).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.Pays.ToLower() != pays.ToLower()).ToList();
                                    break;
                                case "profession":
                                    string prof = (string)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.Profession.ToLower() == prof.ToLower()).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.Profession.ToLower() != prof.ToLower()).ToList();
                                    break;

                                case "remise":
                                    string remise = (string)item.Tag;
                                    decimal dRemise = decimal.Parse(remise);
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.RemiseAccordee == dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.RemiseAccordee != dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "supérieur ou égal")
                                        contrats = contrats.Where(c => c.RemiseAccordee >= dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "inférieur ou égal")
                                        contrats = contrats.Where(c => c.RemiseAccordee <= dRemise).ToList();
                                    break;
                                case "prix de vente":
                                    string prixDeVente = (string)item.Tag;
                                    decimal dPrixDeVente = decimal.Parse(prixDeVente);
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.PrixFinal == dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.PrixFinal != dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "supérieur ou égal")
                                        contrats = contrats.Where(c => c.PrixFinal >= dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "inférieur ou égal")
                                        contrats = contrats.Where(c => c.PrixFinal <= dPrixDeVente).ToList();
                                    break;
                                case "date entrée":
                                    DateTime[] dates = (DateTime[])item.Tag;
                                    contrats = contrats.Where(c => c.Client.DateSouscription >= dates[0]
                                                                 && c.Client.DateSouscription <= dates[1]).ToList();
                                    break;
                                case "date création":
                                    DateTime[] datesCreation = (DateTime[])item.Tag;
                                    contrats = contrats.Where(c => c.Client.DateCreation >= datesCreation[0]
                                                                 && c.Client.DateCreation <= datesCreation[1]).ToList();
                                    break;
                                case "date dépôt":
                                    DateTime[] datesDepot = (DateTime[])item.Tag;
                                    contrats = contrats.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && c.DateSouscription >= datesDepot[0]
                                                                 && c.DateSouscription <= datesDepot[1]).ToList();
                                    break;
                                case "date réservation":
                                    DateTime[] datesReservation = (DateTime[])item.Tag;
                                    contrats = contrats.Where(c => c.DateReservation >= datesReservation[0]
                                                                 && c.DateReservation <= datesReservation[1]).ToList();
                                    break;
                                case "date livraison":
                                    DateTime[] datesLivraison = (DateTime[])item.Tag;
                                    contrats = contrats.Where(c => c.DateLivraisonLot != null && c.DateLivraisonLot >= datesLivraison[0]
                                                                 && c.DateLivraisonLot <= datesLivraison[1]).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion

                        #region Affichage du résultat
                        lvResult.Items.Clear();
                        int i = 1;
                        foreach (var contrat in contrats.ToList())
                        {
                            ListViewItem lviClient = new ListViewItem(contrat.NumeroContrat);
                            foreach (var champ in champs)
                            {
                                if (champ == "Prénom")
                                    lviClient.SubItems.Add(contrat.Client.Prenom);
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Nom")
                                    lviClient.SubItems.Add(contrat.Client.Nom);
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Statut")
                                    lviClient.SubItems.Add("Client");
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Téléphone")
                                    if (contrat.Client.Mobile1 != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Mobile1);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Adresse")
                                    if (contrat.Client.Adresse != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Adresse);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Mail")
                                    if (contrat.Client.Email != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Email);

                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Origine")
                                    if (contrat.Client.Origine != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Origine.ClassOrigine.ToString());

                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Source")
                                    if (contrat.Client.Origine != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Origine.LibelleTypeOrigine);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Nationalité")
                                    lviClient.SubItems.Add(contrat.Client.Nationalite);
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Pays")
                                    lviClient.SubItems.Add(contrat.Client.Pays);
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Profession")
                                    lviClient.SubItems.Add(contrat.Client.Profession);
                            }

                            foreach (var champ in champs)
                            {
                                if (champ == "Date entrée")
                                    if (contrat.Client.DateSouscription != null)
                                        lviClient.SubItems.Add(contrat.Client.DateSouscription.Value.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");
                            }

                            foreach (var champ in champs)
                            {
                                if (champ == "Date création")
                                    if (contrat.Client.DateCreation != null)
                                        lviClient.SubItems.Add(contrat.Client.DateCreation.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Date dépôt")
                                    if (contrat.DateSouscription != null && contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                                        lviClient.SubItems.Add(contrat.DateSouscription.Value.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Date réservation")
                                    if (contrat.DateReservation != null)
                                        lviClient.SubItems.Add(contrat.DateReservation.Value.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Type contrat")
                                    lviClient.SubItems.Add(contrat.TypeContrat.LibelleTypeContrat);
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Type Villa")
                                    lviClient.SubItems.Add(contrat.Lot.TypeVilla.CodeType);
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Prix de vente")
                                    lviClient.SubItems.Add(contrat.PrixFinal.ToString("### ### ###0"));
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Remise")
                                    lviClient.SubItems.Add(contrat.RemiseAccordee.ToString("### ### ##0"));
                            }

                            foreach (var champ in champs)
                            {
                                if (champ == "Date livraison")
                                    if (contrat.DateLivraisonLot != null)
                                        lviClient.SubItems.Add(contrat.DateLivraisonLot.Value.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Montant versé")
                                    lviClient.SubItems.Add(contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ##0"));
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Commercial")
                                    lviClient.SubItems.Add(contrat.Client.Commercial.NomComplet);
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Chef d'équipe")
                                    lviClient.SubItems.Add(contrat.Client.Commercial.ChefEquipe.NomComplet);
                            }

                            foreach (var champ in champs)
                            {
                                if (champ == "Nature apporteur")
                                    if (contrat.Apporteur != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Apporteur.Type.ToString());
                                        //lviClient.SubItems.Add(contrat.Apporteur.NomComplet);
                                        //lviClient.SubItems.Add(contrat.Apporteur.TauxCommission.ToString("##0"));
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                        //lviClient.SubItems.Add("");
                                        //lviClient.SubItems.Add("");
                                    }
                            }

                            foreach (var champ in champs)
                            {
                                if (champ == "Apporteur d'affaires")
                                    if (contrat.Apporteur != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Apporteur.NomComplet);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                            }

                            foreach (var champ in champs)
                            {
                                if (champ == "Taux apporteur")
                                    if (contrat.Apporteur != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Apporteur.TauxCommission.ToString("##0"));
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                            }

                            foreach (var champ in champs)
                            {
                                if (champ == "Coopérative")
                                    if (contrat.Client.Cooperative != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Cooperative.Denomination);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                            }
                            foreach (var champ in champs)
                            {
                                if (champ == "Taux coopérative")
                                    if (contrat.Client.Cooperative != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Cooperative.TauxRemise.ToString("##0"));
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                            }

                            lvResult.Items.Add(lviClient);
                        }
                        #endregion 
                       
                    }
                    else
                    {
                        var prospects = db.Clients.Where(pros => pros.Type == TypeClient.ProspectAvecOptionDepot
                                        || pros.Type == TypeClient.ProspectAvecOptionResa || pros.Type == TypeClient.ProspectSansOption).ToList();
                        if (rbAKYS.Checked)
                        {
                            prospects = prospects.Where(pros => pros.ProjetId == 1).ToList();
                        }
                        else if (rbKERRIA.Checked)
                        {
                            prospects = prospects.Where(pros => pros.ProjetId == 2).ToList();
                        }
                        if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                        {
                            if (Tools.Tools.AgentEnCours.IsChefEquipe == false)
                                prospects = prospects.Where(pros => pros.CommercialID == Tools.Tools.AgentEnCours.Id).ToList();
                            else
                                prospects = prospects.Where(pros => pros.Commercial != null && pros.Commercial.ChefEquipeId == Tools.Tools.AgentEnCours.Id).ToList();
                        }
                        #region Application des critères de filtre

                        foreach (ListViewItem item in lvCriteres.Items)
                        {

                            //string critere = cmbCriteres.SelectedItem.ToString();
                            string critere = item.Text;
                            //var option = prospect.Options.Where(opt => opt.Active == true).FirstOrDefault();
                            switch (critere.ToLower())
                            {

                                case "origine":
                                    ClassOrigine typeOrigine = (ClassOrigine)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Origine.ClassOrigine == typeOrigine).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Origine.ClassOrigine != typeOrigine).ToList();
                                    break;
                                case "source":
                                    TypeOrigine origine = (TypeOrigine)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Origine.TypeOrigineId == origine.TypeOrigineId).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Origine.TypeOrigineId != origine.TypeOrigineId).ToList();
                                    break;
                                case "type villa":
                                    TypeVilla typeVilla = (TypeVilla)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVillaId == typeVilla.TypeVillaId).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVillaId != typeVilla.TypeVillaId).ToList();
                                    break;
                                case "type contrat":
                                    TypeContrat typeContrat = (TypeContrat)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeContratId == typeContrat.ID).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeContratId != typeContrat.ID).ToList();
                                    break;
                                case "commercial":
                                    Agent commercial = (Agent)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.CommercialID == commercial.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.CommercialID != commercial.Id).ToList();
                                    break;
                                case "chef d'équipe":
                                    Agent chefEquipe = (Agent)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Commercial.ChefEquipeId == chefEquipe.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Commercial.ChefEquipeId != chefEquipe.Id).ToList();
                                    break;
                                //case "nature apporteur":
                                //    TypeApporteur typeApp = (TypeApporteur)item.Tag;
                                //    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                //        prospects = prospects.Where(c => c.Apporteur != null && c.Apporteur.Type == typeApp).ToList();
                                //    else
                                //        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                //        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Type != typeApp).ToList();
                                //    break;
                                //case "apporteur d'affaires":
                                //    ApporteurAffaire appAff = (ApporteurAffaire)item.Tag;
                                //    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                //        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Id == appAff.Id).ToList();
                                //    else
                                //        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                //        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Id != appAff.Id).ToList();
                                //    break;
                                case "coopérative":
                                    Cooperative coop = (Cooperative)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Cooperative != null && c.CooperativeId == coop.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Cooperative != null && c.CooperativeId != coop.Id).ToList();
                                    break;
                                case "pays":
                                    string pays = (string)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Pays.ToLower() == pays.ToLower()).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Pays.ToLower() != pays.ToLower()).ToList();
                                    break;
                                case "profession":
                                    string prof = (string)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Profession.ToLower() == prof.ToLower()).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Profession.ToLower() != prof.ToLower()).ToList();
                                    break;

                                case "remise":
                                    string remise = (string)item.Tag;
                                    decimal dRemise = decimal.Parse(remise);
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().MontantRemiseAccordee == dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().MontantRemiseAccordee != dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "supérieur ou égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().MontantRemiseAccordee >= dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "inférieur ou égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().MontantRemiseAccordee <= dRemise).ToList();
                                    break;
                                case "prix de vente":
                                    string prixDeVente = (string)item.Tag;
                                    decimal dPrixDeVente = decimal.Parse(prixDeVente);
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente == dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente != dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "supérieur ou égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente >= dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "inférieur ou égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente <= dPrixDeVente).ToList();
                                    break;
                                case "date entrée":
                                    DateTime[] dates = (DateTime[])item.Tag;
                                    prospects = prospects.Where(c => c.DateSouscription >= dates[0]
                                                                 && c.DateSouscription <= dates[1]).ToList();
                                    break;
                                case "date création":
                                    DateTime[] datesCreation = (DateTime[])item.Tag;
                                    prospects = prospects.Where(c => c.DateCreation >= datesCreation[0]
                                                                 && c.DateCreation <= datesCreation[1]).ToList();
                                    break;
                                    //case "date dépôt":
                                    //    DateTime[] datesDepot = (DateTime[])item.Tag;
                                    //    contrats = contrats.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && c.DateSouscription >= datesDepot[0]
                                    //                                 && c.DateSouscription <= datesDepot[1]).ToList();
                                    //    break;
                                    //case "date réservation":
                                    //    DateTime[] datesReservation = (DateTime[])item.Tag;
                                    //    contrats = contrats.Where(c => c.DateReservation >= datesReservation[0]
                                    //                                 && c.DateReservation <= datesReservation[1]).ToList();
                                    //    break;
                                    //case "date livraison":
                                    //    DateTime[] datesLivraison = (DateTime[])item.Tag;
                                    //    contrats = contrats.Where(c => c.DateLivraisonLot != null && c.DateLivraisonLot >= datesLivraison[0]
                                    //                                 && c.DateLivraisonLot <= datesLivraison[1]).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion

                        #region PROSPECT: AFFICHAGE RESULTATS 

                        lvResult.Items.Clear();
                        int i = 1;
                        foreach (var prospect in prospects.ToList())
                        {
                            ListViewItem lviClient = new ListViewItem(prospect.NumeroClient);
                            foreach (var champ in champs)
                            {
                                if (champ == "Prénom")
                                    lviClient.SubItems.Add(prospect.Prenom);
                                if (champ == "Nom")
                                    lviClient.SubItems.Add(prospect.Nom);
                                if (champ == "Statut")
                                    lviClient.SubItems.Add("Prospect");

                                if (champ == "Téléphone")
                                    lviClient.SubItems.Add(prospect.Mobile1);
                                if (champ == "Adresse")
                                    lviClient.SubItems.Add(prospect.Adresse);
                                if (champ == "Mail")
                                    lviClient.SubItems.Add(prospect.Email);

                                if (champ == "Origine")
                                    if (prospect.Origine != null)
                                    {
                                        lviClient.SubItems.Add(prospect.Origine.ClassOrigine.ToString());
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                                if (champ == "Source")
                                    if (prospect.Origine != null)
                                    {
                                        lviClient.SubItems.Add(prospect.Origine.LibelleTypeOrigine);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                                if (champ == "Nationalité")
                                    lviClient.SubItems.Add(prospect.Nationalite);
                                if (champ == "Pays")
                                    lviClient.SubItems.Add(prospect.Pays);
                                if (champ == "Profession")
                                    lviClient.SubItems.Add(prospect.Profession);
                                if (champ == "Date entrée")
                                    if (prospect.DateSouscription != null)
                                        lviClient.SubItems.Add(prospect.DateSouscription.Value.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Date création")
                                    if (prospect.DateCreation != null)
                                        lviClient.SubItems.Add(prospect.DateCreation.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Date dépôt")
                                    lviClient.SubItems.Add("");

                                if (champ == "Date réservation")
                                    lviClient.SubItems.Add("");

                                var option = prospect.Options.Where(opt => opt.Active == true).FirstOrDefault();
                                //if (option != null)
                                //{

                                if (champ == "Type contrat")
                                    if (option != null)
                                        lviClient.SubItems.Add(option.TypeContrat.LibelleTypeContrat);
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Type Villa")
                                    if (option != null)
                                        lviClient.SubItems.Add(option.Lot.TypeVilla.CodeType);
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Prix de vente")
                                    if (option != null)
                                        lviClient.SubItems.Add(option.PrixDeVente.ToString("### ### ###"));
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Remise")
                                    if (option != null)
                                        lviClient.SubItems.Add(option.MontantRemiseAccordee.ToString("### ### ##0"));
                                    else
                                        lviClient.SubItems.Add("");

                                if (champ == "Date livraison")
                                    lviClient.SubItems.Add("");
                                if (champ == "Montant versé")
                                    lviClient.SubItems.Add(prospect.EncaissementProspects.Where(enc => enc.FraisDeDossier == false).Sum(enc => enc.MontantGlobal).ToString("### ### ##0"));
                                if (champ == "Commercial")
                                    if (prospect.Commercial != null)
                                        lviClient.SubItems.Add(prospect.Commercial.NomComplet);
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Chef d'équipe")
                                    if (prospect.Commercial.ChefEquipe != null)
                                        lviClient.SubItems.Add(prospect.Commercial.ChefEquipe.NomComplet);
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Nature apporteur" || champ == "Apporteur d'affaires" || champ == "Taux apporteur")
                                    lviClient.SubItems.Add("");
                                if (champ == "Coopérative")
                                    if (prospect.Cooperative != null)
                                        lviClient.SubItems.Add(prospect.Cooperative.Denomination);
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Taux coopérative")
                                    if (prospect.Cooperative != null)
                                        lviClient.SubItems.Add(prospect.Cooperative.TauxRemise.ToString("##0"));
                                    else
                                        lviClient.SubItems.Add("");
                            }
                            lvResult.Items.Add(lviClient);
                        }
                        #endregion
                        txtTotal.Text = prospects.Count.ToString("### ##0");
                    }
                }
                cmdExporter.Enabled = true;
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


        private void ExtraireNew()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (var db = new SenImmoDataContext())
                {
                    if (rbClients.Checked)
                    {
                        var contrats = db.Contrats.Where(cont => cont.Statut != StatutContrat.Désisté && cont.Statut != StatutContrat.Résilié).ToList();
                        if (rbAKYS.Checked)
                        {
                            contrats = contrats.Where(cont => cont.ProjetId == 1).ToList();
                        }
                        else if (rbKERRIA.Checked)
                        {
                            contrats = contrats.Where(cont => cont.ProjetId == 2).ToList();
                        }
                        if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                        {
                            if (Tools.Tools.AgentEnCours.IsChefEquipe == false)
                                contrats = contrats.Where(cont => cont.Client.CommercialID == Tools.Tools.AgentEnCours.Id).ToList();
                            else
                                contrats = contrats.Where(cont => cont.Client.Commercial != null && cont.Client.Commercial.ChefEquipeId == Tools.Tools.AgentEnCours.Id).ToList();
                        }

                        #region Application des critères de filtre

                        foreach (ListViewItem item in lvCriteres.Items)
                        {

                            //string critere = cmbCriteres.SelectedItem.ToString();
                            string critere = item.Text;
                            switch (critere.ToLower())
                            {
                                case "origine":
                                    ClassOrigine typeOrigine = (ClassOrigine)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.Origine.ClassOrigine == typeOrigine).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.Origine.ClassOrigine != typeOrigine).ToList();
                                    break;
                                case "source":
                                    TypeOrigine origine = (TypeOrigine)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.Origine.TypeOrigineId == origine.TypeOrigineId).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.Origine.TypeOrigineId != origine.TypeOrigineId).ToList();
                                    break;
                                case "type villa":
                                    TypeVilla typeVilla = (TypeVilla)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Lot.TypeVillaID == typeVilla.TypeVillaId).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Lot.TypeVillaID != typeVilla.TypeVillaId).ToList();
                                    break;
                                case "type contrat":
                                    TypeContrat typeContrat = (TypeContrat)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.TypeContratID == typeContrat.ID).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.TypeContratID != typeContrat.ID).ToList();
                                    break;
                                case "commercial":
                                    Agent commercial = (Agent)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.CommercialID == commercial.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.CommercialID != commercial.Id).ToList();
                                    break;
                                case "chef d'équipe":
                                    Agent chefEquipe = (Agent)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Commercial.ChefEquipeId == chefEquipe.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Commercial.ChefEquipeId != chefEquipe.Id).ToList();
                                    break;
                                case "nature apporteur":
                                    TypeApporteur typeApp = (TypeApporteur)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Type == typeApp).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Type != typeApp).ToList();
                                    break;
                                case "apporteur d'affaires":
                                    ApporteurAffaire appAff = (ApporteurAffaire)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Id == appAff.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Id != appAff.Id).ToList();
                                    break;
                                case "coopérative":
                                    Cooperative coop = (Cooperative)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.Cooperative != null && c.Client.CooperativeId == coop.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.Cooperative != null && c.Client.CooperativeId != coop.Id).ToList();
                                    break;
                                //case "pays":
                                //    string pays = (string)item.Tag;
                                //    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                //        contrats = contrats.Where(c => c.Client.Pays.ToLower() == pays.ToLower()).ToList();
                                //    else
                                //        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                //        contrats = contrats.Where(c => c.Client.Pays.ToLower() != pays.ToLower()).ToList();
                                //    break;
                                case "pays":
                                    Country pays = (Country)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.Country != null && c.Client.Country.Id == pays.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.Country.Id != pays.Id).ToList();
                                    break;
                                case "profession":
                                    string prof = (string)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.Client.Profession.ToLower() == prof.ToLower()).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.Client.Profession.ToLower() != prof.ToLower()).ToList();
                                    break;

                                case "remise":
                                    string remise = (string)item.Tag;
                                    decimal dRemise = decimal.Parse(remise);
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.RemiseAccordee == dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.RemiseAccordee != dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "supérieur ou égal")
                                        contrats = contrats.Where(c => c.RemiseAccordee >= dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "inférieur ou égal")
                                        contrats = contrats.Where(c => c.RemiseAccordee <= dRemise).ToList();
                                    break;
                                case "prix de vente":
                                    string prixDeVente = (string)item.Tag;
                                    decimal dPrixDeVente = decimal.Parse(prixDeVente);
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        contrats = contrats.Where(c => c.PrixFinal == dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        contrats = contrats.Where(c => c.PrixFinal != dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "supérieur ou égal")
                                        contrats = contrats.Where(c => c.PrixFinal >= dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "inférieur ou égal")
                                        contrats = contrats.Where(c => c.PrixFinal <= dPrixDeVente).ToList();
                                    break;
                                case "date entrée":
                                    DateTime[] dates = (DateTime[])item.Tag;
                                    contrats = contrats.Where(c => c.Client.DateSouscription >= dates[0]
                                                                 && c.Client.DateSouscription <= dates[1]).ToList();
                                    break;
                                case "date création":
                                    DateTime[] datesCreation = (DateTime[])item.Tag;
                                    contrats = contrats.Where(c => c.Client.DateCreation >= datesCreation[0]
                                                                 && c.Client.DateCreation <= datesCreation[1]).ToList();
                                    break;
                                case "date dépôt":
                                    DateTime[] datesDepot = (DateTime[])item.Tag;
                                    contrats = contrats.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && c.DateSouscription >= datesDepot[0]
                                                                 && c.DateSouscription <= datesDepot[1]).ToList();
                                    break;
                                case "date réservation":
                                    DateTime[] datesReservation = (DateTime[])item.Tag;
                                    contrats = contrats.Where(c => c.DateReservation >= datesReservation[0]
                                                                 && c.DateReservation <= datesReservation[1]).ToList();
                                    break;
                                case "date livraison":
                                    DateTime[] datesLivraison = (DateTime[])item.Tag;
                                    contrats = contrats.Where(c => c.DateLivraisonLot != null && c.DateLivraisonLot >= datesLivraison[0]
                                                                 && c.DateLivraisonLot <= datesLivraison[1]).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion

                        #region Affichage du résultat
                        lvResult.Items.Clear();
                        int i = 1;
                        foreach (var contrat in contrats.ToList())
                        {
                            ListViewItem lviClient = new ListViewItem(contrat.NumeroContrat);
                            foreach (var champ in champs)
                            {
                                if (champ == "Prénom")
                                    lviClient.SubItems.Add(contrat.Client.Prenom);

                                if (champ == "Nom")
                                    lviClient.SubItems.Add(contrat.Client.Nom);

                                if (champ == "Statut")
                                    lviClient.SubItems.Add("Client");
                                if (champ == "Téléphone")
                                    if (contrat.Client.Mobile1 != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Mobile1);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }

                                if (champ == "Adresse")
                                    if (contrat.Client.Adresse != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Adresse);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }

                                if (champ == "Mail")
                                    if (contrat.Client.Email != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Email);

                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }

                                if (champ == "Origine")
                                    if (contrat.Client.Origine != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Origine.ClassOrigine.ToString());

                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }

                                if (champ == "Source")
                                    if (contrat.Client.Origine != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Origine.LibelleTypeOrigine);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }

                                if (champ == "Nationalité")
                                    lviClient.SubItems.Add(contrat.Client.Nationalite)
                                        ;

                                if (champ == "Pays")
                                    lviClient.SubItems.Add(contrat.Client.Pays);

                                if (champ == "Profession")
                                    lviClient.SubItems.Add(contrat.Client.Profession);

                                if (champ == "Date entrée")
                                    if (contrat.Client.DateSouscription != null)
                                        lviClient.SubItems.Add(contrat.Client.DateSouscription.Value.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");

                                if (champ == "Date création")
                                    if (contrat.Client.DateCreation != null)
                                        lviClient.SubItems.Add(contrat.Client.DateCreation.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");

                                if (champ == "Date dépôt")
                                    if (contrat.DateSouscription != null && contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                                        lviClient.SubItems.Add(contrat.DateSouscription.Value.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");

                                if (champ == "Date réservation")
                                    if (contrat.DateReservation != null)
                                        lviClient.SubItems.Add(contrat.DateReservation.Value.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");

                                if (champ == "Type contrat")
                                    lviClient.SubItems.Add(contrat.TypeContrat.LibelleTypeContrat);

                                if (champ == "Type Villa")
                                    lviClient.SubItems.Add(contrat.Lot.TypeVilla.CodeType);

                                if (champ == "Numéro Lot")
                                    if (!contrat.Lot.LotVirtuel)
                                        lviClient.SubItems.Add(contrat.Lot.NumeroLot);
                                    else
                                        lviClient.SubItems.Add("");

                                if (champ == "Prix de vente")
                                    lviClient.SubItems.Add(contrat.PrixFinal.ToString("### ### ##0"));

                                if (champ == "Remise")
                                    lviClient.SubItems.Add(contrat.RemiseAccordee.ToString("### ### ##0"));

                                if (champ == "Date livraison")
                                    if (contrat.DateLivraisonLot != null)
                                        lviClient.SubItems.Add(contrat.DateLivraisonLot.Value.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");

                                if (champ == "Montant versé")
                                    lviClient.SubItems.Add(contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal).ToString("### ### ##0"));

                                if (champ == "Commercial")
                                    lviClient.SubItems.Add(contrat.Client.Commercial.NomComplet);

                                if (champ == "Chef d'équipe")
                                    if (contrat.Client.Commercial.ChefEquipe != null)
                                        lviClient.SubItems.Add(contrat.Client.Commercial.ChefEquipe.NomComplet);
                                    else
                                        lviClient.SubItems.Add("");

                                if (champ == "Nature apporteur")
                                    if (contrat.Apporteur != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Apporteur.Type.ToString());
                                        //lviClient.SubItems.Add(contrat.Apporteur.NomComplet);
                                        //lviClient.SubItems.Add(contrat.Apporteur.TauxCommission.ToString("##0"));
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                        //lviClient.SubItems.Add("");
                                        //lviClient.SubItems.Add("");
                                    }

                                if (champ == "Apporteur d'affaires")
                                    if (contrat.Apporteur != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Apporteur.NomComplet);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }

                                if (champ == "Taux apporteur")
                                    if (contrat.Apporteur != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Apporteur.TauxCommission.ToString("##0"));
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }

                                if (champ == "Coopérative")
                                    if (contrat.Client.Cooperative != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Cooperative.Denomination);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }

                                if (champ == "Taux coopérative")
                                    if (contrat.Client.Cooperative != null)
                                    {
                                        lviClient.SubItems.Add(contrat.Client.Cooperative.TauxRemise.ToString("##0"));
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }

                            }
                            lvResult.Items.Add(lviClient);
                        }
                        #endregion
                        txtTotal.Text = contrats.Count.ToString("### ##0");
                    }
                    else
                    {
                        var prospects = db.Clients.Where(pros => pros.Type == TypeClient.ProspectAvecOptionDepot
                                        || pros.Type == TypeClient.ProspectAvecOptionResa || pros.Type == TypeClient.ProspectSansOption).ToList();
                        if (rbAKYS.Checked)
                        {
                            prospects = prospects.Where(pros => pros.ProjetId == 1).ToList();
                        }
                        else if (rbKERRIA.Checked)
                        {
                            prospects = prospects.Where(pros => pros.ProjetId == 2).ToList();
                        }
                        if (Tools.Tools.AgentEnCours.Role.CodeRole == "CMC")
                        {
                            if (Tools.Tools.AgentEnCours.IsChefEquipe == false)
                                prospects = prospects.Where(pros => pros.CommercialID == Tools.Tools.AgentEnCours.Id).ToList();
                            else
                                prospects = prospects.Where(pros => pros.Commercial != null && pros.Commercial.ChefEquipeId == Tools.Tools.AgentEnCours.Id).ToList();
                        }
                        #region Application des critères de filtre

                        foreach (ListViewItem item in lvCriteres.Items)
                        {

                            //string critere = cmbCriteres.SelectedItem.ToString();
                            string critere = item.Text;
                            //var option = prospect.Options.Where(opt => opt.Active == true).FirstOrDefault();
                            switch (critere.ToLower())
                            {

                                case "origine":
                                    ClassOrigine typeOrigine = (ClassOrigine)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Origine.ClassOrigine == typeOrigine).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Origine.ClassOrigine != typeOrigine).ToList();
                                    break;
                                case "source":
                                    TypeOrigine origine = (TypeOrigine)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Origine.TypeOrigineId == origine.TypeOrigineId).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Origine.TypeOrigineId != origine.TypeOrigineId).ToList();
                                    break;
                                case "type villa":
                                    TypeVilla typeVilla = (TypeVilla)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVillaId == typeVilla.TypeVillaId).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeVillaId != typeVilla.TypeVillaId).ToList();
                                    break;
                                case "type contrat":
                                    TypeContrat typeContrat = (TypeContrat)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeContratId == typeContrat.ID).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().TypeContratId != typeContrat.ID).ToList();
                                    break;
                                case "commercial":
                                    Agent commercial = (Agent)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.CommercialID == commercial.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.CommercialID != commercial.Id).ToList();
                                    break;
                                case "chef d'équipe":
                                    Agent chefEquipe = (Agent)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Commercial.ChefEquipeId == chefEquipe.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Commercial.ChefEquipeId != chefEquipe.Id).ToList();
                                    break;
                                //case "nature apporteur":
                                //    TypeApporteur typeApp = (TypeApporteur)item.Tag;
                                //    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                //        prospects = prospects.Where(c => c.Apporteur != null && c.Apporteur.Type == typeApp).ToList();
                                //    else
                                //        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                //        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Type != typeApp).ToList();
                                //    break;
                                //case "apporteur d'affaires":
                                //    ApporteurAffaire appAff = (ApporteurAffaire)item.Tag;
                                //    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                //        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Id == appAff.Id).ToList();
                                //    else
                                //        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                //        contrats = contrats.Where(c => c.Apporteur != null && c.Apporteur.Id != appAff.Id).ToList();
                                //    break;
                                case "coopérative":
                                    Cooperative coop = (Cooperative)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Cooperative != null && c.CooperativeId == coop.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Cooperative != null && c.CooperativeId != coop.Id).ToList();
                                    break;
                                //case "pays":
                                //    string pays = (string)item.Tag;
                                //    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                //        prospects = prospects.Where(c => c.Pays.ToLower() == pays.ToLower()).ToList();
                                //    else
                                //        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                //        prospects = prospects.Where(c => c.Pays.ToLower() != pays.ToLower()).ToList();
                                //    break;

                                case "pays":
                                    Country pays = (Country)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Country != null && c.Country.Id == pays.Id).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Country.Id != pays.Id).ToList();
                                    break;

                                case "profession":
                                    string prof = (string)item.Tag;
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Profession.ToLower() == prof.ToLower()).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Profession.ToLower() != prof.ToLower()).ToList();
                                    break;

                                case "remise":
                                    string remise = (string)item.Tag;
                                    decimal dRemise = decimal.Parse(remise);
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().MontantRemiseAccordee == dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().MontantRemiseAccordee != dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "supérieur ou égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().MontantRemiseAccordee >= dRemise).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "inférieur ou égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().MontantRemiseAccordee <= dRemise).ToList();
                                    break;
                                case "prix de vente":
                                    string prixDeVente = (string)item.Tag;
                                    decimal dPrixDeVente = decimal.Parse(prixDeVente);
                                    if (item.SubItems[1].Text.Trim().ToLower() == "égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente == dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "est différent de")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente != dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "supérieur ou égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente >= dPrixDeVente).ToList();
                                    else
                                        if (item.SubItems[1].Text.Trim().ToLower() == "inférieur ou égal")
                                        prospects = prospects.Where(c => c.Options.Where(opt => opt.Active == true).FirstOrDefault().PrixDeVente <= dPrixDeVente).ToList();
                                    break;
                                case "date entrée":
                                    DateTime[] dates = (DateTime[])item.Tag;
                                    prospects = prospects.Where(c => c.DateSouscription >= dates[0]
                                                                 && c.DateSouscription <= dates[1]).ToList();
                                    break;
                                case "date création":
                                    DateTime[] datesCreation = (DateTime[])item.Tag;
                                    prospects = prospects.Where(c => c.DateCreation >= datesCreation[0]
                                                                 && c.DateCreation <= datesCreation[1]).ToList();
                                    break;
                                    //case "date dépôt":
                                    //    DateTime[] datesDepot = (DateTime[])item.Tag;
                                    //    contrats = contrats.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && c.DateSouscription >= datesDepot[0]
                                    //                                 && c.DateSouscription <= datesDepot[1]).ToList();
                                    //    break;
                                    //case "date réservation":
                                    //    DateTime[] datesReservation = (DateTime[])item.Tag;
                                    //    contrats = contrats.Where(c => c.DateReservation >= datesReservation[0]
                                    //                                 && c.DateReservation <= datesReservation[1]).ToList();
                                    //    break;
                                    //case "date livraison":
                                    //    DateTime[] datesLivraison = (DateTime[])item.Tag;
                                    //    contrats = contrats.Where(c => c.DateLivraisonLot != null && c.DateLivraisonLot >= datesLivraison[0]
                                    //                                 && c.DateLivraisonLot <= datesLivraison[1]).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion

                        #region PROSPECT: AFFICHAGE RESULTATS 

                        lvResult.Items.Clear();
                        int i = 1;
                        foreach (var prospect in prospects.ToList())
                        {
                            ListViewItem lviClient = new ListViewItem(prospect.NumeroClient);
                            foreach (var champ in champs)
                            {
                                if (champ == "Prénom")
                                    lviClient.SubItems.Add(prospect.Prenom);
                                if (champ == "Nom")
                                    lviClient.SubItems.Add(prospect.Nom);
                                if (champ == "Statut")
                                    lviClient.SubItems.Add("Prospect");

                                if (champ == "Téléphone")
                                    lviClient.SubItems.Add(prospect.Mobile1);
                                if (champ == "Adresse")
                                    lviClient.SubItems.Add(prospect.Adresse);
                                if (champ == "Mail")
                                    lviClient.SubItems.Add(prospect.Email);

                                if (champ == "Origine")
                                    if (prospect.Origine != null)
                                    {
                                        lviClient.SubItems.Add(prospect.Origine.ClassOrigine.ToString());
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                                if (champ == "Source")
                                    if (prospect.Origine != null)
                                    {
                                        lviClient.SubItems.Add(prospect.Origine.LibelleTypeOrigine);
                                    }
                                    else
                                    {
                                        lviClient.SubItems.Add("");
                                    }
                                if (champ == "Nationalité")
                                    lviClient.SubItems.Add(prospect.Nationalite);
                                if (champ == "Pays")
                                    lviClient.SubItems.Add(prospect.Pays);
                                if (champ == "Profession")
                                    lviClient.SubItems.Add(prospect.Profession);
                                if (champ == "Date entrée")
                                    if (prospect.DateSouscription != null)
                                        lviClient.SubItems.Add(prospect.DateSouscription.Value.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Date création")
                                    if (prospect.DateCreation != null)
                                        lviClient.SubItems.Add(prospect.DateCreation.ToShortDateString());
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Date dépôt")
                                    lviClient.SubItems.Add("");

                                if (champ == "Date réservation")
                                    lviClient.SubItems.Add("");

                                var option = prospect.Options.Where(opt => opt.Active == true).FirstOrDefault();
                                //if (option != null)
                                //{

                                if (champ == "Type contrat")
                                    if (option != null)
                                        lviClient.SubItems.Add(option.TypeContrat.LibelleTypeContrat);
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Type Villa")
                                    if (option != null)
                                        lviClient.SubItems.Add(option.Lot.TypeVilla.CodeType);
                                    else
                                        lviClient.SubItems.Add("");

                                if (champ == "Numéro Lot")
                                    if (option != null)
                                        if (!option.Lot.LotVirtuel)
                                            lviClient.SubItems.Add(option.Lot.NumeroLot);
                                        else
                                            lviClient.SubItems.Add("");
                                    else
                                        lviClient.SubItems.Add("");

                                if (champ == "Prix de vente")
                                    if (option != null)
                                        lviClient.SubItems.Add(option.PrixDeVente.ToString("### ### ###0"));
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Remise")
                                    if (option != null)
                                        lviClient.SubItems.Add(option.MontantRemiseAccordee.ToString("### ### ##0"));
                                    else
                                        lviClient.SubItems.Add("");

                                if (champ == "Date livraison")
                                    lviClient.SubItems.Add("");
                                if (champ == "Montant versé")
                                    lviClient.SubItems.Add(prospect.EncaissementProspects.Where(enc => enc.FraisDeDossier == false).Sum(enc => enc.MontantGlobal).ToString("### ### ##0"));
                                if (champ == "Commercial")
                                    if (prospect.Commercial != null)
                                        lviClient.SubItems.Add(prospect.Commercial.NomComplet);
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Chef d'équipe")
                                    if (prospect.Commercial.ChefEquipe != null)
                                        lviClient.SubItems.Add(prospect.Commercial.ChefEquipe.NomComplet);
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Nature apporteur" || champ == "Apporteur d'affaires" || champ == "Taux apporteur")
                                    lviClient.SubItems.Add("");
                                if (champ == "Coopérative")
                                    if (prospect.Cooperative != null)
                                        lviClient.SubItems.Add(prospect.Cooperative.Denomination);
                                    else
                                        lviClient.SubItems.Add("");
                                if (champ == "Taux coopérative")
                                    if (prospect.Cooperative != null)
                                        lviClient.SubItems.Add(prospect.Cooperative.TauxRemise.ToString("##0"));
                                    else
                                        lviClient.SubItems.Add("");
                            }
                            lvResult.Items.Add(lviClient);
                        }
                        #endregion

                        txtTotal.Text = prospects.Count.ToString("### ##0");
                    }
                }
                cmdExporter.Enabled = true;
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


        private void cmdRAZ_Click(object sender, EventArgs e)
        {
            lvCriteres.Items.Clear();
            lvResult.Items.Clear();
            lvResult.Columns.Clear();
            foreach (ListViewItem item in lvFields.Items)
            {
                item.BackColor = Color.White;
            }
            champs.Clear();
            this.lvResult.Columns.Add("NumeroDossier");
            this.lvResult.Columns[0].Width = 0;
            cmdExtraire.Enabled = false;
            cmdExporter.Enabled = false;
            cmbCriteres.SelectedIndex = -1;
            cmbValeur.SelectedIndex = -1;
            cmbOperateur.SelectedIndex = -1;
            rbAKYS.Checked = false;
            rbKERRIA.Checked = false;
        }


        private void cmbCriteres_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbValeur.DataSource=null;
                if (cmbCriteres.SelectedItem == null) return;
                string critere = cmbCriteres.SelectedItem.ToString();
                using (var db= new SenImmoDataContext())
                {
                    switch (critere)
                    {
                        case "Origine":
                            cmbValeur.DataSource = Enum.GetValues(typeof(ClassOrigine));
                            cmbValeur.SelectedIndex = -1;
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;
                        case "Source":
                            cmbValeur.DataSource = db.TypeOrigines.Where(to =>  to.BActif == true).OrderByDescending(to => to.Clients.Count).ToList();
                            cmbValeur.DisplayMember = "LibelleTypeOrigine";
                            cmbValeur.SelectedIndex = -1;
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;
                        case "Type contrat":
                            cmbValeur.DataSource = db.TypeContrats.ToList();
                            cmbValeur.DisplayMember = "LibelleTypeContrat";
                            cmbValeur.SelectedIndex = -1;
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;
                        case "Type villa":
                            cmbValeur.DataSource = db.TypeVillas.ToList();
                            cmbValeur.DisplayMember = "CodeType";
                            cmbValeur.SelectedIndex = -1;
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;
                        case "Commercial":
                            cmbValeur.DataSource = db.Agents.Where(ag =>ag.Role.CodeRole=="CMC" && ag.IsChefEquipe==false ).ToList();
                            cmbValeur.DisplayMember = "NomComplet";
                            cmbValeur.SelectedIndex = -1;
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;
                         case "Chef d'équipe":
                            cmbValeur.DataSource = db.Agents.Where(ag => ag.Role.CodeRole == "CMC" && ag.IsChefEquipe == true).ToList();
                            cmbValeur.DisplayMember = "NomComplet";
                            cmbValeur.SelectedIndex = -1;
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;
                        case "Nature apporteur":
                            cmbValeur.DataSource = Enum.GetValues(typeof(TypeApporteur));
                            cmbValeur.SelectedIndex = -1;
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;
                        case "Apporteur d'affaires":
                            cmbValeur.DataSource = db.ApporteurAffaires.Where(aaf => aaf.Actif == true).ToList();
                            cmbValeur.DisplayMember = "NomComplet";
                            cmbValeur.SelectedIndex = -1;
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;
                        case "Coopérative":
                            cmbValeur.DataSource = db.Cooperatives.ToList();
                            cmbValeur.DisplayMember = "Denomination";
                            cmbValeur.SelectedIndex = -1;
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;
                        case "Date entrée":
                            cmbOperateur.Visible = false;
                            cmbValeur.Visible = false;
                            pDates.Visible = true;
                            pDates.Left = pOperateur.Left;
                            pDates.Top = pOperateur.Top;
                            break;
                        case "Date création":
                            cmbOperateur.Visible = false;
                            cmbValeur.Visible = false;
                            pDates.Visible = true;
                            pDates.Left = pOperateur.Left;
                            pDates.Top = pOperateur.Top;
                            break;
                        case "Date dépôt":
                            cmbOperateur.Visible = false;
                            cmbValeur.Visible = false;
                            pDates.Visible = true;
                            pDates.Left = pOperateur.Left;
                            pDates.Top = pOperateur.Top;
                            break;
                        case "Date réservation":
                            cmbOperateur.Visible = false;
                            cmbValeur.Visible = false;
                            pDates.Visible = true;
                            pDates.Left = pOperateur.Left;
                            pDates.Top = pOperateur.Top;
                            break;
                        case "Date livraison":
                            cmbOperateur.Visible = false;
                            cmbValeur.Visible = false;
                            pDates.Visible = true;
                            pDates.Left = pOperateur.Left;
                            pDates.Top = pOperateur.Top;
                            break;
                        case "Prix de vente":
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;
                        case "Remise":
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;
                        case "Pays":
                            cmbValeur.DataSource = db.Countries.ToList();
                            cmbValeur.DisplayMember = "CountryName";
                            cmbValeur.SelectedIndex = -1;
                            cmbOperateur.Visible = true;
                            cmbValeur.Visible = true;
                            pDates.Visible = false;
                            break;

                        default:
                            break;
                    } 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void dtpDebut_ValueChanged(object sender, EventArgs e)
        {
            dtpDebut.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpFin_ValueChanged(object sender, EventArgs e)
        {
            dtpFin.CustomFormat = "dd/MM/yyyy";
        }

        private void cmdAjouterCritere_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cmbCriteres.SelectedItem == null) return;
                string critere = cmbCriteres.SelectedItem.ToString();
                ListViewItem lviCritere = new ListViewItem(critere);
               
                switch (critere.ToLower())
                {
                    case "origine":
                        ClassOrigine co = (ClassOrigine)cmbValeur.SelectedItem;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = co;
                        lviCritere.SubItems.Add(co.ToString());

                        break;
                    case "source":
                        TypeOrigine to = (TypeOrigine)cmbValeur.SelectedItem;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = to;
                        lviCritere.SubItems.Add(to.LibelleTypeOrigine.ToString());
                        break;
                    case "type contrat":
                        TypeContrat tc = (TypeContrat)cmbValeur.SelectedItem;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = tc;
                        lviCritere.SubItems.Add(tc.LibelleTypeContrat);
                        break;
                    case "type villa":
                        TypeVilla tv = (TypeVilla)cmbValeur.SelectedItem;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = tv;
                        lviCritere.SubItems.Add(tv.CodeType.ToString());
                        break;
                    case "commercial":
                        Agent ag = (Agent)cmbValeur.SelectedItem;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = ag;
                        lviCritere.SubItems.Add(ag.NomComplet.ToString());
                        break;
                    case "chef d'équipe":
                        Agent chef = (Agent)cmbValeur.SelectedItem;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = chef;
                        lviCritere.SubItems.Add(chef.NomComplet.ToString());
                        break;

                    case "nature apporteur":
                        if (rbProspects.Checked)
                        {
                            MessageBox.Show(this, "Désolé ce critère n'est pas applicable aux prospects",
                                        "Prosopis - Extractions prospects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        TypeApporteur natApp = (TypeApporteur)cmbValeur.SelectedItem;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = natApp;
                        lviCritere.SubItems.Add(natApp.ToString());
                        break;
                    case "apporteur d'affaires":
                        if (rbProspects.Checked)
                        {
                            MessageBox.Show(this, "Désolé ce critère n'est pas applicable aux prospects",
                                        "Prosopis - Extractions prospects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        ApporteurAffaire appAff = (ApporteurAffaire)cmbValeur.SelectedItem;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = appAff;
                        lviCritere.SubItems.Add(appAff.NomComplet);
                        break;
                    case "coopérative":
                        Cooperative coop = (Cooperative)cmbValeur.SelectedItem;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = coop;
                        lviCritere.SubItems.Add(coop.Denomination);
                        break;
                    case "pays":
                        //string pays = (string)cmbValeur.Text;
                        Country pays = (Country)cmbValeur.SelectedItem;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = pays;
                        lviCritere.SubItems.Add(pays.CountryName);
                        break;
                    case "profession":
                        string prof = (string)cmbValeur.Text;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = prof;
                        lviCritere.SubItems.Add(prof);
                        break;
                    case "remise":
                        string remise = (string)cmbValeur.Text;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = remise;
                        lviCritere.SubItems.Add(remise);
                        break;
                    case "prix de vente":
                        string prixDeVente = (string)cmbValeur.Text;
                        lviCritere.SubItems.Add(cmbOperateur.SelectedItem.ToString());
                        lviCritere.Tag = prixDeVente;
                        lviCritere.SubItems.Add(prixDeVente);
                        break;
                    case "date entrée":
                        DateTime[] dates = new DateTime[2];
                        dates[0] = dtpDebut.Value.Date;
                        dates[1] = dtpFin.Value.Date;
                        lviCritere.Tag = dates;
                        lviCritere.SubItems.Add("entre le");
                       lviCritere.SubItems.Add( dates[0].ToShortDateString() + " et le " + dates[1].ToShortDateString());
                        break;
                    case "date création":
                        DateTime[] datesCreation = new DateTime[2];
                        datesCreation[0] = dtpDebut.Value.Date;
                        datesCreation[1] = dtpFin.Value.Date;
                        lviCritere.Tag = datesCreation;
                        lviCritere.SubItems.Add("entre le");
                        lviCritere.SubItems.Add(datesCreation[0].ToShortDateString() + " et le " + datesCreation[1].ToShortDateString());
                        break;
                    case "date réservation":
                        if(rbProspects.Checked)
                        {
                            MessageBox.Show(this, "Désolé ce critère n'est pas applicable aux prospects" ,
                                        "Prosopis - Extractions prospects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        DateTime[] datesReservation = new DateTime[2];
                        datesReservation[0] = dtpDebut.Value.Date;
                        datesReservation[1] = dtpFin.Value.Date;
                        lviCritere.Tag = datesReservation;
                        lviCritere.SubItems.Add("entre le");
                        lviCritere.SubItems.Add(datesReservation[0].ToShortDateString() + " et le " + datesReservation[1].ToShortDateString());
                        break;
                    case "date dépôt":
                        if (rbProspects.Checked)
                        {
                            MessageBox.Show(this, "Désolé ce critère n'est pas applicable aux prospects",
                                        "Prosopis - Extractions prospects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        DateTime[] datesDepot = new DateTime[2];
                        datesDepot[0] = dtpDebut.Value.Date;
                        datesDepot[1] = dtpFin.Value.Date;
                        lviCritere.Tag = datesDepot;
                        lviCritere.SubItems.Add("entre le");
                        lviCritere.SubItems.Add(datesDepot[0].ToShortDateString() + " et le " + datesDepot[1].ToShortDateString());
                        break;
                    case "date livraison":
                        if (rbProspects.Checked)
                        {
                            MessageBox.Show(this, "Désolé ce critère n'est pas applicable aux prospects",
                                        "Prosopis - Extractions prospects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        DateTime[] datesLivraison = new DateTime[2];
                        datesLivraison[0] = dtpDebut.Value.Date;
                        datesLivraison[1] = dtpFin.Value.Date;
                        lviCritere.Tag = datesLivraison;
                        lviCritere.SubItems.Add("entre le");
                        lviCritere.SubItems.Add(datesLivraison[0].ToShortDateString() + " et le " + datesLivraison[1].ToShortDateString());
                        break;
                    default:
                        break;
                }


                lvCriteres.Items.Add(lviCritere);
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

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(lvCriteres.SelectedItems.Count > 0)
            {
                lvCriteres.Items.Remove(lvCriteres.SelectedItems[0]);
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lvResult_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdExporter_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws = (Worksheet)app.ActiveSheet;
                
                int i = 1;
                foreach (ColumnHeader item in lvResult.Columns)
                { 
                    if(item.Index!=0)
                    { 
                        ws.Cells[6, i] = item.Text;
                        ws.Cells[6, i].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        ws.Cells[6,i].Font.Bold = true;
                        i++;
                    }
                }
                int m = 7;
                foreach (ListViewItem item in lvResult.Items)
                {
                    for (int n = 1; n < item.SubItems.Count; n++)
                    {
                        ws.Cells[m, n].NumberFormat = "@";
                        //if (Microsoft.VisualBasic.Information.IsNumeric(txtMyText.Text.Trim()))
                        ws.Cells[m, n] = item.SubItems[n].Text;
                        ws.Cells[m, n].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    }
                    m++;
                }
                app.Visible = true;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lvCriteres_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbProspects_CheckedChanged(object sender, EventArgs e)
        {
            //if(rbProspects.Checked)
            //{
            //    lvFields.Items[13].BackColor = Color.Gray;
            //    lvFields.Items[14].BackColor = Color.Gray;
            //    lvFields.Items[16].BackColor = Color.Gray;
            //    lvFields.Items[20].BackColor = Color.Gray;
            //    lvFields.Items[21].BackColor = Color.Gray;
            //    lvFields.Items[22].BackColor = Color.Gray;
            //}
            //else
            //{
            //    lvFields.Items[10].BackColor = Color.White;
            //    lvFields.Items[11].BackColor = Color.White;
            //    lvFields.Items[16].BackColor = Color.White;
            //    lvFields.Items[20].BackColor = Color.White;
            //    lvFields.Items[21].BackColor = Color.White;
            //    lvFields.Items[22].BackColor = Color.White;
            //}
        }
    }
}