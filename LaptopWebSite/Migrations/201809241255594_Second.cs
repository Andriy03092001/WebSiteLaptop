namespace LaptopWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblProductDescriptionImage",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 150),
                        ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.tlProducts", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProductDescriptionImage", "ProductId", "dbo.tlProducts");
            DropIndex("dbo.tblProductDescriptionImage", new[] { "ProductId" });
            DropTable("dbo.tblProductDescriptionImage");
        }
    }
}
