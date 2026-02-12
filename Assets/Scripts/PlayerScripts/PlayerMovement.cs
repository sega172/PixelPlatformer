using System;
using UnityEngine;

public class PlayerMovement
{
    //Moving
    public float horizontalForce = 1500;
    public float horizontalForceInAir = 500;
    public float horizontalDecelerationForce = 500;

    public float MaxSpeedX = 8;
    public float MaxSpeedY = 30;

    public Rigidbody2D rb;

    GroundCheck groundCheck;
    private bool grounded;
    Transform playerTransform;

    public event Action OnStartRunning, OnStopRunning;
    
    public PlayerMovement(Rigidbody2D rigidbody2D, GroundCheck groundCheck)
    {
        rb = rigidbody2D;
        this.groundCheck = groundCheck;
        groundCheck.OnGroundChanged.AddListener(UpdateGroundedState);

        //подписаться на граундчек
    }

    ~PlayerMovement()
    {
        groundCheck.OnGroundChanged.RemoveListener(UpdateGroundedState);
    }

    void UpdateGroundedState(bool newState)
    {
        
    }
    
    private void HorizontalMovement(float horizontalAxis)
    {
        if (grounded) rb.AddForce(Vector2.right * (horizontalForce * Time.deltaTime * horizontalAxis));
        else rb.AddForce(Vector2.right * (horizontalForceInAir * Time.deltaTime * horizontalAxis));


        if (horizontalAxis == 0)
        {
            //����������
            rb.AddForce(new Vector2(-rb.linearVelocity.x * Time.deltaTime * horizontalDecelerationForce, 0));
        }

        if (horizontalAxis > 0) playerTransform.localScale = new Vector2(1, 1);
        if (horizontalAxis < 0) playerTransform.localScale = new Vector2(-1, 1);
        
    }

    private void ApplyVelocityConstraints()
    {
        var clampedX = Mathf.Clamp(rb.linearVelocity.x, -MaxSpeedX, MaxSpeedX);
        var clampedY = Mathf.Clamp(rb.linearVelocity.y, -MaxSpeedY, MaxSpeedY);

        rb.linearVelocity = new Vector2(clampedX, clampedY);
    }
}