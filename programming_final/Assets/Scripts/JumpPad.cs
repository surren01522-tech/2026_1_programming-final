using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 15f; // 점프 힘

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 캐릭터의 리지드바디나 CharacterController를 찾아 위로 밀어줍니다.
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 속도를 초기화하고 위쪽으로 힘을 가함
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
