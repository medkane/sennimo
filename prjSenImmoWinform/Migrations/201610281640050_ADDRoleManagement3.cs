namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDRoleManagement3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleSousMenus",
                c => new
                    {
                        Role_ID = c.Int(nullable: false),
                        SousMenu_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_ID, t.SousMenu_ID })
                .ForeignKey("dbo.Roles", t => t.Role_ID, cascadeDelete: true)
                .ForeignKey("dbo.SousMenus", t => t.SousMenu_ID, cascadeDelete: true)
                .Index(t => t.Role_ID)
                .Index(t => t.SousMenu_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleSousMenus", "SousMenu_ID", "dbo.SousMenus");
            DropForeignKey("dbo.RoleSousMenus", "Role_ID", "dbo.Roles");
            DropIndex("dbo.RoleSousMenus", new[] { "SousMenu_ID" });
            DropIndex("dbo.RoleSousMenus", new[] { "Role_ID" });
            DropTable("dbo.RoleSousMenus");
        }
    }
}
