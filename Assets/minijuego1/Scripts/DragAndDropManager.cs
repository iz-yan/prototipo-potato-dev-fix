using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class DragAndDropManager : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private Collider2D myCollider;

    [Header("Configuración")]
    [SerializeField] private LayerMask containerLayerMask;
    [SerializeField] private AudioClip correctDropSound;
    private AudioSource audioSource;


    private void Start()
    {
        mainCamera = Camera.main;
        myCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (mainCamera == null)
        {
            Debug.LogError("No se encontró la cámara principal.", this);
        }
    }

    private void OnMouseDown()
    {
        if (!myCollider.enabled) return;

        isDragging = true;
        offset = transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
        myCollider.enabled = false;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;
        myCollider.enabled = true;

        CheckDropOnContainer();
    }

    private void CheckDropOnContainer()
    {
        Vector2 rayOrigin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, 0.1f, containerLayerMask);

        if (hit.collider == null) return;

        if (hit.collider.CompareTag("contenedor-organico"))
        {
            HandleOrganicContainerDrop(hit.collider.gameObject);
        }
        else if (hit.collider.CompareTag("contenedor-vidrio"))
        {
            HandleGlassContainerDrop(hit.collider.gameObject);
        }
        else if (hit.collider.CompareTag("contenedor-papel"))
        {
            HandlePaperContainerDrop(hit.collider.gameObject);
        }
        else if (hit.collider.CompareTag("contenedor-metal"))
        {
            HandleMetalContainerDrop(hit.collider.gameObject);
        }

        Destroy(gameObject);
    }

    private void HandleOrganicContainerDrop(GameObject container)
    {
        if (gameObject.CompareTag("elemento-comida"))
        {
            // 1. Primero reproducir el sonido
            if (correctDropSound != null)
            {
                AudioSource.PlayClipAtPoint(correctDropSound, Camera.main.transform.position, 0.3f);
            }

            ScoreManager.Instance?.AddScore(1);
            SwitchContainers();
            ProcessItems();

            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("item"))
        {
            LifeManager.Instance?.LoseLife(1);
            if (correctDropSound != null)
            {
                AudioSource.PlayClipAtPoint(correctDropSound, Camera.main.transform.position, 0.3f);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator PlaySoundAndDestroy()
    {
        audioSource.PlayOneShot(correctDropSound);

        // Esperar hasta que termine el sonido
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    private void HandleGlassContainerDrop(GameObject container)
    {
        if (gameObject.CompareTag("item-vidrio"))
        {
            if (correctDropSound != null)
            {
                AudioSource.PlayClipAtPoint(correctDropSound, Camera.main.transform.position, 0.3f);
            }
            RemoveGlassContainerAndItems();
            ScoreManager.Instance?.AddScore(1);
            ActivatePaperContainerAndItems();
        }
        else if (gameObject.CompareTag("item-2"))
        {
            if (correctDropSound != null)
            {
                AudioSource.PlayClipAtPoint(correctDropSound, Camera.main.transform.position, 0.3f);
            }
            LifeManager.Instance?.LoseLife(1);
        }
    }

    private void HandlePaperContainerDrop(GameObject container)
    {
        if (gameObject.CompareTag("item-papel"))
        {
            if (correctDropSound != null)
            {
                AudioSource.PlayClipAtPoint(correctDropSound, Camera.main.transform.position, 0.3f);
            }
            ScoreManager.Instance?.AddScore(1);
            RemovePaperContainerAndItems();
            ActivateMetalContainerAndItems();
        }
        else if (gameObject.CompareTag("item-3"))
        {
            if (correctDropSound != null)
            {
                AudioSource.PlayClipAtPoint(correctDropSound, Camera.main.transform.position, 0.3f);
            }
            LifeManager.Instance?.LoseLife(1);
        }
    }

    private void HandleMetalContainerDrop(GameObject container)
    {
        if (gameObject.CompareTag("item-metal"))
        {
            if (correctDropSound != null)
            {
                AudioSource.PlayClipAtPoint(correctDropSound, Camera.main.transform.position, 0.3f);
            }
            ScoreManager.Instance?.AddScore(1);
            RemoveMetalContainerAndItems();
        }
        else if (gameObject.CompareTag("item-4"))
        {
            if (correctDropSound != null)
            {
                AudioSource.PlayClipAtPoint(correctDropSound, Camera.main.transform.position, 0.3f);
            }
            LifeManager.Instance?.LoseLife(1);
        }
    }

    private void ActivateMetalContainerAndItems()
    {
        GameObject[] metalContainers = GameObject.FindGameObjectsWithTag("contenedor-metal");
        foreach (GameObject container in metalContainers)
        {
            container.SetActive(true);

            SpriteRenderer spriteRenderer = container.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) spriteRenderer.enabled = true;

            Collider2D collider = container.GetComponent<Collider2D>();
            if (collider != null) collider.enabled = true;
        }

        GameObject[] metalItems = GameObject.FindGameObjectsWithTag("item-metal");
        foreach (GameObject item in metalItems)
        {
            item.SetActive(true);

            SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = true;

            Collider2D col = item.GetComponent<Collider2D>();
            if (col != null) col.enabled = true;
        }

        GameObject[] items4 = GameObject.FindGameObjectsWithTag("item-4");
        foreach (GameObject item in items4)
        {
            item.SetActive(true);

            SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = true;

            Collider2D col = item.GetComponent<Collider2D>();
            if (col != null) col.enabled = true;
        }
    }

    private void RemoveGlassContainerAndItems()
    {
        GameObject[] glassContainers = GameObject.FindGameObjectsWithTag("contenedor-vidrio");
        foreach (GameObject container in glassContainers)
        {
            Destroy(container);
        }

        GameObject[] items2 = GameObject.FindGameObjectsWithTag("item-2");
        foreach (GameObject item in items2)
        {
            Destroy(item);
        }
    }

    private void RemovePaperContainerAndItems()
    {
        GameObject[] paperContainers = GameObject.FindGameObjectsWithTag("contenedor-papel");
        foreach (GameObject container in paperContainers)
        {
            Destroy(container);
        }

        GameObject[] items3 = GameObject.FindGameObjectsWithTag("item-3");
        foreach (GameObject item in items3)
        {
            Destroy(item);
        }
    }

    private void RemoveMetalContainerAndItems()
    {
        GameObject[] metalContainers = GameObject.FindGameObjectsWithTag("contenedor-metal");
        foreach (GameObject container in metalContainers)
        {
            Destroy(container);
        }

        GameObject[] items4 = GameObject.FindGameObjectsWithTag("item-4");
        foreach (GameObject item in items4)
        {
            Destroy(item);
        }

        GameObject[] metalItems = GameObject.FindGameObjectsWithTag("item-metal");
        foreach (GameObject item in metalItems)
        {
            Destroy(item);
        }
    }

    private void ActivatePaperContainerAndItems()
    {
        GameObject[] paperContainers = GameObject.FindGameObjectsWithTag("contenedor-papel");
        foreach (GameObject container in paperContainers)
        {
            container.SetActive(true);

            SpriteRenderer spriteRenderer = container.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) spriteRenderer.enabled = true;

            Collider2D collider = container.GetComponent<Collider2D>();
            if (collider != null) collider.enabled = true;
        }

        GameObject[] items3 = GameObject.FindGameObjectsWithTag("item-3");
        foreach (GameObject item in items3)
        {
            item.SetActive(true);

            SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = true;

            Collider2D col = item.GetComponent<Collider2D>();
            if (col != null) col.enabled = true;
        }

        GameObject[] paperItems = GameObject.FindGameObjectsWithTag("item-papel");
        foreach (GameObject item in paperItems)
        {
            item.SetActive(true);

            SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = true;

            Collider2D col = item.GetComponent<Collider2D>();
            if (col != null) col.enabled = true;
        }
    }

    private void ProcessItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("item");
        foreach (GameObject item in items)
        {
            Destroy(item);
        }

        SetItemsActiveState("item-vidrio", true);
        SetItemsActiveState("item-2", true);
    }

    private void SetItemsActiveState(string tag, bool activeState)
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject item in items)
        {
            try
            {
                item.SetActive(activeState);

                Renderer renderer = item.GetComponent<Renderer>();
                if (renderer != null) renderer.enabled = activeState;

                Collider2D collider = item.GetComponent<Collider2D>();
                if (collider != null) collider.enabled = activeState;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error al procesar objeto {item.name}: {e.Message}");
            }
        }
    }

    private void SwitchContainers()
    {
        GameObject organicContainer = GameObject.FindGameObjectWithTag("contenedor-organico");
        if (organicContainer != null)
        {
            organicContainer.SetActive(false);
            organicContainer.GetComponent<Collider2D>().enabled = false;
        }

        ActivateGlassContainer();
    }

    private void ActivateGlassContainer()
    {
        GameObject glassContainer = GameObject.FindGameObjectWithTag("contenedor-vidrio");

        if (glassContainer == null)
        {
            Debug.LogError("Contenedor de vidrio no encontrado");
            return;
        }

        glassContainer.SetActive(true);

        Renderer renderer = glassContainer.GetComponent<Renderer>();
        if (renderer != null) renderer.enabled = true;

        Collider2D collider = glassContainer.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = true;
    }

    public bool IsDragging()
    {
        return isDragging;
    }
}