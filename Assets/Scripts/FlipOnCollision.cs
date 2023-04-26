using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOnCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 initialScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];

            // Check if the collision is on the right side
            if (contact.normal.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
            }
            // Check if the collision is on the left side
            else if (contact.normal.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
            }
        }
    }
}
