namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPicturesEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccomodationPackagePictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationPackageID = c.Int(nullable: false),
                        pictureID = c.Int(nullable: false),
                        AccomodationPackagee_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.pictureID, cascadeDelete: true)
                .ForeignKey("dbo.AccomodationPackagees", t => t.AccomodationPackagee_ID)
                .Index(t => t.pictureID)
                .Index(t => t.AccomodationPackagee_ID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AccomodationPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationID = c.Int(nullable: false),
                        pictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.pictureID, cascadeDelete: true)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationID, cascadeDelete: true)
                .Index(t => t.AccomodationID)
                .Index(t => t.pictureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccomodationPictures", "AccomodationID", "dbo.Accomodations");
            DropForeignKey("dbo.AccomodationPictures", "pictureID", "dbo.Pictures");
            DropForeignKey("dbo.AccomodationPackagePictures", "AccomodationPackagee_ID", "dbo.AccomodationPackagees");
            DropForeignKey("dbo.AccomodationPackagePictures", "pictureID", "dbo.Pictures");
            DropIndex("dbo.AccomodationPictures", new[] { "pictureID" });
            DropIndex("dbo.AccomodationPictures", new[] { "AccomodationID" });
            DropIndex("dbo.AccomodationPackagePictures", new[] { "AccomodationPackagee_ID" });
            DropIndex("dbo.AccomodationPackagePictures", new[] { "pictureID" });
            DropTable("dbo.AccomodationPictures");
            DropTable("dbo.Pictures");
            DropTable("dbo.AccomodationPackagePictures");
        }
    }
}
