using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemCollectible : MonoBehaviour
{
    public int collectibles = 0;
    [SerializeField] private Tilemap tilemap; // Assign Tilemap Decoration
    [SerializeField] private AudioClip collectSound; // Assign collect sound effect
    [SerializeField] private TextMeshProUGUI itemCountTextTMP;
    private AudioSource audioSource;
     

    private void Start()
    {
        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // Update UI at start
        UpdateItemCountUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if collider is the player
        {
            // Get the position of the collision
            Vector3 hitPosition = collision.bounds.ClosestPoint(transform.position) + Vector3.up * 0.1f;
            Vector3Int cellPosition = tilemap.WorldToCell(hitPosition); // Convert to tilemap cell position

           if (tilemap.HasTile(cellPosition)) // Check if there's a tile at the position
            {
                // Play sound effect
                PlayCollectSound();

                // Remove tile from tilemap
                tilemap.SetTile(cellPosition, null);

                // Update collectibles dan simpan data
                GameManager.Instance.collectibles++;
                GameManager.Instance.SaveGameData(); // Simpan data setelah mengumpulkan item

                // Update UI
                UpdateItemCountUI();
            }
            
        }
    }

    private void PlayCollectSound()
    {
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound); // Play sound once
        }
        
    }

     private void UpdateItemCountUI()
    {
        if (itemCountTextTMP != null)
        {
            itemCountTextTMP.text = $"Items Collected: {GameManager.Instance.collectibles}";
        }
}
}

