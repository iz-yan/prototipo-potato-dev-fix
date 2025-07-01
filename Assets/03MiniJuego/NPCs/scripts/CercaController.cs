using UnityEngine;
using UnityEngine.SceneManagement;

public class CercaController : MonoBehaviour
{
    [SerializeField] private CercaVacas cercaVacas;
    [SerializeField] private CercaPollos cercaPollos;
    [SerializeField] private CercaChanchitos cercaChanchos;
    [SerializeField] private GameObject npc;
    [SerializeField] private ScriptCamera scriptCamera;
    [SerializeField] private Transform cameraTargetPosition; // Asigna esto en el inspector
    private BehaviourCartel cartel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cartel= FindAnyObjectByType<BehaviourCartel>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cartel!=null&& cartel.AnimalesLiberados)
        {
            Debug.Log("ya estamos por ver si fueron o no agarrados todos los animales");
            IrACharlar(cercaVacas.CercaLlena(), cercaChanchos.CercaLlena(), cercaPollos.CercaLlena());
            scriptCamera.SetCameraTargetPosition(cameraTargetPosition.position);
        }
    }
    public void IrACharlar(bool cerca3,bool cerca2,bool cerca1)
    {
        if(cerca3 && cerca2 && cerca1)
        {
            cartel.AnimalesLiberados = false;
            npc.SetActive(true);
            scriptCamera.ResetCameraToPlayer();
            cartel.AnimalesLiberados = false;

        }
    }
}
