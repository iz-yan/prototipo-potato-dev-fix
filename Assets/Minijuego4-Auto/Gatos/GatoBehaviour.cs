using System.Collections;
using UnityEngine;

public class GatoBehaviour : MonoBehaviour
{
    public bool IsWalking = false;
    public GameObject puntoInicial;
    public GameObject puntoFinal;
    public float velocidad = 2f;

    private Vector3 destinoActual;
    private bool yendoAPuntoFinal = true;
    private bool esperando = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (puntoInicial != null && puntoFinal != null)
        {
            transform.position = puntoInicial.transform.position;
            destinoActual = puntoFinal.transform.position;
            IsWalking = true;
            ActualizarAnimacion();
        }
    }

    void Update()
    {
        if (!IsWalking || esperando)
            return;

        MoverGato();
    }

    void MoverGato()
    {
        transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, destinoActual) < 0.01f)
        {
            StartCoroutine(EsperarEnPunto());
        }
    }

    IEnumerator EsperarEnPunto()
    {
        IsWalking = false;
        esperando = true;
        ActualizarAnimacion();

        yield return new WaitForSeconds(3f); // quieto 3 segundo (Idle)

        yendoAPuntoFinal = !yendoAPuntoFinal;

        if (yendoAPuntoFinal)
        {
            destinoActual = puntoFinal.transform.position;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            destinoActual = puntoInicial.transform.position;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        IsWalking = true;
        esperando = false;
        ActualizarAnimacion();
    }

    void ActualizarAnimacion()
    {
        if (animator != null)
        {
            animator.SetBool("IsWalking", IsWalking && !esperando);
        }
    }

    void OnDrawGizmos()
    {
        if (puntoInicial != null && puntoFinal != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(puntoInicial.transform.position, 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(puntoFinal.transform.position, 0.2f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(puntoInicial.transform.position, puntoFinal.transform.position);
        }
    }
}
