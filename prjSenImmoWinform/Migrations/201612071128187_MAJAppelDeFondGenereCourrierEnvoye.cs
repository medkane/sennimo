namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJAppelDeFondGenereCourrierEnvoye : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppelDeFonds", "FactureGenere", c => c.Boolean(nullable: false));
            AddColumn("dbo.AppelDeFonds", "CourrierEnvoye", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppelDeFonds", "CourrierEnvoye");
            DropColumn("dbo.AppelDeFonds", "FactureGenere");
        }
    }
}
