using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[System.Serializable]
public class PreguntaMatematica
{
    public string enunciado;
    public int respuestaCorrecta;
    public string[] opcionesRespuesta;
}

public class SistemaPreguntas : MonoBehaviour
{
    [Header("Configuración de Preguntas")]
    [SerializeField] private PreguntaMatematica[] preguntas;
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private TMP_Text textoPregunta;
    [SerializeField] private Button[] botonesRespuesta;

    private int preguntaActualIndex = -1;
    private int respuestaCorrectaActual;
    private Action<bool> callbackRespuesta;

    public void ConfigurarPreguntas(PreguntaMatematica[] nuevasPreguntas)
    {
        preguntas = nuevasPreguntas;
    }

    public void MostrarPregunta(int indicePregunta, Action<bool> callback)
    {
        if (indicePregunta < 0 || indicePregunta >= preguntas.Length) return;

        preguntaActualIndex = indicePregunta;
        callbackRespuesta = callback;

        // Configurar pregunta
        var pregunta = preguntas[preguntaActualIndex];
        textoPregunta.text = pregunta.enunciado;
        respuestaCorrectaActual = pregunta.respuestaCorrecta;

        // Configurar botones
        for (int i = 0; i < botonesRespuesta.Length; i++)
        {
            if (i < pregunta.opcionesRespuesta.Length)
            {
                botonesRespuesta[i].gameObject.SetActive(true);
                botonesRespuesta[i].GetComponentInChildren<TMP_Text>().text = pregunta.opcionesRespuesta[i];

                // Guardar índice de respuesta correcta en el botón
                int respuestaIndex = i;
                botonesRespuesta[i].onClick.RemoveAllListeners();
                botonesRespuesta[i].onClick.AddListener(() => Responder(respuestaIndex));
            }
            else
            {
                botonesRespuesta[i].gameObject.SetActive(false);
            }
        }

        panelPregunta.SetActive(true);
    }

    private void Responder(int respuestaSeleccionada)
    {
        bool esCorrecta = (respuestaSeleccionada == respuestaCorrectaActual);
        panelPregunta.SetActive(false);
        callbackRespuesta?.Invoke(esCorrecta);
    }
}