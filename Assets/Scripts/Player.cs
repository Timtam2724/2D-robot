using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [Header("Player Settings")]
    public float maxSpeed = 10f; //Fastest the player can travel
    public float jumpForce = 400f; // Amount of force for jump
    [Range (0,1)]public float crouchSpeed = 0.30f; // Speed applied when crouched
    public bool airControl = false; // Allow steering while in air
    public LayerMask whatIsGround; // A layer mask to indicate ground
    public Text HealthText;

    private Rigidbody2D rb;
    private int health;
    private bool facingRight = true; // Which was is player facing?
    private Transform groundCheck;
    private float groundRadius = 0.2f;
    private bool grounded = false; // Checking if we are grounded
    private Transform ceilingCheck;
    private float ceilingRadius = 0.1f;
    private Animator anim;
    private Rigidbody2D rigid;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = 10;
        SetHealthText();
    }


    // Use this for initialization
    void Awake () {
        // Set up all our references
        groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// FixedUpdate is called at a specific framrate
	void FixedUpdate () {
        // Performing ground check (using Physcis2D)
        grounded = Physics2D.OverlapCircle(
            groundCheck.position, groundRadius,
            whatIsGround);

        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigid.velocity.y);
	}

    void Flip()
    {
        // Switch the way the player is facing
        facingRight = !facingRight;

        // Invert the scale of the player on X
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Inverts X
        transform.localScale = scale;
    }

    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if we can tand up
        if(crouch == false)
        {
            // Check to see if we hit ceiling
            if (Physics2D.OverlapCircle(ceilingCheck.position,
               ceilingRadius,
               whatIsGround))

            {
                crouch = true;
            }
        }

        anim.SetBool("Crouch", crouch);

        // Only control player if grounded or airControl is on
        if (grounded || airControl)
        {
            // Reduce the speed if we are crouching
            move = (crouch ? move * crouchSpeed : move);

            anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

            // If the input is moving player right
            if(move > 00 && !facingRight)
            {
                Flip();
            }
            else if(move < 0 && facingRight)
            {
                Flip();
            }
        }
        // If I'm grounded and trying to jump
        if(grounded && jump && anim.GetBool("Ground"))
        {
            anim.SetBool("Ground", false);
            grounded = false;
            rigid.AddForce(new Vector2(0, jumpForce));
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            health = health + 1;
            SetHealthText();
        }
       
    }

    void SetHealthText()
    {
        HealthText.text = "Health: " + health.ToString();
    }
}
