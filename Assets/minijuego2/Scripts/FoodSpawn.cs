using UnityEngine;

public class FoodSpaw : MonoBehaviour
{
    [Header("Food Settings")]
    [SerializeField] private GameObject[] foodPrefabs; // Primeros goodFoodCount son buenos
    [SerializeField] private int goodFoodCount = 3;

    [Header("Spawn Settings")]
    [SerializeField] private float initialSpawnInterval = 1.5f;
    [SerializeField] private float midGameSpawnInterval = 1.0f;
    [SerializeField] private float lateGameSpawnInterval = 0.7f;
    [SerializeField] private float xRange = 5f;
    [SerializeField] private float offsetY = 3f;
    [SerializeField] private float foodLifetime = 4f;

    private float timer;
    private float gameTime;
    private int consecutiveTypeCount;
    private FoodType lastFoodType;

    private enum FoodType { Good, Bad }

    private void Update()
    {
        timer += Time.deltaTime;
        gameTime += Time.deltaTime;

        float currentSpawnInterval = GetCurrentSpawnInterval();

        if (timer >= currentSpawnInterval)
        {
            SpawnFood();
            timer = 0f;
        }
    }

    private float GetCurrentSpawnInterval()
    {
        if (gameTime >= 40f) return lateGameSpawnInterval;
        if (gameTime >= 20f) return midGameSpawnInterval;
        return initialSpawnInterval;
    }

    private void SpawnFood()
    {
        FoodType currentType = DetermineFoodType();
        GameObject foodPrefab = SelectFoodPrefab(currentType);

        SpawnFoodInstance(foodPrefab);
        UpdateTypeTracking(currentType);
    }

    private FoodType DetermineFoodType()
    {
        // Cambiar de tipo después de 2 repeticiones
        if (consecutiveTypeCount >= 2)
        {
            return lastFoodType == FoodType.Good ? FoodType.Bad : FoodType.Good;
        }

        return Random.Range(0, 2) == 0 ? FoodType.Good : FoodType.Bad;
    }

    private GameObject SelectFoodPrefab(FoodType type)
    {
        int index = type == FoodType.Good
            ? Random.Range(0, goodFoodCount)
            : Random.Range(goodFoodCount, foodPrefabs.Length);

        return foodPrefabs[index];
    }

    private void SpawnFoodInstance(GameObject prefab)
    {
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-xRange, xRange),
            transform.position.y + offsetY,
            transform.position.z
        );

        GameObject food = Instantiate(prefab, spawnPosition, Quaternion.identity);
        Destroy(food, foodLifetime);
    }

    private void UpdateTypeTracking(FoodType currentType)
    {
        if (currentType == lastFoodType)
        {
            consecutiveTypeCount++;
        }
        else
        {
            consecutiveTypeCount = 1;
            lastFoodType = currentType;
        }
    }
}