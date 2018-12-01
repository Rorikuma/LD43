using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Defender_Movement : MonoBehaviour {

    Rigidbody2D rb;
    Transform myTransform;
    AI_Defender brain;
    AI_Stats stats;

    Vector2 targetPosition = Vector2.zero;

    bool reachedDestination = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        brain = GetComponent<AI_Defender>();
        stats = GetComponent<AI_Stats>();
    }

    public void GetNewPosition(Vector2 pos)
    {
        reachedDestination = false;
        targetPosition = pos;
        if(brain.Assignment == Side.Right)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void Update()
    {
        if (reachedDestination == false)
        {
            if ((brain.Assignment == Side.Right && myTransform.position.x > targetPosition.x) ||
                (brain.Assignment == Side.Left && myTransform.position.x < targetPosition.x) ||
                Vector2.Distance(myTransform.position, targetPosition) < 8)
            {
                myTransform.position = targetPosition;
                reachedDestination = true;
            }
            else
            {
                rb.velocity = transform.right * stats.MovementSpeed;
            }
        }
    }

}
