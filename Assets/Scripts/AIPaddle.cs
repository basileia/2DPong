using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private Transform topWall;
    [SerializeField] private Transform bottomWall;
    [SerializeField] private float padding = 0.5f;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float reactionDelay = 0.2f;
    [SerializeField] private float mistakeChance = 0.2f;
    [SerializeField] private float mistakeOffset = 1.0f;

    private float minY;
    private float maxY;

    private float nextReactionTime = 0f;
    private float targetY;

    private float velocityY = 0f;

    private void Start()
    {
        maxY = topWall.position.y - padding;
        minY = bottomWall.position.y + padding;
    }

    private void Update()
    {
        if (Time.time >= nextReactionTime)
        {
            nextReactionTime = Time.time + reactionDelay;

            float ballY = ball.position.y;

            float safeMin = minY + 0.5f;
            float safeMax = maxY - 0.5f;

            if (ballY < safeMin) ballY = safeMin;
            if (ballY > safeMax) ballY = safeMax;

            targetY = ballY;

            if (Random.value < mistakeChance)
            {
                targetY += Random.Range(-mistakeOffset, mistakeOffset);
            }

            targetY = Mathf.Clamp(targetY, minY, maxY);
        }

        Vector3 newPos = transform.position;

        newPos.y = Mathf.SmoothDamp(newPos.y, targetY, ref velocityY, 0.08f);

        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
        transform.position = newPos;
    }
}
