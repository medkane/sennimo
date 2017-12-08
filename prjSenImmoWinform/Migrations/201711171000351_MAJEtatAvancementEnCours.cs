namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJEtatAvancementEnCours : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EtatAvancements", "Encours", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EtatAvancements", "Encours");
        }
    }
}
