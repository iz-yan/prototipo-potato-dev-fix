using UnityEngine;

public class StopCarDialogue : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string autoTag = "auto";
    [SerializeField] private string panelTag = "Chanchito";
    [Header("Configuración de Audio")]
    [SerializeField] private AudioClip collisionSound; // Arrastra tu archivo MP3 aquí
    [SerializeField] private float volume = 1f;

    private GameObject panelAlto;
    private bool panelActivado = false;
    private AudioSource audioSource;

    void Start()
    {
        Debug.Log("Testing porpose---------");

        // Buscar el panel al inicio
        panelAlto = GameObject.FindGameObjectWithTag(panelTag);

        // Añadir componente AudioSource si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configurar AudioSource
        audioSource.playOnAwake = false;
        audioSource.volume = volume;

        // Asegurarse que el panel está desactivado al inicio
        if (panelAlto != null)
        {
            panelAlto.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"No se encontró ningún objeto con el tag {panelTag}");
        }
    }

    void Update()
    {
        if (panelActivado && Input.GetKeyDown(KeyCode.Return))
        {
            DesactivarPanel();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(autoTag))
        {
            Debug.Log("Colisiono auto y objeto invisible---------");
            ActivarPanel();
            PlayCollisionSound();
        }
    }

    void PlayCollisionSound()
    {
        if (collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }
        else
        {
            Debug.LogWarning("No hay sonido asignado para la colisión");
        }
    }

    void ActivarPanel()
    {
        if (panelAlto != null)
        {
            panelAlto.SetActive(true);
            panelActivado = true;
        }
    }

    void DesactivarPanel()
    {
        if (panelAlto != null)
        {
            panelAlto.SetActive(false);
            panelActivado = false;
            Destroy(gameObject); // Destruye el GameObject que tiene este script
        }
    }
}