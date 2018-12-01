using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attacker_Movement : MonoBehaviour {

    Rigidbody2D rb;
    Transform myTransform;
    AI_Stats stats;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<AI_Stats>();
    }

    public void Move()
    {
        rb.velocity = myTransform.right * stats.MovementSpeed;
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }

}
