using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BehaviourNPC : MonoBehaviour
{
    private bool isPlayer;
    private bool didDialogueStart;
    private int indexLine;
    private int puntosRespuestaCorrecta = 100;
    [SerializeField] private GameObject npcSiguiente;
    [SerializeField]private GameObject aviso;
    [SerializeField, TextArea(6, 4)] private string[] lineaTexto;
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TMP_Text textoDialogo;
    [SerializeField] private GameObject panelPreguntas;
    [SerializeField] private Button[] botonesRespuestas;
    [SerializeField] private TMP_Text textoPregunta;
    private int preguntaActual = 0;
    private bool yaPregunto=false;

    [System.Serializable]
    public struct Pregunta
    {
        public string pregunta;
        public string[] respuestas;
        public int respuestaCorrecta;
    }

    [SerializeField] private Pregunta[] preguntas;

    void Start()
    {
        for (int i = 0; i < botonesRespuestas.Length; i++)
        {
            int index = i;
            botonesRespuestas[i].onClick.RemoveAllListeners();
            botonesRespuestas[i].onClick.AddListener(() => ResponderPregunta(index));
        }

        panelPreguntas.SetActive(false);
    }

    void Update()
    {
        if (isPlayer && Input.GetKeyUp(KeyCode.Space))
        {
            if (!didDialogueStart)
            {
                EmpezarDialogo();
                preguntaActual = 0;
            }
            else if (textoDialogo.text == lineaTexto[indexLine])
            {
                if (indexLine == 2 || indexLine == 5 || indexLine == 8)
                {
                    MostrarPregunta(preguntaActual);
                }
                else
                {
                    SiguienteLinea();
                }
            }
            else
            {
                StopAllCoroutines();
                textoDialogo.text = lineaTexto[indexLine];
            }
        }
    }

    public void EmpezarDialogo()
    {
        didDialogueStart = true;
        panelDialogo.SetActive(true);
        aviso.SetActive(false);
        indexLine = yaPregunto ? lineaTexto.Length - 1 : 0; //estoy usando el operador ternario para hacerlo de forma mas compacta a los condicionales
                                                            // son similares equivalentes al iff y al else;
        StartCoroutine(MostrarLineas());
    }
    private IEnumerator MostrarLineas()
    {
        textoDialogo.text=string.Empty;
        foreach(char c in lineaTexto[indexLine])
        {
            textoDialogo.text+=c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void SiguienteLinea()
    {
        if (yaPregunto && didDialogueStart)
        {
            indexLine = lineaTexto.Length - 1;
            textoDialogo.text = lineaTexto[indexLine];

            didDialogueStart = false;
            panelDialogo.SetActive(false);
            aviso.SetActive(true);
            return;
        }
        else
        {
            indexLine++;
            if (indexLine < lineaTexto.Length-1)
            {
                StartCoroutine(MostrarLineas());
            }
            else
            {
                didDialogueStart = false;
                panelDialogo.SetActive(false);
                aviso.SetActive(true);
                yaPregunto = true;
                if (npcSiguiente != null)
                {
                    npcSiguiente.SetActive(true);
                }
                else
                { 
                    //Mostrar el logro de nivel completado
                    AchievementManager.instance.LogroCompletado("nivel2");

                    SceneManager.LoadScene("Victoria");
                }
            }
        }
    }

    private void MostrarPregunta(int numPregunta)
    {
        StopAllCoroutines();
        panelDialogo.SetActive(false);

        panelPreguntas.SetActive(true);
        textoPregunta.text = preguntas[numPregunta].pregunta;

        for (int i = 0; i < botonesRespuestas.Length; i++)
        {
            botonesRespuestas[i].GetComponentInChildren<TMP_Text>().text = preguntas[numPregunta].respuestas[i];
        }
    }

    private void ResponderPregunta(int respuestaIndex)
    {
        Button botonPresionado = botonesRespuestas[respuestaIndex];

        if (respuestaIndex != preguntas[preguntaActual].respuestaCorrecta)
        {
            Debug.Log("Respuesta incorrecta!");

            botonPresionado.image.color = Color.red;

            PlayerScore.Instance.perderVida();

            return;
        }
        else
        {
            Debug.Log("Respuesta correcta!");
            PlayerScore.Instance.GanarPuntos(puntosRespuestaCorrecta);

            foreach (var boton in botonesRespuestas)
            {
                boton.image.color = Color.white;
            }

            panelPreguntas.SetActive(false);
            panelDialogo.SetActive(true);
            preguntaActual++;
            SiguienteLinea();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
            aviso.SetActive(true);
            Debug.Log("ya estoy");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
            Debug.Log("me fui");
            aviso.SetActive(false);
        }
    }
}
