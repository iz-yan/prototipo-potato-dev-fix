using UnityEngine;
using System.Collections;

public class BotonSaltar : MonoBehaviour
{
    public GameObject panelTutorial;
    public AudioSource audioSource;
    public AudioClip sonidoClick;
    public float delay = 0.4f;

    public void SaltarTutorial()
    {
        StartCoroutine(SaltarConSonido());
    }

    IEnumerator SaltarConSonido()
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
