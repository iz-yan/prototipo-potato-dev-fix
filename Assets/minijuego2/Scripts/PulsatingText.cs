using UnityEngine;
using TMPro; // Aseg�rate de tener TextMeshPro en tu proyecto

public class PulsatingText : MonoBehaviour
{
    public float pulseSpeed = 2f;
    public float scaleAmount = 0.1f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * scaleAmount;
        transform.localScale = originalScale * scale;
    }
}
