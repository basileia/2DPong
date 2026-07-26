using UnityEngine;

public enum GoalSide
{
    Left,
    Right
}

public class Goal : MonoBehaviour
{
    [SerializeField] private BallController ball;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private bool isLeftGoal;   // true = levý gól, false = pravý gól

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (isLeftGoal)
            {
                scoreManager.AddRightScore();
            }
            else
            {
                scoreManager.AddLeftScore();
            }

            ball.ResetBall();
        }
    }
}

