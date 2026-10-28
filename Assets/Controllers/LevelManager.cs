using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    static protected LevelManager s_instance = null;

    private int depth;
    private List<AllyArmyMono> armyList;

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
        // s_instance.armyList.Add(AllyArmyMono.Instantiate());     Need to make a linked object to attach & instantiate

        // More as things go
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
        return s_instance.armyList[depth];
    }
}
