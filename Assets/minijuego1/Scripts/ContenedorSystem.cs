using UnityEngine;

public class ContenedorSystem : MonoBehaviour
{
    [Header("Referencias de Contenedores")]
    [SerializeField] private GameObject contenedorOrganico;
    [SerializeField] private GameObject contenedorVidrio;

    [Header("Elementos para Tirar")]
    [SerializeField] private GameObject elementosOrganico;
    [SerializeField] private GameObject elementosVidrio;

    [Header("Configuración Inicial")]
    [SerializeField] private bool iniciarConOrganico = true;

    private void Start()
    {
        // Configuración inicial
        SetContenedorOrganicoActive(iniciarConOrganico);
        SetContenedorVidrioActive(!iniciarConOrganico);
        SetElementosOrganicoActive(iniciarConOrganico);
        SetElementosVidrioActive(!iniciarConOrganico);
    }

    public void CambiarAVidrio()
    {
        SetContenedorOrganicoActive(false);
        SetContenedorVidrioActive(true);
        SetElementosOrganicoActive(false);
        SetElementosVidrioActive(true);

        Debug.Log("Sistema cambiado a modo VIDRIO");
    }

    public void CambiarAOrganico()
    {
        SetContenedorOrganicoActive(true);
        SetContenedorVidrioActive(false);
        SetElementosOrganicoActive(true);
        SetElementosVidrioActive(false);

        Debug.Log("Sistema cambiado a modo ORGÁNICO");
    }

    private void SetContenedorOrganicoActive(bool active)
    {
        if (contenedorOrganico != null)
        {
            contenedorOrganico.SetActive(active);
            ToggleCollider(contenedorOrganico, active);
        }
    }

    private void SetContenedorVidrioActive(bool active)
    {
        if (contenedorVidrio != null)
        {
            contenedorVidrio.SetActive(active);
            ToggleCollider(contenedorVidrio, active);
        }
    }

    private void SetElementosOrganicoActive(bool active)
    {
        if (elementosOrganico != null)
        {
            elementosOrganico.SetActive(active);
        }
    }

    private void SetElementosVidrioActive(bool active)
    {
        if (elementosVidrio != null)
        {
            elementosVidrio.SetActive(active);
        }
    }

    private void ToggleCollider(GameObject obj, bool active)
    {
        var collider = obj.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = active;
    }
}