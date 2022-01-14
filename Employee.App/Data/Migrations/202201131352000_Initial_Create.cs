namespace Employee.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeparmentId = c.Int(nullable: false),
                        EmployeeTypeId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        Department_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .ForeignKey("dbo.EmployeeTypes", t => t.EmployeeTypeId)
                .Index(t => t.EmployeeTypeId)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.EmployeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employes", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "EmployeeId", "dbo.Employes");
            DropForeignKey("dbo.Employes", "EmployeeTypeId", "dbo.EmployeeTypes");
            DropForeignKey("dbo.Employes", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Photos", new[] { "EmployeeId" });
            DropIndex("dbo.Employes", new[] { "Department_Id" });
            DropIndex("dbo.Employes", new[] { "EmployeeTypeId" });
            DropTable("dbo.Photos");
            DropTable("dbo.EmployeeTypes");
            DropTable("dbo.Employes");
            DropTable("dbo.Departments");
        }
    }
}
