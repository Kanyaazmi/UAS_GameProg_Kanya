using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    [SerializeField] private Tilemap tilemap; // Tilemap untuk boundary

    private Vector3 minBounds; // Batas bawah tilemap
    private Vector3 maxBounds; // Batas atas tilemap
    private float camHalfHeight; // Setengah tinggi kamera
    private float camHalfWidth; // Setengah lebar kamera

    private void Start()
    {
        // cellBounds untuk mendapatkan seluruh area tilemap
        BoundsInt cellBounds = tilemap.cellBounds;

        // Konversi cellBounds ke world space menggunakan ukuran tile
        Vector3 minCell = tilemap.CellToWorld(cellBounds.min);
        Vector3 maxCell = tilemap.CellToWorld(cellBounds.max);

        // Perbaiki posisi boundary agar sesuai dengan ukuran tile
        minBounds = minCell + tilemap.tileAnchor; // Tambahkan tileAnchor untuk penyesuaian
        maxBounds = maxCell + tilemap.tileAnchor;

        // Dapatkan ukuran kamera berdasarkan aspek rasio dan orthographic size
        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
    }

    private void Update()
    {
        // Hitung posisi target dengan offset
        Vector3 targetPosition = target.position + offset;

        // Batasi posisi target agar tetap dalam boundary tilemap
        float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);

        // Posisi akhir kamera
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, targetPosition.z);

        // SmoothDamp untuk transisi kamera
        transform.position = Vector3.SmoothDamp(transform.position, clampedPosition, ref velocity, smoothTime);
    }

    private void OnDrawGizmos()
{
    if (tilemap == null) return;

    // Gambarkan boundary dengan warna hijau
    Gizmos.color = Color.green;
    Gizmos.DrawLine(new Vector3(minBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, minBounds.y, 0));
    Gizmos.DrawLine(new Vector3(minBounds.x, maxBounds.y, 0), new Vector3(maxBounds.x, maxBounds.y, 0));
    Gizmos.DrawLine(new Vector3(minBounds.x, minBounds.y, 0), new Vector3(minBounds.x, maxBounds.y, 0));
    Gizmos.DrawLine(new Vector3(maxBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, maxBounds.y, 0));
}

}
