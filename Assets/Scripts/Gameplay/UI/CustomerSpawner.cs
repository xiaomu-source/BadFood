using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public float spawnInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnCustomer();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCustomer()
    {
        if (MapGenerator.GlobalPathPoints.Count < 2) return;

        GameObject customer = Instantiate(
            customerPrefab,
            MapGenerator.GlobalPathPoints[0],
            Quaternion.identity,
            transform
        );

        CustomerMovement movement = customer.GetComponent<CustomerMovement>();
        movement.SetPath(MapGenerator.GlobalPathPoints);
    }
}