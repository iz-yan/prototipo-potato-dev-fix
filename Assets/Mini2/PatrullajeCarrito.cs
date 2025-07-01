using UnityEngine;

public class PatrullajeCarrito : MonoBehaviour
{
    public Transform[] posiciones;
    public float velocidad = 2f;
    private int indiceActual = 0;

    private void Update()
    {
        if (posiciones.Length == 0) return;

        Transform destino = posiciones[indiceActual];
        Vector3 direccion = destino.position - transform.position;

        //Mover hacia el destino
        transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidad * Time.deltaTime);

        if (direccion.x > 0.01f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); // mirando a la derecha
        }
        else if (direccion.x < -0.01f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f); // mirando a la izquierda
        }

        //Avanzar al siguiente punto cuando llega
        if (Vector3.Distance(transform.position, destino.position) < 0.1f)
        {
            indiceActual = (indiceActual + 1) % posiciones.Length;
        }
    }
}
