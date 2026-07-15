using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 8f;     
    public float lifetime = 3f;
    public int damage = 10;      

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }

        else if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}