using System.Collections;
using TMPro;
using UnityEngine;

public class BehaviourCartel : MonoBehaviour
{
    [SerializeField,TextArea(3,3)] private string[] textoCartel;
    [SerializeField] private TMP_Text letrasDelCartel;
    [SerializeField] private TMP_Text contador;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject[] animalesArray;
    [SerializeField] private GameObject señal;
    [SerializeField] private GameObject avisoProximidad;
    private bool isPlayer;
    private bool didDialogueStart=false;
    private int indexLine;
    private bool animalesLiberados=false;
    [SerializeField] private ScriptCamera scriptCamera;
    [SerializeField] private behaviourPlayer playerController;
    [SerializeField] private Transform cameraTargetPosition;

    [SerializeField] private TMP_Text continuarAviso;
    private string continuar = "Presione Espacio >>";

    public bool AnimalesLiberados { get => animalesLiberados; set => animalesLiberados = value; }

    void Update()
    {
        if (isPlayer && Input.GetKeyUp(KeyCode.Space))
        {
            if (!didDialogueStart)
            {
                MostrarTextCartel();
            }
            else if (letrasDelCartel.text == textoCartel[indexLine])
            {
                SiguienteLinea();
            }
        }
    }

    public void MostrarTextCartel()
    {
        playerController.MovementEnabled = false;
        didDialogueStart = true;
        panel.SetActive(true);
        indexLine = 0;
        StartCoroutine(MostrarCadaLetra());
    }

    private IEnumerator MostrarCadaLetra()
    {
        continuarAviso.text= string.Empty;
        letrasDelCartel.text = string.Empty;
        foreach (char c in textoCartel[indexLine])
        {
            letrasDelCartel.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        continuarAviso.text = continuar;
    }
    public void SiguienteLinea()
    {
        if (didDialogueStart)
        {
            indexLine++;
            if (indexLine < textoCartel.Length)
            {
                StartCoroutine(MostrarCadaLetra());
            }
            else
            {
                playerController.MovementEnabled=true;
                didDialogueStart = false;
                panel.SetActive(false);
                //StartCoroutine(ContadorRegresivo());
                GetComponent<Collider2D>().enabled = false;
                isPlayer = false;  // Esto evita que el Update reaccione al Space
                avisoProximidad.SetActive(false);
                StartCoroutine(ContadorRegresivo());
            }
        }
    }

    private IEnumerator ContadorRegresivo()
    {
        playerController.MovementEnabled = false;
        contador.gameObject.SetActive(true);
        // Mostrar "3"
        contador.text = "3";
        yield return new WaitForSeconds(1f);

        // Mostrar "2"
        contador.text = "2";
        yield return new WaitForSeconds(1f);

        // Mostrar "1"
        contador.text = "1";
        yield return new WaitForSeconds(1f);

        contador.text = "¡GO!";
        foreach (GameObject animal in animalesArray)
        {
            animal.SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        scriptCamera.ResetCameraToPlayer();
        playerController.MovementEnabled = true;
        contador.gameObject.SetActive(false);
        animalesLiberados = true;
        gameObject.SetActive(false);//desactivo la caja ostias
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            avisoProximidad.SetActive(true);
            señal.SetActive(false);
            isPlayer = true;
            Debug.Log("abremeee!!!!");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            avisoProximidad.SetActive(false);
            señal.SetActive(true);
            isPlayer = false;
            Debug.Log("NoooooMECIERRREEEsssss!!!!");
        }
    }
}
