using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

[System.Serializable]
public class PreguntaMatematica
{
    public string enunciado;
    public int indiceRespuestaCorrecta;
    public string[] opcionesRespuesta;
}

public class SistemaPreguntas : MonoBehaviour
{
    [Header("Configuración de Preguntas")]
    [SerializeField] private PreguntaMatematica[] preguntas;
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private TMP_Text textoPregunta;
    [SerializeField] private Button[] botonesRespuesta;
    [SerializeField] private Color colorCorrecto = Color.green;
    [SerializeField] private Color colorIncorrecto = Color.red;
    [SerializeField] private Color colorNormal = Color.white;

    private int preguntaActualIndex = -1;
    private int respuestaCorrectaActual;
    private Action<bool> callbackRespuesta;
    private bool preguntaRespondida = false;

    public void ConfigurarPreguntas(PreguntaMatematica[] nuevasPreguntas)
    {
        preguntas = nuevasPreguntas;
    }

    public void MostrarPregunta(int indicePregunta, Action<bool> callback)
    {
        if (indicePregunta < 0 || indicePregunta >= preguntas.Length) return;

        preguntaActualIndex = indicePregunta;
        callbackRespuesta = callback;
        preguntaRespondida = false;

        // Configurar pregunta
        var pregunta = preguntas[preguntaActualIndex];
        textoPregunta.text = pregunta.enunciado;
        respuestaCorrectaActual = pregunta.indiceRespuestaCorrecta;

        // Configurar botones
        for (int i = 0; i < botonesRespuesta.Length; i++)
        {
            if (i < pregunta.opcionesRespuesta.Length)
            {
                var boton = botonesRespuesta[i];
                boton.gameObject.SetActive(true);
                boton.GetComponentInChildren<TMP_Text>().text = pregunta.opcionesRespuesta[i];
                boton.interactable = true;
                boton.image.color = colorNormal;

                // Guardar índice de respuesta correcta en el botón
                int respuestaIndex = i;
                boton.onClick.RemoveAllListeners();
                boton.onClick.AddListener(() => Responder(respuestaIndex));
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
        Debug.Log($"Botón seleccionado: {respuestaSeleccionada} | Respuesta correcta: {respuestaCorrectaActual}");
        if (preguntaRespondida) return;

        bool esCorrecta = (respuestaSeleccionada == respuestaCorrectaActual);
        var botonSeleccionado = botonesRespuesta[respuestaSeleccionada];

        if (esCorrecta)
        {
            botonSeleccionado.image.color = colorCorrecto;
            preguntaRespondida = true;
            StartCoroutine(ProcesarRespuesta(true));
        }
        else
        {
            botonSeleccionado.image.color = colorIncorrecto;
            botonSeleccionado.interactable = false;
        }
    }

    private IEnumerator ProcesarRespuesta(bool esCorrecta)
    {
        //preguntaRespondida = true;
        yield return new WaitForSeconds(1f); // Pequeña pausa para ver el feedback

        panelPregunta.SetActive(false);
        callbackRespuesta?.Invoke(esCorrecta);
    }
}