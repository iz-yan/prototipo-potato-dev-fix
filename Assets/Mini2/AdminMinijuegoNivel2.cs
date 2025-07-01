using UnityEngine;
using UnityEngine.SceneManagement;

public class AdminMinijuegoNivel2 : MonoBehaviour
{
    public string escenaVictoria;
    public string escenaDerrota;

    public int maxRetos = 3;

    private int retosActuales = 0;

    private bool nivelFinalizado = false;

    public void RetoRecibido(int cantidadActual)
    {
        if (nivelFinalizado) return;

        retosActuales = cantidadActual;
        Debug.Log("Retos actuales: " + retosActuales);

        if (retosActuales >= maxRetos)
        {
            nivelFinalizado = true;
            Debug.Log("¡Demasiados dulces! Mamá se enoja...");
            SceneManager.LoadScene(escenaDerrota);
        }
    }

    public void NivelCompletado()
    {
        if (nivelFinalizado) return;

        nivelFinalizado = true;
        Debug.Log("¡Nivel completado exitosamente!");
        SceneManager.LoadScene(escenaVictoria);
    }
}
