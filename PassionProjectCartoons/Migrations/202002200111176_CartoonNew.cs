namespace PassionProjectCartoons.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartoonNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cartoons", "CartoonGender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cartoons", "CartoonGender");
        }
    }
}
