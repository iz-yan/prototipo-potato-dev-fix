using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    [Header("Configuración de Puntos")]
    [SerializeField] private Transform startPoint;  // Punto inicial
    [SerializeField] private Transform endPoint;    // Punto final

    [Header("Configuración de Movimiento")]
    [SerializeField] private float speed = 2f;      // Velocidad de movimiento
    [SerializeField] private bool loopMovement = false; // Si debe volver al inicio

    private bool movingToEnd = true;
    private bool shouldMove = true;
    private float journeyLength;
    private float startTime;

    void Start()
    {
        // Verificar que los puntos estén asignados
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("Los puntos de inicio/fin no están asignados!");
            shouldMove = false;
            return;
        }

        // Calcular distancia total
        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        startTime = Time.time;
    }

    void Update()
    {
        if (!shouldMove) return;

        // Calcular progreso del movimiento (0 a 1)
        float distanceCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distanceCovered / journeyLength;

        if (movingToEnd)
        {
            // Mover hacia el punto final
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionOfJourney);

            // Verificar si llegó al final
            if (fractionOfJourney >= 1f)
            {
                if (loopMovement)
                {
                    // Preparar movimiento de regreso
                    movingToEnd = false;
                    startTime = Time.time;
                }
                else
                {
                    // Detener completamente
                    shouldMove = false;
                    Debug.Log("Objeto llegó al destino final y se detuvo");
                }
            }
        }
        else
        {
            // Mover hacia el punto inicial
            transform.position = Vector3.Lerp(endPoint.position, startPoint.position, fractionOfJourney);

            // Verificar si llegó al inicio
            if (fractionOfJourney >= 1f)
            {
                movingToEnd = true;
                startTime = Time.time;
            }
        }
    }

    // Método para reiniciar el movimiento
    public void ResetMovement()
    {
        movingToEnd = true;
        shouldMove = true;
        startTime = Time.time;
        transform.position = startPoint.position;
    }

    // Método para detener manualmente
    public void StopMovement()
    {
        shouldMove = false;
    }

    // Método para iniciar manualmente
    public void StartMovement()
    {
        shouldMove = true;
        startTime = Time.time;
    }
}