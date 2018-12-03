using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Right, Left, None };

public class AI_Defender : MonoBehaviour
{

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

    AI_Defender ai;
    
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
        um.DeregisterDefender(ai);
        Assignment = s;

        movement.GetNewPosition(GetFurthestBuilding());
        um.RegisterDefender(ai);
    }

    Vector2 GetFurthestBuilding()
    {
        Vector2 furthest;
        if (Assignment == Side.Right)
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
        ai = GetComponent<AI_Defender>();
    }

    private void Start()
    {
        um.RegisterDefender(ai);

        StartCoroutine(FindClosestEnemy(0.3f));
    }

    private void Update()
    {
        if (TargetEnemy != null && Vector2.Distance(myTransform.position, TargetEnemy.position) < stats.AttackRange)
        {
            State = AIState.Attacking;
        }
        else if (movement.reachedDestination == false)
        {
            State = AIState.Moving;
        }
        else
        {
            State = AIState.Idle;
        }
    }

}
