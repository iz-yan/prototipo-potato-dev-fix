using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Dialogues : MonoBehaviour
{
    public TextMeshProUGUI dialogo1;
    public TextMeshProUGUI dialogo2;

    public CanvasGroup dialogo1canvasGroup;
    public CanvasGroup dialogo2canvasGroup;
    public Button botonSiguiente;
    public CanvasGroup botonCanvasGroup;
    public float duracionFade = 0.5f;
    public ProtaCaraAnimator protaAnimador;

    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject titulo;
    public string[] lineas;

    private int indiceLinea = 0;
    private bool puedeAvanzar = true;


    void Start()
    {
        tutorial1.SetActive(false);
        tutorial2.SetActive(false);
        titulo.SetActive(false);

        dialogo1canvasGroup.gameObject.SetActive(true);
        dialogo2canvasGroup.gameObject.SetActive(true);

        dialogo1canvasGroup.alpha = 0f;
        dialogo2canvasGroup.alpha = 0f;
        botonCanvasGroup.alpha = 0f;

        dialogo1.text = "";
        dialogo2.text = "";

        botonCanvasGroup.interactable = false;
        botonSiguiente.onClick.AddListener(MostrarSiguienteLinea);

        MostrarSiguienteLinea();
    }

    public void MostrarSiguienteLinea()
    {
        if (!puedeAvanzar || indiceLinea >= lineas.Length)
        {
            Debug.LogWarning("No hay más líneas para mostrar o índice fuera de rango.");
            return;
        }

        // Mostrar imágenes si es la segunda línea (puedes ajustar esto a otra lógica si querés)
        if (indiceLinea == 1)
        {
            tutorial1.SetActive(true);
            tutorial2.SetActive(true);
            titulo.SetActive(true);

            StartCoroutine(FadeInImagen(tutorial1.GetComponent<CanvasGroup>()));
            StartCoroutine(FadeInImagen(tutorial2.GetComponent<CanvasGroup>()));
            StartCoroutine(FadeInImagen(titulo.GetComponent<CanvasGroup>()));
        }

        // Alternar entre dialogo1 y dialogo2 para usar distintas posiciones
        if (indiceLinea % 2 == 0)
        {
            StartCoroutine(FadeYMostrar(dialogo1, dialogo1canvasGroup, lineas[indiceLinea]));
        }
        else
        {
            StartCoroutine(FadeYMostrar(dialogo2, dialogo2canvasGroup, lineas[indiceLinea]));
        }

        indiceLinea++;
    }

    IEnumerator FadeYMostrar(TextMeshProUGUI textoTMP, CanvasGroup canvasGroup, string nuevaLinea)
    {
        puedeAvanzar = false;

        // Desactivar ambos objetos de diálogo
        dialogo1canvasGroup.gameObject.SetActive(false);
        dialogo2canvasGroup.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);
        protaAnimador.IniciarAnimacion();
        yield return StartCoroutine(FadeOut());

        // Activar el que va a usarse y actualizar texto
        canvasGroup.gameObject.SetActive(true);
        textoTMP.text = nuevaLinea;

        yield return StartCoroutine(FadeInCanvas(canvasGroup));
        yield return StartCoroutine(FadeInBoton());

        puedeAvanzar = true;
    }

    IEnumerator FadeInCanvas(CanvasGroup canvasGroup)
    {
        float t = 0f;
        while (t < duracionFade)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / duracionFade);
            yield return null;
        }
        canvasGroup.alpha = 1f;
        protaAnimador.DetenerAnimacion();
    }

    IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < duracionFade)
        {
            t += Time.deltaTime;
            dialogo1canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / duracionFade);
            dialogo2canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / duracionFade);
            yield return null;
        }
        dialogo1canvasGroup.alpha = 0f;
        dialogo2canvasGroup.alpha = 0f;
    }

    IEnumerator FadeInBoton()
    {
        botonCanvasGroup.alpha = 1f; // Aseguramos que el botón esté visible

        botonCanvasGroup.interactable = true; // Hacerlo interactuable

        // Agregar el control de fade out cuando el botón es presionado
        botonSiguiente.onClick.AddListener(() => StartCoroutine(FadeOutBoton()));

        yield break;
    }

    IEnumerator FadeOutBoton()
    {
        float t = 0f;
        while (t < duracionFade)
        {
            t += Time.deltaTime;
            botonCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t / duracionFade); // Hacer fade out
            yield return null;
        }
        botonCanvasGroup.alpha = 0f;
        botonCanvasGroup.interactable = false; // Desactivamos la interactividad después de hacer fade out
    }

    IEnumerator FadeInImagen(CanvasGroup imagenGroup)
    {
        imagenGroup.alpha = 0f;
        float t = 0f;
        while (t < duracionFade)
        {
            t += Time.deltaTime;
            imagenGroup.alpha = Mathf.Lerp(0f, 1f, t / duracionFade);
            yield return null;
        }
        imagenGroup.alpha = 1f;
    }
}