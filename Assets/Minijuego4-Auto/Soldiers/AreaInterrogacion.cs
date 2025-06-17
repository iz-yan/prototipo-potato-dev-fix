using UnityEngine;

public class AreaInterrogacion : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string targetTag = "auto"; // Tag del objeto a detectar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Se encuentra en el area de interrogacion");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Ha salido del area de interrogacion");
        }
    }

    // Opcional: Dibujar el área en el editor
    void OnDrawGizmos()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Gizmos.color = new Color(1, 0.5f, 0, 0.3f); // Color naranja transparente
            if (collider is BoxCollider2D)
            {
                Gizmos.DrawCube(transform.position + (Vector3)((BoxCollider2D)collider).offset,
                               ((BoxCollider2D)collider).size);
            }
            else if (collider is CircleCollider2D)
            {
                Gizmos.DrawSphere(transform.position + (Vector3)((CircleCollider2D)collider).offset,
                                 ((CircleCollider2D)collider).radius);
            }
        }
    }
}