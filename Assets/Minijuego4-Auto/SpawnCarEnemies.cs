using UnityEngine;
using System.Collections;

public class SpawnCarEnemies : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Transform initialDestination;
    [SerializeField] private Transform finalDestination;
    [SerializeField] private float speed = 1f;

    [Header("Rotation Settings")]
    [SerializeField] private float defaultRotationY = 0f; // Valor por defecto editable en inspector
    [SerializeField] private bool useCustomRotation = false; // Toggle para usar rotación personalizada

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
        // Determinar la rotación a usar
        float rotationY = useCustomRotation ? defaultRotationY : transform.rotation.eulerAngles.y;
        Quaternion spawnRotation = Quaternion.Euler(0, rotationY, 0);

        GameObject enemy = Instantiate(enemyPrefab, transform.position, spawnRotation);
        StartCoroutine(MoveEnemy(enemy, initialDestination.position, finalDestination.position));
    }

    private IEnumerator MoveEnemy(GameObject enemy, Vector3 startPos, Vector3 endPos)
    {
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        while (enemy != null && Vector3.Distance(enemy.transform.position, endPos) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;

            enemy.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }

        if (enemy != null)
        {
            Destroy(enemy);
        }
    }

    // Métodos públicos para cambiar parámetros
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

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Nuevos métodos para controlar la rotación
    public void SetDefaultRotationY(float rotationY)
    {
        defaultRotationY = rotationY;
    }

    public void ToggleCustomRotation(bool useCustom)
    {
        useCustomRotation = useCustom;
    }
}