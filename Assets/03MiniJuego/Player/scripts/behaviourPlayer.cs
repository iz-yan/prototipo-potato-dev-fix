using UnityEngine;

public class behaviourPlayer : MonoBehaviour
{
    [SerializeField]private float velocidad;
    [SerializeField] private Vector2 direccion;
    [SerializeField] private float catchRange = 1f;
    [SerializeField] private LayerMask chanchitoLayer;
    private bool isCarryingChanchito = false;
    private Rigidbody2D Rigidbody2D;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direccion=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;
        if (direccion.x<0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if  (direccion.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space))//logica para agarrar al chanchito
        {
            TryCatchChanchito();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            TryReleaseChanchito();
        }
    }
    private void FixedUpdate()
    {
        Rigidbody2D.MovePosition(Rigidbody2D.position+direccion*velocidad*Time.fixedDeltaTime);
    }

    //para agarrar a los chanchos

    private void TryCatchChanchito()
    {
        if (isCarryingChanchito) return;
        Collider2D[] nearbyChanchitos = Physics2D.OverlapCircleAll(
            transform.position,
            catchRange,
            chanchitoLayer
        );
        if (nearbyChanchitos.Length == 0) return;
        isCarryingChanchito = true;
        foreach (Collider2D chanchito in nearbyChanchitos)
        {
            chanchito.GetComponent<BehavioyrChanchito>().Catch(transform.Find("Manos"));
            break;
        }
    }

    private void TryReleaseChanchito()
    {
        Transform manos = transform.Find("Manos");
        if (manos == null || manos.childCount == 0) return;

        BehavioyrChanchito chanchito = manos.GetChild(0).GetComponent<BehavioyrChanchito>();
        if (chanchito != null)
        {
            chanchito.Release();
            isCarryingChanchito = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, catchRange);
    }


}
