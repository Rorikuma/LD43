using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType { House, Wall, Farm }

public class Tile : MonoBehaviour {
    
    GameObject activeBuilding = null;
    BuildingType type;

    UnitManager um;
    PlayerStats pStats;

    public void BuildHouse(GameObject HousePrefab, bool free)
    {
        if (activeBuilding == null)
        {
            if (free || pStats.Gold >= pStats.HouseCost)
            {
                type = BuildingType.House;
                activeBuilding = Instantiate(HousePrefab, transform.position, Quaternion.identity, this.transform);
                if (free == false)
                {
                    pStats.ChangeGold(type, -1);
                }
            }
        }
    }

    public void BuildWall(GameObject WallPrefab, bool free)
    {
        if (activeBuilding == null)
        {
            if (free || pStats.Gold >= pStats.WallCost)
            {
                type = BuildingType.Wall;
                activeBuilding = Instantiate(WallPrefab, transform.position, Quaternion.identity, this.transform);
                if (free == false)
                {
                    pStats.ChangeGold(type, -1);
                }
            }
        }
    }

    public void BuildFarm(GameObject FarmPrefab, bool free)
    {
        if (activeBuilding == null)
        {
            if (free || pStats.Gold >= pStats.FarmCost)
            {
                type = BuildingType.Farm;
                activeBuilding = Instantiate(FarmPrefab, transform.position, Quaternion.identity, this.transform);
                if (free == false)
                {
                    pStats.ChangeGold(type, -1);
                }
            }
        }
    }

    public void DestroyBuilding()
    {
        if (activeBuilding != null)
        {
            pStats.ChangeGold(type, 1);
            um.DeregisterBuilding(activeBuilding.transform);
            Destroy(activeBuilding);
            activeBuilding = null;
        }
    }
    
    private void Start()
    {
        um = FindObjectOfType<UnitManager>();
        pStats = FindObjectOfType<PlayerStats>();
    }

}
