using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private Transform pointA;  // Primer punto de referencia
    [SerializeField] private Transform pointB;  // Segundo punto de referencia
    [SerializeField] private float moveSpeed = 3f;  // Velocidad de movimiento
    [SerializeField] private float waitTime = 2f;  // Tiempo de espera en cada punto

    private Transform currentTarget;
    private bool isWaiting = false;
    private float waitTimer = 0f;
    private Vector3 originalRotation;  // Para mantener la rotación inicial

    void Start()
    {
        // Verificar que los puntos estén asignados
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Los puntos de referencia no están asignados en el Inspector!");
            enabled = false;
            return;
        }

        // Guardar la rotación original
        originalRotation = transform.eulerAngles;

        // Comenzar moviéndose hacia el punto A
        currentTarget = pointA;
    }

    void Update()
    {
        if (isWaiting)
        {
            // Contar el tiempo de espera
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
            }
            return;
        }

        // Mover hacia el objetivo actual
        MoveToTarget();
    }

    void MoveToTarget()
    {
        // Calcular dirección y distancia
        Vector3 direction = currentTarget.position - transform.position;
        float distanceThisFrame = moveSpeed * Time.deltaTime;

        // Si estamos lo suficientemente cerca
        if (direction.magnitude <= distanceThisFrame)
        {
            // Llegamos al destino
            transform.position = currentTarget.position;

            // Cambiar de objetivo
            currentTarget = (currentTarget == pointA) ? pointB : pointA;

            // Comenzar tiempo de espera
            isWaiting = true;
        }
        else
        {
            // Mover hacia el objetivo (sin cambiar la rotación)
            transform.position += direction.normalized * distanceThisFrame;

            // Mantener la rotación original
            transform.eulerAngles = originalRotation;
        }
    }

    // Dibujar gizmos en el editor para visualizar los puntos
    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pointA.position, 0.2f);
            Gizmos.DrawSphere(pointB.position, 0.2f);
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}