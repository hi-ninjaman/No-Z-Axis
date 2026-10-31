using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    public InputSystem_Actions actions;
    public float speed;
    public float jumpForce;
    public bool doubleJump;
    public float lookDistance;
    public float climbForce;
    public float bounceHeight;
    public Image healthImg;
    public GameObject camera;
    float move;

    Rigidbody2D rb;
    
    public Transform groundcheckTransform;
    public float groundCheckRadius;
    public float wallCheckRadius;
    public LayerMask groundLayer;
    public static int coins = 0;
    bool isGrounded;
    public Animator animation;
    public int health = 100;
    public int keys = 0;
    public SpriteRenderer spriteRenderer;
    public TextMeshProUGUI coinText;
    //public TextMeshProUGUI statusUI;
    void Awake()
    {
        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {

    actions.Player.Enable();
    actions.Player.Move.performed += Movement;
    actions.Player.Jump.performed += Jump;
    actions.Player.Climb.performed += Climb;
    actions.Player.Rotation.performed += Rotating;
    //actions.Player.Look.performed += Looking;

    actions.Player.Move.canceled += Movement;
    actions.Player.Jump.canceled += Jump;
    actions.Player.Climb.canceled += Climb;
    actions.Player.Rotation.canceled += Rotating;
    //actions.Player.Look.canceled += Looking;
    }

    private void OnDisable()
    {
    actions.Player.Disable();
    actions.Player.Move.performed -= Movement;
    actions.Player.Jump.performed -= Jump;
    }

    void Movement(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>().x;
    }

    /*
    void Looking(InputAction.CallbackContext ctx)
    {
        looking = ctx.ReadValue<Vector2>().x;
        looking = ctx.ReadValue<Vector2>().y;
    }
    */

    void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (isGrounded)
            {
                rb.linearVelocityY = jumpForce;  
            }
            else if(doubleJump == true)
            {
                rb.linearVelocityY = jumpForce - 1;
                doubleJump = false;
                //StatusUI.Text = false;
            }   

        }
    }
    void Climb(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(0f, 10f);
            }
        }
    }
    void Rotating(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if(rb.freezeRotation = true)
            {
                rb.freezeRotation = false;
            }
            else
            {
                rb.freezeRotation = true;
            }
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animation = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Checkpoint.savedPosition != Vector2.zero)
        {
            transform.position = Checkpoint.savedPosition;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheckTransform.position, groundCheckRadius, groundLayer);
        rb.linearVelocityX = move * speed;
        //camera.linearVelocityX = move * lookDistance;
        //camera.linearVelocityY = move * lookDistance;
        SetAnimation();
        if (isGrounded == true)
        {
            doubleJump = true;
        }
        coinText.text = $"c0Ins:/>43ev {coins}";

        
        if (Input.GetKey(KeyCode.Q))
        {
            rb.freezeRotation = false;
        }

        if (Input.GetKey(KeyCode.E))
        {
            rb.freezeRotation = true;
        }
        
    }

    private void SetAnimation()
    {
        if (isGrounded)
        {
            if(move == 0)
            {
                animation.Play("Player_idle");
            }
            else
            {
                animation.Play("Player_run");
            }     
       }
       else
        {
            if(rb.linearVelocityY > 0)
            {
                animation.Play("Player_jump");
            }
            else
            {
                animation.Play("Player_fall");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            health -= 25;
            rb.linearVelocityY = jumpForce;
            StartCoroutine(BlinkRed());
            healthImg.fillAmount = health / 100f;

            if(health <= 0)
            {
                Die();
            }
            
        }
        else if (collision.gameObject.CompareTag("bouncePad"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX,bounceHeight);
        }

    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
