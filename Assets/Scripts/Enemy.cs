using Pathfinding;
using UnityEngine;

public class Enemy : StateMachineBehaviour
{
    // Transform player;
    // Rigidbody2D rigidbody2D;
    // public Vector2 moveDirection;
    // public float speed=2.5f;
    // public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     player = GameObject.FindGameObjectWithTag("Player").transform;
    //     rigidbody2D  = animator.GetComponent<Rigidbody2D>();
    // }
    // public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     Vector2 target = new Vector2(player.position.x, rigidbody2D.position.y);
    //     moveDirection = (target - rigidbody2D.position).normalized * speed;
    //     HandleRotation();
    //     rigidbody2D.MovePosition(rigidbody2D.position + moveDirection * Time.fixedDeltaTime);
        
    // }
    // private void HandleRotation()
    // {
    //     if (moveDirection.x != 0)
    //     {
    //         IsDirectFaceRight = moveDirection.x > 0;

    //     }
    // }
    // private void ScoutHandle()
    // {
    //     RaycastHit2D[] rays =
    //     Physics2D.RaycastAll(rigidbody2D.position, IsDirectFaceRight ? Vector2.right : Vector2.left, scoutDistance);
    //     foreach (var hit in rays)
    //     {
    //         //Move to nearest player
    //         if (hit.transform.tag == "Player")
    //         {
    //             targetTransform = hit.transform;
    //            //moveDirection = ((Vector2)hit.transform.position - this.entityRigid.position).normalized;
    //             break;
    //         }
    //     }
    // }
    // // Update is called once per frame
    // private void OnDrawGizmos()
    // {

    // }
    // bool IsDirectFaceRight
    // {
    //     get
    //     {
    //         return entityGPX.localScale.x == 1;
    //     }
    //     set
    //     {
    //         entityGPX.localScale = new Vector3(value ? 1 : -1, 1, 1);
    //     }
    // }
    // bool IsAtEdge
    // {
    //     get
    //     {
    //         if (IsDirectFaceRight)
    //         {
    //             RaycastHit2D ray =
    //             Physics2D.Raycast(transform.position, Vector2.right, scoutDistance, PlatformLayer);
    //         }
    //     }
    // }
}
