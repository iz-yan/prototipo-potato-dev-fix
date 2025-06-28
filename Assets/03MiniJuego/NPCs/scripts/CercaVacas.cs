using UnityEngine;

public class CercaVacas : MonoBehaviour
{
    private bool estaLlenaVacas = false;

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
            }
            
        }
        else
        {
            Animal animal = collision.GetComponent<Animal>();
            if (animal != null&&animal.FueAtrapado)
            {
                PlayerScore.Instance.perderVida();
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
