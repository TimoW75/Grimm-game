using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    //Animations State
    Animator animator;
    string currentState;

    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_WALK_FRONT = "Player_Walk_Front_Outside";
    const string PLAYER_WALK_BACK = "Player_Walk_Back_Outside";
    const string PLAYER_WALK_LEFT = "Player_Walk_Left_Outside";
    const string PLAYER_WALK_RIGHT = "Player_Walk_Right_Outside";

    public AudioClip walkingSound; // Reference to the audio clip

    private AudioSource audioSource; // New audio source variable

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Initialize audio source

        audioSource.clip = walkingSound; // Assign audio clip from the reference
    }

    void Update()
    {
        proccesInput();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void proccesInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveX > 0)
        {
            ChangeAnimationState(PLAYER_WALK_RIGHT);
        }
        else if (moveX < 0)
        {
            ChangeAnimationState(PLAYER_WALK_LEFT);
        }
        else if (moveY > 0)
        {
            ChangeAnimationState(PLAYER_WALK_BACK);
        }
        else if (moveY < 0)
        {
            ChangeAnimationState(PLAYER_WALK_FRONT);
        }
        else
        {
            ChangeAnimationState(PLAYER_IDLE);
        }

        if (moveDirection == Vector2.zero && audioSource.isPlaying) // Stop sound when player stops moving
        {
            audioSource.Stop();
        }
    }

    private void move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        if (moveDirection != Vector2.zero && !audioSource.isPlaying) // Play sound when player is moving
        {
            audioSource.Play();
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
