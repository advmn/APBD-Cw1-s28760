using APBD_Cw1_s28760.Models;
using APBD_Cw1_s28760.Services.Interfaces;

namespace APBD_Cw1_s28760.Services;

public class InMemoryEquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _items = new();

    public void Add(Equipment equipment) => _items.Add(equipment);
    public IEnumerable<Equipment> GetAll() => _items;
    public Equipment? GetById(Guid id) => _items.SingleOrDefault(e => e.Id == id);
}