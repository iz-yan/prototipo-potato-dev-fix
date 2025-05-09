using UnityEngine;
using UnityEngine.UI;

public class DesbloqueoLogros : MonoBehaviour
{
    [System.Serializable]
    public class Logro
    {
        public GameObject filtroOscuro;
        public Image casillaVacia;
        public Image casillaMarcada;
        public string nombreClavePlayerPrefs;
    }

    public Logro[] logros;

    private void Start()
    {
        ActualizarEstadoLogros();
    }

    public void ActualizarEstadoLogros()
    {
        foreach (Logro logro in logros)
        {
            bool desbloqueado = PlayerPrefs.GetInt(logro.nombreClavePlayerPrefs, 0) == 1;

            // Si el logro está desbloqueado
            if (desbloqueado)
            {
                if (logro.filtroOscuro != null)
                    logro.filtroOscuro.SetActive(false);

                if (logro.casillaVacia != null)
                    logro.casillaVacia.gameObject.SetActive(false);

                if (logro.casillaMarcada != null)
                    logro.casillaMarcada.gameObject.SetActive(true);
            }
            else
            {
                if (logro.filtroOscuro != null)
                    logro.filtroOscuro.SetActive(true);

                if (logro.casillaVacia != null)
                    logro.casillaVacia.gameObject.SetActive(true);

                if (logro.casillaMarcada != null)
                    logro.casillaMarcada.gameObject.SetActive(false);
            }
        }
    }

    public static void DesbloquearLogro(string nombreClave)
    {
        PlayerPrefs.SetInt(nombreClave, 1);
        PlayerPrefs.Save();
    }
}
