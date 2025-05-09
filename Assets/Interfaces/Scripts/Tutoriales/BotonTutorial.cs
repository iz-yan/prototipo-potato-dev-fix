using UnityEngine;
using System.Collections;

public class BotonTutorial : MonoBehaviour
{
    public GameObject panelTutorial;
    public AudioSource audioSource;
    public AudioClip sonidoClick;
    public float delay = 0.4f;

    public void MostrarTutorial()
    {
        StartCoroutine(MostrarConSonido());
    }

    public void OcultarTutorial()
    {
        StartCoroutine(OcultarConSonido());
    }

    IEnumerator MostrarConSonido()
    {
        if (audioSource && sonidoClick)
        {
            audioSource.PlayOneShot(sonidoClick);
            yield return new WaitForSecondsRealtime(delay);
        }

        if (panelTutorial != null)
            panelTutorial.SetActive(true);
    }

    IEnumerator OcultarConSonido()
    {
        if (audioSource && sonidoClick)
        {
            audioSource.PlayOneShot(sonidoClick);
            yield return new WaitForSecondsRealtime(delay);
        }

        if (panelTutorial != null)
            panelTutorial.SetActive(false);
    }
}
