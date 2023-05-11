
using System.Collections;
using UnityEngine;

public class LeaveHouse : MonoBehaviour
{
    [SerializeField] private GameObject spawnPointOutsideHouse;
    [SerializeField] private float outDoorTime = 0.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(exitHouse(collision.gameObject));
    }

    private IEnumerator exitHouse(GameObject collision)
    {
        yield return new WaitForSeconds(outDoorTime);
        collision.gameObject.transform.position = spawnPointOutsideHouse.transform.position;

    }
}
