using APBD_Cw1_s28760.Enums;

namespace APBD_Cw1_s28760.Models;

public class Employee : User
{
    public string Department { get; }

    public Employee(string firstName, string lastName, string department)
        : base(firstName, lastName, UserType.Employee)
    {
        Department = department;
    }
}