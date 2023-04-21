using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }
}
