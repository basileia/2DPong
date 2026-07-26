using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform topWall;
    [SerializeField] private Transform bottomWall;
    [SerializeField] private float padding = 0.5f;

    private float minY;
    private float maxY;
    private float lastY;

    public float paddleVelocity;

    private void Start()
    {
        maxY = topWall.position.y - padding;
        minY = bottomWall.position.y + padding;
    }
    private void Update()
    {
        lastY = transform.position.y;

        // klávesnice
        float input = Input.GetAxisRaw("Vertical");
        if (input != 0)
        {
            Move(input);
        }

        // Tablet / mobil (touch)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width * 0.5f) // jen levá polovina obrazovky
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
                MoveTo(worldPos.y);
            }
        }

        // Myš
        if (Input.GetMouseButton(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.mousePosition.x < Screen.width * 0.5f)
            {
                MoveTo(worldPos.y);
            }
        }

        // spočítáme rychlost paddle (pro spin)
        paddleVelocity = (transform.position.y - lastY) / Time.deltaTime;
    }

    private void Move(float input)
    {
        Vector3 direction = Vector3.up * input;
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }

    private void MoveTo(float targetY)
    {
        float clampedY = Mathf.Clamp(targetY, minY, maxY);

        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, clampedY, Time.deltaTime * 10f);
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallController ball = collision.gameObject.GetComponent<BallController>();

        if (ball == null)
            return;

        float paddleHeight = GetComponent<Collider2D>().bounds.size.y;

        float offset =
            (collision.transform.position.y - transform.position.y) /
            (paddleHeight / 2f);


        offset = Mathf.Clamp(offset, -1f, 1f);

        float maxBounceAngle = 60f;
        float angle = offset * maxBounceAngle;

        Vector2 direction;

        if (transform.position.x < 0)
        {
            direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        }
        else
        {
            direction = Quaternion.Euler(0, 0, angle) * Vector2.left;
        }

        collision.rigidbody.linearVelocity =
            direction.normalized * ball.CurrentSpeed;

        ball.IncreaseSpeed();
    }
}