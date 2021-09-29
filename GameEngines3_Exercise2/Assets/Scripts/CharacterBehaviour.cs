using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    public float jumpForce;
    public LayerMask groundMask;
    public Transform groundCheck;

    //Private
    private Rigidbody2D rb;
    private float Direction;

    //States
    private bool isFacingRight = true;
    private bool isJumping;
    private bool isGrounded;
    

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        Direction = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        // Facing
        if(Direction > 0 && !isFacingRight) {
            Flip();
        }
        else if(Direction < 0 && isFacingRight) {
            Flip();
        }

    }

    void FixedUpdate()
    {
        //Check for Ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f,groundMask);

        //Move
        rb.velocity = new Vector2(Direction * moveSpeed, rb.velocity.y);

        //Jumping
        if(isJumping && isGrounded)
        {
            rb.AddForce(new Vector2(0.0f, jumpForce));
        }
        isJumping = false;
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f,180.0f,0.0f);
    }
}
