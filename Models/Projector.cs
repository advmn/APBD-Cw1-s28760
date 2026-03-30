namespace APBD_Cw1_s28760.Models;

public class Projector : Equipment
{
    public int Lumens { get; }
    public string Resolution { get; }

    public Projector(string name, int lumens, string resolution) : base(name)
    {
        Lumens = lumens;
        Resolution = resolution;
    }

    public override string ToString()
        => base.ToString() + $" | {Lumens} lm, {Resolution}";
}