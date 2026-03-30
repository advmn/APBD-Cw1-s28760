using APBD_Cw1_s28760.Models;

namespace APBD_Cw1_s28760.Services.Interfaces;

public interface IEquipmentRepository
{
    void Add(Equipment equipment);
    IEnumerable<Equipment> GetAll();
    Equipment? GetById(Guid id);
}