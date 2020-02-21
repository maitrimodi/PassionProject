namespace PassionProjectCartoons.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tvshowxchannel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TvShowChannels",
                c => new
                    {
                        TvShowChannelID = c.Int(nullable: false, identity: true),
                        TvShowID = c.Int(nullable: false),
                        ChannelID = c.Int(),
                    })
                .PrimaryKey(t => t.TvShowChannelID)
                .ForeignKey("dbo.Channels", t => t.ChannelID)
                .ForeignKey("dbo.TvShows", t => t.TvShowID, cascadeDelete: true)
                .Index(t => t.TvShowID)
                .Index(t => t.ChannelID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TvShowChannels", "TvShowID", "dbo.TvShows");
            DropForeignKey("dbo.TvShowChannels", "ChannelID", "dbo.Channels");
            DropIndex("dbo.TvShowChannels", new[] { "ChannelID" });
            DropIndex("dbo.TvShowChannels", new[] { "TvShowID" });
            DropTable("dbo.TvShowChannels");
        }
    }
}
