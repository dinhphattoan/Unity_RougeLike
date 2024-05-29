
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] LayerMask layerMaskPlatform;
    Animator playerAnimator;
    Rigidbody2D playerRigid;
    PlayerInputAction playerInputAction;
    Transform _spriteTransform;
    [Space]
    [Header("System")]
    [SerializeField] Vector2 inputMoveDirection;
    [SerializeField] bool inputJump;
    public BoxCollider2D playerCollider2D;
    [Space]
    [Header("Player attribute")]
    [SerializeField] float move_smooth = 2f;
    [SerializeField] bool isBlocking = true;
    [SerializeField] bool isFreezed = true;
    public float move_speed = 5f;
    public float jump_Force = 5f;
    public float chargeTime = 1f;
    [Space]
    public float attack1Damage;
    public float attack1Range;
    [SerializeField] float chargeTimeCounter = 0f;
    private void OnEnable()
    {
        if (playerInputAction == null)
            playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();
        playerInputAction.Playermovement.movement.performed += context => inputMoveDirection = context.ReadValue<Vector2>();
        playerCollider2D = GetComponent<BoxCollider2D>();
    }


    private void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        playerRigid = GetComponent<Rigidbody2D>();
        _spriteTransform = this.transform.GetChild(0).transform;
        playerCollider2D = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        Color raycolor = Color.green;
        Debug.DrawRay(playerCollider2D.bounds.center - new Vector3(0, playerCollider2D.bounds.size.y / 2), onFaceDirection * attack1Range, raycolor);
        Debug.DrawRay(playerCollider2D.bounds.center + new Vector3(0, playerCollider2D.bounds.size.y / 2), onFaceDirection * attack1Range, raycolor);
        Debug.DrawRay(playerCollider2D.bounds.center + new Vector3(playerCollider2D.bounds.size.x / 2, -playerCollider2D.bounds.size.y / 2), Vector3.down * (playerCollider2D.bounds.extents.y), raycolor);

    }

    private void FixedUpdate()
    {
        HandleMovement(); HandleJump(); HandleActions();
        inputJump = Input.GetKey(KeyCode.Space);
        playerAnimator.SetBool("OnGround", OnGround);
    }



    void HandleMovement()

    {
        //Rotation handling on mouse direction
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            if (angle > 90 || angle < -90)
            {
                this._spriteTransform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                this._spriteTransform.localScale = new Vector3(1, 1, 1);
            }
        if (inputMoveDirection.x != 0 && !isFreezed && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            //Rotation handling base on direction going
            float horizontalMovement = inputMoveDirection.x;
            _spriteTransform.transform.localScale = new Vector3(horizontalMovement > 0 ? 1 : -1, 1, 1);




            //Movement handling
            this.playerRigid.AddForce(new Vector2(inputMoveDirection.x, 0) * Time.fixedDeltaTime * move_speed, ForceMode2D.Impulse);
            playerAnimator.SetBool("Run", true);

        }
        else
        {
            playerAnimator.SetBool("Run", false);
        }

        prevFaceDirection = onFaceDirection;
    }
    void HandleJump()
    {

        if (inputJump && OnGround)
        {
            playerRigid.AddForce(Vector2.up * jump_Force, ForceMode2D.Impulse);
            playerAnimator.Play("Jump");
        }
        if (playerRigid.velocity.y < 0f)
        {
            playerAnimator.Play("Fall");
        }

    }
    void HandleActions()
    {
        _Handle_Attack();
        _Handle_Block();
    }

    private void _Handle_Block()
    {
        if (Input.GetMouseButton(1) && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            playerAnimator.SetBool("ShieldBlock", true);
            isBlocking = true;
            isFreezed = true;
        }
        else
        {
            playerAnimator.SetBool("ShieldBlock", false);
            isBlocking = false;
            isFreezed = false;
        }
    }

    void _Handle_Attack()
    {
        if (chargeTimeCounter >= chargeTime)
        {
            if (Input.GetMouseButton(0))
            {
                playerAnimator.SetBool("ShieldBlock", false);
                isBlocking = false;
                chargeTimeCounter = 0;
                playerAnimator.Play("Attack");
            }
        }
        else
        {
            chargeTimeCounter += Time.deltaTime;
        }

    }
    public Vector2 prevFaceDirection;
    public Vector2 onFaceDirection
    {
        get
        {
            if (this._spriteTransform.localScale.x > 0)
            {
                return Vector2.right;
            }
            else
            {
                return Vector2.left;
            }
        }
    }

    bool OnGround
    {
        get
        {
            float extraHeightText = 0.1f;
            RaycastHit2D hit = Physics2D.BoxCast(playerCollider2D.bounds.center, playerCollider2D.bounds.size, 0f, Vector2.down, extraHeightText, layerMaskPlatform);
            Color raycolor;
            if (hit.collider != null)
            {
                raycolor = Color.green;
            }
            else
            {
                raycolor = Color.red;
            }

            Debug.DrawRay(playerCollider2D.bounds.center + new Vector3(playerCollider2D.bounds.extents.x, 0), Vector2.down * (playerCollider2D.bounds.extents.y + extraHeightText), raycolor);
            Debug.DrawRay(playerCollider2D.bounds.center - new Vector3(playerCollider2D.bounds.extents.x, 0), Vector2.down * (playerCollider2D.bounds.extents.y + extraHeightText), raycolor);
            Debug.DrawRay(playerCollider2D.bounds.center - new Vector3(playerCollider2D.bounds.extents.x, playerCollider2D.bounds.extents.y + extraHeightText), Vector2.right * (playerCollider2D.bounds.extents.x), raycolor);
            return hit.collider != null;

        }
    }
    private void OnDrawGizmos()
    {
        
    }
}