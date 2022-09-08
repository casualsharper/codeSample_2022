using System.Globalization;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Linq;
using EvolutionTask.Model;

namespace EvolutionTask;

public static class CsvReaderHelper
{
    private static readonly CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Delimiter = ";"
    };

    public static IEnumerable<Employee> ReadEmployeeFile(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            return csv.GetRecords<Employee>()
                .ToList();
        }
    }
}