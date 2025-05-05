using UnityEngine;

public class DamageCollision : MonoBehaviour
{
    public int damage = 1;
    public float cooldown = 1f;
    private float nextDamageTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            collision.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
            nextDamageTime = Time.time + cooldown;
        }
    }
}