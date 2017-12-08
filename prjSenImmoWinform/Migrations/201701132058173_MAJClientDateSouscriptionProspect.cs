namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientDateSouscriptionProspect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "DateSouscription", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "DateSouscription");
        }
    }
}
