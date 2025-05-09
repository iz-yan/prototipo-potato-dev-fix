using UnityEngine;

[RequireComponent(typeof(DragAndDropManager))]
public class EfectoObjetos : MonoBehaviour
{
    public float moveDistance = 0.4f;
    public float bounceHeight = 0.6f;
    public float bounceSpeed = 2f;
    public float tiltAngle = 25f;

    private Vector3 startPosition;
    private DragAndDropManager dragManager;

    void Start()
    {
        startPosition = transform.position;
        dragManager = GetComponent<DragAndDropManager>();
    }

    void Update()
    {
        if (dragManager != null && dragManager.IsDragging())
            return;

        AnimateBounceM();
    }

    private void AnimateBounceM()
    {
        float time = Time.time * bounceSpeed;

        float horizontal = Mathf.Sin(time * 2f) * moveDistance;
        float vertical = Mathf.Abs(Mathf.Sin(time)) * bounceHeight;
        float tilt = Mathf.Sin(time * 2f) * tiltAngle;

        transform.position = startPosition + new Vector3(horizontal, vertical, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, -tilt);
    }
}
