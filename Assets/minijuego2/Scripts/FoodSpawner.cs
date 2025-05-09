
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    public float spawnInterval = 1.5f;
    public float xRange = 5f;
    public float offsetY = 3f;

    private float timer;
    private float gameTime;

    void Update()
    {
        timer += Time.deltaTime;
        gameTime += Time.deltaTime;

        if (gameTime >= 20f && gameTime < 40f)
        {
            spawnInterval = 1.0f;
        }
        else if (gameTime >= 40f)
        {
            spawnInterval = 0.7f;
        }

        if (timer >= spawnInterval)
        {
            SpawnFood();
            timer = 0f;
        }
    }

    void SpawnFood()
    {
        int index = Random.Range(0, foodPrefabs.Length);
        GameObject food = Instantiate(foodPrefabs[index]);

        float randomX = Random.Range(-xRange, xRange);

        Vector3 spawnPosition = new Vector3(transform.position.x + randomX, transform.position.y + offsetY, transform.position.z);

        food.transform.position = spawnPosition;
    }
}