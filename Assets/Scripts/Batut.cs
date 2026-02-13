using System;
using UnityEngine;

public class Batut : MonoBehaviour
{
    public float velocityY;
    public AudioClip boostClip;
    public bool resetJumps = true;
    public int jumpsToSet = 2;
    private void OnTriggerEnter2D(Collider2D collision) => ProcessCollision(collision.gameObject);

    private void OnCollisionEnter2D(Collision2D other) => ProcessCollision(other.gameObject);
    private void ProcessCollision(GameObject other)
    {
        if (!other.CompareTag("Player")) return;
        var rb = other.GetComponent<Rigidbody2D>();

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocityY);

        if(boostClip != null)
            SoundManager.PlaySfx(boostClip);

        if (resetJumps)
        {
            var player = other.gameObject.GetComponent<Player>();
            player.SetJumpsAvailable(2);
        }
    }
}
