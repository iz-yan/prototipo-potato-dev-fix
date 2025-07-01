using UnityEngine;
using UnityEngine.UI;

public class RecolectableUI : MonoBehaviour
{
    public GameObject mensajeRecolectar;

    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            OcultarMensaje();
        }
    }

    private void OcultarMensaje()
    {
        mensajeRecolectar.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            mensajeRecolectar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
            mensajeRecolectar.SetActive(false);
        }
    }
}
