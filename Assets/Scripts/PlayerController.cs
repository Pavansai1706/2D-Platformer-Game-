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
    private ScoreController scoreController; // Added field for ScoreController
   public Animator animator;

    private float speed = 5.0f;
    private float jumpForce = 4.0f; // Decreased jump force

    private Rigidbody2D rigidBody2d;
    private bool isCrouching = false;
    private BoxCollider2D playerCollider;
    private Vector2 normalColliderCenter;
    private Vector2 normalColliderSize;
    private bool isGrounded = false;
    private bool jump;

    private const string CROUCH = "Crouch";
    private const string JUMP = "Jump";
    private const string GROUND = "Ground";
    private const string HORIZONTAL = "Horizontal";
    private const string SPEED = "Speed";

    private void Awake()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        normalColliderCenter = playerCollider.offset;
        normalColliderSize = playerCollider.size;

        // Find the ScoreController in the scene
        scoreController = FindObjectOfType<ScoreController>();
        if (scoreController == null)
        {
            Debug.LogError("ScoreController not found in the scene.");
        }
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw(HORIZONTAL);
        float vertical = Input.GetAxisRaw(JUMP);
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
        if (animator != null)
        {
            animator.SetFloat(SPEED, Mathf.Abs(horizontal));
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
                animator.SetBool(JUMP, true);
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
            animator.SetBool(CROUCH, true);
            StartCoroutine(ResizeCollider(crouchHeight));
        }
        else
        {
            animator.SetBool(CROUCH, false);
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
        if (other.transform.CompareTag(GROUND))
        {
            isGrounded = true;
            if (animator != null)
            {
                animator.SetBool(JUMP, !isGrounded);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag(GROUND))
        {
            isGrounded = false;
        }
    }

    private void KillPlayer() => gameOverController.PlayerDied();

    public void PickUpKey()
    {
        if (scoreController != null)
        {
            scoreController.IncreaseScore(10);
        }
    }
}
