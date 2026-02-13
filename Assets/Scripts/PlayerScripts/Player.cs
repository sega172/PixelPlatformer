using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamagable
{
    public Health healthComponent;
    private PlayerMovement playerMovement;
    


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

    [SerializeField] private List<AudioClip> hurtSounds;


    private void Awake()
    {
        healthComponent = new Health(3, 3);
        healthComponent.Damaged += Damage;
        healthComponent.Died += Die;
    }
    private void OnDisable()
    {
        healthComponent.Damaged -= Damage;
        healthComponent.Died -= Die;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck.OnGroundChanged.AddListener(ChangeGroundedState);

        playerMovement = new(rb, groundCheck, transform);
    }

    void Damage()
    {
        SoundManager.PlaySfx(hurtSounds);
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void ChangeGroundedState(bool newGroundedState)
    {
        grounded = newGroundedState;
        if (grounded) jumpsAvailable = 2;
        if (!grounded && jumpsAvailable == 2) jumpsAvailable = 1;
        
  
    }
    public void SetJumpsAvailable(int amount) => jumpsAvailable = amount;

    private float _horizontalAxis;
    private bool _jumpPressed;
    private Animator _animator;

    private void Update()
    {
        _horizontalAxis = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) _horizontalAxis -= 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) _horizontalAxis += 1;
        _jumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);


        if (_horizontalAxis != 0 && !RunAudioSource.isPlaying && grounded)
        {
            RunAudioSource.Play();
        }
        else if (_horizontalAxis == 0 && RunAudioSource.isPlaying || !grounded)
        {
            RunAudioSource.Stop();
        }
        
        
        JumpProcessing();
        _animator.SetBool("IsRunning", _horizontalAxis != 0);
    }

    private void FixedUpdate()
    {
        playerMovement.HorizontalMovement(_horizontalAxis);
    }
    private void JumpProcessing()
    {
        if (!_jumpPressed || jumpsAvailable <= 0) return;
        
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        switch (jumpsAvailable)
        {
            case 2:
                SoundManager.PlaySfx(jumpSound);
                break;
            case 1:
                SoundManager.PlaySfx(doubleJumpSound);
                break;
        }
        jumpsAvailable--;
    }

    

    public void TakeDamage(int amount)
    {
        healthComponent.TakeDamage(amount);
    }
}
