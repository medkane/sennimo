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
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;

namespace prjSenImmoWinform
{
    public partial class FrmReporting : Form
    {
        private SenImmoDataContext db;

        public FrmReporting()
        {
            InitializeComponent();
            db = new SenImmoDataContext();
            dtpDateDebut.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDateFin.Value = DateTime.Now.Date;

            var RoleCOmmerciale = db.Roles.Where(c => c.CodeRole == "CMC").SingleOrDefault();
            cmbCommerciaux.DataSource= db.Commercials.Where(c => c.RoleId== RoleCOmmerciale.ID).ToList();
            cmbCommerciaux.DisplayMember = "NomComplet";
            cmbCommerciaux.SelectedIndex = -1;

            cmbIlotsTechnique.DataSource = db.Ilots.ToList();
            cmbIlotsTechnique.DisplayMember = "NomIlot";
            //cmbIlotsTechnique.SelectedIndex = -1;

            cmbIlotsTechnique.DataSource = db.Ilots.ToList();
            cmbIlotsTechnique.DisplayMember = "NomIlot";
            //cmbIlotsTechnique.SelectedIndex = -1;

            cmbTypeVillas.DataSource = db.TypeVillas.ToList();
            cmbTypeVillas.DisplayMember = "CodeType";
            cmbTypeVillas.SelectedIndex = -1;

        }

