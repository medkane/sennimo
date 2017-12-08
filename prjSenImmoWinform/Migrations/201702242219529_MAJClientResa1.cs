namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientResa1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientResas", "OrdreEtatAvancement", c => c.Int(nullable: false));
            AddColumn("dbo.ClientResas", "Comentaires", c => c.String());
            AddColumn("dbo.ClientResas", "APrendre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientResas", "APrendre");
            DropColumn("dbo.ClientResas", "Comentaires");
            DropColumn("dbo.ClientResas", "OrdreEtatAvancement");
        }
    }
}
