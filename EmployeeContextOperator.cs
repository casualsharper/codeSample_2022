using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EvolutionTask.Model;
using Microsoft.EntityFrameworkCore;

namespace EvolutionTask;

public class EmployeeContextOperator : IDisposable
{
    private EmployeeContext employeeContext;
    private bool disposedValue;

    public EmployeeContextOperator()
    {
        employeeContext = new EmployeeContext();
        employeeContext.Database.Migrate();
    }

    public int AddEmployees(IEnumerable<Employee> employees)
    {
        var sanitizedData = SanitizeData(employees);

        employeeContext.BulkMerge(sanitizedData, options =>
        {
            options.MergeKeepIdentity = true;
        });

        return sanitizedData.Count();
    }

    public IEnumerable<Employee> SearchEmployee(string searchString)
    {
        return employeeContext.Employees
            .Where(w =>
                EF.Functions.Like(w.CompanyEmail, $"%{searchString}%") ||
                EF.Functions.Like(w.Email, $"%{searchString}%") ||
                EF.Functions.Like(w.FirstName, $"%{searchString}%") ||
                EF.Functions.Like(w.LastName, $"%{searchString}%"))
            .ToList();
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                employeeContext.Dispose();
            }

            disposedValue = true;
        }
    }

    private IEnumerable<Employee> SanitizeData(IEnumerable<Employee> employees)
    {
        return employees
        .GroupBy(g => g.EmployeeId)
        .Select(s => s.First())
        .ToList();
    }
}