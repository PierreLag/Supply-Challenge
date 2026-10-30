using System;
using System.Collections.Generic;
using UnityEngine;

public class AllyArmyMono : MonoBehaviour
{
    private PressureController pressureController;
    private ResourcesController resourcesController;

    private float foodUseCd;
    private float ammoUseCd;
    private float manpowerCheckCd;
    private float medicalUseCd;
    private Dictionary<ResourceType, float> cdDictionary;

    private bool isAttacked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pressureController = new PressureController();
        resourcesController = new ResourcesController();

        foodUseCd = 20f;
        ammoUseCd = 5f;
        manpowerCheckCd = 10f;
        medicalUseCd = 20f;

        cdDictionary = new Dictionary<ResourceType, float>();
        cdDictionary.Add(ResourceType.FOOD, foodUseCd);
        cdDictionary.Add(ResourceType.AMMO, ammoUseCd);
        cdDictionary.Add(ResourceType.MANPOWER, manpowerCheckCd);
        cdDictionary.Add(ResourceType.MEDICAL, medicalUseCd);

        isAttacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        float timePassed = Time.deltaTime;
        foreach(KeyValuePair<ResourceType, float> line in cdDictionary)
        {
            cdDictionary[line.Key] = (timePassed > line.Value) ? 0f : line.Value - timePassed;
            if (cdDictionary[line.Key] == 0f)
            {
                switch (line.Key)
                {
                    case ResourceType.FOOD:
                        FoodCheck();
                        cdDictionary[line.Key] = foodUseCd;
                        break;
                    case ResourceType.AMMO:
                        AmmoCheck();
                        cdDictionary[line.Key] = ammoUseCd;
                        break;
                    case ResourceType.MANPOWER:
                        ManpowerCheck();
                        cdDictionary[line.Key] = manpowerCheckCd;
                        break;
                    case ResourceType.MEDICAL:
                        MedicalCheck();
                        cdDictionary[line.Key] = medicalUseCd;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public PressureController GetPressureController()
    {
        return pressureController;
    }

    public ResourcesController GetResourcesController()
    {
        return resourcesController;
    }

    void FoodCheck()
    {
        resourcesController.FoodRemove(resourcesController.GetManpower());
        if (resourcesController.GetFood() == 0)
        {
            pressureController.PressureAdd(25);
        }
    }

    void AmmoCheck()
    {
        if (isAttacked)
        {
            resourcesController.AmmoRemove(resourcesController.GetManpower());
            if (resourcesController.GetAmmo() == 0)
            {
                pressureController.PressureAdd(5);
            }
        }
    }

    void ManpowerCheck()
    {
        if (isAttacked)
        {
            pressureController.PressureAdd(1);
        }
        else
        {
            pressureController.PressureRemove(1);
        }

        float pressureRatio = (float)pressureController.GetPressure() / (float)pressureController.GetPressureMax();

        if (pressureRatio > 0.1f)
        {
            float rand = UnityEngine.Random.Range(0f, 1f);
            if (rand < pressureRatio)
            {
                int injuryAmount = (int)UnityEngine.Random.Range(1f, Math.Max(2.5f, (float)resourcesController.GetManpower() / 10f));
                resourcesController.MedicalAdd(injuryAmount);
            }
        }
    }

    void MedicalCheck()
    {
        if (resourcesController.GetInjured() > 0 && resourcesController.GetMedical() > 0)
        {
            float pressureRatio = (float)pressureController.GetPressure() / (float)pressureController.GetPressureMax();

            if (pressureRatio < 0.1f)
            {
                resourcesController.MedicalRemove(Math.Max(resourcesController.GetInjured(), resourcesController.GetMedical()));
                resourcesController.InjuredRemove(resourcesController.GetInjured());
            }
            else
            {
                float rand = UnityEngine.Random.Range(0f, 1f);
                if (rand < pressureRatio)
                {
                    resourcesController.MedicalRemove(1);
                    resourcesController.InjuredRemove(1);
                }
            }
        }
        
        if (resourcesController.GetMedical() == 0)
        {
            if (resourcesController.GetInjured() > 0)
            {
                float rand = UnityEngine.Random.Range(0f, 1f);
                if (rand < 0.4f)
                {
                    resourcesController.InjuredRemove(1);
                    resourcesController.ManpowerRemove(1);
                }
            }
            
            pressureController.PressureAdd(10);
        }
    }

    public void SetAttacked (bool value)
    {
        isAttacked = value;
    }
}
