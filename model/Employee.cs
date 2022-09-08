using System;
using System.Linq;
using System.Text;
using CsvHelper.Configuration.Attributes;

namespace EvolutionTask.Model;

public class Employee
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Phone { get; set; }
    public string Email { get; set; } = default!;
    public string? Country { get; set; }
    [Name("EmployeeID")]
    public int EmployeeId { get; set; }
    [Name("In Training")]
    [BooleanTrueValues("Yes")]
    [BooleanFalseValues("No")]
    public bool InTraining { get; set; }
    public string? Department { get; set; }
    public string? Position { get; set; }
    public DateTime HiredAt { get; set; }
    public DateTime? TerminatedAt { get; set; }

    private string? hrStatus;
    [Ignore]
    public string HrStatus { get { return hrStatus ?? GetHrStatus(InTraining, TerminatedAt); } set { hrStatus = value; } }

    private string? companyEmail;
    [Ignore]
    public string CompanyEmail { get { return companyEmail ?? GetCompanyEmail(FirstName, LastName); } set { companyEmail = value; } }

    public override string ToString()
    {
        return GetType().GetProperties()
                .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"))
                .Aggregate(
                    new StringBuilder(),
                    (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                    sb => sb.ToString());
    }

    private static string GetCompanyEmail(string firstName, string lastName)
    {
        return firstName.First() + lastName + Consts.COMPANY_EMAIL_DOMAIN;
    }

    private static string GetHrStatus(bool inTraining, DateTime? terminatedAt)
    {
        var result = EmployeeStatus.Active;

        if (terminatedAt.HasValue)
        {
            result = EmployeeStatus.Terminated;
        }
        else if (inTraining)
        {
            result = EmployeeStatus.InTraining;
        }

        return result.ToString();
    }
}