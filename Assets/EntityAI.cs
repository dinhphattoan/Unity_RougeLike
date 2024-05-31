
using UnityEngine;

public class EntityAI : MonoBehaviour
{



    [SerializeField] Transform entityGFX;
    [SerializeField] BoxCollider2D entityCollider2D;
    [SerializeField] LayerMask layerMaskPlatform;
    [SerializeField] float valueHeadHeight = 2f;
    [SerializeField] float valueFeetHeight = 1f;
    public Vector2 desiredPosition = Vector2.zero;// current position desired
    Rigidbody2D entityRb;
    [Header("Scout attributes")]
    public float scoutingRangeFront = 30f; // Vision scout, 
    public float scoutingRangeBack = 10f;//Usually blured out when no vision to see, player could sneak in to it and perform additional damages
    [Space]
    [Header("Entity to Player Attributes")]
    public Transform playerTransform;// The transform of player when see!
    public float maxIdentifyTime = 1f; // Determine a number of second does the entity can identify player
    public float maxIdentifyTimeCounter = 0f;
    [Space]
    [Header("Entity attributes")]
    public float speed = 300f;
    public float speedWhenAlerted = 400f;
    public float enityThinkingSecondMax = 3f;
    public float enityThinkingSecondCounter = 0f;
    public float maxJumpDistance = 2f;
    public bool CanMove = true;

