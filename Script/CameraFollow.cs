using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Siapa yang mau diikutin (Player)
    public float smoothing = 5f; // Seberapa halus ikutannya
    public Vector3 offset = new Vector3(0, 2, -10); // Jarak kamera dari player

    void FixedUpdate()
    {
        if (target != null)
        {
            // Tentukan posisi tujuan (posisi player + jarak aman)
            Vector3 targetPosition = target.position + offset;

            // Gerakkan kamera pelan-pelan ke posisi tujuan agar smooth
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}   