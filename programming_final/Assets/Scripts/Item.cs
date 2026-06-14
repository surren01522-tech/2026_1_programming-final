using UnityEngine;

public class Item : MonoBehaviour
{
    public int scoreValue = 10; // 아이템당 점수

    void Update()
    {
        transform.Rotate(new Vector3(0, 50 * Time.deltaTime, 0)); // 회전 효과
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManager에 점수 추가 요청 (3단계에서 서술)
            GameManager.instance.AddScore(scoreValue);
            Destroy(gameObject); // 아이템 삭제
        }
    }
}
