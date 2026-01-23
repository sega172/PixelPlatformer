using UnityEngine;

public class Player : MonoBehaviour
{

    public float force = 1500;
    public float forceInAir = 500;
    public float decelerationforce = 500;
    public float jumpSpeed = 16;

    public float MaxSpeedX = 8;
    public float MaxSpeedY = 30;

    public AudioClip jumpSound;
    public AudioClip doubleJumpSound;

    public AudioSource RunAudioSource;

    public GroundCheck groundCheck;

    Rigidbody2D rb;

    public bool grounded = false;
    public int jumpsAvailable = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck.OnGroundChanged.AddListener(ChangeGroundedState);
    }

    void ChangeGroundedState(bool grounded)
    {
        this.grounded = grounded;
        if (grounded) jumpsAvailable = 2;
        if (!grounded && jumpsAvailable == 2) jumpsAvailable = 0;

    }
    public void SetJumpsAvailable(int amount)
    {
        jumpsAvailable = amount;
    }

    float h;
    bool jump;
    private void Update()
    {
        h = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) h -= 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) h += 1;
        jump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);


        JumpProcessing();
        HorizontalMovement(h);
        ApplyVelocityConstraints();
    }
    private void JumpProcessing()
    {
        
        if (jump && jumpsAvailable > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);

            switch (jumpsAvailable)
            {
                case 2:
                    SoudManager.PlaySfx(jumpSound);
                    break;
                case 1:
                    SoudManager.PlaySfx(doubleJumpSound);
                    break;
            }
            jumpsAvailable--;
        }
    }

    private void HorizontalMovement(float h)
    {
        
        

        
        if(grounded) rb.AddForce(Vector2.right * force * Time.deltaTime * h);
        else         rb.AddForce(Vector2.right * forceInAir * Time.deltaTime * h);



        if (h == 0)
        {//замедление
            rb.AddForce(new Vector2(-rb.linearVelocity.x * Time.deltaTime * decelerationforce, 0));
        }

        //поворот
        if (h > 0) transform.localScale = new Vector2(1, 1);
        if (h < 0) transform.localScale = new Vector2(-1, 1);

        if (h != 0 && grounded)
        {
            if (!RunAudioSource.isPlaying) RunAudioSource.Play();
        }
        else
        {
            if (RunAudioSource.isPlaying) RunAudioSource.Stop();
        }
    }

    private void ApplyVelocityConstraints()
    {
        float clampedX = Mathf.Clamp(rb.linearVelocity.x, -MaxSpeedX, MaxSpeedX);
        float clampedY = Mathf.Clamp(rb.linearVelocity.y, -MaxSpeedY, MaxSpeedY);

        rb.linearVelocity = new Vector2(clampedX, clampedY);
    }
}
