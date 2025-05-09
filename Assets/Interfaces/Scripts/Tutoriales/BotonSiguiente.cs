using UnityEngine;
using System.Collections;

public class BotonSiguiente : MonoBehaviour
{
    public GameObject panelActual;
    public GameObject panelSiguiente;
    public AudioSource audioSource;
    public AudioClip sonidoClick;
    public float delay = 0.4f;

    public void Avanzar()
    {
        StartCoroutine(AvanzarConSonido());
    }

    IEnumerator AvanzarConSonido()
    {
        if (audioSource && sonidoClick)
        {
            audioSource.PlayOneShot(sonidoClick);
            yield return new WaitForSecondsRealtime(delay);
        }

        if (panelActual != null)
            panelActual.SetActive(false);

        if (panelSiguiente != null)
            panelSiguiente.SetActive(true);
    }
}
