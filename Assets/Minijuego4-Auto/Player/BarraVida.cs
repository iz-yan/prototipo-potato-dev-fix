using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private AutoBehaviour autoBehaviour;
    private float vidaMaxima;

    void Start()
    {
        GameObject autoObj = GameObject.FindWithTag("auto");

        if (autoObj == null)
        {
            Debug.LogError("No se encontró ningún GameObject con el tag 'auto'");
            return;
        }

        autoBehaviour = autoObj.GetComponent<AutoBehaviour>();

        if (autoBehaviour == null)
        {
            Debug.LogError("El GameObject con tag 'auto' no tiene el componente AutoBehaviour");
            return;
        }

        if (rellenoBarraVida == null)
        {
            Debug.LogError("No se asignó la imagen rellenoBarraVida en el Inspector");
            return;
        }

        vidaMaxima = autoBehaviour.vida;
    }

    void Update()
    {
        if (autoBehaviour != null && vidaMaxima > 0)
        {
            rellenoBarraVida.fillAmount = autoBehaviour.vida / vidaMaxima;
        }
    }
}
