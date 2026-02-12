using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public Health healthComponent;

    


    //Jumping
    public float jumpSpeed = 16;
    
    //Visuals
    public AudioClip jumpSound;
    public AudioClip doubleJumpSound;

    public AudioSource RunAudioSource;

    //GC
    public GroundCheck groundCheck;

    Rigidbody2D rb;

    public bool grounded = false;
    public int jumpsAvailable = 0;


    private void Awake()
    {
        healthComponent = new Health(3, 3);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck.OnGroundChanged.AddListener(ChangeGroundedState);

    }

    private void ChangeGroundedState(bool newGroundedState)
    {
        grounded = newGroundedState;
        if (grounded) jumpsAvailable = 2;
        if (!grounded && jumpsAvailable == 2) jumpsAvailable = 0;
        
  
    }
    public void SetJumpsAvailable(int amount) => jumpsAvailable = amount;

    private float _horizontalAxis;
    private bool _jumpPressed;
    private void Update()
    {
        _horizontalAxis = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) _horizontalAxis -= 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) _horizontalAxis += 1;
        _jumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);


        JumpProcessing();

    }
    private void JumpProcessing()
    {
        if (!_jumpPressed || jumpsAvailable <= 0) return;
        
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

    

    public void TakeDamage(int amount)
    {
        healthComponent.TakeDamage(amount);
    }
}
