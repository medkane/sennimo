namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeOrigineProjet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOrigines", "ProjetId", c => c.Int());
            CreateIndex("dbo.TypeOrigines", "ProjetId");
            AddForeignKey("dbo.TypeOrigines", "ProjetId", "dbo.Projets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeOrigines", "ProjetId", "dbo.Projets");
            DropIndex("dbo.TypeOrigines", new[] { "ProjetId" });
            DropColumn("dbo.TypeOrigines", "ProjetId");
        }
    }
}
