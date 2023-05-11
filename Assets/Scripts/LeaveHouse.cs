using System.Collections;
using UnityEngine;

public class LeaveHouse : MonoBehaviour
{
    [SerializeField] private GameObject spawnPointOutsideHouse;
    [SerializeField] private float outDoorTime = 0.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CapsuleCollider2D capsuleCollider = collision.GetComponent<CapsuleCollider2D>();
        if (capsuleCollider == null)
        {
            // Ignore collisions with other types of colliders
            return;
        }

        StartCoroutine(ExitHouse(collision.gameObject));
    }

    private IEnumerator ExitHouse(GameObject collision)
    {
        yield return new WaitForSeconds(outDoorTime);
        collision.gameObject.transform.position = spawnPointOutsideHouse.transform.position;

    }
}
