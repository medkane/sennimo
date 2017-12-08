namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJAgentObjectifAnnuel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ObjectifAnnuels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Annee = c.Int(nullable: false),
                        objectifVente = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Commentaire = c.String(),
                        CommercialId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agents", t => t.CommercialId)
                .Index(t => t.CommercialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ObjectifAnnuels", "CommercialId", "dbo.Agents");
            DropIndex("dbo.ObjectifAnnuels", new[] { "CommercialId" });
            DropTable("dbo.ObjectifAnnuels");
        }
    }
}
