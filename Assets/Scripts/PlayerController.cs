using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;
    public float speed;
    public float jumpSpeed = 30f;
    bool movingRight = true;
    Animator animator;

    public LayerMask ground;

    public int health = 4;
    public int gemCount = 0;

    GameUI gameUI;

    public Transform groundCheckL;
    public Transform groundCheckM;
    public Transform groundCheckR;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameUI = FindObjectOfType<GameUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        CheckFalling();
        //Debug.Log(IsGrounded());
    }

    public void UpdateGemCount()
    {
        gameUI.UpdateGem(gemCount);
    }

    public void TakeDamage()
    {
        health--;
        gameUI.UpdateHealthUI(-1);

        if (health < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }
    }

    void CheckFalling()
    {
        if (!IsGrounded())
        {
            if(rb2D.velocity.y > 0)
            {
                animator.Play("JumpUpAnimation");

            }
            else
            {
                animator.Play("JumpDownAnimation");
            }
        }
    }

    void Run()
    {
        float horzontalMovement = Input.GetAxisRaw("Horizontal");

        bool isGrounded = IsGrounded();

        if (horzontalMovement != 0)
        {
            if(isGrounded)
                animator.Play("RunAnimation");

            if (horzontalMovement < 0 && movingRight)
            {
                Flip();
            }
            else if (horzontalMovement > 0 && !movingRight)
            {
                Flip();
            }

            float moveBy = Input.GetAxisRaw("Horizontal") * speed;
            rb2D.velocity = new Vector2(moveBy, rb2D.velocity.y);
        }
        else if(isGrounded)
        {
            animator.Play("PlayerIdle");
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
        RaycastHit2D hitL = Physics2D.Raycast(groundCheckL.position, -Vector2.up, 1.1f, ground);
        RaycastHit2D hitM = Physics2D.Raycast(groundCheckM.position, -Vector2.up, 1.1f, ground);
        RaycastHit2D hitR = Physics2D.Raycast(groundCheckR.position, -Vector2.up, 1.1f, ground);

        return hitL || hitM || hitR;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckL.position, groundCheckL.position - Vector3.up * 1.1f);
        Gizmos.DrawLine(groundCheckM.position, groundCheckM.position - Vector3.up * 1.1f);
        Gizmos.DrawLine(groundCheckR.position, groundCheckR.position - Vector3.up * 1.1f);

    }

}
