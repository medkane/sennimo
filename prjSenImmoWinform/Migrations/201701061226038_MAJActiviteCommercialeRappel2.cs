namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJActiviteCommercialeRappel2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActiviteCommerciales", "BRappel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActiviteCommerciales", "BRappel");
        }
    }
}
