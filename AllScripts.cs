// --- Parallax System ---
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour {
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
    private float oldPosition;

    void Start() { oldPosition = transform.position.x; }

    void Update() {
        if (transform.position.x != oldPosition) {
            onCameraTranslate?.Invoke(oldPosition - transform.position.x);
            oldPosition = transform.position.x;
        }
    }
}

using System.Collections.Generic;
[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour {
    public ParallaxCamera parallaxCamera;
    private readonly List<ParallaxLayer> parallaxLayers = new();

    void Start() {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();

        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;

        SetLayers();
    }

    private void SetLayers() {
        parallaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++) {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();
            if (layer != null) {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }

    private void Move(float delta) {
        foreach (ParallaxLayer layer in parallaxLayers)
            layer.Move(delta);
    }
}

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour {
    public float parallaxFactor;

    public void Move(float delta) {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor;
        transform.localPosition = newPos;
    }
}

// --- Enemy System ---
[System.Serializable]
public class EnemyHealth : MonoBehaviour {
    public int maxHealth = 3;
    private int currentHealth;
    private Animator animator;

    void Start() { currentHealth = maxHealth; }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}

public class Grunt : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject player;

    void Update() { HandleShooting(); }

    void HandleShooting() {
        if (Vector3.Distance(player.transform.position, transform.position) < 2.0f) {
            Shoot();
        }
    }

    void Shoot() {
        // Bullet specific logic
    }
}

public class SlimeControl : MonoBehaviour {
    public float speed = 2.0f;
    private int direction = 1;

    void Update() {
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        // Collision checks to invert direction
    }
}

// --- Bullets ---
public class MageBullet : MonoBehaviour {
    public int damage = 1;
    private Rigidbody2D rb;

    void Start() { rb = GetComponent<Rigidbody2D>(); }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            // Apply damage
            Destroy(gameObject);
        }
    }
}