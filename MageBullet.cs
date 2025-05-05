using UnityEngine;

public class MageBullet : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 1;
    public float bulletStrength = 0.02f;
    public Transform target;

    private Rigidbody2D rb;
    private Vector2 currentDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if (target != null)
            currentDirection = (target.position - transform.position).normalized;

        rb.velocity = currentDirection * speed;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 targetDirection = ((Vector2)target.position - rb.position).normalized;
        currentDirection = Vector2.Lerp(currentDirection, targetDirection, bulletStrength);
        rb.velocity = currentDirection * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerHealth>()?.TakeDamage(damage);

        Destroy(gameObject);
    }
}