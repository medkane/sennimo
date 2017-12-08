namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJIlotLivraison : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ilots", "DateDebutLivraison", c => c.DateTime());
            AddColumn("dbo.Ilots", "DateFinLivraison", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ilots", "DateFinLivraison");
            DropColumn("dbo.Ilots", "DateDebutLivraison");
        }
    }
}
