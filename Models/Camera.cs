namespace APBD_Cw1_s28760.Models;

public class Camera : Equipment
{
    public string SensorType { get; }
    public bool HasStabilization { get; }

    public Camera(string name, string sensorType, bool hasStabilization) : base(name)
    {
        SensorType = sensorType;
        HasStabilization = hasStabilization;
    }

    public override string ToString()
        => base.ToString() + $" | Sensor: {SensorType}, Stabilization: {HasStabilization}";
}