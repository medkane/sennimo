using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjSenImmoWinform.Models;

namespace prjSenImmoWinform.DAL
{
    public class IlotRepository : IRepository<Ilot>
    {

        SenImmoDataContext db;
        //ContratRepository contratRep;

        public IlotRepository()
        {
            db = new SenImmoDataContext();
            //contratRep = new ContratRepository();

        }
        public IEnumerable<Ilot> List
        {
            get
            {
                return db.Ilots;
            }

        }

        public void Add(Ilot entity)
        {
            db.Ilots.Add(entity);
            db.SaveChanges();
        }

        public void Delete(Ilot entity)
        {
            db.Ilots.Remove(entity);
            db.SaveChanges();
        }

        public void Update(Ilot entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public Ilot FindById(int Id)
        {
            var result = (from r in db.Ilots where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public Ilot FindIlotByName(string name)
        {
            var result = (from r in db.Ilots where r.NomIlot.StartsWith(name) select r).FirstOrDefault();
            return result;
        }

        public Ilot FindIlotByName(string name, TypeConstruction tc)
        {
            var result = (from r in db.Ilots where r.NomIlot.StartsWith(name) && r.TypeConstruction==tc select r).FirstOrDefault();
            return result;
        }

        public Ilot FindIlotByName(string name, Projet prj,TypeConstruction tc)
        {
            var result = (from r in db.Ilots where r.NomIlot.StartsWith(name) && r.TypeConstruction == tc
                          && r.ProjetId == prj.Id select r).FirstOrDefault();
            return result;
        }

        //===============================================
        public Lot FindLotById(int Id)
        {
            var result = (from r in db.Lots where r.ID == Id select r).FirstOrDefault();
            return result;
        }

        public Lot FindLotByNumero(string numeroLot)
        {
            var result = (from r in db.Lots where r.NumeroLot == numeroLot select r).FirstOrDefault();
            return result;
        }

        public IEnumerable<Lot> GetLots(int IdiLot)
        {
            db.Dispose();
            db = new SenImmoDataContext();
            var result = db.Lots.Where(l =>l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive );

            if (IdiLot != 0)
                result = result.Where(l => l.IlotID == IdiLot);
            
            return result;
        }

        public IEnumerable<Lot> GetLotsParTypeConstruction(int IdiLot,TypeConstruction tc)
        {
            db.Dispose();
            db = new SenImmoDataContext();
            var result = db.Lots.Where(l => l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive && l.TypeVilla.TypeConstruction == tc);

            if (IdiLot != 0)
                result = result.Where(l => l.IlotID == IdiLot);

            return result;
        }

        public IEnumerable<Lot> GetLotsParTypeConstruction(Projet prj,int IdiLot, TypeConstruction tc)
        {
            db.Dispose();
            db = new SenImmoDataContext();
            var result = db.Lots.Where(l =>l.Ilot.ProjetId== prj.Id && l.LotVirtuel == false && l.StatutLot != StatutLot.Desactive && l.TypeVilla.TypeConstruction == tc);

            if (IdiLot != 0)
                result = result.Where(l => l.IlotID == IdiLot);

            return result;
        }

        public IEnumerable<Lot> GetAllLots()
        {
            var result = (from lot in db.Lots where lot.LotVirtuel == false && lot.StatutLot != StatutLot.Desactive select lot );
            return result;
        }

        public Lot GetLotVirtuel(TypeVilla tv, PositionLot pl)
        {
            var result = (from lot in db.Lots where lot.TypeVilla.TypeVillaId == tv.TypeVillaId && lot.PositionLot==pl && lot.LotVirtuel == true select lot).FirstOrDefault();
            return result;
        }
        //===============================================
        public IEnumerable<TypeVilla> GetTypeVillas()
        {
            try
            {
                return db.TypeVillas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TypeImmeuble> GetTypeImmeubles()
        {
            try
            {
                return db.TypeImmeubles;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TypeEtatAvancement> GetNiveauxAvancements(int ProjetId, TypeConstruction tc)
        {
            try
            {
                return db.TypeEtatAvancements.Where(na=>na.NiveauTechnique==true && na.ProjetId== ProjetId && na.TypeConstruction==tc);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TypeEtatAvancement> GetNiveauxAvancements(int ProjetId, TypeConstruction tc, TypeContrat tco)
        {
            try
            {
                return db.TypeEtatAvancements.Where(na => na.NiveauTechnique == true && na.ProjetId == ProjetId && na.TypeConstruction == tc && na.TypeContratId==tco.ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TypeEtatAvancement> GetNiveauxAvancementsAppelsDeFond()
        {
            try
            {
                return db.TypeEtatAvancements.Where(na => na.NiveauTechnique == true && na.AppelFonds==true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TypeEtatAvancement GetNiveauAvancement(int idOrdre)
        {
            try
            {
                return db.TypeEtatAvancements.Where(na => na.ordre >= idOrdre).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public void AddLot(Lot entity)
        //{
        //    db.Lots.Add(entity);
           
        //    db.SaveChanges(); 
        //    //Ajouter tous les etats d'avancement à la création du lot
        //    AjouterLesEtatAvancement(entity.ID);
           
        //}
        public void AddLot(Lot entity,int projetId)
        {
            db.Lots.Add(entity);

            db.SaveChanges();
            //Ajouter tous les etats d'avancement à la création du lot
            AjouterLesEtatAvancement(entity.ID, projetId);

        }

        public void DeleteLot(int lotId)
        {
            try
            {
                var lot= db.Lots.Find(lotId);
                db.Lots.Remove(lot);
                

                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AjouterLesEtatAvancement(int lotID, int projetId)
        {
            try
            {
                #region GENERATION LES ETATS D'AVANCEMENT SANS LES FACTURES APPELS DE FOND CORRESPONDANT AU CAS ECHEANT
                var lot = db.Lots.Find(lotID);
                var typeVilla = db.TypeVillas.Find(lot.TypeVillaID);
                foreach (var niveauAvancement in db.TypeEtatAvancements.Where(ta => ta.ProjetId == projetId && ta.TypeConstruction == typeVilla.TypeConstruction).ToList())
                {
                    //Générer d'office tous les etats d'avancement; sans préciser la date.
                    EtatAvancement etatAvancement;
                    etatAvancement = db.EtatAvancements.Where(ea => ea.LotId == lotID && ea.TypeEtatAvancementID == niveauAvancement.ID).SingleOrDefault();

                    if (etatAvancement == null)
                    {
                        etatAvancement = new EtatAvancement()
                        {
                            LotId = lotID,
                            TypeEtatAvancementID = niveauAvancement.ID,
                            Actif = false,

                        };
                        db.EtatAvancements.Add(etatAvancement);

                        db.SaveChanges();
                    }
                }
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public void AjouterLesEtatAvancement(int lotID)
        //{
        //    try
        //    {
        //        #region GENERATION LES ETATS D'AVANCEMENT SANS LES FACTURES APPELS DE FOND CORRESPONDANT AU CAS ECHEANT

        //        foreach (var niveauAvancement in db.TypeEtatAvancements.ToList())
        //        {
        //            //Générer d'office tous les etats d'avancement; sans préciser la date.
        //            EtatAvancement etatAvancement;
        //            etatAvancement = db.EtatAvancements.Where(ea => ea.LotId == lotID && ea.TypeEtatAvancementID == niveauAvancement.ID).SingleOrDefault();

        //            if (etatAvancement == null)
        //            {
        //                etatAvancement = new EtatAvancement()
        //                {
        //                    LotId = lotID,
        //                    TypeEtatAvancementID = niveauAvancement.ID,
        //                    Actif = false,

        //                };
        //                db.EtatAvancements.Add(etatAvancement);

        //                db.SaveChanges();
        //            }
        //        }
        //        #endregion
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public void ActiverEtatAvancement(int lotID, TypeEtatAvancement niveauAvancement, DateTime dateAvancement, string commentaire)
        {
            try
            {
                //Générer d'office tous les etats d'avancement; sans préciser la date.
                EtatAvancement etatAvancement;
                etatAvancement = db.EtatAvancements.Where(ea => ea.LotId == lotID && ea.TypeEtatAvancementID == niveauAvancement.ID).SingleOrDefault();

                if (etatAvancement == null)
                {
                    etatAvancement = new EtatAvancement()
                    {
                        LotId = lotID,
                        TypeEtatAvancementID = niveauAvancement.ID,
                        Actif = false,

                    };
                    db.EtatAvancements.Add(etatAvancement);

                    db.SaveChanges();
                }

                etatAvancement.Actif = true;
                etatAvancement.DateSaisieAvancement = dateAvancement;
                etatAvancement.Commentaire = commentaire;
                db.SaveChanges();
            }

            catch (Exception)
            {

                throw;
            }
        }


        public void AddEtatAvancement(Lot lot, TypeEtatAvancement niveauAvancement, DateTime dateAvancement,string commentaire)
        {
            try
            {
                //Vérifier si l'Etat d'avancement avait déjà été généré à vide lors de la création d'un contrat sur ce lot
                // auquel cas il faudra juste mettre à jour ce dernier, sinon le créer 
                EtatAvancement etatAvancement;
                etatAvancement = db.EtatAvancements.Where(ea => ea.LotId == lot.ID && ea.TypeEtatAvancementID == niveauAvancement.ID).SingleOrDefault();

                if (etatAvancement == null)
                {
                    etatAvancement = new EtatAvancement()
                    {
                        DateSaisieAvancement = dateAvancement,
                        Actif = true,
                        Commentaire=commentaire,
                        TypeEtatAvancementID = niveauAvancement.ID,
                        TypeEtatAvancement = niveauAvancement,
                        LotId = lot.ID,
                        Lot = lot
                    };
                    lot.EtatsAvancements.Add(etatAvancement);
                }
                else
                {
                    etatAvancement.DateSaisieAvancement = dateAvancement;
                    etatAvancement.Actif = true;
                    etatAvancement.Commentaire = commentaire;
                }
                db.SaveChanges();
                //GESTION DES APPELS DE FONDS
                // Au cas où le lot est réservé ou en cours de réservation et 
                // l'etat est générateur d'appels de fond
                if ( lot.StatutLot == StatutLot.Reserve
                        && niveauAvancement.AppelFonds == true)
                {
                    var contrat = GetContratsLot(lot);
                    if (contrat != null)
                    {

                        var montantAppelDefond = contrat.PrixFinal * niveauAvancement.TauxDecaissement / 100;
                        //La facture étant déjà générée lors de l'enregistrement du contrat, on met à jours les différents éléments restant
                        var factureExistant = GetFactureEtatAvancement(etatAvancement.ID);
                        if(factureExistant!=null)
                        { 
                            factureExistant.DateEcheanceFacture = dateAvancement;
                            factureExistant.Active = true;
                            factureExistant.Echue = true;
                        }

                        //var agentRecouvrement = db.Agents.Where(a => a.Role.CodeRole == "RCV" && a.RecouvrementResa==true).FirstOrDefault();
                        //if (agentRecouvrement != null)
                        //{
                        //    if (agentRecouvrement.Email!=string.Empty)
                        //        Tools.Tools.EmailSend(agentRecouvrement.Email, "", "Appel de fond sur "+lot.NumeroLot,
                        //        @" Bonjour " + "\n\n L'appel de fond " + niveauAvancement.LibelleCommercial + " a été généré pour le client " + contrat.Client.NomComplet+ " sur le lot " + lot.NumeroLot + "\n\n Cordialement");
                        //}
                    }

                    //Facture facture = new Facture
                    //{
                    //    TypeFacture = TypeFacture.AppelDeFond,
                    //    ContratId = contrat.Id,
                    //    ClientId = contrat.ClientID,
                    //    Date = dateAvancement,
                    //    EtatAvancementId = etatAvancement.ID,
                    //    FacturePayee = false,
                    //    NumeroFacture = "",
                    //    Montant = montantAppelDefond,
                    //    Motif = niveauAvancement.Description,
                    //    Active = true

                    //};
                    //contrat.Factures.Add(facture);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Contrat GetContratsLot(Lot lot)
        {
            try
            {
                return db.Contrats.Where(ct => ct.LotId == lot.ID && ct.Statut== StatutContrat.Actif).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Facture GetFactureEtatAvancement(int etatAvancementId)
        {
            try
            {
                return db.Factures.Where(fc => fc.EtatAvancementId == etatAvancementId).SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void DeteteEtatAvancement(int etatAvancementId)
        {
            try
            {
                var ea = db.EtatAvancements.Find(etatAvancementId);
                if (ea != null)
                {
                    //Vérifier qu'il n'y a pas d'appel de fond lié à cet Etat d'avancement
                    if (ea.TypeEtatAvancement.AppelFonds == true)
                    { 
                        var appelDeFond = db.Factures.FirstOrDefault(af => af.EtatAvancementId == etatAvancementId);
                        if(appelDeFond!=null)
                        {
                            db.Factures.Remove(appelDeFond);
                        }
                    }
                    db.EtatAvancements.Remove(ea);
                    db.SaveChanges();
                }
                else
                    throw new Exception("Etat d'avancement introuvable");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
