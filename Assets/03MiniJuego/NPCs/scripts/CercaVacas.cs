using UnityEngine;

public class CercaVacas : MonoBehaviour
{
    private bool estaLlenaVacas = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip acierto;
    [SerializeField] private AudioClip error;
    [SerializeField] private float volumen;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public bool EstaLlenaVacas { get => estaLlenaVacas; set => estaLlenaVacas = value; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vaca"))
        {
            Animal vaca = collision.GetComponent<Animal>();
            if (vaca != null&& vaca.FueAtrapado)
            {
                Debug.Log("adiooVaquita");
                collision.gameObject.SetActive(false);
                PlayerScore.Instance.GanarPuntos(vaca.Puntaje);
                audioSource.PlayOneShot(acierto, volumen);
            }
            
        }
        else
        {
            Animal animal = collision.GetComponent<Animal>();
            if (animal != null&&animal.FueAtrapado)
            {
                PlayerScore.Instance.perderVida();
                audioSource.PlayOneShot(error, volumen);
                //Ingresar Sonido de ERror
            }
        }
        
    }
    public bool CercaLlena()
    {
        Debug.Log("Vaca estaLLena");
        return EstaLlenaVacas = GameObject.FindGameObjectsWithTag("Vaca").Length == 0;
    }
}
