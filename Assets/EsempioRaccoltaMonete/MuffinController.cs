using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuffinController : MonoBehaviour
{
    private Rigidbody2D rbody2d;
    private Animator animator;

    [Header("Movement")]
    public float maxSpeed;
    public float moveForce;
    public Transform modelTransform;

    [Header("Gound Check")]
    public LayerMask groundMask;
    public float rayLenght = 0.3f;
    public Transform rayOrigin;

    private bool isFacingRight = true;
    float currentRotation = 90;

    int currentDirection = 0;

    void Start()
    {
        rbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        Destroy(gameObject, 10f);
        currentDirection = -1;
    }

    private void FixedUpdate()
    {
       
        if (currentDirection * rbody2d.velocity.x < maxSpeed)
        {
            rbody2d.AddForce(Vector2.right * currentDirection * moveForce);
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

        if (rbody2d.velocity.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (rbody2d.velocity.x < 0 && isFacingRight)
        {
            Flip();
        }

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.position, Vector2.down, rayLenght, groundMask);
        if (!hit)
        {
            currentDirection *= -1;
            rbody2d.velocity = Vector2.right * currentDirection;
            rbody2d.angularVelocity = 0f;
        }

    }


    void Flip()
    {
        isFacingRight = !isFacingRight;

        if (isFacingRight)
        {
            modelTransform.localRotation = Quaternion.Euler(Vector3.up * 90);

        }
        else
        {
            modelTransform.localRotation = Quaternion.Euler(Vector3.up * -90);
        }
    }

    private void OnDrawGizmos()
    {
        if (rayOrigin != null)
        {
            Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + (Vector3.down) * rayLenght);
        }
    }
}
