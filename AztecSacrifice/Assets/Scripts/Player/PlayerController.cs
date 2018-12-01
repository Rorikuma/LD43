using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    float moveTimer = 0;

    float currentSpeed = 0;
    float lastAxis = 1;
    float slowdownSpeed = 1.05f;
    bool slowdown = false;

    public float maxSpeed = 10;
    public float acceleration = 30;
    public float jumpHeight = 750;
    public int jumpLimit = 1;
    public float gravityScale = 3;
    public float maxWalkableSlopeAngle = 60;

    public string horizontalAxisName;
    public string verticalAxisName;

    PlayerMovement movement;
    
	void Start () {

        movement = GetComponent<PlayerMovement>();
        movement.ChangeGravityScale(gravityScale);

	}
    
    void CalculateSpeed()
    {
        currentSpeed = maxSpeed * Input.GetAxis(horizontalAxisName);
    }

    public void TouchMovement(float axis)
    {
        currentSpeed = maxSpeed * axis;
        movement.GetMovement(currentSpeed);
    }
    
	void Update () {

        // Movement
        CalculateSpeed();

        movement.GetMovement(currentSpeed);
                
	}
}
