namespace PassionProjectCartoons.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cartoonxtvshow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartoonTvShows",
                c => new
                    {
                        CartoonTvShowID = c.Int(nullable: false, identity: true),
                        CartoonID = c.Int(nullable: false),
                        TvShowID = c.Int(),
                    })
                .PrimaryKey(t => t.CartoonTvShowID)
                .ForeignKey("dbo.Cartoons", t => t.CartoonID, cascadeDelete: true)
                .ForeignKey("dbo.TvShows", t => t.TvShowID)
                .Index(t => t.CartoonID)
                .Index(t => t.TvShowID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartoonTvShows", "TvShowID", "dbo.TvShows");
            DropForeignKey("dbo.CartoonTvShows", "CartoonID", "dbo.Cartoons");
            DropIndex("dbo.CartoonTvShows", new[] { "TvShowID" });
            DropIndex("dbo.CartoonTvShows", new[] { "CartoonID" });
            DropTable("dbo.CartoonTvShows");
        }
    }
}
