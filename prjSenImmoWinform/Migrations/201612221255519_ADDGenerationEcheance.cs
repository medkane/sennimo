namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDGenerationEcheance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GenerationEcheances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Mois = c.Int(nullable: false),
                        Annee = c.Int(nullable: false),
                        Comentaire = c.String(),
                        AgentId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agents", t => t.AgentId)
                .Index(t => t.AgentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GenerationEcheances", "AgentId", "dbo.Agents");
            DropIndex("dbo.GenerationEcheances", new[] { "AgentId" });
            DropTable("dbo.GenerationEcheances");
        }
    }
}
