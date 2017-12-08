namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJOptionDepot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Options", "Surface", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Options", "PrixStandard", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Options", "PrixRevise", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Options", "PrixRevise");
            DropColumn("dbo.Options", "PrixStandard");
            DropColumn("dbo.Options", "Surface");
        }
    }
}
