using UnityEngine;

public class BotonReproducir : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoClick;

    public void ReproducirSonido()
    {
        if (audioSource != null && sonidoClick != null)
            audioSource.PlayOneShot(sonidoClick);
    }
}
