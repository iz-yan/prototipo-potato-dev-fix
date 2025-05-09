using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EfectoHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject imagenHover;
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private Button boton;

    private void Awake()
    {
        boton = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (boton != null && !boton.interactable)
            return;

        if (imagenHover != null)
            imagenHover.SetActive(true);

        if (audioSource != null && hoverSound != null)
            audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (boton != null && !boton.interactable)
            return;

        if (imagenHover != null)
            imagenHover.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (boton != null && !boton.interactable)
            return;

        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }
}
