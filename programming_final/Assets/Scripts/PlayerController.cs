using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;         //캐릭터 이동속도
    public float jumpHeight = 2f;        //캐릭터 점프 높이
    public float gravity = -20f;         //캐릭터 중력 가속도

    public Transform cameraTransform;    //카메라 방향을 기준으로 움직이게 하기 위한 카메라 정보 저장 변수

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();       //캐릭터의 CharacterController 컴포넌트를 가져와서 controller 변수에 할당
        animator = GetComponent<Animator>();                    //캐릭터의 Animator 컴포넌트를 가져와서 animator 변수에 할당

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;            //카메라 정보가 할당되지 않았을 때 메인 카메라의 Transform을 자동으로 할당
    }

    void Update()
    {
        isGrounded = controller.isGrounded;            

        animator.SetBool("Grounded", isGrounded);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;                     //땅에 닿았을 때 캐릭터가 땅에 붙어 있도록 하기 위해 y축 속도를 약간 음수로 설정

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        move.y = 0f;
        move.Normalize();                         //입력 방향을 정규화하여 대각선 이동 시에도 일정한 속도로 이동하도록 함

        animator.SetBool("Run", move.magnitude > 0.1f);

        if (move.magnitude > 0.1f)
        {
            controller.Move(move * moveSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(move),
                10f * Time.deltaTime                     //캐릭터가 이동하는 방향으로 부드럽게 회전하도록 Slerp를 사용하여 회전
            );
        }

        if (Input.GetButtonDown("Jump") && isGrounded)        //바닥에서 스페이스 클릭 시 점프
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);      //필요한 속도 계산
        }

        velocity.y += gravity * Time.deltaTime;                    //캐릭터가 다시 땅에 떨어지도록 함
        controller.Move(velocity * Time.deltaTime);             //중력과 점프 속도를 적용하여 캐릭터를 이동시키는 부분
    }

    public void AddJumpForce(float force)
    {
        velocity.y = force;
    }
}
