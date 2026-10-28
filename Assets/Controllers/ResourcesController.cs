using UnityEngine;

public class ResourcesController
{
    private int foodQty;
    private int ammoQty;
    private int manpowerQty;
    private int medicalQty;
    private int injuredQty;

    public ResourcesController()
    {
        foodQty = 0;
        ammoQty = 0;
        manpowerQty = 0;
        medicalQty = 0;
        injuredQty = 0;
    }

    public void InitializeRun()
    {
        foodQty = 100;
        ammoQty = 400;
        manpowerQty = 20;
        medicalQty = 10;
        injuredQty = 0;
    }

    public void FoodAdd(int value)
    {
        foodQty += value;
    }

    public void AmmoAdd(int value)
    {
        ammoQty += value;
    }

    public void ManpowerAdd(int value)
    {
        manpowerQty += value;
    }

    public void MedicalAdd(int value)
    {
        medicalQty += value;
    }

    public void InjuredAdd(int value)
    {
        ManpowerRemove(value);
        injuredQty += value;
    }

    public void FoodRemove(int value)
    {
        foodQty = (value > foodQty) ? 0 : foodQty - value;
    }

    public void AmmoRemove(int value)
    {
        ammoQty = (value > ammoQty) ? 0 : ammoQty - value;
    }

    public void ManpowerRemove(int value)
    {
        manpowerQty = (value > manpowerQty) ? 0 : manpowerQty - value;
    }

    public void MedicalRemove(int value)
    {
        medicalQty = (value > medicalQty) ? 0 : medicalQty - value;
    }

    public void InjuredRemove(int value)
    {
        int actualValue = (value > injuredQty) ? injuredQty : value;
        injuredQty -= actualValue;
        ManpowerAdd(actualValue);
    }

    public int GetFood()
    {
        return foodQty;
    }

    public int GetAmmo()
    {
        return ammoQty;
    }

    public int GetManpower()
    {
        return manpowerQty;
    }

    public int GetMedical()
    {
        return medicalQty;
    }

    public int GetInjured()
    {
        return injuredQty;
    }
}
