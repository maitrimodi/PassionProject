namespace PassionProjectCartoons.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TvShowNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TvShows", "ChannelID", c => c.Int(nullable: false));
            CreateIndex("dbo.TvShows", "ChannelID");
            AddForeignKey("dbo.TvShows", "ChannelID", "dbo.Channels", "ChannelID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TvShows", "ChannelID", "dbo.Channels");
            DropIndex("dbo.TvShows", new[] { "ChannelID" });
            DropColumn("dbo.TvShows", "ChannelID");
        }
    }
}
