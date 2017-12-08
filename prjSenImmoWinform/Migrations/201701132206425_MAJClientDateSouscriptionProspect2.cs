namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientDateSouscriptionProspect2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "DateSouscription", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "DateSouscription", c => c.DateTime(nullable: false));
        }
    }
}
