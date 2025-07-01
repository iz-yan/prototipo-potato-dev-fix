using UnityEngine;
using TMPro;

public class Cartelito : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public float duracionMensaje = 3f;

    private float borrarMensaje = 0f;

    private void Update()
    {
        if (texto == null) return;

        if (Time.time >= borrarMensaje && texto.text != "")
        {
            texto.text = "";
        }
    }

    public void Mostrar(string mensaje)
    {
        texto.text = mensaje;
        borrarMensaje = Time.time + duracionMensaje;
    }
}
