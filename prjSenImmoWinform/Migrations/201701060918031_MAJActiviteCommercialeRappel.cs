namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJActiviteCommercialeRappel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActiviteCommerciales", "DateRappel", c => c.DateTime());
            AddColumn("dbo.ActiviteCommerciales", "HeureRappel", c => c.Time(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActiviteCommerciales", "HeureRappel");
            DropColumn("dbo.ActiviteCommerciales", "DateRappel");
        }
    }
}
