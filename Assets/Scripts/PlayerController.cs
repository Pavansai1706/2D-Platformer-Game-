using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float crouchHeight = 0.5f;
    public float normalHeight = 1.0f;
    public float crouchSpeed = 5.0f;
    public Animator animator;

    public float speed;
    public float jump;

    private Rigidbody2D rb2d;
   
    private bool isCrouching =  false;
    private BoxCollider2D playerCollider;
    private Vector2 normalColliderCenter;
    private Vector2 normalColliderSize;
    

    private void Awake()
    {
        Debug.Log("Player Controller awake");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
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
        MoveCharacter(horizontal , vertical);
        PlayerMovementAnimation(horizontal , vertical);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }
    }
    
    private void MoveCharacter(float horizontal , float vertical)
    {
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;
        if (vertical > 0)
        {
            rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        }

    }
    

    private void PlayerMovementAnimation(float horizontal , float vertical)
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
        
        if (vertical > 0)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump" , false);
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
        IEnumerator ResizeCollider(float targetHeight)
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
    }
    }
