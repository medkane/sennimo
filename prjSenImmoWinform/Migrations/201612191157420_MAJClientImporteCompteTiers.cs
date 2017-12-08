namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientImporteCompteTiers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Importe", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clients", "CompteTiers", c => c.String());
            AddColumn("dbo.Clients", "IntituleCompteTiers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "IntituleCompteTiers");
            DropColumn("dbo.Clients", "CompteTiers");
            DropColumn("dbo.Clients", "Importe");
        }
    }
}
