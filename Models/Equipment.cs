using APBD_Cw1_s28760.Enums;

namespace APBD_Cw1_s28760.Models;

public abstract class Equipment
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public EquipmentStatus Status { get; private set; } = EquipmentStatus.Available;

    protected Equipment(string name)
    {
        Name = name;
    }

    public void MarkUnavailable() => Status = EquipmentStatus.Unavailable;
    public void MarkAvailable() => Status = EquipmentStatus.Available;
    public void MarkRented() => Status = EquipmentStatus.Rented;

    public override string ToString()
        => $"{Id} | {Name} | {GetType().Name} | {Status}";
}