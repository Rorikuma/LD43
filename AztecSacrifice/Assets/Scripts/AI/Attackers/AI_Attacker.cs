using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState { Moving, Idle, Attacking }

public class AI_Attacker : MonoBehaviour {

    public AIState State = AIState.Moving;

    AI_Attacker_Movement movement;

    public Transform target;
    Transform shrine;
    Transform myTransform;

    AI_Attacker attackerInFront;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "WallRight" || collision.gameObject.tag == "WallLeft" 
            || collision.gameObject.tag == "Defender")
        {
            State = AIState.Attacking;
            movement.Stop();
            target = collision.gameObject.transform;
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            if(collision.gameObject.GetComponent<AI_Attacker>().State == AIState.Attacking)
            {
                State = AIState.Idle;
                attackerInFront = collision.gameObject.GetComponent<AI_Attacker>();
                movement.Stop();
            }
        }
        else if(collision.gameObject.transform.root.tag == "Player")
        {
            State = AIState.Attacking;
            Debug.Log("Player");
            movement.Stop();
            target = collision.gameObject.transform.root;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.transform.root.tag == "Player")
        {
            target = null;
            State = AIState.Moving;
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
        if((target == null && State != AIState.Idle) || (attackerInFront != null && attackerInFront.State == AIState.Moving))
        {
            State = AIState.Moving;
        }
    }

    private void FixedUpdate()
    {
        if(State == AIState.Moving)
        {
            movement.Move();
        }
    }

}
