using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BotonVolverAlMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoClick;

    public void VolverAlMenu()
    {
        StartCoroutine(EsperarYCargarMenu());
    }

    IEnumerator EsperarYCargarMenu()
    {
        if (audioSource && sonidoClick)
        {
            audioSource.PlayOneShot(sonidoClick);
            yield return new WaitForSecondsRealtime(0.4f);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
