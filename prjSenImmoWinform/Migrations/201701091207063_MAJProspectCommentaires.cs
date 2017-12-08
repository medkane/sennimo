namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJProspectCommentaires : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentaireProspects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateDebutTypeOrigine = c.DateTime(),
                        Comentaire = c.String(),
                        ProspectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ProspectId, cascadeDelete: true)
                .Index(t => t.ProspectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentaireProspects", "ProspectId", "dbo.Clients");
            DropIndex("dbo.CommentaireProspects", new[] { "ProspectId" });
            DropTable("dbo.CommentaireProspects");
        }
    }
}
