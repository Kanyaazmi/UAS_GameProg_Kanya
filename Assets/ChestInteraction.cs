using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Untuk memuat scene

public class ChestInteraction : MonoBehaviour
{
    public string sceneName = "MainMenu"; 

    private void Update()
    {
        // Deteksi input tombol F
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Lakukan tindakan jika tombol F ditekan
            InteractWithChest();
        }
    }

    // Fungsi untuk menangani interaksi
    void InteractWithChest()
    {
        // Logika untuk memuat scene Main Menu
        Debug.Log("Interaksi dengan Chest, kembali ke Main Menu");
        SceneManager.LoadScene(sceneName); 
    }


    private void OnTriggerEnter(Collider other)
    {
        // Pastikan hanya player yang berinteraksi dengan chest
        if (other.CompareTag("Player"))
        {
            InteractWithChest(); // Panggil fungsi interaksi saat berinteraksi dengan chest
        }
    }
}