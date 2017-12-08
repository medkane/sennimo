namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDTypeImmeuble : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeImmeubles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeTypeImmeuble = c.String(),
                        LibelleTypeImmeuble = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Ilots", "TypeImmeubleId", c => c.Int());
            AddColumn("dbo.TypeVillas", "TypeImmeubleId", c => c.Int());
            CreateIndex("dbo.Ilots", "TypeImmeubleId");
            CreateIndex("dbo.TypeVillas", "TypeImmeubleId");
            AddForeignKey("dbo.Ilots", "TypeImmeubleId", "dbo.TypeImmeubles", "Id");
            AddForeignKey("dbo.TypeVillas", "TypeImmeubleId", "dbo.TypeImmeubles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeVillas", "TypeImmeubleId", "dbo.TypeImmeubles");
            DropForeignKey("dbo.Ilots", "TypeImmeubleId", "dbo.TypeImmeubles");
            DropIndex("dbo.TypeVillas", new[] { "TypeImmeubleId" });
            DropIndex("dbo.Ilots", new[] { "TypeImmeubleId" });
            DropColumn("dbo.TypeVillas", "TypeImmeubleId");
            DropColumn("dbo.Ilots", "TypeImmeubleId");
            DropTable("dbo.TypeImmeubles");
        }
    }
}
