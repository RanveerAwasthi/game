using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    [SerializeField]private LayerMask JumpableGround;
    private float dirx = 0f;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 6f;

    private enum MovementState { Idle,Run,Jump,Fall }
   

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {

        dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();


    }
    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirx > 0f)
        {
            state = MovementState.Run;
            sprite.flipX = false;
        }
        else if (dirx < 0f)
        {
            state = MovementState.Run;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.Idle;
        }
        if (rb.velocity.y > .01f)
        {
            state = MovementState.Jump;
        }
        else if (rb.velocity.y < -.01f)
        {
            state = MovementState.Fall;
        }
        anim.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }
}