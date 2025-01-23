using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public int collectibles; // Jumlah collectibles
    public int enemiesDefeated;
    public List<Vector3Int> collectedItemPositions; // Daftar posisi item yang sudah dikoleksi
}


