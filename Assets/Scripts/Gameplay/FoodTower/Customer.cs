using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public int id;
    public string customerName;
    public string coins;

    public string image;

    // 顾客的当前卡路里值
    public float calCur;
    public float calReq;

    // 用于显示卡路里值的TextMeshPro组件
    private TextMeshProUGUI caloriesText;

    // 用于显示顾客图像的Image组件
    private Image customerImage;

    // 顾客销毁事件
    public UnityEvent onCustomerDestroyed = new UnityEvent();

    [Header("移动设置")] public float speed = 200f;
    private float rotationSpeed = 5f;
    private float reachThreshold = 0.1f;

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

    private void Start()
    {
        // 在顾客游戏对象下查找TextMeshProUGUI组件
        caloriesText = GetComponentInChildren<TextMeshProUGUI>();
        if (caloriesText == null)
        {
            Debug.LogError("未找到用于显示卡路里的TextMeshProUGUI组件，请确保顾客游戏对象下有TextMeshProUGUI子对象。");
        }
        else
        {
            // 初始化时更新UI显示
            UpdateCaloriesText();
        }

        // 在顾客游戏对象下查找Image组件
        customerImage = GetComponentInChildren<Image>();
        if (customerImage == null)
        {
            Debug.LogError("未找到用于显示顾客图像的Image组件，请确保顾客游戏对象下有Image子对象。");
        }
        else
        {
            // 从指定路径加载顾客图像
            LoadCustomerImage();
        }
    }

    // 加载顾客图像的方法
    private void LoadCustomerImage()
    {
        Sprite customerSprite = Resources.Load<Sprite>(image);
        if (customerSprite != null)
        {
            customerImage.sprite = customerSprite;
        }
        else
        {
            Debug.LogError($"未能加载顾客图像，路径：{image}");
        }
    }

    // 增加卡路里的方法
    public void AddCalories(float amount)
    {
        calCur += amount;
        // 更新UI显示
        UpdateCaloriesText();
    }

    // 更新卡路里值显示的方法
    private void UpdateCaloriesText()
    {
        if (caloriesText != null)
        {
            caloriesText.text = $"{calCur}";
        }
    }

    private void OnDestroy()
    {
        // 触发顾客销毁事件
        onCustomerDestroyed.Invoke();
    }
}