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

namespace prjSenImmoWinform
{
    public partial class Form5 : Form
    {
        private SenImmoDataContext db = null;
        public Form5()
        {
            InitializeComponent();
            db = new SenImmoDataContext();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db = new SenImmoDataContext();
            //using (var db= new SenImmoDataContext())
            //{
            var AllOptions= db.Options.ToList();
            AllOptions = AllOptions.Where(o =>o.Active==true && o.TypeContrat.CategorieContrat== CategorieContrat.Dépôt && o.ContratGenere ==true).ToList();
            //Client RESA
            //AllOptions = AllOptions.Where(o => o.TypeContrat.CategorieContrat == CategorieContrat.Réservation && o.Client.EncaissementProspects.Where(enc => enc.FraisDeDossier == false).Sum(enc => enc.MontantGlobal) <
            //                                     (o.PrixDeVente * o.TypeContrat.SeuilEntreeEnVigueur / 100)).ToList();

            //AllOptions = AllOptions.Where(o => o.TypeContrat.CategorieContrat == CategorieContrat.Dépôt
            //                            && o.Client.EncaissementProspects.Where(enc => enc.FraisDeDossier == false).Sum(enc => enc.MontantGlobal) <
            //                                    (o.PrixDeVente * o.TypeContrat.SeuilEntreeEnVigueur / 100)).ToList();
            ////dgResult.DataSource = AllOptions;
            //foreach (Option option in AllOptions)
            //{
            //    option.SeuilContratAtteint = false;
            //    db.SaveChanges();
            //}
            //
            var listIDClients = (from c in db.Contrats
                                select c.ClientID).ToList();
            AllOptions = AllOptions.Where(o => !listIDClients.Contains(o.ClientID.Value)).ToList();
            foreach (Option option in AllOptions)
            {
                option.ContratGenere = false;
                db.SaveChanges();
            }
            dgResult.DataSource =( from o in AllOptions
                                  select new
                                  {
                                      Client = o.Client.NomComplet,
                                      Option = o.TypeContrat.LibelleTypeContrat,
                                      TypeVilla = o.TypeVilla.NomComplet

                                  }).ToList();
            MessageBox.Show("TERMINE");
            //}

               
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            db = new SenImmoDataContext();
            //using (var db= new SenImmoDataContext())
            //{
            var AllContrats = db.Contrats.Where(c => c.Statut == StatutContrat.Actif).ToList();
            var AllContratsDoublons= AllContrats.GroupBy(c => new { Numero = c.NumeroContrat })
                                    .Where(grp => grp.Count() > 1)
                                    .Select(grp => grp).ToList();

            //foreach (var groupContrat in AllContratsDoublons)
            //{
            //    var lesContratsASupprimer = db.Contrats.Where(c => c.NumeroContrat == groupContrat.Key.Numero).Max(c => c.Id);
            //}

           

          //  AllContrats = AllContrats.Where(c => c.Statut == StatutContrat.Actif ).ToList();
            //Client RESA
            //AllOptions = AllOptions.Where(o => o.TypeContrat.CategorieContrat == CategorieContrat.Réservation && o.Client.EncaissementProspects.Where(enc => enc.FraisDeDossier == false).Sum(enc => enc.MontantGlobal) <
            //                                     (o.PrixDeVente * o.TypeContrat.SeuilEntreeEnVigueur / 100)).ToList();

            //AllOptions = AllOptions.Where(o => o.TypeContrat.CategorieContrat == CategorieContrat.Dépôt
            //                            && o.Client.EncaissementProspects.Where(enc => enc.FraisDeDossier == false).Sum(enc => enc.MontantGlobal) <
            //                                    (o.PrixDeVente * o.TypeContrat.SeuilEntreeEnVigueur / 100)).ToList();
            ////dgResult.DataSource = AllOptions;
            //foreach (Option option in AllOptions)
            //{
            //    option.SeuilContratAtteint = false;
            //    db.SaveChanges();
            //}
            ////
            //var listIDClients = (from c in db.Contrats
            //                     select c.ClientID).ToList();
            //AllOptions = AllOptions.Where(o => !listIDClients.Contains(o.ClientID.Value)).ToList();
            //foreach (Option option in AllOptions)
            //{
            //    option.ContratGenere = false;
            //    db.SaveChanges();
            //}
            dgResult.DataSource = (from o in AllContratsDoublons
                                   select new
                                   { numero = o.Key.Numero }
                                  ).ToList();
            MessageBox.Show("TERMINE");
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void cmdDeRelettrer_Click(object sender, EventArgs e)
        {

            db = new SenImmoDataContext();
            //using (var db= new SenImmoDataContext())

            List<Encaissement> ListeEncaissements=new List<Encaissement>();

            var lesContrats = db.Contrats.Where(c => c.Statut == StatutContrat.Actif && c.TypeContrat.CategorieContrat == CategorieContrat.Dépôt).ToList();
            foreach (var contrat in lesContrats)
            {
                try
                {
                    var AllEncaissementsGlobaux = db.EncaissementGlobals.Where(
                                                            c => c.Contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt
                                                            && c.NumeroEncaissement.Substring(0, 4) != "ENFD"
                                                            && c.ContratId == contrat.Id).ToList();
                    foreach (var encG in AllEncaissementsGlobaux)
                    {
                        //if(encG.ID== 20024)
                        //{
                        foreach (var lettrage in encG.Encaissements)
                        {
                            lettrage.Facture.FacturePayee = false;
                        }
                        db.Encaissements.RemoveRange(encG.Encaissements);


                        #region VENTILLATION DU PAIEMENT SUR LES FACTURES
                        var montantAVentiller = encG.MontantGlobal;
                        var lesFactures = encG.Contrat.Factures.Where(ech => ech.FacturePayee == false && ech.TypeFacture != TypeFacture.FraisDossier).OrderBy(ech => ech.DateEcheanceFacture);
                        foreach (var fact in lesFactures)
                        {
                            if (montantAVentiller > 0)
                            {
                                if (fact.Encaissements != null)
                                {
                                    var totalEncaissement = fact.Encaissements.Sum(u => u.Montant);
                                    var resteAEncaisser = fact.Montant - totalEncaissement;
                                    decimal montantAEncaisser = 0;

                                    if (montantAVentiller >= resteAEncaisser)
                                    {
                                        montantAEncaisser = resteAEncaisser;
                                    }
                                    else
                                    {
                                        montantAEncaisser = montantAVentiller;
                                    }
                                    Encaissement nouvelEncaissement = new Encaissement
                                    {
                                        Date = encG.DateEncaissement,
                                        ModePaiement = encG.ModePaiement,
                                        Montant = montantAEncaisser,
                                        Commentaire = encG.Commentaire,
                                        ReferencePaiement = encG.ReferencePaiement
                                    };
                                    fact.Encaissements.Add(nouvelEncaissement);

                                    encG.Encaissements.Add(nouvelEncaissement);
                                    if (montantAVentiller >= resteAEncaisser)
                                    {
                                        fact.FacturePayee = true;

                                    }
                                    fact.Active = true;
                                    montantAVentiller -= montantAEncaisser;
                                }
                            }
                            else
                                break;
                        }
                        encG.EncaissementLettre = true;
                        // VERIFIER SI LES FACTURES DU CONTRAT SONT TOUTES SOLDEES
                        //if (contrat.Factures.Sum(f => f.Montant - f.Encaissements.Sum(enc => enc.Montant)) <= 0)
                        //{
                        //    contrat.ContratSolde = true;
                        //}
                        #endregion


                        //}
                        db.SaveChanges();
                        // ListeEncaissements.AddRange(encG.Encaissements);
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Erreur sur le contrat:" + contrat.NumeroContrat + "(" + contrat.Id + ")");
                    continue;
                }
            }
            dgResult.DataSource = ListeEncaissements;
           MessageBox.Show("TERMINE");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            db = new SenImmoDataContext();
            //using (var db= new SenImmoDataContext())

            List<Encaissement> ListeEncaissements = new List<Encaissement>();

            var lesContrats = db.Contrats.Where(c => c.Statut == StatutContrat.Actif 
                                                    && c.PrixRevise==0).ToList();
            dgResult.DataSource = lesContrats;
            foreach (var contrat in lesContrats)
            {
                try
                {
                    contrat.PrixRevise = contrat.Lot.PrixRevise;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erreur sur le contrat:" + contrat.NumeroContrat + "(" + contrat.Id + ")");
                    continue;
                }
            }
            dgResult.DataSource = lesContrats;

        }

        private void cmdPrixRevise_Click(object sender, EventArgs e)
        {
            try
            {
                db = new SenImmoDataContext();
                decimal prixRevise;
                //using (var db= new SenImmoDataContext())
                var listLots = db.Lots.ToList();
                foreach (var lot in listLots)
                {
                    if (lot.StatutLot!= StatutLot.Reserve && lot.StatutLot != StatutLot.Vendu)
                    {
                        var prixStandard = lot.TypeVilla.PrixStandard;
                        var superficieStandard = lot.TypeVilla.SurfaceDeBase;
                        var differenceSurface = lot.Superficie - superficieStandard;
                        var differencePrix = differenceSurface * 40000;

                        prixRevise = prixStandard + differencePrix;
                        if (lot.PositionLot == PositionLot.Angle)
                            prixRevise += prixRevise * 10 / 100;
                        lot.PrixRevise = prixRevise;

                        db.SaveChanges();
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur sur le client:"+ex.Message);
                
            }
        }

        private void cmdFormatterTel_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                foreach (Client client in db.Clients.ToList())
                {
                    try
                    {
                        if (client.TelephoneFixe != null) client.TelephoneFixe = client.TelephoneFixe.Replace(" ", string.Empty);
                        if (client.Mobile1 != null) client.Mobile1 = client.Mobile1.Replace(" ", string.Empty);
                        if (client.TelephoneBureau != null) client.TelephoneBureau = client.TelephoneBureau.Replace(" ", string.Empty);
                        if (client.Fax != null) client.Fax = client.Fax.Replace(" ", string.Empty);
                        db.SaveChanges();
                        label1.Text = i++ + ": client " + client.ID + " - " + client.NomComplet;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(i++ + ": client " + client.ID + " - " + client.NomComplet);
                        continue;
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur sur le client:" + ex.Message);

            }
        }

        private void cmdMAJDateContrat_Click(object sender, EventArgs e)
        {
            try
            {
                decimal dMontantEncaisse = 0;
                decimal dMontantSeuil = 0;
                int i = 0;
                foreach (Contrat contrat in db.Contrats.ToList())
                {
                    dMontantEncaisse = 0;
                    try
                    {
                        var encaissements = contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4)!= "ENFD")
                            .OrderBy(enc =>enc.DateEncaissement).ToList();
                        foreach (EncaissementGlobal encaissement in encaissements)
                        {
                            dMontantEncaisse += encaissement.MontantGlobal;
                            dMontantSeuil = contrat.PrixFinal * contrat.TypeContrat.SeuilSouscription / 100;
                            if (dMontantEncaisse >= dMontantSeuil)
                            {
                                contrat.DateSouscription = encaissement.DateEncaissement;
                                db.SaveChanges();
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(i++ + ": Contrat " + contrat.Id + " - " + contrat.NumeroContrat);
                        continue;
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur sur le client:" + ex.Message);

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int lotId = 0;
            try
            {
                foreach (Lot lot in db.Lots.Where(l => l.LotVirtuel==false).ToList())
                {
                    lotId = lot.ID;

                    if (db.EtatAvancements.Where(ea => ea.LotId == lot.ID && ea.Actif == true).Count() >0)
                    {
                        var typeEtatAvidMax = db.EtatAvancements.Where(ea => ea.LotId == lot.ID && ea.Actif == true).Max(etat => etat.TypeEtatAvancementID);
                        if (typeEtatAvidMax != 0)
                        {
                            var etatAvancement = db.EtatAvancements.FirstOrDefault(ea => ea.LotId == lot.ID && ea.Actif == true && ea.TypeEtatAvancementID == typeEtatAvidMax);

                            etatAvancement.Encours = true;

                            db.SaveChanges();
                        } 
                    }
                }
                
                MessageBox.Show("terminé");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur sur le lot:"+ lotId+ ", " + ex.Message);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var clients = db.Clients.Where(cl => cl.Contrats.Where(ct => ct.Statut == StatutContrat.Actif).Count() > 1).ToList();

                MessageBox.Show("terminé");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur " + ex.Message);

            }
        }
    }
}





