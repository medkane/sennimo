namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeETatAvancementProjet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeEtatAvancements", "ProjetId", c => c.Int());
            CreateIndex("dbo.TypeEtatAvancements", "ProjetId");
            AddForeignKey("dbo.TypeEtatAvancements", "ProjetId", "dbo.Projets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeEtatAvancements", "ProjetId", "dbo.Projets");
            DropIndex("dbo.TypeEtatAvancements", new[] { "ProjetId" });
            DropColumn("dbo.TypeEtatAvancements", "ProjetId");
        }
    }
}
