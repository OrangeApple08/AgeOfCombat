// Should probably add functions for everything like jump and collisions
using System;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;


public class Movement : MonoBehaviour
{
    // player variables
    public float moveSpeed;
    private Rigidbody2D rigi;
    private SpriteRenderer rigiSprite;
    private BoxCollider2D collide;
    private bool flipSide = true;
    public int playerNumber;
    Vector2 moveInput;

    // inputs
    private IA_PlayerInputs ctrl;

    // raycast stuff to change to box
    public float maxRayDistance;
    private float yBounds;
    private float ySpacing = 0.1f;

    // jump
    public float jumpForce;
    public float jumpTime;
    public bool grounded = false;
    private float groundCount;
    private float countSpeed = 50f;

    // alive
    public bool isAlive1 = true;
    public bool isAlive2 = true;
    public GameObject restartBtn;


    void Awake()
    {
        // get rigidbody
        rigi = GetComponent<Rigidbody2D>();
        rigiSprite = GetComponent<SpriteRenderer>();
        collide = GetComponent<BoxCollider2D>();
        yBounds = collide.bounds.extents.y;

        // connect input map to this ctrl variable
        ctrl = new IA_PlayerInputs();
        ctrl.Enable();

        // alive thinsg
        isAlive1 = true;
        isAlive2 = true;
        restartBtn.SetActive(false);
    }


    void OnDisable()
    {
        // deactivate the input
        ctrl.Disable();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // get inputs
        if (playerNumber == 1) 
        {
            if (isAlive1) 
            {
                moveInput = ctrl.Fighting.Move1.ReadValue<Vector2>();
            }
        } 
        else 
        {
            if (isAlive2) 
            {
                moveInput = ctrl.Fighting.Move2.ReadValue<Vector2>();
            }
        }
        float jumpInput = moveInput.y;
        
        // set the direction the player is facing, there's probably way better ways to do this...
        if (moveInput.x == 1)
        {
            flipSide = true;
        }
        else if (moveInput.x == -1)
        {
            flipSide = false;
        }
        rigiSprite.flipX = flipSide;
        

        // player movement
        rigi.linearVelocity = new Vector2(moveInput.x * moveSpeed, rigi.linearVelocity.y);
        

        // jump
        // if (jumpInput == 1 && grounded)
        // {
        //     rigi.linearVelocityY = jumpForce;
        //     grounded = false;
        // }

        // see if grounded
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - yBounds - ySpacing), Vector2.down, maxRayDistance);
        if (hit.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (grounded)
        {
            groundCount = 0f;
        }
        else
        {
            groundCount += countSpeed * Time.deltaTime;
        }

        if (jumpInput == 1)
        {
            // check groundcount
            if (groundCount <= jumpTime)
            {
                //rigi.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
                rigi.linearVelocity = new Vector2(rigi.linearVelocity.x, jumpForce);
                //rigi.linearVelocityY += jumpForce;
            }
        }

        // show ray for purposes
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - yBounds - ySpacing), Vector2.down * (maxRayDistance), Color.cyan);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.transform.tag == "KillPlane")
        {
            isAlive1 = false;
            isAlive2 = false;
            restartBtn.SetActive(true);
        }
        Debug.Log(other.transform.tag);
    }
}
