using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Right, Left, None };

public class AI_Defender : MonoBehaviour {
    
    Rigidbody2D rb;
    Transform myTransform;
    
    public Side Assignment = Side.None;
    Transform TargetWall;

    AI_Defender_Movement movement;

    Transform GetFurthestWall(Side s)
    {
        GameObject[] walls;
        GameObject target = null;

        if (s == Side.Right)
        {
            walls = GameObject.FindGameObjectsWithTag("WallRight");
        }
        else
        {
            walls = GameObject.FindGameObjectsWithTag("WallLeft");
        }
        //Debug.Log(walls);
        if(walls.Length == 0)
        {
            // TODO: Find furthest building.
        }
        else
        {
            float longestDistance = 0;
            float distance = 0;

            foreach(GameObject g in walls)
            {
                distance = Vector2.Distance(myTransform.position, g.transform.position);
                if (distance > longestDistance)
                {
                    longestDistance = distance;
                    target = g;
                }
            }
        }

        return target.transform;
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
    }

    private void Update()
    {
        
    }

}
