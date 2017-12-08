namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDRoleManagement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CodeMenu = c.String(),
                        LibelleMenu = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SousMenus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CodeSousMenu = c.String(),
                        LibelleSousMenu = c.String(),
                        MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.MenuRoles",
                c => new
                    {
                        Menu_ID = c.Int(nullable: false),
                        Role_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Menu_ID, t.Role_ID })
                .ForeignKey("dbo.Menus", t => t.Menu_ID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.Menu_ID)
                .Index(t => t.Role_ID);
            
            AddColumn("dbo.Roles", "CodeRole", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SousMenus", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.MenuRoles", "Role_ID", "dbo.Roles");
            DropForeignKey("dbo.MenuRoles", "Menu_ID", "dbo.Menus");
            DropIndex("dbo.MenuRoles", new[] { "Role_ID" });
            DropIndex("dbo.MenuRoles", new[] { "Menu_ID" });
            DropIndex("dbo.SousMenus", new[] { "MenuId" });
            DropColumn("dbo.Roles", "CodeRole");
            DropTable("dbo.MenuRoles");
            DropTable("dbo.SousMenus");
            DropTable("dbo.Menus");
        }
    }
}
