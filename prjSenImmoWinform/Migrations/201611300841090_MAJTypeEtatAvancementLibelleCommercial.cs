namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeEtatAvancementLibelleCommercial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeEtatAvancements", "LibelleCommercial", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeEtatAvancements", "LibelleCommercial");
        }
    }
}
