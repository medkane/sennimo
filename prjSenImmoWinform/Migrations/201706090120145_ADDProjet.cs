namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDProjet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DenominationProjet = c.String(),
                        Localisation = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Agents", "ProjetId", c => c.Int());
            AddColumn("dbo.Clients", "ProjetId", c => c.Int());
            AddColumn("dbo.Ilots", "ProjetId", c => c.Int());
            AddColumn("dbo.TypeVillas", "ProjetId", c => c.Int());
            CreateIndex("dbo.Agents", "ProjetId");
            CreateIndex("dbo.Clients", "ProjetId");
            CreateIndex("dbo.Ilots", "ProjetId");
            CreateIndex("dbo.TypeVillas", "ProjetId");
            AddForeignKey("dbo.Ilots", "ProjetId", "dbo.Projets", "Id");
            AddForeignKey("dbo.TypeVillas", "ProjetId", "dbo.Projets", "Id");
            AddForeignKey("dbo.Clients", "ProjetId", "dbo.Projets", "Id");
            AddForeignKey("dbo.Agents", "ProjetId", "dbo.Projets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agents", "ProjetId", "dbo.Projets");
            DropForeignKey("dbo.Clients", "ProjetId", "dbo.Projets");
            DropForeignKey("dbo.TypeVillas", "ProjetId", "dbo.Projets");
            DropForeignKey("dbo.Ilots", "ProjetId", "dbo.Projets");
            DropIndex("dbo.TypeVillas", new[] { "ProjetId" });
            DropIndex("dbo.Ilots", new[] { "ProjetId" });
            DropIndex("dbo.Clients", new[] { "ProjetId" });
            DropIndex("dbo.Agents", new[] { "ProjetId" });
            DropColumn("dbo.TypeVillas", "ProjetId");
            DropColumn("dbo.Ilots", "ProjetId");
            DropColumn("dbo.Clients", "ProjetId");
            DropColumn("dbo.Agents", "ProjetId");
            DropTable("dbo.Projets");
        }
    }
}
