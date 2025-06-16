using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    private GameObject player;
    private Vector3 cameraPosition;
    private Vector3 offsetZ=new Vector3(0,0,-10);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = player.transform.position+offsetZ;
        MoverCamara(cameraPosition);
    }

    public void MoverCamara(Vector3 player)
    {
        transform.position = player;
    }
}
