using APBD_Cw1_s28760.Models;
using APBD_Cw1_s28760.Services.Interfaces;

namespace APBD_Cw1_s28760.Services;

public class SimplePenaltyPolicy : IPenaltyPolicy
{
    private readonly decimal _dailyRate;

    public SimplePenaltyPolicy(decimal dailyRate)
    {
        _dailyRate = dailyRate;
    }

    public decimal CalculatePenalty(Rental rental, DateTime returnDate)
    {
        if (returnDate <= rental.DueDate)
            return 0m;

        var daysLate = (returnDate.Date - rental.DueDate.Date).Days;
        return daysLate * _dailyRate;
    }
}