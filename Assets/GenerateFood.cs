using UnityEngine;

public class GenerateFood : MonoBehaviour
{
    public GameObject foodPrefab;
    public float xBounds = 25f;
    public float zBounds = 25f;

    void Start()
    {
        InvokeRepeating("SpawnFood", 0, 4);
    }

    void SpawnFood()
    {
        Vector3 position = new Vector3(Random.Range(-xBounds, xBounds), 0.5f, Random.Range(-zBounds, zBounds));
        Instantiate(foodPrefab, position, Quaternion.identity);
    }
}
