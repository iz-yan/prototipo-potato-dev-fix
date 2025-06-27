using TMPro;
using UnityEngine;

public class ContadorDeAnimales : MonoBehaviour
{
    [Header("Configuración Matemática")]
    [SerializeField] private int operando1;
    [SerializeField] private int operando2;
    [SerializeField] private string operador = "+";
    [SerializeField] private int respuestaCorrecta;

    [Header("Referencias")]
    [SerializeField] private GameObject npcOculto;
    [SerializeField] private string tagAnimal = "Animal";
    [SerializeField] private TMP_Text problemaTexto;

    private int contadorAnimales = 0;

    private void Start()
    {
        GenerarProblema();
        ActualizarUI();
    }

    private void GenerarProblema()
    {
        operando1 = Random.Range(1, 10);
        operando2 = Random.Range(1, 10);

        switch (operador)
        {
            case "+":
                respuestaCorrecta = operando1 + operando2;
                break;
            case "-":
                respuestaCorrecta = operando1 - operando2;
                break;
            case "*":
                respuestaCorrecta = operando1 * operando2;
                break;
        }
    }

    private void ActualizarUI()
    {
        if (problemaTexto != null)
        {
            problemaTexto.text = $"{operando1} {operador} {operando2} = ?";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagAnimal))
        {
            Animal animal = collision.GetComponent<Animal>();
            if (animal != null && animal.IsCaught)
            {
                VerificarRespuesta(animal);
            }
        }
    }

    private void VerificarRespuesta(Animal animal)
    {
        if (animal.ValorAnimal == respuestaCorrecta)
        {
            contadorAnimales++;
            PlayerScore.Instance.GanarPuntos(animal.Puntaje);

            // Desactivar el animal correcto
            animal.gameObject.SetActive(false);

            if (contadorAnimales >= 3)
            {
                CompletarCorral();
            }
        }
        else
        {
            // Devolver animal si es incorrecto
            animal.Release();
        }
    }

    private void CompletarCorral()
    {
        GameObject[] animalesRestantes = GameObject.FindGameObjectsWithTag(tagAnimal);
        foreach (GameObject animal in animalesRestantes)
        {
            animal.SetActive(false);
        }

        if (npcOculto != null)
        {
            npcOculto.SetActive(true);
        }

        // Notificar al gestor de niveles
        NivelManager.Instance.AvanzarNivel();
    }
    // En ContadorDeAnimales
    public void GenerarProblemaDificil()
    {
        operador = Random.Range(0, 2) == 0 ? "+" : "-";
        operando1 = Random.Range(5, 15);
        operando2 = Random.Range(1, 10);

        if (operador == "-" && operando2 > operando1)
        {
            // Asegurar que no sea negativo
            int temp = operando1;
            operando1 = operando2;
            operando2 = temp;
        }

        respuestaCorrecta = operador == "+" ?
            operando1 + operando2 :
            operando1 - operando2;

        ActualizarUI();
    }
}