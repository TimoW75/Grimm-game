
using System.Collections;
using UnityEngine;

public class LeaveHouse : MonoBehaviour
{
    [SerializeField] private GameObject spawnPointOutsideHouse;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(exitHouse(collision.gameObject));
    }

    private IEnumerator exitHouse(GameObject collision)
    {
        yield return new WaitForSeconds(1f);
        collision.gameObject.transform.position = spawnPointOutsideHouse.transform.position;

    }
}
