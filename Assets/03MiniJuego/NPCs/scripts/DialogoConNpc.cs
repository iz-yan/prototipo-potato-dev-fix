using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class DialogoConNpc : MonoBehaviour
{
    //Variables de Parrafos
    [Header("Elementos De Dialogo")]
    [SerializeField] private GameObject aviso;
    [SerializeField, TextArea(6,4)] private string[] parrafosDeDialogo;
    [SerializeField, TextArea(6, 4)] private string[] parrafosDeDialogoFinal;
    [SerializeField] GameObject panelParrafos;
    [SerializeField] TMP_Text textoTMP;
    private string[] dialogoActual;
    private int indexParrafo=0;

    [Header("Imagenes y Colores")]
    [SerializeField] private Image playerImage;
    [SerializeField] private Image npcImage;
    [SerializeField] private Color colorActivo= Color.white;
    [SerializeField] private Color colorInactivo=new Color(0.7f, 0.7f, 0.7f, 0.5f);
    
    //variables con el PJ
    private bool isPlayer=false;
    private bool isDialogueStart=false;
    private bool npcHabla;
    private bool isEnd=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (isPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            dialogoActual = isEnd ? parrafosDeDialogoFinal : parrafosDeDialogo;
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
        if (indexParrafo < parrafos.Length - 1)
        {
            indexParrafo++;
            StartCoroutine(MostrarLineas(parrafos));
            npcHabla = (indexParrafo % 2 == 0);//en indices pares el npc toma color y en los impares el player
            CambiarEnfoque(npcHabla);
        }
        else
        {
            panelParrafos.SetActive(false);
            isDialogueStart = false;
            indexParrafo = 0;
            if (!isEnd)
            {
                
                isPlayer=false;
                aviso.SetActive(false);
                //AchievementManager.instance.LogroCompletado("nivel2");
            }
            else
            {
                Debug.Log("ADiosssssSSS");
                SceneManager.LoadScene("Victoria");
            }
                isEnd = true;
            
        }
    }

    public void CambiarEnfoque(bool estaNpcHablando)
    {
        npcImage.color=estaNpcHablando? colorActivo:colorInactivo;
        playerImage.color=estaNpcHablando? colorInactivo:colorActivo;
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
            Debug.Log("Estoy");
            isPlayer = true;
        }
    }
}
