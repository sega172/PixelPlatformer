using UnityEngine;

public class Batut : MonoBehaviour
{
    public float velocityY;
    public AudioClip boostClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocityY);
            SoudManager.PlaySfx(boostClip);

            Player player = collision.GetComponent<Player>();
            player.SetJumpsAvailable(2);
        }
    }
}
