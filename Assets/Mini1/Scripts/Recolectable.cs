using UnityEngine;

public class Recolectable : MonoBehaviour
{
    public string id;
    public GameObject filtroRecolectable;

    private bool jugadorCerca = false;
    private PlayerInventario playerInventario;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            RecogerRecolectable();
        }
    }

    void RecogerRecolectable()
    {
        if (!playerInventario.TieneRecolectable())
        {
            playerInventario.TomarRecolectable(this);
            playerInventario.audioSource.PlayOneShot(playerInventario.sonidoRecolectar);

            gameObject.SetActive(false);
            Debug.Log("Recogido: " + id);
        }
        else
        {
            Debug.Log("Ya tienes un recolectable en tus manos.");
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
