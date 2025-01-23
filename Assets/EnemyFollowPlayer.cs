using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f; // Kecepatan pergerakan
    [SerializeField] private float deathDelay = 0.5f; // Waktu sebelum enemy dihancurkan setelah mati
    [SerializeField] private TextMeshProUGUI enemyCountText; // UI untuk enemy count

    private GameObject player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false; // Status kematian enemy

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        UpdateEnemyCountUI();
    }

    void Update()
    {
        if (!isDead && player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    // Fungsi untuk menerima damage dari player
    public void TakeDamage()
    {
        if (isDead) return; // Cek jika enemy sudah mati

        // Panggil fungsi untuk memperbarui jumlah musuh yang dikalahkan
        GameManager.Instance.IncrementEnemiesDefeated();

        // Panggil fungsi untuk memulai proses kematian
        Die(); 
    }

    // Fungsi kematian enemy
    private void Die()
    {
        if (isDead) return; // Pastikan die hanya dipanggil sekali

        isDead = true; 
        animator.SetTrigger("isDead"); 
        spriteRenderer.enabled = false; 

        // Update UI setelah musuh mati
        UpdateEnemyCountUI();

        // Hancurkan enemy setelah animasi selesai
        StartCoroutine(DestroyAfterDeath());
    }

    // Coroutine untuk menunggu hingga animasi kematian selesai sebelum menghancurkan enemy
    private IEnumerator DestroyAfterDeath()
    {
        // Tunggu hingga animasi selesai berdasarkan waktu yang ditentukan
        yield return new WaitForSeconds(deathDelay);

        // Hancurkan enemy
        Destroy(gameObject);
    }

    // Fungsi jika player menyerang enemy dengan collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack")) // Pastikan serangan memiliki tag "PlayerAttack"
        {
            TakeDamage(); 
        }
    }

    // Update jumlah musuh yang dikalahkan di UI
    public void UpdateEnemyCountUI()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = $"Enemies Defeated: {GameManager.Instance.enemiesDefeated}";
        }
    }
}
