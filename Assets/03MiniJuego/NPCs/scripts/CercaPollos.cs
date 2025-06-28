using UnityEngine;

public class CercaPollos : MonoBehaviour
{
    private bool estaLlenaPollos = false;

    public bool EstaLlenaPollos { get => estaLlenaPollos; set => estaLlenaPollos = value; }

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
            }
        }
        else
        {
            Animal animal = collision.GetComponent<Animal>();
            if (animal!= null && animal.FueAtrapado)
            {
                PlayerScore.Instance.perderVida();
                //Ingresar Sonido de ERror
            }
        }
    }
    public bool CercaLlena()
    {
        return EstaLlenaPollos = GameObject.FindGameObjectsWithTag("Pollo").Length == 0;
    }
}
