namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJActiviteCommercialeStatut : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActiviteCommerciales", "StatutActiviteCommerciale", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActiviteCommerciales", "StatutActiviteCommerciale");
        }
    }
}
