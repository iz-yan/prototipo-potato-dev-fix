using UnityEngine;

public class AutoBehaviour : MonoBehaviour
{
    public float vida = 200f;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidadBase = 5f;
    [SerializeField] private float velocidadMaxima = 15f;
    [SerializeField] private float aceleracion = 2f;
    [SerializeField] private float velocidadVertical = 3f;

    [Header("Configuración de Daño")]
    [SerializeField] private float danioPorFueraDeCarretera = 15f; // Daño por segundo fuera de carretera
    [SerializeField] private float intervaloDanio = 1f; // Cada cuánto tiempo se aplica el daño

    private float velocidadHorizontal;
    private float inputVertical;
    private bool estaAcelerando = false;
    private bool estaEnCarretera = true;
    private float tiempoFueraDeCarretera = 0f;
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
        VerificarDanioPorCarretera();
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

    private void VerificarDanioPorCarretera()
    {
        if (!estaEnCarretera)
        {
            tiempoFueraDeCarretera += Time.deltaTime;

            if (tiempoFueraDeCarretera >= intervaloDanio)
            {
                vida -= danioPorFueraDeCarretera;
                tiempoFueraDeCarretera = 0f;
                Debug.Log("Fuera de carretera! Vida: " + vida);
                vida = Mathf.Clamp(vida, 0, 200);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("road"))
        {
            estaEnCarretera = true;
            tiempoFueraDeCarretera = 0f; // Resetear el contador cuando vuelve a la carretera
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("road"))
        {
            estaEnCarretera = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstaculo"))
        {
            vida -= 30f;
            Debug.Log("Colisionó con obstáculo. Vida: " + vida);
            vida = Mathf.Clamp(vida, 0, 200);
        }
    }

    public float ObtenerVelocidadActual()
    {
        return velocidadHorizontal;
    }
}