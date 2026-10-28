using UnityEngine;

public class Contract
{
    private ResourceType resourceType;
    private int amount;
    private static float cooldown = 60f;

    public Contract(ResourceType resourceType, int amount)
    {
        this.resourceType = resourceType;
        this.amount = amount;
    }

    public ResourceType GetResourceType()
    {
        return resourceType;
    }

    public int GetAmount()
    {
        return amount;
    }

    public static float GetCooldown()
    {
        return cooldown;
    }
}
