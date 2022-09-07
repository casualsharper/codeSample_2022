using Microsoft.EntityFrameworkCore;
using System.IO;

namespace EvolutionTask.Model;

class EmployeeContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = default!;

    public string DbPath { get; } = Path.Join(Utils.ExecutableBaseFolder, Consts.DB_NAME);

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.EnableSensitiveDataLogging()
            .UseSqlite($"Data Source={DbPath}");
}