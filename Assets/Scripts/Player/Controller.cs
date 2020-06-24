using UnityEngine;
using UnityEngine.Events;

public class Controller : MonoBehaviour
{
    public float jumpForce = 400f;                         
    [Range(0, 1)] [SerializeField] private float crouchSpeed = 0.36f;         
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = 0.05f;
    [SerializeField] private bool airController = false;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Collider2D crouchDisableCollider;

    private readonly float grounded_Radius = 0.2f;
    private readonly float ceiling_Radius = 0.2f;
    private bool grounded;
    private bool facingRight = true;
    private Rigidbody2D rigidb2D;
    private Vector3 velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    public BoolEvent OnJumpEvent;

    private bool wasCrouching = false;
    private bool wasJumping = false;

    private void Awake()
    {
        rigidb2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

        if (OnJumpEvent == null)
            OnJumpEvent = new BoolEvent();
    }
    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, grounded_Radius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }
    public void Move(float move, bool crouch, bool jump)
    {
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceiling_Radius, whatIsGround))
            {
                crouch = true;
            }
        }
        if(grounded || airController)
        {
            if (crouch)
            {
                if (!wasCrouching)
                {
                    wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                move *= crouchSpeed;

                if(crouchDisableCollider != null)
                    crouchDisableCollider.enabled = false;
            }
            else
            {
                if(crouchDisableCollider != null)
                {
                    crouchDisableCollider.enabled = true;
                }

                if (wasCrouching)
                {
                    wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }
           
            Vector3 targetVelocity = new Vector2(move * 10f, rigidb2D.velocity.y);
            rigidb2D.velocity = Vector3.SmoothDamp(rigidb2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            if(move > 0 && !facingRight)
            {
                Flip();

            }else if (move < 0 && facingRight)
            {
                Flip();
            }
        }
        if (grounded)
        {
            if (jump)
            {
                if (!wasJumping)
                {
                    wasJumping = true;
                    OnJumpEvent.Invoke(true);
                }

                rigidb2D.velocity = Vector2.up * jumpForce;
            }
            else
            {
                if (wasJumping)
                {
                    wasJumping = false;
                    OnJumpEvent.Invoke(false);
                }
            }
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
