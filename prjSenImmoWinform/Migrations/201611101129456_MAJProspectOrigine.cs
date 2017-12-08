namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJProspectOrigine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOrigines",
                c => new
                    {
                        TypeOrigineId = c.Int(nullable: false, identity: true),
                        ClassOrigine = c.Int(nullable: false),
                        LibelleTypeOrigine = c.String(),
                        BLimiteDansLeTemps = c.Boolean(nullable: false),
                        DateDebutTypeOrigine = c.DateTime(),
                        DateFinTypeOrigine = c.DateTime(),
                    })
                .PrimaryKey(t => t.TypeOrigineId);
            
            AddColumn("dbo.Clients", "OrigineId", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "Origine_TypeOrigineId", c => c.Int());
            CreateIndex("dbo.Clients", "Origine_TypeOrigineId");
            AddForeignKey("dbo.Clients", "Origine_TypeOrigineId", "dbo.TypeOrigines", "TypeOrigineId");
            DropColumn("dbo.Clients", "Origine");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Origine", c => c.Int(nullable: false));
            DropForeignKey("dbo.Clients", "Origine_TypeOrigineId", "dbo.TypeOrigines");
            DropIndex("dbo.Clients", new[] { "Origine_TypeOrigineId" });
            DropColumn("dbo.Clients", "Origine_TypeOrigineId");
            DropColumn("dbo.Clients", "OrigineId");
            DropTable("dbo.TypeOrigines");
        }
    }
}
