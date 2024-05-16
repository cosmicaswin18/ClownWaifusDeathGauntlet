using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private int jumpsRemaining = 2; // Set to 2 for double jump
    private bool canDoubleJump = true;
    private bool isDashing = false;
    private int Direction = 1;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float dashSpeed = 40;
    [SerializeField] private float dashDuration = 0.7f;

    private enum MovementState { idle, running, jumping, falling, doublejump, dashing }

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource doubleJumpSoundEffect;
    [SerializeField] private AudioSource dashSoundEffect;

    public static bool isDoubleJump = false;
    public static bool isDash = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if(Input.GetButtonDown("Jump") && canDoubleJump && jumpsRemaining > 0 && isDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = false;
            jumpsRemaining--;
            doubleJumpSoundEffect.Play();
        }
        if (Input.GetButtonDown("Dash") && isDash)
        {
            dashSoundEffect.Play();
            StartCoroutine(Dash());
        }


        UpdateAnimationState();
    }
  /*  private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door")  && ItemCollector.keyEnabled)
        {
            Destroy(collision.gameObject);
            ItemCollector.keyEnabled = false;
        }
        
    }*/
        
    private IEnumerator Dash()
    {
        isDashing = true;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            rb.velocity = new Vector2(Direction * dashSpeed * Time.fixedDeltaTime * 20, rb.velocity.y);
            yield return null;
        }

        // Ensure that the velocity is reset after the dash ends
        rb.velocity = new Vector2(0f, rb.velocity.y);
        isDashing = false;
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
            Direction = 1;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
            Direction = -1;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
            if (rb.velocity.y > .1f && !canDoubleJump)
            {
                state = MovementState.doublejump;
                Debug.Log("Double Jump");
            }
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
            canDoubleJump = false;
        }
        if (isDashing)
        {
            state = MovementState.dashing;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        if(Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround))
        {
            jumpsRemaining = 2; // Reset jumps when grounded
            canDoubleJump = true;
        }
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
