namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDClientsDepot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientDepots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatriculeCommercial = c.String(),
                        Numero = c.String(),
                        Prenom = c.String(),
                        Nom = c.String(),
                        CompteTiers = c.String(),
                        Telephone = c.String(),
                        Mail = c.String(),
                        Adresse = c.String(),
                        Pays = c.String(),
                        Sexe = c.String(),
                        TypeVilla = c.String(),
                        PrixDeVente = c.String(),
                        FraisDossier = c.String(),
                        DureeDepot = c.String(),
                        Periodicite = c.String(),
                        NbEcheances = c.String(),
                        DateDebutVersement = c.String(),
                        DateFinVersement = c.String(),
                        MontantVersement = c.String(),
                        DepotInitial = c.String(),
                        DateSignatureContrat = c.String(),
                        ContratSigne = c.String(),
                        Comentaires = c.String(),
                        APrendre = c.String(),
                        Importe = c.Boolean(nullable: false),
                        OptionGenere = c.Boolean(nullable: false),
                        EncaissementsImporte = c.Boolean(nullable: false),
                        ContratGenere = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ClientDepots");
        }
    }
}
