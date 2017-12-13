namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeEtatAvancementType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeEtatAvancements", "TypeAvancement", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeEtatAvancements", "TypeAvancement");
        }
    }
}
