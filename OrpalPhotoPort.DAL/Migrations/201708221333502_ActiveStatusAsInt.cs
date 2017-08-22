namespace OrpalPhotoPort.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActiveStatusAsInt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ActiveStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "ActiveStatus");
        }
    }
}
