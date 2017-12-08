namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeOption : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOrigines", "BActif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeOrigines", "BActif");
        }
    }
}
