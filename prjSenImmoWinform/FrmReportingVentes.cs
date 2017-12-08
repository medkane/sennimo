using prjSenImmoWinform.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;

namespace prjSenImmoWinform
{
    public partial class FrmReportingVentes : Form
    {
        private SenImmoDataContext db;
        private TypeVilla F2;
        private TypeVilla F3;
        private TypeVilla F4A;
        private TypeVilla F4B;
        private TypeVilla F5A;
        private TypeVilla F5B;
        public FrmReportingVentes()
        {
            InitializeComponent();
            db = new SenImmoDataContext();
            dtpDateDebut.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDateFin.Value = DateTime.Now.Date;

            var RoleCOmmerciale = db.Roles.Where(c => c.CodeRole == "CMC").SingleOrDefault();
            F2 = db.TypeVillas.Where(tv => tv.CodeType == "F2A").SingleOrDefault();
            F3 = db.TypeVillas.Where(tv => tv.CodeType == "F3").SingleOrDefault();
            F4A = db.TypeVillas.Where(tv => tv.CodeType == "F4A").SingleOrDefault();
            F4B= db.TypeVillas.Where(tv => tv.CodeType == "F4B").SingleOrDefault();
            F5A= db.TypeVillas.Where(tv => tv.CodeType == "F5A").SingleOrDefault();
            F5B= db.TypeVillas.Where(tv => tv.CodeType == "F5B").SingleOrDefault();


            cmbCommerciaux.DataSource = db.Agents.Where(c => c.RoleId == RoleCOmmerciale.ID).ToList();
            cmbCommerciaux.DisplayMember = "NomComplet";
            cmbCommerciaux.SelectedIndex = -1;


            cmbIlotsVentes.DataSource = db.Ilots.ToList();
            cmbIlotsVentes.DisplayMember = "NomIlot";
            cmbIlotsVentes.SelectedIndex = -1;
            if(Tools.Tools.AgentEnCours.Role.ID==RoleCOmmerciale.ID)
            {
                cmbCommerciaux.SelectedItem = db.Agents.Find(Tools.Tools.AgentEnCours.Id);
                cmbCommerciaux.Enabled = false;
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdVentesParTypeContratTypeVilla_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                #region Ventes par unité et CA
                lvReportingCommercial.Items.Clear();

                var queryReportingCommercial = db.Contrats.Where(c => c.DateSouscription >= dtpDateDebut.Value
                                             && c.DateSouscription <= dtpDateFin.Value).OrderBy(c => c.DateSouscription.Value.Year).ThenBy(c => c.DateSouscription.Value.Month).ToList();
                if (cmbCommerciaux.SelectedItem != null)
                {
                    var commercial = (Agent)cmbCommerciaux.SelectedItem;
                    if(!commercial.IsChefEquipe)
                    { 
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.CommercialID == commercial.Id).ToList();
                    }
                    else
                    {
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.Commercial.ChefEquipeId == commercial.Id).ToList();
                    }
                }
                

                if (cmbIlotsVentes.SelectedItem != null)
                {
                    var ilot = (Ilot)cmbIlotsVentes.SelectedItem;
                    queryReportingCommercial = queryReportingCommercial.Where(c => c.Lot.IlotID == ilot.Id).ToList();
                }

                var queryReportingCommercial2 = queryReportingCommercial.GroupBy(c => new
                {
                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                        .Select(n => new
                                        {
                                            Annee = n.Key.Year,
                                            Mois = n.Key.Month,
                                            Résa = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Réservation).Count(),
                                            Dépôt = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt).Count()
                                        }).ToList();


                foreach (var item in queryReportingCommercial2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                    lviRepCommercial.SubItems.Add(item.Résa.ToString());

                    lviRepCommercial.SubItems.Add(item.Dépôt.ToString());
                    lviRepCommercial.SubItems.Add((item.Résa + item.Dépôt).ToString());
                    lvReportingCommercial.Items.Add(lviRepCommercial);
                }
                txtTotalResa.Text = queryReportingCommercial2.Sum(n => n.Résa).ToString();
                txtTotalDepot.Text = queryReportingCommercial2.Sum(n => n.Dépôt).ToString();
                txtTotalVentes.Text = (queryReportingCommercial2.Sum(n => n.Résa) + queryReportingCommercial2.Sum(n => n.Dépôt)).ToString();

                var dtf = CultureInfo.CurrentCulture.DateTimeFormat;
                #region Chargement chart Vente par unité
                //DataPoint point = new DataPoint();

                chartVentes.Series.Clear();
                chartVentes.Series.Add("Résa");
                chartVentes.Series.Add("Dépôt");

                chartVentes.Series["Résa"].IsValueShownAsLabel = true;

                chartVentes.Series["Dépôt"].IsValueShownAsLabel = true;

                chartVentes.Series["Résa"].LegendText = "Réservation";
                chartVentes.Series["Dépôt"].LegendText = "Dépôt";

                foreach (var item in queryReportingCommercial2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    var periode = mois + " " + annee;
                    chartVentes.Series["Résa"].Points.AddXY(periode, item.Résa);
                }
                foreach (var item in queryReportingCommercial2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    var periode = mois + " " + annee;
                    chartVentes.Series["Dépôt"].Points.AddXY(periode, item.Dépôt);
                }
                #endregion

                var queryVentesCA = queryReportingCommercial.GroupBy(c => new
                {
                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                       .Select(n => new
                                       {
                                           Annee = n.Key.Year,
                                           Mois = n.Key.Month,
                                           Résa = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Réservation).Sum(c => c.PrixFinal),
                                           Dépôt = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt).Sum(c => c.PrixFinal)
                                       }).ToList();

                lvVentesCA.Items.Clear();
                foreach (var item in queryVentesCA)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                    lviRepCommercial.SubItems.Add(item.Résa.ToString("### ### ##0"));
                    lviRepCommercial.SubItems.Add(item.Dépôt.ToString("### ### ##0"));
                    lviRepCommercial.SubItems.Add((item.Résa + item.Dépôt).ToString("### ### ##0"));
                    lvVentesCA.Items.Add(lviRepCommercial);
                }
                #region Chargement chart Vente par unité
                //DataPoint point2 = new DataPoint();

                chartVentesCA.Series.Clear();
                chartVentesCA.Series.Add("Résa");
                chartVentesCA.Series.Add("Dépôt");

                chartVentesCA.Series["Résa"].IsValueShownAsLabel = true;

                chartVentesCA.Series["Dépôt"].IsValueShownAsLabel = true;

