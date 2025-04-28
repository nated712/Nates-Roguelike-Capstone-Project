using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PManager : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    [HideInInspector] public Vector2 moveDir;
    [HideInInspector] public float lastVerticalVector;
    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public Vector2 lastMovedVector;

    private bool isDashing = false;
    private bool dashOnCooldown = false;
    private float dashMultiplier = 2f;
    private float dashDuration = .3f;
    private float dashCooldown = 5f;
    public Image dashIndicator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(-1, 0f);
    }

    void Update()
    {
        InputManagement();

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && !dashOnCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }
        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    void Move()
    {
        rb.linearVelocity = moveDir * moveSpeed;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        dashOnCooldown = true;

        // Change to black when dashing (NOT ready)
        if (dashIndicator != null) dashIndicator.color = Color.black;

        moveSpeed *= dashMultiplier;
        yield return new WaitForSeconds(dashDuration);
        moveSpeed /= dashMultiplier;

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;

        // Change back to white when dash is ready
        if (dashIndicator != null) dashIndicator.color = Color.white;
    }

}
