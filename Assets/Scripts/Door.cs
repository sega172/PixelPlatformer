using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Events;
using System;

public class Door : MonoBehaviour
{
    
    [SerializeField] private Transform doorObject;
    [SerializeField] private Transform openedPoint;
    [SerializeField] private Transform closedPoint;

    [SerializeField] private float speed = 2f;          
    [SerializeField] private bool startOpened = false;

    [SerializeField] private AudioSource audioSource;

    public UnityEvent onOpened;
    public UnityEvent onClosed;

    private bool opened;
    private CancellationTokenSource cts;

    private void Awake()
    {
        opened = startOpened;
        doorObject.position = startOpened ? openedPoint.position : closedPoint.position;
    }

    private void OnDestroy()
    {
        cts?.Cancel();
        cts?.Dispose();
    }

    public void Open()
    {
        if (opened) return;
        StartMove(true);
    }

    public void Close()
    {
        if (!opened) return;
        StartMove(false);
    }

    public void Switch()
    {
        StartMove(!opened);
    }


    private void StartMove(bool targetOpened)
    {
        cts?.Cancel();
        cts?.Dispose();

        cts = new CancellationTokenSource();
        var token = cts.Token;

        opened = targetOpened;
        Vector3 target = targetOpened ? openedPoint.position : closedPoint.position;

        MoveToTarget(target, token).Forget();
    }

    private async UniTask MoveToTarget(Vector3 target, CancellationToken token)
    {
        audioSource?.Stop();
        audioSource.Play();
        try
        {
            while (Vector3.Distance(doorObject.position, target) > 0.01f)
            {
                token.ThrowIfCancellationRequested();

                doorObject.position = Vector3.MoveTowards(
                    doorObject.position,
                    target,
                    speed * Time.deltaTime
                );

                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
            doorObject.position = target;

            if (opened)
                onOpened?.Invoke();
            else
                onClosed?.Invoke();
        }
        catch (OperationCanceledException)
        {

        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при движении двери: {ex}", this);
        }
    }
}