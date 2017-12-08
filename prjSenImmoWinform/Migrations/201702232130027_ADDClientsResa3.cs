namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDClientsResa3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientResas", "OptionGenere", c => c.Boolean(nullable: false));
            AddColumn("dbo.ClientResas", "EncaissementsImporte", c => c.Boolean(nullable: false));
            AddColumn("dbo.ClientResas", "ContratGenere", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientResas", "ContratGenere");
            DropColumn("dbo.ClientResas", "EncaissementsImporte");
            DropColumn("dbo.ClientResas", "OptionGenere");
        }
    }
}
