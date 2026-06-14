using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float speed = 2f;
    private bool movingToPos2 = true;

    void Start() { startPos = transform.position; }

    void Update()
    {
        Vector3 target = movingToPos2 ? endPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            movingToPos2 = !movingToPos2; // 방향 전환
        }
    }
}
