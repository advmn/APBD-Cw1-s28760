using APBD_Cw1_s28760.Models;
using APBD_Cw1_s28760.Services.Interfaces;

namespace APBD_Cw1_s28760.Services;

public class InMemoryRentalRepository : IRentalRepository
{
    private readonly List<Rental> _items = new();

    public void Add(Rental rental) => _items.Add(rental);
    public IEnumerable<Rental> GetAll() => _items;
    public Rental? GetById(Guid id) => _items.SingleOrDefault(r => r.Id == id);
}