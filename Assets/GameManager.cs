using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    public int collectibles; // Variabel untuk menyimpan jumlah collectibles
    public int enemiesDefeated; // Variabel untuk menyimpan jumlah musuh yang dikalahkan


    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hancurkan object ini saat scene berubah
            LoadGameData(); // Panggil saat aplikasi mulai atau scene dimuat
        }
        else
        {
            Destroy(gameObject); // Hapus duplikasi GameManager
        }
        
       
    }
    
        // Menyimpan data ke file JSON
    public void SaveGameData()
    {
        GameData gameData = new GameData
        {
            collectibles = collectibles,
            enemiesDefeated = enemiesDefeated,
            collectedItemPositions = new List<Vector3Int>() // Kosongkan atau isi sesuai dengan item yang sudah dikoleksi
        };
        // Simpan data JSON ke file
        string json = JsonUtility.ToJson(gameData, true);
        string path = Application.persistentDataPath + "/gameData.json"; // Lokasi penyimpanan data
        File.WriteAllText(path, json);
        Debug.Log("Data disimpan ke " + path);
    }

    // Memuat data dari file JSON
    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/gameData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData gameData = JsonUtility.FromJson<GameData>(json);
            collectibles = gameData.collectibles; // Mengambil nilai collectibles
            enemiesDefeated = gameData.enemiesDefeated; // Mengambil jumlah musuh yang dikalahkan
            
        }
        
    }
    // Fungsi untuk menambahkan collectibles
    public void AddCollectible()
    {
        collectibles++;
        SaveGameData(); // Simpan data setelah menambahkan collectible
    }

    // Fungsi untuk memperbarui jumlah musuh yang dikalahkan
    public void IncrementEnemiesDefeated()
    {
        enemiesDefeated++;
        SaveGameData(); // Simpan data setelah memperbarui jumlah musuh yang dikalahkan
    }

    // Fungsi untuk mereset data permainan 
    public void ResetGameData()
    {
        collectibles = 0; // Reset collectibles
        enemiesDefeated = 0; // Reset musuh yang dikalahkan
        SaveGameData(); // Simpan data yang sudah di-reset
        Debug.Log("Data permainan di-reset.");
    }

    // Reset data otomatis saat aplikasi ditutup atau gameplay selesai
    private void OnApplicationQuit()
    {
        ResetGameData(); // Reset data saat aplikasi ditutup
    }
    
    }

