using UnityEngine;

public class MoveFireball : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        Destroy(gameObject, 4f);
    }

    // GUE GABUNGIN TRIGGER DAN COLLISION BIAR PASTI KENA
    private void OnTriggerEnter2D(Collider2D other) { HandleCollision(other.gameObject); }
    private void OnCollisionEnter2D(Collision2D collision) { HandleCollision(collision.gameObject); }

    void HandleCollision(GameObject obj)
    {
        // Kalau kena apapun yang namanya ada Square atau Layer-nya Pagar
        if (obj.name.Contains("Square") || obj.layer == LayerMask.NameToLayer("Pagar"))
        {
            Debug.Log("API MENTOK!");
            Destroy(gameObject);
        }

        if (obj.CompareTag("Player"))
        {
            HealthManager hm = obj.GetComponent<HealthManager>();
            if (hm != null) hm.KurangiNyawa();
            Destroy(gameObject);
        }
    }
}