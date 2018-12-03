using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

    List<AI_Defender> defendersUnassigned;
    List<AI_Defender> defendersRight;
    List<AI_Defender> defendersLeft;
    List<Transform> buildings;

    public Vector2 furthestBuildingOnRight { get; protected set; }
    public Vector2 furthestBuildingOnLeft { get; protected set; }

    float furthestDistanceRight = 0;
    float furthestDistanceLeft = 0;

    Vector2 shrinePosition = Vector2.zero;

    public void DeregisterDefender(AI_Defender d)
    {
        if (defendersUnassigned.Contains(d))
        {
            defendersUnassigned.Remove(d);
        }
        else if (defendersLeft.Contains(d))
        {
            defendersLeft.Remove(d);
        }
        else if (defendersRight.Contains(d))
        {
            defendersRight.Remove(d);
        }
    }

    public void RegisterDefender(AI_Defender d)
    {
        if(d.Assignment == Side.None)
        {
            defendersUnassigned.Add(d);
        }
        else if(d.Assignment == Side.Left)
        {
            defendersLeft.Add(d);
        }
        else if(d.Assignment == Side.Right)
        {
            defendersRight.Add(d);
        }
    }
    public void DeregisterBuilding(Transform b)
    {
        if (buildings.Contains(b))
        {
            buildings.Remove(b);
        }
    }

    public void RegisterBuilding(Transform b)
    {
        if (buildings.Contains(b) == false)
        {
            buildings.Add(b);
        }

        CheckNewBuilding(b);
    }


    void CheckNewBuilding(Transform b)
    {
        float distance = Vector2.Distance(b.position, shrinePosition);
        if (b.position.x > shrinePosition.x)
        {
            if (distance > furthestDistanceRight)
            {
                furthestDistanceRight = distance;

                foreach (AI_Defender d in defendersUnassigned)
                {
                    if(d.Assignment == Side.Right)
                    {
                        //d.GetFurthestBuilding()
                    }
                }
            }
        }
        else
        {
            if(distance > furthestDistanceLeft)
            {
                furthestDistanceLeft = distance;
            }
        }
    }

    void FindFurthestBuilding()
    {
        // TODO: Find furthest building
    }

    void SetupBuildings()
    {
        buildings = new List<Transform>();
    }

    void SetupDefenders()
    {
        defendersUnassigned = new List<AI_Defender>();
        defendersRight = new List<AI_Defender>();
        defendersLeft = new List<AI_Defender>();
    }

    private void Start()
    {
        shrinePosition = GameObject.FindGameObjectWithTag("Shrine").transform.position;
        SetupDefenders();
        SetupBuildings();
        FindFurthestBuilding();
    }

}
