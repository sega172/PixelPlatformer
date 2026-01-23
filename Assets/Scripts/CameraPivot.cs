using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public Rigidbody2D targetRb;
    public float xMultiplier = 1.2f;
    public float yMultiplier = 0.3f;

    public float smoothTime = 0.5f;
    public Vector2 offset = Vector2.up;
    Vector2 velocity;


    
    void FixedUpdate()
    {
        Vector2 rbVelocity = targetRb.linearVelocity;
        rbVelocity = new Vector2(rbVelocity.x * xMultiplier, rbVelocity.y * yMultiplier);

        Vector2 target = (Vector2)targetRb.transform.position + rbVelocity + offset;
        transform.position = Vector2.SmoothDamp(transform.position, target, ref velocity, smoothTime);



        
    }
}
