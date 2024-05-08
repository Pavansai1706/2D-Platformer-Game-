using UnityEngine;

public class PlayerSound : MonoBehaviour
{
   [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float footstepInterval = 0.5f; // Interval between each footstep sound

    private float nextFootstepTime; // Time for the next footstep sound
    private  bool isJumping = false;
    private bool isCrouching = false;

    private const string HORIZONTAL = "Horizontal";


    private void Start()
    {
        nextFootstepTime = Time.time; // Initialize next footstep time
    }

    private void Update()
    {
        // Check if the player is grounded and not jumping or crouching
        if (Input.GetAxisRaw(HORIZONTAL) != 0 && IsGrounded() && Time.time >= nextFootstepTime && !isJumping && !isCrouching)
        {
            PlayFootstepSound(); // Play footstep sound when moving horizontally and grounded
        }
    }

    private void PlayFootstepSound()
    {
        // Randomly select a footstep sound from the array
        AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];

        // Play the selected footstep sound
        footstepSource.clip = footstepSound;
        footstepSource.Play();

        // Set the next footstep time based on the footstep interval
        nextFootstepTime = Time.time + footstepInterval;
    }

    private void PlayJumpSound()
    {
        footstepSource.clip = jumpSound;
        footstepSource.Play();
    }

    private bool IsGrounded()
    {
        // Cast a ray downwards to check if the player is grounded
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }

    private void SetJumping(bool value)
    {
        isJumping = value;
        if (isJumping)
        {
            PlayJumpSound(); // Play jump sound when the player starts jumping
        }
    }

    private void SetCrouching(bool value)
    {
        isCrouching = value;
    }
}
