using UnityEngine;

public class DamageSource : MonoBehaviour
{
    public int damageAmount = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();            
            playerHealth.TakeDamage(damageAmount);
            Destroy(gameObject);
            Debug.Log("TakeDamage");
        }
    }
}
