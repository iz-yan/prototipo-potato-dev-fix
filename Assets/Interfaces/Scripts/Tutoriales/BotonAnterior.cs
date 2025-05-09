using UnityEngine;
using System.Collections;

public class BotonAnterior : MonoBehaviour
{
    public GameObject panelActual;
    public GameObject panelAnterior;
    public AudioSource audioSource;
    public AudioClip sonidoClick;
    public float delay = 0.4f;

    public void Retroceder()
    {
        StartCoroutine(RetrocederConSonido());
    }

    IEnumerator RetrocederConSonido()
    {
        if (audioSource && sonidoClick)
        {
            audioSource.PlayOneShot(sonidoClick);
            yield return new WaitForSecondsRealtime(delay);
        }

        if (panelActual != null)
            panelActual.SetActive(false);

        if (panelAnterior != null)
            panelAnterior.SetActive(true);
    }
}
