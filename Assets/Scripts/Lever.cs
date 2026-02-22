using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public UnityEvent OnSwitch = new();
    public UnityEvent OnSwitchToLeft = new();
    public UnityEvent OnSwitchToRight = new();

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private LayerMask interactorLayers;

    private bool checking = false;
    private Vector2 lastLocalPosition;

    private bool right = true;
    private bool left = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!checking) return;
        if (!MatchesLayerMask(collision.gameObject)) return;

        Vector2 newPosition = transform.InverseTransformPoint(collision.transform.position);

        if (left && lastLocalPosition.x < 0 && newPosition.x > 0)
        {
            SwitchToRight();
        }
        else if (right && lastLocalPosition.x > 0 && newPosition.x < 0)
        {
            SwitchToLeft();
        }

        lastLocalPosition = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (MatchesLayerMask(collision.gameObject))
        {
            lastLocalPosition = transform.InverseTransformPoint(collision.transform.position);
            checking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (MatchesLayerMask(collision.gameObject))
        {
            checking = false;
        }
    }
    private bool MatchesLayerMask(GameObject gameObject) => ((1 << gameObject.layer) & interactorLayers) != 0;

    private void SwitchToLeft()
    {
        left = true;
        right = false;
        spriteRenderer.flipX = true;
        PlaySound();
        OnSwitch?.Invoke();
        OnSwitchToLeft?.Invoke();
    }
    private void SwitchToRight()
    {
        left = false;
        right = true;
        spriteRenderer.flipX = false;
        PlaySound();
        OnSwitch?.Invoke();
        OnSwitchToRight?.Invoke();
    }

    void PlaySound()
    {
        if(audioSource == null)
        {
            Debug.Log("Не установлен AudioSource", gameObject);
            return;
        }

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }
}