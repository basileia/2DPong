using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private Rigidbody2D rb;
    private bool allowAntiStuck = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        LaunchBall();
    }

    private void Update()
    {
        if (allowAntiStuck && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            float newY = Random.Range(-0.3f, 0.3f);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, newY).normalized * speed;
        }
    }

    private void LaunchBall()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float angle = Random.Range(15f, 45f) * Mathf.Deg2Rad;

        float y = Mathf.Sin(angle) * (Random.Range(0, 2) == 0 ? -1 : 1);

        Vector2 direction = new Vector2(x, y).normalized;
        rb.linearVelocity = direction * speed;
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;

        allowAntiStuck = false;

        Invoke(nameof(LaunchBall), 1f);
        Invoke(nameof(EnableAntiStuck), 1.5f);

        
    }
    private void EnableAntiStuck()
    {
        allowAntiStuck = true;
    }
}
