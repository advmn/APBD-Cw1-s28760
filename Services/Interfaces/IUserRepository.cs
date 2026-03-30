using APBD_Cw1_s28760.Models;

namespace APBD_Cw1_s28760.Services.Interfaces;

public interface IUserRepository
{
    void Add(User user);
    IEnumerable<User> GetAll();
    User? GetById(Guid id);
}