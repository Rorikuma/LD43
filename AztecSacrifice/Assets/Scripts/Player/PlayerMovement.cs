using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    int jump = 0;
    bool jumped = false;
    [HideInInspector]
    public bool wantsToJump = false;
    float jumpTimer = 0;
    float verticalVelocity = 0;
    
    bool grounded = false;
    bool colliding = false;

    Vector2 move = Vector2.zero;

    PlayerController pc;
    Rigidbody2D rb;

    public void SetIsGrounded(bool isEnabled)
    {
        ChangeGravityScale(pc.gravityScale);
        grounded = isEnabled;
        StartCoroutine(E_SetIsGrounded());
    }

    IEnumerator E_SetIsGrounded()
    {
        yield return new WaitForSeconds(0.01f);
        ChangeGravityScale(pc.gravityScale);
        grounded = false;
        ChangeGravityScale(pc.gravityScale);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumped = false;
        if (Vector2.Angle(collision.contacts[0].normal, Vector2.up) >= -pc.maxWalkableSlopeAngle
            && Vector2.Angle(collision.contacts[0].normal, Vector2.up) <= pc.maxWalkableSlopeAngle)
        {
            grounded = true;
            jump = 0;
            //rb.gravityScale = 0;
        }
        colliding = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Vector2.Angle(collision.contacts[0].normal, Vector2.up) >= -pc.maxWalkableSlopeAngle
            && Vector2.Angle(collision.contacts[0].normal, Vector2.up) <= pc.maxWalkableSlopeAngle)
        {
            grounded = true;
            //jump = 0;
            rb.gravityScale = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.gravityScale = pc.gravityScale;
        jumpTimer = Time.time + 0.3f;
        grounded = false;
        colliding = false;
    }

    public void BoxHit(float t)
    {
        StartCoroutine(BoxHitCoroutine(t));
    }

    IEnumerator BoxHitCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    public void ChangeGravityScale(float grav)
    {
        rb.gravityScale = grav;
    }

    public void GetMovement(float speed)
    {
        move = new Vector2(speed, rb.velocity.y);
        if(speed < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(speed > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void PrepareJump()
    {
        if (jump < pc.jumpLimit)
        {
            wantsToJump = true;
        }
    }

    public void Jump()
    {
        jumped = true;
        wantsToJump = false;
        jump++;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * pc.jumpHeight);
    }

    void ExecuteMovement()
    {
        rb.velocity = move;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start () {

        pc = GetComponent<PlayerController>();

    }
    
    void FixedUpdate () {

        ExecuteMovement();
        
	}
    
}
