using UnityEngine;
using UnityEngine.SceneManagement;

public class AdminMinijuego : MonoBehaviour
{
    public int contenedoresTotales = 4;
    private int contenedoresCompletados = 0;

    public string pantallaVictoria = "Victoria";

    public void ContenedorCompletado()
    {
        contenedoresCompletados++;
        Debug.Log("Contenedor completados: " + contenedoresCompletados);

        if (contenedoresCompletados >= contenedoresTotales)
        {
            Debug.Log("¡Todos los contenedores han sido completados!");
            SceneManager.LoadScene(pantallaVictoria);
        }
    }
}
