using System;
using UnityEngine;


public class TriggerDamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] LayerMask targetLayers;

    public event Action DealtDamage;

    private void OnTriggerEnter2D(Collider2D collision) => ProcessCollision(collision.gameObject);
    private void OnCollisionEnter2D(Collision2D collision) => ProcessCollision(collision.gameObject);

    private void ProcessCollision(GameObject other)
    {
        bool isTarget = (targetLayers & (1 << other.layer)) != 0;
        if (!isTarget) return;

        if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.TakeDamage(damage);
            DealtDamage?.Invoke();
        }
    }
}
