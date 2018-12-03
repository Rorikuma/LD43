using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTiles : MonoBehaviour {

    public string LayerName;

    public int MapSize = 10;
    public float BuildingX = 64;
    public float BuildingY = 64;
    public float TriggerSizeX = 48;

    public int StartingKids = 2;
    public int StartingAdults = 2;
    public int StartingOld = 2;

    public GameObject WallPrefab;
    public GameObject DefenderPrefab;
    public GameObject DefenderPrefabAdult;
    public GameObject DefenderPrefabOld;

    int wallPos = 8;

    UnitManager um;

	GameObject CreateTile(float x, int i, Transform parent)
    {
        GameObject g = new GameObject("BuildingTile[" + i + "]");
        g.layer = LayerMask.NameToLayer(LayerName);
        g.transform.parent = parent;
        g.transform.position = new Vector3(x, 0, 0);

        BoxCollider2D box = g.AddComponent<BoxCollider2D>();
        box.isTrigger = true;
        box.size = new Vector2(TriggerSizeX, BuildingY);
        box.offset = new Vector2(0, BuildingY / 2);

        g.AddComponent<Tile>();

        return g;
    }

    private void Start()
    {
        um = GetComponent<UnitManager>();

        if(MapSize%2 != 0)
        {
            MapSize += 1;
        }
        Transform p = new GameObject("BuildingTiles").transform;
        GameObject currentTile = null;

        for (int i = 0; i < MapSize; i++)
        {
            if (i != (MapSize / 2) - 1)
            {
                currentTile = CreateTile(-BuildingX * (MapSize / 2) + (BuildingX * (i + 1)), i, p);
            }

            if(i == (MapSize/2 - wallPos - 1) || i == (MapSize / 2 + wallPos - 1))
            {
                currentTile.GetComponent<Tile>().BuildWall(WallPrefab, true);
            }
        }

        float offset = 8;
        GameObject instance;
        Vector3 pos = new Vector3(-offset * (StartingKids + StartingAdults + StartingOld) / 2, 0, 0);

        for (int i = 0; i < StartingKids; i++)
        {
            Instantiate(DefenderPrefab, pos, Quaternion.identity);
            pos += new Vector3(offset, 0, 0);
        }

        for (int i = 0; i < StartingAdults; i++)
        {
            instance = Instantiate(DefenderPrefabAdult, pos, Quaternion.identity);
            pos += new Vector3(offset, 0, 0);

        }

        for (int i = 0; i < StartingOld; i++)
        {
            instance = Instantiate(DefenderPrefabOld, pos, Quaternion.identity);
            pos += new Vector3(offset, 0, 0);
        }
    }

}
