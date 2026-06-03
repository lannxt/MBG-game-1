using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    [Header("Aset")]
    public GameObject fireballPrefab; // Tarik Prefab Api ke sini
    public Transform mouthPoint;     // Tarik objek "mulut naga" ke sini

    [Header("Waktu Tembak")]
    public float shootingInterval = 2f; // Jeda antar tembakan (detik)
    private float timer;

    void Start()
    {
        // Biar tembakannya nggak langsung pas game mulai
        timer = shootingInterval;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootingInterval)
        {
            Shoot();
            timer = 0; // Reset waktu
        }
    }

    void Shoot()
    {
        // PENTING: Mencegah tembakan ganda!
        // Cek dulu apakah api sebelumnya sudah hilang atau belum.
        // Kita cari objek di Scene yang namanya "Semburan_Api(Clone)".
        GameObject existingFire = GameObject.Find("Semburan_Api(Clone)");

        // Kalau nggak ada api di scene, baru boleh nembak.
        if (existingFire == null)
        {
            // Munculkan api di depan mulut
            Instantiate(fireballPrefab, mouthPoint.position, transform.rotation);
        }
    }
}