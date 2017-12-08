namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDNoteContrat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NoteContrats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateNote = c.DateTime(),
                        Note = c.String(),
                        ContratId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contrats", t => t.ContratId)
                .Index(t => t.ContratId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoteContrats", "ContratId", "dbo.Contrats");
            DropIndex("dbo.NoteContrats", new[] { "ContratId" });
            DropTable("dbo.NoteContrats");
        }
    }
}
