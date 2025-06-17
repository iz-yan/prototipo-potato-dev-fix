using UnityEngine;

public class GatoController : MonoBehaviour
{
    public float velocidad = 5f;
    public bool IsWalking = false;

    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float movimientoX = Input.GetAxisRaw("Horizontal"); // -1 (izquierda), 1 (derecha), 0 (quieto)

        // Mover al gato
        rb.linearVelocity = new Vector2(movimientoX * velocidad, rb.linearVelocity.y);

        // Determinar si est� caminando
        IsWalking = movimientoX != 0;

        // Actualizar animaci�n
        animator.SetBool("IsWalking", IsWalking);

        // Flip horizontalmente si cambia de direcci�n
        if (movimientoX != 0)
        {
            Vector3 escala = transform.localScale;
            escala.x = Mathf.Sign(movimientoX) * Mathf.Abs(escala.x);
            transform.localScale = escala;
        }
    }
}
