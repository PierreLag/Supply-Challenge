using System.Collections.Generic;
using UnityEngine;

public class ContractManager : MonoBehaviour
{
    private Dictionary<ResourceType, float> cooldownDictionary;
    private Dictionary<ResourceType, Contract> contractDictionary;

    void Start()
    {
        cooldownDictionary = new Dictionary<ResourceType, float>();
        cooldownDictionary.Add(ResourceType.FOOD, 0f);
        cooldownDictionary.Add(ResourceType.AMMO, 0f);
        cooldownDictionary.Add(ResourceType.MANPOWER, 0f);
        cooldownDictionary.Add(ResourceType.MEDICAL, 0f);

        contractDictionary = new Dictionary<ResourceType, Contract>();
        contractDictionary.Add(ResourceType.FOOD, null);
        contractDictionary.Add(ResourceType.AMMO, null);
        contractDictionary.Add(ResourceType.MANPOWER, null);
        contractDictionary.Add(ResourceType.MEDICAL, null);
    }

    void Update()
    {
        float timePassed = Time.deltaTime;
        foreach(KeyValuePair<ResourceType, float> line in cooldownDictionary)
        {
            cooldownDictionary[line.Key] = (timePassed > line.Value) ? 0f : line.Value - timePassed;
            if (cooldownDictionary[line.Key] == 0f)
            {
                contractDictionary[line.Key] = null;
            }
        }
    }

    public Contract GetContract (ResourceType type)
    {
        try
        {
            return contractDictionary[type];
        }
        catch (System.Exception)
        {
            return null;
        }
    }

    public void AddContract (ResourceType type, int value)
    {
        Contract contract = new Contract(type, value);
        contractDictionary[type] = contract;
        cooldownDictionary[type] = Contract.GetCooldown();
    }
}
