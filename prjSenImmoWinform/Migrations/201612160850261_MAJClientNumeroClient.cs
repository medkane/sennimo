namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientNumeroClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "NumeroClient", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "NumeroClient");
        }
    }
}
