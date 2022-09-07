using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EvolutionTask.Model;

namespace EvolutionTask;

public class EmployeeContextOperator : IDisposable
{
    private EmployeeContext employeeContext;
    private bool disposedValue;

    public EmployeeContextOperator()
    {
        employeeContext = new EmployeeContext();
    }

    public async Task AddEmployeesAsync(IEnumerable<Employee> employees)
    {
        await employeeContext.AddRangeAsync(employees);
        await employeeContext.SaveChangesAsync();
    }

    public IEnumerable<Employee> SearchEmployee(string searchString)
    {
        return employeeContext.Employees.Where(w =>
        w.CompanyEmail.Contains(searchString) ||
        w.Email.Contains(searchString) ||
        w.FirstName.Contains(searchString) ||
        w.LastName.Contains(searchString)
        ).ToList();
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

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}