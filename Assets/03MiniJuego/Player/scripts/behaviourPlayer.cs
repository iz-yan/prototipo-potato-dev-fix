using UnityEngine;

public class behaviourPlayer : MonoBehaviour
{
    [SerializeField]private float velocidad;
    [SerializeField] private Vector2 direccion;
    [SerializeField] private float catchRange = 1f;
    [SerializeField] private LayerMask animalLayer;
    [SerializeField] private Animator Animation;
    private bool isRunning=false;
    private bool isCarryingAnimal = false;
    private Rigidbody2D Rigidbody2D;

    public bool IsCarryingAnimal { get => isCarryingAnimal; set => isCarryingAnimal = value; }
    public float Velocidad { get => velocidad; set => velocidad = value; }

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direccion=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;
        if(direccion.x!=0f) isRunning=true;//sirven solo para la animacion
        else isRunning=false;
        if (direccion.x < 0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (direccion.x > 0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        Animation.SetBool("IsRun", isRunning);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryCatchAnimal(); 
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            TryReleaseAnimal(); 
        }
    }
    private void FixedUpdate()
    {
        Rigidbody2D.MovePosition(Rigidbody2D.position + direccion*Time.deltaTime*velocidad);
    }

    //para agarrar a los chanchos

    private void TryCatchAnimal()
    {
        if (IsCarryingAnimal) return;

        Collider2D[] nearbyAnimals = Physics2D.OverlapCircleAll(
            transform.position,
            catchRange,
            animalLayer.value
        );
        if (nearbyAnimals.Length == 0) return;

        IsCarryingAnimal = true;
        foreach (Collider2D animalCollider in nearbyAnimals)
        {
            Animal animal = animalCollider.GetComponent<Animal>();
            if (animal != null && !animal.IsCaught)
            {
                animal.Catch(transform.Find("Manos"));
                break;
            }
        }
    }

    private void TryReleaseAnimal()
    {
        Transform manos = transform.Find("Manos");
        if (manos == null || manos.childCount == 0) return;

        Animal animal = manos.GetChild(0).GetComponent<Animal>();
        if (animal != null)
        {
            animal.Release();
            IsCarryingAnimal = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, catchRange);
    }
}
