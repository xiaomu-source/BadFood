using UnityEngine;

public class Food : MonoBehaviour
{
    // 该美食提供的卡路里值
    public float caloriesProvided = 10f;

    // 食物移动的速度
    public float moveSpeed = 2f;

    private Transform targetCustomer;

    private Customer customer;

    public void SetTarget(Transform customer)
    {
        targetCustomer = customer;
    }

    private void Start()
    {
        // 订阅顾客销毁事件
        if (targetCustomer != null)
        {
            customer = targetCustomer.GetComponent<Customer>();
            if (customer != null)
            {
                customer.onCustomerDestroyed.AddListener(DestroyFood);
            }
        }
    }

    private void Update()
    {
        if (targetCustomer != null)
        {
            // 移动食物向顾客
            transform.position =
                Vector3.MoveTowards(transform.position, targetCustomer.position, moveSpeed * Time.deltaTime);

            // 检查是否到达顾客位置
            if (Vector3.Distance(transform.position, targetCustomer.position) < 0.1f)
            {
                // 获取顾客的 Customer 组件
                Customer customer = targetCustomer.GetComponent<Customer>();
                if (customer != null)
                {
                    // 调用 AddCalories 方法增加卡路里值
                    customer.AddCalories(caloriesProvided);
                }

                // 销毁美食
                Destroy(gameObject);
            }
        }
    }

    private void DestroyFood()
    {
        // 销毁食物
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞的对象是否是顾客
        if (other.CompareTag("Customer"))
        {
            targetCustomer = other.transform;
            customer = targetCustomer.GetComponent<Customer>();
            if (customer != null)
            {
                customer.onCustomerDestroyed.AddListener(DestroyFood);
            }
        }
    }
}