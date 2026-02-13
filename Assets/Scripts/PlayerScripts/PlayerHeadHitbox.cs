using UnityEngine;

public class PlayerHeadHitbox : MonoBehaviour
{
    public AudioClip hitSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            SoundManager.PlaySfx(hitSound, 1f);
        }
    }
}