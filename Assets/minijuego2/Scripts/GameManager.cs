using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int PuntosTotales { get { return puntosTotales; } }
    private int puntosTotales;
    public GameObject[] confettis; //confetti
    public Transform[] posicionesConfetti;//confetti
    public GameObject particulasDestruccion;// explosion
    public int Vidas { get; private set; } = 5;

    private bool yaGano = false;
    private bool yaPerdio = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SumarP(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        Debug.Log(puntosTotales);

        if (puntosTotales >= 5 && !yaGano)
        {
            yaGano = true;
            Debug.Log("¡Ganaste!");

            //Desbloquear el nivel 3
            DesbloqueoNiveles.DesbloquearNivel("nivel_3_desbloqueado");

            //Mostrar el logro de nivel completado
            //      AchievementManager.instance.LogroCompletado("nivel2");

            // Instanciar confettis

            ActivarConfetti();

            StartCoroutine(CargarEscenaConDelay("Victoria", 2.5f));
        }

    }

    public void RestarVida()
    {
        Vidas--;
        Debug.Log("Vidas restantes: " + Vidas);

        if (Vidas <= 1)
        {
            Debug.Log("Te queda una vida");
        }

        if (Vidas <= 0 && !yaPerdio)
        {
            yaPerdio = true;
            Debug.Log("¡Juego terminado!");
            StartCoroutine(CargarEscenaConDelay("GameOver", 2.5f));
        }
    }
    private void ActivarConfetti()
    {
        for (int i = 0; i < posicionesConfetti.Length && i < confettis.Length; i++)
        {
            GameObject nuevo = Instantiate(confettis[i], posicionesConfetti[i].position, posicionesConfetti[i].rotation);
            ParticleSystem ps = nuevo.GetComponent<ParticleSystem>();

            if (ps != null && !ps.isPlaying)
            {
                ps.Play();
            }
        }
    }
    public void LimpiarComida()
    {
        GameObject[] comidas = GameObject.FindGameObjectsWithTag("comida");
        foreach (GameObject comida in comidas)
        {
            // Instanciar partículas en la posición de la comida
            if (particulasDestruccion != null)
            {
                Instantiate(particulasDestruccion, comida.transform.position, Quaternion.identity);
            }
            Destroy(comida);
        }
    }
    private IEnumerator CargarEscenaConDelay(string nombreEscena, float delay)
    {
        LimpiarComida(); // Limpia toda la comida antes de cambiar de escena
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nombreEscena);
    }
}