                chartVentesCA.Series["Résa"].LegendText = "Réservation";
                chartVentesCA.Series["Dépôt"].LegendText = "Dépôt";

                foreach (var item in queryVentesCA)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    var periode = mois + " " + annee;
                    chartVentesCA.Series["Résa"].Points.AddXY(periode, item.Résa);
                }
                foreach (var item in queryVentesCA)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    var periode = mois + " " + annee;
                    chartVentesCA.Series["Dépôt"].Points.AddXY(periode, item.Dépôt);
                }

                foreach (Series b in chartVentesCA.Series)
                {
                    foreach (DataPoint c in b.Points)
                    {
                        //Sets just the legend to the dollar values
                        c.Label = c.YValues[0].ToString("### ### ##0");
                    }
                }
                #endregion

                #endregion

                #region VENTE PAR UNITE PAR TYPE VILLA
                lvVentesUTV.Items.Clear();
                var queryVenteUTV = db.Contrats.Where(c => c.DateSouscription >= dtpDateDebut.Value
                                            && c.DateSouscription <= dtpDateFin.Value).OrderBy(c => c.DateSouscription.Value.Year).ThenBy(c => c.DateSouscription.Value.Month).ToList();
                if (cmbCommerciaux.SelectedItem != null)
                {
                    var commercial = (Agent)cmbCommerciaux.SelectedItem;
                    //query = query.Where(c => c.CommercialID == commercial.Id).ToList();

                    if (!commercial.IsChefEquipe)
                    {
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.CommercialID == commercial.Id).ToList();
                    }
                    else
                    {
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.Commercial.ChefEquipeId == commercial.Id).ToList();
                    }
                }


                if (cmbIlotsVentes.SelectedItem != null)
                {
                    var ilot = (Ilot)cmbIlotsVentes.SelectedItem;
                    queryReportingCommercial = queryReportingCommercial.Where(c => c.Lot.IlotID == ilot.Id).ToList();
                }

                var queryVenteUTV2 = queryReportingCommercial.GroupBy(c => new
                {
                   
                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                        .Select(n => new
                                        {
                                            Annee = n.Key.Year,
                                            Mois = n.Key.Month,
                                            F2 = n.Where(c => c.Lot.TypeVilla.CodeType ==F2.CodeType ).Count(),
                                            F3 = n.Where(c => c.Lot.TypeVilla.CodeType == F3.CodeType).Count(),
                                            F4A = n.Where(c => c.Lot.TypeVilla.CodeType == F4A.CodeType).Count(),
                                            F4B= n.Where(c => c.Lot.TypeVilla.CodeType == F4B.CodeType).Count(),
                                            F5A= n.Where(c => c.Lot.TypeVilla.CodeType == F5A.CodeType).Count(),
                                            F5B= n.Where(c => c.Lot.TypeVilla.CodeType == F5B.CodeType).Count(),
                                        }).ToList();


                foreach (var item in queryVenteUTV2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                    lviRepCommercial.SubItems.Add(item.F2.ToString());
                    lviRepCommercial.SubItems.Add(item.F3.ToString());
                    lviRepCommercial.SubItems.Add(item.F4A.ToString());
                    lviRepCommercial.SubItems.Add(item.F4B.ToString());
                    lviRepCommercial.SubItems.Add(item.F5A.ToString());
                    lviRepCommercial.SubItems.Add(item.F5B.ToString());

                    lviRepCommercial.SubItems.Add((item.F2 + item.F3 + item.F4A + item.F4B + item.F5A + item.F5B).ToString());
                    lvVentesUTV.Items.Add(lviRepCommercial);
                }
                #region PCA
                lvVentesCATV.Items.Clear();
                var queryVenteCATV2 = queryReportingCommercial.GroupBy(c => new
                {

                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                        .Select(n => new
                                        {
                                            Annee = n.Key.Year,
                                            Mois = n.Key.Month,
                                            F2 = n.Where(c => c.Lot.TypeVilla.CodeType == F2.CodeType).Sum(c => c.PrixFinal),
                                            F3 = n.Where(c => c.Lot.TypeVilla.CodeType == F3.CodeType).Sum(c => c.PrixFinal),
                                            F4A = n.Where(c => c.Lot.TypeVilla.CodeType == F4A.CodeType).Sum(c => c.PrixFinal),
                                            F4B = n.Where(c => c.Lot.TypeVilla.CodeType == F4B.CodeType).Sum(c => c.PrixFinal),
                                            F5A = n.Where(c => c.Lot.TypeVilla.CodeType == F5A.CodeType).Sum(c => c.PrixFinal),
                                            F5B = n.Where(c => c.Lot.TypeVilla.CodeType == F5B.CodeType).Sum(c => c.PrixFinal),
                                        }).ToList();


                foreach (var item in queryVenteCATV2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                    lviRepCommercial.SubItems.Add(item.F2.ToString("### ### ##0"));
                    lviRepCommercial.SubItems.Add(item.F3.ToString("### ### ##0"));
                    lviRepCommercial.SubItems.Add(item.F4A.ToString("### ### ##0"));
                    lviRepCommercial.SubItems.Add(item.F4B.ToString("### ### ##0"));
                    lviRepCommercial.SubItems.Add(item.F5A.ToString("### ### ##0"));
                    lviRepCommercial.SubItems.Add(item.F5B.ToString("### ### ##0"));

                    lviRepCommercial.SubItems.Add((item.F2 + item.F3 + item.F4A + item.F4B + item.F5A + item.F5B).ToString());
                    lvVentesCATV.Items.Add(lviRepCommercial);
                }
                #endregion
                #endregion

                #region VENTE PAR OBJECTIF
                lvReportingParObjectif.Items.Clear();

                var queryRealise = db.Contrats.Where(c => c.DateSouscription >= dtpDateDebut.Value
                                             && c.DateSouscription <= dtpDateFin.Value).OrderBy(c => c.DateSouscription.Value.Year).ThenBy(c => c.DateSouscription.Value.Month).ToList();
                if (cmbCommerciaux.SelectedItem != null)
                {
                    var commercial = (Agent)cmbCommerciaux.SelectedItem;
                    if (!commercial.IsChefEquipe)
                    {
                        queryRealise = queryRealise.Where(c => c.CommercialID == commercial.Id).ToList();
                        
                            var queryRealise2 = queryRealise.GroupBy(c => new
                            {
                                c.DateSouscription.Value.Year,
                                c.DateSouscription.Value.Month,
                            })
                                              .Select(n => new
                                              {
                                                  Annee = n.Key.Year,
                                                  Mois = n.Key.Month,
                                                  Objectif = db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                                                             db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0,
                                                  Realisé = n.Count(),
                                                  Taux = n.Count()/(db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                                                             db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0) * 100,
                                              }).ToList();

                            foreach (var item in queryRealise2)
                            {
                                var annee = item.Annee;
                                var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                                ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                                lviRepCommercial.SubItems.Add(item.Objectif.ToString("##0"));

                                lviRepCommercial.SubItems.Add(item.Realisé.ToString());
                                lviRepCommercial.SubItems.Add(item.Taux.ToString() + "%");
                                lvReportingParObjectif.Items.Add(lviRepCommercial);
                            }

                            chartObjectifsVentes.Series.Clear();
                            chartObjectifsVentes.Series.Add("Objectif");
                            chartObjectifsVentes.Series.Add("Réalisé");

                            chartObjectifsVentes.Series["Objectif"].IsValueShownAsLabel = true;

                            chartObjectifsVentes.Series["Réalisé"].IsValueShownAsLabel = true;

                            chartObjectifsVentes.Series["Objectif"].LegendText = "Objectif";
                            chartObjectifsVentes.Series["Réalisé"].LegendText = "Réalisé";
                            foreach (var item in queryRealise2)
                            {
                                var annee = item.Annee;
                                var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                                var periode = mois + " " + annee;
                                chartObjectifsVentes.Series["Objectif"].Points.AddXY(periode, item.Objectif);
                            }
                            foreach (var item in queryRealise2)
                            {
                                var annee = item.Annee;
                                var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                                var periode = mois + " " + annee;
                                chartObjectifsVentes.Series["Réalisé"].Points.AddXY(periode, item.Realisé);
                            }
                    }
                    else
                    {
                        queryRealise = queryRealise.Where(c => c.Commercial.ChefEquipeId == commercial.Id).ToList();

                        var queryRealise2 = queryRealise.GroupBy(c => new
                        {
                            c.DateSouscription.Value.Year,
                            c.DateSouscription.Value.Month,
                        })
                                        .Select(n => new
                                        {
                                            Annee = n.Key.Year,
                                            Mois = n.Key.Month,
                                            Objectif = db.ObjectifMensuels.Where(o => o.Commercial.ChefEquipeId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                                                       db.ObjectifMensuels.Where(o => o.Commercial.ChefEquipeId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0,
                                            Realisé = n.Count(),
                                            Taux = (n.Count() / (db.ObjectifMensuels.Where(o => o.Commercial.ChefEquipeId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                                                        db.ObjectifMensuels.Where(o => o.Commercial.ChefEquipeId== commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0) * 100),
                                        }).ToList();

                        foreach (var item in queryRealise2)
                        {
                            var annee = item.Annee;
                            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                            ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                            lviRepCommercial.SubItems.Add(item.Objectif.ToString("##0"));

                            lviRepCommercial.SubItems.Add(item.Realisé.ToString());
                            lviRepCommercial.SubItems.Add(item.Taux.ToString() + "%");
                            lvReportingParObjectif.Items.Add(lviRepCommercial);
                        }
                        chartObjectifsVentes.Series.Clear();
                        chartObjectifsVentes.Series.Add("Objectif");
                        chartObjectifsVentes.Series.Add("Réalisé");

                        chartObjectifsVentes.Series["Objectif"].IsValueShownAsLabel = true;

                        chartObjectifsVentes.Series["Réalisé"].IsValueShownAsLabel = true;

                        chartObjectifsVentes.Series["Objectif"].LegendText = "Objectif";
                        chartObjectifsVentes.Series["Réalisé"].LegendText = "Réalisé";
                        foreach (var item in queryRealise2)
                        {
                            var annee = item.Annee;
                            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                            var periode = mois + " " + annee;
                            chartObjectifsVentes.Series["Objectif"].Points.AddXY(periode, item.Objectif);
                        }
                        foreach (var item in queryRealise2)
                        {
                            var annee = item.Annee;
                            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                            var periode = mois + " " + annee;
                            chartObjectifsVentes.Series["Réalisé"].Points.AddXY(periode, item.Realisé);
                        }

                    }
                }
                else
                {
                    var queryRealise2 = queryRealise.GroupBy(c => new
                    {
                        c.DateSouscription.Value.Year,
                        c.DateSouscription.Value.Month,
                    })
                                        .Select(n => new
                                        {
                                            Annee = n.Key.Year,
                                            Mois = n.Key.Month,
                                            Objectif = db.ObjectifMensuels.Where(o => o.Annee == n.Key.Year && o.Mois == n.Key.Month).Sum(o => o.objectifVente),
                                            Realisé = n.Count(),
                                            Taux = (n.Count() / db.ObjectifMensuels.Where(o => o.Annee == n.Key.Year && o.Mois == n.Key.Month).Sum(o => o.objectifVente) * 100),
                                        }).ToList();

                    foreach (var item in queryRealise2)
                    {
                        var annee = item.Annee;
                        var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                        ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                        lviRepCommercial.SubItems.Add(item.Objectif.ToString("##0"));

                        lviRepCommercial.SubItems.Add(item.Realisé.ToString());
                        lviRepCommercial.SubItems.Add(item.Taux.ToString() + "%");
                        lvReportingParObjectif.Items.Add(lviRepCommercial);
                    }
                    chartObjectifsVentes.Series.Clear();
                    chartObjectifsVentes.Series.Add("Objectif");
                    chartObjectifsVentes.Series.Add("Réalisé");

                    chartObjectifsVentes.Series["Objectif"].IsValueShownAsLabel = true;

                    chartObjectifsVentes.Series["Réalisé"].IsValueShownAsLabel = true;

                    chartObjectifsVentes.Series["Objectif"].LegendText = "Objectif";
                    chartObjectifsVentes.Series["Réalisé"].LegendText = "Réalisé";
                    foreach (var item in queryRealise2)
                    {
                        var annee = item.Annee;
                        var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                        var periode = mois + " " + annee;
                        chartObjectifsVentes.Series["Objectif"].Points.AddXY(periode, item.Objectif);
                    }
                    foreach (var item in queryRealise2)
                    {
                        var annee = item.Annee;
                        var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                        var periode = mois + " " + annee;
                        chartObjectifsVentes.Series["Réalisé"].Points.AddXY(periode, item.Realisé);
                    }
                }

                // var queryObjectif = db.ObjectifMensuels.Where(o => o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ? db.ObjectifMensuels.Where(o => o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0;
                //var queryRealise2 = queryRealise.ToList() ;
                if (cmbCommerciaux.SelectedItem != null)
                {
                    //var commercial = (Agent)cmbCommerciaux.SelectedItem;
                    //if (!commercial.IsChefEquipe) //Commercial
                    //{
                    //    var queryRealise2 = queryRealise.GroupBy(c => new
                    //    {
                    //        c.DateSouscription.Value.Year,
                    //        c.DateSouscription.Value.Month,
                    //    })
                    //                      .Select(n => new
                    //                      {
                    //                          Annee = n.Key.Year,
                    //                          Mois = n.Key.Month,
                    //                          Objectif = db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                    //                                     db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0,
                    //                          Realisé = n.Count(),
                    //                          Taux =( db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                    //                                     db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0/ n.Count()*100),
                    //                      }).ToList();

                    //    foreach (var item in queryRealise2)
                    //    {
                    //        var annee = item.Annee;
                    //        var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    //        ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                    //        lviRepCommercial.SubItems.Add(item.Objectif.ToString("##0"));

                    //        lviRepCommercial.SubItems.Add(item.Realisé.ToString());
                    //        lviRepCommercial.SubItems.Add(item.Taux.ToString("##0")+"%");
                    //        lvReportingParObjectif.Items.Add(lviRepCommercial);
                    //    }

                    //    chartObjectifsVentes.Series.Clear();
                    //    chartObjectifsVentes.Series.Add("Objectif");
                    //    chartObjectifsVentes.Series.Add("Réalisé");

                    //    chartObjectifsVentes.Series["Objectif"].IsValueShownAsLabel = true;

                    //    chartObjectifsVentes.Series["Réalisé"].IsValueShownAsLabel = true;

                    //    chartObjectifsVentes.Series["Objectif"].LegendText = "Objectif";
                    //    chartObjectifsVentes.Series["Réalisé"].LegendText = "Réalisé";
                    //    foreach (var item in queryRealise2)
                    //    {
                    //        var annee = item.Annee;
                    //        var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    //        var periode = mois + " " + annee;
                    //        chartObjectifsVentes.Series["Objectif"].Points.AddXY(periode, item.Objectif);
                    //    }
                    //    foreach (var item in queryRealise2)
                    //    {
                    //        var annee = item.Annee;
                    //        var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    //        var periode = mois + " " + annee;
                    //        chartObjectifsVentes.Series["Réalisé"].Points.AddXY(periode, item.Realisé);
                    //    }


                    //}
                    //else
                    //{
                        //var queryRealise2 = queryRealise.GroupBy(c => new
                        //{
                        //    c.DateSouscription.Value.Year,
                        //    c.DateSouscription.Value.Month,
                        //})
                        //                 .Select(n => new
                        //                 {
                        //                     Annee = n.Key.Year,
                        //                     Mois = n.Key.Month,
                        //                     Objectif = db.ObjectifMensuels.Where(o => o.Commercial.ChefEquipeId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                        //                                db.ObjectifMensuels.Where(o => o.Commercial.ChefEquipeId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0,
                        //                     Realisé = n.Count(),
                        //                     Taux = (  n.Count()/(db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                        //                                 db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0) * 100),
                        //                 }).ToList();

                        //foreach (var item in queryRealise2)
                        //{
                        //    var annee = item.Annee;
                        //    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                        //    ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                        //    lviRepCommercial.SubItems.Add(item.Objectif.ToString("##0"));

                        //    lviRepCommercial.SubItems.Add(item.Realisé.ToString());
                        //    lviRepCommercial.SubItems.Add(item.Taux.ToString());
                        //    lvReportingParObjectif.Items.Add(lviRepCommercial);
                        //}
                        //chartObjectifsVentes.Series.Clear();
                        //chartObjectifsVentes.Series.Add("Objectif");
                        //chartObjectifsVentes.Series.Add("Réalisé");

                        //chartObjectifsVentes.Series["Objectif"].IsValueShownAsLabel = true;

                        //chartObjectifsVentes.Series["Réalisé"].IsValueShownAsLabel = true;

                        //chartObjectifsVentes.Series["Objectif"].LegendText = "Objectif";
                        //chartObjectifsVentes.Series["Réalisé"].LegendText = "Réalisé";
                        //foreach (var item in queryRealise2)
                        //{
                        //    var annee = item.Annee;
                        //    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                        //    var periode = mois + " " + annee;
                        //    chartObjectifsVentes.Series["Objectif"].Points.AddXY(periode, item.Objectif);
                        //}
                        //foreach (var item in queryRealise2)
                        //{
                        //    var annee = item.Annee;
                        //    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                        //    var periode = mois + " " + annee;
                        //    chartObjectifsVentes.Series["Réalisé"].Points.AddXY(periode, item.Realisé);
                        //}
                    //}

                }
                else
                {
                    //var queryRealise2 = queryRealise.GroupBy(c => new
                    //{
                    //    c.DateSouscription.Value.Year,
                    //    c.DateSouscription.Value.Month,
                    //})
                    //                     .Select(n => new
                    //                     {
                    //                         Annee = n.Key.Year,
                    //                         Mois = n.Key.Month,
                    //                         Objectif =  db.ObjectifMensuels.Where(o => o.Annee == n.Key.Year && o.Mois == n.Key.Month).Sum(o => o.objectifVente),
                    //                         Realisé = n.Count(),
                    //                         Taux = (n.Count() /db.ObjectifMensuels.Where(o => o.Annee == n.Key.Year && o.Mois == n.Key.Month).Sum(o => o.objectifVente)  * 100),
                    //                     }).ToList();

                    //foreach (var item in queryRealise2)
                    //{
                    //    var annee = item.Annee;
                    //    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    //    ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                    //    lviRepCommercial.SubItems.Add(item.Objectif.ToString("##0"));

                    //    lviRepCommercial.SubItems.Add(item.Realisé.ToString());
                    //    lviRepCommercial.SubItems.Add(item.Taux.ToString());
                    //    lvReportingParObjectif.Items.Add(lviRepCommercial);
                    //}
                    //chartObjectifsVentes.Series.Clear();
                    //chartObjectifsVentes.Series.Add("Objectif");
                    //chartObjectifsVentes.Series.Add("Réalisé");

                    //chartObjectifsVentes.Series["Objectif"].IsValueShownAsLabel = true;

                    //chartObjectifsVentes.Series["Réalisé"].IsValueShownAsLabel = true;

                    //chartObjectifsVentes.Series["Objectif"].LegendText = "Objectif";
                    //chartObjectifsVentes.Series["Réalisé"].LegendText = "Réalisé";
                    //foreach (var item in queryRealise2)
                    //{
                    //    var annee = item.Annee;
                    //    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    //    var periode = mois + " " + annee;
                    //    chartObjectifsVentes.Series["Objectif"].Points.AddXY(periode, item.Objectif);
                    //}
                    //foreach (var item in queryRealise2)
                    //{
                    //    var annee = item.Annee;
                    //    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    //    var periode = mois + " " + annee;
                    //    chartObjectifsVentes.Series["Réalisé"].Points.AddXY(periode, item.Realisé);
                    //}
                }
                


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                     "Prosopis - Gestion des rapports de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdExporter_Click(object sender, EventArgs e)
        {
            try
            {
                #region Vente par Unite 
                this.Cursor = Cursors.WaitCursor;
                
                #region Ventes par unité et CA

                var queryReportingCommercial = db.Contrats.Where(c => c.DateSouscription >= dtpDateDebut.Value
                                             && c.DateSouscription <= dtpDateFin.Value).OrderBy(c => c.DateSouscription.Value.Year).ThenBy(c => c.DateSouscription.Value.Month).ToList();
                if (cmbCommerciaux.SelectedItem != null)
                {
                    var commercial = (Agent)cmbCommerciaux.SelectedItem;
                    if (!commercial.IsChefEquipe)
                    {
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.CommercialID == commercial.Id).ToList();
                    }
                    else
                    {
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.Commercial.ChefEquipeId == commercial.Id).ToList();
                    }
                }


                if (cmbIlotsVentes.SelectedItem != null)
                {
                    var ilot = (Ilot)cmbIlotsVentes.SelectedItem;
                    queryReportingCommercial = queryReportingCommercial.Where(c => c.Lot.IlotID == ilot.Id).ToList();
                }

                var queryReportingCommercial2 = queryReportingCommercial.GroupBy(c => new
                {
                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                        .Select(n => new
                                        {
                                            Annee = n.Key.Year,
                                            Mois = n.Key.Month,
                                            Résa = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Réservation).Count(),
                                            Dépôt = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt).Count()
                                        }).ToList();

                #endregion

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                //if (!bPrint)
                xlApp.Visible = true;
                //xlWorkBook = xlApp.Workbooks.Add(misValue);
                string dossierTemplates = Tools.Tools.DossierTemplates;

                xlWorkBook = xlApp.Workbooks.Open(dossierTemplates + "RapportDesVentes.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
           

                int numOrdre = 0;
                int iDepart = 14;// à partir ligne 15
                Excel.Range range = xlWorkSheet.UsedRange;

                foreach (var vente in queryReportingCommercial2)
                {
                    var annee = vente.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(vente.Mois);
                    var periode = mois + " " + annee;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = periode;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 2] = vente.Résa;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 3] = vente.Dépôt;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 4] = vente.Dépôt+vente.Résa;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    numOrdre++;
                }
                #endregion
                
                #region Vente en CA
                var queryVentesCA = queryReportingCommercial.GroupBy(c => new
                {
                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                       .Select(n => new
                                       {
                                           Annee = n.Key.Year,
                                           Mois = n.Key.Month,
                                           Résa = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Réservation).Sum(c => c.PrixFinal),
                                           Dépôt = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt).Sum(c => c.PrixFinal)
                                       }).ToList();
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
                numOrdre = 0;
                iDepart = 14;// à partir ligne 15
                range = xlWorkSheet.UsedRange;

                foreach (var vente in queryVentesCA)
                {
                    var annee = vente.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(vente.Mois);
                    var periode = mois + " " + annee;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = periode;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 2] = vente.Résa;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 3] = vente.Dépôt;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 4] = vente.Dépôt + vente.Résa;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    numOrdre++;
                }
                #endregion

                #region Vente en unite par Type villa
                var queryVenteUTV = db.Contrats.Where(c => c.DateSouscription >= dtpDateDebut.Value
                                           && c.DateSouscription <= dtpDateFin.Value).OrderBy(c => c.DateSouscription.Value.Year).ThenBy(c => c.DateSouscription.Value.Month).ToList();
                if (cmbCommerciaux.SelectedItem != null)
                {
                    var commercial = (Agent)cmbCommerciaux.SelectedItem;
                    //query = query.Where(c => c.CommercialID == commercial.Id).ToList();

                    if (!commercial.IsChefEquipe)
                    {
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.CommercialID == commercial.Id).ToList();
                    }
                    else
                    {
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.Commercial.ChefEquipeId == commercial.Id).ToList();
                    }
                }


                if (cmbIlotsVentes.SelectedItem != null)
                {
                    var ilot = (Ilot)cmbIlotsVentes.SelectedItem;
                    queryReportingCommercial = queryReportingCommercial.Where(c => c.Lot.IlotID == ilot.Id).ToList();
                }

                var queryVenteUTV2 = queryReportingCommercial.GroupBy(c => new
                {

                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                        .Select(n => new
                                        {
                                            Annee = n.Key.Year,
                                            Mois = n.Key.Month,
                                            F2 = n.Where(c => c.Lot.TypeVilla.CodeType == F2.CodeType).Count(),
                                            F3 = n.Where(c => c.Lot.TypeVilla.CodeType == F3.CodeType).Count(),
                                            F4A = n.Where(c => c.Lot.TypeVilla.CodeType == F4A.CodeType).Count(),
                                            F4B = n.Where(c => c.Lot.TypeVilla.CodeType == F4B.CodeType).Count(),
                                            F5A = n.Where(c => c.Lot.TypeVilla.CodeType == F5A.CodeType).Count(),
                                            F5B = n.Where(c => c.Lot.TypeVilla.CodeType == F5B.CodeType).Count(),
                                        }).ToList();

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(3);
                numOrdre = 0;
                iDepart = 14;// à partir ligne 15
                range = xlWorkSheet.UsedRange;

                foreach (var vente in queryVenteUTV2)
                {
                    var annee = vente.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(vente.Mois);
                    var periode = mois + " " + annee;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = periode;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 2] = vente.F2;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 3] = vente.F3;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 4] = vente.F4A ;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 5] = vente.F4B;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 6] = vente.F5A;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 7] = vente.F5A;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    numOrdre++;
                }
                #endregion
                #region vente en CA par Type villa 
                var queryVenteCATV2 = queryReportingCommercial.GroupBy(c => new
                {

                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                        .Select(n => new
                                        {
                                            Annee = n.Key.Year,
                                            Mois = n.Key.Month,
                                            F2 = n.Where(c => c.Lot.TypeVilla.CodeType == F2.CodeType).Sum(c => c.PrixFinal),
                                            F3 = n.Where(c => c.Lot.TypeVilla.CodeType == F3.CodeType).Sum(c => c.PrixFinal),
                                            F4A = n.Where(c => c.Lot.TypeVilla.CodeType == F4A.CodeType).Sum(c => c.PrixFinal),
                                            F4B = n.Where(c => c.Lot.TypeVilla.CodeType == F4B.CodeType).Sum(c => c.PrixFinal),
                                            F5A = n.Where(c => c.Lot.TypeVilla.CodeType == F5A.CodeType).Sum(c => c.PrixFinal),
                                            F5B = n.Where(c => c.Lot.TypeVilla.CodeType == F5B.CodeType).Sum(c => c.PrixFinal),
                                        }).ToList();
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(4);
                numOrdre = 0;
                iDepart = 14;// à partir ligne 15
                range = xlWorkSheet.UsedRange;

                foreach (var vente in queryVenteCATV2)
                {
                    var annee = vente.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(vente.Mois);
                    var periode = mois + " " + annee;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = periode;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 2] = vente.F2;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 3] = vente.F3;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 4] = vente.F4A;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 5] = vente.F4B;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 6] = vente.F5A;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 7] = vente.F5A;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    numOrdre++;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                   "Prosopis - Gestion des rapports de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdObjReal_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
            //var queryRealise = db.Contrats.Where(c => c.DateSouscription >= dtpDateDebut.Value
            //                               && c.DateSouscription <= dtpDateFin.Value).OrderBy(c => c.DateSouscription.Value.Year).ThenBy(c => c.DateSouscription.Value.Month).ToList();

                var queryRealise = db.ObjectifMensuels.Where(o => o.DateDebut>= dtpDateDebut.Value
                                               && o.DateFin <= dtpDateFin.Value).OrderBy(o => o.Annee).ThenBy(c => c.Mois).ToList(); ;


                var queryRealise2 = queryRealise.GroupBy(c => new
                {
                    c.Annee,
                    c.Mois
                })
                                    .Select(n => new
                                    {
                                        Annee = n.Key.Annee,
                                        Mois = n.Key.Mois,
                                        Objectif = n.Sum(o => o.objectifVente),
                                        Realise = db.Contrats.Where(c => c.DateSouscription.Value.Year == n.Key.Annee && c.DateSouscription.Value.Month == n.Key.Mois).Count(),
                                        Taux=(int) db.Contrats.Where(c => c.DateSouscription.Value.Year == n.Key.Annee && c.DateSouscription.Value.Month == n.Key.Mois).Count() / n.Sum(o => o.objectifVente)*100,
                                        //Realisé = n.Count(),
                                        //Taux = n.Count() / (db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                                        //           db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0) * 100,
                                    }).ToList();
                lvReportingParObjectif.Items.Clear();
                foreach (var item in queryRealise2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                    lviRepCommercial.SubItems.Add(item.Objectif.ToString("##0"));

                    lviRepCommercial.SubItems.Add(item.Realise.ToString());
                    lviRepCommercial.SubItems.Add(item.Taux.ToString("##0") + "%");
                    lvReportingParObjectif.Items.Add(lviRepCommercial);
                }
                chartObjectifsVentes.Series.Clear();
                chartObjectifsVentes.Series.Add("Objectif");
                chartObjectifsVentes.Series.Add("Réalisé");

                chartObjectifsVentes.Series["Objectif"].IsValueShownAsLabel = true;

                chartObjectifsVentes.Series["Réalisé"].IsValueShownAsLabel = true;

                chartObjectifsVentes.Series["Objectif"].LegendText = "Objectif";
                chartObjectifsVentes.Series["Réalisé"].LegendText = "Réalisé";
                foreach (var item in queryRealise2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    var periode = mois + " " + annee;
                    chartObjectifsVentes.Series["Objectif"].Points.AddXY(periode, item.Objectif);
                }
                foreach (var item in queryRealise2)
                {
                    var annee = item.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                    var periode = mois + " " + annee;
                    chartObjectifsVentes.Series["Réalisé"].Points.AddXY(periode, item.Realise);
                }










                if (cmbCommerciaux.SelectedItem != null)
                {
                    var commercial = (Agent)cmbCommerciaux.SelectedItem;
                    if (!commercial.IsChefEquipe)
                    {
                        queryRealise = queryRealise.Where(o => o.CommercialId == commercial.Id).ToList();
                        var queryRealise2Commercial = queryRealise.GroupBy(c => new
                        {
                            c.Annee,
                            c.Mois
                        })
                                          .Select(n => new
                                          {
                                              Annee = n.Key.Annee,
                                              Mois = n.Key.Mois,
                                              Objectif = n.Sum(o => o.objectifVente),
                                              Realise = db.Contrats.Where(c =>c.CommercialID==commercial.Id && c.DateSouscription.Value.Year == n.Key.Annee && c.DateSouscription.Value.Month == n.Key.Mois).Count(),
                                              Taux= (int)db.Contrats.Where(c => c.CommercialID == commercial.Id && c.DateSouscription.Value.Year == n.Key.Annee && c.DateSouscription.Value.Month == n.Key.Mois).Count() / n.Sum(o => o.objectifVente)*100,
                                              //Realisé = n.Count(),
                                              //Taux = n.Count() / (db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                                              //           db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0) * 100,
                                          }).ToList();
                        lvReportingParObjectif.Items.Clear();
                        foreach (var item in queryRealise2Commercial)
                        {
                            var annee = item.Annee;
                            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                            ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                            lviRepCommercial.SubItems.Add(item.Objectif.ToString("##0"));

                            lviRepCommercial.SubItems.Add(item.Realise.ToString());
                            lviRepCommercial.SubItems.Add(item.Taux.ToString("##0") + "%");
                            lvReportingParObjectif.Items.Add(lviRepCommercial);
                        }
                        chartObjectifsVentes.Series.Clear();
                        chartObjectifsVentes.Series.Add("Objectif");
                        chartObjectifsVentes.Series.Add("Réalisé");

                        chartObjectifsVentes.Series["Objectif"].IsValueShownAsLabel = true;

                        chartObjectifsVentes.Series["Réalisé"].IsValueShownAsLabel = true;

                        chartObjectifsVentes.Series["Objectif"].LegendText = "Objectif";
                        chartObjectifsVentes.Series["Réalisé"].LegendText = "Réalisé";
                        foreach (var item in queryRealise2Commercial)
                        {
                            var annee = item.Annee;
                            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                            var periode = mois + " " + annee;
                            chartObjectifsVentes.Series["Objectif"].Points.AddXY(periode, item.Objectif);
                        }
                        foreach (var item in queryRealise2Commercial)
                        {
                            var annee = item.Annee;
                            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                            var periode = mois + " " + annee;
                            chartObjectifsVentes.Series["Réalisé"].Points.AddXY(periode, item.Realise);
                        }

                    }
                    else
                    {
                        queryRealise = queryRealise.Where(o => o.Commercial.ChefEquipeId == commercial.Id).ToList();
                        var queryRealise2ChefEquipe = queryRealise.GroupBy(c => new
                        {
                            c.Annee,
                            c.Mois
                        })
                                          .Select(n => new
                                          {
                                              Annee = n.Key.Annee,
                                              Mois = n.Key.Mois,
                                              Objectif = n.Sum(o => o.objectifVente),
                                              Realise = db.Contrats.Where(c => c.Commercial.ChefEquipeId == commercial.Id && c.DateSouscription.Value.Year == n.Key.Annee && c.DateSouscription.Value.Month == n.Key.Mois).Count(),
                                              Taux = (int)db.Contrats.Where(c => c.Commercial.ChefEquipeId == commercial.Id && c.DateSouscription.Value.Year == n.Key.Annee && c.DateSouscription.Value.Month == n.Key.Mois).Count() / n.Sum(o => o.objectifVente)*100,
                                              //Realisé = n.Count(),
                                              //Taux = n.Count() / (db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                                              //           db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0) * 100,
                                          }).ToList();
                        lvReportingParObjectif.Items.Clear();
                        foreach (var item in queryRealise2ChefEquipe)
                        {
                            var annee = item.Annee;
                            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                            ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                            lviRepCommercial.SubItems.Add(item.Objectif.ToString("##0"));

                            lviRepCommercial.SubItems.Add(item.Realise.ToString());
                            lviRepCommercial.SubItems.Add(item.Taux.ToString("##0")+"%");
                            lvReportingParObjectif.Items.Add(lviRepCommercial);
                        }
                        chartObjectifsVentes.Series.Clear();
                        chartObjectifsVentes.Series.Add("Objectif");
                        chartObjectifsVentes.Series.Add("Réalisé");

                        chartObjectifsVentes.Series["Objectif"].IsValueShownAsLabel = true;

                        chartObjectifsVentes.Series["Réalisé"].IsValueShownAsLabel = true;

                        chartObjectifsVentes.Series["Objectif"].LegendText = "Objectif";
                        chartObjectifsVentes.Series["Réalisé"].LegendText = "Réalisé";
                        foreach (var item in queryRealise2ChefEquipe)
                        {
                            var annee = item.Annee;
                            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                            var periode = mois + " " + annee;
                            chartObjectifsVentes.Series["Objectif"].Points.AddXY(periode, item.Objectif);
                        }
                        foreach (var item in queryRealise2ChefEquipe)
                        {
                            var annee = item.Annee;
                            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                            var periode = mois + " " + annee;
                            chartObjectifsVentes.Series["Réalisé"].Points.AddXY(periode, item.Realise);
                        }
                    }
                }

                        //        foreach (var item in queryRealise2)
                        //        {
                        //            var annee = item.Annee;
                        //            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                        //            ListViewItem lviRepCommercial = new ListViewItem(mois + " " + annee);
                        //            lviRepCommercial.SubItems.Add(item.Objectif.ToString("##0"));

                        //            lviRepCommercial.SubItems.Add(item.Realisé.ToString());
                        //            lviRepCommercial.SubItems.Add(item.Taux.ToString() + "%");
                        //            lvReportingParObjectif.Items.Add(lviRepCommercial);
                        //        }

                        //        chartObjectifsVentes.Series.Clear();
                        //        chartObjectifsVentes.Series.Add("Objectif");
                        //        chartObjectifsVentes.Series.Add("Réalisé");

                        //        chartObjectifsVentes.Series["Objectif"].IsValueShownAsLabel = true;

                        //        chartObjectifsVentes.Series["Réalisé"].IsValueShownAsLabel = true;

                        //        chartObjectifsVentes.Series["Objectif"].LegendText = "Objectif";
                        //        chartObjectifsVentes.Series["Réalisé"].LegendText = "Réalisé";
                        //        foreach (var item in queryRealise2)
                        //        {
                        //            var annee = item.Annee;
                        //            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                        //            var periode = mois + " " + annee;
                        //            chartObjectifsVentes.Series["Objectif"].Points.AddXY(periode, item.Objectif);
                        //        }
                        //        foreach (var item in queryRealise2)
                        //        {
                        //            var annee = item.Annee;
                        //            var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Mois);
                        //            var periode = mois + " " + annee;
                        //            chartObjectifsVentes.Series["Réalisé"].Points.AddXY(periode, item.Realisé);
                        //        }
                        //    }
                        //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                   "Prosopis - Gestion des rapports de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void tsExporter_Click(object sender, EventArgs e)
        {
            try
            {
                #region Vente par Unite 
                this.Cursor = Cursors.WaitCursor;

                #region Ventes par unité et CA

                var queryReportingCommercial = db.Contrats.Where(c => c.DateSouscription >= dtpDateDebut.Value
                                             && c.DateSouscription <= dtpDateFin.Value).OrderBy(c => c.DateSouscription.Value.Year).ThenBy(c => c.DateSouscription.Value.Month).ToList();
                if (cmbCommerciaux.SelectedItem != null)
                {
                    var commercial = (Agent)cmbCommerciaux.SelectedItem;
                    if (!commercial.IsChefEquipe)
                    {
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.CommercialID == commercial.Id).ToList();
                    }
                    else
                    {
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.Commercial.ChefEquipeId == commercial.Id).ToList();
                    }
                }


                if (cmbIlotsVentes.SelectedItem != null)
                {
                    var ilot = (Ilot)cmbIlotsVentes.SelectedItem;
                    queryReportingCommercial = queryReportingCommercial.Where(c => c.Lot.IlotID == ilot.Id).ToList();
                }

                var queryReportingCommercial2 = queryReportingCommercial.GroupBy(c => new
                {
                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                        .Select(n => new
                                        {
                                            Annee = n.Key.Year,
                                            Mois = n.Key.Month,
                                            Résa = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Réservation).Count(),
                                            Dépôt = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt).Count()
                                        }).ToList();

                #endregion

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                //if (!bPrint)
                xlApp.Visible = true;
                //xlWorkBook = xlApp.Workbooks.Add(misValue);
                string dossierTemplates = Tools.Tools.DossierTemplates;

                xlWorkBook = xlApp.Workbooks.Open(dossierTemplates + "RapportDesVentes.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


                int numOrdre = 0;
                int iDepart = 14;// à partir ligne 15
                Excel.Range range = xlWorkSheet.UsedRange;

                foreach (var vente in queryReportingCommercial2)
                {
                    var annee = vente.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(vente.Mois);
                    var periode = mois + " " + annee;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = periode;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 2] = vente.Résa;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 3] = vente.Dépôt;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 4] = vente.Dépôt + vente.Résa;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    numOrdre++;
                }
                #endregion

                #region Vente en CA
                var queryVentesCA = queryReportingCommercial.GroupBy(c => new
                {
                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                       .Select(n => new
                                       {
                                           Annee = n.Key.Year,
                                           Mois = n.Key.Month,
                                           Résa = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Réservation).Sum(c => c.PrixFinal),
                                           Dépôt = n.Where(c => c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt).Sum(c => c.PrixFinal)
                                       }).ToList();
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
                numOrdre = 0;
                iDepart = 14;// à partir ligne 15
                range = xlWorkSheet.UsedRange;

                foreach (var vente in queryVentesCA)
                {
                    var annee = vente.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(vente.Mois);
                    var periode = mois + " " + annee;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = periode;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 2] = vente.Résa;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 3] = vente.Dépôt;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 4] = vente.Dépôt + vente.Résa;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    numOrdre++;
                }
                #endregion

                #region Vente en unite par Type villa
                var queryVenteUTV = db.Contrats.Where(c => c.DateSouscription >= dtpDateDebut.Value
                                           && c.DateSouscription <= dtpDateFin.Value).OrderBy(c => c.DateSouscription.Value.Year).ThenBy(c => c.DateSouscription.Value.Month).ToList();
                if (cmbCommerciaux.SelectedItem != null)
                {
                    var commercial = (Agent)cmbCommerciaux.SelectedItem;
                    //query = query.Where(c => c.CommercialID == commercial.Id).ToList();

                    if (!commercial.IsChefEquipe)
                    {
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.CommercialID == commercial.Id).ToList();
                    }
                    else
                    {
                        queryReportingCommercial = queryReportingCommercial.Where(c => c.Commercial.ChefEquipeId == commercial.Id).ToList();
                    }
                }


                if (cmbIlotsVentes.SelectedItem != null)
                {
                    var ilot = (Ilot)cmbIlotsVentes.SelectedItem;
                    queryReportingCommercial = queryReportingCommercial.Where(c => c.Lot.IlotID == ilot.Id).ToList();
                }

                var queryVenteUTV2 = queryReportingCommercial.GroupBy(c => new
                {

                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                        .Select(n => new
                                        {
                                            Annee = n.Key.Year,
                                            Mois = n.Key.Month,
                                            F2 = n.Where(c => c.Lot.TypeVilla.CodeType == F2.CodeType).Count(),
                                            F3 = n.Where(c => c.Lot.TypeVilla.CodeType == F3.CodeType).Count(),
                                            F4A = n.Where(c => c.Lot.TypeVilla.CodeType == F4A.CodeType).Count(),
                                            F4B = n.Where(c => c.Lot.TypeVilla.CodeType == F4B.CodeType).Count(),
                                            F5A = n.Where(c => c.Lot.TypeVilla.CodeType == F5A.CodeType).Count(),
                                            F5B = n.Where(c => c.Lot.TypeVilla.CodeType == F5B.CodeType).Count(),
                                        }).ToList();

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(3);
                numOrdre = 0;
                iDepart = 14;// à partir ligne 15
                range = xlWorkSheet.UsedRange;

                foreach (var vente in queryVenteUTV2)
                {
                    var annee = vente.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(vente.Mois);
                    var periode = mois + " " + annee;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = periode;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 2] = vente.F2;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 3] = vente.F3;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 4] = vente.F4A;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 5] = vente.F4B;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 6] = vente.F5A;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 7] = vente.F5A;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    numOrdre++;
                }
                #endregion
                #region vente en CA par Type villa 
                var queryVenteCATV2 = queryReportingCommercial.GroupBy(c => new
                {

                    c.DateSouscription.Value.Year,
                    c.DateSouscription.Value.Month,
                })
                                        .Select(n => new
                                        {
                                            Annee = n.Key.Year,
                                            Mois = n.Key.Month,
                                            F2 = n.Where(c => c.Lot.TypeVilla.CodeType == F2.CodeType).Sum(c => c.PrixFinal),
                                            F3 = n.Where(c => c.Lot.TypeVilla.CodeType == F3.CodeType).Sum(c => c.PrixFinal),
                                            F4A = n.Where(c => c.Lot.TypeVilla.CodeType == F4A.CodeType).Sum(c => c.PrixFinal),
                                            F4B = n.Where(c => c.Lot.TypeVilla.CodeType == F4B.CodeType).Sum(c => c.PrixFinal),
                                            F5A = n.Where(c => c.Lot.TypeVilla.CodeType == F5A.CodeType).Sum(c => c.PrixFinal),
                                            F5B = n.Where(c => c.Lot.TypeVilla.CodeType == F5B.CodeType).Sum(c => c.PrixFinal),
                                        }).ToList();
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(4);
                numOrdre = 0;
                iDepart = 14;// à partir ligne 15
                range = xlWorkSheet.UsedRange;

                foreach (var vente in queryVenteCATV2)
                {
                    var annee = vente.Annee;
                    var mois = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(vente.Mois);
                    var periode = mois + " " + annee;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1] = periode;
                    xlWorkSheet.Cells[numOrdre + iDepart, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 2] = vente.F2;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //Catégorie contrat
                    xlWorkSheet.Cells[numOrdre + iDepart, 3] = vente.F3;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 4] = vente.F4A;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 5] = vente.F4B;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 5].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 6] = vente.F5A;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 6].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWorkSheet.Cells[numOrdre + iDepart, 7] = vente.F5A;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[numOrdre + iDepart, 7].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    numOrdre++;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                   "Prosopis - Gestion des rapports de vente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}

