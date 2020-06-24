using UnityEngine;

public class Movement : MonoBehaviour
{
    public Controller controller;
    public Animator animator;
    [SerializeField] private GhostTrail ghostTrail;

    [SerializeField] public float speed = 40f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    private float horizontalMove;
    public bool jump = false;
    public bool crouch = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            FindObjectOfType<AudioManager>().Play("Player Jump");
        }else if (Input.GetKeyUp(KeyCode.Space))
            jump = false;

        if (GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (GetComponent<Rigidbody2D>().velocity.y > 0 && !Input.GetButton("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.S))
            crouch = true;
        else if (Input.GetKeyUp(KeyCode.S))
            crouch = false;
    }
    private void FixedUpdate()
    {
        Move();
    }
    public void Move() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);

        ghostTrail.makeGhost = true;
    }
    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
        jump = false;
    }
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }
    public void OnJumping(bool isJumping)
    {
        animator.SetBool("isJumping", isJumping);
    }
}
