using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 8f;
    public float jumpForce = 12f;
    private float horizontal;
    private bool isFacingRight = true;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Ambil Input (A/D atau Panah)
        horizontal = Input.GetAxisRaw("Horizontal");

        // UPDATE INI: Pastikan ejaan "isGrounded" dan "isRunning" sama persis dengan di Animator
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isRunning", horizontal != 0);

        // Kode lompat
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 4. Membalik Arah Karakter (Flip)
        Flip();

        // Tambahkan baris ini di bawah anim.SetBool("isRunning", ...);
        anim.SetBool("isGrounded", isGrounded);
    }

    void FixedUpdate()
    {
        // Menggerakkan karakter
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        // Mengecek apakah kaki menyentuh tanah (Ground Layer)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Biar kelihatan di Scene posisi GroundCheck-nya
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}