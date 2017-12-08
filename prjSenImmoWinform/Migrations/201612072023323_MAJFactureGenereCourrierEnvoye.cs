namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJFactureGenereCourrierEnvoye : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Factures", "FactureGenere", c => c.Boolean(nullable: false));
            AddColumn("dbo.Factures", "CourrierEnvoye", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Factures", "CourrierEnvoye");
            DropColumn("dbo.Factures", "FactureGenere");
        }
    }
}
