using APBD_Cw1_s28760.Enums;

namespace APBD_Cw1_s28760.Models;

public abstract class User
{
    public Guid Id { get; } = Guid.NewGuid();
    public string FirstName { get; }
    public string LastName { get; }
    public UserType Type { get; }

    protected User(string firstName, string lastName, UserType type)
    {
        FirstName = firstName;
        LastName = lastName;
        Type = type;
    }

    public override string ToString()
        => $"{Id} | {FirstName} {LastName} | {Type}";
}