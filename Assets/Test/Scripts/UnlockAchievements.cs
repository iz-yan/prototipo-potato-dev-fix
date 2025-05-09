using UnityEngine;

public class UnlockAllAchievements : MonoBehaviour
{
    // Lista de nombres de los logros que quieres desbloquear
    private string[] nombresLogros = { "nivel1", "nivel2", "nivel3" };

    public void DesbloquearTodosLosLogros()
    {
        foreach (string nombre in nombresLogros)
        {
            PlayerPrefs.SetInt(nombre, 1);
        }

        PlayerPrefs.Save(); // Guardamos todos los cambios
        Debug.Log("¡Todos los logros han sido desbloqueados!");
    }
}
