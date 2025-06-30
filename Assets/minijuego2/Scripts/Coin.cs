using UnityEngine;

public class Coin : MonoBehaviour
{
    public int valor = 1;
    public AudioClip sonidoComida;
    public float rotationSpeed = 80f; // Velocidad de rotación en grados por segundo

    private void Update()
    {
        // Rotación continua alrededor del eje Z (hacia adelante para efecto 2D)
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hay colisión");

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SumarP(valor);
            AudioManager.Instance.ReproducirSonido(sonidoComida);
            Destroy(this.gameObject);
        }
    }
}