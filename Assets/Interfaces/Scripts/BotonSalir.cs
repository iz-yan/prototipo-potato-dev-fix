using UnityEngine;

public class BotonSalir : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoSalir;
    public float delay = 0.3f;

    public void SalirDelJuego()
    {
        if (audioSource != null && sonidoSalir != null)
        {
            audioSource.PlayOneShot(sonidoSalir);
            Invoke("CerrarApp", delay);
        }
        else
        {
            CerrarApp();
        }
    }

    void CerrarApp()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
