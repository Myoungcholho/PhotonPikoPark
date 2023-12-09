using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private bool isGround;
    private int jumpCount;
    private PlayerInput playerInput;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerInput.onJump += Jump;
    }

    private void Update()
    {
        if(playerInput.horizontal == -1)
        {
            playerRenderer.flipX = true;
        }
        else if(playerInput.horizontal == 1)
        {
            playerRenderer.flipX = false;
        }

    }

    private void FixedUpdate()
    {
        Move();
        PlayAnim();
    }

    private void Move()
    {
        playerRigidbody.velocity = new Vector2(playerInput.horizontal * speed, playerRigidbody.velocity.y);
    }
    private void Jump()
    {
        if(jumpCount < 1)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }
    private void PlayAnim()
    {
        if (playerInput.horizontal != 0 )
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }

        if (playerAnimator.GetBool("isGround") != isGround)
        {
            playerAnimator.SetBool("isGround", isGround);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            if (collision.contacts[0].normal.y > 0.7f)
            {
                isGround = true;
                jumpCount = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    private void OnDestroy()
    {
        playerInput.delegateJump -= Jump;
    }
}
