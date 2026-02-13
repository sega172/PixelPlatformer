using UnityEngine;

public class Projectile : MonoBehaviour
{

    bool initialized = false;
    Vector2 direction;
    float speed;
    [SerializeField] private AudioClip explosionClip;

    public void Initialize(float damage, Vector2 direction, float speed, float lifetime)
    {
        this.direction = direction;
        this.speed = speed;

        Destroy(gameObject, lifetime);

        initialized = true;
    }

    private void Update()
    {
        if (!initialized) return;

        transform.Translate(direction * Time.deltaTime * speed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!initialized) return;

        if (explosionClip != null)
            SoundManager.PlaySfx(explosionClip);
        else
            Debug.Log($"У {name} не установлен звук");
            Destroy(gameObject);
    }


}
