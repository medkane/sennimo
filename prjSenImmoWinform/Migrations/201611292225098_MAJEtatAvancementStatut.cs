namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJEtatAvancementStatut : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EtatAvancements", "Statut", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EtatAvancements", "Statut");
        }
    }
}
