namespace Employee.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Birthday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employes", "Birthday", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employes", "Birthday");
        }
    }
}
