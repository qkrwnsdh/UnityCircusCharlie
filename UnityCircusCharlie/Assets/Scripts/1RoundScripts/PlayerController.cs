using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = default;
    public AudioClip deathClip;
    private float jumpForce = 400f;
    private int jumpCount = 0;

    private bool isGrounded = false;
    private bool isDead = false;
    private bool isWalk = false;
    private bool isJump = false;
    private bool isAni = false;

    public GameObject playerObject = default;
    private Rigidbody2D playerRigid = default;
    private Animator animator = default;
    private AudioSource playerAudio = default;
    private CapsuleCollider2D capsuleCollider = default;

    // Start is called before the first frame update


    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();


        GFunc.Assert(playerRigid != null);
        GFunc.Assert(animator != null);
        GFunc.Assert(playerAudio != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { return; }

        Move();
    }

    private void Move()
    {
        // 점프 움직임
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            jumpCount += 1;
            playerRigid.velocity = Vector2.zero;
            playerRigid.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
            animator.SetBool("Player_Jump", true);
            animator.SetBool("Player_Ani", false);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && 0 < playerRigid.velocity.y)
        {
            playerRigid.velocity = playerRigid.velocity * 0.5f;

        }
        // 왼쪽으로 움직임
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetBool("Player_Walk", true);
            animator.SetBool("Player_Jump", false);
            animator.SetBool("Player_Ani", false);
        }
        // 오른쪽으로 움직임
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetBool("Player_Walk", true);
            animator.SetBool("Player_Jump", false);
            animator.SetBool("Player_Ani", false);
        }
        // 춤
        else if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetBool("Player_Ani", true);
        }
        // 안움직임
        else
        {
            animator.SetBool("Player_Jump",false);
            animator.SetBool("Player_Walk", false);
            animator.SetBool("Player_Ani", false);
        }
    }

    private void Dead()
    {
        animator.SetTrigger("Dead");
        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigid.velocity = Vector2.zero;
        isDead = true;

        GameManager.instance.OnPlayerDead();

    }

    private IEnumerator Die()
    {
        Dead();
        yield return new WaitForSeconds(0.5f);
        capsuleCollider.size = new Vector2(capsuleCollider.size.x, 0.4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Dead") && isDead == false)
        {
            StartCoroutine(Die());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (0.7 < collision.contacts[0].normal.y)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
