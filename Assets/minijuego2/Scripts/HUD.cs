using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI puntos;
    public TextMeshProUGUI vidas;
    public TextMeshProUGUI mensaje;
    public AudioSource peligro;

    private int vidasAnteriores;
    private int puntosAnteriores;

    void Start()
    {
        vidasAnteriores = GameManager.Instance.Vidas;
        puntosAnteriores = GameManager.Instance.PuntosTotales;
    }

    void Update()
    {
        
        puntos.text = "Puntos: " + GameManager.Instance.PuntosTotales.ToString();
        vidas.text = "Vidas: " + GameManager.Instance.Vidas.ToString();

        
        int vidasActuales = GameManager.Instance.Vidas;
        int puntosActuales = GameManager.Instance.PuntosTotales;

        vidasAnteriores = vidasActuales;
        puntosAnteriores = puntosActuales;
    }

}
