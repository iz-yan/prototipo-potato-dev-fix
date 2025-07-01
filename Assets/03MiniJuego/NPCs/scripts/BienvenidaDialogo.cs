using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using TMPro;
using System.Collections;

public class BienvenidaDialogo : MonoBehaviour
{
    private bool isPlayer;
    private bool didDialogueStart;
    private int indexLine = 0;
    [SerializeField] private TMP_Text letrasDelCartel;
    [SerializeField,TextArea(6,4)] private string[] textoCartel;
    [SerializeField] private behaviourPlayer playerController;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text continuarAviso;
    private string continuar = "Presione Espacio >>";


    // Update is called once per frame
    void Update()
    {
        if (isPlayer )
        {
            if (!didDialogueStart)
            {
                MostrarTextCartel();
            }
            else if (letrasDelCartel.text == textoCartel[indexLine] && Input.GetKeyDown(KeyCode.Space))
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
        continuarAviso.text=string.Empty;
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
                playerController.MovementEnabled = true;
                didDialogueStart = false;
                panel.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }
}
