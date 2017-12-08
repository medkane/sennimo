namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeEtatAvancementStatutTermine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeEtatAvancements", "StatutTermine", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeEtatAvancements", "StatutTermine");
        }
    }
}
