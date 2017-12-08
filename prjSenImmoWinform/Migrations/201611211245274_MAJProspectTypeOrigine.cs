namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJProspectTypeOrigine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOrigines", "CommentaireTypeOrigine", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeOrigines", "CommentaireTypeOrigine");
        }
    }
}
