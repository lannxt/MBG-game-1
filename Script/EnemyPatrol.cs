using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    private bool movingRight = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (movingRight)
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pake "Tag" atau "Name" yang konsisten
        if (other.name.Contains("Square"))
        {
            Debug.Log("Nabrak Square: " + other.name);
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        // Cara balik badan pake Scale (Lebih aman buat Collider)
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}