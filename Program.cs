using System;
using System.Linq;
using System.IO;
using EvolutionTask.Model;

namespace EvolutionTask;

class Program
{
    private const int MAX_HITS_TO_DISPLAY = 5;
    static void Main(string[] args)
    {
        var sampleFilePath = Path.Combine(Utils.ExecutableBaseFolder, Consts.SAMPLE_FILE_NAME);

        var data = CsvReaderHelper.ReadEmployeeFile(sampleFilePath);

        using var employeeContextOperator = new EmployeeContextOperator();

        InsertData(data, employeeContextOperator);

        SearchRoutine(employeeContextOperator);
    }

    private static void InsertData(IEnumerable<Employee> data, EmployeeContextOperator employeeContextOperator)
    {
        employeeContextOperator.AddEmployeesAsync(data).GetAwaiter();

        Console.WriteLine($"Employees added: {data.Count()}");
    }

    private static void SearchRoutine(EmployeeContextOperator employeeContextOperator)
    {
        while (true)
        {
            Console.WriteLine("Enter search criteria:");

            var searchCriteria = Console.ReadLine();

            if (string.IsNullOrEmpty(searchCriteria))
            {
                Console.Clear();
                continue;
            }

            var foundEmployees = employeeContextOperator.SearchEmployee(searchCriteria);

            DisplaySearchResults(foundEmployees);

            Console.WriteLine("Press any key to continue");
            Console.Read();
        }
    }

    private static void DisplaySearchResults(IEnumerable<Employee> foundEmployees)
    {
        var totalEntries = foundEmployees.Count();

        if (totalEntries == 0)
        {
            Console.WriteLine("0 hits, try changing search criteria");
            return;
        }
        else if (totalEntries > MAX_HITS_TO_DISPLAY)
        {
            Console.WriteLine($"Total hits: {totalEntries}");
            Console.WriteLine($"Shortening result list to {MAX_HITS_TO_DISPLAY} entries");
        }

        foreach (var employee in foundEmployees)
        {
            Console.WriteLine($"{employee.CompanyEmail}");
        }
    }
}