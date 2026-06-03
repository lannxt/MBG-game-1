using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [Header("Daftar Tombol Level")]
    public Button[] levelButtons; // Masukin tombol 1-6 ke sini

    void Start()
    {
        // Ambil data level mana yang terakhir kebuka
        // Angka 1 itu default (Level 1 kebuka dari awal)
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Tombol level 1 itu index 0, dst.
            // Kalau index + 1 lebih besar dari level yang sudah dicapai, kunci!
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false; // Tombol jadi mati/abu-abu
            }
            else
            {
                levelButtons[i].interactable = true; // Tombol aktif
            }
        }
    }

    // Tambahin ini biar tombol bisa pindah scene
    public void BukaLevel(string namaLevel)
    {
        Time.timeScale = 1f; // Reset waktu biar gak freeze
        UnityEngine.SceneManagement.SceneManager.LoadScene(namaLevel);
    }
}