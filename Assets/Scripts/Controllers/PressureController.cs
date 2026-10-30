using UnityEngine;

public class PressureController
{
    private int pressure;
    private int pressureMax;

    public PressureController()
    {
        pressure = 0;
        pressureMax = 100;
    }

    public void PressureAdd(int value)
    {
        pressure = (pressure + value > pressureMax) ? pressureMax : pressure + value;
    }

    public void PressureRemove(int value)
    {
        pressure = (pressure + value < 0) ? 0 : pressure + value;
    }

    public void PressureMaxAdd(int value)
    {
        pressureMax += value;
    }

    public void PressureMaxRemove(int value)
    {
        pressureMax = (value > pressureMax) ? 0 : pressureMax - value;
    }

    public int GetPressure()
    {
        return pressure;
    }

    public int GetPressureMax()
    {
        return pressureMax;
    }
}
