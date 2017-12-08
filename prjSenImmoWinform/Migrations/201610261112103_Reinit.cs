namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActiviteCommerciales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateActivite = c.DateTime(nullable: false),
                        HeureActivite = c.Time(nullable: false, precision: 7),
                        MyProperty = c.Int(),
                        DateFinActivite = c.DateTime(),
                        TypeActivite = c.Int(nullable: false),
                        AutreTypeActivite = c.String(),
                        Commentaire = c.String(),
                        ClientID = c.Int(nullable: false),
                        CommercialID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Agents", t => t.CommercialID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.CommercialID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Genre = c.Int(nullable: false),
                        DateDeNaissance = c.DateTime(),
                        LieuDeNaissance = c.String(),
                        Nationalite = c.String(),
                        Profession = c.String(),
                        TypePieceIdentite = c.Int(),
                        NumeroPieceIdentification = c.String(),
                        DateDeDelivrancePiece = c.DateTime(),
                        Adresse = c.String(),
                        Ville = c.String(),
                        Pays = c.String(),
                        Email = c.String(),
                        Mobile1 = c.String(),
                        Mobile2 = c.String(),
                        TelephoneFixe = c.String(),
                        TelephoneBureau = c.String(),
                        Fax = c.String(),
                        DateCreation = c.DateTime(nullable: false),
                        Actif = c.Boolean(nullable: false),
                        NotesClient = c.String(),
                        Type = c.Int(nullable: false),
                        DateAffectationCommercial = c.DateTime(),
                        Origine = c.Int(nullable: false),
                        AutreOrigine = c.String(),
                        DateMariage = c.DateTime(),
                        LieuDeMariage = c.String(),
                        DateContratMariage = c.DateTime(),
                        SituationMatrimoniale = c.Int(nullable: false),
                        NomConjoint = c.String(),
                        PrenomConjoint = c.String(),
                        DateDeNaissanceConjoint = c.DateTime(),
                        LieuDeNaissanceConjoint = c.String(),
                        NationaliteConjoint = c.String(),
                        ProfessionConjoint = c.String(),
                        RegimeMatrimoniale = c.Int(nullable: false),
                        PrenomNotaire = c.String(),
                        NomNotaire = c.String(),
                        AdresseNotaire = c.String(),
                        CommercialID = c.Int(),
                        CooperativeId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agents", t => t.CommercialID)
                .ForeignKey("dbo.Cooperatives", t => t.CooperativeId)
                .Index(t => t.CommercialID)
                .Index(t => t.CooperativeId);
            
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Matricule = c.String(),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Adresse = c.String(),
                        Mobile1 = c.String(),
                        Mobile2 = c.String(),
                        Email = c.String(),
                        RoleId = c.Int(nullable: false),
                        ResponsableId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Agents", t => t.ResponsableId)
                .Index(t => t.RoleId)
                .Index(t => t.ResponsableId);
            
            CreateTable(
                "dbo.Contrats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroContrat = c.String(),
                        Statut = c.Int(nullable: false),
                        DateSouscription = c.DateTime(),
                        DateReservation = c.DateTime(),
                        DateCreationSysteme = c.DateTime(nullable: false),
                        PrixRevise = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemiseAccordee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrixFinal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommissionApporteur = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontantPremierVersement = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontantVerse = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateLivraisonLot = c.DateTime(),
                        Souscrit = c.Boolean(nullable: false),
                        AReserve = c.Boolean(nullable: false),
                        AttribuerLot = c.Boolean(nullable: false),
                        LotAttribue = c.Boolean(nullable: false),
                        ClientID = c.Int(nullable: false),
                        TypeContratID = c.Int(nullable: false),
                        TypeEcheancier = c.Int(),
                        NbEcheances = c.Int(),
                        MontantEcheance = c.Decimal(precision: 18, scale: 2),
                        LotId = c.Int(),
                        CommercialID = c.Int(),
                        ApporteurID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApporteurAffaires", t => t.ApporteurID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Agents", t => t.CommercialID)
                .ForeignKey("dbo.Lots", t => t.LotId)
                .ForeignKey("dbo.TypeContrats", t => t.TypeContratID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.TypeContratID)
                .Index(t => t.LotId)
                .Index(t => t.CommercialID)
                .Index(t => t.ApporteurID);
            
            CreateTable(
                "dbo.ApporteurAffaires",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Adresse = c.String(),
                        Email = c.String(),
                        Mobile1 = c.String(),
                        Mobile2 = c.String(),
                        TelephoneFixe = c.String(),
                        TauxCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreation = c.DateTime(nullable: false),
                        Actif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Commissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Motif = c.String(),
                        MontantTotalCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        Payee = c.Boolean(nullable: false),
                        ContratId = c.Int(nullable: false),
                        ApporteurAffaireId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApporteurAffaires", t => t.ApporteurAffaireId, cascadeDelete: true)
                .ForeignKey("dbo.Contrats", t => t.ContratId, cascadeDelete: true)
                .Index(t => t.ContratId)
                .Index(t => t.ApporteurAffaireId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LibelleRole = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EncaissementGlobals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroEncaissement = c.String(),
                        MontantGlobal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateEncaissement = c.DateTime(),
                        Commentaire = c.String(),
                        ReferencePaiement = c.String(),
                        ModePaiement = c.Int(nullable: false),
                        ContratId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contrats", t => t.ContratId, cascadeDelete: true)
                .Index(t => t.ContratId);
            
            CreateTable(
                "dbo.Encaissements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        ModePaiement = c.Int(nullable: false),
                        Commentaire = c.String(),
                        ReferencePaiement = c.String(),
                        FactureId = c.Int(nullable: false),
                        EncaissementGlobalId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EncaissementGlobals", t => t.EncaissementGlobalId)
                .ForeignKey("dbo.Factures", t => t.FactureId, cascadeDelete: true)
                .Index(t => t.FactureId)
                .Index(t => t.EncaissementGlobalId);
            
            CreateTable(
                "dbo.Factures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroFacture = c.String(),
                        Motif = c.String(),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        DateEcheanceFacture = c.DateTime(),
                        TypeFacture = c.Int(nullable: false),
                        FacturePayee = c.Boolean(nullable: false),
                        MontantTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                        Echue = c.Boolean(nullable: false),
                        ClientId = c.Int(),
                        ContratId = c.Int(),
                        EtatAvancementId = c.Int(),
                        EcheanceId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Contrats", t => t.ContratId)
                .ForeignKey("dbo.Echeances", t => t.EcheanceId)
                .ForeignKey("dbo.EtatAvancements", t => t.EtatAvancementId)
                .Index(t => t.ClientId)
                .Index(t => t.ContratId)
                .Index(t => t.EtatAvancementId)
                .Index(t => t.EcheanceId);
            
            CreateTable(
                "dbo.Echeances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateEcheance = c.DateTime(nullable: false),
                        MontantEcheance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DelaiEcheance = c.DateTime(nullable: false),
                        ClientId = c.Int(),
                        ContratId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Contrats", t => t.ContratId)
                .Index(t => t.ClientId)
                .Index(t => t.ContratId);
            
            CreateTable(
                "dbo.EtatAvancements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateSaisieAvancement = c.DateTime(),
                        Commentaire = c.String(),
                        Actif = c.Boolean(nullable: false),
                        LotId = c.Int(nullable: false),
                        TypeEtatAvancementID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lots", t => t.LotId, cascadeDelete: true)
                .ForeignKey("dbo.TypeEtatAvancements", t => t.TypeEtatAvancementID)
                .Index(t => t.LotId)
                .Index(t => t.TypeEtatAvancementID);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroLot = c.String(),
                        Superficie = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PositionLot = c.Int(nullable: false),
                        PrixRevise = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StatutLot = c.Int(nullable: false),
                        LotVirtuel = c.Boolean(nullable: false),
                        TypeVillaID = c.Int(nullable: false),
                        IlotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ilots", t => t.IlotID, cascadeDelete: true)
                .ForeignKey("dbo.TypeVillas", t => t.TypeVillaID, cascadeDelete: true)
                .Index(t => t.TypeVillaID)
                .Index(t => t.IlotID);
            
            CreateTable(
                "dbo.Ilots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomIlot = c.String(),
                        Superficie = c.Double(nullable: false),
                        DateOuverturePrevue = c.DateTime(),
                        DateOuvertureEffective = c.DateTime(),
                        DateDemarrageTravaux = c.DateTime(),
                        DateFinTravaux = c.DateTime(),
                        StatutOuverture = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatePriseOption = c.DateTime(),
                        DateFinOption = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        SeuilContratAtteint = c.Boolean(nullable: false),
                        ContratGenere = c.Boolean(nullable: false),
                        ClientID = c.Int(),
                        LotId = c.Int(),
                        TypeContratId = c.Int(nullable: false),
                        TypeVillaId = c.Int(nullable: false),
                        PositionLot = c.Int(nullable: false),
                        PrixDeVente = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommercialID = c.Int(nullable: false),
                        ApporteurID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApporteurAffaires", t => t.ApporteurID)
                .ForeignKey("dbo.Clients", t => t.ClientID)
                .ForeignKey("dbo.Agents", t => t.CommercialID, cascadeDelete: true)
                .ForeignKey("dbo.Lots", t => t.LotId)
                .ForeignKey("dbo.TypeContrats", t => t.TypeContratId, cascadeDelete: true)
                .ForeignKey("dbo.TypeVillas", t => t.TypeVillaId, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.LotId)
                .Index(t => t.TypeContratId)
                .Index(t => t.TypeVillaId)
                .Index(t => t.CommercialID)
                .Index(t => t.ApporteurID);
            
            CreateTable(
                "dbo.TypeContrats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LibelleTypeContrat = c.String(),
                        SeuilSouscription = c.Int(nullable: false),
                        SeuilEntreeEnVigueur = c.Int(nullable: false),
                        CategorieContrat = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TypeVillas",
                c => new
                    {
                        TypeVillaId = c.Int(nullable: false, identity: true),
                        NomType = c.String(),
                        CodeType = c.String(),
                        ClasseVilla = c.Int(nullable: false),
                        Description = c.String(),
                        SurfaceDeBase = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrixStandard = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Chambre = c.Int(nullable: false),
                        ChambreAvecSalleDeBain = c.Int(nullable: false),
                        Salon = c.Int(nullable: false),
                        Cuisine = c.Int(nullable: false),
                        Toilette = c.Int(nullable: false),
                        Patio = c.Int(nullable: false),
                        CourArriere = c.Int(nullable: false),
                        ImageVilla = c.String(),
                    })
                .PrimaryKey(t => t.TypeVillaId);
            
            CreateTable(
                "dbo.TypeEtatAvancements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ordre = c.Int(nullable: false),
                        AppelFonds = c.Boolean(nullable: false),
                        TauxDecaissement = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NiveauTechnique = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FactureCommissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Motif = c.String(),
                        MontantAPayer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        Payee = c.Boolean(nullable: false),
                        FactureGenere = c.Boolean(nullable: false),
                        ContratId = c.Int(nullable: false),
                        EncaissementGlobalId = c.Int(nullable: false),
                        FactureCommissionGlobaleId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contrats", t => t.ContratId, cascadeDelete: true)
                .ForeignKey("dbo.EncaissementGlobals", t => t.EncaissementGlobalId, cascadeDelete: false)
                .ForeignKey("dbo.FactureCommissionGlobales", t => t.FactureCommissionGlobaleId)
                .Index(t => t.ContratId)
                .Index(t => t.EncaissementGlobalId)
                .Index(t => t.FactureCommissionGlobaleId);
            
            CreateTable(
                "dbo.FactureCommissionGlobales",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Motif = c.String(),
                        MontantAPayer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        Payee = c.Boolean(nullable: false),
                        ApporteurAffaireId = c.Int(nullable: false),
                        PaiementCommissionGlobalId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApporteurAffaires", t => t.ApporteurAffaireId, cascadeDelete: true)
                .ForeignKey("dbo.PaiementCommissionGlobals", t => t.PaiementCommissionGlobalId)
                .Index(t => t.ApporteurAffaireId)
                .Index(t => t.PaiementCommissionGlobalId);
            
            CreateTable(
                "dbo.PaiementCommissionGlobals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MontantPaye = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        ModePaiement = c.Int(nullable: false),
                        Commentaire = c.String(),
                        ReferencePaiement = c.String(),
                        ApporteurAffaireId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApporteurAffaires", t => t.ApporteurAffaireId, cascadeDelete: true)
                .Index(t => t.ApporteurAffaireId);
            
            CreateTable(
                "dbo.PaiementCommissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MontantPaye = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        ModePaiement = c.Int(nullable: false),
                        Commentaire = c.String(),
                        ReferencePaiement = c.String(),
                        FactureCommissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FactureCommissions", t => t.FactureCommissionId, cascadeDelete: true)
                .Index(t => t.FactureCommissionId);
            
            CreateTable(
                "dbo.Cooperatives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomCooperative = c.String(),
                        TauxRemise = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EncaissementProspects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroEncaissement = c.String(),
                        MontantGlobal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateEncaissement = c.DateTime(),
                        Commentaire = c.String(),
                        ReferencePaiement = c.String(),
                        ModePaiement = c.Int(nullable: false),
                        FraisDeDossier = c.Boolean(nullable: false),
                        Deverse = c.Boolean(nullable: false),
                        ContratId = c.Int(),
                        ProspectId = c.Int(nullable: false),
                        FactureId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contrats", t => t.ContratId)
                .ForeignKey("dbo.FactureProspects", t => t.FactureId)
                .ForeignKey("dbo.Clients", t => t.ProspectId, cascadeDelete: true)
                .Index(t => t.ContratId)
                .Index(t => t.ProspectId)
                .Index(t => t.FactureId);
            
            CreateTable(
                "dbo.FactureProspects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroFacture = c.String(),
                        Motif = c.String(),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        DateEcheanceFacture = c.DateTime(),
                        TypeFacture = c.Int(nullable: false),
                        FacturePayee = c.Boolean(nullable: false),
                        MontantTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProspectId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ProspectId)
                .Index(t => t.ProspectId);
            
            CreateTable(
                "dbo.AppelDeFonds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Motif = c.String(),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        EtatAvancementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EtatAvancements", t => t.EtatAvancementId, cascadeDelete: true)
                .Index(t => t.EtatAvancementId);
            
            CreateTable(
                "dbo.Parametres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        valeurInt = c.Int(nullable: false),
                        valeurString = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Remboursements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroRemboursement = c.String(),
                        MontantRembourse = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateRemboursement = c.DateTime(),
                        Commentaire = c.String(),
                        ReferencePaiement = c.String(),
                        ModePaiement = c.Int(nullable: false),
                        SoldeDeToutCompteId = c.Int(nullable: false),
                        ContratId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contrats", t => t.ContratId, cascadeDelete: true)
                .ForeignKey("dbo.SoldeDeToutComptes", t => t.SoldeDeToutCompteId, cascadeDelete: true)
                .Index(t => t.SoldeDeToutCompteId)
                .Index(t => t.ContratId);
            
            CreateTable(
                "dbo.SoldeDeToutComptes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateResiliation = c.DateTime(nullable: false),
                        NumeroFacture = c.String(),
                        PrixDeVente = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontantTotalEncaisse = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontantDepotDeGarantie = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontantARembourser = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontantTotalCommissionsVersees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remboursé = c.Boolean(nullable: false),
                        ContratId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contrats", t => t.ContratId, cascadeDelete: false)
                .Index(t => t.ContratId);
            
            CreateTable(
                "dbo.Versements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Motif = c.String(),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        CommissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Commissions", t => t.CommissionId, cascadeDelete: true)
                .Index(t => t.CommissionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Versements", "CommissionId", "dbo.Commissions");
            DropForeignKey("dbo.Remboursements", "SoldeDeToutCompteId", "dbo.SoldeDeToutComptes");
            DropForeignKey("dbo.SoldeDeToutComptes", "ContratId", "dbo.Contrats");
            DropForeignKey("dbo.Remboursements", "ContratId", "dbo.Contrats");
            DropForeignKey("dbo.AppelDeFonds", "EtatAvancementId", "dbo.EtatAvancements");
            DropForeignKey("dbo.EncaissementProspects", "ProspectId", "dbo.Clients");
            DropForeignKey("dbo.FactureProspects", "ProspectId", "dbo.Clients");
            DropForeignKey("dbo.EncaissementProspects", "FactureId", "dbo.FactureProspects");
            DropForeignKey("dbo.EncaissementProspects", "ContratId", "dbo.Contrats");
            DropForeignKey("dbo.Clients", "CooperativeId", "dbo.Cooperatives");
            DropForeignKey("dbo.Agents", "ResponsableId", "dbo.Agents");
            DropForeignKey("dbo.Contrats", "TypeContratID", "dbo.TypeContrats");
            DropForeignKey("dbo.PaiementCommissions", "FactureCommissionId", "dbo.FactureCommissions");
            DropForeignKey("dbo.FactureCommissionGlobales", "PaiementCommissionGlobalId", "dbo.PaiementCommissionGlobals");
            DropForeignKey("dbo.PaiementCommissionGlobals", "ApporteurAffaireId", "dbo.ApporteurAffaires");
            DropForeignKey("dbo.FactureCommissions", "FactureCommissionGlobaleId", "dbo.FactureCommissionGlobales");
            DropForeignKey("dbo.FactureCommissionGlobales", "ApporteurAffaireId", "dbo.ApporteurAffaires");
            DropForeignKey("dbo.FactureCommissions", "EncaissementGlobalId", "dbo.EncaissementGlobals");
            DropForeignKey("dbo.FactureCommissions", "ContratId", "dbo.Contrats");
            DropForeignKey("dbo.Factures", "EtatAvancementId", "dbo.EtatAvancements");
            DropForeignKey("dbo.EtatAvancements", "TypeEtatAvancementID", "dbo.TypeEtatAvancements");
            DropForeignKey("dbo.Lots", "TypeVillaID", "dbo.TypeVillas");
            DropForeignKey("dbo.Options", "TypeVillaId", "dbo.TypeVillas");
            DropForeignKey("dbo.Options", "TypeContratId", "dbo.TypeContrats");
            DropForeignKey("dbo.Options", "LotId", "dbo.Lots");
            DropForeignKey("dbo.Options", "CommercialID", "dbo.Agents");
            DropForeignKey("dbo.Options", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.Options", "ApporteurID", "dbo.ApporteurAffaires");
            DropForeignKey("dbo.Lots", "IlotID", "dbo.Ilots");
            DropForeignKey("dbo.EtatAvancements", "LotId", "dbo.Lots");
            DropForeignKey("dbo.Contrats", "LotId", "dbo.Lots");
            DropForeignKey("dbo.Encaissements", "FactureId", "dbo.Factures");
            DropForeignKey("dbo.Factures", "EcheanceId", "dbo.Echeances");
            DropForeignKey("dbo.Echeances", "ContratId", "dbo.Contrats");
            DropForeignKey("dbo.Echeances", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Factures", "ContratId", "dbo.Contrats");
            DropForeignKey("dbo.Factures", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Encaissements", "EncaissementGlobalId", "dbo.EncaissementGlobals");
            DropForeignKey("dbo.EncaissementGlobals", "ContratId", "dbo.Contrats");
            DropForeignKey("dbo.Contrats", "CommercialID", "dbo.Agents");
            DropForeignKey("dbo.Agents", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Contrats", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.Contrats", "ApporteurID", "dbo.ApporteurAffaires");
            DropForeignKey("dbo.Commissions", "ContratId", "dbo.Contrats");
            DropForeignKey("dbo.Commissions", "ApporteurAffaireId", "dbo.ApporteurAffaires");
            DropForeignKey("dbo.Clients", "CommercialID", "dbo.Agents");
            DropForeignKey("dbo.ActiviteCommerciales", "CommercialID", "dbo.Agents");
            DropForeignKey("dbo.ActiviteCommerciales", "ClientID", "dbo.Clients");
            DropIndex("dbo.Versements", new[] { "CommissionId" });
            DropIndex("dbo.SoldeDeToutComptes", new[] { "ContratId" });
            DropIndex("dbo.Remboursements", new[] { "ContratId" });
            DropIndex("dbo.Remboursements", new[] { "SoldeDeToutCompteId" });
            DropIndex("dbo.AppelDeFonds", new[] { "EtatAvancementId" });
            DropIndex("dbo.FactureProspects", new[] { "ProspectId" });
            DropIndex("dbo.EncaissementProspects", new[] { "FactureId" });
            DropIndex("dbo.EncaissementProspects", new[] { "ProspectId" });
            DropIndex("dbo.EncaissementProspects", new[] { "ContratId" });
            DropIndex("dbo.PaiementCommissions", new[] { "FactureCommissionId" });
            DropIndex("dbo.PaiementCommissionGlobals", new[] { "ApporteurAffaireId" });
            DropIndex("dbo.FactureCommissionGlobales", new[] { "PaiementCommissionGlobalId" });
            DropIndex("dbo.FactureCommissionGlobales", new[] { "ApporteurAffaireId" });
            DropIndex("dbo.FactureCommissions", new[] { "FactureCommissionGlobaleId" });
            DropIndex("dbo.FactureCommissions", new[] { "EncaissementGlobalId" });
            DropIndex("dbo.FactureCommissions", new[] { "ContratId" });
            DropIndex("dbo.Options", new[] { "ApporteurID" });
            DropIndex("dbo.Options", new[] { "CommercialID" });
            DropIndex("dbo.Options", new[] { "TypeVillaId" });
            DropIndex("dbo.Options", new[] { "TypeContratId" });
            DropIndex("dbo.Options", new[] { "LotId" });
            DropIndex("dbo.Options", new[] { "ClientID" });
            DropIndex("dbo.Lots", new[] { "IlotID" });
            DropIndex("dbo.Lots", new[] { "TypeVillaID" });
            DropIndex("dbo.EtatAvancements", new[] { "TypeEtatAvancementID" });
            DropIndex("dbo.EtatAvancements", new[] { "LotId" });
            DropIndex("dbo.Echeances", new[] { "ContratId" });
            DropIndex("dbo.Echeances", new[] { "ClientId" });
            DropIndex("dbo.Factures", new[] { "EcheanceId" });
            DropIndex("dbo.Factures", new[] { "EtatAvancementId" });
            DropIndex("dbo.Factures", new[] { "ContratId" });
            DropIndex("dbo.Factures", new[] { "ClientId" });
            DropIndex("dbo.Encaissements", new[] { "EncaissementGlobalId" });
            DropIndex("dbo.Encaissements", new[] { "FactureId" });
            DropIndex("dbo.EncaissementGlobals", new[] { "ContratId" });
            DropIndex("dbo.Commissions", new[] { "ApporteurAffaireId" });
            DropIndex("dbo.Commissions", new[] { "ContratId" });
            DropIndex("dbo.Contrats", new[] { "ApporteurID" });
            DropIndex("dbo.Contrats", new[] { "CommercialID" });
            DropIndex("dbo.Contrats", new[] { "LotId" });
            DropIndex("dbo.Contrats", new[] { "TypeContratID" });
            DropIndex("dbo.Contrats", new[] { "ClientID" });
            DropIndex("dbo.Agents", new[] { "ResponsableId" });
            DropIndex("dbo.Agents", new[] { "RoleId" });
            DropIndex("dbo.Clients", new[] { "CooperativeId" });
            DropIndex("dbo.Clients", new[] { "CommercialID" });
            DropIndex("dbo.ActiviteCommerciales", new[] { "CommercialID" });
            DropIndex("dbo.ActiviteCommerciales", new[] { "ClientID" });
            DropTable("dbo.Versements");
            DropTable("dbo.SoldeDeToutComptes");
            DropTable("dbo.Remboursements");
            DropTable("dbo.Parametres");
            DropTable("dbo.AppelDeFonds");
            DropTable("dbo.FactureProspects");
            DropTable("dbo.EncaissementProspects");
            DropTable("dbo.Cooperatives");
            DropTable("dbo.PaiementCommissions");
            DropTable("dbo.PaiementCommissionGlobals");
            DropTable("dbo.FactureCommissionGlobales");
            DropTable("dbo.FactureCommissions");
            DropTable("dbo.TypeEtatAvancements");
            DropTable("dbo.TypeVillas");
            DropTable("dbo.TypeContrats");
            DropTable("dbo.Options");
            DropTable("dbo.Ilots");
            DropTable("dbo.Lots");
            DropTable("dbo.EtatAvancements");
            DropTable("dbo.Echeances");
            DropTable("dbo.Factures");
            DropTable("dbo.Encaissements");
            DropTable("dbo.EncaissementGlobals");
            DropTable("dbo.Roles");
            DropTable("dbo.Commissions");
            DropTable("dbo.ApporteurAffaires");
            DropTable("dbo.Contrats");
            DropTable("dbo.Agents");
            DropTable("dbo.Clients");
            DropTable("dbo.ActiviteCommerciales");
        }
    }
}
