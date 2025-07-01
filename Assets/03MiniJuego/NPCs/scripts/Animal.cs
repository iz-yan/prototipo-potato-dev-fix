using TMPro;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    [Header("Configuración Base")]
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float changeDirectionTime = 3f;
    [SerializeField] protected int puntaje = 50;
    [Header("Visualización Número")]

    protected Vector2 randomDirection;
    protected float timer;
    protected bool isCaught = false;
    private bool fueAtrapado = false;
    protected Rigidbody2D rb;
    [Header("sonidos")]
    [SerializeField] protected AudioClip sonidoAtrapado;
    [SerializeField] protected float volumenSonido = 1f;
    protected AudioSource audioSource;



    public int Puntaje => puntaje;
    public bool IsCaught { get => isCaught; set => isCaught = value; }
    public bool FueAtrapado { get => fueAtrapado; set => fueAtrapado = value; }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!TryGetComponent<AudioSource>(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1f; // Para sonido 3D (opcional)
        }
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
        if (randomDirection.x < 0f) transform.localScale = new Vector2(1, 1);
        else transform.localScale = new Vector2(-1,1);
            timer = changeDirectionTime;
    }

    protected virtual void MoveAnimal()
    {
        rb.linearVelocity = randomDirection * moveSpeed;
    }

    public virtual void Catch(Transform manosJugador)
    {
        IsCaught = true;
        fueAtrapado=true;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(manosJugador);
        transform.localPosition = Vector3.zero;
        GetComponent<Collider2D>().enabled = false;
        if (sonidoAtrapado != null)
        {
            audioSource.PlayOneShot(sonidoAtrapado, volumenSonido);
        }
    }

    public virtual void Release()
    {
        isCaught = false;
        transform.SetParent(null);
        GetComponent<Collider2D>().enabled = true;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.localPosition = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Rebote con límites
        if (collision.gameObject.layer == LayerMask.NameToLayer("AnimalBoundary"))
        {
            Vector2 normal = collision.contacts[0].normal;
            randomDirection = Vector2.Reflect(randomDirection, normal).normalized;
            rb.linearVelocity = randomDirection * moveSpeed;
        }
        else // Cambio de dirección con otros objetos
        {
            PickNewDirection();
        }
    }

}
