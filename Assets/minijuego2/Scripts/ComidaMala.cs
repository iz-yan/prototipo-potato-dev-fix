using UnityEngine;

public class ComidaMala : MonoBehaviour
{
    public AudioClip sonidoChoque;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.RestarVida();
            Destroy(gameObject);
            AudioManager.Instance.ReproducirSonido(sonidoChoque);
        }
    }
}
