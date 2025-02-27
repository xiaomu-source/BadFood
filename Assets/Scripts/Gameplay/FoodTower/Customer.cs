using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Customer : MonoBehaviour
{
    // 顾客的初始卡路里值
    public float calories = 0f;

    // 用于显示卡路里值的TextMeshPro组件
    private TextMeshProUGUI caloriesText;
    
    // 顾客销毁事件
    public UnityEvent onCustomerDestroyed = new UnityEvent();

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
    }

    // 增加卡路里的方法
    public void AddCalories(float amount)
    {
        calories += amount;
        // 更新UI显示
        UpdateCaloriesText();
        // Debug.Log($"顾客的卡路里值增加了 {amount}，当前卡路里值为 {calories}");
    }

    // 更新卡路里值显示的方法
    private void UpdateCaloriesText()
    {
        if (caloriesText != null)
        {
            caloriesText.text = $"{calories}";
        }
    }

    private void OnDestroy()
    {
        // 触发顾客销毁事件
        onCustomerDestroyed.Invoke();
    }
}