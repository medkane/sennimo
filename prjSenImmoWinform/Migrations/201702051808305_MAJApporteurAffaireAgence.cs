namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJApporteurAffaireAgence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApporteurAffaires", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.ApporteurAffaires", "RaisonSociale", c => c.String());
            AddColumn("dbo.ApporteurAffaires", "NINEA", c => c.String());
            AddColumn("dbo.ApporteurAffaires", "RCCM", c => c.String());
            AddColumn("dbo.ApporteurAffaires", "AdresseBureau", c => c.String());
            AddColumn("dbo.ApporteurAffaires", "TelephoneAgence", c => c.String());
            AddColumn("dbo.ApporteurAffaires", "NomGerant", c => c.String());
            AddColumn("dbo.ApporteurAffaires", "EmailAgence", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApporteurAffaires", "EmailAgence");
            DropColumn("dbo.ApporteurAffaires", "NomGerant");
            DropColumn("dbo.ApporteurAffaires", "TelephoneAgence");
            DropColumn("dbo.ApporteurAffaires", "AdresseBureau");
            DropColumn("dbo.ApporteurAffaires", "RCCM");
            DropColumn("dbo.ApporteurAffaires", "NINEA");
            DropColumn("dbo.ApporteurAffaires", "RaisonSociale");
            DropColumn("dbo.ApporteurAffaires", "Type");
        }
    }
}