    // Start is called before the first frame update
    void Start()
    {
        entityCollider2D = GetComponent<BoxCollider2D>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        entityGFX = this.transform.GetChild(0).transform;
        entityRb = this.GetComponent<Rigidbody2D>();
        desiredPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        HandleMovement();

    }
    public void HandleMovement()
    {
        targetPosition = OnScoutPlayer;
        //if player is scouted
        if (targetPosition != Vector2.zero)
        {
            //When identify is fully complete
            //Chase player
            if (maxIdentifyTimeCounter == maxIdentifyTime)
            {
                moveToPosition(speedWhenAlerted, ref targetPosition, targetPosition - (Vector2)this.transform.position);
                lastKnownTarget = targetPosition;
                lastKnownSide = lastKnownTarget - (Vector2)this.transform.position;
            }
        }
        //If the player is outside of scouting range
        else
        {
            //Go check the last known place that player was scouted
            if (lastKnownTarget != Vector2.zero)
            {
                moveToPosition(speedWhenAlerted, ref lastKnownTarget,lastKnownSide);
            }
            //If checked the last known place that player was scouted but didn't find any or out of reach
            else
            {
                maxIdentifyTimeCounter = Mathf.MoveTowards(maxIdentifyTimeCounter, 0, Time.fixedDeltaTime);
                //When not alerted or lost of player vision
                if (maxIdentifyTimeCounter == 0)
                {
                    //Make enemy go left and right
                    Vector2 side = OnEdgeSideAllowed;
                    if (side == OnDirectionFacing)
                    {
                        enityThinkingSecondCounter = Mathf.MoveTowards(enityThinkingSecondCounter, enityThinkingSecondMax, Time.fixedDeltaTime);
                        if (enityThinkingSecondCounter >= enityThinkingSecondMax)
                        {
                            OnFaceDirection = OnFaceDirection * -1;
                            enityThinkingSecondCounter = 0;
                        }
                    }
                    else
                    {
                        if (OnFaceDirection == Vector2.left)
                        {
                            this.entityRb.AddForce(OnFaceDirection * speed * Time.deltaTime, ForceMode2D.Impulse);
                        }
                        else if (OnDirectionFacing == Vector2.right)
                        {
                            this.entityRb.AddForce(OnFaceDirection * speed * Time.deltaTime, ForceMode2D.Impulse);
                        }
                    }
                }

            }

        }

    }
    public bool isInvestigating = false;
    public Vector2 targetPosition = Vector2.zero;
    private Vector2 lastKnownTarget = Vector2.zero;
    private Vector2 lastKnownSide = Vector2.zero;
    private void moveToPosition(float speed, ref Vector2 position, Vector2 directionSide)
    {
        isInvestigating = true;
        Vector2 side = OnEdgeSideAllowed;
        if (directionSide.x > 0)
        {
            OnFaceDirection = Vector2.right;
            //Check if the way is clear to move
            if (this.transform.position.x < position.x && side != Vector2.right)
            {
                this.entityRb.AddForce(OnFaceDirection * speed * Time.deltaTime, ForceMode2D.Impulse);
                return;
            }
        }
        else
        {
            OnFaceDirection = Vector2.left;
            //Check if the way is clear to move
            if (this.transform.position.x > position.x && side != Vector2.left)
            {
                this.entityRb.AddForce(OnFaceDirection * speed * Time.deltaTime, ForceMode2D.Impulse);
                return;
            }
        }
        //Otherwise stop the intend when it reach out of boundaries by setting it to zero
        position = Vector2.zero;
    }
    private bool HaveReached(Vector2 targetPosition, Vector2 side)
    {
        if (side.x > 0)
        {
            return this.transform.position.x >= targetPosition.x;
        }
        else
        {
            return this.transform.position.x <= targetPosition.x;
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
        Gizmos.color = Color.cyan;
        //Scout radius
        Gizmos.DrawWireCube(new Vector2(this.transform.position.x, this.transform.position.y + 2f), new Vector2(scoutingRangeFront * 2, this.entityCollider2D.bounds.size.y + 2f));
        //Gizmo jump distance allowed
        Gizmos.color = Color.black;
        Gizmos.DrawLine(this.transform.position, (Vector2)this.transform.position + (OnDirectionFacing * maxJumpDistance));
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
    //Get if the enitity is on the edge of the platform, return the side is on edge or a wall
    public Vector2 OnEdgeSideAllowed
    {
        get
        {
            float extraHeightText = 5f;
            //Check wall two side
            //wall left upper
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position + new Vector3(0, valueHeadHeight), Vector2.left, entityCollider2D.bounds.extents.x + 1f, layerMaskPlatform);
            Debug.DrawLine(this.transform.position + new Vector3(-entityCollider2D.bounds.extents.x, valueHeadHeight), this.transform.position + new Vector3(-entityCollider2D.bounds.extents.x, valueHeadHeight) + Vector3.left * (+1f));
            if (hit)
            {
                return Vector2.left;
            }
            //Wall left down
            hit = Physics2D.Raycast(this.transform.position + new Vector3(0, valueFeetHeight), Vector2.left, entityCollider2D.bounds.extents.x + 1f, layerMaskPlatform);
            Debug.DrawLine(this.transform.position + new Vector3(-entityCollider2D.bounds.extents.x, valueFeetHeight), this.transform.position + new Vector3(-entityCollider2D.bounds.extents.x, valueFeetHeight) + Vector3.left * (+1f));
            if (hit)
            {
                return Vector2.left;
            }
            //Wall right upper
            hit = Physics2D.Raycast(this.transform.position + new Vector3(0, valueHeadHeight), Vector2.right, entityCollider2D.bounds.extents.x + 1f, layerMaskPlatform);
            Debug.DrawLine(this.transform.position + new Vector3(entityCollider2D.bounds.extents.x, valueHeadHeight), this.transform.position + new Vector3(entityCollider2D.bounds.extents.x, valueHeadHeight) + Vector3.right * (+1f));
            if (hit)
            {
                return Vector2.right;
            }
            //Wall right down
            hit = Physics2D.Raycast(this.transform.position + new Vector3(0, valueFeetHeight), Vector2.right, entityCollider2D.bounds.extents.x + 1f, layerMaskPlatform);
            Debug.DrawLine(this.transform.position + new Vector3(entityCollider2D.bounds.extents.x, valueFeetHeight), this.transform.position + new Vector3(entityCollider2D.bounds.extents.x, valueFeetHeight) + Vector3.right * (+1f));
            if (hit)
            {
                return Vector2.right;
            }
            //Check onEdge two side
            //left edge
            hit = Physics2D.Raycast(this.transform.position + new Vector3(-entityCollider2D.bounds.extents.x, -entityCollider2D.bounds.extents.y), Vector2.down, extraHeightText, layerMaskPlatform);
            Color color = Color.white;
            Debug.DrawLine(this.transform.position + new Vector3(-entityCollider2D.bounds.extents.x, -entityCollider2D.bounds.extents.y), (Vector2)(this.transform.position + new Vector3(-entityCollider2D.bounds.extents.x, 0)) + (Vector2.down * extraHeightText));
            if (!hit)
            {
                return Vector2.left;
            }
            //right
            hit = Physics2D.Raycast(this.transform.position + new Vector3(entityCollider2D.bounds.extents.x, -entityCollider2D.bounds.extents.y), Vector2.down, extraHeightText, layerMaskPlatform);
            Debug.DrawLine(this.transform.position + new Vector3(entityCollider2D.bounds.extents.x, -entityCollider2D.bounds.extents.y), (Vector2)(this.transform.position + new Vector3(entityCollider2D.bounds.extents.x, 0)) + (Vector2.down * extraHeightText));
            if (!hit)
            {
                return Vector2.right;
            }
            return Vector2.zero;
        }
    }
    //Get the position of player from scouting
    public Vector2 OnScoutPlayer
    {
        get
        {
            //Scouting_States in scouting the player
            //Scout_State 1: Identitfy player when in range, in front of entity
            float extraHeightText = 2f;
            RaycastHit2D[] hit = Physics2D.BoxCastAll(this.transform.position, new Vector2(scoutingRangeFront * 2, this.entityCollider2D.bounds.size.y + extraHeightText), 0f, Vector2.up, extraHeightText);

            for (int i = 0; i < hit.GetLength(0); i++)
            {
                if (hit[i].transform.tag == "Player")
                {
                    //Check if the player is behind the wall
                    bool flag = false;
                    for (int j = 0; j <= i; j++)
                    {
                        if (hit[j].transform.tag == "Foreground")
                        {
                            //Check the player on front 
                            if (hit[i].transform.position.x > this.transform.position.x)
                            {
                                if (hit[j].transform.position.x > this.transform.position.x && hit[j].transform.position.x < hit[i].transform.position.y)
                                {
                                    flag = true;
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                if (hit[j].transform.position.x < this.transform.position.x && hit[j].transform.position.x > hit[i].transform.position.y)
                                {
                                    flag = true;
                                }
                                else
                                {

                                }
                            }
                            if (!(hit[j].transform.position.y < (this.transform.position.y + entityCollider2D.bounds.extents.y)
                                    && hit[j].transform.position.y > (this.transform.position.y - entityCollider2D.bounds.extents.y)))
                            {
                                flag = false;
                                break;
                            }
                        }

                    }
                    if (flag)
                    {
                        return Vector2.zero;
                    }
                    maxIdentifyTimeCounter = Mathf.MoveTowards(maxIdentifyTimeCounter, maxIdentifyTime, Time.fixedDeltaTime);

                    return hit[i].transform.position;
                }
            }

            return Vector2.zero;

        }
    }
    public Vector2 OnFaceDirection
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
        set
        {
            Vector2 direct = value;
            if (direct.x > 0)
            {
                entityGFX.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                entityGFX.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
