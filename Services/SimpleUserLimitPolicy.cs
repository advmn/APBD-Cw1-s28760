using APBD_Cw1_s28760.Enums;
using APBD_Cw1_s28760.Models;
using APBD_Cw1_s28760.Services.Interfaces;

namespace APBD_Cw1_s28760.Services;

public class SimpleUserLimitPolicy : IUserLimitPolicy
{
    public int GetMaxActiveRentals(User user)
        => user.Type switch
        {
            UserType.Student => 2,
            UserType.Employee => 5,
            _ => 0
        };
}