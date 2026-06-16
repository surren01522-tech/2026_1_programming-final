using UnityEngine;

public class FinishZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 골인 지점에 닿은 물체가 플레이어(유령)인지 확인
        if (other.CompareTag("Player") || other.name == "Ghost")
        {

            Debug.Log("골인 지점에 플레이어가 충돌했습니다!");
            // GameManager에게 게임이 끝났음(성공)을 알려줍니다.
            if (GameManager.instance != null)
            {
                GameManager.instance.TriggerGameFinish();
            }
        }
    }
}
