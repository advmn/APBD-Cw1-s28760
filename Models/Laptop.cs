namespace APBD_Cw1_s28760.Models;

public class Laptop : Equipment
{
    public string Cpu { get; }
    public int RamGb { get; }

    public Laptop(string name, string cpu, int ramGb) : base(name)
    {
        Cpu = cpu;
        RamGb = ramGb;
    }

    public override string ToString()
        => base.ToString() + $" | CPU: {Cpu}, RAM: {RamGb}GB";
}