
using UnityEngine;

public class EntityAI : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 5f;
    public float scoutingRange = 5f;
    [SerializeField] Transform entityGFX;
    [SerializeField] BoxCollider2D entityCollider2D;
    [SerializeField] LayerMask layerMaskPlatform;
    public float enityThinkingSecondMax = 3f;
    public float enityThinkingSecondCounter = 0f;
    public Vector2 desiredPosition = Vector2.zero;// current position desired
    Rigidbody2D entityRb;
    // Start is called before the first frame update
    void Start()
    {
        entityCollider2D = GetComponent<BoxCollider2D>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        entityGFX = this.transform.GetChild(0).transform;
        entityRb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (desiredPosition.x != 0)
        {
            //performing goving to right
            if (desiredPosition.x > 0)
            {
                if (OnEdge != Vector2.right)
                {
                    this.entityRb.AddForce(new Vector2(1, 0) * Time.fixedDeltaTime * speed, ForceMode2D.Impulse);
                }
                else
                {
                    desiredPosition.x = 0;
                    enityThinkingSecondCounter = 0;
                }
            }
            else
            {
                if (OnEdge != Vector2.right)
                {
                    this.entityRb.AddForce(new Vector2(-1, 0) * Time.fixedDeltaTime * speed, ForceMode2D.Impulse);
                }
                else
                {
                    desiredPosition.x = 0;
                    enityThinkingSecondCounter = 0;
                }

            }
        }
        Mathf.Lerp(enityThinkingSecondCounter, enityThinkingSecondMax, Time.fixedDeltaTime);
        HandleMovement(ref desiredPosition);
        desiredPosition = OnScoutPlayer == Vector2.zero ? desiredPosition : OnScoutPlayer;
    }
    public void HandleMovement(ref Vector2 desiredPos)
    {
        //Check if enity is picking moving
        if (desiredPosition.x == 0 && enityThinkingSecondCounter == enityThinkingSecondMax)
        {
            //All movement must be handled here in priority
            //1: walk around movement, random choosing
            float x = Random.Range(0, 5f);
            desiredPosition.x = x;

        }

    }
    public Vector2 OnDirectionFacing
    {
        get
        {
            if (entityGFX.localScale.x > 0)
            {
                return Vector2.right;
            }
            else
            {
                return Vector2.left;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(this.transform.position, (Vector2)this.transform.position + (OnDirectionFacing * scoutingRange));
    }
    //Check if entity is on ground
    public bool OnGround
    {
        get
        {
            float extraHeightText = 0.1f;
            RaycastHit2D hit = Physics2D.BoxCast(entityCollider2D.bounds.center, entityCollider2D.bounds.size, 0f, Vector2.down, extraHeightText, layerMaskPlatform);
            Color raycolor;
            if (hit.collider != null)
            {
                raycolor = Color.green;
            }
            else
            {
                raycolor = Color.red;
            }

            Debug.DrawRay(entityCollider2D.bounds.center + new Vector3(entityCollider2D.bounds.extents.x, 0), Vector2.down * (entityCollider2D.bounds.extents.y + extraHeightText), raycolor);
            Debug.DrawRay(entityCollider2D.bounds.center - new Vector3(entityCollider2D.bounds.extents.x, 0), Vector2.down * (entityCollider2D.bounds.extents.y + extraHeightText), raycolor);
            Debug.DrawRay(entityCollider2D.bounds.center - new Vector3(entityCollider2D.bounds.extents.x, entityCollider2D.bounds.extents.y + extraHeightText), Vector2.right * (entityCollider2D.bounds.extents.x), raycolor);
            return hit.collider != null;
        }
    }
    //Get if the enitity is on the edge of the platform, return the side is on edge
    public Vector2? OnEdge
    {
        get
        {
            float extraHeightText = 2f;
            //Check onEdge two side
            //left
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position + new Vector3(-entityCollider2D.bounds.extents.x, 0), Vector2.down, extraHeightText, layerMaskPlatform);
            Color color = Color.white;
            Debug.DrawLine(this.transform.position + new Vector3(-entityCollider2D.bounds.extents.x, 0), Vector2.down * extraHeightText);
            if (hit)
            {
                return Vector2.left;
            }
            hit = Physics2D.Raycast(this.transform.position + new Vector3(entityCollider2D.bounds.extents.x, 0), Vector2.down, extraHeightText, layerMaskPlatform);
            Debug.DrawLine(this.transform.position + new Vector3(-entityCollider2D.bounds.extents.x, 0), Vector2.down * extraHeightText);
            if (hit)
            {
                return Vector2.right;
            }
            return null;
        }
    }
    //Get the position of player from scouting
    public Vector2 OnScoutPlayer
    {
        get
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(this.transform.position, OnDirectionFacing, scoutingRange);
            Debug.DrawRay(this.transform.position + new Vector3(OnDirectionFacing.x * scoutingRange, 0), Vector2.down, Color.white);
            if (raycastHit2D && raycastHit2D.transform.tag == "Player")
            {
                return raycastHit2D.transform.position;
            }
            return Vector2.zero;
        }
    }
}
