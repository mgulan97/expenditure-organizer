namespace OrganizerProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EndBudgetDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Budget", "EndBudgetDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Budget", "EndBudgetDate");
        }
    }
}
