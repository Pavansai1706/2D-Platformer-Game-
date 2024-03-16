using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float crouchHeight = 0.5f;
    public float normalHeight = 1.0f;
    public float crouchSpeed = 5.0f;
    public GameOverController gameOverContoller;
    public ScoreController scoreController;
    public Animator animator;

    private AudioSource footStepSource;
    public float speed = 5.0f;
    public float jumpForce = 6.0f;

    public Rigidbody2D rb2d;
    public bool isCrouching = false;
    private BoxCollider2D playerCollider;
    private Vector2 normalColliderCenter;
    private Vector2 normalColliderSize;
    private bool isGrounded = false;
    private bool jump;

    private void Awake()
    {
        Debug.Log("Player Controller awake");
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void KillPlayer()
    {
        Debug.Log("Player Killed by the player");
        gameOverContoller.PlayerDied();
    }

    public void PickUpKey()
    {
        Debug.Log("Picked up the key");
        scoreController.IncreaseScore(10);
    }

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        normalColliderCenter = playerCollider.offset;
        normalColliderSize = playerCollider.size;
        footStepSource = GetComponent<AudioSource>();
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
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void PlayerMovementAnimation(float horizontal, float vertical)
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
            animator.SetFloat("yVelocity", rb2d.velocity.y);
            animator.SetFloat("yVelocity", rb2d.velocity.y);
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
            animator.SetBool("Jump", !isGrounded);
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