        private void cmdAfficher_Click(object sender, EventArgs e)
        {
            try
            {
                var query = db.Contrats
                                        .GroupBy(c => new { c.TypeContrat })
                                        .Select(n => new
                                        {
                                            Type = n.Key.TypeContrat.CategorieContrat.ToString(),
                                            Count = n.Count()
                                        }).ToList();
                dgResultResa.DataSource = query;



                chartVentes.DataSource = query;
                chartVentes.Series["Ventes"].Points.Clear();
                chartVentes.Series["Ventes"].XValueMember = "Type";
                chartVentes.Series["Ventes"].YValueMembers = "Count";
                chartVentes.DataBind();

                //chartVentes.Series["Ventes"].YValueMembers = "Count";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                     "Prosopis - Gestion des rapports de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void cmdVentesParTypeContratTypeVilla_Click(object sender, EventArgs e)
        {

            try
            {
                lvReportingCommercial.Items.Clear();

                var query = db.Contrats.Where(c => c.DateSouscription >= dtpDateDebut.Value
                                             && c.DateSouscription <= dtpDateFin.Value).OrderBy(c => c.DateSouscription.Value.Year).ThenBy(c => c.DateSouscription.Value.Month).ToList();
                if(cmbCommerciaux.SelectedItem!=null)
                {
                    var commercial =(Commercial) cmbCommerciaux.SelectedItem;
                    query = query.Where(c =>c.CommercialID==commercial.Id).ToList();
                }
                if (cmbTypeVillas.SelectedItem != null)
                {
                    var typeVilla = (TypeVilla)cmbTypeVillas.SelectedItem;
                    query = query.Where(c => c.Lot.TypeVillaID == typeVilla.TypeVillaId).ToList();
                }

                if (cmbIlotsVentes.SelectedItem != null)
                {
                    var ilot = (Ilot)cmbIlotsVentes.SelectedItem;
                    query = query.Where(c => c.Lot.IlotID == ilot.Id).ToList();
                }

                var  query2 = query.GroupBy(c => new
                                        {
                                            c.DateSouscription.Value.Year,
                                            c.DateSouscription.Value.Month,
                                        })
                                        .Select(n => new
                                        {
                                            Annee= n.Key.Year,
                                            Mois = n.Key.Month,
                                            Résa = n.Where(c => c.TypeContrat.CategorieContrat== CategorieContrat.Réservation).Count(),
                                            Dépôt = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt).Count()
                                        }).ToList();
           

                foreach (var item in query2)
                {
                    var annee = item.Annee;
                    var mois=CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    ListViewItem lviRepCommercial = new ListViewItem(mois+" "+annee);
                    lviRepCommercial.SubItems.Add(item.Résa.ToString());

                    lviRepCommercial.SubItems.Add(item.Dépôt.ToString());
                    lviRepCommercial.SubItems.Add((item.Résa+item.Dépôt).ToString());
                    lvReportingCommercial.Items.Add(lviRepCommercial);
                }
                txtTotalResa.Text = query2.Sum(n => n.Résa).ToString();
                txtTotalDepot.Text = query2.Sum(n => n.Dépôt).ToString();
                txtTotalVentes.Text = (query2.Sum(n => n.Résa) + query2.Sum(n => n.Dépôt)).ToString();
                ////chartVentes.DataSource = query;
                //chartVentes.DataBindCrossTable(query, "Periode", "Periode", "Count","");

                //chartVentes.Series[0].YValueMembers = "Count";
                ////'Bind() déclenche le Binding
                //chartVentes.DataBind();


                ////'Chart1 existe déjà
                ////' Créer un Chart Area


                ////' création d 'une premiere series
                //chartVentes.Series.Add("series1");
                ////'On affiche la series dans ChartArea1
                //// chartVentes.Series["series1"].ChartArea = chartArea1.Name;
                ////'On affiche des Stacked column
                //chartVentes.Series["series1"].ChartType = SeriesChartType.StackedColumn;

                var dtf = CultureInfo.CurrentCulture.DateTimeFormat;
                //string monthName = dtf.GetMonthName(month);
                //string abbreviatedMonthName = dtf.GetAbbreviatedMonthName(month);

                DataPoint point = new DataPoint();

                chartVentes.Series.Clear();
                chartVentes.Series.Add("Résa");
                chartVentes.Series.Add("Dépôt");
                //chartVentes.Series["Résa"].Color = Color.Blue;
                //chartVentes.Series["Dépôt"].Color = Color.Red;
               
                chartVentes.Series["Résa"].IsValueShownAsLabel = true;
                
                chartVentes.Series["Dépôt"].IsValueShownAsLabel = true;

                chartVentes.Series["Résa"].LegendText = "Réservation";
                chartVentes.Series["Dépôt"].LegendText = "Dépôt";

                foreach (var item in query2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    var periode= mois + " " + annee;
                    chartVentes.Series["Résa"].Points.AddXY(periode, item.Résa);
                }
                foreach (var item in query2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    var periode = mois + " " + annee;
                    chartVentes.Series["Dépôt"].Points.AddXY(periode, item.Dépôt);
                }

                // var mois = new int[5] {7,8, 9, 10, 11 };
                // var bTrouve = false;
                //foreach (var month in mois)
                //{
                //    foreach (var item in query.Where(c => c.Type == CategorieContrat.Réservation))
                //    {
                //        //xValues.Add(item.Mois.ToString());
                //        //yValues.Add(item.Count);
                //        //chartVentes.Series["series1"].Points.DataBindXY(xValues, yValues);

                //        //chartVentes.Series["series1"].Points.AddXY (dtf.GetMonthName(item.Mois), item.Count);

                //        if (month == item.Mois)
                //        {
                //            chartVentes.Series["series1"].Points.AddXY(item.Mois, item.Count);
                //            bTrouve = true;
                //        }
                //        if(!bTrouve)
                //            chartVentes.Series["series1"].Points.AddXY(item.Mois, 0);
                //    }

                //}

                //foreach (var month in mois)
                //{
                //    foreach (var item in query.Where(c => c.Type == CategorieContrat.Dépôt))
                //    {
                //        if (month == item.Mois)
                //        {
                //            //chartVentes.Series["series2"].Points.AddXY(dtf.GetMonthName(item.Mois), item.Count);
                //            chartVentes.Series["series2"].Points.AddXY(item.Mois, item.Count);
                //            bTrouve = true;
                //        }
                //        if (!bTrouve)
                //            chartVentes.Series["series2"].Points.AddXY(item.Mois, 0);
                //    }

                //}

               // chartVentes.Series["series2"].EmptyPointStyle.CustomProperties = "EmptyPointValue = Zero";
                //var xValues = new string[] { "France", "Canada", "Allemagne", "USA", "Italie", "Espagne", "Russie", "Suisse", "Japon" };
                //var yValues = new double[] { 65, 75, 60, 34, 85, 55, 63, 55, 77 };



                ////'Ajout de 3 points dans la premiere serie
                //var p = new DataPoint();
                //p.XValue = 1;
                //p.YValues = new double[] { 2 };
                //chartVentes.Series["series1"].Points.Add(p);


                //var p1 = new DataPoint();
                //p1.XValue = 2;
                //p1.YValues = new double[] { 6 };
                //chartVentes.Series["series1"].Points.Add(p1);

                //var p2 = new DataPoint();
                //p2.XValue = 3;
                //p2.YValues = new double[] { 7 };
                //chartVentes.Series["series1"].Points.Add(p2);

                //////'On met des cylindres pour faire joli!!
                //chartVentes.Series["series1"].CustomProperties = "DrawingStyle=cylinder";
                //chartVentes.Series["series2"].CustomProperties = "DrawingStyle=cylinder";

                //////'Second series
                ////chartVentes.Series.Add("series2");
                //////'On affiche la series dans ChartArea1
                ////// chartVentes.Series["series2"].ChartArea = chartArea1.Name;
                //////'On affiche en StackedColumn
                //chartVentes.Series["series1"].ChartType = SeriesChartType.StackedColumn;
                //chartVentes.Series["series2"].ChartType = SeriesChartType.StackedColumn;


                ////'Ajout de 3 points dans la seconde series 
                //var p3 = new DataPoint();
                //p3.XValue = 1;
                //p3.YValues = new double[] { 5 };
                //chartVentes.Series["series2"].Points.Add(p3);


                //var p4 = new DataPoint();
                //p4.XValue = 2;
                //p4.YValues = new double[] { 4 };
                //chartVentes.Series["series2"].Points.Add(p4);

                //var p5 = new DataPoint();
                //p5.XValue = 3;
                //p5.YValues = new double[] { 3 };
                //chartVentes.Series["series2"].Points.Add(p5);



                //'On met les 2 series dans le même StackedGroup
                //'(Peut importe le nom du group)
               
               





                ////Deux séries du genre objectifs et réalisés
                //Series series = chartVentesCumules.Series.Add("Résa");
                //series.ChartType = SeriesChartType.Column;
                //series.Name = "Résa";
                //series.Points.Add(55);
                //series.Points.Add(10);
                //series.Points.Add(50);
                //series.Points.Add(50);

                //series = chartVentesCumules.Series.Add("Dépot");
                //series.ChartType = SeriesChartType.Column;
                //series.Name = "Dépot";
                //series.Points.Add(80);
                //series.Points.Add(90);
                //series.Points.Add(10);
                //series.Points.Add(10);










                //chartVentesCumules.DataSource = query;
                //chartVentesCumules.Series["Ventes"].Points.Clear();
                //chartVentesCumules.Series["Ventes"].XValueMember = "Type";
                //chartVentesCumules.Series["Ventes"].YValueMembers = "Count";
                //chartVentesCumules.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                     "Prosopis - Gestion des rapports de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdRecouvrementResa_Click(object sender, EventArgs e)
        {
            try
            {
                var q = db.Contrats.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                .GroupBy(c => new
                {
                    c.Lot.Ilot.NomIlot,

                })
                .Select(n => new
                {
                    Ilot = n.Key.NomIlot,
                    NbVilla = n.Count(),
                    CA = n.Sum(c => c.PrixFinal),
                    AEncaisser = n.Sum(c => c.Factures.Where(f => f.Echue == true).Sum(f => f.Montant)),
                    Encaisse = n.Sum(c => c.EncaissementGlobals.Sum(enc => enc.MontantGlobal)),
                    //ARecouvrer = n.Sum(c => c.Factures.Where(f => f.Echue == true).Sum(f => f.Montant))
                    //            - n.Sum(c => c.EncaissementGlobals.Sum(enc => enc.MontantGlobal)),
                    ARec = n.Sum(c => c.Factures.Where(f => f.Echue == true).Sum(f => f.Montant - f.Encaissements.Sum(enc => enc.Montant)))
                }).ToList();
                dgResultResa.DataSource = null;
                dgResultResa.DataSource = q;

                var queryDepot = db.Contrats.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
               .GroupBy(c => new
               {
                   c.Lot.Ilot.NomIlot,
               })
               .Select(n => new
               {

                   NbVilla = n.Count(),
                   CA = n.Sum(c => c.PrixFinal),
                   DepotInitial = n.Sum(c => c.Factures.Where(f => f.TypeFacture == TypeFacture.DepotMinimum).Sum(f => f.Encaissements.Sum(enc => enc.Montant))),
                   AEncaisser = n.Sum(c => c.Factures.Where(f => f.Echue == true).Sum(f => f.Montant)),
                   Encaisse = n.Sum(c => c.EncaissementGlobals.Sum(enc => enc.MontantGlobal)),
                   //ARecouvrer = n.Sum(c => c.Factures.Where(f => f.Echue == true).Sum(f => f.Montant))
                   //            - n.Sum(c => c.EncaissementGlobals.Sum(enc => enc.MontantGlobal)),
                   ARec = n.Sum(c => c.Factures.Where(f => f.Echue == true).Sum(f => f.Montant - f.Encaissements.Sum(enc => enc.Montant)))

               }).ToList();
                dgResultDepot.DataSource = null;
                dgResultDepot.DataSource = queryDepot;



                
                dgResultResa.Columns[0].Width = 70;
                dgResultResa.Columns[1].HeaderText = "NB villas";
                dgResultResa.Columns[1].Width = 80;

                dgResultResa.Columns[2].Width = 90;
                dgResultResa.Columns[2].DefaultCellStyle.Format = "### ### ###";
                dgResultResa.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgResultResa.Columns[3].HeaderText = "A encaisser";
                dgResultResa.Columns[3].Width = 90;
                dgResultResa.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgResultResa.Columns[3].DefaultCellStyle.Format = "### ### ###";

                dgResultResa.Columns[4].Width = 90;
                dgResultResa.Columns[4].DefaultCellStyle.Format = "### ### ###";
                dgResultResa.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgResultResa.Columns[5].HeaderText = "A recouvrer";
                dgResultResa.Columns[5].DefaultCellStyle.Format = "### ### ###";
                dgResultResa.Columns[5].Width = 90;
                dgResultResa.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                ////////
                dgResultDepot.Columns[0].Width = 80;
                dgResultDepot.Columns[0].HeaderText = "NB villas";

                dgResultDepot.Columns[1].Width = 90;
                dgResultDepot.Columns[1].DefaultCellStyle.Format = "### ### ###";
                dgResultDepot.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgResultDepot.Columns[2].HeaderText = "Dépôt Initial";
                dgResultDepot.Columns[2].Width = 90;
                dgResultDepot.Columns[2].DefaultCellStyle.Format = "### ### ###";
                dgResultDepot.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgResultDepot.Columns[3].HeaderText = "A encaisser";
                dgResultDepot.Columns[3].Width = 90;
                dgResultDepot.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgResultDepot.Columns[3].DefaultCellStyle.Format = "### ### ###";

                dgResultDepot.Columns[4].Width = 90;
                dgResultDepot.Columns[4].DefaultCellStyle.Format = "### ### ###";
                dgResultDepot.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgResultDepot.Columns[5].HeaderText = "A recouvrer";
                dgResultDepot.Columns[5].DefaultCellStyle.Format = "### ### ###";
                dgResultDepot.Columns[5].Width = 90;
                dgResultDepot.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                     "Prosopis - Gestion des rapports de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdRecouvrementDepot_Click(object sender, EventArgs e)
        {
            try
            {
                var q = db.Contrats.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                .GroupBy(c => new
                {
                    c.Lot.Ilot.NomIlot,
                })
                .Select(n => new
                {

                    NbVilla = n.Count(),
                    CA = n.Sum(c => c.PrixFinal),
                    DepotInitial = n.Sum(c => c.Factures.Where(f => f.TypeFacture == TypeFacture.DepotMinimum).Sum(f => f.Encaissements.Sum(enc => enc.Montant))),
                    AEncaisser = n.Sum(c => c.Factures.Where(f => f.Echue == true).Sum(f => f.Montant)),
                    Encaisse = n.Sum(c => c.EncaissementGlobals.Sum(enc => enc.MontantGlobal)),
                    ARecouvrer = n.Sum(c => c.Factures.Where(f => f.Echue == true).Sum(f => f.Montant))
                                - n.Sum(c => c.EncaissementGlobals.Sum(enc => enc.MontantGlobal)),
                    ARec=n.Sum(c =>c.Factures.Where(f => f.Echue == true).Sum(f => f.Montant - f.Encaissements.Sum(enc => enc.Montant)))

                }).ToList();
                dgResultDepot.DataSource = null;
                dgResultDepot.DataSource = q;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                     "Prosopis - Gestion des rapports de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdReportingtechnique_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateDebutTechnique = new DateTime(dtpDateDebutTechnique.Value.Year, dtpDateDebutTechnique.Value.Month, DateTime.DaysInMonth(dtpDateDebutTechnique.Value.Year, dtpDateDebutTechnique.Value.Month));
                DateTime dateFinTechnique = new DateTime(dtpDateFinTechnique.Value.Year, dtpDateFinTechnique.Value.Month, DateTime.DaysInMonth(dtpDateFinTechnique.Value.Year, dtpDateFinTechnique.Value.Month));
                ////Indicateur par Ilot
                //var query = db.EtatAvancements.Where(ea => ea.Lot.IlotID == 1 && ea.TypeEtatAvancement.StatutTermine == true)
                //    .GroupBy(eal => new
                //    {
                //        eal.TypeEtatAvancement
                //    }
                //    )
                //    .OrderBy(ea => ea.Key.TypeEtatAvancement.ordre)
                //    .Select(n => new
                //    {
                //        Avancement = n.Key.TypeEtatAvancement.LibelleTechnique,
                //        Nombre = n.Count(ea => ea.Actif == true),
                //        NombreTotal = db.Lots.Where(ea => ea.IlotID == 1 && ea.LotVirtuel == false).Count(),
                //        NiveauRealisation = ((double)n.Count(ea => ea.Actif == true) / db.Lots.Where(ea => ea.IlotID == 1 && ea.LotVirtuel == false).Count()*100)+"%"
                //    }
                //    )
                //    .ToList();

                //dgResult.DataSource = query;
                int idIlot=0;
                if (cmbIlotsTechnique.SelectedItem!=null)
                {
                    idIlot = ((Ilot)cmbIlotsTechnique.SelectedItem).Id;
                }
                else return;

                int nbVillas = db.Lots.Where(ea => ea.IlotID == idIlot && ea.LotVirtuel == false).Count();
                var query = db.EtatAvancements.Where(ea => ea.Lot.IlotID == idIlot && ea.TypeEtatAvancement.StatutTermine == true)
                    .GroupBy(eal => new
                    {
                        eal.TypeEtatAvancement
                    }
                    )
                    .OrderBy(ea => ea.Key.TypeEtatAvancement.ordre)
                    .Select(n => new
                    {
                        Avancement = n.Key.TypeEtatAvancement.LibelleTechnique,
                        Nombre1 = n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateDebutTechnique.Date),
                        Nombre2 = n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateFinTechnique.Date),
                        NiveauRealisation1 = ((double)n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateDebutTechnique.Date) / nbVillas * 100) + "%",
                        NiveauRealisation2 = ((double)n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateFinTechnique.Date) / nbVillas * 100) + "%",
                        DYN=( ((double)n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateFinTechnique.Date) / nbVillas * 100) - ((double)n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateDebutTechnique.Date) / nbVillas * 100))+"%",
                        Commentaires= n.Count(ea => ea.Actif == true && ea.DateSaisieAvancement <= dateFinTechnique.Date).ToString()+ "/"+ nbVillas
                    }
                    )
                    .ToList();

                dgReportingTechnique.DataSource = query;
                dgReportingTechnique.Columns[0].HeaderText = "POSTES TRAVAUX";
                dgReportingTechnique.Columns[0].Width = 250;
                dgReportingTechnique.Columns[1].HeaderText = " NOMBRE DE VILLA EN FIN " + dateDebutTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                dgReportingTechnique.Columns[1].Width = 80;
                dgReportingTechnique.Columns[2].HeaderText = "NOMBRE DE VILLA EN FIN " + dateFinTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                dgReportingTechnique.Columns[2].Width = 80;
                dgReportingTechnique.Columns[3].HeaderText = "FIN " + dateDebutTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                dgReportingTechnique.Columns[3].Width = 80;
                dgReportingTechnique.Columns[4].HeaderText = "FIN " + dateFinTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                dgReportingTechnique.Columns[4].Width = 80;
                //dgReportingTechnique.Columns[3].DefaultCellStyle.Format = "### ### ###";
                //dgReportingTechnique.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                lbNombreDeVillas.Text = nbVillas.ToString();

                //chartVentes.DataSource = query;
                //chartVentes.Series["Ventes"].Points.Clear();
                //chartVentes.Series["Ventes"].XValueMember = "Type";
                //chartVentes.Series["Ventes"].YValueMembers = "Count";
                //chartVentes.DataBind();

                //chartVentes.Series["Ventes"].YValueMembers = "Count";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                     "Prosopis - Gestion des rapports de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdReportingMarketing_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime dateDebutTechnique = new DateTime(dtpDateDebutTechnique.Value.Year, dtpDateDebutTechnique.Value.Month, DateTime.DaysInMonth(dtpDateDebutTechnique.Value.Year, dtpDateDebutTechnique.Value.Month));
                //DateTime dateFinTechnique = new DateTime(dtpDateFinTechnique.Value.Year, dtpDateFinTechnique.Value.Month, DateTime.DaysInMonth(dtpDateFinTechnique.Value.Year, dtpDateFinTechnique.Value.Month));
               
                //int idIlot = 0;
                //if (cmbIlotsTechnique.SelectedItem != null)
                //{
                //    idIlot = ((Ilot)cmbIlotsTechnique.SelectedItem).Id;
                //}
                //else return;

                //int nbClients = db.Clients.Where(Pros => Pros. == idIlot && ea.LotVirtuel == false).Count();
                var query = db.Clients.Where(Pros => Pros.DateAffectationCommercial >= dtpDateDebutMarketing.Value.Date && Pros.DateAffectationCommercial <= dtpDateFinMarketing.Value.Date)
                    .GroupBy(pros => new
                    {
                        pros.Origine
                    }
                    )
                   
                    .Select(n => new
                    {
                        Origine = n.Key.Origine.LibelleTypeOrigine,
                        Nombre1 = n.Count(),
                        
                    }
                    )
                    .ToList();
                dgReportingMarketing.DataSource = query;
                //°°°°°°°°°°°°°°°°°°°°°°°°°°
                var queryCumule = db.Clients.Where(Pros => Pros.DateAffectationCommercial >= dtpDateDebutMarketing.Value.Date && Pros.DateAffectationCommercial <= dtpDateFinMarketing.Value.Date)
                    .GroupBy(pros => new
                    {
                        Origine=pros.Origine,
                        Periode=pros.DateAffectationCommercial.Value.Year+" "+pros.DateAffectationCommercial.Value.Month
                    }
                    )

                    .Select(n => new
                    {
                        Periode=n.Key.Periode,
                        Origine = n.Key.Origine.LibelleTypeOrigine,
                        Nombre1 = n.Count(),

                    }
                    )
                    .ToList();
                dgReportingCumuleMarketing.DataSource = queryCumule;
                //dgReportingTechnique.DataSource = query;
                //dgReportingTechnique.Columns[0].HeaderText = "POSTES TRAVAUX";
                //dgReportingTechnique.Columns[0].Width = 250;
                //dgReportingTechnique.Columns[1].HeaderText = " NOMBRE DE VILLA EN FIN " + dateDebutTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                //dgReportingTechnique.Columns[1].Width = 80;
                //dgReportingTechnique.Columns[2].HeaderText = "NOMBRE DE VILLA EN FIN " + dateFinTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                //dgReportingTechnique.Columns[2].Width = 80;
                //dgReportingTechnique.Columns[3].HeaderText = "FIN " + dateDebutTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                //dgReportingTechnique.Columns[3].Width = 80;
                //dgReportingTechnique.Columns[4].HeaderText = "FIN " + dateFinTechnique.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                //dgReportingTechnique.Columns[4].Width = 80;
                ////dgReportingTechnique.Columns[3].DefaultCellStyle.Format = "### ### ###";
                ////dgReportingTechnique.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //lbNombreDeVillas.Text = nbVillas.ToString();

                //chartVentes.DataSource = query;
                //chartVentes.Series["Ventes"].Points.Clear();
                //chartVentes.Series["Ventes"].XValueMember = "Type";
                //chartVentes.Series["Ventes"].YValueMembers = "Count";
                //chartVentes.DataBind();

                //chartVentes.Series["Ventes"].YValueMembers = "Count";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                     "Prosopis - Gestion des rapports de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
