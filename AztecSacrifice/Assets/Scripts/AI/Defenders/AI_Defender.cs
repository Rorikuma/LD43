using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Right, Left, None };

public class AI_Defender : MonoBehaviour {

    public AIState State = AIState.Moving;

    Rigidbody2D rb;
    Transform myTransform;
    AI_Stats stats;
    AI_Defender_Attack attack;
    
    public Side Assignment = Side.None;
    Transform TargetWall;
    Transform TargetEnemy;

    UnitManager um;
    Vector2 furthestBuilding;

    AI_Defender_Movement movement;

    //Transform GetFurthest(GameObject[] gs)
    //{
    //    GameObject t = null;
    //    float longestDistance = 0;
    //    float distance = 0;

    //    foreach (GameObject g in gs)
    //    {
    //        distance = Vector2.Distance(myTransform.position, g.transform.position);
    //        if (distance > longestDistance)
    //        {
    //            longestDistance = distance;
    //            t = g;
    //        }
    //    }

    //    return t.transform;
    //}

    Transform GetClosest(GameObject[] gs)
    {
        GameObject t = null;
        float shortestDistance = Mathf.Infinity;
        float distance = Mathf.Infinity;

        foreach (GameObject g in gs)
        {
            distance = Vector2.Distance(myTransform.position, g.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                t = g;
            }
        }

        return t.transform;
    }

    //Transform GetFurthestWall(Side s)
    //{
    //    GameObject[] walls;
    //    Transform target = null;

    //    if (s == Side.Right)
    //    {
    //        walls = GameObject.FindGameObjectsWithTag("WallRight");
    //    }
    //    else
    //    {
    //        walls = GameObject.FindGameObjectsWithTag("WallLeft");
    //    }
    //    if(walls.Length == 0)
    //    {
    //        // TODO: Find furthest building.
    //    }
    //    else
    //    {
    //        target = GetFurthest(walls);
    //    }

    //    return target;
    //}

    IEnumerator FindClosestEnemy(float t)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            TargetEnemy = GetClosest(enemies);
        }

        yield return new WaitForSeconds(t);

        StartCoroutine(FindClosestEnemy(t));
    }

    public void ChangeAssignment(Side s)
    {
        Assignment = s;

        //movement.GetNewPosition(GetFurthestWall(s).position);
        movement.GetNewPosition(GetFurthestBuilding());
    }

    Vector2 GetFurthestBuilding()
    {
        Vector2 furthest;
        if(Assignment == Side.Right)
        {
            furthest = um.furthestBuildingOnRight;
        }
        else
        {
            furthest = um.furthestBuildingOnLeft;
        }

        return furthest;
    }

    public void GetFurthestBuilding(Vector2 v)
    {
        if (furthestBuilding != v)
        {
            furthestBuilding = v;
            movement.GetNewPosition(furthestBuilding);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        movement = GetComponent<AI_Defender_Movement>();
        stats = GetComponent<AI_Stats>();
        attack = GetComponent<AI_Defender_Attack>();
        um = FindObjectOfType<UnitManager>();
    }

    private void Start()
    {
        um.RegisterDefender(GetComponent<AI_Defender>());

        StartCoroutine(FindClosestEnemy(0.3f));
    }

    private void Update()
    {
        if(TargetEnemy != null && Vector2.Distance(myTransform.position, TargetEnemy.position) < stats.AttackRange)
        {
            State = AIState.Attacking;
        }
        else if(movement.reachedDestination == false)
        {
            State = AIState.Moving;
        }
        else
        {
            State = AIState.Idle;
        }
    }

}
