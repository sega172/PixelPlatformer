using System;
using UnityEngine;

public class StatedTrigger : MonoBehaviour
{
    public event Action<bool> StateChanged;

    private void OnTriggerEnter2D(Collider2D collision) => StateChanged?.Invoke(true);
    private void OnTriggerExit2D(Collider2D collision) => StateChanged?.Invoke(false);
}
