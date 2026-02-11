using System;
using UnityEngine;


public class TriggerDamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] LayerMask targetLayers;

    public event Action DealtDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isTarget = (targetLayers & (1 << collision.gameObject.layer)) != 0;
        if (!isTarget) return;

        if (collision.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.TakeDamage(damage);
            DealtDamage?.Invoke();
        }
    }
}
