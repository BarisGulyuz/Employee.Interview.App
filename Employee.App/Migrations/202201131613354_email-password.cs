namespace Employee.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailpassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employes", "Mail", c => c.String(nullable: false));
            AddColumn("dbo.Employes", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employes", "Password");
            DropColumn("dbo.Employes", "Mail");
        }
    }
}
