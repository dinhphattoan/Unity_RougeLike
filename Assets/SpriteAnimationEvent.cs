using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player attributes")]
    private BoxCollider2D playerCollider2D;
    private PlayerManager playerManager;
    [SerializeField] float knockbackForce = 100f; // The force of the knockback
    [SerializeField] float upwardKnockbackForce = 5f; // The upward force of the knockback
    [SerializeField] int attackDamage = 10; // The amount of damage dealt by the attack

    [SerializeField] LayerMask layerMask;

    private void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        playerCollider2D = playerManager.playerCollider2D;
    }

    void EventAttack(int attackId)
    {
        if (attackId == 1)
        {
            RaycastHit2D[] hit = Physics2D.BoxCastAll(playerCollider2D.bounds.center, new Vector2(playerManager.attack1Range, playerCollider2D.size.y), 0f, playerManager.onFaceDirection, playerManager.attack1Range / 2, layerMask);
            foreach (var h in hit)
            {
                Debug.Log("Attacked!" + h.transform.name);
                // Deal damage to the enemy
                Heath enemy = h.transform.GetComponent<Heath>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                }
                
                Rigidbody2D enemyRb = h.transform.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    // Calculate knockback direction
                    Vector2 knockbackDirection = (h.transform.position - transform.position).normalized;
                    knockbackDirection.y += upwardKnockbackForce; // Add upward force
                    enemyRb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
