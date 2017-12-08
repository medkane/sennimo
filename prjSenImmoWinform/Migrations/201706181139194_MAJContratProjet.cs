namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJContratProjet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contrats", "ProjetId", c => c.Int());
            CreateIndex("dbo.Contrats", "ProjetId");
            AddForeignKey("dbo.Contrats", "ProjetId", "dbo.Projets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contrats", "ProjetId", "dbo.Projets");
            DropIndex("dbo.Contrats", new[] { "ProjetId" });
            DropColumn("dbo.Contrats", "ProjetId");
        }
    }
}
