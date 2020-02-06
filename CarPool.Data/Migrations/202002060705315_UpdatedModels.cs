namespace CarPool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "Status", c => c.Short(nullable: false));
            AlterColumn("dbo.Cars", "CarType", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "CarType", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "Status", c => c.Int(nullable: false));
        }
    }
}
