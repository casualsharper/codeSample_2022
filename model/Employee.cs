using System;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace EvolutionTask.Model;

public record Employee
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

    private static string GetCompanyEmail(string firstName, string lastName)
    {
        return firstName.First() + "." + lastName + Consts.COMPANY_EMAIL_DOMAIN;
    }

    private static string GetHrStatus(bool inTraining, DateTime? terminatedAt)
    {
        var result = EmployeeStatus.Active;

        if (inTraining)
        {
            result = EmployeeStatus.InTraining;
        }
        else if (terminatedAt.HasValue)
        {
            result = EmployeeStatus.Terminated;
        }

        return result.ToString();
    }
}