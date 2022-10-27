using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure
{
    public class OperationContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }

        public DbSet<TypeOfIncome> TypeOfIncomes { get; set; }

        public OperationContext(DbContextOptions<OperationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}