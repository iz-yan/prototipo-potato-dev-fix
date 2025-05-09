using UnityEngine;

public class ResetAchievements : MonoBehaviour
{
    public void ResetearTodosLosLogros()
    {
        foreach (Achievement logro in AchievementManager.instance.logros)
        {
            PlayerPrefs.DeleteKey(logro.id);
            logro.desbloqueado = false;
        }

        PlayerPrefs.Save();
        Debug.Log("Todos los logros han sido reseteados.");
    }
}
