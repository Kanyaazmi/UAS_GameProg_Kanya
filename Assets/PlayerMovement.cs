using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isAttacking = false;
    [SerializeField] private GameObject attackArea; // Area serangan
    [SerializeField] private AudioSource attackSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Matikan area serangan di awal
        if (attackArea != null)
        {
            attackArea.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveInput * moveSpeed;

        // Attack input (Spacebar)
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);
        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

        // Flip sprite untuk arah kiri
        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true; // Membalik sprite jika ke kiri
        }
        else if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false; // Kembali normal jika ke kanan
        }
    }

    // Handle attack input
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking) // Trigger attack only if not already attacking
        {
            StartCoroutine(PerformAttack());
        }
    }

    // Coroutine to handle attack logic
    private IEnumerator PerformAttack()
    {
        isAttacking = true; // Start attack
        animator.SetBool("isAttacking", true); // Trigger attack animation

        // Play attack sound effect
        if (attackSound != null)
        {
            attackSound.Play(); // Mainkan efek suara
        }

        // Aktifkan area serangan
        if (attackArea != null)
        {
            attackArea.SetActive(true);
        }

        // Flip the attack animation based on the movement direction (use last input direction)
        if (animator.GetFloat("LastInputX") < 0)
        {
            spriteRenderer.flipX = true; // Flip attack for left
        }
        else if (animator.GetFloat("LastInputX") > 0)
        {
            spriteRenderer.flipX = false; // Normal attack for right
        }

        // Wait for the attack animation to finish 
        yield return new WaitForSeconds(0.5f); 
        // Nonaktifkan area serangan
        if (attackArea != null)
        {
            attackArea.SetActive(false);
        }

        // End the attack
        isAttacking = false;
        animator.SetBool("isAttacking", false); // Reset attack animation
    }
}
