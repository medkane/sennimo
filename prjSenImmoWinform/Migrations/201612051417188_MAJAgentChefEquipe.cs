namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJAgentChefEquipe : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Agents", name: "ResponsableId", newName: "ChefEquipeId");
            RenameIndex(table: "dbo.Agents", name: "IX_ResponsableId", newName: "IX_ChefEquipeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Agents", name: "IX_ChefEquipeId", newName: "IX_ResponsableId");
            RenameColumn(table: "dbo.Agents", name: "ChefEquipeId", newName: "ResponsableId");
        }
    }
}
