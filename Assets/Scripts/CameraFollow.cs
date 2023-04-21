using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Movement")]
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 offset;
    private Vector3 desiredPos;


    void Update()
    {
        desiredPos = new Vector3((player.position.x + offset.x), transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed);
    }
}

