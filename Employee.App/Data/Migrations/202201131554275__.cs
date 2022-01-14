namespace Employee.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employes", "ManagerId", c => c.Int(nullable: false));
            AddColumn("dbo.Photos", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "Url");
            DropColumn("dbo.Employes", "ManagerId");
        }
    }
}
