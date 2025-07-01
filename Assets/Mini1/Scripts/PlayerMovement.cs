using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector2 movement;
    private Rigidbody2D rb;

    private float minX = -19.2f + 0.35f;
    private float maxX = 19.2f - 0.35f;
    private float minY = -10.8f + 0.35f;
    private float maxY = 10.8f - 0.35f;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        float velocidad = movement.magnitude;
        animator.SetFloat("Velocidad", velocidad);
        animator.SetBool("Izquierda", movement.x < 0);
    }

    private void FixedUpdate()
    {
        Vector2 newPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        rb.MovePosition(newPos);
    }
}
