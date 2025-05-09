using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoClick;
    public float delay = 0.3f;

    public void VolverAlMenu()
    {
        if (audioSource && sonidoClick)
        {
            audioSource.PlayOneShot(sonidoClick);
            Invoke(nameof(CargarMenu), delay);
        }
        else
        {
            CargarMenu();
        }
    }

    void CargarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
