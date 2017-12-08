namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeContratProjet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeContrats", "ProjetId", c => c.Int());
            CreateIndex("dbo.TypeContrats", "ProjetId");
            AddForeignKey("dbo.TypeContrats", "ProjetId", "dbo.Projets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeContrats", "ProjetId", "dbo.Projets");
            DropIndex("dbo.TypeContrats", new[] { "ProjetId" });
            DropColumn("dbo.TypeContrats", "ProjetId");
        }
    }
}
