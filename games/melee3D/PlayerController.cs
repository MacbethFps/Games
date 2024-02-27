using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    AudioSource audioSource;

    [Header("Controller")]
    public float moveSpeed = 5;
    public float gravity = -9.8f;
    public float jumpHeight = 1.2f;

    Vector3 _playerVelocity;
    bool isGrounded;

    [Header("Camera")]
    public Camera cam;
    public float sensitivity;

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public GameObject sword; // Reference to the sword GameObject
    public GameObject hitEffect;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    bool attacking = false;
    bool readyToAttack = true;

    Animator swordAnimator; // Sword's Animator

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        swordAnimator = sword.GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize player's health
        currentHealth = maxHealth;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        // Repeat Inputs...
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
            attacking = true;
        }

        // Check jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        MoveInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

    void LateUpdate()
    {
        LookInput(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
    }

    void MoveInput(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
        _playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && _playerVelocity.y < 0)
            _playerVelocity.y = -2f;
        controller.Move(_playerVelocity * Time.deltaTime);
    }

    void LookInput(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        float xRotation = -mouseY * sensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -40, 40);

        cam.transform.localRotation *= Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * sensitivity * Time.deltaTime));
    }

    void Jump()
    {
        _playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
    }

    void Attack()
    {
        if (readyToAttack)
        {
            readyToAttack = false;
            swordAnimator.SetTrigger("AttackTrigger");
            Invoke(nameof(ResetAttack), attackSpeed);
            float animationDuration = 0.5f; // Adjust this based on your animation length
            Invoke(nameof(AttackRaycast), animationDuration);
        }
    }

    void ResetAttack()
    {
        readyToAttack = true;
        attacking = false;

        // Reset the "AttackTrigger" in the sword's animator
        swordAnimator.ResetTrigger("AttackTrigger");
    }

    void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            // Check if the hit object has an EnemyAI component
            EnemyAI enemyAI = hit.collider.GetComponent<EnemyAI>();
            Debug.Log("Hit something at: " + hit.point);
            if (enemyAI != null)
            {
                // Deal damage to the enemy
                enemyAI.TakeDamage(attackDamage);

            }
        }

        void HitTarget(Vector3 pos)
        {
            GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
            Destroy(GO, 20);
        }
    }

    // Function to handle taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if health is less than or equal to zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Handle player death here
    void Die()
    {
        // Implement game over logic or other actions on player death
        // For example, you can restart the level or show a game over screen.
    }
}
