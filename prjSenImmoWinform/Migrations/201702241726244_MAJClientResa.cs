namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientResa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientResas", "FraisDossier", c => c.String());
            AddColumn("dbo.ClientResas", "DateLivraison", c => c.String());
            AddColumn("dbo.ClientResas", "DateContrat", c => c.String());
            AddColumn("dbo.ClientResas", "DernierEtatAvancement", c => c.String());
            AddColumn("dbo.ClientResas", "Sexe", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientResas", "Sexe");
            DropColumn("dbo.ClientResas", "DernierEtatAvancement");
            DropColumn("dbo.ClientResas", "DateContrat");
            DropColumn("dbo.ClientResas", "DateLivraison");
            DropColumn("dbo.ClientResas", "FraisDossier");
        }
    }
}
