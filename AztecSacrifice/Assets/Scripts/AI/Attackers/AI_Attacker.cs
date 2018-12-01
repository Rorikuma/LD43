﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState { Moving, Idle, Attacking }

public class AI_Attacker : MonoBehaviour {

    public AIState State = AIState.Moving;

    AI_Attacker_Movement movement;

    public Transform target;
    Transform shrine;
    Transform myTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "WallRight" || collision.gameObject.tag == "WallLeft" 
            || collision.gameObject.tag == "Defender")
        {
            State = AIState.Attacking;
            movement.Stop();
            target = collision.gameObject.transform;
            // TODO: Attack.
        }
    }
    
    void FaceTowardsShrine()
    {
        if(myTransform.position.x > shrine.position.x)
        {
            // Face left.
            myTransform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            myTransform.eulerAngles = Vector3.zero;
        }
    }

    private void Awake()
    {
        movement = GetComponent<AI_Attacker_Movement>();
        myTransform = GetComponent<Transform>();
        shrine = GameObject.Find("Shrine").transform;
    }

    private void Start()
    {
        FaceTowardsShrine();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(State == AIState.Moving)
        {
            movement.Move();
        }
    }

}
