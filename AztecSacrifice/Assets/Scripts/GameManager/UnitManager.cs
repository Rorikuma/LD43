using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

    List<AI_Defender> defendersUnassigned;
    List<AI_Defender> defendersRight;
    List<AI_Defender> defendersLeft;
    List<Transform> buildings;

    public int LeftKids = 0;
    public int LeftAdults = 0;
    public int LeftOld = 0;

    public int RightKids = 0;
    public int RightAdults = 0;
    public int RightOld = 0;

    public int UnassignedKids = 0;
    public int UnassignedAdults = 0;
    public int UnassignedOld = 0;

    public bool UpdateUI = false;

    public Vector2 furthestBuildingOnRight { get; protected set; }
    public Vector2 furthestBuildingOnLeft { get; protected set; }

    float furthestDistanceRight = 0;
    float furthestDistanceLeft = 0;

    Vector2 shrinePosition = Vector2.zero;


    public void NotEnoughFood(int food)
    {
        foreach (AI_Defender d in defendersUnassigned)
        {
            d.gameObject.GetComponent<AI_Stats>().TakeDamage(food);
        }
        foreach (AI_Defender d in defendersLeft)
        {
            d.gameObject.GetComponent<AI_Stats>().TakeDamage(food);
        }
        foreach (AI_Defender d in defendersRight)
        {
            d.gameObject.GetComponent<AI_Stats>().TakeDamage(food);
        }
    }

    void TryToAssign(List<AI_Defender> list, Phase a, Side s, out bool success)
    {
        bool succ = false;

        foreach (AI_Defender d in list)
        {
            if (d.GetComponent<AI_Stats>().Age == a)
            {
                succ = true;
                d.ChangeAssignment(s);
                break;
            }
        }

        success = succ;
    }

    public void ChangeAssignment(Side newSide, Phase age)
    {
        bool unassignedWasAssigned = false;

        TryToAssign(defendersUnassigned, age, newSide, out unassignedWasAssigned);
        
        if (unassignedWasAssigned == false)
        {
            if (newSide == Side.Right)
            {
                TryToAssign(defendersLeft, age, newSide, out unassignedWasAssigned);
            }
            else
            {
                TryToAssign(defendersRight, age, newSide, out unassignedWasAssigned);
            }
        }
    }

    public void DeregisterDefender(AI_Defender d)
    {
        if (defendersUnassigned.Contains(d))
        {
            defendersUnassigned.Remove(d);
            switch (d.gameObject.GetComponent<AI_Stats>().Age)
            {
                case Phase.Kid:
                    UnassignedKids -= 1;
                    break;
                case Phase.Adult:
                    UnassignedAdults -= 1;
                    break;
                case Phase.Old:
                    UnassignedOld -= 1;
                    break;
            }
        }
        else if (defendersLeft.Contains(d))
        {
            defendersLeft.Remove(d);
            switch (d.gameObject.GetComponent<AI_Stats>().Age)
            {
                case Phase.Kid:
                    LeftKids -= 1;
                    break;
                case Phase.Adult:
                    LeftAdults -= 1;
                    break;
                case Phase.Old:
                    LeftOld -= 1;
                    break;
            }
        }
        else if (defendersRight.Contains(d))
        {
            defendersRight.Remove(d);
            switch (d.gameObject.GetComponent<AI_Stats>().Age)
            {
                case Phase.Kid:
                    RightKids -= 1;
                    break;
                case Phase.Adult:
                    RightAdults -= 1;
                    break;
                case Phase.Old:
                    RightOld -= 1;
                    break;
            }
        }
    }

    public void RegisterDefender(AI_Defender d)
    {
        if(d.Assignment == Side.None)
        {
            defendersUnassigned.Add(d);
            switch (d.gameObject.GetComponent<AI_Stats>().Age)
            {
                case Phase.Kid:
                    UnassignedKids += 1;
                    break;
                case Phase.Adult:
                    UnassignedAdults += 1;
                    break;
                case Phase.Old:
                    UnassignedOld += 1;
                    break;
            }
        }
        else if(d.Assignment == Side.Left)
        {
            defendersLeft.Add(d);
            switch (d.gameObject.GetComponent<AI_Stats>().Age)
            {
                case Phase.Kid:
                    LeftKids += 1;
                    break;
                case Phase.Adult:
                    LeftAdults += 1;
                    break;
                case Phase.Old:
                    LeftOld += 1;
                    break;
            }
        }
        else if(d.Assignment == Side.Right)
        {
            defendersRight.Add(d);
            switch (d.gameObject.GetComponent<AI_Stats>().Age)
            {
                case Phase.Kid:
                    RightKids += 1;
                    break;
                case Phase.Adult:
                    RightAdults += 1;
                    break;
                case Phase.Old:
                    RightOld += 1;
                    break;
            }
        }
    }

    public void DeregisterBuilding(Transform b)
    {
        if (buildings.Contains(b))
        {
            buildings.Remove(b);
        }

        OnDeregisterBuilding(b);
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
                furthestBuildingOnRight = b.position;

                foreach (AI_Defender d in defendersRight)
                {
                    d.GetFurthestBuilding(b.position);
                }
            }
        }
        else
        {
            if(distance > furthestDistanceLeft)
            {
                furthestDistanceLeft = distance;
                furthestBuildingOnLeft = b.position;

                foreach (AI_Defender d in defendersLeft)
                {
                    d.GetFurthestBuilding(b.position);
                }
            }
        }
    }

    void OnDeregisterBuilding(Transform b)
    {
        if(b.position.x == furthestBuildingOnLeft.x)
        {
            float furthest = 0;

            foreach (Transform t in buildings)
            {
                if(t.position.x < shrinePosition.x)
                {
                    if(Vector2.Distance(t.position, shrinePosition) > furthest)
                    {
                        furthestBuildingOnLeft = t.position;
                        furthest = Vector2.Distance(t.position, shrinePosition);
                    }
                }
            }

            foreach (AI_Defender d in defendersLeft)
            {
                d.GetFurthestBuilding(furthestBuildingOnLeft);
            }
        }
        else if (b.position.x == furthestBuildingOnRight.x)
        {
            float furthest = 0;

            foreach (Transform t in buildings)
            {
                if (t.position.x > shrinePosition.x)
                {
                    if (Vector2.Distance(t.position, shrinePosition) > furthest)
                    {
                        furthestBuildingOnRight = t.position;
                        furthest = Vector2.Distance(t.position, shrinePosition);
                    }
                }
            }

            foreach (AI_Defender d in defendersRight)
            {
                d.GetFurthestBuilding(furthestBuildingOnRight);
            }
        }
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
    }

}
