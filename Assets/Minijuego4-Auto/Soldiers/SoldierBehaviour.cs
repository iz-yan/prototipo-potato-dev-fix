using UnityEngine;

public class SoldierBehaviour : MonoBehaviour
{
    [Header("Referencias de Objetos")]
    [SerializeField] private GameObject general;
    [SerializeField] private GameObject helmet;
    [SerializeField] private GameObject ordinary;

    [Header("Configuración de Animación")]
    [SerializeField] private int animationState = 2; // 2 para caminar por defecto

    private Animator animGeneral;
    private Animator animHelmet;
    private Animator animOrdinary;

    void Start()
    {
        // Obtener los componentes Animator al inicio
        InitializeAnimators();

        // Activar la animación de caminar automáticamente
        SetAnimationState(animationState);
    }

    void InitializeAnimators()
    {
        if (general != null) animGeneral = general.GetComponent<Animator>();
        if (helmet != null) animHelmet = helmet.GetComponent<Animator>();
        if (ordinary != null) animOrdinary = ordinary.GetComponent<Animator>();
    }

    void Update()
    {
        // Aquí puedes poner lógica de cambio de animaciones si lo deseas
    }

    public void SetAnimationState(int newState)
    {
        animationState = newState;

        if (animGeneral != null) animGeneral.SetInteger("Animate", animationState);
        if (animHelmet != null) animHelmet.SetInteger("Animate", animationState);
        if (animOrdinary != null) animOrdinary.SetInteger("Animate", animationState);
    }

    public void SetIdleState()
    {
        SetAnimationState(0); // 0 para idle
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("auto"))
        {
            Debug.Log("el auto colisiono con el soldado!");
            // Detener el movimiento del auto
            Rigidbody rbAuto = collision.gameObject.GetComponent<Rigidbody>();
            if (rbAuto != null)
            {
                // Usar la propiedad moderna linearVelocity
                rbAuto.linearVelocity = Vector3.zero;

                // Si también angularVelocity está obsoleta, reemplazarla por angularVelocity (si existe una alternativa)
                rbAuto.angularVelocity = Vector3.zero;
            }
        }
    }
}
