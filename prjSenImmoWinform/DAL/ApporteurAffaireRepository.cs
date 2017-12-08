using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjSenImmoWinform.Models;

namespace prjSenImmoWinform.DAL
{
    public class ApporteurAffaireRepository : IRepository<ApporteurAffaire>
    {

        SenImmoDataContext DB;

        public ApporteurAffaireRepository()
        {
            DB = new SenImmoDataContext();

        }
        public IEnumerable<ApporteurAffaire> List
        {
            get
            {
                return DB.ApporteurAffaires.Where(c => c.Actif==true);
            }
        }

        public IEnumerable<ApporteurAffaire> GetAllApporteurAffaires()
        {
            try
            {
                return DB.ApporteurAffaires.Where(c => c.Actif==true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Contrat> GetAllContrats(ApporteurAffaire af)
        {
            try
            {
                return DB.Contrats.Where(c =>c.ApporteurID==af.Id && c.Statut!= StatutContrat.Résilié);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<FactureCommissionGlobale> GetAllFactureCommissionGlobales(ApporteurAffaire af)
        {
            try
            {
                return DB.FactureCommissionsGlobales.Where(c => c.ApporteurAffaireId == af.Id);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public FactureCommissionGlobale GetFactureCommissionGlobales(int factureCommissionGlobaleId)
        {
            try
            {
                return DB.FactureCommissionsGlobales.Find( factureCommissionGlobaleId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GenererFactureGlobale(ApporteurAffaire apporteurAffaires)
        {
            try
            {
                var lesCommissionsAFacturer = DB.FactureCommissions.Where(fact => fact.EncaissementGlobal.Contrat.ApporteurID == apporteurAffaires.Id 
                                                                        && fact.FactureGenere == false);
                //lesCommissionsAFacturer = lesCommissionsAFacturer.Where(fact => fact.Date < System.Data.Entity.DbFunctions.AddMonths(DateTime.Now, -1));
                if (lesCommissionsAFacturer.Count() <=0 )
                    throw new Exception("Il n'y a pas d'encaissements à facturer pour cet apporteur d'affaires");
                //Verifier qu'il n'y a pas de facture globale impayée avant de générer une autre facture
                //var lesFacturesGlobalesGénéreesEtImpayees = DB.FactureCommissionsGlobales.Where(fact => fact.ApporteurAffaireId== apporteurAffaires.Id 
                //                                            && fact.F == false);
                //if (lesFacturesGlobalesGénéreesEtImpayees.Count() > 0)
                //    throw new Exception("Il existent des factures impayées pour cet apporteur d'affaires, veuillez dabord les solder avant de lancer une autre facturation");

               // var contratsApporteur = this.GetAllContrats(apporteurAffaires);
               // var lesContratAFacturer = contratsApporteur.Where(cont => cont.FactureCommissions.Any(fact => fact.FactureGenere==false));

            

                //var montantGlobal = lesContratAFacturer.Sum(cont => cont.FactureCommissions.Sum(fact => fact.MontantAPayer - fact.PaiementCommissions.Sum(enc => enc.MontantPaye)));
                var montantGlobal = lesCommissionsAFacturer.Sum(fact => fact.MontantAPayer);
                var factureCommissionGlobale = new FactureCommissionGlobale()
                {
                    ApporteurAffaireId = apporteurAffaires.Id,
                    MontantAPayer = montantGlobal,
                    Date = DateTime.Now.Date,
                    Motif = "",
                    Payee = false

                };
                DB.FactureCommissionsGlobales.Add(factureCommissionGlobale);
              
                    foreach (var facture in lesCommissionsAFacturer)
                    {
                        factureCommissionGlobale.FactureCommissions.Add(facture);
                        facture.FactureGenere = true;
                    }
               
                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<MouvementComptable> GetOperationsApporteurAffaires(int apporteurId)
        {
            try
            {


                var query = (from fact in DB.FactureCommissionsGlobales
                             where fact.ApporteurAffaireId == apporteurId
                             select new MouvementComptable
                             {
                                 Id = fact.ID,
                                 DateOp = fact.Date.Value,
                                 NumeroPiece = "",
                                 LibelleOp = fact.Motif,
                                 Debit = 0,
                                 Credit = fact.MontantAPayer,
                                 Solde = fact.Payee? 0 : fact.MontantAPayer,
                                 TypeOp = "F"
                             })
                               .Union
                               (from enc in DB.PaiementCommissionGlobals
                                where enc.ApporteurAffaireId == apporteurId
                                select new MouvementComptable
                                {
                                    Id = enc.ID,
                                    DateOp = enc.Date.Value,
                                    NumeroPiece = "",
                                    LibelleOp = enc.ReferencePaiement,
                                    Debit = enc.MontantPaye,
                                    Credit =0 ,
                                    Solde = 0,
                                    TypeOp = "E"
                                }).OrderBy(op => op.DateOp);
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EnregistrerPaiementCommission(int apporteurId, List<FactureCommissionGlobale> fatureCommissionsGlobales, DateTime dateVersement, decimal montantVersement, ModePaiement modePaiement, string referencePaiement, string commentairePaiement)
        {
            try
            {

                //// Enregistrer le premier versement

                var paiementCommission = new PaiementCommissionGlobal()
                {
                    //Numero = "PC",
                    ApporteurAffaireId= apporteurId,
                    Date = dateVersement.Date,
                    MontantPaye = montantVersement,
                    //FactureCommissionGlobaleId = contrat.Id,
                    ModePaiement = modePaiement,
                    ReferencePaiement = referencePaiement,
                    Commentaire = commentairePaiement
                };

                DB.PaiementCommissionGlobals.Add(paiementCommission);
             

                foreach (var facture in fatureCommissionsGlobales)
                {
                    
                    paiementCommission.FactureCommissionGlobales.Add(facture);

                    facture.Payee = true;
                    foreach (var fact in facture.FactureCommissions)
                    {
                        fact.Payee = true;
                    }
                }
                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Add(ApporteurAffaire entity) { DB.ApporteurAffaires.Add(entity); DB.SaveChanges(); }
        public void Delete(ApporteurAffaire entity)
        {
            try
            {
                if (entity.Commissions.Count > 0)
                {
                    throw new Exception("Désolé vous ne pouvez supprimer cet apporteur car des encaissements lui sont rattachés.");
                }
                entity.Actif = false;
                DB.SaveChanges();
            }
            catch (Exception)
	        {
                throw;
            }
        }
        public void Update(ApporteurAffaire entity) { }

        public ApporteurAffaire FindById(int Id)
        {
            var result = (from r in DB.ApporteurAffaires where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        internal IEnumerable<FactureCommission> GetFacturesCommissions(int idContrat)
        {

            try
            {
                return DB.FactureCommissions.Where(f => f.EncaissementGlobal.ContratId== idContrat);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //internal IEnumerable<FactureCommission> GetFacturesCommissions(int idContrat)
        //{

        //    try
        //    {
        //        return DB.FactureCommissions.Where(f => f.EncaissementGlobal.ContratId == idContrat);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        public void EnregistrerLesReglementsCommission(List<FactureCommission>  lesFactures)
        {
            try
            {
                foreach (FactureCommission item in lesFactures)
                {
                    var reglementCommission = new PaiementCommission
                    {
                        Date = DateTime.Now.Date,
                        FactureCommissionId = item.ID,
                        ModePaiement = ModePaiement.Chèque,
                        MontantPaye = item.MontantAPayer,
                        ReferencePaiement = ""
                    };
                    DB.PaiementCommissions.Add(reglementCommission);
                    item.Payee = true;
                    DB.SaveChanges();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void Save()
        {
            try
            {
                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
   
}
