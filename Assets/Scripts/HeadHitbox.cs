using UnityEngine;

public class HeadHitbox : MonoBehaviour
{
    public AudioClip hitSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            SoudManager.PlaySfx(hitSound, 1f);
        }
    }
}
