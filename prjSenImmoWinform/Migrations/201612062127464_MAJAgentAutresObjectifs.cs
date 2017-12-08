namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJAgentAutresObjectifs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ObjectifMensuels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Annee = c.Int(nullable: false),
                        Mois = c.Int(nullable: false),
                        objectifVente = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Commentaire = c.String(),
                        CommercialId = c.Int(),
                        ObjectifAnnuelId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agents", t => t.CommercialId)
                .ForeignKey("dbo.ObjectifAnnuels", t => t.ObjectifAnnuelId)
                .Index(t => t.CommercialId)
                .Index(t => t.ObjectifAnnuelId);
            
            CreateTable(
                "dbo.ObjectifTrimestriels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Annee = c.Int(nullable: false),
                        Trimestre = c.Int(nullable: false),
                        objectifVente = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Commentaire = c.String(),
                        CommercialId = c.Int(),
                        ObjectifAnnuelId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agents", t => t.CommercialId)
                .ForeignKey("dbo.ObjectifAnnuels", t => t.ObjectifAnnuelId)
                .Index(t => t.CommercialId)
                .Index(t => t.ObjectifAnnuelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ObjectifTrimestriels", "ObjectifAnnuelId", "dbo.ObjectifAnnuels");
            DropForeignKey("dbo.ObjectifTrimestriels", "CommercialId", "dbo.Agents");
            DropForeignKey("dbo.ObjectifMensuels", "ObjectifAnnuelId", "dbo.ObjectifAnnuels");
            DropForeignKey("dbo.ObjectifMensuels", "CommercialId", "dbo.Agents");
            DropIndex("dbo.ObjectifTrimestriels", new[] { "ObjectifAnnuelId" });
            DropIndex("dbo.ObjectifTrimestriels", new[] { "CommercialId" });
            DropIndex("dbo.ObjectifMensuels", new[] { "ObjectifAnnuelId" });
            DropIndex("dbo.ObjectifMensuels", new[] { "CommercialId" });
            DropTable("dbo.ObjectifTrimestriels");
            DropTable("dbo.ObjectifMensuels");
        }
    }
}
