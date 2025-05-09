using UnityEngine;

public class BotonLogros : MonoBehaviour
{
    public GameObject panelLogros;
    public GameObject iconoNuevoLogro;
    public AudioSource audioSource;
    public AudioClip sonidoClick;
    public float delay = 0.3f;

    void Start()
    {
        if (PlayerPrefs.GetInt("nuevo_logro", 0) == 1)
        {
            iconoNuevoLogro.SetActive(true);
        }
        else
        {
            iconoNuevoLogro.SetActive(false);
        }
    }

    public void MostrarLogros()
    {
        if (audioSource != null && sonidoClick != null)
            audioSource.PlayOneShot(sonidoClick);

        if (panelLogros != null)
            panelLogros.SetActive(true);

        AlAbrirLogros();
    }

    public void OcultarLogros()
    {
        if (audioSource != null && sonidoClick != null)
        {
            audioSource.PlayOneShot(sonidoClick);
            Invoke(nameof(DesactivarPanel), delay);
        }
        else
        {
            DesactivarPanel();
        }
    }

    private void DesactivarPanel()
    {
        if (panelLogros != null)
            panelLogros.SetActive(false);
    }

    private void AlAbrirLogros()
    {
        PlayerPrefs.SetInt("nuevo_logro", 0);
        PlayerPrefs.Save();

        iconoNuevoLogro.SetActive(false);
    }
}
