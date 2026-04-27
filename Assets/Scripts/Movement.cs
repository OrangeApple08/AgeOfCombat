// Should probably add functions for everything like jump and collisions
using System;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;


public class Movement : MonoBehaviour
{
    // player variables
    public float moveSpeed;
    public float jumpForce;
    public bool playerGrounded = false;
    private Rigidbody2D rigi;
    private SpriteRenderer rigiSprite;
    private BoxCollider2D collide;
    private bool flipSide = true;

    // inputs
    private IA_PlayerInputs ctrl;

    // raycast stuff that I may or may not understand
    public float maxRayDistance;
    private float yBounds;
    private float ySpacing = 0.1f;


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
        Vector2 moveInput = ctrl.Fighting.Move.ReadValue<Vector2>();
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
        if (jumpInput == 1 && playerGrounded)
        {
            rigi.linearVelocityY = jumpForce;
            playerGrounded = false;
        }

        // see if grounded
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - yBounds - ySpacing), Vector2.down, maxRayDistance);
        if (hit.collider != null)
        {
            print(hit.collider);
            playerGrounded = true;
        }
        else
        {
            playerGrounded = false;
        }

        // show ray for cool purposes
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - yBounds - ySpacing), Vector2.down * (maxRayDistance), Color.cyan);
    }
}
