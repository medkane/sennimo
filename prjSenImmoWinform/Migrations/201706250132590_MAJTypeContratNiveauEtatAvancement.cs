namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeContratNiveauEtatAvancement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeEtatAvancements", "TypeContratId", c => c.Int());
            CreateIndex("dbo.TypeEtatAvancements", "TypeContratId");
            AddForeignKey("dbo.TypeEtatAvancements", "TypeContratId", "dbo.TypeContrats", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeEtatAvancements", "TypeContratId", "dbo.TypeContrats");
            DropIndex("dbo.TypeEtatAvancements", new[] { "TypeContratId" });
            DropColumn("dbo.TypeEtatAvancements", "TypeContratId");
        }
    }
}
