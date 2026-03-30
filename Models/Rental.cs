namespace APBD_Cw1_s28760.Models;

public class Rental
{
    public Guid Id { get; } = Guid.NewGuid();
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime StartDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public decimal Penalty { get; private set; }

    public bool IsReturned => ReturnDate.HasValue;
    public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;
    public bool WasReturnedLate => IsReturned && ReturnDate.Value > DueDate;

    public Rental(User user, Equipment equipment, DateTime startDate, DateTime dueDate)
    {
        User = user;
        Equipment = equipment;
        StartDate = startDate;
        DueDate = dueDate;
    }

    public void Close(DateTime returnDate, decimal penalty)
    {
        ReturnDate = returnDate;
        Penalty = penalty;
    }

    public override string ToString()
        => $"{Id} | {User.FirstName} {User.LastName} | {Equipment.Name} | " +
           $"Start: {StartDate:d}, Due: {DueDate:d}, Returned: {ReturnDate:d}, Penalty: {Penalty:C}";
}