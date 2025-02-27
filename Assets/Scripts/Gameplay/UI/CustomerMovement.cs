using UnityEngine;
using System.Collections.Generic;

public class CustomerMovement : MonoBehaviour
{
    [Header("移动设置")] public float speed = 2f;
    public float rotationSpeed = 5f;
    public float reachThreshold = 0.1f;

    private List<Vector3> pathPoints;
    private int currentIndex = 0;

    public void SetPath(List<Vector3> points)
    {
        pathPoints = new List<Vector3>(points);
        currentIndex = 0;
    }

    void Update()
    {
        if (pathPoints == null || currentIndex >= pathPoints.Count)
        {
            Debug.Log("路径未设置或已到达终点");
            return;
        }

        Vector3 target = pathPoints[currentIndex];
        // Debug.Log($"当前位置：{transform.position}，目标位置：{target}");
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        UpdateRotation(target);

        if (Vector3.Distance(transform.position, target) < reachThreshold)
        {
            // Debug.Log($"到达路径点 {currentIndex}");
            currentIndex++;
            if (currentIndex >= pathPoints.Count)
            {
                Destroy(gameObject); // 到达终点后销毁
            }
        }
    }

    void UpdateRotation(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}