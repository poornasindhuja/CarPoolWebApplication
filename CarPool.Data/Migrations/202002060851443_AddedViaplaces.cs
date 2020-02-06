namespace CarPool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedViaplaces : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rides", "ViaPlaces", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rides", "ViaPlaces");
        }
    }
}
