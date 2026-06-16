using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 15f;

    private void OnTriggerEnter(Collider other)
    {
        // 부딪힌 물체의 태그가 Player이거나 이름이 Ghost인지 확인
        if (other.CompareTag("Player") || other.name == "Ghost")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                // 플레이어 스크립트에게 튕겨 나가라고 명령 전달!
                player.AddJumpForce(jumpForce);
            }
        }
    }
}