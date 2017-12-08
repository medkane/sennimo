namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDImportEncaissements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportEncaissements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateEncaissement = c.String(),
                        Compte = c.String(),
                        Reference = c.String(),
                        Libelle = c.String(),
                        InituleComptetiers = c.String(),
                        Montant = c.String(),
                        Importe = c.Boolean(nullable: false),
                        iDClient = c.Int(nullable: false),
                        NomClient = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImportEncaissements");
        }
    }
}
