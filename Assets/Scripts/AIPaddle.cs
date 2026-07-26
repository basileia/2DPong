using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform ball;
    [SerializeField] private Transform topWall;
    [SerializeField] private Transform bottomWall;

    [Header("AI Difficulty")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float reactionDelay = 0.35f;

    [SerializeField]
    private float mistakeChance = 0.35f;

    [SerializeField]
    private float mistakeOffset = 1.2f;

    [Header("Movement")]
    [SerializeField] private float centerReturnSpeed = 2f;
    [SerializeField] private float padding = 0.5f;

    private float minY;
    private float maxY;

    private float targetY;
    private float nextReactionTime;

    private void Start()
    {
        maxY = topWall.position.y - padding;
        minY = bottomWall.position.y + padding;

        targetY = transform.position.y;
    }

    private void Update()
    {
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();

        bool ballComing =
            ballRb.linearVelocity.x > 0;

        if (ballComing)
        {
            ReactToBall();
        }
        else
        {
            targetY = 0;
        }

        MovePaddle(ballComing);
    }


    private void ReactToBall()
    {
        if (Time.time < nextReactionTime)
            return;

        nextReactionTime =
            Time.time + reactionDelay;

        float ballY = ball.position.y;

        float error = 0;

        if (Random.value < mistakeChance)
        {
            error = Random.Range(
                -mistakeOffset,
                mistakeOffset
            );
        }

        targetY = ballY + error;

        targetY = Mathf.Clamp(
            targetY,
            minY,
            maxY
        );
    }

    private void MovePaddle(bool ballComing)
    {
        float currentY = transform.position.y;

        float moveSpeed =
            ballComing ? speed : centerReturnSpeed;

        float newY = Mathf.MoveTowards(
            currentY,
            targetY,
            moveSpeed * Time.deltaTime
        );

        newY = Mathf.Clamp(
            newY,
            minY,
            maxY
        );

        transform.position =
            new Vector3(
                transform.position.x,
                newY,
                transform.position.z
            );
    }
}