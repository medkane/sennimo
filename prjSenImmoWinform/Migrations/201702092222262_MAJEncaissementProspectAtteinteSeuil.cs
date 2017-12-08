namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJEncaissementProspectAtteinteSeuil : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EncaissementProspects", "AtteinteSeuil", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EncaissementProspects", "AtteinteSeuil");
        }
    }
}
