using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class LevelManager : MonoBehaviour
{
    static protected LevelManager s_instance = null;

    private int depth;
    private List<AllyArmyMono> armyList;
    private HQMono hq;

    [SerializeField]
    private static AllyArmyMono allyArmyTemplate;
    [SerializeField]
    private float resourceTransferTime = 5f;

    void Awake()
    {
        if (s_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            s_instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void Initialise()
    {
        s_instance.depth = 0;

        s_instance.armyList = new List<AllyArmyMono>();
        s_instance.armyList.Add(GameObject.Instantiate(allyArmyTemplate));    // Need to make a linked object to attach & instantiate

        s_instance.hq = GameObject.FindGameObjectsWithTag("HQ")[0].GetComponent<HQMono>();    // Give HQ tag to gameobject with the HQ script
    }

    public static int GetDepth()
    {
        return s_instance.depth;
    }

    public static void GoDeeper()
    {
        s_instance.depth++;
    }

    public static AllyArmyMono GetArmy(int depth)
    {
        if (depth > s_instance.depth)
            return null;

        return s_instance.armyList[depth];
    }

    public static async void MoveResource(int armyDepthSource, int armyDepthDest, ResourceType resourceType, int amount)
    {
        ResourcesController armyRscSrc = GetArmy(armyDepthSource).GetResourcesController();
        ResourcesController armyRscDst = GetArmy(armyDepthDest).GetResourcesController();

        switch (resourceType)
        {
            case ResourceType.FOOD:
                armyRscSrc.FoodRemove(amount);
                break;
            case ResourceType.AMMO:
                armyRscSrc.AmmoRemove(amount);
                break;
            case ResourceType.MANPOWER:
                armyRscSrc.ManpowerRemove(amount);
                break;
            case ResourceType.MEDICAL:
                armyRscSrc.MedicalRemove(amount);
                break;
            default: break;
        }

        await Task.Delay((int)(s_instance.resourceTransferTime * (float)Math.Abs(armyDepthDest - armyDepthSource) * 100f));

        switch (resourceType)
        {
            case ResourceType.FOOD:
                armyRscDst.FoodAdd(amount);
                break;
            case ResourceType.AMMO:
                armyRscDst.AmmoAdd(amount);
                break;
            case ResourceType.MANPOWER:
                armyRscDst.ManpowerAdd(amount);
                break;
            case ResourceType.MEDICAL:
                armyRscDst.MedicalAdd(amount);
                break;
            default: break;
        }
    }

    public static async void MoveResourceFromHQ(int armyDepthDest, ResourceType resourceType, int amount)
    {
        ResourcesController hqRsc = s_instance.hq.GetResourcesController();
        ResourcesController armyRscDst = GetArmy(armyDepthDest).GetResourcesController();

        switch (resourceType)
        {
            case ResourceType.FOOD:
                hqRsc.FoodRemove(amount);
                break;
            case ResourceType.AMMO:
                hqRsc.AmmoRemove(amount);
                break;
            case ResourceType.MANPOWER:
                hqRsc.ManpowerRemove(amount);
                break;
            case ResourceType.MEDICAL:
                hqRsc.MedicalRemove(amount);
                break;
            default: break;
        }

        await Task.Delay((int)(s_instance.resourceTransferTime * (float)(armyDepthDest + 1) * 100f));

        switch (resourceType)
        {
            case ResourceType.FOOD:
                armyRscDst.FoodAdd(amount);
                break;
            case ResourceType.AMMO:
                armyRscDst.AmmoAdd(amount);
                break;
            case ResourceType.MANPOWER:
                armyRscDst.ManpowerAdd(amount);
                break;
            case ResourceType.MEDICAL:
                armyRscDst.MedicalAdd(amount);
                break;
            default: break;
        }
    }
}
