using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    [Header("Configuración Base")]
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float changeDirectionTime = 3f;
    [SerializeField] protected int valorAnimal;
    [SerializeField] protected int puntaje = 50;

    protected Vector2 randomDirection;
    protected float timer;
    protected bool isCaught = false;
    protected Rigidbody2D rb;

    public int ValorAnimal => valorAnimal;
    public int Puntaje => puntaje;
    public bool IsCaught { get => isCaught; set => isCaught = value; }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PickNewDirection();
    }

    protected virtual void Update()
    {
        if (IsCaught) return;

        timer -= Time.deltaTime;
        if (timer <= 0) PickNewDirection();

        MoveAnimal();
    }

    protected void PickNewDirection()
    {
        randomDirection = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
        timer = changeDirectionTime;
    }

    protected virtual void MoveAnimal()
    {
        rb.linearVelocity = randomDirection * moveSpeed;
    }

    public virtual void Catch(Transform manosJugador)
    {
        IsCaught = true;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(manosJugador);
        transform.localPosition = Vector3.zero;
        GetComponent<Collider2D>().enabled = false;
    }

    public virtual void Release()
    {
        transform.SetParent(null);
        GetComponent<Collider2D>().enabled = true;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.localPosition = transform.position;
    }
    // En la clase Animal
    public void GenerarValorAleatorio(int min = 1, int max = 10)
    {
        valorAnimal = Random.Range(min, max + 1);
    }
}
