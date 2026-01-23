using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointMover : MonoBehaviour
{
    public Transform moveableObject;
    public int currentPointIndex = 0;
    public Transform pointsParent;
    public List<Transform> points;

    public float standTime = 1;
    public float speed = 2;

    Vector3 targetPoint;


    private void Awake()
    {
        points = new List<Transform>();
        for (int i = 0; i < pointsParent.childCount; i++)
        {
            points.Add(pointsParent.GetChild(i));
        }
        currentPointIndex = 0;

        targetPoint = points[currentPointIndex].position;
        moveableObject.position = targetPoint;
    }

    bool moving = false;
    float elapsedTime = 0;
    void Update()
    {

        if (moving)
        {
            moveableObject.position = Vector2.MoveTowards(moveableObject.position, targetPoint, speed * Time.deltaTime);

            if (moveableObject.position == targetPoint)
            {
                moving = false;
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= standTime)
            {
                currentPointIndex = (currentPointIndex + 1) % points.Count;
                elapsedTime = 0;
                moving = true;
                targetPoint = points[currentPointIndex].position;

            }
        }
    }
    void OnDrawGizmos()
    {
        if (pointsParent.childCount < 2) return;

        // Set gizmo color
        Gizmos.color = Color.red;
        // Draw lines connecting the points in chain order
        if (true)
        {
            for (int i = 0; i < pointsParent.childCount; i++)
            {
                if (pointsParent.GetChild(i) != null && pointsParent.GetChild((i + 1) % pointsParent.childCount) != null)
                {
                    Gizmos.DrawLine(pointsParent.GetChild(i).position, pointsParent.GetChild((i + 1) % pointsParent.childCount).position);
                    Gizmos.DrawSphere(pointsParent.GetChild(i).position, 0.15f);
                }
            }
        }
    }
}
