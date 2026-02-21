using UnityEngine;

public class Lever : MonoBehaviour
{
    public StatedTrigger left, right;
    public SpriteRenderer spriteRenderer;

    private bool leftState = false;
    private bool rightState = false;

    public AudioClip clip;
    private void Start()
    {
        // Подписываемся на события триггеров
        left.StateChanged += OnLeftTriggerStateChanged;
        right.StateChanged += OnRightTriggerStateChanged;
    }

    private void OnDestroy()
    {
        // Отписываемся от событий при уничтожении объекта
        left.StateChanged -= OnLeftTriggerStateChanged;
        right.StateChanged -= OnRightTriggerStateChanged;
    }

    private void OnLeftTriggerStateChanged(bool state)
    {
        leftState = state;
        UpdateLeverDirection();
    }

    private void OnRightTriggerStateChanged(bool state)
    {
        rightState = state;
        UpdateLeverDirection();
    }

    private void UpdateLeverDirection()
    {
        // Если игрок внутри левого триггера и вышел из правого - движение влево
        if (leftState && !rightState)
        {
            SetLeverDirection("Лево");
        }
        // Если игрок внутри правого триггера и вышел из левого - движение вправо
        else if (rightState && !leftState)
        {
            SetLeverDirection("Право");
        }
    }

    private void SetLeverDirection(string direction)
    {
        switch (direction)
        {
            case "Лево":
                spriteRenderer.flipX = true; // Поворачиваем спрайт влево
                Debug.Log("Рычаг переключен в состояние: Лево");
                SoundManager.PlaySfx(clip);
                break;

            case "Право":
                spriteRenderer.flipX = false; // Возвращаем спрайт в исходное положение (вправо)
                Debug.Log("Рычаг переключен в состояние: Право");
                SoundManager.PlaySfx(clip);
                break;
        }
    }
}