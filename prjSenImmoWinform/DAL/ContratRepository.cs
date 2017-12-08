using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjSenImmoWinform.Models;
using System.Globalization;
using System.Data.Entity;
using System.Transactions;

namespace prjSenImmoWinform.DAL
{
    public class ContratRepository : IRepository<Contrat>
    {

        SenImmoDataContext DB;
        private bool derniereEchance;
        private IOrderedEnumerable<Facture> lesFactures;

        public IlotRepository ilotRep { get; set; }
        public ContratRepository()
        {
            DB = new SenImmoDataContext();
            ilotRep = new IlotRepository();
        }
        public IEnumerable<Contrat> List
        {
            get
            {
                return DB.Contrats;
            }
        }

        public void Add(Contrat entity)
        {
            DB.Contrats.Add(entity);
            DB.SaveChanges();
        }
        public void Delete(Contrat entity) { }
        public void Update(Contrat entity) { }

        public Contrat FindById(int Id)
        {
            var result = (from r in DB.Contrats where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable<TypeContrat> GetTypeContrats()
        {
            try
            {
                return DB.TypeContrats.Where(c => c.Actif ==true).OrderBy(c => c.CategorieContrat).ThenBy(c => c.SeuilSouscription);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<Agent> GetCommerciaux(Agent agentConnecte)
        {

            var RoleCOmmerciale = DB.Roles.Where(c => c.CodeRole == "CMC").SingleOrDefault();
            if (agentConnecte.RoleId == RoleCOmmerciale.ID)
            {
                if (!agentConnecte.IsChefEquipe)
                    return null;
                else
                    return DB.Agents.Where(c => c.ChefEquipeId == agentConnecte.Id);
            }
            else
            {
                return DB.Agents;
            }


        }

        public IEnumerable<Projet> GetProjets()
        {
            try
            {
                return DB.Projets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Ilot> GetIlots(int idProjet)
        {
            try
            {
                return DB.Ilots.Where(ilo =>ilo.ProjetId== idProjet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Projet GetProjet(int projetId)
        {
            try
            {
                return DB.Projets.Find(projetId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public TypeContrat GetTypeContrat(CategorieContrat cc, int pourcentageDebut)
        {
            try
            {
                return DB.TypeContrats.Where(tc => tc.CategorieContrat == cc && tc.SeuilSouscription == pourcentageDebut).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public TypeContrat GetTypeContratBy(int id)
        //{
        //    try
        //    {
        //        return DB.TypeContrats.Where(tc => tc.CategorieContrat == cc && tc.SeuilSouscription == pourcentageDebut).FirstOrDefault();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public Contrat GetContratById(int contratId)
        {
            try
            {
                DB.Dispose();
                DB = new SenImmoDataContext();
                return DB.Contrats.Find(contratId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Contrat GetContratByNumero(string numeroContrat)
        {
            try
            {
                return DB.Contrats.Where(c => c.NumeroContrat == numeroContrat).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Contrat> GetContratsClient(Client client)
        {
            try
            {
                return DB.Contrats.Where(tc => tc.ClientID == client.ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Contrat> GetContratsCommercial(int commercialId)
        {
            try
            {
                var commercial = DB.Agents.Find(commercialId);
                if (commercial.Role.CodeRole == "CMC")
                {
                    if (!commercial.IsChefEquipe)
                        return DB.Contrats.Where(tc => tc.CommercialID == commercialId && tc.ProjetId==commercial.ProjetId && tc.Statut == StatutContrat.Actif).OrderBy((tc => tc.TypeContratID));
                    else
                        return DB.Contrats.Where(tc => tc.Commercial.ChefEquipeId == commercialId && tc.ProjetId == commercial.ProjetId && tc.Statut == StatutContrat.Actif).OrderBy((tc => tc.TypeContratID));
                }
                else if (commercial.Role.CodeRole == "DC")
                    return DB.Contrats.Where(tc => tc.Statut == StatutContrat.Actif && tc.ProjetId == commercial.ProjetId).OrderBy((tc => tc.TypeContratID));
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Contrat> GetContratsChefEquipe(int chefEquipeId)
        {
            try
            {
                return DB.Contrats.Where(tc => tc.Commercial.ChefEquipeId == chefEquipeId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Contrat> GetContratsDC(int chefEquipeId)
        {
            try
            {
                return DB.Contrats;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TypeVilla> GetTypeVillas()
        {
            try
            {
                return DB.TypeVillas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TypeEtatAvancement> GetNiveauxAvancements()
        {
            try
            {
                return DB.TypeEtatAvancements;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ApporteurAffaire> GetApporteurAffaires()
        {
            try
            {
                return DB.ApporteurAffaires;
            }
            catch (Exception)
            {

                throw;
            }
        }



        //private void SimulerEcheancesDepot(TypeVilla typeVilla, PositionLot position, decimal tauxRemise, ApporteurAffaire apporteur,
        //    TypeEcheancier typeEcheance, DateTime dateDebut, DateTime datefin,
        //    decimal montantPremierVersement,
        //    out decimal montantEcheance, out int nbEcheance, out decimal montantDerniereEcheance )
        //{
        //    Lot lotVirtuel = ilotRep.GetLotVirtuel(typeVilla, position);

        //    //dMontantPremierVersement = decimal.Parse(txtMontantPremierVersement.Text);
        //    //txtPrixVenteResume.Text = dPrixDeVente.ToString("### ### ###");
        //    //txtMontantPremierVersementResume.Text = dMontantPremierVersement.ToString("### ### ###");
        //    //var dDepotDeGarantie = dPrixDeVente * leTypeContratEnCours.SeuilSouscription / 100;
        //    //txtDepotGarantieDepot.Text = dDepotDeGarantie.ToString("### ### ###");
        //    if(tauxRemise > 0)
        //    {
        //        dMontantRemise = lotVirtuel.PrixRevise * (decimal)tauxRemise / 100;
        //    }
        //    var dMontantApresDepot = dPrixDeVente - dDepotDeGarantie;

        //    switch (typeEcheance)
        //    {
        //        case TypeEcheancier.Mensuel:
        //            nbEcheance = (datefin.Year - dateDebut.Year) * 12 + datefin.Month - dateDebut.Month;
        //            break;
        //        //case TypeEcheancier.Bimensuel:
        //        //    nombreDeTraite = ((datefin.Year - dateDebut.Year) * 12 + datefin.Month - dateDebut.Month) / 2;
        //        //    break;
        //        case TypeEcheancier.Trimestriel:
        //            nbEcheance = ((datefin.Year - dateDebut.Year) * 12 + datefin.Month - dateDebut.Month) / 3;
        //            break;
        //        case TypeEcheancier.Semestriel:
        //            nbEcheance = ((datefin.Year - dateDebut.Year) * 12 + datefin.Month - dateDebut.Month) / 6;
        //            break;
        //        case TypeEcheancier.Annuel:
        //            nbEcheance = ((datefin.Year - dateDebut.Year) * 12 + datefin.Month - dateDebut.Month) / 12;
        //            break;
        //        default:
        //            break;
        //    }

        //    //model.DateFinPrevue = derniereEcheance;
        //    montantEcheance = (int)dMontantApresDepot / nbEcheance;
        //    txtMontantEcheance.Text = MontantEcheancePrevu.ToString("### ### ###");
        //    var montantDerniereEcheance = (int)(dMontantApresDepot - MontantEcheancePrevu * nombreDeTraite);
        //    if (montantDerniereTraite > 0)
        //        nombreDeTraite++;


        //}



        public int AjouterContratDepot(int clientId, int commercialId, int apporteurAffaireId,
            TypeVilla typeVilla, PositionLot position, decimal prixDeVente, decimal tauxRemiseAccordee, decimal MontantRemiseAccordee,
            TypeContrat typeContrat, TypeEcheancier typeEcheancier, DateTime datePremiereEcheance, DateTime dateDerniereEcheance,
            decimal montantPremierVersement, DateTime dateContrat, DateTime DatePremierVersement, String CommentaireGlobal,
            string ReferenceGlobal, ModePaiement modePaiement, DateTime dateLivraisonPrevue)
        {


            Contrat contrat = null;

            try
            {
                //var lot = ctx.Lots.Find(lotId);
                var client = DB.Clients.Find(clientId);
                ApporteurAffaire apporteur = null;
                //Rechercher le lot virtuel corresponsdant au type de villa et à la position choisie
                Lot lotVirtuel = ilotRep.GetLotVirtuel(typeVilla, position);
                if (apporteurAffaireId != 0)
                {
                    apporteur = new ApporteurAffaireRepository().FindById(apporteurAffaireId);
                }
                decimal prixDevente = lotVirtuel.PrixRevise;


                CalculerRemise(ref prixDevente, ref tauxRemiseAccordee, ref MontantRemiseAccordee);

                //Vérifier que le montant versé est suffisant pour déclencher le contrat
                var montantDepotDeGarantie = prixDevente * typeContrat.SeuilSouscription / 100;

                var montantAVentiller = montantPremierVersement;// - montantDepotDeGarantie;
                //if (montantAVentiller < 0)
                //{
                //    throw new Exception("Désolé ce montant est inférieur au dépôt de garantie, veuiller l'enregistrer dans le compte du prospect comme 'Accompte sur Dépôt de garantie'");
                //}

                var dernierNumeroContratDepot = DB.Parametres.Find(1);
                dernierNumeroContratDepot.valeurInt++;
                // Enregistrer le contrat
                contrat = new Contrat
                {
                    NumeroContrat = typeVilla.CodeType + "-" + dernierNumeroContratDepot.valeurInt.ToString().PadLeft(5, '0'),
                    ClientID = client.ID,
                    CommercialID = commercialId,
                    DateCreationSysteme = DateTime.Now.Date,
                    PrixRevise = lotVirtuel.PrixRevise,
                    PrixFinal = prixDeVente,
                    // PrixStandar = leTypeVillaDepotEnCours.PrixStandard,
                    RemiseAccordee = MontantRemiseAccordee,
                    TypeContratID = typeContrat.ID,
                    TypeEcheancier = typeEcheancier,
                    LotId = lotVirtuel.ID,
                    DateLivraisonLot = dateLivraisonPrevue,
                    Souscrit = false,
                    AReserve = false,
                    AttribuerLot = false,
                    LotAttribue = false,
                    Statut = StatutContrat.Actif


                    //DateLivraisonVilla = DateTime.Parse(model.DateLivraisonVilla)
                };


                if (apporteur != null)
                {
                    contrat.ApporteurID = apporteur.Id;
                    contrat.CommissionApporteur = prixDeVente * apporteur.TauxCommission / 100;
                }

                //PROBLEME DE TRANSACTION
                this.Add(contrat);
                // Générer la facture Dépot de Garantie

                Facture factureDepotDeGarantie = new Facture
                {
                    TypeFacture = TypeFacture.DepotMinimum,
                    ClientId = client.ID,
                    Date = DateTime.Now.Date,
                    DateEcheanceFacture = dateContrat,
                    Motif = "Facture Dépot minimun de " + contrat.TypeContrat.SeuilSouscription + "%",
                    FacturePayee = false,
                };
                factureDepotDeGarantie.Montant = montantDepotDeGarantie;

                factureDepotDeGarantie.NumeroFacture = "DG" + contrat.NumeroContrat.ToString().PadLeft(5, '0') + typeEcheancier.ToString().Substring(0, 2).ToUpper()
                                                       + dateContrat.Month.ToString().PadLeft(2, '0') + dateContrat.Year.ToString().Substring(2, 2);
                contrat.Factures.Add(factureDepotDeGarantie);


                var montantRestantADispacher = prixDevente - montantDepotDeGarantie;
                var nbEcheances = 0;
                // Calculer le nombre d'échéances
                nbEcheances = CalculNombreEcheances(typeEcheancier, datePremiereEcheance, dateDerniereEcheance, ref nbEcheances);
                contrat.NbEcheances = nbEcheances;

                //model.DateFinPrevue = derniereEcheance;
                var montantEcheance = (int)montantRestantADispacher / nbEcheances;
                contrat.MontantEcheance = montantEcheance;
                var montantDerniereEcheance = (int)(montantRestantADispacher - montantEcheance * nbEcheances);
                if (montantDerniereEcheance > 0)
                    nbEcheances++;
                //for (int i = 1; i < nbEcheances; i++)
                //{

                //}

                //Générer les échéances
                for (int i = 0; i < nbEcheances; i++)
                {
                    //var echeance = new Echeance()
                    //{

                    //};


                    Facture facture = new Facture
                    {
                        TypeFacture = TypeFacture.Echeance,
                        ClientId = client.ID,
                        Date = DateTime.Now.Date,
                        //DateEcheanceFacture = dateContrat,
                        FacturePayee = false,

                    };

                    if (i == nbEcheances - 2 && montantDerniereEcheance > 0)
                    {
                        facture.Montant = montantDerniereEcheance;
                        derniereEchance = true;
                    }
                    else
                    {
                        facture.Montant = montantEcheance;
                    }

                    switch (typeEcheancier)
                    {
                        case TypeEcheancier.Mensuel:
                            facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 1);
                            facture.Motif = "Echéance Mensuelle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                            break;
                        case TypeEcheancier.Trimestriel:
                            facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 3);
                            facture.Motif = "Echéance trimestrielle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                            break;
                        case TypeEcheancier.Semestriel:
                            facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 6);
                            facture.Motif = "Echéance semestrielle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                            break;
                        case TypeEcheancier.Annuel:
                            facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 12);
                            facture.Motif = "Echéance annuelle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                            break;
                        default:
                            break;
                    }
                    facture.NumeroFacture = GenererNumeroFacturesDepot(contrat.NumeroContrat, typeEcheancier, facture.DateEcheanceFacture.Value);
                    contrat.Factures.Add(facture);
                    if (derniereEchance)
                    {
                        break;
                    }

                }
                //// Enregistrer le premier versement

                var premierVersement = new EncaissementGlobal()
                {
                    NumeroEncaissement = "EN" + contrat.NumeroContrat.ToString().PadLeft(5, '0') + dateContrat.Month.ToString().PadLeft(2, '0') + dateContrat.Year.ToString().Substring(2, 2),
                    DateEncaissement = DatePremierVersement.Date,
                    MontantGlobal = montantPremierVersement,
                    ContratId = contrat.Id,
                    Contrat = contrat,
                    ModePaiement = modePaiement,
                    Commentaire = CommentaireGlobal,
                    ReferencePaiement = ReferenceGlobal

                };

                DB.EncaissementGlobals.Add(premierVersement);
                contrat.MontantPremierVersement = montantPremierVersement;
                contrat.MontantVerse += premierVersement.MontantGlobal;
                DB.SaveChanges();
                //Enregistrer le paiement de commission à l'apporteur d'affaire
                if (apporteur != null)
                {
                    var factureCommission = new FactureCommission
                    {
                        EncaissementGlobalId = premierVersement.ID,
                        Date = DatePremierVersement.Date,
                        MontantAPayer = premierVersement.MontantGlobal * apporteur.TauxCommission / 100,
                        Motif = "Commission sur premier versement",
                        Payee = false

                    };
                    contrat.FactureCommissions.Add(factureCommission);
                }



                DB.SaveChanges();
                VerifierStatutContratDepot(contrat.Id, dateContrat);

                //Enregistrer le paiement du Dépot de Garantie

                decimal montantDepotDeGarantieAEncaisser = 0;

                if (montantAVentiller >= montantDepotDeGarantie)
                {
                    montantDepotDeGarantieAEncaisser = montantDepotDeGarantie;
                }
                else
                {
                    montantDepotDeGarantieAEncaisser = montantAVentiller;
                }


                Encaissement encaissementDepotDeGarantie = new Encaissement
                {
                    Date = DatePremierVersement.Date,
                    ModePaiement = modePaiement,
                    Montant = montantDepotDeGarantieAEncaisser,
                    Commentaire = "Encaissement du dépot minimun de " + contrat.TypeContrat.SeuilSouscription + "%",
                    ReferencePaiement = ReferenceGlobal,

                };
                factureDepotDeGarantie.Encaissements.Add(encaissementDepotDeGarantie);
                factureDepotDeGarantie.Echue = true;
                if (montantAVentiller >= montantDepotDeGarantie)
                {
                    factureDepotDeGarantie.FacturePayee = true;
                    factureDepotDeGarantie.Active = true;

                }
                premierVersement.Encaissements.Add(encaissementDepotDeGarantie);
                //Ventiller le versement aprés avoir extrait le dépot de garantie
                montantAVentiller = montantAVentiller - montantDepotDeGarantieAEncaisser;
                foreach (var facture in contrat.Factures.Where(u => u.FacturePayee == false && u.TypeFacture == TypeFacture.Echeance))
                {
                    if (montantAVentiller > 0)
                    {
                        if (facture.Encaissements != null)
                        {
                            var totalEncaissement = facture.Encaissements.Sum(u => u.Montant);
                            var resteAEncaisser = facture.Montant - totalEncaissement;
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
                                Date = DatePremierVersement.Date,
                                ModePaiement = modePaiement,
                                Montant = montantAEncaisser,
                                Commentaire = CommentaireGlobal,
                                ReferencePaiement = ReferenceGlobal
                            };
                            facture.Encaissements.Add(nouvelEncaissement);

                            premierVersement.Encaissements.Add(nouvelEncaissement);
                            if (montantAVentiller >= resteAEncaisser)
                            {
                                facture.FacturePayee = true;

                            }
                            facture.Active = true;
                            montantAVentiller -= montantAEncaisser;
                        }
                    }
                    else
                        break;
                }

                DB.SaveChanges();

                return contrat.Id;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string GenererNumeroFacturesDepot(string numeroContrat, TypeEcheancier typeEcheancier, DateTime DateEcheanceFacture)
        {
            return "FD" + numeroContrat.ToString().PadLeft(5, '0') + typeEcheancier.ToString().Substring(0, 2).ToUpper() + DateEcheanceFacture.Month.ToString().PadLeft(2, '0') + DateEcheanceFacture.Year.ToString().Substring(2, 2);
        }


        private string GenererNumeroFacturesResa(string numeroContrat, string etatAvancement, DateTime DateEcheanceFacture)
        {
            return "FR" + numeroContrat.ToString().PadLeft(5, '0') + etatAvancement.Substring(0, 2).ToUpper() + DateEcheanceFacture.Month.ToString().PadLeft(2, '0') + DateEcheanceFacture.Year.ToString().Substring(2, 2);
        }


        public static int CalculNombreEcheances(TypeEcheancier typeEcheancier, DateTime datePremiereEcheance, DateTime dateDerniereEcheance, ref int nbEcheances)
        {
            switch (typeEcheancier)
            {
                case TypeEcheancier.Mensuel:
                    nbEcheances = (dateDerniereEcheance.Year - datePremiereEcheance.Year) * 12 + dateDerniereEcheance.Month - datePremiereEcheance.Month;
                    break;
                //case TypeEcheancier.Bimensuel:
                //    nombreDeTraite = ((datefin.Year - dateDebut.Year) * 12 + datefin.Month - dateDebut.Month) / 2;
                //    break;
                case TypeEcheancier.Trimestriel:
                    nbEcheances = ((dateDerniereEcheance.Year - datePremiereEcheance.Year) * 12 + dateDerniereEcheance.Month - datePremiereEcheance.Month) / 3;
                    break;
                case TypeEcheancier.Semestriel:
                    nbEcheances = ((dateDerniereEcheance.Year - datePremiereEcheance.Year) * 12 + dateDerniereEcheance.Month - datePremiereEcheance.Month) / 6;
                    break;
                case TypeEcheancier.Annuel:
                    nbEcheances = ((dateDerniereEcheance.Year - datePremiereEcheance.Year) * 12 + dateDerniereEcheance.Month - datePremiereEcheance.Month) / 12;
                    break;
                default:
                    break;
            }

            return nbEcheances;
        }

        public static int CalculNombreEcheancesDepot(DateTime datePremiereEcheance, DateTime dateDerniereEcheance, TypeEcheancier typeEcheancier)
        {
            int nbEcheances = 0;
            switch (typeEcheancier)
            {
                case TypeEcheancier.Mensuel:
                    nbEcheances = (dateDerniereEcheance.Year - datePremiereEcheance.Year) * 12 + dateDerniereEcheance.Month - datePremiereEcheance.Month;
                    break;
                //case TypeEcheancier.Bimensuel:
                //    nombreDeTraite = ((datefin.Year - dateDebut.Year) * 12 + datefin.Month - dateDebut.Month) / 2;
                //    break;
                case TypeEcheancier.Trimestriel:
                    nbEcheances = ((dateDerniereEcheance.Year - datePremiereEcheance.Year) * 12 + dateDerniereEcheance.Month - datePremiereEcheance.Month) / 3;
                    break;
                case TypeEcheancier.Semestriel:
                    nbEcheances = ((dateDerniereEcheance.Year - datePremiereEcheance.Year) * 12 + dateDerniereEcheance.Month - datePremiereEcheance.Month) / 6;
                    break;
                case TypeEcheancier.Annuel:
                    nbEcheances = ((dateDerniereEcheance.Year - datePremiereEcheance.Year) * 12 + dateDerniereEcheance.Month - datePremiereEcheance.Month) / 12;
                    break;
                default:
                    break;
            }

            return nbEcheances + 1;
        }

        public void CalculerRemise(ref decimal prixDevente, ref decimal tauxRemiseAccordee, ref decimal MontantRemiseAccordee)
        {
            if (tauxRemiseAccordee != 0)
            {
                MontantRemiseAccordee = (prixDevente * tauxRemiseAccordee / 100);
                prixDevente = prixDevente - MontantRemiseAccordee;
            }
            else if (MontantRemiseAccordee != 0)
            {
                tauxRemiseAccordee = (MontantRemiseAccordee / prixDevente) * 100;
                prixDevente = prixDevente - MontantRemiseAccordee;

            }

        }


        public int AjouterContratReservation(int clientId, int commercialId, int apporteurAffaireId, int lotId,
             decimal prixRevise, decimal prixDeVente, decimal tauxRemiseAccordee, decimal MontantRemiseAccordee, TypeContrat typeContrat,
            decimal montantPremierVersement, DateTime dateContrat, String CommentaireGlobal, string ReferenceGlobal,
            ModePaiement modePaiement, DateTime dateLivraisonPrevue, decimal tauxDeRemiseFraisDeDossier)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    Contrat contrat = null;

                    using (var ctx = new SenImmoDataContext())
                    {

                        var lot = ctx.Lots.Find(lotId);
                        var client = ctx.Clients.Find(clientId);

                        ApporteurAffaire apporteur = null;
                        if (apporteurAffaireId != 0)
                        {
                            apporteur = new ApporteurAffaireRepository().FindById(apporteurAffaireId);
                        }
                        ////ENREGISTREMENT DU CONTRAT
                        //CALCUL DU MONTANT DE LA REMISE SI ACCORDEE ET DETERMINATION DU PRIX DE VENTE FINAL
                        decimal prixDevente = prixRevise;
                        CalculerRemise(ref prixDevente, ref tauxRemiseAccordee, ref MontantRemiseAccordee);
                        // VERIFIER SI LE MONTANT DU PREMIER VERSEMENT OU CELUI CUMULE DANS LE COMPTE DU CLIENT A ATTEINT LE SEUIL DE RESERVATION




                        // Enregistrer le contrat
                        contrat = new Contrat
                        {
                            NumeroContrat = lot.TypeVilla.CodeType + "-" + lot.NumeroLot.PadLeft(5, '0'),
                            ClientID = clientId,
                            CommercialID = commercialId,
                            DateCreationSysteme = DateTime.Now.Date,
                            PrixRevise = prixRevise,
                            PrixFinal = prixDeVente,
                            RemiseAccordee = MontantRemiseAccordee,
                            TypeContratID = typeContrat.ID,
                            LotId = lotId,
                            DateLivraisonLot = dateLivraisonPrevue.Date,
                            Statut = StatutContrat.Actif
                        };

                        if (apporteur != null)
                        {
                            contrat.ApporteurID = apporteur.Id;
                            contrat.CommissionApporteur = prixDeVente * apporteur.TauxCommission / 100;
                        }


                        var montantDepotDeGarantie = prixDevente * typeContrat.SeuilSouscription / 100;
                        var montantAventiller = prixDevente - montantDepotDeGarantie;



                        ctx.Contrats.Add(contrat);

                        ////GENERER LA FACTURE FRAIS DE DOSSIER
                        //var montantFraisDeDossier = (200000 - (200000 * tauxDeRemiseFraisDeDossier / 100));
                        //Facture facture = new Facture
                        //{
                        //    TypeFacture = TypeFacture.FraisDossier,
                        //    ContratId = contrat.Id,
                        //    ClientId = client.ID,
                        //    Date = dateContrat,
                        //    DateEcheanceFacture = dateContrat,

                        //    FacturePayee = false,
                        //    Echue = true,
                        //    Active=true,
                        //    NumeroFacture = "FDXXXXXXXXXXXXXXXXX",
                        //    Montant = montantFraisDeDossier,
                        //    Motif = "Frais de dossier"
                        //};

                        //contrat.Factures.Add(facture);



                        //GENERER LES ETATS D'AVANCEMENT
                        foreach (var niveauAvancement in ctx.TypeEtatAvancements.ToList())
                        {
                            //Générer d'office tous les etats d'avancement; sans préciser la date.
                            var etatAvancement = new EtatAvancement()
                            {
                                LotId = lot.ID,
                                TypeEtatAvancementID = niveauAvancement.ID,
                                Actif = false,

                            };
                            ctx.EtatAvancements.Add(etatAvancement);

                            ctx.SaveChanges();
                            //Si le niveau d'avancement correspond à un état générateur d'appel de fond alors générer l'appel de fond
                            //et la facture correspondants
                            if (niveauAvancement.AppelFonds == true)
                            {
                                var montantAppelDefond = contrat.PrixFinal * niveauAvancement.TauxDecaissement / 100;
                                var facture = new Facture
                                {
                                    TypeFacture = TypeFacture.AppelDeFond,
                                    ContratId = contrat.Id,
                                    ClientId = client.ID,
                                    Date = DateTime.Now.Date,
                                    DateEcheanceFacture = dateContrat,
                                    EtatAvancementId = etatAvancement.ID,
                                    FacturePayee = false,
                                    NumeroFacture = "",
                                    Montant = montantAppelDefond,
                                    Motif = niveauAvancement.Description
                                };

                                facture.NumeroFacture = GenererNumeroFacturesResa(contrat.NumeroContrat, niveauAvancement.Description.ToUpper(), facture.DateEcheanceFacture.Value);
                                contrat.Factures.Add(facture);
                                ctx.SaveChanges();

                                if (facture.EtatAvancement.TypeEtatAvancement.ordre == 1 || facture.EtatAvancement.TypeEtatAvancement.ordre == 2)
                                {
                                    facture.DateEcheanceFacture = dateContrat;
                                    facture.Echue = true;
                                }
                            }
                        }


                        //// ENREGISTRER LE PREMIER ENCAISSEMENT

                        var premierVersement = new EncaissementGlobal()
                        {
                            NumeroEncaissement = "EN" + contrat.NumeroContrat.ToString().PadLeft(5, '0') + dateContrat.Month.ToString().PadLeft(2, '0') + dateContrat.Year.ToString().Substring(2, 2),
                            DateEncaissement = dateContrat.Date,
                            MontantGlobal = montantPremierVersement,
                            ContratId = contrat.Id,
                            ModePaiement = modePaiement,
                            Commentaire = CommentaireGlobal,
                            ReferencePaiement = ReferenceGlobal

                        };
                        ctx.EncaissementGlobals.Add(premierVersement);

                        ////Mis à jour des éléments contractuels
                        contrat.MontantVerse += premierVersement.MontantGlobal;

                        //Enregistrer le paiement de commission à l'apporteur d'affaire
                        if (apporteur != null)
                        {
                            var factureCommission = new FactureCommission
                            {
                                EncaissementGlobalId = premierVersement.ID,
                                Date = dateContrat.Date,
                                MontantAPayer = premierVersement.MontantGlobal * apporteur.TauxCommission / 100,
                                Motif = "Commission sur premier versement",
                                Payee = false

                            };
                            contrat.FactureCommissions.Add(factureCommission);
                        }

                        ctx.SaveChanges();
                        VerifierStatutContratResa(contrat.Id, dateContrat);




                        //Ventiller le versement aprés avoir extrait le dépot de garantie
                        var montantAVentiller = montantPremierVersement;
                        ////Encaisser la facture Frais de dossier
                        //var factFraisDeDossier = contrat.Factures.Where(u => u.TypeFacture == TypeFacture.FraisDossier).SingleOrDefault();
                        //Encaissement encaissementFraisDeDossier = new Encaissement
                        //{
                        //    Date = dateContrat.Date,
                        //    ModePaiement = modePaiement,
                        //    Montant = factFraisDeDossier.Montant,
                        //    Commentaire = CommentaireGlobal,
                        //    ReferencePaiement = ReferenceGlobal
                        //};
                        //factFraisDeDossier.Encaissements.Add(encaissementFraisDeDossier);
                        //factFraisDeDossier.FacturePayee = true;

                        //premierVersement.Encaissements.Add(encaissementFraisDeDossier);

                        //montantAVentiller -= factFraisDeDossier.Montant;

                        //ctx.SaveChanges();



                        foreach (var fact in contrat.Factures.Where(u => u.FacturePayee == false && u.TypeFacture != TypeFacture.FraisDossier).OrderBy(u => u.EtatAvancement.TypeEtatAvancement.ordre))
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
                                        Date = dateContrat.Date,
                                        ModePaiement = modePaiement,
                                        Montant = montantAEncaisser,
                                        Commentaire = CommentaireGlobal,
                                        ReferencePaiement = ReferenceGlobal
                                    };
                                    fact.Encaissements.Add(nouvelEncaissement);

                                    premierVersement.Encaissements.Add(nouvelEncaissement);
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
                        ctx.SaveChanges();
                    }
                    scope.Complete();
                    return contrat.Id;

                }

            }
            catch (Exception)
            {

                throw;
            }

        }


        public void EnregistrerVersement(int lotId, int clientId, DateTime dateVersement, Decimal montantVersement, int contratId,
            ModePaiement modePaiement, string referencePaiement, string commentaire)
        {
            try
            {
                var lot = DB.Lots.Find(lotId);
                var client = DB.Clients.Find(clientId);
                var contrat = DB.Contrats.Find(contratId);

                //// Enregistrer le premier versement

                var versement = new EncaissementGlobal()
                {
                    NumeroEncaissement = "EN" + contrat.NumeroContrat.ToString().PadLeft(5, '0') + dateVersement.Month.ToString().PadLeft(2, '0') + dateVersement.Year.ToString().Substring(2, 2),
                    DateEncaissement = dateVersement.Date,
                    MontantGlobal = montantVersement,
                    ContratId = contrat.Id,
                    ModePaiement = modePaiement,
                    ReferencePaiement = referencePaiement,
                    Commentaire = commentaire
                };

                DB.EncaissementGlobals.Add(versement);

                ////Mis à jour des éléments contractuels
                contrat.MontantVerse += versement.MontantGlobal;

                if (contrat.Apporteur != null)
                {
                    var factureCommission = new FactureCommission
                    {
                        EncaissementGlobalId = versement.ID,
                        Date = dateVersement.Date,
                        MontantAPayer = versement.MontantGlobal * contrat.Apporteur.TauxCommission / 100,
                        Motif = "Commission sur l'encaissement " + versement.NumeroEncaissement + ", référence: " + versement.ReferencePaiement + ":" + versement.Commentaire,
                        Payee = false

                    };
                    contrat.FactureCommissions.Add(factureCommission);
                }

                DB.SaveChanges();

                //Controler le niveau d'encaissement et mettre à jour le contrat
                if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                {
                    VerifierStatutContratResa(contrat.Id, dateVersement);
                    lesFactures = contrat.Factures.Where(af => af.FacturePayee == false).OrderBy(af => af.EtatAvancement.TypeEtatAvancement.ordre);
                }
                else
                {
                    VerifierStatutContratDepot(contrat.Id, dateVersement);
                    lesFactures = contrat.Factures.Where(ech => ech.FacturePayee == false).OrderBy(ech => ech.DateEcheanceFacture);
                }




                //Ventiller le versement aprés avoir extrait le dépot de garantie
                var montantAVentiller = montantVersement;

                foreach (var facture in lesFactures)
                {
                    if (montantAVentiller > 0)
                    {
                        if (facture.Encaissements != null)
                        {
                            var totalEncaissement = facture.Encaissements.Sum(u => u.Montant);
                            var resteAEncaisser = facture.Montant - totalEncaissement;
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
                                Date = dateVersement,
                                ModePaiement = modePaiement,
                                Montant = montantAEncaisser,
                                Commentaire = commentaire,
                                ReferencePaiement = referencePaiement
                            };
                            facture.Encaissements.Add(nouvelEncaissement);

                            versement.Encaissements.Add(nouvelEncaissement);
                            if (montantAVentiller >= resteAEncaisser)
                            {
                                facture.FacturePayee = true;
                                //facture.Echue = true;
                            }
                            facture.Active = true;
                            montantAVentiller -= montantAEncaisser;
                        }
                    }
                    else
                        break;
                }

                DB.SaveChanges();
                if (montantAVentiller > 0)
                {
                    throw new Exception("Montant restant: " + montantAVentiller.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void MAJFactureGeneree(int id)
        {
            try
            {
                var facture = this.GetFactureById(id);
                facture.FactureGenere = true;
                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void MAJFactureCourrierEnvoye(int id)
        {
            var facture = this.GetFactureById(id);
            facture.CourrierEnvoye = true;
            DB.SaveChanges();
        }

        //public EncaissementProspect GetEncaissementProspect(int encaissementProspectId)
        //{
        //    try
        //    {
        //        return DB.EncaissementProspects.Find(encaissementProspectId);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        internal void SupprimerEncaissementProspect(int encaissementID)
        {
            try
            {
                var enc = DB.EncaissementProspects.Find(encaissementID);
                if (enc.FraisDeDossier == true)
                {
                    DB.FactureProspects.Remove(DB.FactureProspects.Find(enc.FactureId));
                }
                DB.EncaissementProspects.Remove(enc);

                var prospect = DB.Clients.Find(enc.ProspectId);
                //Si le prospect a pris une option, vérifier l'atteinte du seuil de contractualisation
                if (prospect.Options.Where(opt => opt.Active == true).Count() > 0)
                {
                    var option = prospect.Options.Where(opt => opt.Active == true).FirstOrDefault();

                    if (prospect.EncaissementProspects != null && prospect.EncaissementProspects.Count > 0)
                    {
                        var encaissementsProspect = prospect.EncaissementProspects.Where(encaissement => encaissement.FraisDeDossier == false).Sum(encaissement => encaissement.MontantGlobal);
                        var tauxEncaissement = encaissementsProspect / option.PrixDeVente * 100;

                        if ((option.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && tauxEncaissement >= option.TypeContrat.SeuilSouscription) ||
                                (option.TypeContrat.CategorieContrat == CategorieContrat.Réservation && tauxEncaissement >= option.TypeContrat.SeuilEntreeEnVigueur)
                            )
                        {
                            option.SeuilContratAtteint = true;
                            option.ContratGenere = false;
                        }
                        else
                        {
                            option.SeuilContratAtteint = false;
                            option.ContratGenere = false;
                        }
                    }

                }
                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void SupprimerEncaissementGlobal(int encID)
        {
            try
            {
                DB.Dispose();
                DB = new SenImmoDataContext();
                var encGlobal = DB.EncaissementGlobals.Find(encID);

                ////Vérifier que l'encaissement global n'est pas lèttrée sur une facture 
                //if (encGlobal.Encaissements.Any(enc => enc.Facture.TypeFacture == TypeFacture.FraisDossier || enc.Facture.TypeFacture == TypeFacture.DepotMinimum || enc.Facture.TypeFacture == TypeFacture.AvanceDemarrage))
                //    throw new Exception("Suppression impossible: encaissement lettré sur une facture ayant permit de générer un contrat");

                foreach (var enc in encGlobal.Encaissements)
                {
                    var fact = DB.Factures.Find(enc.FactureId);
                    fact.FacturePayee = false;
                    DB.SaveChanges();
                }

                DB.Encaissements.RemoveRange(encGlobal.Encaissements);
                DB.FactureCommissions.RemoveRange(encGlobal.FactureCommissions);
                DB.EncaissementGlobals.Remove(encGlobal);
                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void EnregistrerVersementProspect(int prospectId, DateTime dateVersement, Decimal montantVersement, ModePaiement modePaiement, string referencePaiement, string commentaire)
        {
            try
            {
                var prospect = DB.Clients.Find(prospectId);

                //// Vérifier s'il s'agit du premier encaissement au
                
                //// Enregistrer le premier versement

                var versement = new EncaissementProspect()
                {
                    NumeroEncaissement = "ENPR" + prospect.ID.ToString().PadLeft(4, '0') + dateVersement.Month.ToString().PadLeft(2, '0') + dateVersement.Year.ToString().Substring(2, 2),
                    DateEncaissement = dateVersement.Date,
                    MontantGlobal = montantVersement,
                    ProspectId = prospectId,
                    ModePaiement = modePaiement,
                    ReferencePaiement = referencePaiement,
                    Commentaire = commentaire
                };

                DB.EncaissementProspects.Add(versement);
                //Si le prospect a pris une option, vérifier l'atteinte du seuil de contractualisation
                if (prospect.Options.Where(opt => opt.Active == true).Count() > 0)
                {
                    var option = prospect.Options.Where(opt => opt.Active == true).FirstOrDefault();
                    var encaissementsProspect = prospect.EncaissementProspects.Where(enc => enc.FraisDeDossier == false).Sum(mvt => mvt.MontantGlobal);
                    var tauxEncaissement = encaissementsProspect / option.PrixDeVente * 100;

                    if (tauxEncaissement >= option.TypeContrat.SeuilSouscription && option.SeuilContratAtteint ==false)
                    {
                        option.SeuilContratAtteint = true;
                        option.ContratGenere = false;
                        versement.AtteinteSeuil = true;
                        option.DateAtteinteSeuil = dateVersement;
                    }
                }
                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void EnregistrerRemboursement(int soldeToutCompteId, DateTime dateVersement, Decimal montantVersement,
            ModePaiement modePaiement, string referencePaiement, string commentaire)
        {
            try
            {
                var soldeToutCompte = DB.SoldeDeToutComptes.Find(soldeToutCompteId);
                //// Enregistrer le premier versement
                var remboursement = new Remboursement()
                {
                    NumeroRemboursement = "RB" + soldeToutCompte.ContratId.ToString().PadLeft(4, '0') + dateVersement.Month.ToString().PadLeft(2, '0') + dateVersement.Year.ToString().Substring(2, 2),
                    DateRemboursement = dateVersement.Date,
                    MontantRembourse = montantVersement,
                    SoldeDeToutCompteId = soldeToutCompte.Id,
                    ModePaiement = modePaiement,
                    ReferencePaiement = referencePaiement,
                    Commentaire = commentaire,
                    ContratId = soldeToutCompte.ContratId
                };

                DB.Remboursements.Add(remboursement);


                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int GetNombreFacturesSoldees(int contratId)
        {
            try
            {
                var contrat = DB.Contrats.Find(contratId);
                if (contrat == null) throw new Exception("Contrat introuvable");
                var nbFacturesTrouvees = DB.Factures.Where(f => f.ContratId == contratId).Where(f => f.FacturePayee == true && f.TypeFacture == TypeFacture.Echeance).Count();
                return nbFacturesTrouvees;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int GetNombreFacturesNonSoldees(int contratId)
        {
            try
            {
                var contrat = DB.Contrats.Find(contratId);
                if (contrat == null) throw new Exception("Contrat introuvable");
                var nbFacturesTrouvees = DB.Factures.Where(f => f.ContratId == contratId).Where(f => f.FacturePayee == false && f.TypeFacture == TypeFacture.Echeance).Count();
                return nbFacturesTrouvees;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int GetNombreFacturesEchuesNonSoldees(int contratId)
        {
            try
            {
                var contrat = DB.Contrats.Find(contratId);
                if (contrat == null) throw new Exception("Contrat introuvable");
                var nbFacturesTrouvees = DB.Factures.Where(f => f.ContratId == contratId).Where(f => f.Echue == true && f.FacturePayee == false && f.TypeFacture == TypeFacture.Echeance).Count();
                return nbFacturesTrouvees;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int GetNombreTotalFactures(int contratId)
        {
            try
            {
                var contrat = DB.Contrats.Find(contratId);
                if (contrat == null) throw new Exception("Contrat introuvable");
                var nbFacturesTrouvees = DB.Factures.Where(f => f.ContratId == contratId && f.TypeFacture == TypeFacture.Echeance).Count();
                return nbFacturesTrouvees;
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
                DB.Set<Facture>().AsNoTracking();
                var contrat = DB.Contrats.Find(contratId);
                if (contrat == null) throw new Exception("Contrat introuvable");
                var lesFacturesTrouvees = DB.Factures.Where(f => f.ContratId == contratId);//&& f.Active==true
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

        public Facture GetDerniereEcheances(int contratId)
        {
            try
            {
                var contrat = DB.Contrats.Find(contratId);
                if (contrat == null) throw new Exception("Contrat introuvable");
                var laDerniereEcheance = DB.Factures.Where(f => f.ContratId == contratId && f.Active == true && f.Echue == true)
                                                    .OrderBy(f => f.DateEcheanceFacture).FirstOrDefault();//&& f.Active==true
                //if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && etatFacture=="")
                //    lesFacturesTrouvees = lesFacturesTrouvees.Where(f => f.Active == true);
                return laDerniereEcheance;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<EncaissementGlobal> GetVersementsClient(int contratId)
        {
            try
            {
                var versementsTrouves = DB.EncaissementGlobals.Where(eg => eg.ContratId == contratId);

                return versementsTrouves;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<Facture> GetNewAppelsDeFonds()
        {
            try
            {
                var lesFacturesTrouvees = DB.Factures.Where(f => f.Contrat.Statut == StatutContrat.Actif && f.Active == true && f.TypeFacture == TypeFacture.AppelDeFond &&
               f.EtatAvancement.TypeEtatAvancement.AppelFonds == true && f.EtatAvancement.TypeEtatAvancement.NiveauTechnique == true && f.FactureGenere == false);//&& f.Active==true

                return lesFacturesTrouvees;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Facture GetDernierAppelsDeFonds(int contratId)
        {
            try
            {
                var lesAppelsDeFonds = DB.Factures.Where(f => f.Contrat.Id == contratId && f.Active == true && f.Echue == true
                   && f.TypeFacture == TypeFacture.AppelDeFond);

                if (lesAppelsDeFonds.Count() > 0)
                {

                    var lastNiveauAppelDeFond = lesAppelsDeFonds.Max(f => f.EtatAvancement.TypeEtatAvancement.ordre);


                    var dernierAppelDeFonds = DB.Factures.Where(f => f.ContratId == contratId
                                && f.TypeFacture == TypeFacture.AppelDeFond && f.Active == true && f.Echue == true && f.EtatAvancement.TypeEtatAvancement.NiveauTechnique == true
                                && f.EtatAvancement.TypeEtatAvancement.ordre == lastNiveauAppelDeFond).FirstOrDefault();

                    return dernierAppelDeFonds;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public IEnumerable<Contrat> GetContratsResa()
        {
            try
            {
                //var contratsResa = DB.Contrats.Select(cont => new
                //{
                //    Lot = cont.Lot.NumeroLot,
                //    Type = cont.Lot.TypeVilla.CodeType,
                //    Client = cont.Client.NomComplet,
                //    PrixVente = cont.PrixFinal,
                //    MontantEncaisse = cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal),
                //    SeuilCumule = cont.Factures.Where(f => f.Echue == true).Sum(f => f.EtatAvancement.TypeEtatAvancement.TauxDecaissement),
                //    MontantCumule = cont.Factures.Where(f => f.Echue == true).Sum(f => f.Montant)
                //});

                return DB.Contrats.Where(cont => cont.TypeContrat.CategorieContrat == CategorieContrat.Réservation);


            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Contrat> GetContratsDepot()
        {
            try
            {
                //var contratsResa = DB.Contrats.Select(cont => new
                //{
                //    Lot = cont.Lot.NumeroLot,
                //    Type = cont.Lot.TypeVilla.CodeType,
                //    Client = cont.Client.NomComplet,
                //    PrixVente = cont.PrixFinal,
                //    MontantEncaisse = cont.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal),
                //    SeuilCumule = cont.Factures.Where(f => f.Echue == true).Sum(f => f.EtatAvancement.TypeEtatAvancement.TauxDecaissement),
                //    MontantCumule = cont.Factures.Where(f => f.Echue == true).Sum(f => f.Montant)
                //});

                return DB.Contrats.Where(cont => cont.TypeContrat.CategorieContrat == CategorieContrat.Dépôt);


            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<Facture> GetEcheances()
        {
            try
            {

                var lesFacturesTrouvees = DB.Factures.Where(f => f.Contrat.Statut == StatutContrat.Actif && f.TypeFacture == TypeFacture.Echeance && f.Echue == true);
                return lesFacturesTrouvees;

            }
            catch (Exception)
            {
                throw;
            }
        }

        //public IEnumerable<Facture> GetEcheances()
        //{
        //    try
        //    {

        //        var lesFacturesTrouvees = DB.Factures.Where(f => f.Contrat.Statut == StatutContrat.Actif && f.TypeFacture == TypeFacture.Echeance && f.Echue == true);
        //        return lesFacturesTrouvees;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        public IEnumerable<MouvementComptable> GetOperationsContrat(int contratId)
        {
            try
            {
                var query = (from fact in DB.Factures
                             where fact.ContratId == contratId && fact.Active == true && fact.Echue == true
                             select new MouvementComptable
                             {
                                 Id = fact.Id,
                                 DateOp = fact.DateEcheanceFacture.Value,
                                 NumeroPiece = fact.NumeroFacture,
                                 LibelleOp = fact.Motif,
                                 Debit = fact.Montant,
                                 Credit = 0,
                                 Solde = fact.Encaissements.Count() > 0 ? fact.Montant - fact.Encaissements.Sum(u => u.Montant) : fact.Montant,
                                 TypeOp = "F"
                             })
                               .Union
                               (from enc in DB.EncaissementGlobals
                                where enc.ContratId == contratId
                                select new MouvementComptable
                                {
                                    Id = enc.ID,
                                    DateOp = enc.DateEncaissement.Value,
                                    NumeroPiece = enc.NumeroEncaissement,
                                    LibelleOp = enc.ReferencePaiement,
                                    Debit = 0,
                                    Credit = enc.MontantGlobal,
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


        public IEnumerable<MouvementComptable> GetCompteRemboursements(int contratId)
        {
            try
            {
                var query = (from fact in DB.SoldeDeToutComptes
                             where fact.ContratId == contratId
                             select new MouvementComptable
                             {
                                 Id = fact.Id,
                                 DateOp = fact.DateResiliation,
                                 NumeroPiece = fact.NumeroFacture,
                                 LibelleOp = "Solde de tout compte",
                                 Debit = 0,
                                 Credit = fact.MontantARembourser,
                                 Solde = fact.MontantARembourser,// fact.Encaissements.Count() > 0 ? fact.Montant - fact.Encaissements.Sum(u => u.Montant) : fact.Montant,
                                 TypeOp = "F"
                             })
                               .Union
                               (from enc in DB.Remboursements
                                where enc.ContratId == contratId
                                select new MouvementComptable
                                {
                                    Id = enc.ID,
                                    DateOp = enc.DateRemboursement.Value,
                                    NumeroPiece = enc.NumeroRemboursement,
                                    LibelleOp = enc.ReferencePaiement,
                                    Debit = enc.MontantRembourse,
                                    Credit = 0,
                                    Solde = 0,
                                    TypeOp = "R"
                                })
                               .OrderBy(op => op.DateOp);
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal EncaissementProspect GetFraisDeDossierNonDeverse(int idProspect)
        {
            try
            {
                var encaissementFraisDeDossier = DB.EncaissementProspects.Where(enc => enc.FraisDeDossier == true && enc.Deverse == false
                                                     && enc.ProspectId == idProspect).FirstOrDefault();
                return encaissementFraisDeDossier;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MouvementComptable> GetMouvementsProspect(int prospectId)
        {
            try
            {
                DB.Dispose();
                DB = new SenImmoDataContext();

                var query = (from fact in DB.FactureProspects
                             where fact.ProspectId == prospectId
                             select new MouvementComptable
                             {
                                 Id = fact.Id,
                                 DateOp = fact.Date.Value,
                                 NumeroPiece = fact.NumeroFacture,
                                 LibelleOp = "Frais de dossier",
                                 Debit = fact.Montant,
                                 Credit = 0,
                                 Solde = 0,// fact.Encaissements.Count() > 0 ? fact.Montant - fact.Encaissements.Sum(u => u.Montant) : fact.Montant,
                                 TypeOp = "F"
                             })
                            .Union
                            (from encPros in DB.EncaissementProspects
                             where encPros.ProspectId == prospectId
                             select new MouvementComptable
                             {
                                 Id = encPros.ID,
                                 DateOp = encPros.DateEncaissement.Value,
                                 NumeroPiece = encPros.NumeroEncaissement,
                                 LibelleOp = encPros.ReferencePaiement + ": " + encPros.Commentaire,
                                 Debit = 0,
                                 Credit = encPros.MontantGlobal,
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


        public IEnumerable<EncaissementProspect> GetEncaissementProspect(int prospectId)
        {
            try
            {
                var query = DB.EncaissementProspects.Where(enc => enc.ProspectId == prospectId);

                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EncaissementProspect GetEncaissementProspectById(int encProspectId)
        {
            try
            {
                return DB.EncaissementProspects.Find(encProspectId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Encaissement> GetEncaissementGlobals(int contratId)
        {
            try
            {
                var query = DB.Encaissements.Where(enc => enc.EncaissementGlobal.ContratId == contratId && enc.Facture.TypeFacture != TypeFacture.FraisDossier);

                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EncaissementGlobal GetEncaissementGlobalById(int encGlobalId)
        {
            try
            {
                return DB.EncaissementGlobals.Find(encGlobalId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DateTime GetMaxEncaissementGlobal(int contratId)
        {
            try
            {
                return DB.EncaissementGlobals.Where(enc => enc.ContratId == contratId).Max(enc => enc.DateEncaissement).Value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Option> GetOptionsAyantAtteintsSeuilContrat(int commercialId)
        {
            try
            {
                var query = DB.Options.Where(op => op.Active == true && op.CommercialID == commercialId && op.SeuilContratAtteint == true
                            && op.ContratGenere == false).ToList();
                return query;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Facture> GetNouveauxAppelsDeFond()
        {
            try
            {
                var query = DB.Factures.Where(fact => fact.TypeFacture == TypeFacture.AppelDeFond
                            && fact.FactureGenere == false);
                return query;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Client> GetNouveauProspects(int commercialId)
        {
            try
            {
                var query = DB.Clients.Where(c => c.Actif == true && (c.Type == TypeClient.ProspectSansOption || c.Type == TypeClient.ProspectAvecOptionResa || c.Type == TypeClient.ProspectAvecOptionDepot)
                && c.CommercialID == commercialId && c.ProspectEdite == false);
                return query;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ActiviteCommerciale> GetActivitesCommerciales(int commercialId, DateTime date)
        {
            try
            {
                var query = DB.ActiviteCommerciales.Where(ac => ac.CommercialID == commercialId && ac.DateActivite == date);
                return query;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<ActiviteCommerciale> GetActivitesCommercialesATerme(int commercialId)
        {
            DateTime dateRappel = DateTime.Now.Date;
            TimeSpan heureRappel = DateTime.Now.TimeOfDay;
            TimeSpan heureFinRappel = heureRappel.Add(TimeSpan.FromMinutes(10));
            try
            {
                var query = DB.ActiviteCommerciales.Where(ac => ac.CommercialID == commercialId && ac.DateRappel == dateRappel
                                                            && ac.HeureRappel <= heureRappel && ac.BRappel == true);
                return query;
                //
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
                return DB.Contrats.Where(ct => ct.LotId == lot.ID).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Option GetOptionProspect(int prospectId)
        {
            try
            {
                return DB.Options.Where(opt => opt.ClientID == prospectId && opt.Active == true).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Facture GetFactureById(int idFacture)
        {
            try
            {
                return DB.Factures.Find(idFacture);
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void VerifierStatutContratResa(int contratId, DateTime dateEncaissement)
        {

            try
            {
                //Controler le niveau d'encaissement et mettre à jour le contrat
                var contrat = DB.Contrats.Find(contratId);
                if (contrat != null)
                {
                    var niveauEncaissement = contrat.MontantVerse / contrat.PrixFinal * 100;
                    var typeContrat = DB.TypeContrats.Find(contrat.TypeContratID);
                    if (niveauEncaissement >= typeContrat.SeuilEntreeEnVigueur && contrat.AReserve == false)
                    {
                        contrat.AReserve = true;
                        contrat.Lot.StatutLot = StatutLot.Reserve;
                        contrat.Client.Type = TypeClient.Client;
                        contrat.DateReservation = dateEncaissement;
                        DB.SaveChanges();
                    }
                    else if (niveauEncaissement >= typeContrat.SeuilSouscription && contrat.Souscrit == false)
                    {
                        contrat.Souscrit = true;
                        //contrat.Lot.StatutLot = StatutLot.ReservationEnCours;
                        contrat.Client.Type = TypeClient.ClientEnCours;
                        contrat.DateSouscription = dateEncaissement;
                        DB.SaveChanges();
                    }
                }
                else
                    throw new Exception("Contrat introuvable");

            }
            catch (Exception)
            {

                throw;
            }

        }


        private void VerifierStatutContratDepot(int contratId, DateTime dateEncaissement)
        {
            try
            {
                var contrat = DB.Contrats.Find(contratId);
                if (contrat != null)
                {
                    //Controler le niveau d'encaissement et mettre à jour le contrat
                    var niveauEncaissement = contrat.MontantVerse / contrat.PrixFinal * 100;
                    if (niveauEncaissement >= contrat.TypeContrat.SeuilEntreeEnVigueur && contrat.AReserve == false)
                    {
                        contrat.AReserve = true;
                        //contrat.Lot.StatutLot = StatutLot.Reserve;
                        contrat.AttribuerLot = true;
                        contrat.Client.Type = TypeClient.Client;
                        contrat.DateReservation = dateEncaissement;
                    }
                    else if (niveauEncaissement >= contrat.TypeContrat.SeuilSouscription && contrat.Souscrit == false)
                    {
                        contrat.Souscrit = true;
                        //contrat.Lot.StatutLot = StatutLot.ReservationEnCours;
                        contrat.AttribuerLot = false;
                        contrat.Client.Type = TypeClient.ClientEnCours;
                        contrat.DateSouscription = dateEncaissement;
                    }
                    DB.SaveChanges();
                }
                else
                    throw new Exception("Contrat introuvable");
            }
            catch (Exception)
            {

                throw;
            }

        }


        public bool AttribuerLotDepot(int contratId, int lotId)
        {
            try
            {
                var contrat = DB.Contrats.Find(contratId);
                var lot = DB.Lots.Find(lotId);
                contrat.LotId = lot.ID;
                contrat.LotAttribue = true;
                contrat.DateReservation = DateTime.Now.Date;
                lot.StatutLot = StatutLot.Reserve;
                DB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public decimal ResilierContrat(int contratId, DateTime dateResiliation)
        {
            try
            {
                //Calculer le cumul des montants encaissés sur le contrat
                var contrat = this.GetContratById(contratId);
                decimal montantTotalEncaisse = contrat.EncaissementGlobals.Sum(enc => enc.MontantGlobal);
                var montantArembourser = montantTotalEncaisse;
                decimal montantDepotGarantie = 0;
                // Si le taux d'encaissement supérieur au niveau d'entée en vigueur soustraire le montant de dépot de garantie 5%
                var tauxEncaissement = montantTotalEncaisse / contrat.PrixFinal * 100;
                if ((contrat.TypeContrat.CategorieContrat == CategorieContrat.Dépôt && tauxEncaissement >= contrat.TypeContrat.SeuilSouscription) ||
                    (contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation && tauxEncaissement >= contrat.TypeContrat.SeuilEntreeEnVigueur))
                {
                    montantDepotGarantie = contrat.PrixFinal * 5 / 100;
                    montantArembourser -= montantDepotGarantie;
                }
                // Si existence d'apporteur d'affaire, retrancher l'ensemble des commissions déjà versées à l'AA
                decimal montantCommissionsVersees = 0;
                if (contrat.Apporteur != null)
                {

                    montantCommissionsVersees = contrat.FactureCommissions.Where(fact => fact.FactureCommissionGlobale != null && fact.FactureCommissionGlobale.Payee == true)
                                                       .Sum(fact => fact.MontantAPayer);


                    montantArembourser -= montantCommissionsVersees;
                }
                //Enregistrer le solde tout compte
                var soldeToutCompte = new SoldeDeToutCompte()
                {
                    DateResiliation = dateResiliation,
                    PrixDeVente = contrat.PrixFinal,
                    MontantTotalEncaisse = montantTotalEncaisse,
                    MontantDepotDeGarantie = montantDepotGarantie,
                    MontantTotalCommissionsVersees = montantCommissionsVersees,
                    MontantARembourser = montantArembourser,
                    Remboursé = false,
                    ContratId = contratId
                };
                DB.SoldeDeToutComptes.Add(soldeToutCompte);


                // Mettre le statut du contrat à "Résilié"
                contrat.Statut = StatutContrat.Résilié;

                //Remettre le lot à libre
                if (contrat.Lot != null)
                    contrat.Lot.StatutLot = StatutLot.Libre;

                // Mettre le statut du client à "Desisté"
                // vérifier si le client a d'autres contrat, auquel cas laisser son statut par rapport à l'autre contrat, sinon flaguer le client comme résilié
                if (contrat.Client.Contrats.Where(cont => cont.Statut == StatutContrat.Actif).Count() <= 0)
                    contrat.Client.Type = TypeClient.Résilié;


                DB.SaveChanges();
                return montantArembourser;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public SoldeDeToutCompte GetSoldeToutCompte(int contratId)
        {
            return DB.SoldeDeToutComptes.Where(contrat => contrat.ContratId == contratId).FirstOrDefault();
        }


        public void AddOptionProspectResa(int prospectId, int typeContratId, int typeVillaId, decimal prixStandard, decimal prixRevise, PositionLot position, decimal surface, int lotId, int commercialID, decimal prixDeVente,
            bool bLimiteDansLeTemps, DateTime dateDebutOption, DateTime dateFinOption, decimal remiseAccordee, decimal tauxRemise)
        {
            try
            {
                var optionResa = new Option()
                {
                    ClientID = prospectId,
                    CommercialID = commercialID,
                    LotId = lotId,
                    PositionLot = position,
                    PrixStandard = prixStandard,
                    PrixRevise = prixRevise,
                    Surface = surface,
                    PrixDeVente = prixDeVente,
                    MontantRemiseAccordee = remiseAccordee,
                    TauxRemiseAccordee = tauxRemise,
                    TypeContratId = typeContratId,
                    TypeVillaId = typeVillaId,
                    Active = true,
                    SeuilContratAtteint = false,
                    ContratGenere = false,
                    DatePriseOption = dateDebutOption
                };

                if (bLimiteDansLeTemps)
                {
                    optionResa.DateFinOption = dateFinOption;
                }
                DB.Options.Add(optionResa);
                var leLot = DB.Lots.Where(lot => lot.ID == lotId).SingleOrDefault();
                leLot.StatutLot = StatutLot.Option;
                var leProspect = DB.Clients.Where(prospect => prospect.ID == prospectId).SingleOrDefault();
                leProspect.Type = TypeClient.ProspectAvecOptionResa;
                DB.SaveChanges();

                var option = leProspect.Options.Where(opt => opt.Active == true).FirstOrDefault();
                var encaissementsProspect = leProspect.EncaissementProspects.Sum(mvt => mvt.MontantGlobal);
                var tauxEncaissement = encaissementsProspect / option.PrixDeVente * 100;

                if (option.TypeContrat.CategorieContrat == CategorieContrat.Réservation && tauxEncaissement >= option.TypeContrat.SeuilEntreeEnVigueur)
                {
                    option.SeuilContratAtteint = true;
                    option.ContratGenere = false;
                }

                DB.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void AddOptionProspectDepot(int prospectId, int typeContratId, int typeVillaId, decimal prixStandard, decimal prixRevise, PositionLot position, decimal surface,
            int commercialID, decimal prixDeVente, bool bLimiteDansLeTemps, DateTime dateDebutOption, DateTime dateFinOption, int lotId, decimal remiseAccordee, decimal tauxRemise)
        {
            try
            {

                var optionDepot = new Option()
                {
                    ClientID = prospectId,
                    CommercialID = commercialID,
                    PositionLot = position,
                    Surface = surface,
                    PrixStandard = prixStandard,
                    PrixRevise = prixRevise,
                    MontantRemiseAccordee = remiseAccordee,
                    TauxRemiseAccordee = tauxRemise,
                    PrixDeVente = prixDeVente,
                    TypeContratId = typeContratId,
                    TypeVillaId = typeVillaId,
                    Active = true,
                    SeuilContratAtteint = false,
                    ContratGenere = false,
                    DatePriseOption = dateDebutOption,
                    LotId = lotId
                };
                if (bLimiteDansLeTemps)
                {
                    optionDepot.DateFinOption = dateFinOption;
                }
                DB.Options.Add(optionDepot);
                var leProspect = DB.Clients.Where(prospect => prospect.ID == prospectId).SingleOrDefault();
                leProspect.Type = TypeClient.ProspectAvecOptionDepot;
                DB.SaveChanges();

                var option = leProspect.Options.Where(opt => opt.Active == true).FirstOrDefault();
                var encaissementsProspect = leProspect.EncaissementProspects.Where(enc => enc.FraisDeDossier == false).Sum(mvt => mvt.MontantGlobal);
                var tauxEncaissement = encaissementsProspect / option.PrixDeVente * 100;

                if (DB.TypeContrats.Find(option.TypeContratId).CategorieContrat == CategorieContrat.Dépôt && tauxEncaissement >= option.TypeContrat.SeuilSouscription)
                {
                    option.SeuilContratAtteint = true;
                    option.ContratGenere = false;
                }

                DB.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CloturerOption(int optionId, int lotChoisiId)
        {
            try
            {
                var option = DB.Options.Find(optionId);
                if (option.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                {
                    if(option.LotId != lotChoisiId)
                        option.Lot.StatutLot = StatutLot.Libre; 
                }
                option.ContratGenere = true;

                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void EnregistrerFraisDeDossierProspect(int prospectId, DateTime dateVersement,
            Decimal montantFraisDeDossier, ModePaiement modePaiement, string referencePaiement, string commentaire)
        {
            try
            {
                var prospect = DB.Clients.Find(prospectId);

                var newFacture = new FactureProspect()
                {
                    Motif = "Frais de dossier",
                    Date = dateVersement.Date,
                    Montant = montantFraisDeDossier,
                    TypeFacture = TypeFacture.FraisDossier,
                    ProspectId = prospectId,

                };
                var newEncaissement = new EncaissementProspect()
                {
                    NumeroEncaissement = "ENFD" + dateVersement.Month.ToString().PadLeft(2, '0') + dateVersement.Year.ToString().Substring(2, 2),
                    DateEncaissement = dateVersement.Date,
                    MontantGlobal = montantFraisDeDossier,
                    ProspectId = prospectId,
                    ReferencePaiement = referencePaiement,
                    Commentaire = "Encaissement frais de dossier " + (referencePaiement != string.Empty ? "sur " + referencePaiement : ""),
                    FraisDeDossier = true
                };
                newFacture.EncaissementsProspects.Add(newEncaissement);
                DB.FactureProspects.Add(newFacture);

                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void TransfererFraisDeDossier(int contratId)
        {
            try
            {
                var contrat = DB.Contrats.Find(contratId);
                if (contrat == null) throw new Exception("Contrat introuvable");
                //Recuperer la facture et encaissement Frais de Dossier
                var encaissementFraisDeDossier = DB.EncaissementProspects.Where(enc => enc.FraisDeDossier == true && enc.Deverse == false
                                             && enc.ProspectId == contrat.ClientID).FirstOrDefault();
                if (encaissementFraisDeDossier == null)
                    throw new Exception("Frais de dossier introuvable");
                var newFacture = new Facture
                {
                    TypeFacture = TypeFacture.FraisDossier,
                    ContratId = contrat.Id,
                    ClientId = contrat.ClientID,
                    Date = DateTime.Now.Date,
                    DateEcheanceFacture = encaissementFraisDeDossier.DateEncaissement,
                    Active = true,
                    Echue = true,
                    FacturePayee = true,
                    NumeroFacture = "FCFD" + contrat.NumeroContrat.ToString().PadLeft(5, '0') + encaissementFraisDeDossier.DateEncaissement.Value.Month.ToString().PadLeft(2, '0') + encaissementFraisDeDossier.DateEncaissement.Value.Year.ToString().Substring(2, 2),
                    Montant = encaissementFraisDeDossier.Facture.Montant,
                    Motif = "Frais de dossier"
                };

                contrat.Factures.Add(newFacture);


                var versement = new EncaissementGlobal()
                {
                    NumeroEncaissement = "ENFD" + contrat.NumeroContrat.ToString().PadLeft(5, '0') + encaissementFraisDeDossier.DateEncaissement.Value.Month.ToString().PadLeft(2, '0') + encaissementFraisDeDossier.DateEncaissement.Value.Year.ToString().Substring(2, 2),
                    DateEncaissement = encaissementFraisDeDossier.DateEncaissement.Value,
                    MontantGlobal = encaissementFraisDeDossier.MontantGlobal,
                    ContratId = contrat.Id,
                    ModePaiement = encaissementFraisDeDossier.ModePaiement,
                    ReferencePaiement = encaissementFraisDeDossier.ReferencePaiement,
                    Commentaire = "Frais de dossier",

                };

                DB.EncaissementGlobals.Add(versement);

                ////Mis à jour des éléments contractuels
                contrat.MontantVerse += versement.MontantGlobal;

                Encaissement nouvelEncaissement = new Encaissement
                {
                    Date = encaissementFraisDeDossier.DateEncaissement.Value,
                    ModePaiement = encaissementFraisDeDossier.ModePaiement,
                    Montant = encaissementFraisDeDossier.MontantGlobal,
                    Commentaire = "Frais de dossier",
                    ReferencePaiement = encaissementFraisDeDossier.ReferencePaiement,
                };
                newFacture.Encaissements.Add(nouvelEncaissement);

                versement.Encaissements.Add(nouvelEncaissement);

                encaissementFraisDeDossier.Deverse = true;
                DB.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void ValiderContrat(int contratId)
        {
            try
            {
                var contrat = DB.Contrats.Find(contratId);
                if (contrat == null) throw new Exception("Contrat introuvable");
                contrat.ContratValide = true;
                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void ValiderContratDepot(int contratId)
        {
            try
            {
                var contrat = DB.Contrats.Find(contratId);
                if (contrat == null) throw new Exception("Contrat introuvable");
                contrat.ContratDepotValide = true;
                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AjouterContratReservationBis(int projetId,int clientId, int commercialId, int apporteurAffaireId, int lotId,
             decimal prixRevise, decimal prixDeVente, decimal tauxRemiseAccordee, decimal MontantRemiseAccordee, TypeContrat typeContrat,
            DateTime dateContrat, DateTime dateLivraisonPrevue, decimal tauxDeRemiseFraisDeDossier, bool bAvenant, int idContratDesiste)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    Contrat contrat = null;

                    using (var database = new SenImmoDataContext())
                    {
                        #region RECUPERATION DES DIFFERENTS CONSTITUANTS DU CONTRAT

                        var lot = database.Lots.Find(lotId);
                        var client = database.Clients.Find(clientId);

                        ApporteurAffaire apporteur = null;
                        if (apporteurAffaireId != 0)
                        {
                            apporteur = new ApporteurAffaireRepository().FindById(apporteurAffaireId);
                        }
                        #endregion


                        #region CALCUL DU MONTANT DE LA REMISE SI ACCORDEE ET DETERMINATION DU PRIX DE VENTE FINAL

                        decimal prixDevente = prixRevise;
                        //CalculerRemise(ref prixDevente, ref tauxRemiseAccordee, ref MontantRemiseAccordee);

                        #endregion
                        // VERIFIER SI LE MONTANT DU PREMIER VERSEMENT OU CELUI CUMULE DANS LE COMPTE DU CLIENT A ATTEINT LE SEUIL DE RESERVATION

                        #region CREATION DU CONTRAT

                        // Enregistrer le contrat
                        contrat = new Contrat
                        {
                            NumeroContrat = lot.TypeVilla.CodeType + "-" + lot.NumeroLot.PadLeft(5, '0'),
                            ClientID = clientId,
                            CommercialID = commercialId,
                            DateCreationSysteme = DateTime.Now.Date,
                            DateSouscription = dateContrat,
                            PrixRevise = prixRevise,
                            PrixFinal = prixDeVente,
                            RemiseAccordee = MontantRemiseAccordee,
                            TauxRemiseAccordee=tauxRemiseAccordee,
                            TypeContratID = typeContrat.ID,
                            LotId = lotId,
                            DateLivraisonLot = dateLivraisonPrevue.Date,
                            Statut = StatutContrat.Actif,
                            ProjetId= projetId
                        };

                        if (bAvenant)
                        {
                            var precedentContrat = database.Contrats.Find(idContratDesiste);
                            if (precedentContrat != null)
                            {
                                contrat.AvecAvenant = true;
                                contrat.DateSouscription = precedentContrat.DateSouscription;
                                contrat.ContratPrecedentID = precedentContrat.Id;
                            }
                        }

                        //DB.SaveChanges();

                        if (apporteur != null)
                        {
                            contrat.ApporteurID = apporteur.Id;
                            contrat.CommissionApporteur = prixDeVente * apporteur.TauxCommission / 100;
                        }


                        var montantDepotDeGarantie = prixDevente * typeContrat.SeuilSouscription / 100;
                        // var montantAventiller = prixDevente - montantDepotDeGarantie;

                        contrat.AReserve = true;
                        contrat.DateReservation = dateContrat;
                        database.Contrats.Add(contrat);

                        lot.StatutLot = StatutLot.Reserve;
                        client.Type = TypeClient.Client;

                        #endregion


                        #region GENERATION SYSTEMATIQUE DE TOUS LES APPELS DE FOND DU CONTRAT

                        foreach (var niveauAvancement in database.TypeEtatAvancements.Where(ta =>ta.TypeContratId== typeContrat.ID).ToList())
                        {
                            #region RECUPERER l'ETAT D'AVANCEMENT CORRESPONDANT
                            //Générer d'office tous les etats d'avancement; sans préciser la date.
                            EtatAvancement etatAvancement;
                            etatAvancement = database.EtatAvancements.Where(ea => ea.LotId == lot.ID && ea.TypeEtatAvancementID == niveauAvancement.ID).SingleOrDefault();
                            #region AU CAS OU L'ETAT D'AVANCEMENT EST INEXISTANT(CAS QUASI IMPOSSIBLE CAR GENERES DEPUIS LA CREATION DU LOT)
                            if (etatAvancement == null)
                            {

                                etatAvancement = new EtatAvancement()
                                {
                                    LotId = lot.ID,
                                    TypeEtatAvancementID = niveauAvancement.ID,
                                    Actif = false,

                                };
                                database.EtatAvancements.Add(etatAvancement);

                                database.SaveChanges();

                            }
                            #endregion

                            #endregion

                            //Si le niveau d'avancement correspond à un état générateur d'appel de fond alors générer l'appel de fond
                            //et la facture correspondants
                            if (niveauAvancement.AppelFonds == true)
                            {
                                //
                                decimal montantAppelDefond;
                                TypeContrat tc;
                                //if (projetId == 1)
                                //{
                                //    if (niveauAvancement.ordre == 2)
                                //    {
                                //        tc = DB.TypeContrats.Find(contrat.TypeContratID);
                                //        montantAppelDefond = contrat.PrixFinal * (tc.SeuilSouscription - 5) / 100;
                                //    }
                                //    else
                                //                                if (niveauAvancement.ordre == 26)
                                //    {
                                //        tc = DB.TypeContrats.Find(contrat.TypeContratID);
                                //        montantAppelDefond = contrat.PrixFinal * (70 - tc.SeuilSouscription) / 100;
                                //    }
                                //    else
                                //    {
                                //        montantAppelDefond = contrat.PrixFinal * niveauAvancement.TauxDecaissement / 100;
                                //    }
                                //}
                                //else
                                //{
                                    montantAppelDefond = contrat.PrixFinal * niveauAvancement.TauxDecaissement / 100;
                               // }


                                var facture = new Facture
                                {
                                    TypeFacture = niveauAvancement.ordre == 1 ? TypeFacture.DepotMinimum : (niveauAvancement.ordre == 2 && niveauAvancement.TypeContratId==1? TypeFacture.DepotMinimum : TypeFacture.AppelDeFond),
                                    ContratId = contrat.Id,
                                    ClientId = client.ID,
                                    Date = DateTime.Now.Date,
                                    DateEcheanceFacture = dateContrat,
                                    EtatAvancementId = etatAvancement.ID,

                                    FacturePayee = false,
                                    NumeroFacture = "",
                                    Montant = montantAppelDefond,
                                    Motif = niveauAvancement.LibelleCommercial
                                };

                                facture.NumeroFacture = GenererNumeroFacturesResa(contrat.NumeroContrat, niveauAvancement.Description.ToUpper(), facture.DateEcheanceFacture.Value);
                                contrat.Factures.Add(facture);
                                database.SaveChanges();
                                //if (projetId == 1)
                                //{ 
                                //    if (niveauAvancement.ordre == 1 || niveauAvancement.ordre == 2)
                                //    {
                                //        facture.DateEcheanceFacture = dateContrat;
                                //        facture.Echue = true;
                                //    }
                                //}
                                //else if (projetId == 2)
                                //{
                                if (niveauAvancement.ordre == 1)
                                {
                                    facture.DateEcheanceFacture = dateContrat;
                                    facture.Echue = true;
                                }
                                //}
                                if (etatAvancement.Actif)
                                {
                                    facture.DateEcheanceFacture = dateContrat;
                                    facture.Echue = true;
                                }
                            }
                        }
                        #endregion


                        #region ENREGISTREMENT DES ENCAISSEMENTS DU CLIENT(PREALABLEMENT ENREGISTRES SUR SON COMPTE PROSPECT)

                        foreach (var encaissementProspect in GetEncaissementProspect(client.ID).Where(encProspect => encProspect.FraisDeDossier == false
                                                                                                      && encProspect.MontantGlobal > 0).ToList())
                        {
                            #region CREER L'ENCAISSEMENT GLOBAL
                            var encaissementGlobal = new EncaissementGlobal()
                            {
                                NumeroEncaissement = encaissementProspect.NumeroEncaissement,
                                DateEncaissement = encaissementProspect.DateEncaissement.Value.Date,
                                MontantGlobal = encaissementProspect.MontantGlobal,
                                ContratId = contrat.Id,
                                ModePaiement = encaissementProspect.ModePaiement,
                                Commentaire = encaissementProspect.Commentaire,
                                ReferencePaiement = encaissementProspect.ReferencePaiement

                            };
                            database.EncaissementGlobals.Add(encaissementGlobal);

                            ////Mis à jour des éléments contractuels
                            contrat.MontantVerse += encaissementGlobal.MontantGlobal;
                            #endregion

                            #region ENREGISTRER LA COMMISSION DE L'APPORTEUR D'AFFAIRE SI APPLICABLE
                            if (apporteur != null)
                            {
                                var factureCommission = new FactureCommission
                                {
                                    ContratId = contrat.Id,
                                    EncaissementGlobalId = encaissementGlobal.ID,
                                    Date = encaissementGlobal.DateEncaissement.Value.Date,
                                    MontantAPayer = encaissementGlobal.MontantGlobal * apporteur.TauxCommission / 100,
                                    Motif = "Commission encaissement n° " + encaissementGlobal.NumeroEncaissement + " sur " + encaissementGlobal.ReferencePaiement,
                                    Payee = false
                                };
                                contrat.FactureCommissions.Add(factureCommission);
                            }

                            database.SaveChanges();
                            #endregion

                            #region VERIFICATION DU NIVEAU D'ENCAISSEMENT ET MISES A JOUR NECESSAIRES DU CONTRAT

                            var niveauEncaissement = contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) / contrat.PrixFinal * 100;

                            if (niveauEncaissement >= typeContrat.SeuilEntreeEnVigueur && contrat.AReserve == false)
                            {
                                contrat.AReserve = true;
                                contrat.Lot.StatutLot = StatutLot.Reserve;
                                contrat.Client.Type = TypeClient.Client;
                                contrat.DateReservation = dateContrat;
                                DB.SaveChanges();
                            }
                            #endregion

                            #region VENTILLATION DU PAIEMENT SUR LES FACTURES
                            var montantAVentiller = encaissementGlobal.MontantGlobal;
                            foreach (var fact in contrat.Factures.Where(u => u.FacturePayee == false && u.TypeFacture != TypeFacture.FraisDossier).OrderBy(u => u.EtatAvancement.TypeEtatAvancement.ordre))
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
                                            Date = dateContrat.Date,
                                            ModePaiement = encaissementGlobal.ModePaiement,
                                            Montant = montantAEncaisser,
                                            Commentaire = encaissementGlobal.Commentaire,
                                            ReferencePaiement = encaissementGlobal.ReferencePaiement
                                        };
                                        fact.Encaissements.Add(nouvelEncaissement);

                                        encaissementGlobal.Encaissements.Add(nouvelEncaissement);
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
                            encaissementGlobal.EncaissementLettre = true;
                            //VERIFIER SI LES FACTURES DU CONTRAT SONT TOUTES SOLDEES
                            if (contrat.Factures.Sum(f => f.Montant - f.Encaissements.Sum(enc => enc.Montant)) <= 0)
                            {
                                contrat.ContratSolde = true;
                            }
                            #endregion

                        }
                        #endregion

                        database.SaveChanges();
                    }
                    scope.Complete();
                    return contrat.Id;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int AjouterContratDepotBis(int clientId, int commercialId, int apporteurAffaireId,
            TypeVilla typeVilla, PositionLot position, decimal prixDeVente, decimal tauxRemiseAccordee, decimal MontantRemiseAccordee,
            TypeContrat typeContrat, TypeEcheancier typeEcheancier, DateTime datePremiereEcheance, DateTime dateDerniereEcheance,
           DateTime dateContrat, DateTime dateLivraisonPrevue)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    Contrat contrat = null;

                    using (var ctx = new SenImmoDataContext())
                    {
                        #region RECUPERATION DES DIFFERENTS CONSTITUANTS DU CONTRAT

                        Lot lot = ilotRep.GetLotVirtuel(typeVilla, position);
                        var client = ctx.Clients.Find(clientId);

                        ApporteurAffaire apporteur = null;
                        if (apporteurAffaireId != 0)
                        {
                            apporteur = new ApporteurAffaireRepository().FindById(apporteurAffaireId);
                        }

                        decimal prixDevente = lot.PrixRevise;

                        #endregion

                        #region CALCUL DU MONTANT DE LA REMISE SI ACCORDEE ET DETERMINATION DU PRIX DE VENTE FINAL

                        CalculerRemise(ref prixDevente, ref tauxRemiseAccordee, ref MontantRemiseAccordee);

                        #endregion
                        // VERIFIER SI LE MONTANT DU PREMIER VERSEMENT OU CELUI CUMULE DANS LE COMPTE DU CLIENT A ATTEINT LE SEUIL DE RESERVATION

                        #region CREATION DU CONTRAT

                        // Enregistrer le contrat
                        var dernierNumeroContratDepot = ctx.Parametres.Where(param => param.Nom == "DernierNumeroContratDepot").FirstOrDefault();
                        dernierNumeroContratDepot.valeurInt++;

                        contrat = new Contrat
                        {
                            NumeroContrat = typeVilla.CodeType + "-" + dernierNumeroContratDepot.valeurInt.ToString().PadLeft(5, '0'),
                            ClientID = client.ID,
                            CommercialID = commercialId,
                            DateCreationSysteme = DateTime.Now.Date,
                            DateSouscription = dateContrat,
                            PrixRevise = lot.PrixRevise,
                            PrixFinal = prixDeVente,
                            // PrixStandar = leTypeVillaDepotEnCours.PrixStandard,
                            RemiseAccordee = MontantRemiseAccordee,
                            TypeContratID = typeContrat.ID,
                            TypeEcheancier = typeEcheancier,
                            LotId = lot.ID,
                            DateLivraisonLot = dateLivraisonPrevue,
                            Souscrit = false,
                            AReserve = false,
                            AttribuerLot = false,
                            LotAttribue = false,
                            Statut = StatutContrat.Actif
                        };

                        if (apporteur != null)
                        {
                            contrat.ApporteurID = apporteur.Id;
                            contrat.CommissionApporteur = prixDeVente * apporteur.TauxCommission / 100;
                        }

                        ctx.Contrats.Add(contrat);

                        #endregion

                        #region ENREGISTREMENT FACTURE DEPOT MINIMAL

                        var montantDepotDeGarantie = prixDevente * typeContrat.SeuilSouscription / 100;


                        Facture factureDepotDeGarantie = new Facture
                        {
                            TypeFacture = TypeFacture.DepotMinimum,
                            ClientId = client.ID,
                            Date = DateTime.Now.Date,
                            DateEcheanceFacture = dateContrat,
                            Motif = "Facture Dépot minimun de " + typeContrat.SeuilSouscription + "%",
                            FacturePayee = false,

                        };
                        factureDepotDeGarantie.Montant = montantDepotDeGarantie;

                        factureDepotDeGarantie.NumeroFacture = "DG" + contrat.NumeroContrat.ToString().PadLeft(5, '0') + typeEcheancier.ToString().Substring(0, 2).ToUpper()
                                                               + dateContrat.Month.ToString().PadLeft(2, '0') + dateContrat.Year.ToString().Substring(2, 2);
                        contrat.Factures.Add(factureDepotDeGarantie);

                        #endregion

                        #region CALCUL DES ECHEANCES
                        var montantRestantADispacher = prixDevente - montantDepotDeGarantie;
                        var nbEcheances = 0;
                        // Calculer le nombre d'échéances
                        nbEcheances = CalculNombreEcheances(typeEcheancier, datePremiereEcheance, dateDerniereEcheance, ref nbEcheances);
                        contrat.NbEcheances = nbEcheances;

                        //model.DateFinPrevue = derniereEcheance;
                        var montantEcheance = (int)montantRestantADispacher / nbEcheances;
                        contrat.MontantEcheance = montantEcheance;
                        var montantDerniereEcheance = (int)(montantRestantADispacher - montantEcheance * nbEcheances);
                        if (montantDerniereEcheance > 0)
                            nbEcheances++;
                        #endregion

                        #region GENERATION DES ECHEANCES

                        for (int i = 0; i < nbEcheances; i++)
                        {
                            Facture facture = new Facture
                            {
                                TypeFacture = TypeFacture.Echeance,
                                ClientId = client.ID,
                                Date = dateContrat.Date,
                                //DateEcheanceFacture = dateContrat,
                                FacturePayee = false,

                            };

                            if (i == nbEcheances - 2 && montantDerniereEcheance > 0)
                            {
                                facture.Montant = montantDerniereEcheance;
                                derniereEchance = true;
                            }
                            else
                            {
                                facture.Montant = montantEcheance;
                            }

                            switch (typeEcheancier)
                            {
                                case TypeEcheancier.Mensuel:
                                    facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 1);
                                    facture.Motif = "Echéance Mensuelle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                                    break;
                                case TypeEcheancier.Trimestriel:
                                    facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 3);
                                    facture.Motif = "Echéance trimestrielle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                                    break;
                                case TypeEcheancier.Semestriel:
                                    facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 6);
                                    facture.Motif = "Echéance semestrielle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                                    break;
                                case TypeEcheancier.Annuel:
                                    facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 12);
                                    facture.Motif = "Echéance annuelle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                                    break;
                                default:
                                    break;
                            }
                            facture.NumeroFacture = GenererNumeroFacturesDepot(contrat.NumeroContrat, typeEcheancier, facture.DateEcheanceFacture.Value);
                            contrat.Factures.Add(facture);
                            if (derniereEchance)
                            {
                                break;
                            }

                        }
                        ctx.SaveChanges();
                        #endregion

                        #region ENREGISTREMENT DES ENCAISSEMENTS DU CLIENT(PREALABLEMENT ENREGISTRES SUR SON COMPTE PROSPECT)

                        foreach (var encaissementProspect in GetEncaissementProspect(client.ID).Where(encProspect => encProspect.FraisDeDossier == false).ToList())
                        {
                            #region CREER L'ENCAISSEMENT GLOBAL
                            var encaissementGlobal = new EncaissementGlobal()
                            {
                                NumeroEncaissement = encaissementProspect.NumeroEncaissement,
                                DateEncaissement = encaissementProspect.DateEncaissement.Value.Date,
                                MontantGlobal = encaissementProspect.MontantGlobal,
                                ContratId = contrat.Id,
                                ModePaiement = encaissementProspect.ModePaiement,
                                Commentaire = encaissementProspect.Commentaire,
                                ReferencePaiement = encaissementProspect.ReferencePaiement

                            };
                            ctx.EncaissementGlobals.Add(encaissementGlobal);

                            ////Mis à jour des éléments contractuels
                            contrat.MontantVerse += encaissementGlobal.MontantGlobal;
                            #endregion

                            #region ENCAISSEMENT DU DEPOT MINIMUM
                            decimal montantAVentiller = 0;
                            var factureDepotMinimum = contrat.Factures.Where(u => u.TypeFacture == TypeFacture.DepotMinimum).FirstOrDefault();
                            if (factureDepotMinimum.FacturePayee == false)
                            {
                                montantAVentiller = encaissementProspect.MontantGlobal;

                                decimal montantDepotDeGarantieRestant = factureDepotMinimum.Montant - factureDepotMinimum.MontantEncaisse; ;

                                decimal montantDepotDeGarantieAEncaisser = 0;

                                if (montantAVentiller >= montantDepotDeGarantieRestant)
                                {
                                    montantDepotDeGarantieAEncaisser = montantDepotDeGarantieRestant;
                                }
                                else
                                {
                                    montantDepotDeGarantieAEncaisser = montantAVentiller;
                                }


                                Encaissement encaissementDepotDeGarantie = new Encaissement
                                {
                                    Date = encaissementGlobal.DateEncaissement,
                                    ModePaiement = encaissementGlobal.ModePaiement,
                                    Montant = montantDepotDeGarantieAEncaisser,
                                    Commentaire = "Encaissement du dépot minimun de " + typeContrat.SeuilSouscription + "%",
                                    ReferencePaiement = encaissementGlobal.ReferencePaiement,

                                };
                                factureDepotDeGarantie.Encaissements.Add(encaissementDepotDeGarantie);
                                factureDepotDeGarantie.Echue = true;
                                if (montantAVentiller >= montantDepotDeGarantieRestant)
                                {
                                    factureDepotDeGarantie.FacturePayee = true;
                                    factureDepotDeGarantie.Active = true;

                                }
                                encaissementGlobal.Encaissements.Add(encaissementDepotDeGarantie);
                                //Ventiller le versement aprés avoir extrait le dépot de garantie
                                montantAVentiller = montantAVentiller - montantDepotDeGarantieAEncaisser;
                            }
                            #endregion

                            #region ENREGISTRER LA COMMISSION DE L'APPORTEUR D'AFFAIRE SI APPLICABLE
                            if (apporteur != null)
                            {
                                var factureCommission = new FactureCommission
                                {
                                    ContratId = contrat.Id,
                                    EncaissementGlobalId = encaissementGlobal.ID,
                                    Date = encaissementGlobal.DateEncaissement.Value.Date,
                                    MontantAPayer = encaissementGlobal.MontantGlobal * apporteur.TauxCommission / 100,
                                    Motif = "Commission encaissement n° " + encaissementGlobal.NumeroEncaissement + " sur " + encaissementGlobal.ReferencePaiement,
                                    Payee = false
                                };
                                contrat.FactureCommissions.Add(factureCommission);
                            }

                            ctx.SaveChanges();
                            #endregion

                            #region VERIFICATION DU NIVEAU D'ENCAISSEMENT ET MISES A JOUR NECESSAIRES DU CONTRAT

                            var niveauEncaissement = contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) / contrat.PrixFinal * 100;

                            if (niveauEncaissement >= typeContrat.SeuilEntreeEnVigueur && contrat.AReserve == false)
                            {
                                contrat.AReserve = true;
                                //contrat.Lot.StatutLot = StatutLot.Reserve;
                                contrat.AttribuerLot = true;
                                contrat.Client.Type = TypeClient.Client;
                                contrat.DateReservation = dateContrat;
                            }
                            else if (niveauEncaissement >= typeContrat.SeuilSouscription && contrat.Souscrit == false)
                            {
                                contrat.Souscrit = true;
                                //contrat.Lot.StatutLot = StatutLot.ReservationEnCours;
                                contrat.AttribuerLot = false;
                                contrat.Client.Type = TypeClient.ClientEnCours;
                                contrat.DateSouscription = dateContrat;
                            }
                            #endregion

                            #region VENTILLATION DU PAIEMENT SUR LES FACTURES

                            foreach (var fact in contrat.Factures.Where(u => u.FacturePayee == false && u.TypeFacture == TypeFacture.Echeance).OrderBy(u => u.DateEcheanceFacture))
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
                                            Date = dateContrat.Date,
                                            ModePaiement = encaissementGlobal.ModePaiement,
                                            Montant = montantAEncaisser,
                                            Commentaire = encaissementGlobal.Commentaire,
                                            ReferencePaiement = encaissementGlobal.ReferencePaiement
                                        };
                                        fact.Encaissements.Add(nouvelEncaissement);

                                        encaissementGlobal.Encaissements.Add(nouvelEncaissement);
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
                            //VERIFIER SI LES FACTURES DU CONTRAT SONT TOUTES SOLDEES
                            if (contrat.Factures.Sum(f => f.Montant - f.Encaissements.Sum(enc => enc.Montant)) <= 0)
                            {
                                contrat.ContratSolde = true;
                            }

                            ctx.SaveChanges();
                            #endregion

                        }
                        #endregion

                        ctx.SaveChanges();
                    }
                    scope.Complete();
                    return contrat.Id;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        public int AjouterContratDepotTer(int clientId, int commercialId, int apporteurAffaireId,
            TypeVilla typeVilla, PositionLot position, decimal prixDeVente, decimal tauxRemiseAccordee, decimal MontantRemiseAccordee,
            TypeContrat typeContrat, TypeEcheancier typeEcheancier, DateTime datePremiereEcheance, DateTime dateDerniereEcheance,
           DateTime dateContrat, DateTime dateLivraisonPrevue, decimal montantDepotMinimum, decimal montantEcheance,
           int nbEcheances, decimal montantDerniereEcheance, int dureeDepot)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    Contrat contrat = null;

                    using (var ctx = new SenImmoDataContext())
                    {
                        #region RECUPERATION DES DIFFERENTS CONSTITUANTS DU CONTRAT

                        Lot lot = ilotRep.GetLotVirtuel(typeVilla, position);
                        var client = ctx.Clients.Find(clientId);

                        ApporteurAffaire apporteur = null;
                        if (apporteurAffaireId != 0)
                        {
                            apporteur = new ApporteurAffaireRepository().FindById(apporteurAffaireId);
                        }

                        //decimal prixDevente = lot.PrixRevise;

                        #endregion

                        //#region CALCUL DU MONTANT DE LA REMISE SI ACCORDEE ET DETERMINATION DU PRIX DE VENTE FINAL
                        //prixDeVente += MontantRemiseAccordee;

                        //CalculerRemise(ref prixDeVente, ref tauxRemiseAccordee, ref MontantRemiseAccordee);

                        //#endregion
                        // VERIFIER SI LE MONTANT DU PREMIER VERSEMENT OU CELUI CUMULE DANS LE COMPTE DU CLIENT A ATTEINT LE SEUIL DE RESERVATION

                        #region CREATION DU CONTRAT

                        // Enregistrer le contrat
                        var dernierNumeroContratDepot = ctx.Parametres.Where(param => param.Nom == "DernierNumeroContratDepot").FirstOrDefault();
                        dernierNumeroContratDepot.valeurInt++;

                        contrat = new Contrat
                        {
                            NumeroContrat = typeVilla.CodeType + "-" + dernierNumeroContratDepot.valeurInt.ToString().PadLeft(5, '0'),
                            ClientID = client.ID,
                            CommercialID = commercialId,
                            DateCreationSysteme = DateTime.Now.Date,
                            DateSouscription = dateContrat,
                            PrixRevise = lot.PrixRevise,
                            PrixFinal = prixDeVente,
                            // PrixStandar = leTypeVillaDepotEnCours.PrixStandard,
                            RemiseAccordee = MontantRemiseAccordee,
                            TypeContratID = typeContrat.ID,
                            TypeEcheancier = typeEcheancier,
                            LotId = lot.ID,
                            DateLivraisonLot = dateLivraisonPrevue,
                            Souscrit = false,
                            AReserve = false,
                            AttribuerLot = false,
                            LotAttribue = false,
                            Statut = StatutContrat.Actif,
                            DureeDepot = dureeDepot

                        };

                        if (apporteur != null)
                        {
                            contrat.ApporteurID = apporteur.Id;
                            contrat.CommissionApporteur = prixDeVente * apporteur.TauxCommission / 100;
                        }


                        ctx.Contrats.Add(contrat);



                        #endregion

                        #region ENREGISTREMENT FACTURE DEPOT MINIMAL

                        //var montantDepotDeGarantie = prixDevente * typeContrat.SeuilSouscription / 100;


                        Facture factureDepotDeGarantie = new Facture
                        {
                            TypeFacture = TypeFacture.DepotMinimum,
                            ClientId = client.ID,
                            Date = DateTime.Now.Date,
                            DateEcheanceFacture = datePremiereEcheance,
                            Motif = "Facture Dépot minimun de " + typeContrat.SeuilSouscription + "%",
                            FacturePayee = false,

                        };
                        factureDepotDeGarantie.Montant = montantDepotMinimum;

                        factureDepotDeGarantie.NumeroFacture = "DG" + contrat.NumeroContrat.ToString().PadLeft(5, '0') + typeEcheancier.ToString().Substring(0, 2).ToUpper()
                                                               + dateContrat.Month.ToString().PadLeft(2, '0') + dateContrat.Year.ToString().Substring(2, 2);
                        contrat.Factures.Add(factureDepotDeGarantie);

                        #endregion

                        #region CALCUL DES ECHEANCES
                        var montantRestantADispacher = prixDeVente - montantDepotMinimum;
                        //var nbEcheances = 0;
                        // Calculer le nombre d'échéances
                        //nbEcheances = CalculNombreEcheances(typeEcheancier, datePremiereEcheance, dateDerniereEcheance, ref nbEcheances);
                        contrat.NbEcheances = nbEcheances;

                        //model.DateFinPrevue = derniereEcheance;
                        //var montantEcheance = (int)montantRestantADispacher / nbEcheances;
                        contrat.MontantEcheance = montantEcheance;
                        //var montantDerniereEcheance = (int)(montantRestantADispacher - montantEcheance * nbEcheances);
                        if (montantDerniereEcheance > 0)
                            nbEcheances++;
                        #endregion

                        #region GENERATION DES ECHEANCES

                        for (int i = 0; i < nbEcheances; i++)
                        {
                            Facture facture = new Facture
                            {
                                TypeFacture = TypeFacture.Echeance,
                                ClientId = client.ID,
                                Date = dateContrat.Date,
                                //DateEcheanceFacture = dateContrat,
                                FacturePayee = false,

                            };

                            if (i == nbEcheances - 2)
                            {
                                facture.Montant = montantEcheance + montantDerniereEcheance;
                                derniereEchance = true;
                            }
                            else
                            {
                                facture.Montant = montantEcheance;
                            }

                            switch (typeEcheancier)
                            {
                                case TypeEcheancier.Mensuel:
                                    facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 1);
                                    facture.Motif = "Echéance mensuelle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                                    break;
                                case TypeEcheancier.Trimestriel:
                                    facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 3);
                                    facture.Motif = "Echéance trimestrielle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                                    break;
                                case TypeEcheancier.Semestriel:
                                    facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 6);
                                    facture.Motif = "Echéance semestrielle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                                    break;
                                case TypeEcheancier.Annuel:
                                    facture.DateEcheanceFacture = datePremiereEcheance.AddMonths(i * 12);
                                    facture.Motif = "Echéance annuelle de " + String.Format("{0:y}", facture.DateEcheanceFacture.Value);
                                    break;
                                default:
                                    break;
                            }
                            facture.NumeroFacture = GenererNumeroFacturesDepot(contrat.NumeroContrat, typeEcheancier, facture.DateEcheanceFacture.Value);
                            contrat.Factures.Add(facture);
                            if (derniereEchance)
                            {
                                break;
                            }

                        }
                        ctx.SaveChanges();
                        #endregion

                        #region ENREGISTREMENT DES ENCAISSEMENTS DU CLIENT(PREALABLEMENT ENREGISTRES SUR SON COMPTE PROSPECT)

                        foreach (var encaissementProspect in GetEncaissementProspect(client.ID).Where(encProspect => encProspect.FraisDeDossier == false && encProspect.MontantGlobal > 0).ToList())
                        {
                            #region CREER L'ENCAISSEMENT GLOBAL
                            var encaissementGlobal = new EncaissementGlobal()
                            {
                                NumeroEncaissement = encaissementProspect.NumeroEncaissement,
                                DateEncaissement = encaissementProspect.DateEncaissement.Value.Date,
                                MontantGlobal = encaissementProspect.MontantGlobal,
                                ContratId = contrat.Id,
                                ModePaiement = encaissementProspect.ModePaiement,
                                Commentaire = encaissementProspect.Commentaire,
                                ReferencePaiement = encaissementProspect.ReferencePaiement

                            };
                            ctx.EncaissementGlobals.Add(encaissementGlobal);

                            ////Mis à jour des éléments contractuels
                            contrat.MontantVerse += encaissementGlobal.MontantGlobal;
                            #endregion

                            #region ENCAISSEMENT DU DEPOT MINIMUM
                            decimal montantAVentiller = encaissementGlobal.MontantGlobal;
                            var factureDepotMinimum = contrat.Factures.Where(u => u.TypeFacture == TypeFacture.DepotMinimum).FirstOrDefault();
                            if (factureDepotMinimum.FacturePayee == false)
                            {
                                // montantAVentiller = encaissementProspect.MontantGlobal;

                                decimal montantDepotDeGarantieRestant = factureDepotMinimum.Montant - factureDepotMinimum.MontantEncaisse;

                                decimal montantDepotDeGarantieAEncaisser = 0;

                                if (montantAVentiller >= montantDepotDeGarantieRestant)
                                {
                                    montantDepotDeGarantieAEncaisser = montantDepotDeGarantieRestant;
                                }
                                else
                                {
                                    montantDepotDeGarantieAEncaisser = montantAVentiller;
                                }


                                Encaissement encaissementDepotDeGarantie = new Encaissement
                                {
                                    Date = encaissementGlobal.DateEncaissement,
                                    ModePaiement = encaissementGlobal.ModePaiement,
                                    Montant = montantDepotDeGarantieAEncaisser,
                                    Commentaire = "Encaissement du dépot minimun de " + typeContrat.SeuilSouscription + "%",
                                    ReferencePaiement = encaissementGlobal.ReferencePaiement,

                                };
                                factureDepotDeGarantie.Encaissements.Add(encaissementDepotDeGarantie);
                                factureDepotDeGarantie.Echue = true;
                                if (montantAVentiller >= montantDepotDeGarantieRestant)
                                {
                                    factureDepotDeGarantie.FacturePayee = true;
                                    factureDepotDeGarantie.Active = true;

                                }
                                encaissementGlobal.Encaissements.Add(encaissementDepotDeGarantie);
                                //Ventiller le versement aprés avoir extrait le dépot de garantie
                                montantAVentiller = montantAVentiller - montantDepotDeGarantieAEncaisser;
                            }
                            #endregion

                            #region ENREGISTRER LA COMMISSION DE L'APPORTEUR D'AFFAIRE SI APPLICABLE
                            if (apporteur != null)
                            {
                                var factureCommission = new FactureCommission
                                {
                                    ContratId = contrat.Id,
                                    EncaissementGlobalId = encaissementGlobal.ID,
                                    Date = encaissementGlobal.DateEncaissement.Value.Date,
                                    MontantAPayer = encaissementGlobal.MontantGlobal * apporteur.TauxCommission / 100,
                                    Motif = "Commission encaissement n° " + encaissementGlobal.NumeroEncaissement + " sur " + encaissementGlobal.ReferencePaiement,
                                    Payee = false
                                };
                                contrat.FactureCommissions.Add(factureCommission);
                            }

                            ctx.SaveChanges();
                            #endregion

                            #region VERIFICATION DU NIVEAU D'ENCAISSEMENT ET MISES A JOUR NECESSAIRES DU CONTRAT

                            var niveauEncaissement = contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) / contrat.PrixFinal * 100;

                            if (niveauEncaissement >= typeContrat.SeuilEntreeEnVigueur && contrat.AReserve == false)
                            {
                                contrat.AReserve = true;
                                //contrat.Lot.StatutLot = StatutLot.Reserve;
                                contrat.AttribuerLot = true;
                                contrat.Client.Type = TypeClient.Client;
                                contrat.DateReservation = dateContrat;
                            }
                            else if (niveauEncaissement >= typeContrat.SeuilSouscription && contrat.Souscrit == false)
                            {
                                contrat.Souscrit = true;
                                //contrat.Lot.StatutLot = StatutLot.ReservationEnCours;
                                contrat.AttribuerLot = false;
                                contrat.Client.Type = TypeClient.ClientEnCours;
                                contrat.DateSouscription = dateContrat;
                            }
                            #endregion

                            #region VENTILLATION DU PAIEMENT SUR LES FACTURES

                            foreach (var fact in contrat.Factures.Where(u => u.FacturePayee == false && u.TypeFacture == TypeFacture.Echeance).OrderBy(u => u.DateEcheanceFacture))
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
                                            Date = dateContrat.Date,
                                            ModePaiement = encaissementGlobal.ModePaiement,
                                            Montant = montantAEncaisser,
                                            Commentaire = encaissementGlobal.Commentaire,
                                            ReferencePaiement = encaissementGlobal.ReferencePaiement
                                        };
                                        fact.Encaissements.Add(nouvelEncaissement);

                                        encaissementGlobal.Encaissements.Add(nouvelEncaissement);
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
                            //VERIFIER SI LES FACTURES DU CONTRAT SONT TOUTES SOLDEES
                            if (contrat.Factures.Sum(f => f.Montant - f.Encaissements.Sum(enc => enc.Montant)) <= 0)
                            {
                                contrat.ContratSolde = true;
                            }

                            ctx.SaveChanges();
                            #endregion

                        }
                        #endregion

                        ctx.SaveChanges();
                    }
                    scope.Complete();
                    return contrat.Id;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal void UpdateEncaissement(int encaissementId, DateTime dateEncaissement, ModePaiement modePaiement, string reference, string commentaire, string numeroPiece)
        {
            try
            {
                var encaissement = DB.EncaissementGlobals.Find(encaissementId);
                encaissement.DateEncaissement = dateEncaissement;
                encaissement.ModePaiement = modePaiement;
                encaissement.ReferencePaiement = reference;
                encaissement.Commentaire = commentaire;
                encaissement.NumeroEncaissement = numeroPiece;
                DB.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void UpdateEncaissementProspect(int encaissementId, DateTime dateEncaissement, ModePaiement modePaiement, string reference, string commentaire, string numeroPiece)
        {
            try
            {
                var encaissement = DB.EncaissementProspects.Find(encaissementId);
                encaissement.DateEncaissement = dateEncaissement;
                encaissement.ModePaiement = modePaiement;
                encaissement.ReferencePaiement = reference;
                encaissement.Commentaire = commentaire;
                encaissement.NumeroEncaissement = numeroPiece;
                DB.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int EnregistrerVersementBis(int lotId, int clientId, DateTime dateVersement, Decimal montantVersement, int contratId,
            ModePaiement modePaiement, string referencePaiement, string commentaire)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    using (var context = new SenImmoDataContext())
                    {
                        var lot = context.Lots.Find(lotId);
                        var client = context.Clients.Find(clientId);
                        var contrat = context.Contrats.Find(contratId);


                        #region CREER L'ENCAISSEMENT GLOBAL
                        var encaissementGlobal = new EncaissementGlobal()
                        {
                            NumeroEncaissement = "EN" + contrat.NumeroContrat.ToString().PadLeft(5, '0') + dateVersement.Month.ToString().PadLeft(2, '0') + dateVersement.Year.ToString().Substring(2, 2),
                            DateEncaissement = dateVersement,
                            MontantGlobal = montantVersement,
                            ContratId = contrat.Id,
                            ModePaiement = modePaiement,
                            Commentaire = commentaire,
                            ReferencePaiement = referencePaiement

                        };
                        context.EncaissementGlobals.Add(encaissementGlobal);

                        ////Mis à jour des éléments contractuels
                        contrat.MontantVerse += encaissementGlobal.MontantGlobal;
                        #endregion

                        #region ENREGISTRER LA COMMISSION DE L'APPORTEUR D'AFFAIRE SI APPLICABLE

                        if (contrat.Apporteur != null)
                        {
                            var factureCommission = new FactureCommission
                            {
                                ContratId = contrat.Id,
                                EncaissementGlobalId = encaissementGlobal.ID,
                                Date = encaissementGlobal.DateEncaissement.Value.Date,
                                MontantAPayer = encaissementGlobal.MontantGlobal * contrat.Apporteur.TauxCommission / 100,
                                Motif = "Commission encaissement n° " + encaissementGlobal.NumeroEncaissement + " sur " + encaissementGlobal.ReferencePaiement,
                                Payee = false
                            };
                            contrat.FactureCommissions.Add(factureCommission);
                        }

                        context.SaveChanges();
                        #endregion

                        #region VERIFICATION DU NIVEAU D'ENCAISSEMENT ET MISES A JOUR NECESSAIRES DU CONTRAT ET RECUPERATION DES FACTURES EN INSTANCE DE R7GLEMENT

                        var niveauEncaissement = contrat.EncaissementGlobals.Where(enc => enc.NumeroEncaissement.Substring(0, 4) != "ENFD").Sum(enc => enc.MontantGlobal) / contrat.PrixFinal * 100;
                        if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                        {
                            if (niveauEncaissement >= contrat.TypeContrat.SeuilEntreeEnVigueur && contrat.AReserve == false)
                            {
                                contrat.AReserve = true;
                                contrat.Lot.StatutLot = StatutLot.Reserve;
                                contrat.Client.Type = TypeClient.Client;
                                contrat.DateReservation = dateVersement;
                                context.SaveChanges();
                            }

                            lesFactures = contrat.Factures.Where(af => af.FacturePayee == false && af.TypeFacture != TypeFacture.FraisDossier).OrderBy(af => af.EtatAvancement.TypeEtatAvancement.ordre);
                        }
                        else
                        {

                            if (niveauEncaissement >= contrat.TypeContrat.SeuilEntreeEnVigueur && contrat.AReserve == false)
                            {
                                contrat.AReserve = true;
                                //contrat.Lot.StatutLot = StatutLot.Reserve;
                                contrat.AttribuerLot = true;
                                contrat.Client.Type = TypeClient.Client;
                                contrat.DateReservation = dateVersement;
                                Tools.Tools.EmailSend(contrat.Commercial.Email, "", "Génération du Contrat de réservation",
                               @" Bonjour " + "\n\n Le prospect " + contrat.Client.NomComplet + " a atteint le seuil d'encaissement minimum pour bénéficier d'un lot \n\n Cordialement");
                            }
                            else if (niveauEncaissement >= contrat.TypeContrat.SeuilSouscription && contrat.Souscrit == false)
                            {
                                contrat.Souscrit = true;
                                //contrat.Lot.StatutLot = StatutLot.ReservationEnCours;
                                contrat.AttribuerLot = false;
                                contrat.Client.Type = TypeClient.ClientEnCours;
                                contrat.DateSouscription = dateVersement;
                               
                            }
                            lesFactures = contrat.Factures.Where(ech => ech.FacturePayee == false && ech.TypeFacture != TypeFacture.FraisDossier).OrderBy(ech => ech.DateEcheanceFacture);
                        }

                        #endregion

                        #region VENTILLATION DU PAIEMENT SUR LES FACTURES
                        var montantAVentiller = encaissementGlobal.MontantGlobal;

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
                                        Date = dateVersement.Date,
                                        ModePaiement = encaissementGlobal.ModePaiement,
                                        Montant = montantAEncaisser,
                                        Commentaire = encaissementGlobal.Commentaire,
                                        ReferencePaiement = encaissementGlobal.ReferencePaiement
                                    };
                                    fact.Encaissements.Add(nouvelEncaissement);

                                    encaissementGlobal.Encaissements.Add(nouvelEncaissement);
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
                        encaissementGlobal.EncaissementLettre = true;
                        //VERIFIER SI LES FACTURES DU CONTRAT SONT TOUTES SOLDEES
                        if (contrat.Factures.Sum(f => f.Montant - f.Encaissements.Sum(enc => enc.Montant)) <= 0)
                        {
                            contrat.ContratSolde = true;
                        }
                        #endregion
                        context.SaveChanges();
                        scope.Complete();
                        return encaissementGlobal.ID;

                        //if (montantAVentiller > 0)
                        //{
                        //    throw new Exception("Montant restant: " + montantAVentiller.ToString());
                        //}

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }


        }


        internal void AjouterNote(int contratId, string Note)
        {
            try
            {
                DB.Contrats.Find(contratId).NoteContrats.Add(new NoteContrat()
                {
                    DateNote = DateTime.Now.Date,
                    Note = Note

                });
                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void ModifierNote(int idNote, string texte)
        {
            try
            {
                var note = DB.NoteContrats.Find(idNote);
                note.Note = texte;
                DB.SaveChanges();
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
                DB.NoteContrats.Remove(DB.NoteContrats.Find(idNote));
                DB.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<NoteContrat> GetNotesContrat(int contratId)
        {
            try
            {
                DB.Dispose();
                DB = new SenImmoDataContext();
                return DB.NoteContrats.Where(note => note.ContratId == contratId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LettrerEncaissement(int encaissementId, int contratId)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    using (var context = new SenImmoDataContext())
                    {
                        var contrat = context.Contrats.Find(contratId);
                        var encaissementGlobal = context.EncaissementGlobals.Find(encaissementId);
                        if (contrat == null || encaissementGlobal == null) throw new Exception("Contrat ou encaissement introuvable");
                        #region
                        if (contrat.TypeContrat.CategorieContrat == CategorieContrat.Réservation)
                        {
                            lesFactures = contrat.Factures.Where(af => af.FacturePayee == false && af.TypeFacture != TypeFacture.FraisDossier).OrderBy(af => af.EtatAvancement.TypeEtatAvancement.ordre);
                        }
                        else
                        {
                            lesFactures = contrat.Factures.Where(ech => ech.FacturePayee == false && ech.TypeFacture != TypeFacture.FraisDossier).OrderBy(ech => ech.DateEcheanceFacture);
                        }

                        #endregion

                        #region VENTILLATION DU PAIEMENT SUR LES FACTURES
                        var montantAVentiller = encaissementGlobal.MontantGlobal;

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
                                        Date = encaissementGlobal.DateEncaissement,
                                        ModePaiement = encaissementGlobal.ModePaiement,
                                        Montant = montantAEncaisser,
                                        Commentaire = encaissementGlobal.Commentaire,
                                        ReferencePaiement = encaissementGlobal.ReferencePaiement
                                    };
                                    fact.Encaissements.Add(nouvelEncaissement);

                                    encaissementGlobal.Encaissements.Add(nouvelEncaissement);
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

                            context.SaveChanges();
                        }
                        encaissementGlobal.EncaissementLettre = true;
                        //VERIFIER SI LES FACTURES DU CONTRAT SONT TOUTES SOLDEES
                        if (contrat.Factures.Sum(f => f.Montant - f.Encaissements.Sum(enc => enc.Montant)) <= 0)
                        {
                            contrat.ContratSolde = true;
                        }
                        #endregion
                        context.SaveChanges();
                        scope.Complete();

                        //if (montantAVentiller > 0)
                        //{
                        //    throw new Exception("Montant restant: " + montantAVentiller.ToString());
                        //}

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }




















    public class MouvementComptable
    {
        public int Id { get; set; }
        public DateTime DateOp { get; set; }
        public string NumeroPiece { get; set; }
        public string  LibelleOp { get; set; }
        public string TypeOp { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Solde { get; set; }
    }
}

