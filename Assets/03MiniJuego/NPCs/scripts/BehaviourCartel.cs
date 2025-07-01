using System.Collections;
using TMPro;
using UnityEngine;

public class BehaviourCartel : MonoBehaviour
{
    [SerializeField,TextArea(3,3)] private string[] textoCartel;
    [SerializeField] private TMP_Text letrasDelCartel;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject[] chanchitosArray;
    private bool isPlayer;
    private bool didDialogueStart=false;
    private int indexLine;

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
            else
            {
                StopAllCoroutines();
                letrasDelCartel.text = textoCartel[indexLine];
            }
        }
    }

    public void MostrarTextCartel()
    {
        didDialogueStart = true;
        panel.SetActive(true);
        indexLine = 0;
        StartCoroutine(MostrarCadaLetra());
    }

    private IEnumerator MostrarCadaLetra()
    {
        letrasDelCartel.text = string.Empty;
        foreach (char c in textoCartel[indexLine])
        {
            letrasDelCartel.text += c;
            yield return new WaitForSeconds(0.05f);
        }
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
                didDialogueStart = false;
                panel.SetActive(false);
                gameObject.SetActive(false);//desactivo la caja ostias
                foreach (GameObject chanchos in chanchitosArray)
                {
                    chanchos.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
            Debug.Log("abremeee!!!!");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
            Debug.Log("NoooooMECIERRREEEsssss!!!!");
        }
    }
}
