using System.Collections;
using UnityEngine;

public class LeaveHouse : MonoBehaviour
{
    [SerializeField] private GameObject spawnPointOutsideHouse;

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
        yield return new WaitForSeconds(1f);
        collision.transform.position = spawnPointOutsideHouse.transform.position;
    }
}
