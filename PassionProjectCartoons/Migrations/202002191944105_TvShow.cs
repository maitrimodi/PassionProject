namespace PassionProjectCartoons.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TvShow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TvShows",
                c => new
                    {
                        TvShowID = c.Int(nullable: false, identity: true),
                        TvShowName = c.String(),
                        Director = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Episodes = c.Int(nullable: false),
                        Genre = c.String(),
                        Language = c.String(),
                    })
                .PrimaryKey(t => t.TvShowID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TvShows");
        }
    }
}
