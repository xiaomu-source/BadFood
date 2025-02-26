using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodTower : MonoBehaviour
{
    public GameObject foodPrefab;
    public float generationInterval = 3f;

    void Start()
    {
        StartCoroutine(GenerateFoodRoutine());
    }

    IEnumerator GenerateFoodRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(generationInterval);
            GenerateFood();
        }
    }

    void GenerateFood()
    {
        if (foodPrefab == null) return;

        // 找到最近的顾客
        Customer nearestCustomer = FindNearestCustomer();
        if (nearestCustomer == null) return;

        // 生成食物对象
        GameObject foodObject = Instantiate(foodPrefab, transform.position, Quaternion.identity, transform);
        Food food = foodObject.GetComponent<Food>();

        // 设置食物的目标顾客
        food.SetTarget(nearestCustomer.transform);
    }

    private Customer FindNearestCustomer()
    {
        Customer[] allCustomers = FindObjectsOfType<Customer>();
        if (allCustomers.Length == 0) return null;

        Customer nearestCustomer = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Customer customer in allCustomers)
        {
            float distance = Vector3.Distance(transform.position, customer.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestCustomer = customer;
            }
        }

        return nearestCustomer;
    }
}