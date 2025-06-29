using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DialogoConNpc : MonoBehaviour
{
    [Header("Elementos De Dialogo")]
    [SerializeField] private GameObject aviso;
    [SerializeField, TextArea(6, 4)] private string[] parrafosDeDialogo;
    [SerializeField, TextArea(6, 4)] private string[] parrafosDeDialogoFinal;
    [SerializeField] GameObject panelParrafos;
    [SerializeField] TMP_Text textoTMP;
    private string[] dialogoActual;
    private int indexParrafo = 0;

    [Header("Imagenes y Colores")]
    [SerializeField] private Image playerImage;
    [SerializeField] private Image npcImage;
    [SerializeField] private Color colorActivo = Color.white;
    [SerializeField] private Color colorInactivo = new Color(0.7f, 0.7f, 0.7f, 0.5f);

    [Header("Sistema de Preguntas")]
    [SerializeField] private SistemaPreguntas sistemaPreguntas;
    [SerializeField] private PreguntaMatematica[] preguntasMatematicas;
    private bool esperandoRespuesta = false;

    private bool isPlayer = false;
    private bool isDialogueStart = false;
    private bool npcHabla;
    private bool isEnd = false;

    public bool IsEnd { get => isEnd; set => isEnd = value; }

    void Start()
    {
        if (sistemaPreguntas != null && preguntasMatematicas.Length > 0)
        {
            sistemaPreguntas.ConfigurarPreguntas(preguntasMatematicas);
        }
    }

    void Update()
    {
        if (isPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            dialogoActual = IsEnd ? parrafosDeDialogoFinal : parrafosDeDialogo;
            ElegirDialogo(dialogoActual);
        }
    }

    public void ElegirDialogo(string[] parrafo)
    {
        if (!isDialogueStart)
        {
            EmpezarDialogo(parrafo);
        }
        else if (textoTMP.text == parrafo[indexParrafo])
        {
            SiguienteParrafo(parrafo);
        }
    }

    public void EmpezarDialogo(string[] parrafos)
    {
        panelParrafos.SetActive(true);
        aviso.SetActive(false);
        StartCoroutine(MostrarLineas(parrafos));
        isDialogueStart = true;
        CambiarEnfoque(true);
    }

    public void SiguienteParrafo(string[] parrafos)
    {
        if (esperandoRespuesta) return;

        if (indexParrafo < parrafos.Length - 1)
        {
            indexParrafo++;

            if (indexParrafo == 2) // Pregunta en la tercera línea
            {
                panelParrafos.SetActive(false); // Desactiva panel de diálogo
                MostrarPregunta();
            }
            else if (indexParrafo == 4) // Pregunta en la quinta línea
            {
                panelParrafos.SetActive(false); // Desactiva panel de diálogo
                MostrarPregunta();
            }
            else
            {
                StartCoroutine(MostrarLineas(parrafos));
                npcHabla = (indexParrafo % 2 == 0);
                CambiarEnfoque(npcHabla);
            }
        }
        else
        {
            FinalizarDialogo();
        }
    }

    private void FinalizarDialogo()
    {
        panelParrafos.SetActive(false);
        isDialogueStart = false;
        indexParrafo = 0;

        if (!IsEnd)
        {
            isPlayer = false;
            aviso.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene("Victoria");
        }
        IsEnd = true;
    }

    private void MostrarPregunta()
    {
        if (sistemaPreguntas == null || preguntasMatematicas.Length == 0) return;

        int preguntaIndex = UnityEngine.Random.Range(0, preguntasMatematicas.Length);
        esperandoRespuesta = true;

        sistemaPreguntas.MostrarPregunta(preguntaIndex, (esCorrecta) =>
        {
            if (esCorrecta)
            {
                PlayerScore.Instance.GanarPuntos(10);
                panelParrafos.SetActive(true); // Reactiva panel de diálogo
                StartCoroutine(MostrarLineas(dialogoActual));
                esperandoRespuesta = false;
            }
            // Si es incorrecta, no hacemos nada (el sistema mantiene la pregunta visible)
        });
    }

    public void CambiarEnfoque(bool estaNpcHablando)
    {
        npcImage.color = estaNpcHablando ? colorActivo : colorInactivo;
        playerImage.color = estaNpcHablando ? colorInactivo : colorActivo;
    }

    private IEnumerator MostrarLineas(string[] parrafos)
    {
        textoTMP.text = string.Empty;
        foreach (char c in parrafos[indexParrafo])
        {
            textoTMP.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            aviso.SetActive(true);
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            aviso.SetActive(false);
            isPlayer = false;
        }
    }
}