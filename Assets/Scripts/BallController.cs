using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float startSpeed = 8f;
    [SerializeField] private float speedIncrease = 0.3f;
    [SerializeField] private float maxSpeed = 16f;

    private Rigidbody2D rb;

    private bool allowAntiStuck = true;

    public float CurrentSpeed { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        CurrentSpeed = startSpeed;
        LaunchBall();
    }

    private void Update()
    {
        if (allowAntiStuck)
        {
            PreventFlatMovement();
        }
    }

    private void LaunchBall()
    {
        float angle = Random.Range(20f, 60f);

        Vector2 direction = Quaternion.Euler(0, 0, Random.Range(0, 2) == 0 ? angle : -angle) * Vector2.right;

        if (Random.Range(0, 2) == 0)
            direction.x *= -1;

        rb.linearVelocity = direction.normalized * CurrentSpeed;
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;

        rb.linearVelocity = Vector2.zero;

        CurrentSpeed = startSpeed;

        allowAntiStuck = false;

        Invoke(nameof(LaunchBall), 1f);
        Invoke(nameof(EnableAntiStuck), 1.5f);
    }

    private void EnableAntiStuck()
    {
        allowAntiStuck = true;
    }

    public void IncreaseSpeed()
    {
        CurrentSpeed += speedIncrease;
        CurrentSpeed = Mathf.Min(CurrentSpeed, maxSpeed);

        rb.linearVelocity = rb.linearVelocity.normalized * CurrentSpeed;
    }

    private void PreventFlatMovement()
    {
        Vector2 dir = rb.linearVelocity.normalized;

        if (Mathf.Abs(dir.y) < 0.12f)
        {
            dir.y = dir.y >= 0 ? 0.12f : -0.12f;
            rb.linearVelocity = dir.normalized * CurrentSpeed;
        }

        if (Mathf.Abs(dir.x) < 0.25f)
        {
            dir.x = dir.x >= 0 ? 0.25f : -0.25f;
            rb.linearVelocity = dir.normalized * CurrentSpeed;
        }
    }
}