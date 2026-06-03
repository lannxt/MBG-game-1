using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    // Variabel Nyawa & UI Hati
    public int nyawa = 3;
    public Image[] hatiUI;

    // Variabel Panel Pas Kalah
    public GameObject gameOverPanel;
    public GameObject coinTextDisplay;
    public GameObject nyawaUI;

    // Variabel Lokasi Kembali
    public Transform tiangStart;
    private Vector3 posisiRespawn;

    void Start()
    {
        // RESET WAKTU KE NORMAL (1) SETIAP KALI SCENE DIMULAI
        Time.timeScale = 1f;

        if (tiangStart != null)
        {
            posisiRespawn = tiangStart.position;
            transform.position = posisiRespawn;
        }
    }

    void Update()
    {
        // Kalau jatuh ke jurang (Y < -10)
        if (transform.position.y < -10f)
        {
            KurangiNyawa();
        }
    }

    // Fungsi utama untuk ngurangin nyawa
    public void KurangiNyawa()
    {
        nyawa--;
        Debug.Log("Nyawa sisa: " + nyawa);

        // Update UI Hati
        if (hatiUI != null && nyawa >= 0 && nyawa < hatiUI.Length)
        {
            if (hatiUI[nyawa] != null)
            {
                hatiUI[nyawa].gameObject.SetActive(false);
            }
        }

        if (nyawa <= 0)
        {
            ProsesGameOver();
        }
        else
        {
            // Respawn ke tiang start
            if (tiangStart != null)
            {
                transform.position = tiangStart.position;
            }
            else
            {
                Debug.LogError("Tiang Start kosong di Inspector!");
            }
        }
    }

    void ProsesGameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (coinTextDisplay != null) coinTextDisplay.SetActive(false);
        if (nyawaUI != null) nyawaUI.SetActive(false);

        Time.timeScale = 0f; // Berhentiin game
        Debug.Log("GAME OVER - PLAYER KALAH");
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // DETEKSI 1: Untuk benda tembus pandang / sensor (Gergaji, Api, Sensor Square)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Kena Trigger Musuh: " + other.name);
            KurangiNyawa();
        }
    }

    // DETEKSI 2: Untuk benda padat (Badan Naga, Musuh yang tidak di-trigger)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Tabrakan Fisik Musuh: " + collision.gameObject.name);
            KurangiNyawa();
        }
    }
}