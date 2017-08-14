namespace OrpalPhotoPort.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateTimeField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RegDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "RegDateTime");
        }
    }
}
