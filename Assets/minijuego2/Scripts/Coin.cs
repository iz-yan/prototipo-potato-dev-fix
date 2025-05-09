using UnityEngine;

public class Coin : MonoBehaviour
{
    public int valor = 1;
   
    public AudioClip sonidoComida;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hay colisión");

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SumarP(valor);
            Destroy(this.gameObject);
            AudioManager.Instance.ReproducirSonido(sonidoComida);
        }
    }
}
