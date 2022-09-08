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

        var data = CsvReaderHelper.ReadEmployeesFile(sampleFilePath);

        using var employeeContextOperator = new EmployeeContextOperator();

        UpsertData(data, employeeContextOperator);

        AwaitingNewCommandMessage();

        SearchRoutine(employeeContextOperator);
    }

    private static void UpsertData(IEnumerable<Employee> data, EmployeeContextOperator employeeContextOperator)
    {
        var newEntries = employeeContextOperator.ProcessEmployees(data);

        Console.WriteLine($"Employees processed: {newEntries}");
    }

    private static void SearchRoutine(EmployeeContextOperator employeeContextOperator)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter search criteria for employee:");

            var searchCriteria = Console.ReadLine();

            Console.Clear();

            if (string.IsNullOrEmpty(searchCriteria))
            {
                continue;
            }

            Console.WriteLine($"Search criteria: {searchCriteria}");

            var foundEmployees = employeeContextOperator.SearchEmployee(searchCriteria);

            DisplaySearchResults(foundEmployees);

            AwaitingNewCommandMessage();
        }
    }

    private static void AwaitingNewCommandMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Press any enter to continue");
        Console.ReadLine();
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

        Console.WriteLine();

        var limitedResultList = foundEmployees
            .Take(MAX_HITS_TO_DISPLAY)
            .ToArray();

        for (int i = 0; i < limitedResultList.Length; i++)
        {
            Console.WriteLine($"{(i + 1)}. " + Environment.NewLine + limitedResultList[i]);
        }
    }
}