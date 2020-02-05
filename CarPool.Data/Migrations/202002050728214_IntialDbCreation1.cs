namespace CarPool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialDbCreation1 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Bookings");
            DropTable("dbo.Cars");
            DropTable("dbo.Rides");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        EmailAddress = c.String(),
                        Address = c.String(),
                        Gender = c.String(),
                        PetName = c.String(nullable: false),
                        Password = c.String(maxLength: 16),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Rides",
                c => new
                    {
                        RideId = c.Int(nullable: false, identity: true),
                        NoOfSeatsAvailable = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Source = c.String(nullable: false),
                        Destination = c.String(nullable: false),
                        CarNumber = c.String(nullable: false),
                        RideProviderId = c.Int(nullable: false),
                        PricePerKilometer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateOfRide = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RideId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarNo = c.String(nullable: false, maxLength: 128),
                        CarName = c.String(),
                        CarType = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        OwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarNo);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        RideId = c.Int(nullable: false),
                        Source = c.String(maxLength: 50),
                        Destination = c.String(nullable: false, maxLength: 50),
                        UserId = c.Int(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        CostOfBooking = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberSeatsSelected = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId);
            
        }
    }
}
