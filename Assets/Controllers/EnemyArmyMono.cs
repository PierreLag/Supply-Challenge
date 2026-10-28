using UnityEngine;

public class EnemyArmyMono : MonoBehaviour
{
    private int node;
    private int maxWave;
    private int currentWave;

    private float waveCd;
    private float timeElapsed;

    private bool waveOngoing;

    private float waveStrength;
    private AllyArmyMono defendingArmy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxWave = LevelManager.GetDepth() * 2;
        currentWave = 0;
        
        waveCd = 25f - LevelManager.GetDepth() * 2;
        timeElapsed = 0f;

        waveOngoing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!waveOngoing)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= waveCd)
            {
                waveOngoing = true;
                timeElapsed = 0f;

                currentWave++;
                waveStrength = 15f * node;

                if (defendingArmy != null)
                {
                    defendingArmy.SetAttacked(true);
                }
                else
                {
                    AttackNode(node);
                }
            }
        }
        else
        {
            float defPressure = defendingArmy.GetPressureController().GetPressure();
            float defPressureMax = defendingArmy.GetPressureController().GetPressureMax();
            waveStrength -= Time.deltaTime * defendingArmy.GetResourcesController().GetManpower() * (defPressureMax - defPressure) / defPressureMax;

            if (waveStrength <= 0f)
            {
                defendingArmy.SetAttacked(false);
                waveOngoing = false;
            }
        }
    }

    public void AttackNode(int node)
    {
        defendingArmy = LevelManager.GetArmy(node);
        defendingArmy.SetAttacked(true);
    }
}
