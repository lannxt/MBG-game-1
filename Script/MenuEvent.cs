using UnityEngine;
using UnityEngine.SceneManagement; // WAJIB buat pindah scene

public class MenuEvent : MonoBehaviour
{
    public GameObject menuUtama; // Ini isinya MenuUI
    public GameObject menuLevel; // Ini isinya Levels
    public GameObject titleGambar; // Untuk naruh objek Title

    // --- FUNGSI MENU UTAMA ---

    public void BukaPilihLevel()
    {
        if (menuUtama) menuUtama.SetActive(false);
        if (menuLevel) menuLevel.SetActive(true);
        if (titleGambar) titleGambar.SetActive(false);
    }

    public void TutupPilihLevel()
    {
        if (menuUtama) menuUtama.SetActive(true);
        if (menuLevel) menuLevel.SetActive(false);
        if (titleGambar) titleGambar.SetActive(true);
    }

    // --- FUNGSI TOMBOL DI PANEL (WIN/LOSE) ---

    // 1. Fungsi buat tombol Next Level
    public void NextLevelOtomatis()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // 2. Fungsi buat tombol Home
    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    // 3. FUNGSI BARU: Buat tombol Retry (Main ulang level ini)
    public void RetryGame()
    {
        // 1. Reset waktu biar gak freeze (karena pas kalah kan TimeScale jadi 0)
        Time.timeScale = 1f;

        // 2. Load ulang scene yang SEKARANG lagi dibuka
        // Ini kodenya sakti, di level 2 dia bakal restart level 2, dst.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log("Restarting level: " + SceneManager.GetActiveScene().name);
    }
}