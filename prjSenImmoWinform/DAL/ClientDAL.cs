using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjSenImmoWinform.Models;
using System.Transactions;

namespace prjSenImmoWinform.DAL
{
    class ClientRepository
    {
        SenImmoDataContext db;

        public ClientRepository()
        {
            db = new SenImmoDataContext();
        }


        public IEnumerable<Client> GetAllClients(bool bResilie)
        {
            try
            {
                if (bResilie)
                    return db.Clients.Where(c => c.Actif == true && (c.Type == TypeClient.Résilié));
                else
                    return db.Clients.Where(c => c.Actif == true && (c.Type == TypeClient.Client || c.Type == TypeClient.ClientEnCours));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Client> GetAllClientsEtProspects()
        {
            try
            {
                return db.Clients.Where(c => c.Actif == true && c.Type != TypeClient.Résilié && !(c.Prenom.Trim()==string.Empty && c.Nom.Trim()==string.Empty));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TypeStatutProspect> GetTypeStatutProspects()
        {
            try
            {
                return db.TypeStatutProspects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Client> GetAllClientsResilie()
        {
            try
            {
                var contratsResilies = db.Contrats.Where(c => c.Statut == StatutContrat.Résilié);


                return contratsResilies.Select(c => c.Client);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Cooperative> GetAllCooperatives()
        {
            try
            {
                var cooperatives = db.Cooperatives;


                return cooperatives;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Country> GetAllPays()
        {
            try
            {
                var pays = db.Countries;


                return pays;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TypeOrigine> GetAllTypeOrigines()
        {
            try
            {
                return db.TypeOrigines.Where(to => to.ClassOrigine==ClassOrigine.Desk && to.BActif==true).OrderByDescending(to =>to.Clients.Count) ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TypeOrigine> GetRealAllTypeOrigines()
        {
            try
            {
                return db.TypeOrigines.Where(to => to.ClassOrigine == ClassOrigine.Desk ).OrderByDescending(to => to.Clients.Count);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<NoteProspect> GetNotesProspect(int prospectID)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                return db.Clients.Find(prospectID).Notes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<StatutProspect> GetStatutsProspect(int prospectID)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                return db.Clients.Find(prospectID).StatutProspects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddStatutsProspect(StatutProspect statut)
        {
            try
            {
                db.StatutProspects.Add(statut);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<NoteProspect> GetNotesActivite(int activiteId)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                return db.NotesProspects.Where(note =>note.ActivitecommercialId==activiteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Client> GetAllProspects()
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                return db.Clients.Where(c => c.Actif == true && (c.Type == TypeClient.ProspectSansOption || c.Type == TypeClient.ProspectAvecOptionResa || c.Type == TypeClient.ProspectAvecOptionDepot));
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Client GetClient(int idClient)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                return db.Clients.Where(c => c.ID == idClient && c.Actif == true).SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Client> GetClients(string nomClient, string prenomClient, string telephoneClient, string emailClient, bool bResilie, string numeroLot)
        {
            try
            {
                List<Client> lesClientTrouves;
                if (bResilie)
                {
                    lesClientTrouves = db.Clients.Where(c => c.Actif == true && (c.Type == TypeClient.Résilié)).ToList();

                }
                else
                    lesClientTrouves = db.Clients.Where(c => c.Actif == true && (c.Type == TypeClient.Client || c.Type == TypeClient.ClientEnCours)).ToList();

                if (nomClient != string.Empty)
                    lesClientTrouves = lesClientTrouves.Where(c => c.Nom != null && c.Nom.ToLower().Contains(nomClient.ToLower())).ToList();
                if (prenomClient != string.Empty)
                    lesClientTrouves = lesClientTrouves.Where(c => c.Prenom != null && c.Prenom.ToLower().Contains(prenomClient.ToLower())).ToList();

                if (telephoneClient != string.Empty)
                    lesClientTrouves = lesClientTrouves.Where(c => c.Mobile1 != null && c.Mobile1.StartsWith(telephoneClient)).ToList();

                if (emailClient != string.Empty)
                    lesClientTrouves = lesClientTrouves.Where(c => c.Email != null && c.Email.ToLower().StartsWith(emailClient.ToLower())).ToList();

                if (numeroLot != string.Empty)
                    lesClientTrouves = lesClientTrouves.Where(c => c.Contrats.Any(ct =>ct.Lot.NumeroLot==numeroLot && ct.Statut== StatutContrat.Actif)).ToList();
                return lesClientTrouves;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal TypeOrigine GetTypeOrigine(int idTypeOrigine)
        {
            try
            {
                return db.TypeOrigines.Where(to => to.TypeOrigineId == idTypeOrigine).SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Client> GetClientsProspects(string nomClient, string prenomClient)
        {
            try
            {
                var lesClientTrouves = db.Clients.Where(c => c.Actif == true && (c.Type == TypeClient.Client || c.Type == TypeClient.ClientEnCours));

                if (nomClient != string.Empty)
                    lesClientTrouves = lesClientTrouves.Where(c => c.Nom.StartsWith(nomClient));
                if (prenomClient != string.Empty)
                    lesClientTrouves = lesClientTrouves.Where(c => c.Prenom.StartsWith(prenomClient));

                return lesClientTrouves;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<Client> GetProspects(string nomClient, string prenomClient,string telephoneClient, string emailClient)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                var lesClientTrouves = db.Clients.Where(c => c.Actif == true && (c.Type == TypeClient.ProspectSansOption || c.Type == TypeClient.ProspectAvecOptionResa || c.Type == TypeClient.ProspectAvecOptionDepot));

                if (nomClient != string.Empty)
                    lesClientTrouves = lesClientTrouves.Where(c => c.Nom != null && c.Nom.ToLower().Contains(nomClient.ToLower()));
                if (prenomClient != string.Empty)
                    lesClientTrouves = lesClientTrouves.Where(c => c.Prenom != null && c.Prenom.ToLower().Contains(prenomClient));
                if (telephoneClient != string.Empty)
                    lesClientTrouves = lesClientTrouves.Where(c => c.Mobile1 != null && c.Mobile1.StartsWith(telephoneClient));
                if (emailClient != string.Empty)
                    lesClientTrouves = lesClientTrouves.Where(c => c.Email != null && c.Email.ToLower().StartsWith(emailClient.ToLower()));

                return lesClientTrouves;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddClient(Client client)
        {
            try
            {
                db.Clients.Add(client);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Cooperative GetCooperative(int cooperativeId)
        {
            try
            {
               return db.Cooperatives.Find(cooperativeId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddProspetOption(Option option)
        {
            try
            {
                db.Options.Add(option);
                var leLot = db.Lots.Where(lot => lot.ID == option.LotId).SingleOrDefault();
                leLot.StatutLot = StatutLot.Option;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void AddProspetActiviteCommerciale(ActiviteCommerciale activite)
        {
            try
            {
                db.ActiviteCommerciales.Add(activite);

                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }
        //public void SaveChanges( )
        //{
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        public void AddTypeOrigine(TypeOrigine typeOrigine)
        {
            try
            {
                db.TypeOrigines.Add(typeOrigine);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<Option> GetOptionsProspect(Client client)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                var lesOptionsTrouves = db.Options.Where(o => o.ClientID == client.ID && o.Active==true) ;

                return lesOptionsTrouves;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Option> GetAllOptionsProspect()
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                var lesOptionsTrouves = db.Options.Where(o => o.Active == true && o.ContratGenere==false );

                return lesOptionsTrouves;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void LeverOption(int OptionId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var  option= db.Options.Find(OptionId);
                    option.Active = false;
                    if(option.Lot!=null)
                        option.Lot.StatutLot = StatutLot.Libre;
                    option.Client.Type = TypeClient.ProspectSansOption;

                    db.SaveChanges();
                    scope.Complete();
                   
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        public void UpdateClient(Client newClient)
        {

        }

        public void SaveChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteClient(int IdClient)
        {
            try
            {
                var client = db.Clients.Find(IdClient);
                foreach (var option in client.Options)
                {
                    if(option.Active==true)
                    {
                        throw new Exception("Ce client a une option active, veuillez d'abord lever l'option avant suppression");
                    }
                }
                if(client.Options.Count() > 0)
                {
                    db.Options.RemoveRange(client.Options);
                }
                if (client.EncaissementProspects.Count() > 0)
                {
                    throw new Exception("Ce prospect a des encaissement dans son compte, veuillez d'abord les supprimer.");
                }

                    db.Clients.Remove(client);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteActiviteCommerciale(int acId)
        {
            try
            {
                var acComm = db.ActiviteCommerciales.Find(acId);
                acComm.StatutActiviteCommerciale = StatutActiviteCommerciale.Annulée;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CloturerActiviteCommerciale(int acId)
        {
            try
            {
                var acComm = db.ActiviteCommerciales.Find(acId);
                acComm.StatutActiviteCommerciale = StatutActiviteCommerciale.Exécutée;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EchoirActiviteCommerciale(int acId)
        {
            try
            {
                var acComm = db.ActiviteCommerciales.Find(acId);
                acComm.StatutActiviteCommerciale = StatutActiviteCommerciale.EchueNonExecutée;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ActiviteCommerciale> GetActivitesCommercialesProspect(Client client)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                var lesActivitesCommercialesTrouves = db.ActiviteCommerciales.Where(a => a.ClientID == client.ID && a.StatutActiviteCommerciale!= StatutActiviteCommerciale.Annulée);

                return lesActivitesCommercialesTrouves;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public  ActiviteCommerciale GetActivitesCommercialesById(int acId)
        {
            try
            {
                var lActivitesCommerciale = db.ActiviteCommerciales.Find(acId);

                return lActivitesCommerciale;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ActiviteCommerciale> GetActivitesCommerciales(int commercialId, DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                var commercial = db.Agents.Find(commercialId);
                if(commercial.Role.CodeRole=="CMC")
                if(commercial.IsChefEquipe)
                    return  db.ActiviteCommerciales.Where(a => a.StatutActiviteCommerciale!= StatutActiviteCommerciale.Annulée &&  a.Commercial!=null && a.Commercial.ChefEquipeId == commercialId && a.DateActivite >= dateDebut && a.DateActivite <= dateFin);
                else

                    return  db.ActiviteCommerciales.Where(a => a.StatutActiviteCommerciale != StatutActiviteCommerciale.Annulée && a.CommercialID== commercialId &&  a.DateActivite>= dateDebut &&  a.DateActivite <= dateFin);
                else
                    return db.ActiviteCommerciales.Where(a => a.StatutActiviteCommerciale != StatutActiviteCommerciale.Annulée && a.DateActivite >= dateDebut && a.DateActivite <= dateFin && a.Commercial.ProjetId == Tools.Tools.AgentEnCours.ProjetId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ActiviteCommerciale> GetActivitesCommerciales( DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                return db.ActiviteCommerciales.Where(a => a.DateActivite >= dateDebut && a.DateActivite <= dateFin);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ActiviteCommerciale> GetActivitesCommercialesEchues(DateTime dateReference)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                return db.ActiviteCommerciales.Where(a => a.DateActivite <= dateReference);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<EncaissementProspect> GetEncaissementsProspect(int prospectId)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                var lesEncaissementTrouves = db.EncaissementProspects.Where(e => e.ProspectId == prospectId);

                return lesEncaissementTrouves;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EncaissementGlobal> GetEncaissementsGlobauxClient(int contratId)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();
                var lesEncaissementGlobauxTrouves = db.EncaissementGlobals.Where(e => e.ContratId == contratId).OrderBy(e => e.DateEncaissement);

                return lesEncaissementGlobauxTrouves;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public EncaissementGlobal GetEncaissementsGlobal(int encaissementGlobalId)
        {
            try
            {
                return db.EncaissementGlobals.Find(encaissementGlobalId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EncaissementProspect GetEncaissementProspect(int encaissementProspectId)
        {
            try
            {
                return db.EncaissementProspects.Find(encaissementProspectId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Encaissement GetEncaissement(int encaissementId)
        {
            try
            {

                return db.Encaissements.Find(encaissementId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Facture> GetEcheancesClient(int contratId, string etatFacture)
        {
            try
            {
               
                var contrat = db.Contrats.Find(contratId);
                if (contrat == null) throw new Exception("Contrat introuvable");
                var lesFacturesTrouvees = db.Factures.Where(f => f.ContratId == contratId);//&& f.Active==true
                //if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && etatFacture=="")
                //    lesFacturesTrouvees = lesFacturesTrouvees.Where(f => f.Active == true);

                if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && etatFacture == "soldees")
                    lesFacturesTrouvees = lesFacturesTrouvees.Where(f => f.FacturePayee == true);
                if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && etatFacture == "nonSoldees")
                    lesFacturesTrouvees = lesFacturesTrouvees.Where(f => f.FacturePayee == false);
                if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && etatFacture == "EchuesNonSoldees")
                    lesFacturesTrouvees = lesFacturesTrouvees.Where(f => f.Echue == true && f.FacturePayee == false);
                return lesFacturesTrouvees.OrderByDescending(f => f.TypeFacture);

            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<Encaissement> GetEncaissementsClient(int encaissementGlobalId)
        {
            try
            {
                db.Dispose();
                db = new SenImmoDataContext();

                var lesEncaissementsTrouves = db.Encaissements.Where(e => e.EncaissementGlobalId == encaissementGlobalId);

                return lesEncaissementsTrouves;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Facture> GetFacturesClient(int contratId, string etatFacture)
        {
            try
            {
                var contrat = db.Contrats.Find(contratId);
                if (contrat == null) throw new Exception("Contrat introuvable");
                var lesFacturesTrouvees = db.Factures.Where(f => f.ContratId == contratId);//&& f.Active==true
                if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt)
                    lesFacturesTrouvees = lesFacturesTrouvees.Where(f => f.Active == true);

                if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && etatFacture == "soldees")
                    lesFacturesTrouvees = lesFacturesTrouvees.Where(f => f.FacturePayee == true);
                if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && etatFacture == "NonSoldees")
                    lesFacturesTrouvees = lesFacturesTrouvees.Where(f => f.FacturePayee == false);
                return lesFacturesTrouvees;

            }
            catch (Exception)
            {

                throw;
            }
        }

        internal TypeOrigine GetTypeOriginePerso()
        {
            return db.TypeOrigines.Where(to => to.ClassOrigine == ClassOrigine.Perso).FirstOrDefault();
        }

       internal void AjouterNote(int prospectID, string Note)
        {
            db.Clients.Find(prospectID).Notes.Add(new NoteProspect()
            {
                DateDebutTypeOrigine = DateTime.Now.Date,
                Comentaire = Note

            });
            db.SaveChanges();
        }
        internal void AjouterNote(int prospectID,int activiteId, string Note)
        {
            db.Clients.Find(prospectID).Notes.Add(new NoteProspect()
            {
                ActivitecommercialId=activiteId,
                DateDebutTypeOrigine = DateTime.Now.Date,
                Comentaire = Note

            });
            db.SaveChanges();
        }
        internal void ModifierNote(int idNote, string texte)
        {
            try
            {
                var note = db.NotesProspects.Find(idNote);
                note.Comentaire = texte;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal void SupprimerNote(int idNote)
        {
            try
            {
                db.NotesProspects.Remove(db.NotesProspects.Find(idNote));
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void DeleteStatutsProspect(StatutProspect stp)
        {
            db.StatutProspects.Remove(db.StatutProspects.Find(stp.ID));
            db.SaveChanges();
        }
    }
}
