namespace OrganizerProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserBudgetExpense : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budget",
                c => new
                    {
                        BudgetId = c.Int(nullable: false, identity: true),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalaryDate = c.DateTime(nullable: false),
                        user_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.BudgetId)
                .ForeignKey("dbo.User", t => t.user_UserId)
                .Index(t => t.user_UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        ExpenseId = c.Int(nullable: false),
                        BudgetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Expense",
                c => new
                    {
                        ExpenseId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Descriptions = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Users_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpenseId)
                .ForeignKey("dbo.User", t => t.Users_UserId)
                .Index(t => t.Users_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expense", "Users_UserId", "dbo.User");
            DropForeignKey("dbo.Budget", "user_UserId", "dbo.User");
            DropIndex("dbo.Expense", new[] { "Users_UserId" });
            DropIndex("dbo.Budget", new[] { "user_UserId" });
            DropTable("dbo.Expense");
            DropTable("dbo.User");
            DropTable("dbo.Budget");
        }
    }
}
