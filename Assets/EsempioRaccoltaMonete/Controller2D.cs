using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour
{
    private Rigidbody2D rbody2d;
    private Animator animator;

    public float maxSpeed;
    public float moveForce;
    public bool jump;
    public float jumpForce;
    public Transform groundCheck;
    public Transform modelTransform;
    private bool isFacingRight = true;
    float currentRotation = 90;

    void Start()
    {
        rbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput * rbody2d.velocity.x < maxSpeed)
        {
            rbody2d.AddForce(Vector2.right * horizontalInput * moveForce);
        }

        if (Mathf.Abs(rbody2d.velocity.x) > maxSpeed)
        {
            rbody2d.velocity = new Vector2(Mathf.Sign(rbody2d.velocity.x) * maxSpeed, rbody2d.velocity.y);
        }

        if (rbody2d.velocity.x != 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if(rbody2d.velocity.x>0 && !isFacingRight)
        {
            Flip();
        }
        else if(rbody2d.velocity.x < 0 && isFacingRight)
        {
            Flip();
        }

        if (jump)
        {
            animator.SetTrigger("jump");
            rbody2d.AddForce(Vector2.up * jumpForce);
            jump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (isGrounded.collider != null)
        {
            Debug.Log(isGrounded.collider.name);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        if(isFacingRight)
        {
            modelTransform.localRotation = Quaternion.Euler(Vector3.up *90);

        }
        else
        {
            modelTransform.localRotation = Quaternion.Euler(Vector3.up * -90);
        }
    }
}
