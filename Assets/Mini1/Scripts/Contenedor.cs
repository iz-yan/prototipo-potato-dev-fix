using UnityEngine;

public class Contenedor : MonoBehaviour
{
    public string tipoContenedor;
    public int cantidadTotal = 3;
    private int cantidadActual = 0;

    private bool jugadorCerca = false;
    private PlayerInventario playerInventario;

    public AudioSource audioSource;
    public AudioClip sonidoDepositoCorrecto;
    public AudioClip sonidoDepositoIncorrecto;
    public AudioClip sonidoContenedorCompleto;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.F))
        {
            IntentarDepositar();
        }
    }

    void IntentarDepositar()
    {
        if (playerInventario != null && playerInventario.TieneRecolectable())
        {
            Recolectable r = playerInventario.recolectableActual;
            if (r.id == tipoContenedor)
            {
                Debug.Log(r.id + " depositado correctamente.");

                r.filtroRecolectable.SetActive(false);

                Destroy(r.gameObject);
                playerInventario.SoltarRecolectable();

                cantidadActual++;

                audioSource.PlayOneShot(sonidoDepositoCorrecto);

                if (cantidadActual >= cantidadTotal)
                {
                    Debug.Log("¡Contenedor de " + tipoContenedor + " completado!");
                    audioSource.PlayOneShot(sonidoContenedorCompleto);

                    AdminMinijuego admin = FindFirstObjectByType<AdminMinijuego>();
                    admin.ContenedorCompletado();
                }
            }
            else
            {
                Debug.Log("Tipo de recolectable incorrecto: " + r.id);

                audioSource.PlayOneShot(sonidoDepositoIncorrecto);
            }
        }
        else
        {
            Debug.Log("No tienes ningún recolectable en tus manos.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            playerInventario = collision.GetComponent<PlayerInventario>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
            playerInventario = null;
        }
    }
}
