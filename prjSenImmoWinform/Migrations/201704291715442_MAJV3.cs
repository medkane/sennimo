namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJV3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Echeances", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Echeances", "ContratId", "dbo.Contrats");
            DropForeignKey("dbo.Factures", "EcheanceId", "dbo.Echeances");
            DropIndex("dbo.Factures", new[] { "EcheanceId" });
            DropIndex("dbo.Echeances", new[] { "ClientId" });
            DropIndex("dbo.Echeances", new[] { "ContratId" });
            DropColumn("dbo.Factures", "EcheanceId");
            DropTable("dbo.Echeances");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Factures", "EcheanceId", c => c.Int());
            CreateIndex("dbo.Echeances", "ContratId");
            CreateIndex("dbo.Echeances", "ClientId");
            CreateIndex("dbo.Factures", "EcheanceId");
            AddForeignKey("dbo.Factures", "EcheanceId", "dbo.Echeances", "ID");
            AddForeignKey("dbo.Echeances", "ContratId", "dbo.Contrats", "Id");
            AddForeignKey("dbo.Echeances", "ClientId", "dbo.Clients", "ID");
        }
    }
}
