using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private float speed = 4f;          
    [SerializeField] private float reactionDelay = 0.2f; 
    [SerializeField] private float mistakeChance = 0.2f;
    [SerializeField] private float mistakeOffset = 1.0f;

    private float nextReactionTime = 0f;
    private float targetY;

    private void Update()
    {
        if (Time.time >= nextReactionTime)
        {
            nextReactionTime = Time.time + reactionDelay;

            targetY = ball.position.y;

            if (Random.value < mistakeChance)
            {
                targetY += Random.Range(-mistakeOffset, mistakeOffset);
            }
        }

        Vector3 newPos = transform.position;
        newPos.y = Mathf.MoveTowards(newPos.y, targetY, speed * Time.deltaTime);
        transform.position = newPos;
    }
}
