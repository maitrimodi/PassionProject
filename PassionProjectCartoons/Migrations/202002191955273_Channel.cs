namespace PassionProjectCartoons.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Channel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        ChannelID = c.Int(nullable: false, identity: true),
                        ChannelName = c.String(),
                        ChannelDescription = c.String(),
                        Country = c.String(),
                        ChannelOwner = c.String(),
                    })
                .PrimaryKey(t => t.ChannelID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Channels");
        }
    }
}
