using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarMinijuego : MonoBehaviour
{
    public string nombreEscena;
    public AudioSource audioSource;
    public AudioClip sonidoClick;
    public float delay = 0.4f;

    public void CargarNivel()
    {
        if (audioSource && sonidoClick)
        {
            audioSource.PlayOneShot(sonidoClick);
            Invoke(nameof(CargarEscena), delay);
        }
        else
        {
            CargarEscena();
        }
    }

    void CargarEscena()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
