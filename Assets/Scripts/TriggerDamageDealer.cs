using System;
using UnityEngine;


public class TriggerDamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] LayerMask targetLayers;

    public event Action DealtDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Триггер");
        bool isTarget = (targetLayers & (1 << collision.gameObject.layer)) != 0;
        if (!isTarget) return;
        Debug.Log("подходит");

        if (collision.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            Debug.Log("Взял дамагл");
            damagable.TakeDamage(damage);
            DealtDamage?.Invoke();
        }
    }
}
