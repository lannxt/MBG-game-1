using UnityEngine;
using TMPro; // Wajib buat TextMeshPro

public class CoinCollector : MonoBehaviour
{
    public int koin = 0;
    public TextMeshProUGUI coinText; // Referensi Text Apel di layar

    [Header("UI Pas Menang")]
    public GameObject winPanel;
    public GameObject coinTextDisplay;
    public GameObject nyawaUI;

    [Header("Level Settings")]
    public int targetApel = 5; // Ganti angka ini di Inspector tiap level (Level 2 jadi 10, dsb)

    // FUNGSI BARU: Biar angka koin gak lanjut dari level sebelumnya
    void Start()
    {
        koin = 0; // Reset koin ke 0 setiap scene/level baru dimulai
        UpdateTextUI(); // Pastikan tulisan di layar langsung "Apel: 0 / 5"
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Ambil Apel
        if (other.CompareTag("Coin"))
        {
            koin++;
            UpdateTextUI();
            Destroy(other.gameObject);
        }

        // 2. Finish
        if (other.CompareTag("Finish"))
        {
            if (koin >= targetApel)
            {
                MuncullkanWinPanel();
            }
            else
            {
                Debug.Log("Apel belum cukup! Butuh: " + targetApel);
            }
        }
    }

    // Fungsi biar kodenya rapi
    void UpdateTextUI()
    {
        if (coinText != null)
        {
            coinText.text = "Apel: " + koin + " / " + targetApel;
        }
    }

    void MuncullkanWinPanel()
    {
        if (winPanel != null) winPanel.SetActive(true);

        // Ngilangin UI biar bersih pas menang
        if (coinTextDisplay != null) coinTextDisplay.SetActive(false);
        if (nyawaUI != null) nyawaUI.SetActive(false);

        Time.timeScale = 0; // Game berhenti

        // --- BAGIAN SIMPAN PROGRESS YANG LEBIH AMAN ---

        // 1. Ambil index level yang lagi dimainin sekarang
        int levelSekarang = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        // 2. Cek dulu, sebelumnya kita sudah sampai level berapa?
        // Kalau belum ada data, default-nya level 1
        int levelYangPernahDicapai = PlayerPrefs.GetInt("LevelReached", 1);

        // 3. Hanya simpan kalau level baru ini lebih tinggi dari sebelumnya
        // Contoh: Kalau lu main Level 1 lagi, tapi lu udah tamat Level 3, dia GAK AKAN ngerubah data Level 3 lu.
        if (levelSekarang + 1 > levelYangPernahDicapai)
        {
            PlayerPrefs.SetInt("LevelReached", levelSekarang + 1);
            PlayerPrefs.Save();
            Debug.Log("Progress Baru Disimpan! Level Terbuka: " + (levelSekarang + 1));
        }
    }
}