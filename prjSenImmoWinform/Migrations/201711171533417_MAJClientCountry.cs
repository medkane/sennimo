namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientCountry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clients", "CountryId", c => c.Int());
            CreateIndex("dbo.Clients", "CountryId");
            AddForeignKey("dbo.Clients", "CountryId", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "CountryId", "dbo.Countries");
            DropIndex("dbo.Clients", new[] { "CountryId" });
            DropColumn("dbo.Clients", "CountryId");
            DropTable("dbo.Countries");
        }
    }
}
