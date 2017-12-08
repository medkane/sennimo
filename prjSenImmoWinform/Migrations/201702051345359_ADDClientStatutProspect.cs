namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDClientStatutProspect : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatutProspects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Motif = c.String(),
                        DateStatut = c.DateTime(nullable: false),
                        Commentaires = c.String(),
                        ProspectId = c.Int(nullable: false),
                        TypeStatutProspectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ProspectId, cascadeDelete: true)
                .ForeignKey("dbo.TypeStatutProspects", t => t.TypeStatutProspectId, cascadeDelete: true)
                .Index(t => t.ProspectId)
                .Index(t => t.TypeStatutProspectId);
            
            CreateTable(
                "dbo.TypeStatutProspects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatutProspects", "TypeStatutProspectId", "dbo.TypeStatutProspects");
            DropForeignKey("dbo.StatutProspects", "ProspectId", "dbo.Clients");
            DropIndex("dbo.StatutProspects", new[] { "TypeStatutProspectId" });
            DropIndex("dbo.StatutProspects", new[] { "ProspectId" });
            DropTable("dbo.TypeStatutProspects");
            DropTable("dbo.StatutProspects");
        }
    }
}
