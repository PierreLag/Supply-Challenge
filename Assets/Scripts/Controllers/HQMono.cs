using UnityEngine;

public class HQMono : MonoBehaviour
{
    private ResourcesController resourcesController;

    [SerializeField]
    private float resupplyCooldown = 10f;
    private float timeElapsed;

    [SerializeField]
    private int foodRefuelAmount = 10;
    [SerializeField]
    private int ammoRefuelAmount = 50;
    [SerializeField]
    private int manpowerRefuelAmount = 2;
    [SerializeField]
    private int medicalRefuelAmount = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resourcesController = new ResourcesController();

        timeElapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= resupplyCooldown)
        {
            timeElapsed -= resupplyCooldown;
            Refuel();
        }
    }

    private void Refuel()
    {
        resourcesController.FoodAdd(foodRefuelAmount);
        resourcesController.AmmoAdd(ammoRefuelAmount);
        resourcesController.ManpowerAdd(manpowerRefuelAmount);
        resourcesController.MedicalAdd(medicalRefuelAmount);
    }

    public ResourcesController GetResourcesController()
    {
        return resourcesController;
    }
}
