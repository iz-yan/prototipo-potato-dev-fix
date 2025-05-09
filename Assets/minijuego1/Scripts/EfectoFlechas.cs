using UnityEngine;

public class EfectoFlechas : MonoBehaviour
{
    public float bounceHeight = 0.5f;
    public float bounceSpeed = 2f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float verticalMovement = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;

        transform.position = originalPosition + new Vector3(0f, verticalMovement, 0f);
    }
}
