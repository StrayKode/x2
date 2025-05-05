using UnityEngine;

public class SlimeControl : MonoBehaviour
{
    public float velocity = 5f;
    private Rigidbody2D rb;
    private int direction = -1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(direction * velocity, rb.velocity.y);
        transform.localScale = new Vector3(direction == -1 ? 1.0f : -1.0f, 1.0f, 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Player"))
        {
            direction *= -1;
        }

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>()?.TakeDamage(1);
        }
    }
}