using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player attributes")]
    private BoxCollider2D playerCollider2D;
    private PlayerManager playerManager;

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
            RaycastHit2D[] hit = Physics2D.BoxCastAll(playerCollider2D.bounds.center, new Vector2(playerManager.attack1Range, playerCollider2D.size.y), 0f, playerManager.onFaceDirection, playerManager.attack1Range/2, layerMask);
            foreach(var h in hit)
            {
                Debug.Log("Attacked!" +h.transform.name);
            }
        }

    }
}
