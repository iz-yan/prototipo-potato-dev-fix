using UnityEngine;

public class BehavioyrChanchito : MonoBehaviour
{
 
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float changeDirectionTime = 3f;
    private Vector2 randomDirection;
    private float timer;
    private bool isCaught = false;
    private int puntajeChancho=50;
    private Rigidbody2D rb;

    public int PuntajeChancho { get => puntajeChancho; set => puntajeChancho = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PickNewDirection();
    }

    private void Update()
    {
        if (isCaught) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            PickNewDirection();
        }

        MoveChanchito();
    }

    private void PickNewDirection()
    {
        randomDirection = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        timer = changeDirectionTime;
    }

    private void MoveChanchito()
    {
        rb.linearVelocity = randomDirection * moveSpeed;
    }

    public void Catch(Transform manosJugador)
    {
        isCaught = true;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType=RigidbodyType2D.Kinematic;
        transform.SetParent(manosJugador);
        transform.localPosition = Vector3.zero;
        GetComponent<Collider2D>().enabled = false;
    }

    public void Release()
    {
        isCaught = true;
        transform.SetParent(null);
        GetComponent<Collider2D>().enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity=Vector2.zero;
    }
}
