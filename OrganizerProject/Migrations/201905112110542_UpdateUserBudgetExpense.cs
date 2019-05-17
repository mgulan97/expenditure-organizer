namespace OrganizerProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserBudgetExpense : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Budget", "user_UserId", "dbo.User");
            DropForeignKey("dbo.Expense", "Users_UserId", "dbo.User");
            DropIndex("dbo.Budget", new[] { "user_UserId" });
            DropIndex("dbo.Expense", new[] { "Users_UserId" });
            RenameColumn(table: "dbo.Budget", name: "user_UserId", newName: "UserId");
            RenameColumn(table: "dbo.Expense", name: "Users_UserId", newName: "UserId");
            AlterColumn("dbo.Budget", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Expense", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Budget", "UserId");
            CreateIndex("dbo.Expense", "UserId");
            AddForeignKey("dbo.Budget", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Expense", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            DropColumn("dbo.User", "ExpenseId");
            DropColumn("dbo.User", "BudgetId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "BudgetId", c => c.Int(nullable: false));
            AddColumn("dbo.User", "ExpenseId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Expense", "UserId", "dbo.User");
            DropForeignKey("dbo.Budget", "UserId", "dbo.User");
            DropIndex("dbo.Expense", new[] { "UserId" });
            DropIndex("dbo.Budget", new[] { "UserId" });
            AlterColumn("dbo.Expense", "UserId", c => c.Int());
            AlterColumn("dbo.Budget", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Expense", name: "UserId", newName: "Users_UserId");
            RenameColumn(table: "dbo.Budget", name: "UserId", newName: "user_UserId");
            CreateIndex("dbo.Expense", "Users_UserId");
            CreateIndex("dbo.Budget", "user_UserId");
            AddForeignKey("dbo.Expense", "Users_UserId", "dbo.User", "UserId");
            AddForeignKey("dbo.Budget", "user_UserId", "dbo.User", "UserId");
        }
    }
}
