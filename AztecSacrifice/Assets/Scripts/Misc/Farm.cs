using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {

    int spawnedDay = 0;

    PlayerStats pStats;
    GManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GManager>();
        pStats = FindObjectOfType<PlayerStats>();

        FindObjectOfType<UnitManager>().RegisterBuilding(this.transform);

        spawnedDay = gm.Day;
    }

    public void NewDay()
    {
        if(gm.Day == spawnedDay + 3)
        {
            IncreaseFood();
        }
    }

    void IncreaseFood()
    {
        pStats.ChangeFood();
    }

}
