using APBD_Cw1_s28760.Enums;

namespace APBD_Cw1_s28760.Models;

public class Student : User
{
    public string IndexNumber { get; }

    public Student(string firstName, string lastName, string indexNumber)
        : base(firstName, lastName, UserType.Student)
    {
        IndexNumber = indexNumber;
    }
}