using System;
using UnityEngine;

public class GrabPuzzleBlock : MonoBehaviour
{
    private GameObject carriedObject;
    private bool isCarrying;
    private float lastHorizontal = 0f;
    private float lastVertical = -1f;
    [SerializeField] private float objectOffset = 0.5f;
    [SerializeField] private float distance;
    [SerializeField] private float smooth = 0.5f;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // update last direction when player moves
        if (horizontal != 0f || vertical != 0f)
        {
            lastHorizontal = horizontal;
            lastVertical = vertical;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isCarrying)
            {
                Drop();
            }
            else
            {
                Pickup(lastHorizontal, lastVertical);
            }
        }

        if (isCarrying)
        {
            Carry(carriedObject, lastHorizontal, lastVertical);
        }

    }

    void Carry(GameObject obj, float horizontal, float vertical)
    {

        Vector2 newPosition = gameObject.transform.position + new Vector3(horizontal, vertical) * objectOffset;
        obj.transform.position = Vector2.Lerp(obj.transform.position, newPosition, Time.deltaTime * smooth);
    }

    void Pickup(float horizontal, float vertical)
    {
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, distance);
        Debug.DrawRay(transform.position, direction * distance, Color.green);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Objects") && hit.collider.gameObject.GetComponent<Rigidbody2D>())
            {
                carriedObject = hit.collider.gameObject;
                isCarrying = true;
                carriedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                carriedObject.GetComponent<BoxCollider2D>().isTrigger = true;
                carriedObject.transform.parent = transform;

                break;
            }
        }
    }



    void Drop()
    {
        isCarrying = false;
        carriedObject.GetComponent<Rigidbody2D>().isKinematic = false;
        carriedObject.GetComponent<BoxCollider2D>().isTrigger = false;
        carriedObject.transform.parent = null;
        carriedObject = null;
    }
}
