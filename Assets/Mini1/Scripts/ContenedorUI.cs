using UnityEngine;
using UnityEngine.UI;

public class ContenedorUI : MonoBehaviour
{
    public GameObject mensajeDepositar;

    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.F))
        {
            OcultarMensaje();
        }
    }

    private void OcultarMensaje()
    {
            mensajeDepositar.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            mensajeDepositar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
            mensajeDepositar.SetActive(false);
        }
    }
}
