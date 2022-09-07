using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public static class CsvReaderHelper
{
    public static IEnumerable<Employee> ReadEmployeeFile(string filePath)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            return csv.GetRecords<Employee>()
                .ToList();
        }
    }
}