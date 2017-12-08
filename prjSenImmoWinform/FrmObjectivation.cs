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
using System.Transactions;
using System.Windows.Forms;

namespace prjSenImmoWinform
{
    public partial class FrmObjectivation : Form
    {
        private AgentRepository agentRep;
        private SenImmoDataContext db;
        private Agent leCommercialEnCours;

        public FrmObjectivation()
        {
            InitializeComponent();
            db = new SenImmoDataContext();
            var RoleCOmmerciale = db.Roles.Where(c => c.CodeRole == "CMC").SingleOrDefault();
            dtpAnnee.Format = DateTimePickerFormat.Custom;
            dtpAnnee.CustomFormat = "yyyy";
            dtpAnnee.ShowUpDown = true;


            cmbCommerciaux.DataSource = db.Agents.Where(c => c.RoleId == RoleCOmmerciale.ID).ToList();
            cmbCommerciaux.DisplayMember = "NomComplet";
            cmbCommerciaux.SelectedIndex = -1;


            agentRep = new AgentRepository();
            ChargerObjectifsAnnuels(dtpAnnee.Value.Year);
        }

        private void ChargerObjectifsAnnuels(int annee)
        {
            var query = agentRep.GetObjectifsAgents(annee).ToList();
            dgObjectifsAgents.DataSource = query.Select
                (ob => new
                {
                    id = ob.ID,
                    Commercial = ob.Commercial.NomComplet,
                    Anee = ob.Annee,
                    Objectif = ob.objectifVente
                }).ToList();
            //txtObjectifCumule.Text = query.Sum(ob => ob.objectifVente).ToString("###");
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {

        }

        private void txtObjectifAnnuel_Validated(object sender, EventArgs e)
        {
            try
            {
                decimal objectifAnnuel = decimal.Parse(txtObjectifAnnuel.Text);
                //txtObjectifTrimestriel.Text = (objectifAnnuel / 4).ToString();
                var objectifMensuel = (objectifAnnuel / 12);
                //txtObjectifMensuel.Text = (objectifAnnuel / 12).ToString();
                txtJanvier.Text = objectifMensuel.ToString();
                txtFevrier.Text = objectifMensuel.ToString();
                txtMars.Text = objectifMensuel.ToString();
                txtAvril.Text = objectifMensuel.ToString();
                txtMai.Text = objectifMensuel.ToString();
                txtJuin.Text = objectifMensuel.ToString();
                txtJuillet.Text = objectifMensuel.ToString();
                txtAout.Text = objectifMensuel.ToString();
                txtSeptembre.Text = objectifMensuel.ToString();
                txtOctobre.Text = objectifMensuel.ToString();
                txtNovembre.Text = objectifMensuel.ToString();
                txtDecembre.Text = objectifMensuel.ToString();



                txtJanvier.Text = objectifMensuel.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis -  Gestion des objectifs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgObjectifsAgents_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                int objectifAnnuelId;
                if (dgObjectifsAgents.SelectedRows.Count > 0)
                {
                    objectifAnnuelId = (int)dgObjectifsAgents.SelectedRows[0].Cells[0].Value;
                    var objectifsMensuels = db.ObjectifMensuels.Where(om => om.ObjectifAnnuelId == objectifAnnuelId);

                    // dgObjectifsMensuels.DataSource = objectifsMensuels.ToList();
                   

                }
                else
                    return;

                var commercialId = db.ObjectifMensuels.Where(om => om.ObjectifAnnuelId == objectifAnnuelId).FirstOrDefault().CommercialId;
                leCommercialEnCours = db.Agents.Find(commercialId);
                AfficherObjectif(leCommercialEnCours,dtpAnnee.Value.Year);
                var queryRealise = db.ObjectifMensuels.Where(om => om.CommercialId == commercialId && om.DateDebut >= dtpDateDebut.Value.Date
                                                             && om.DateFin <= dtpDateFin.Value.Date );
                //.Where(o => o.DateDebut >= dtpDateDebut.Value
                //                          && o.DateFin <= dtpDateFin.Value).OrderBy(o => o.Annee).ThenBy(c => c.Mois).ToList(); ;


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
                                        Realise = db.Contrats.Where(c => c.CommercialID == commercialId && c.DateSouscription.Value.Year == n.Key.Annee && c.DateSouscription.Value.Month == n.Key.Mois).Count(),
                                        Taux = (int)db.Contrats.Where(c => c.CommercialID == commercialId && c.DateSouscription.Value.Year == n.Key.Annee && c.DateSouscription.Value.Month == n.Key.Mois).Count() / n.Sum(o => o.objectifVente) * 100,
                                        //CA = db.Contrats.Where(c => c.CommercialID == commercialId && c.DateSouscription.Value.Year == n.Key.Annee && c.DateSouscription.Value.Month == n.Key.Mois).Sum(),
                                        //Realisé = n.Count(),
                                        //Taux = n.Count() / (db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault() != null ?
                                        //           db.ObjectifMensuels.Where(o => o.CommercialId == commercial.Id && o.Annee == n.Key.Year && o.Mois == n.Key.Month).FirstOrDefault().objectifVente : 0) * 100,
                                    }).ToList();
                dgObjectifs.DataSource = queryRealise2;
                EffacerFormEvaluation();
                CalculerCommissionCommercial((int)commercialId, dtpDateDebut.Value.Date, dtpDateFin.Value.Date);


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis -  Gestion des objectifs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EffacerForm()
        {
            txtObjectifAnnuel.Text = string.Empty;
          
            txtJanvier.Text = string.Empty;
            txtFevrier.Text = string.Empty;
            txtMars.Text = string.Empty;
            txtAvril.Text = string.Empty;
            txtMai.Text = string.Empty;
            txtJuin.Text = string.Empty;
            txtJuillet.Text = string.Empty;
            txtAout.Text = string.Empty;
            txtSeptembre.Text = string.Empty;
            txtOctobre.Text = string.Empty;
            txtNovembre.Text = string.Empty;
            txtDecembre.Text = string.Empty;
        }

        private void AfficherObjectif(Agent leCommercial,int annee)
        {
            cmbCommerciaux.SelectedItem = leCommercial;
            var objectifAnnuel = db.ObjectifAnnuels.Where(om => om.CommercialId == leCommercial.Id 
                                        && om.Annee==annee).FirstOrDefault();
            txtObjectifAnnuel.Text = objectifAnnuel.objectifVente.ToString("##0");
            var objectifsMensuel = db.ObjectifMensuels.Where(om => om.CommercialId == leCommercial.Id
                                         && om.Annee == annee).OrderBy(om => om.Mois);
            txtJanvier.Text = objectifsMensuel.Where(om => om.Mois == 1).FirstOrDefault().objectifVente.ToString("##0");
            txtFevrier.Text = objectifsMensuel.Where(om => om.Mois == 2).FirstOrDefault().objectifVente.ToString("##0");
            txtMars.Text = objectifsMensuel.Where(om => om.Mois == 3).FirstOrDefault().objectifVente.ToString("##0");
            txtAvril.Text = objectifsMensuel.Where(om => om.Mois == 4).FirstOrDefault().objectifVente.ToString("##0");
            txtMai.Text = objectifsMensuel.Where(om => om.Mois == 5).FirstOrDefault().objectifVente.ToString("##0");
            txtJuin.Text = objectifsMensuel.Where(om => om.Mois == 6).FirstOrDefault().objectifVente.ToString("##0");
            txtJuillet.Text = objectifsMensuel.Where(om => om.Mois == 7).FirstOrDefault().objectifVente.ToString("##0");
            txtAout.Text = objectifsMensuel.Where(om => om.Mois == 8).FirstOrDefault().objectifVente.ToString("##0");
            txtSeptembre.Text = objectifsMensuel.Where(om => om.Mois == 9).FirstOrDefault().objectifVente.ToString("##0");
            txtOctobre.Text = objectifsMensuel.Where(om => om.Mois == 10).FirstOrDefault().objectifVente.ToString("##0");
            txtNovembre.Text = objectifsMensuel.Where(om => om.Mois == 11).FirstOrDefault().objectifVente.ToString("##0");
            txtDecembre.Text = objectifsMensuel.Where(om => om.Mois == 12).FirstOrDefault().objectifVente.ToString("##0");
        }

        private void CalculerCommissionCommercial(int commercialId, DateTime DateDebut, DateTime DateFin)
        {
           var objectifCommercial= db.ObjectifMensuels.Where(om => om.CommercialId == commercialId && om.DateDebut>=DateDebut 
           && om.DateFin <= DateFin).Sum(o => (decimal?)o.objectifVente) ?? 0;
            //.Sum(f => (decimal?)f.Encaissements.Sum(enc =>enc.Montant)))??0,
            txtObjectif.Text = objectifCommercial.ToString("##0");
            var realiseCommercial = db.Contrats.Where(c => c.CommercialID == commercialId && c.DateSouscription >= DateDebut && c.DateSouscription <= DateFin).Count();
            txtRealise.Text = realiseCommercial.ToString();
            if (realiseCommercial > 0)
            {
                var tauxAtteinteCommercial = (realiseCommercial / objectifCommercial * 100);
                txtTauxAtteinte.Text = tauxAtteinteCommercial.ToString("##0") + "%";
                var tauxCommmission = db.TauxAtteintes.Where(t => tauxAtteinteCommercial >= t.TauxMinimun && tauxAtteinteCommercial < t.TauxMaximun).FirstOrDefault();
                txtIntervalle.Text = "[ " + tauxCommmission.TauxMinimun.ToString("##0") + "% - " + tauxCommmission.TauxMaximun.ToString("##0") + "%]";
                txtTauxCommission.Text = tauxCommmission.Taux.ToString() + "%";
                var caRealiseCommercial = db.Contrats.Where(c => c.CommercialID == commercialId && c.DateSouscription >= DateDebut && c.DateSouscription <= DateFin).Sum(c => c.PrixFinal);
                txtCA.Text = caRealiseCommercial.ToString("### ### ##0");
                var commissionCommercial = (caRealiseCommercial * tauxCommmission.Taux / 100);
                txtCommission.Text = commissionCommercial.ToString("### ### ##0");
            }
        }

        public void EffacerFormEvaluation()
        {
            txtObjectif.Text = string.Empty;
            txtRealise.Text = string.Empty;
            txtTauxAtteinte.Text = string.Empty;
            txtIntervalle.Text = string.Empty;
            txtTauxCommission.Text = string.Empty;
            txtCA.Text = string.Empty;
            txtCommission.Text = string.Empty;
        }
        private void RecalculerObjectifAnnuel(object sender, EventArgs e)
        {

        }
        private void cmdEnregistrer_Click(object sender, EventArgs e)
        {
            List<TextBox> ListTxtMOis = new List<TextBox>()
            {
                txtJanvier,txtFevrier,txtMars,txtAvril,txtMai,txtJuin,txtJuillet,txtAout,txtSeptembre,txtOctobre,txtNovembre,txtDecembre
            };
            try
            {
                this.Cursor = Cursors.AppStarting;
                using (var scope = new TransactionScope())
                {
                    using (var ctx = new SenImmoDataContext())
                    {
                        //Vérifier si l'objectif annuel n'est pas déja enregistré
                        var objectifAnnuel = int.Parse(txtObjectifAnnuel.Text);
                        var objAnnuelDejaEnregistre = ctx.ObjectifAnnuels.Where(o => o.CommercialId == leCommercialEnCours.Id && o.Annee == dtpAnnee.Value.Year).ToList();
                        if(objAnnuelDejaEnregistre.Count > 0)
                        {
                            MessageBox.Show(this, "L'objectif annuel "+ dtpAnnee.Value.Year+" a déjà été enregistré pour ce commercial:" ,
                                            "Prosopis - Gestion des objectifs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            scope.Dispose();
                            return;
                        }
                        DateTime dateDebutAnnee = new DateTime(dtpAnnee.Value.Year, 1, 1);
                        DateTime dateFinAnnee = new DateTime(dtpAnnee.Value.Year, 12, 31);

                        ObjectifAnnuel objAnnuel = new ObjectifAnnuel
                        {
                            CommercialId = leCommercialEnCours.Id,
                            Annee = dtpAnnee.Value.Year,
                            objectifVente = objectifAnnuel,
                            DateDebut = dateDebutAnnee.Date,
                            DateFin = dateFinAnnee.Date

                        };
                        ctx.ObjectifAnnuels.Add(objAnnuel);
                        int i = 1;
                        decimal objAnnuelCalcule = 0;
                        foreach (var textbox in ListTxtMOis)
                        {
                            DateTime dateDebutMois = new DateTime(dtpAnnee.Value.Year, i, 1);
                            DateTime dateFinMois = new DateTime(dtpAnnee.Value.Year, i, DateTime.DaysInMonth(dtpAnnee.Value.Year, i));
                            var objectifMensuel = new ObjectifMensuel()
                            {
                                objectifVente = int.Parse(textbox.Text),
                                Mois = i,
                                Annee = dtpAnnee.Value.Year,
                                ObjectifAnnuelId = objAnnuel.ID,
                                CommercialId = leCommercialEnCours.Id,
                                DateDebut= dateDebutMois,
                                DateFin= dateFinMois

                            };

                            ctx.ObjectifMensuels.Add(objectifMensuel);
                            objAnnuelCalcule += int.Parse(textbox.Text);
                            i++;
                        }
                        objAnnuel.objectifVente = objAnnuelCalcule;

                        ctx.SaveChanges();
                       
                    }
                    scope.Complete();
                    MessageBox.Show(this, "Objectif annuel enregistré avec succes:" ,
                        "Prosopis - Gestion des objectifs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
                ChargerObjectifsAnnuels(dtpAnnee.Value.Year);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:" + ex.Message,
                         "Prosopis - Gestion des objectifs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmbCommerciaux_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbCommerciaux.SelectedItem!=null)
            {
                leCommercialEnCours = (Agent)cmbCommerciaux.SelectedItem;
                EffacerForm();


            }
        }

        private void dtpAnnee_ValueChanged(object sender, EventArgs e)
        {
            EffacerForm();
            dtpDateDebut.Value = DateTime.Parse("01/01/"+dtpAnnee.Value.Year);
            dtpDateFin.Value = DateTime.Parse("31/12/" + dtpAnnee.Value.Year);
            ChargerObjectifsAnnuels(dtpAnnee.Value.Year);
        }

        private void dtpDateDebut_ValueChanged(object sender, EventArgs e)
        {
            dgObjectifsAgents_SelectionChanged(sender, e);
        }

        private void FrmObjectivation_Load(object sender, EventArgs e)
        {
            dtpDateDebut.Value = DateTime.Parse("01/01/" + dtpAnnee.Value.Year);
            dtpDateFin.Value = DateTime.Parse("31/12/" + dtpAnnee.Value.Year);
        }

        
        

        private void dtpDateFin_ValueChanged(object sender, EventArgs e)
        {
            dgObjectifsAgents_SelectionChanged(sender, e);
        }

        private void rbReservation_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReservation.Checked)
            {
                DateTime dateReference = dtpDateDebutRecouvrement.Value.Date.AddDays(-1);

                try
                {
                    var objectifRecouvrementResa = db.Factures.Where(f => f.Contrat.Statut == StatutContrat.Actif 
                    && f.Echue == true && f.TypeFacture == TypeFacture.AppelDeFond
                    );//&& f.Active==true
                   var ARecouvrer= objectifRecouvrementResa
                        .Where(f => f.FacturePayee == false &&
                        f.DateEcheanceFacture <= dateReference).ToList().Select(f => new
                        {
                            Date = f.DateEcheanceFacture,
                            Numero=f.NumeroFacture,
                            Contrat=f.Contrat.NumeroContrat,
                            Montant = f.Montant,
                            Restant = f.Montant - (decimal?)f.Encaissements.Where(enc => enc.Date <= dateReference).Sum(enc => enc.Montant) ?? 0
                        }
                    ).ToList();
                    DgObjectifRecouvrement.DataSource = ARecouvrer;
                    var totalARecouvrer= ARecouvrer.Sum(f =>f.Restant);
                    txtObjectifRecouvrement.Text = totalARecouvrer.ToString("### ### ##0");

                    var encaissementsSurAppelsDeFond = db.Encaissements.Where(enc => enc.Facture.Contrat.Statut == StatutContrat.Actif
                      && enc.Facture.Echue == true && enc.Facture.TypeFacture == TypeFacture.AppelDeFond 
                      && enc.Date >= dtpDateDebutRecouvrement.Value.Date && enc.Date <= dtpDateFinRecouvrement.Value.Date);
                    DgRealiseRecouvrement.DataSource = encaissementsSurAppelsDeFond.ToList();
                    var realiseRecouvrement= encaissementsSurAppelsDeFond.Sum(enc => enc.Montant);
                    txtRealiseRecouvrement.Text= realiseRecouvrement.ToString("### ### ##0");
                    //
                    if (realiseRecouvrement > 0)
                    {
                        var tauxAtteinteRecouvrement = (realiseRecouvrement / totalARecouvrer * 100);
                        txtTauxAtteinteRecouvrement.Text = tauxAtteinteRecouvrement.ToString("##0") + "%";
                        var tauxCommmissionReouvrement = db.TauxAtteintes.Where(t => tauxAtteinteRecouvrement >= t.TauxMinimun && tauxAtteinteRecouvrement < t.TauxMaximun).FirstOrDefault();
                        txtIntervalleRecouvrement.Text = "[ " + tauxCommmissionReouvrement.TauxMinimun.ToString("##0") + "% - " + tauxCommmissionReouvrement.TauxMaximun.ToString("##0") + "%]";
                        txtTauxCommissionRecouvrement.Text = tauxCommmissionReouvrement.Taux.ToString() + "%";
                        //var caRealiseCommercial = db.Contrats.Where(c => c.CommercialID == commercialId && c.DateSouscription >= DateDebut && c.DateSouscription <= DateFin).Sum(c => c.PrixFinal);
                        //txtCA.Text = caRealiseCommercial.ToString("### ### ##0");
                        var commissionRecouvrement = (realiseRecouvrement * tauxCommmissionReouvrement.Taux / 100);
                        txtCommissionRecouvrement.Text = commissionRecouvrement.ToString("### ### ##0");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Erreur:" + ex.Message,
                            "Prosopis - Gestion des objectifs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dtpDateDebutRecouvrement_ValueChanged(object sender, EventArgs e)
        {
            dtpDateFinRecouvrement.Value = dtpDateDebutRecouvrement.Value.AddMonths(3);
        }
    }
}
