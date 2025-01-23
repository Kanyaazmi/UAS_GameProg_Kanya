using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Pastikan menggunakan namespace UI

public class TabController : MonoBehaviour
{
    public Image[] tabImages;   // Array untuk Image yang digunakan sebagai tab
    public GameObject[] pages;  // Array untuk halaman yang akan ditampilkan

    // Start is called before the first frame update
    void Start()
    {
        ActivateTab(0);  // Aktivasi tab pertama secara default
    }

    // Update is called once per frame
    public void ActivateTab(int tabNo)
    {
        // Deaktivasi semua halaman dan set warna tab menjadi abu-abu
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            if (tabImages[i] != null)
            {
                tabImages[i].color = Color.gray;  // Ganti warna tab menjadi abu-abu
            }
        }

        // Aktivasi halaman yang dipilih dan set warna tab menjadi putih
        pages[tabNo].SetActive(true);
        if (tabImages[tabNo] != null)
        {
            tabImages[tabNo].color = Color.white;  // Ganti warna tab menjadi putih
        }
    }
}

