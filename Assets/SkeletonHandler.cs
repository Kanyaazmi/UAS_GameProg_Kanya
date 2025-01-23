using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkeletonHandler : MonoBehaviour
{
    public int enemiesDefeated =0;
    [SerializeField] private float moveSpeed = 3f; // Kecepatan pergerakan skeleton
    [SerializeField] private float detectionRange = 5f; // Jarak deteksi untuk mengikuti player
    [SerializeField] private TextMeshProUGUI enemyCountText;
    private Transform player; // Referensi ke posisi player
    private Rigidbody2D rb; // Komponen Rigidbody2D enemy
    private Animator animator; // Komponen Animator untuk animasi
    private SpriteRenderer spriteRenderer; // Untuk membalik sprite
    private Vector2 movement; // Arah pergerakan

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Cari GameObject dengan tag "Player"
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateEnemyCountUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                Vector2 direction = (player.position - transform.position).normalized; // Hitung arah menuju player
                movement = direction;

                // Update animasi berdasarkan arah pergerakan
                UpdateAnimationAndFlip(direction);
            }
            else
            {
                movement = Vector2.zero; // Diam jika di luar jarak deteksi
                animator.SetBool("isWalking", false); // Set animasi idle
            }
        }
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (movement != Vector2.zero)
        {
            rb.velocity = movement * moveSpeed; // Gerakkan enemy menuju player
        }
        else
        {
            rb.velocity = Vector2.zero; // Hentikan pergerakan
        }
    }

    private void UpdateAnimationAndFlip(Vector2 direction)
    {
        animator.SetBool("isWalking", true);

        // Tentukan animasi berdasarkan arah pergerakan
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) // Horizontal movement
        {
            if (direction.x > 0) // Ke kanan
            {
                spriteRenderer.flipX = false; // Sprite tidak terbalik
            }
            else // Ke kiri
            {
                spriteRenderer.flipX = true; // Membalik sprite
            }
            animator.Play("WalkRight"); // Mainkan animasi jalan (gunakan animasi yang sama untuk kiri/kanan)
        }
        else // Vertical movement
        {
            if (direction.y > 0) // Ke atas
            {
                animator.Play("WalkUp"); // Mainkan animasi berjalan ke atas
            }
            else // Ke bawah
            {
                animator.Play("WalkDown"); // Mainkan animasi berjalan ke bawah
            }
        }
    }
    // Fungsi untuk menangani serangan player
    public void TakeDamage()
    {
        // Hancurkan gameObject (skeleton)
        Destroy(gameObject);
    }

    // Deteksi serangan player melalui trigger collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack")) // Pastikan collider dari serangan player memiliki tag "PlayerAttack"
        {
            TakeDamage();
            GameManager.Instance.enemiesDefeated++;
            GameManager.Instance.SaveGameData();

            UpdateEnemyCountUI();

        }
    }
    // Fungsi untuk memperbarui UI berdasarkan jumlah musuh yang dikalahkan
    public void UpdateEnemyCountUI()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = $"Enemies Defeated: {GameManager.Instance.enemiesDefeated}";
        }
        
    }
    
    }


