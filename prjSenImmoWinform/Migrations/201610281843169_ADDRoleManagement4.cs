namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDRoleManagement4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agents", "Fixe", c => c.String());
            DropColumn("dbo.Agents", "Mobile2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agents", "Mobile2", c => c.String());
            DropColumn("dbo.Agents", "Fixe");
        }
    }
}
