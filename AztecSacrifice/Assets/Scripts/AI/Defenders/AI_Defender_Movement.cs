using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Defender_Movement : MonoBehaviour {

    Rigidbody2D rb;
    Transform myTransform;
    AI_Defender brain;
    AI_Stats stats;
    CapsuleCollider2D capsule;

    Vector2 targetPosition = Vector2.zero;

    bool reachedDestination = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Defender" && collision.gameObject.GetComponent<AI_Defender>().Assignment != brain.Assignment)
        {
            StartCoroutine(TurnOffCollision(0.5f));
        }
    }

    IEnumerator TurnOffCollision(float t)
    {
        capsule.enabled = false;
        float grav = rb.gravityScale;
        rb.gravityScale = 0;

        yield return new WaitForSeconds(t);

        rb.gravityScale = grav;
        capsule.enabled = true;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        brain = GetComponent<AI_Defender>();
        stats = GetComponent<AI_Stats>();
        capsule = GetComponent<CapsuleCollider2D>();
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
