using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private BallController ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ball.ResetBall();
        }
    }
}
