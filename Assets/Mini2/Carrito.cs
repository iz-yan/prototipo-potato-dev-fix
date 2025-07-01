using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Carrito : MonoBehaviour
{
    public int cantidadPorTipo = 3;
    public List<string> tiposPermitidos = new List<string> { "leche", "pan", "fruta", "jugo" };
    public string tiposProhibidos = "dulce";
    public int intentosMaximos = 3;

    public AudioSource audioSource;
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoTipoCompleto;
    public AudioClip sonidoCarritoLleno;
    public AudioClip sonidoError;

    public Cartelito cartelito;

    private Dictionary<string, int> conteoPorTipo = new Dictionary<string, int>();
    private int totalDepositados = 0;
    private int intentosActuales = 0;

    private PlayerInventario playerInventario;

    private void Start()
    {
        foreach (string tipo in tiposPermitidos)
        {
            conteoPorTipo[tipo] = 0;
        }
    }

    private void Update()
    {
        if (playerInventario != null && Input.GetKeyDown(KeyCode.F))
        {
            IntentarDepositar();
        }
    }

    private void IntentarDepositar()
    {
        if (!playerInventario.TieneRecolectable()) return;

        Recolectable r = playerInventario.recolectableActual;
        string tipo = r.id;

        //Recolectable prohibido
        if (tipo == tiposProhibidos)
        {
            intentosActuales++;
            audioSource.PlayOneShot(sonidoError);
            cartelito.Mostrar("Mama:- ¡No te pases, solo lo que te he pedido! Intento #" + intentosActuales);

            if (r.filtroRecolectable != null)
                r.filtroRecolectable.SetActive(false);

            Destroy(r.gameObject);
            playerInventario.SoltarRecolectable();

            if (intentosActuales >= intentosMaximos)
            {
                //ESCENA DERROTA
            }

            return;
        }

        //Recolectable permitido pero el tipo ya está completo
        if (tiposPermitidos.Contains(tipo) && conteoPorTipo[tipo] >= cantidadPorTipo)
        {
            audioSource.PlayOneShot(sonidoError);
            cartelito.Mostrar($"Mama:- Ya no necesitamos {tipo}, déjame devolverlo a la góndola.");

            if (r.filtroRecolectable != null)
                r.filtroRecolectable.SetActive(false);

            Destroy(r.gameObject);
            playerInventario.SoltarRecolectable();

            return;
        }

        //Recolectable permitido que no está completo
        if (tiposPermitidos.Contains(tipo))
        {
            conteoPorTipo[tipo]++;
            totalDepositados++;
            audioSource.PlayOneShot(sonidoCorrecto);

            if (r.filtroRecolectable != null)
                r.filtroRecolectable.SetActive(false);

            Destroy(r.gameObject);
            playerInventario.SoltarRecolectable();

            cartelito.Mostrar($"Puesto en el carrito: {tipo} ({conteoPorTipo[tipo]}/{cantidadPorTipo})");

            if (conteoPorTipo[tipo] == cantidadPorTipo)
            {
                StartCoroutine(EsperarYReproducirSonidoFinal(tipo));
            }

            return;
        }
    }

    private IEnumerator EsperarYReproducirSonidoFinal(string tipo)
    {
        audioSource.PlayOneShot(sonidoTipoCompleto);
        cartelito.Mostrar($"Mamá:- Ya tenemos todos los {tipo}s necesarios, no necesitamos más...");

        yield return new WaitForSeconds(0.4f);

        if (totalDepositados >= cantidadPorTipo * tiposPermitidos.Count)
        {
            audioSource.PlayOneShot(sonidoCarritoLleno);
            
            //ESCENA VICTORIA
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInventario = collision.GetComponent<PlayerInventario>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInventario = null;
        }
    }
}
