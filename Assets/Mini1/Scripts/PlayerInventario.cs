using UnityEngine;

public class PlayerInventario : MonoBehaviour
{
    public Recolectable recolectableActual;
    public SpriteRenderer recolectableVisual;

    public AudioSource audioSource;
    public AudioClip sonidoRecolectar;

    public bool TieneRecolectable()
    {
        return recolectableActual != null;
    }

    public void TomarRecolectable(Recolectable recolectable)
    {
        recolectableActual = recolectable;

        Sprite objetoSprite = recolectable.GetComponent<SpriteRenderer>().sprite;
        recolectableVisual.sprite = objetoSprite;
        recolectableVisual.enabled = true;
    }

    public Recolectable SoltarRecolectable()
    {
        Recolectable r = recolectableActual;
        recolectableActual = null;

        recolectableVisual.enabled = false;
        recolectableVisual.sprite = null;

        return r;
    }
}
