namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJclientProspectAffecte : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "ProspectAffecte", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "ProspectAffecte");
        }
    }
}
