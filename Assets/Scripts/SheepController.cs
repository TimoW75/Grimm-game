using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    public float normalSpeed = 1.0f;
    public float scaredSpeed = 2.0f;
    public float detectionRadius = 5.0f;
    public string playerTag = "Player";
    public float stopChance = 0.1f;
    [SerializeField] private Item sheepItem;
    public InventoryManager inventoryManage;

    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 randomDirection;
    private float directionChangeInterval = 2.0f;

    // Animation State
    Animator animator;
    string currentState;

    const string SHEEP_IDLE = "Sheep_Idle";
    const string SHEEP_WALK_FRONT = "Sheep_Front";
    const string SHEEP_WALK_BACK = "Sheep_Back";
    const string SHEEP_WALK_LEFT = "Sheep_Left";
    const string SHEEP_WALK_RIGHT = "Sheep_Right";
    const string SHEEP_IDLE_FRONT = "Sheep_Idle_Front";
    const string SHEEP_IDLE_BACK = "Sheep_Idle_Back";
    const string SHEEP_IDLE_LEFT = "Sheep_Idle_Left";
    const string SHEEP_IDLE_RIGHT = "Sheep_Idle_Right";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag(playerTag);
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeDirection());
    }

    void Update()
{
    float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

    if (distanceToPlayer < detectionRadius)
    {
        Vector2 fleeDirection = (transform.position - player.transform.position).normalized;

        // Only move in straight lines
        float absX = Mathf.Abs(fleeDirection.x);
        float absY = Mathf.Abs(fleeDirection.y);
        if (absX > absY)
        {
            fleeDirection = new Vector2(Mathf.Sign(fleeDirection.x), 0);
        }
        else
        {
            fleeDirection = new Vector2(0, Mathf.Sign(fleeDirection.y));
        }

        rb.velocity = fleeDirection * scaredSpeed;
    }
    else
    {
        rb.velocity = randomDirection;
    }

    UpdateAnimation();
}

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            if (Random.value < stopChance)
            {
                randomDirection = Vector2.zero;
            }
            else
            {
                float moveX = (Random.value < 0.5f) ? -1 : 1;
                float moveY = (Random.value < 0.5f) ? -1 : 1;

                if (Random.value < 0.5f)
                {
                    randomDirection = new Vector2(moveX * normalSpeed, 0);
                }
                else
                {
                    randomDirection = new Vector2(0, moveY * normalSpeed);
                }
            }
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void UpdateAnimation()
{
    if (rb.velocity.x > 0)
    {
        ChangeAnimationState(SHEEP_WALK_RIGHT);
        if (Mathf.Approximately(rb.velocity.x, 0f))
        {
            ChangeAnimationState(SHEEP_IDLE_RIGHT);
        }
    }
    else if (rb.velocity.x < 0)
    {
        ChangeAnimationState(SHEEP_WALK_LEFT);
        if (Mathf.Approximately(rb.velocity.x, 0f))
        {
            ChangeAnimationState(SHEEP_IDLE_LEFT);
        }
    }
    else if (rb.velocity.y > 0)
    {
        ChangeAnimationState(SHEEP_WALK_BACK);
        if (Mathf.Approximately(rb.velocity.y, 0f))
        {
            ChangeAnimationState(SHEEP_IDLE_BACK);
        }
    }
    else if (rb.velocity.y < 0)
    {
        ChangeAnimationState(SHEEP_WALK_FRONT);
        if (Mathf.Approximately(rb.velocity.y, 0f))
        {
            ChangeAnimationState(SHEEP_IDLE_FRONT);
        }
    }
    else
    {
        if (currentState == SHEEP_WALK_FRONT)
        {
            ChangeAnimationState(SHEEP_IDLE_FRONT);
        }
        else if (currentState == SHEEP_WALK_BACK)
        {
            ChangeAnimationState(SHEEP_IDLE_BACK);
        }
        else if (currentState == SHEEP_WALK_LEFT)
        {
            ChangeAnimationState(SHEEP_IDLE_LEFT);
        }
        else if (currentState == SHEEP_WALK_RIGHT)
        {
            ChangeAnimationState(SHEEP_IDLE_RIGHT);
        }
    }
}

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        print(gameObject.tag);
        if (collision.gameObject.CompareTag(playerTag) && gameObject.CompareTag("Olive") && sheepItem != null)
        {
            Debug.Log("Schaap gevangen");
            Destroy(gameObject);
            InventoryManager.Instance.AddItem(sheepItem);

        }
    }

}
