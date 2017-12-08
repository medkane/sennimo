using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class SenImmoDataContext : DbContext
    {
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Commercial> Commercials { get; set; }
        public DbSet<AppelDeFond> AppelDeFonds { get; set; }
        public DbSet<ApporteurAffaire> ApporteurAffaires { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<TypeOrigine> TypeOrigines { get; set; }
        public DbSet<Contrat> Contrats { get; set; }
        public DbSet<ActiviteCommerciale> ActiviteCommerciales { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Encaissement> Encaissements { get; set; }
        public DbSet<EncaissementGlobal> EncaissementGlobals { get; set; }
        public DbSet<EtatAvancement> EtatAvancements { get; set; }
        public DbSet<Ilot> Ilots { get; set; }
        public DbSet<TypeContrat> TypeContrats { get; set; }
        public DbSet<TypeEtatAvancement> TypeEtatAvancements { get; set; }
        public DbSet<TypeVilla> TypeVillas { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Commission> Commissions { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Versement> Versements { get; set; }
        public DbSet<Parametre> Parametres { get; set; }
        public DbSet<FactureCommission> FactureCommissions { get; set; }
        public DbSet<FactureCommissionGlobale> FactureCommissionsGlobales { get; set; }
        public DbSet<PaiementCommission> PaiementCommissions { get; set; }
        public DbSet<PaiementCommissionGlobal> PaiementCommissionGlobals { get; set; }
       public  DbSet<SoldeDeToutCompte> SoldeDeToutComptes { get; set; }
        public DbSet<FactureProspect> FactureProspects { get; set; }
        public DbSet<EncaissementProspect> EncaissementProspects { get; set; }
        public DbSet<Remboursement> Remboursements { get; set; }
        public DbSet<Cooperative> Cooperatives { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<SousMenu> SousMenus { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ObjectifAnnuel> ObjectifAnnuels { get; set; }
        public DbSet<ObjectifTrimestriel> ObjectifTrimestriels { get; set; }
        public DbSet<ObjectifMensuel> ObjectifMensuels { get; set; }
        public DbSet<TauxAtteinte> TauxAtteintes { get; set; }
        public DbSet<ImportEtatAvancement> ImportEtatAvancements { get; set; }
        public DbSet<ImportEncaissement> ImportEncaissements { get; set; }
        public DbSet<ImportCompteTiers> ImportCompteTiers { get; set; }
        public DbSet<GenerationEcheance> GenerationEcheances { get; set; }
        public DbSet<NoteProspect> NotesProspects { get; set; }
        public DbSet<TypeStatutProspect> TypeStatutProspects { get; set; }
        public DbSet<StatutProspect> StatutProspects { get; set; }
        public DbSet<ClientResa> ClientResas { get; set; }
        public DbSet<ImportEncaissementSaari> ImportEncaissementSaaris { get; set; }
        public DbSet<ClientDepot> ClientDepots { get; set; }
        public DbSet<NoteContrat> NoteContrats { get; set; }
        public DbSet<Projet> Projets { get; set; }
        public DbSet<TypeImmeuble> TypeImmeubles { get; set; }
        public DbSet<Country> Countries { get; set; }

        public SenImmoDataContext(): base() 
        {
                Database.SetInitializer<SenImmoDataContext>(new CreateDatabaseIfNotExists<SenImmoDataContext>());

            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
        }
        }
    }
