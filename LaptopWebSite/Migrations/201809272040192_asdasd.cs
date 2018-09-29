namespace LaptopWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdasd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblUserProfiler",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblUserProfiler", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.tblUserProfiler", new[] { "Id" });
            DropTable("dbo.tblUserProfiler");
        }
    }
}
