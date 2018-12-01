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

    AI_Defender_Movement movement;

    Transform GetFurthest(GameObject[] gs)
    {
        GameObject t = null;
        float longestDistance = 0;
        float distance = 0;

        foreach (GameObject g in gs)
        {
            distance = Vector2.Distance(myTransform.position, g.transform.position);
            if (distance > longestDistance)
            {
                longestDistance = distance;
                t = g;
            }
        }

        return t.transform;
    }

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

    Transform GetFurthestWall(Side s)
    {
        GameObject[] walls;
        Transform target = null;

        if (s == Side.Right)
        {
            walls = GameObject.FindGameObjectsWithTag("WallRight");
        }
        else
        {
            walls = GameObject.FindGameObjectsWithTag("WallLeft");
        }
        if(walls.Length == 0)
        {
            // TODO: Find furthest building.
        }
        else
        {
            target = GetFurthest(walls);
        }

        return target;
    }

    IEnumerator FindClosestEnemy(float t)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        TargetEnemy = GetClosest(enemies);

        yield return new WaitForSeconds(t);

        StartCoroutine(FindClosestEnemy(t));
    }

    public void ChangeAssignment(Side s)
    {
        Assignment = s;

        movement.GetNewPosition(GetFurthestWall(s).position);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        movement = GetComponent<AI_Defender_Movement>();
        stats = GetComponent<AI_Stats>();
        attack = GetComponent<AI_Defender_Attack>();
    }

    private void Start()
    {
        StartCoroutine(FindClosestEnemy(0.3f));
    }

    private void Update()
    {
        if(Vector2.Distance(myTransform.position, TargetEnemy.position) < stats.AttackRange)
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
