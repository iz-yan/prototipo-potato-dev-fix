using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProtaCaraAnimator : MonoBehaviour
{
    public Image imagenCara;
    public Sprite[] frames;
    public float velocidadCambio = 0.3f;

    private Coroutine animacionActual;

    public void IniciarAnimacion()
    {
        if (animacionActual != null) StopCoroutine(animacionActual);
        animacionActual = StartCoroutine(AnimarBoca());
    }

    public void DetenerAnimacion()
    {
        if (animacionActual != null) StopCoroutine(animacionActual);
        imagenCara.sprite = frames[0]; // boca cerrada por defecto
    }

    IEnumerator AnimarBoca()
    {
        int indice = 0;
        while (true)
        {
            imagenCara.sprite = frames[indice];
            indice = (indice + 1) % frames.Length;
            yield return new WaitForSeconds(velocidadCambio);
        }
    }
}

