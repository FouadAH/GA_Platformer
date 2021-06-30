using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb2D;
    public float speed;
    public float jumpSpeed = 30f;
    bool movingRight = true;
    Animator animator;

    public LayerMask ground;
    public Transform wallChecker;
    public Transform groundChecker;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run()
    {
        rb2D.velocity = new Vector2(speed * -transform.localScale.x, rb2D.velocity.y);

        if (IsHittinWall() || !IsGrounded())
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x = playerScale.x * -1;
        transform.localScale = playerScale;
    }


    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundChecker.position, -Vector2.up, 1.1f, ground);
        return hit;
    }

    bool IsHittinWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(wallChecker.position, Vector2.right * -transform.localScale.x, 1.3f, ground);
        return hit;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundChecker.position, groundChecker.position - Vector3.up * 1.1f);
        Gizmos.DrawLine(wallChecker.position, wallChecker.position + Vector3.right * -transform.localScale.x * 1.2f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
