using UnityEngine;

public class ComidaMala : MonoBehaviour
{
    public AudioClip sonidoChoque;
    public float rotationSpeed = 80f; // Velocidad de rotación en grados por segundo
    private void Update()
    {
        // Rotación continua alrededor del eje Z (hacia adelante para efecto 2D)
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
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
