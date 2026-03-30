using APBD_Cw1_s28760.Models;

namespace APBD_Cw1_s28760.Services.Interfaces;

public interface IUserLimitPolicy
{
    int GetMaxActiveRentals(User user);
}