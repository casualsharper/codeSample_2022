using Microsoft.EntityFrameworkCore;
using System.IO;

namespace EvolutionTask.Model;

class EmployeeContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = default!;

    public string DbPath { get; } = Path.Join(Utils.ExecutableBaseFolder, "employee.db");

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}