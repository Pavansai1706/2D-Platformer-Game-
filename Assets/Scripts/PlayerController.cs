using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float crouchHeight = 0.5f;
    private float normalHeight = 1.0f;
    private float crouchSpeed = 5.0f;
    private GameOverController gameOverController;
    private ScoreController scoreController;
    private Animator animator; // <-- Initialize this field

    private float speed = 5.0f;
    private float jumpForce = 6.0f;

    private Rigidbody2D rigidBody2d;
    private bool isCrouching = false;
    private BoxCollider2D playerCollider;
    private Vector2 normalColliderCenter;
    private Vector2 normalColliderSize;
    private bool isGrounded = false;
    private bool jump;

    private void Awake()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // <-- Initialize the animator
    }

    public void KillPlayer()
    {
        gameOverController.PlayerDied();
    }

    public void PickUpKey()
    {
        scoreController.IncreaseScore(10);
    }

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        normalColliderCenter = playerCollider.offset;
        normalColliderSize = playerCollider.size;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        MoveCharacter(horizontal, vertical);
        PlayerMovementAnimation(horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }
    }

    private void MoveCharacter(float horizontal, float vertical)
    {
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;

        if (vertical > 0 && isGrounded)
        {
            rigidBody2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void PlayerMovementAnimation(float horizontal, float vertical)
    {
        if (animator != null) // Check if the animator is initialized
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontal));
            Vector3 scale = transform.localScale;
            if (horizontal < 0)
            {
                scale.x = -1f * Mathf.Abs(scale.x);
            }
            else if (horizontal > 0)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;

            if (vertical > 0 && !isCrouching)
            {
                jump = true;
                animator.SetBool("Jump", true);
            }
            else
            {
                jump = false;
                animator.SetFloat("yVelocity", rigidBody2d.velocity.y);
            }
        }
    }

    private void ToggleCrouch()
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            animator.SetBool("Crouch", true);
            StartCoroutine(ResizeCollider(crouchHeight));
        }
        else
        {
            animator.SetBool("Crouch", false);
            StartCoroutine(ResizeCollider(normalHeight));
        }
    }

    private IEnumerator ResizeCollider(float targetHeight)
    {
        Vector2 currentSize = playerCollider.size;
        Vector2 currentCenter = playerCollider.offset;
        Vector2 targetSize = new Vector2(normalColliderSize.x, targetHeight);
        Vector2 targetOffset = new Vector2(normalColliderCenter.x, targetHeight / 2f);
        float timer = 0;
        while (Mathf.Abs(currentSize.y - targetHeight) > 0.01f)
        {
            timer += Time.deltaTime * crouchSpeed;
            playerCollider.size = Vector2.Lerp(currentSize, targetSize, timer);
            playerCollider.offset = Vector2.Lerp(currentCenter, targetOffset, timer);
            yield return null;
        }

        playerCollider.size = targetSize;
        playerCollider.offset = targetOffset;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            if (animator != null) // Check if the animator is initialized
            {
                animator.SetBool("Jump", !isGrounded);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

