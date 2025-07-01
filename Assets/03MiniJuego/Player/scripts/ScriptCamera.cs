using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    [SerializeField ]private GameObject player;
    private Vector3 offsetZ = new Vector3(0, 0, -10);
    private bool isFollowingPlayer = true;
    private Vector3 targetPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isFollowingPlayer)
        {
            MoverCamara(player.transform.position + offsetZ);
        }
        else
        {
            MoverCamara(targetPosition);
        }
    }

    public void MoverCamara(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    // Llama a este método cuando empiece el contador
    public void SetCameraTargetPosition(Vector3 position)
    {
        targetPosition = position + offsetZ;
        isFollowingPlayer = false;
    }

    // Llama a este método cuando termine el contador
    public void ResetCameraToPlayer()
    {
        isFollowingPlayer = true;
    }
}