using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string targetTag = "auto"; // Tag del objeto a seguir
    [SerializeField] private float smoothSpeed = 5f; // Suavizado del movimiento
    [SerializeField] private Vector2 minBounds; // Límite mínimo en X/Y
    [SerializeField] private Vector2 maxBounds; // Límite máximo en X/Y

    private Transform target; // Referencia al Transform del auto
    private Vector3 offset; // Distancia inicial entre cámara y auto
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        // Buscar el objeto con el tag "auto" al inicio
        GameObject auto = GameObject.FindGameObjectWithTag(targetTag);

        if (auto != null)
        {
            target = auto.transform;
            offset = transform.position - target.position; // Calcular offset inicial

            // Calcular límites automáticos si no están asignados
            if (minBounds == Vector2.zero && maxBounds == Vector2.zero)
            {
                CalculateCameraBounds();
            }
        }
        else
        {
            Debug.LogWarning($"No se encontró ningún objeto con el tag {targetTag}");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Posición deseada (siguiendo al auto en X e Y, manteniendo Z original)
            Vector3 desiredPosition = new Vector3(
                target.position.x + offset.x,
                target.position.y + offset.y,
                transform.position.z
            );

            // Aplicar límites a la posición
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

            // Movimiento suavizado
            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,
                desiredPosition,
                smoothSpeed * Time.deltaTime
            );

            transform.position = smoothedPosition;
        }
    }

    void CalculateCameraBounds()
    {
        // Este método debería implementarse según tu escena
        // Puedes asignar valores manuales en el Inspector o calcularlos automáticamente

        // Ejemplo básico (ajusta estos valores según tu escena)
        minBounds = new Vector2(-10, -5);
        maxBounds = new Vector2(10, 5);
    }

    // Dibuja los límites en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = new Vector3(
            (minBounds.x + maxBounds.x) * 0.5f,
            (minBounds.y + maxBounds.y) * 0.5f,
            transform.position.z
        );

        Vector3 size = new Vector3(
            maxBounds.x - minBounds.x,
            maxBounds.y - minBounds.y,
            0.1f
        );

        Gizmos.DrawWireCube(center, size);
    }
}