using UnityEngine;

public class AutoBehaviour : MonoBehaviour
{
    public float vida = 200f;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidadBase = 5f;
    [SerializeField] private float velocidadMaxima = 15f;
    [SerializeField] private float aceleracion = 2f;
    [SerializeField] private float velocidadVertical = 3f;

    private float velocidadHorizontal;
    private float inputVertical;
    private bool estaAcelerando = false;

    private Rigidbody2D rb;

    void Start()
    {
        velocidadHorizontal = velocidadBase;
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
            Debug.LogError("Falta Rigidbody2D en el GameObject.");
    }

    void Update()
    {
        ProcesarEntrada();
        CalcularVelocidad();
    }

    void FixedUpdate()
    {
        AplicarMovimiento();
    }

    private void ProcesarEntrada()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        estaAcelerando = Input.GetKey(KeyCode.Space) && inputHorizontal > 0;
        inputVertical = Input.GetAxis("Vertical");
    }

    private void CalcularVelocidad()
    {
        if (estaAcelerando)
        {
            velocidadHorizontal = Mathf.Min(velocidadHorizontal + aceleracion * Time.deltaTime, velocidadMaxima);
        }
        else
        {
            velocidadHorizontal = velocidadBase;
        }
    }

    private void AplicarMovimiento()
    {
        float movimientoX = Input.GetAxis("Horizontal") * velocidadHorizontal * Time.fixedDeltaTime;
        float movimientoY = inputVertical * velocidadVertical * Time.fixedDeltaTime;

        Vector2 nuevaPosicion = rb.position + new Vector2(movimientoX, movimientoY);
        rb.MovePosition(nuevaPosicion);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstaculo"))
        {
            vida -= 20f;
            Debug.Log("Colisionó con obstáculo. Vida: " + vida);
            vida = Mathf.Clamp(vida, 0, 200);
        }
    }

    public float ObtenerVelocidadActual()
    {
        return velocidadHorizontal;
    }
}
