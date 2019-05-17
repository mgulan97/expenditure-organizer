using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using OrganizerProject.Models;

namespace OrganizerProject.DAL
{
    public class OrganizerDBContext : DbContext
    {
        public OrganizerDBContext() : base("OrganizerDBContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Budget> Budgets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public List<User> GetUserList()
        {
            List<User> userList = new List<User>();

            return Users.ToList();

        }

        public List<Budget> GetBudgetList()
        {
            List<Budget> budgetList = new List<Budget>();

            return Budgets.ToList();

        }

        public List<Expense> GetExpenseList()
        {
            List<Expense> expenseList = new List<Expense>();

            return Expenses.ToList();

        }
    }
}