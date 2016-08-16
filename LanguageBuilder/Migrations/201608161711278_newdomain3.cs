namespace LanguageBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdomain3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "UserCrossID", c => c.String());
            AddColumn("dbo.Students", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Students", "User_Id");
            AddForeignKey("dbo.Students", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Students", new[] { "User_Id" });
            DropColumn("dbo.Students", "User_Id");
            DropColumn("dbo.Students", "UserCrossID");
        }
    }
}
