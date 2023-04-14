using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndDrop : MonoBehaviour
{
    public float grabDistance = 1f;
    public string objectTag = "Objects";
    public float objectOffset = 0.5f;

    private GameObject grabbedObject;
    private bool isGrabbing = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!isGrabbing)
            {
                GrabObject();
            }
            else
            {
                DropObject();
            }
        }

        if (isGrabbing)
        {
            if (spriteRenderer.flipX)
            {
                grabbedObject.transform.position = transform.position - transform.right * objectOffset;
            }
            else
            {
                grabbedObject.transform.position = transform.position + transform.right * objectOffset;
            }
        }
    }

    void GrabObject()
    {
        Vector3 grabDirection = transform.right;
        if (spriteRenderer.flipX)
        {
            grabDirection = -transform.right;
        }
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, grabDirection, grabDistance);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.CompareTag(objectTag))
            {
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.transform.position = transform.position + grabDirection * objectOffset;
                grabbedObject.transform.parent = transform;
                isGrabbing = true;
                break;
            }
        }
    }

    void DropObject()
    {
        grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
        grabbedObject.transform.parent = null;
        grabbedObject = null;
        isGrabbing = false;
    }
}
