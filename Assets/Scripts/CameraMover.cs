using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    Vector3 baseOffsef = Vector3.back;

    Vector3 velocity;
    public float smoothTime = 0.5f;

    private void LateUpdate()
    {
        Vector3 targetCamPosition = target.position + offset + baseOffsef;
        transform.position = Vector3.SmoothDamp(transform.position, targetCamPosition, ref velocity, smoothTime);
        //transform.position = targetCamPosition;
    }
}
