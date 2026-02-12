using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    public UnityEvent<bool> OnGroundChanged;
    
    public AudioClip groundSound;
    public float volumeScale = 0.3f;


    private void Awake()
    {
        OnGroundChanged = new();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            OnGroundChanged?.Invoke(true);
            SoudManager.PlaySfx(groundSound, volumeScale);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            OnGroundChanged?.Invoke(false);
        }
    }
}
