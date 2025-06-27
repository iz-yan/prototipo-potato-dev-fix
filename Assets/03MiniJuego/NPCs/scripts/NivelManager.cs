using UnityEngine;

public class NivelManager : MonoBehaviour
{
    public static NivelManager Instance;

    [SerializeField] private GameObject[] chanchitos;
    [SerializeField] private GameObject[] pollitos;
    [SerializeField] private string[] problemasNivel2;

    private int nivelActual = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AvanzarNivel()
    {
        nivelActual++;
        ConfigurarNivel(nivelActual);
    }

    private void ConfigurarNivel(int nivel)
    {
        switch (nivel)
        {
            case 1:
                ActivarAnimales(chanchitos, true);
                ActivarAnimales(pollitos, false);
                break;

            case 2:
                ActivarAnimales(chanchitos, false);
                ActivarAnimales(pollitos, true);

                // Configurar nuevos problemas matemáticos
                ContadorDeAnimales contador = FindFirstObjectByType<ContadorDeAnimales>();
                if (contador != null)
                {
                    contador.GenerarProblemaDificil();
                }
                break;
        }
    }

    private void ActivarAnimales(GameObject[] animales, bool activar)
    {
        foreach (GameObject animal in animales)
        {
            animal.SetActive(activar);
            if (activar)
            {
                Animal script = animal.GetComponent<Animal>();
                script.GenerarValorAleatorio();
            }
        }
    }
}