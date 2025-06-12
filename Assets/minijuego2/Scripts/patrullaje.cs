using UnityEngine;

public class Patrullaje : MonoBehaviour
{
    [Header("Puntos de patrullaje")]
    public Transform posicionA;
    public Transform posicionB;

    [Header("Tiempo que tarda en ir de A a B (en segundos)")]
    public float tiempoDeIda = 2f;

    private float distanciaTotal;
    private Vector3 direccion;

    void Start()
    {
        if (posicionA == null || posicionB == null)
        {
            Debug.LogError("Faltan asignar los puntos A y B en el inspector.");
            enabled = false;
            return;
        }

        // Calcula la distancia (L) y la dirección unitaria
        distanciaTotal = Vector3.Distance(posicionA.position, posicionB.position);
        direccion = (posicionB.position - posicionA.position).normalized;
    }

    void Update()
    {
        float t = Time.time; // tiempo desde que comenzó el juego

        // Normalización y aplicación de función triangular
        float ciclo = (t / tiempoDeIda) % 2f;
        float factor = Mathf.Abs(ciclo - 1f); // varía entre 0 y 1 y luego de 1 a 0

        // Posición final
        Vector3 nuevaPos = posicionA.position + direccion * distanciaTotal * factor;
        transform.position = nuevaPos;
    }

    void OnDrawGizmos()
    {
        if (posicionA != null && posicionB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(posicionA.position, posicionB.position);
        }
    }
}
