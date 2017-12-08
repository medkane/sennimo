namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJOptionRemise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Options", "TauxRemiseAccordee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Options", "MontantRemiseAccordee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Options", "MontantRemiseAccordee");
            DropColumn("dbo.Options", "TauxRemiseAccordee");
        }
    }
}
