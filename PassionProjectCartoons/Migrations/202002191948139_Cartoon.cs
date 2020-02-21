namespace PassionProjectCartoons.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cartoon : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cartoons",
                c => new
                    {
                        CartoonID = c.Int(nullable: false, identity: true),
                        CartoonName = c.String(),
                        CartoonType = c.String(),
                        Weight = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                        HasPic = c.Int(nullable: false),
                        PicExtension = c.String(),
                        TvShowID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartoonID)
                .ForeignKey("dbo.TvShows", t => t.TvShowID, cascadeDelete: true)
                .Index(t => t.TvShowID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cartoons", "TvShowID", "dbo.TvShows");
            DropIndex("dbo.Cartoons", new[] { "TvShowID" });
            DropTable("dbo.Cartoons");
        }
    }
}
