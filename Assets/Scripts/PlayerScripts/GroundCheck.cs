using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    public UnityEvent<bool> OnGroundChanged;

    public AudioClip groundSound;
    public float volumeScale = 0.3f;

    private Collider2D groundCheckCollider;
    private ContactFilter2D contactFilter;
    private Collider2D[] results = new Collider2D[5]; // Массив для результатов
    private bool isGrounded = false;

    private void Awake()
    {
        OnGroundChanged = new UnityEvent<bool>();
        groundCheckCollider = GetComponent<Collider2D>();

        if (groundCheckCollider == null)
        {
            Debug.LogError("GroundCheck требует Collider2D!");
            return;
        }

        // Настраиваем фильтр контактов
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Default")); // Или нужный слой
        contactFilter.useTriggers = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            CheckGroundStatus();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            CheckGroundStatus();
        }
    }

    private void CheckGroundStatus()
    {
        bool wasGrounded = isGrounded;

        // Используем OverlapCollider с существующим коллайдером
        int contactCount = groundCheckCollider.Overlap(contactFilter, results);

        // Проверяем, есть ли контакт с тегом "Ground"
        bool groundedNow = false;
        for (int i = 0; i < contactCount; i++)
        {
            if (results[i] != null && results[i].CompareTag("Ground"))
            {
                groundedNow = true;
                break;
            }
        }

        // Если статус изменился
        if (wasGrounded != groundedNow)
        {
            isGrounded = groundedNow;
            OnGroundChanged?.Invoke(groundedNow);

            // Играем звук только при касании земли
            if (groundedNow && groundSound != null)
            {
                SoundManager.PlaySfx(groundSound, volumeScale);
            }
        }
    }
}