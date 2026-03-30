using APBD_Cw1_s28760.Models;
using APBD_Cw1_s28760.Services.Interfaces;

namespace APBD_Cw1_s28760.Services;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _items = new();

    public void Add(User user) => _items.Add(user);
    public IEnumerable<User> GetAll() => _items;
    public User? GetById(Guid id) => _items.SingleOrDefault(u => u.Id == id);
}