using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromPlayer : MonoBehaviour
{
public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

 

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

 

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        direction.Normalize();
        movement = -direction;
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("renne: " + movement);
            // rb.MovePosition((Vector2)transform.position + (movement * moveSpeed * Time.deltaTime));
            if (Mathf.Abs (rb.velocity.x) >= moveSpeed) {								//If sideways velocity is higher than or equal to set movement speed,
			    rb.velocity = new Vector2 (1 * moveSpeed, rb.velocity.y);	// set sideways velocity to movement speed.
            } else {
                rb.AddForce (new Vector2 (1 * (moveSpeed * 10f), 0));		//Else add sideways force to player
            }
        }
    }
}
