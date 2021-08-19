using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PockemonController : MonoBehaviour
{
    private Rigidbody2D rbody2d;
    private Animator animator;

    public int score = 2;

    public float maxSpeed;
    public float moveForce;
    public bool jump;
    public float jumpForce;

    public Transform groundCheck;
    public Transform modelTransform;

    private bool isFacingRight = true;
    private float currentRotation = 90;
    private Transform playerController;
    private float direction;
    private Player player;
    private bool isCollidedWithPLayer = false;
    public AudioSource oneTimeSoundPrefab;
    public AudioClip sound;

    void Start()
    {
        rbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerController = GameObject.FindObjectOfType<Controller2D>().transform;
        player = playerController.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        direction = Mathf.Sign(playerController.position.x - transform.position.x);

        if (direction * rbody2d.velocity.x < maxSpeed)
        {
            rbody2d.AddForce(Vector2.right * direction * moveForce);
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

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(0.5f, 0.5f) * direction);
        Debug.DrawRay(transform.position, new Vector2(0.5f, 0.5f) * direction, Color.red);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.CompareTag("Map"))
            {
                if (isGrounded&& playerController.position.y>transform.position.y)
                {
                    jump = true;
                }
                break;
            }
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            isCollidedWithPLayer = true;
            player.TakeDamage(gameObject);
            AudioSource source = Instantiate(oneTimeSoundPrefab);
            source.clip = sound;
            source.Play();
            Destroy(source.gameObject, source.clip.length + 0.1f);

        }
    }

    private void OnDestroy()
    {
        if(!isCollidedWithPLayer)
        {
            player.AddScore(score);
        }
    }

}
