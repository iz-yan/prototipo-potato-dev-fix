using UnityEngine;
using UnityEngine.SceneManagement;

public class PuntoVictoria : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string tagObjetivo = "auto"; // Tag del objeto que activa la victoria

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            CargarEscenaVictoria();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagObjetivo))
        {
            CargarEscenaVictoria();
        }
    }

    private void CargarEscenaVictoria()
    {
        Debug.Log("¡Victoria alcanzada! Cargando escena de victoria...");
        SceneManager.LoadScene("Victoria");
    }

    // Opcional: Dibujar un gizmo para visualizar el punto en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawIcon(transform.position, "flag.png", true);
    }
}