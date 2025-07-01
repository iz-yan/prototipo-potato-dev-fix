using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogoConNpc : MonoBehaviour
{
    [Header("Elementos De Dialogo")]
    [SerializeField] private GameObject aviso;
    [SerializeField] private GameObject avisoProximidad;
    [SerializeField, TextArea(6, 4)] private string[] parrafosDeDialogo; // Solo este array ahora
    [SerializeField] GameObject panelParrafos;
    [SerializeField] TMP_Text textoTMP;

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
    private int indexParrafo = 0;
    [SerializeField] private ScriptCamera scriptCamera;
    [SerializeField] private behaviourPlayer playerController;
    [SerializeField] private Transform cameraTargetPosition;

    [SerializeField] private TMP_Text continuarAviso;
    private string continuar = "Presione Espacio >>";

    [Header("Sonidos")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip acierto;
    [SerializeField] private AudioClip error;
    [SerializeField] private float volumen;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (sistemaPreguntas != null && preguntasMatematicas.Length > 0)
        {
            sistemaPreguntas.ConfigurarPreguntas(preguntasMatematicas);
        }
    }

    void Update()
    {
        if (isPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDialogueStart)
            {
                EmpezarDialogo();
            }
            else if (textoTMP.text == parrafosDeDialogo[indexParrafo])
            {
                SiguienteParrafo();
            }
        }
    }

    public void EmpezarDialogo()
    {
        playerController.MovementEnabled=false;
        panelParrafos.SetActive(true);
        aviso.SetActive(false);
        StartCoroutine(MostrarLineas());
        isDialogueStart = true;
        CambiarEnfoque(true);
    }

    public void SiguienteParrafo()
    {
        if (esperandoRespuesta) return;

        if (indexParrafo < parrafosDeDialogo.Length - 1)
        {
            indexParrafo++;

            if (indexParrafo == 2 || indexParrafo == 4 || indexParrafo == 6 || indexParrafo == 8 || indexParrafo == 10) // Preguntas en líneas específicas
            {
                panelParrafos.SetActive(false);
                MostrarPregunta();
            }
            else
            {
                StartCoroutine(MostrarLineas());
                npcHabla = (indexParrafo % 2 == 0);
                CambiarEnfoque(npcHabla);
            }
        }
        else // Al terminar el diálogo, carga la escena
        {
            //playerController.MovementEnabled = true;
            panelParrafos.SetActive(false);
            SceneManager.LoadScene("Victoria");
        }
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
                audioSource.PlayOneShot(acierto, volumen);
                PlayerScore.Instance.GanarPuntos(10);
                panelParrafos.SetActive(true);
                npcHabla = (indexParrafo % 2 == 0);
                CambiarEnfoque(npcHabla);
                StartCoroutine(MostrarLineas());
                esperandoRespuesta = false;
            }
            else audioSource.PlayOneShot(error,volumen);
        });
    }

    public void CambiarEnfoque(bool estaNpcHablando)
    {
        npcImage.color = estaNpcHablando ? colorActivo : colorInactivo;
        playerImage.color = estaNpcHablando ? colorInactivo : colorActivo;
        panelParrafos.GetComponent<Image>().color = Color.white;
    }

    private IEnumerator MostrarLineas()
    {
        continuarAviso.text= string.Empty;
        textoTMP.text = string.Empty;
        foreach (char c in parrafosDeDialogo[indexParrafo])
        {
            textoTMP.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        continuarAviso.text = continuar;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            aviso.SetActive(true);
            if (avisoProximidad != null) avisoProximidad.SetActive(true);
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            aviso.SetActive(false);
            if (avisoProximidad != null) avisoProximidad.SetActive(false);
            isPlayer = false;
        }
    }
}