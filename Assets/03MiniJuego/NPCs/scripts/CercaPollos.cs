using UnityEngine;

public class CercaPollos : MonoBehaviour
{
    private bool estaLlenaPollos = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip acierto;
    [SerializeField] private AudioClip error;
    [SerializeField] private float volumen;
    public bool EstaLlenaPollos { get => estaLlenaPollos; set => estaLlenaPollos = value; }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pollo"))
        {
            Animal pollo = collision.GetComponent<Animal>();
            if (pollo!=null && pollo.FueAtrapado)
            {
                Debug.Log("adiooPollito");
                collision.gameObject.SetActive(false);
                PlayerScore.Instance.GanarPuntos(pollo.Puntaje);
                audioSource.PlayOneShot(acierto,volumen);
            }
        }
        else
        {
            Animal animal = collision.GetComponent<Animal>();
            if (animal!= null && animal.FueAtrapado)
            {
                PlayerScore.Instance.perderVida();
                audioSource.PlayOneShot(error, volumen);
                //Ingresar Sonido de ERror
            }
        }
    }
    public bool CercaLlena()
    {
        Debug.Log("Pollos estaLLena");
        return EstaLlenaPollos = GameObject.FindGameObjectsWithTag("Pollo").Length == 0;
    }
}
