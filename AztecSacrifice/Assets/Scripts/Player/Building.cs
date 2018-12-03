using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public GameObject HousePrefab;
    public GameObject WallPrefab;
    public GameObject FarmPrefab;

    GameObject collidingWith;

    void BuildHouse(GameObject g)
    {
        g.GetComponent<Tile>().BuildHouse(HousePrefab, false);
    }

    void BuildWall(GameObject g)
    {
        g.GetComponent<Tile>().BuildWall(WallPrefab, false);
    }

    void BuildFarm(GameObject g)
    {
        g.GetComponent<Tile>().BuildFarm(FarmPrefab, false);
    }

    private void DestroyBuilding()
    {
        collidingWith.GetComponent<Tile>().DestroyBuilding();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("BuildingTile"))
        {
            collidingWith = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("BuildingTile") && collidingWith == collision.gameObject)
        {
            collidingWith = null;
        }
    }
    
    private void Update()
    {
        if(collidingWith != null)
        {
            if (Input.GetButtonDown("BuildWall"))
            {
                BuildWall(collidingWith);
            }
            if (Input.GetButtonDown("BuildFarm"))
            {
                BuildFarm(collidingWith);
            }
            if (Input.GetButtonDown("BuildHouse"))
            {
                BuildHouse(collidingWith);
            }
            if (Input.GetButtonDown("DestroyBuilding"))
            {
                DestroyBuilding();
            }
        }
    }
}
