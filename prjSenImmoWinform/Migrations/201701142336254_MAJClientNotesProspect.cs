namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientNotesProspect : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CommentaireProspects", newName: "NoteProspects");
            AddColumn("dbo.NoteProspects", "ActivitecommercialId", c => c.Int());
            AddColumn("dbo.NoteProspects", "ActiviteCommerciale_Id", c => c.Int());
            CreateIndex("dbo.NoteProspects", "ActiviteCommerciale_Id");
            AddForeignKey("dbo.NoteProspects", "ActiviteCommerciale_Id", "dbo.ActiviteCommerciales", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoteProspects", "ActiviteCommerciale_Id", "dbo.ActiviteCommerciales");
            DropIndex("dbo.NoteProspects", new[] { "ActiviteCommerciale_Id" });
            DropColumn("dbo.NoteProspects", "ActiviteCommerciale_Id");
            DropColumn("dbo.NoteProspects", "ActivitecommercialId");
            RenameTable(name: "dbo.NoteProspects", newName: "CommentaireProspects");
        }
    }
}
