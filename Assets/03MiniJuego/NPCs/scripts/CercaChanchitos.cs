using UnityEngine;

public class CercaChanchitos : MonoBehaviour
{
    private bool estaLlenachanchitos= false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip acierto;
    [SerializeField] private AudioClip error;
    [SerializeField] private float volumen;

    public bool EstaLlenachanchitos { get => estaLlenachanchitos; set => estaLlenachanchitos = value; }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chanchito"))
        {
            Animal chanchito = collision.GetComponent<Animal>();
            if (chanchito != null && chanchito.FueAtrapado)
            {
                Debug.Log("adiooChanchito");
                collision.gameObject.SetActive(false);
                PlayerScore.Instance.GanarPuntos(chanchito.Puntaje);
                audioSource.PlayOneShot(acierto,volumen);
            }
        }
        else
        {
            Animal animal = collision.GetComponent<Animal>();
            if (animal != null&& animal.FueAtrapado)
            {
                PlayerScore.Instance.perderVida();
                audioSource.PlayOneShot(error, volumen);
                //Ingresar Sonido de ERror
            }
        }
    }
    public bool CercaLlena()
    {
        Debug.Log("Chanchitos esta LLena");
        return estaLlenachanchitos = GameObject.FindGameObjectsWithTag("Chanchito").Length == 0;
    }
}
