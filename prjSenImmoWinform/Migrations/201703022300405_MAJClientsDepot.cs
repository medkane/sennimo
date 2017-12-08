namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientsDepot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientDepots", "Lot", c => c.String());
            AddColumn("dbo.ClientDepots", "TypeDepot", c => c.String());
            AddColumn("dbo.ClientDepots", "DateSignatureContratDepot", c => c.String());
            AddColumn("dbo.ClientDepots", "DateSignatureContratResa", c => c.String());
            DropColumn("dbo.ClientDepots", "DateSignatureContrat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientDepots", "DateSignatureContrat", c => c.String());
            DropColumn("dbo.ClientDepots", "DateSignatureContratResa");
            DropColumn("dbo.ClientDepots", "DateSignatureContratDepot");
            DropColumn("dbo.ClientDepots", "TypeDepot");
            DropColumn("dbo.ClientDepots", "Lot");
        }
    }
}
