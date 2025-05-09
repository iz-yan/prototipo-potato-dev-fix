using UnityEngine;
using System.Collections;

public class BotonReanudar : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoClick;

    public MenuPausa menuPausa;

    public void Reanudar()
    {
        StartCoroutine(ReanudarJuego());
    }

    IEnumerator ReanudarJuego()
    {
        if (audioSource && sonidoClick)
        {
            audioSource.PlayOneShot(sonidoClick);
            yield return new WaitForSecondsRealtime(0.4f);
        }

        menuPausa.Reanudar();
    }
}
