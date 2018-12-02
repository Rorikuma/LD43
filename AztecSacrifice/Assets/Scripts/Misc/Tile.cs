using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    
    GameObject activeBuilding = null;

    public void BuildHouse(GameObject HousePrefab)
    {
        if (activeBuilding == null)
        {
            activeBuilding = Instantiate(HousePrefab, transform.position, Quaternion.identity, this.transform);
        }
    }

    public void BuildWall(GameObject WallPrefab)
    {
        if (activeBuilding == null)
        {
            activeBuilding = Instantiate(WallPrefab, transform.position, Quaternion.identity, this.transform);
        }
    }

    public void BuildFarm(GameObject FarmPrefab)
    {
        if (activeBuilding == null)
        {
            activeBuilding = Instantiate(FarmPrefab, transform.position, Quaternion.identity, this.transform);
        }
    }

    public void DestroyBuilding()
    {
        Destroy(activeBuilding);
        activeBuilding = null;
    }
    
}
