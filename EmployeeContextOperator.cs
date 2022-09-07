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

    public int AddEmployees(IEnumerable<Employee> employees)
    {
        var sanitizedData = SanitizeData(employees);

        employeeContext.AddRange(sanitizedData);
        employeeContext.SaveChanges();

        return sanitizedData.Count;
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

    private List<Employee> SanitizeData(IEnumerable<Employee> employees)
    {
        return employees
        .GroupBy(g => g.EmployeeId)
        .Select(s => s.First())
        .ToList();
    }
}