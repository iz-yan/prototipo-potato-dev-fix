using UnityEngine;

public class CercaChanchitos : MonoBehaviour
{
    private bool estaLlenachanchitos= false;

    public bool EstaLlenachanchitos { get => estaLlenachanchitos; set => estaLlenachanchitos = value; }

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
            }
        }
        else
        {
            Animal animal = collision.GetComponent<Animal>();
            if (animal != null&& animal.FueAtrapado)
            {
                PlayerScore.Instance.perderVida();
                //Ingresar Sonido de ERror
            }
        }
    }
    public bool CercaLlena()
    {
        return estaLlenachanchitos = GameObject.FindGameObjectsWithTag("Chanchito").Length == 0;
    }
}
