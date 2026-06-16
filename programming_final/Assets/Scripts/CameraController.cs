using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;           // 바라볼 대상 (유령 캐릭터)
    public float mouseSensitivity = 100f; // 마우스 민감도
    public float distanceFromTarget = 5f; // 캐릭터와 카메라 사이의 거리

    public float yMinLimit = -20f;     // 아래로 내려다보는 각도 제한
    public float yMaxLimit = 80f;      // 위로 올려다보는 각도 제한

    private float rotationX = 0f;      // 마우스 X축 이동량에 따른 Y축 회전값
    private float rotationY = 0f;      // 마우스 Y축 이동량에 따른 X축 회전값

    void Start()
    {
        // 게임 플레이 중 마우스 커서를 화면 중앙에 가두고 숨깁니다. (ESC를 누르면 풀림)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 시작할 때 현재 카메라의 초기 회전각을 동기화합니다.
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 마우스의 움직임(마우스 델타 값)을 읽어옵니다.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX += mouseX;
        rotationY -= mouseY; // 마우스를 위로 올리면 카메라가 위를 봐야 하므로 빼줍니다.

        // 화면이 까뒤집히는 것을 막기 위해 위아래 회전 각도를 제한합니다.
        rotationY = Mathf.Clamp(rotationY, yMinLimit, yMaxLimit);

        // 계산된 회전 값을 쿼터니언으로 변환합니다.
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);

        // 캐릭터의 위치에서 '카메라 회전 방향의 뒤쪽'으로 거리만큼 떨어진 위치를 계산합니다.
        Vector3 position = target.position - (rotation * Vector3.forward * distanceFromTarget);

        // 카메라의 위치와 회전 각도를 최종 적용합니다.
        transform.rotation = rotation;
        transform.position = position;
    }
}