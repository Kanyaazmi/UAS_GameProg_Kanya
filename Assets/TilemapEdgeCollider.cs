using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(EdgeCollider2D))]
public class TilemapEdgeCollider : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap; // Assign Tilemap di Inspector

    private void Start()
    {
        // Ambil komponen EdgeCollider2D
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();

        // Ambil batas Tilemap
        BoundsInt bounds = groundTilemap.cellBounds;

        // Konversi posisi Tilemap ke world position
        Vector3 bottomLeft = groundTilemap.CellToWorld(bounds.min);                      // Kiri bawah
        Vector3 bottomRight = groundTilemap.CellToWorld(new Vector3Int(bounds.max.x, bounds.min.y, bounds.min.z)); // Kanan bawah
        Vector3 topRight = groundTilemap.CellToWorld(bounds.max);                       // Kanan atas
        Vector3 topLeft = groundTilemap.CellToWorld(new Vector3Int(bounds.min.x, bounds.max.y, bounds.min.z)); // Kiri atas

        // Tentukan titik-titik keliling Tilemap
        Vector2[] points = new Vector2[]
        {
            new Vector2(bottomLeft.x, bottomLeft.y), // Kiri bawah
            new Vector2(bottomRight.x, bottomRight.y), // Kanan bawah
            new Vector2(topRight.x, topRight.y), // Kanan atas
            new Vector2(topLeft.x, topLeft.y), // Kiri atas
            new Vector2(bottomLeft.x, bottomLeft.y) // Kembali ke titik awal
        };

        // Set points di EdgeCollider
        edgeCollider.points = points;

      
    }
}

