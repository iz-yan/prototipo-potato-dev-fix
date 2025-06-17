using UnityEngine;
using System.Collections;

public class SpawnCarEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Transform initialDestination;
    [SerializeField] private Transform finalDestination;
    [SerializeField] private float speed = 1f; // Nueva variable de velocidad

    private void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned in the inspector!");
            return;
        }

        if (initialDestination == null || finalDestination == null)
        {
            Debug.LogError("Destinations are not assigned in the inspector!");
            return;
        }

        StartCoroutine(SpawnEnemiesRoutine());
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        StartCoroutine(MoveEnemy(enemy, initialDestination.position, finalDestination.position));
    }

    private IEnumerator MoveEnemy(GameObject enemy, Vector3 startPos, Vector3 endPos)
    {
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        while (enemy != null && Vector3.Distance(enemy.transform.position, endPos) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * speed; // Ahora usa la variable speed
            float fractionOfJourney = distanceCovered / journeyLength;

            enemy.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }

        if (enemy != null)
        {
            Destroy(enemy);
        }
    }

    // Métodos públicos para cambiar parámetros durante el juego si es necesario
    public void SetSpawnInterval(float interval)
    {
        spawnInterval = interval;
    }

    public void SetDestinations(Transform initial, Transform final)
    {
        initialDestination = initial;
        finalDestination = final;
    }

    public void SetEnemyPrefab(GameObject prefab)
    {
        enemyPrefab = prefab;
    }

    // Nuevo método para ajustar la velocidad
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}