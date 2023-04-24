using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollisionHandler : MonoBehaviour
{
    private BoxCollider2D itemCollider;
    private SpriteRenderer itemRenderer;

    void Start()
    {
        itemCollider = GetComponent<BoxCollider2D>();
        itemRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Get the position of the player and the item.
            Vector2 playerPos = collision.transform.position;
            Vector2 itemPos = transform.position;

            // Check if the player collided on the right side of the item.
            if (playerPos.x > itemPos.x + itemCollider.bounds.extents.x)
            {
                // Flip the item on the X-axis.
                itemRenderer.flipX = true;
            }
            // If the player collided on the left side, uncheck the flipX property.
            else if (playerPos.x < itemPos.x - itemCollider.bounds.extents.x)
            {
                itemRenderer.flipX = false;
            }
        }
    }
}
