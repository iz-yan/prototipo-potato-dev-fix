using UnityEngine;

public class FoodCannon : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    public float minForce = 2f;
    public float maxForce = 6f;
    public bool shootToRight = true; // Control de dirección

    void Start()
    {
        InvokeRepeating("ShootFood", 10f, 6f);
    }

    void ShootFood()
    {
        GameObject food = Instantiate(foodPrefabs[Random.Range(0, foodPrefabs.Length)],
                                transform.position,
                                Quaternion.identity);

        Rigidbody2D rb = food.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float randomForce = Random.Range(minForce, maxForce);
            // Dirección según el booleano
            float direction = shootToRight ? 1 : -1;
            Vector2 force = new Vector2(randomForce * direction, randomForce);
            rb.AddForce(force, ForceMode2D.Impulse);
        }

        Destroy(food, 4f);
    }
}