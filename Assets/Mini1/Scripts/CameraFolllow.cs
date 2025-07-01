using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform jugador;

    public Vector2 minLimite;
    public Vector2 maxLimite;

    //public float suavizado = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        if (jugador == null) return;

        //Vector3 posicionDeseada = jugador.position + offset;
        //Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, suavizado);

        Vector3 posicionFinal = jugador.position + offset;

        float camaraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float camaraHalfHeight = Camera.main.orthographicSize;

        float limiteX = Mathf.Clamp(posicionFinal.x, minLimite.x + camaraHalfWidth, maxLimite.x - camaraHalfWidth);
        float limiteY = Mathf.Clamp(posicionFinal.y, minLimite.y + camaraHalfHeight, maxLimite.y - camaraHalfHeight);

        transform.position = new Vector3(limiteX, limiteY, transform.position.z);
    }
}
