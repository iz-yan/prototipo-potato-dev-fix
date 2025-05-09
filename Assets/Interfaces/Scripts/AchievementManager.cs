using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    public List<Achievement> logros = new List<Achievement>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CrearLogros();

        CargarLogros();
    }

    private void CrearLogros()
    {
        logros.Add(new Achievement("nivel1", "Nivel 1 Completado", "Has completado el nivel 1."));
        logros.Add(new Achievement("nivel2", "Nivel 2 Completado", "Has completado el nivel 2."));
        logros.Add(new Achievement("nivel3", "Nivel 3 Completado", "Has completado el nivel 3."));
    }

    private void CargarLogros()
    {
        foreach (Achievement logro in logros)
        {
            if (PlayerPrefs.GetInt(logro.id, 0) == 1)
            {
                logro.desbloqueado = true;
            }
        }
    }

    public void LogroCompletado(string id)
    {
        Achievement logro = logros.Find(l => l.id == id);
        if (logro != null && !logro.desbloqueado)
        {
            logro.Desbloquear();

            PlayerPrefs.SetInt("nuevo_logro", 1);
            PlayerPrefs.Save();
        }
    }

}
