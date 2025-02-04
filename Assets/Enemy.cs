using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;          // Enemy's health
    private Animator _animator;          // Animator component
    private bool isDead = false;         // To check if the enemy is already dead

    void Start()
    {
        _animator = GetComponent<Animator>();   // Get the Animator component
    }

    void Update()
    {
        // If health is zero or less and the enemy isn't already dead
        if (health <= 0 && !isDead)
        {
            Die();  // Call the Die function to trigger the death animation
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;  // Prevent damage if already dead

        health -= damage;  // Reduce health by damage

        if (health <= 0 && !isDead)
        {
            Die();  // Trigger die animation if health is 0 or less
        }
    }

    private void Die()
    {
        isDead = true;  // Mark the enemy as dead
        _animator.SetTrigger("Die");  // Set the Die trigger in the Animator
        
    }

    
}
