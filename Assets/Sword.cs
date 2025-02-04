using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Collider swordCollider; // Assign the sword's collider in the inspector
    public int swordDamage = 50; // Damage dealt by the sword
    public string enemyTag = "Enemy"; // Tag for enemies
    private Animator anim;

    void Start()
    {
        // Ensure the sword collider is initially disabled
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }

        anim = GetComponentInParent<Animator>(); // Assuming the animator is on the parent object
    }

    // Enable the sword collider
    public void EnableSwordCollider()
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = true;
        }
    }

    // Disable the sword collider
    public void DisableSwordCollider()
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }
    }

    // Handle collision detection
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider hit an enemy
        if (other.CompareTag(enemyTag))
        {
            Debug.Log("Enemy Hit!");

            // Access the enemy script and apply damage
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(swordDamage);
            }
        }
    }
}
