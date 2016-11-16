namespace Garage_2._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parkingspots",
                c => new
                    {
                        ParkId = c.Int(nullable: false, identity: true),
                        RegNum = c.String(maxLength: 128),
                        SSN = c.String(maxLength: 128),
                        TimeOfRental = c.DateTime(),
                        RentalTime = c.Time(precision: 7),
                    })
                .PrimaryKey(t => t.ParkId)
                .ForeignKey("dbo.Vehicles", t => t.RegNum)
                .ForeignKey("dbo.People", t => t.SSN)
                .Index(t => t.RegNum)
                .Index(t => t.SSN);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        RegNum = c.String(nullable: false, maxLength: 128),
                        SSN = c.String(maxLength: 128),
                        NumberOfWheels = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegNum)
                .ForeignKey("dbo.People", t => t.SSN)
                .Index(t => t.SSN);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        SSN = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Phonenumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SSN);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parkingspots", "SSN", "dbo.People");
            DropForeignKey("dbo.Parkingspots", "RegNum", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "SSN", "dbo.People");
            DropIndex("dbo.Vehicles", new[] { "SSN" });
            DropIndex("dbo.Parkingspots", new[] { "SSN" });
            DropIndex("dbo.Parkingspots", new[] { "RegNum" });
            DropTable("dbo.People");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Parkingspots");
        }
    }
}
