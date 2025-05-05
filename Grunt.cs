using UnityEngine;

public class Grunt : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float lastShoot;

    void Update()
    {
        HandleOrientation();
        HandleShooting();
    }

    private void HandleOrientation()
    {
        Vector3 direction = player.transform.position - transform.position;
        transform.localScale = direction.x >= 0 ? Vector3.one : new Vector3(-1.0f, 1.0f, 1.0f);
    }

    private void HandleShooting()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < 2.0f && Time.time > lastShoot + 1.0f)
        {
            Shoot();
            lastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector2 direction = transform.localScale.x == 1.0f ? Vector2.right : Vector2.left;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
}