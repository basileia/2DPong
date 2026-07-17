using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform topWall;
    [SerializeField] private Transform bottomWall;
    [SerializeField] private float padding = 0.5f;

    private float minY;
    private float maxY;

    private void Start()
    {
        maxY = topWall.position.y - padding;
        minY = bottomWall.position.y + padding;
    }
    private void Update()
    {
        float input = Input.GetAxisRaw("Vertical");
        Move(input);
    }

    private void Move(float input)
    {
        Vector3 direction = Vector3.up * input;
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }
}