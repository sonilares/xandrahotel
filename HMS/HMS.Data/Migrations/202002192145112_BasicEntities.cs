namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccomodationPackagees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        NoOfRoom = c.Int(nullable: false),
                        FeePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccomodationTypes", t => t.AccomodationTypeId, cascadeDelete: true)
                .Index(t => t.AccomodationTypeId);
            
            CreateTable(
                "dbo.AccomodationTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Accomodations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationPackageeID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccomodationPackagees", t => t.AccomodationPackageeID, cascadeDelete: true)
                .Index(t => t.AccomodationPackageeID);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationID = c.Int(nullable: false),
                        fromDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationID, cascadeDelete: true)
                .Index(t => t.AccomodationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "AccomodationID", "dbo.Accomodations");
            DropForeignKey("dbo.Accomodations", "AccomodationPackageeID", "dbo.AccomodationPackagees");
            DropForeignKey("dbo.AccomodationPackagees", "AccomodationTypeId", "dbo.AccomodationTypes");
            DropIndex("dbo.Bookings", new[] { "AccomodationID" });
            DropIndex("dbo.Accomodations", new[] { "AccomodationPackageeID" });
            DropIndex("dbo.AccomodationPackagees", new[] { "AccomodationTypeId" });
            DropTable("dbo.Bookings");
            DropTable("dbo.Accomodations");
            DropTable("dbo.AccomodationTypes");
            DropTable("dbo.AccomodationPackagees");
        }
    }
}
