using Microsoft.EntityFrameworkCore;

class HrDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = default!;
}