namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDRoleManagement2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MenuRoles", newName: "RoleMenus");
            DropPrimaryKey("dbo.RoleMenus");
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CodeAction = c.String(),
                        LibelleAction = c.String(),
                        SousMenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SousMenus", t => t.SousMenuId, cascadeDelete: true)
                .Index(t => t.SousMenuId);
            
            AddPrimaryKey("dbo.RoleMenus", new[] { "Role_ID", "Menu_ID" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actions", "SousMenuId", "dbo.SousMenus");
            DropIndex("dbo.Actions", new[] { "SousMenuId" });
            DropPrimaryKey("dbo.RoleMenus");
            DropTable("dbo.Actions");
            AddPrimaryKey("dbo.RoleMenus", new[] { "Menu_ID", "Role_ID" });
            RenameTable(name: "dbo.RoleMenus", newName: "MenuRoles");
        }
    }
}
