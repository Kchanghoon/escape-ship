using UnityEngine;

public class NPCLook : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform
    public float lookAtDistance = 5.0f;  // NPC가 플레이어를 쳐다보는 거리
    public float rotationSpeed = 5.0f;  // 회전 속도
    public float lookDuration = 3.0f;   // NPC가 플레이어를 쳐다보는 시간

    private Quaternion originalRotation;  // NPC의 원래 회전값
    private bool isLookingAtPlayer = false;  // NPC가 플레이어를 쳐다보는지 여부
    private float lookTimer = 0.0f;  // 플레이어를 쳐다본 후 경과한 시간

    void Start()
    {
        // 초기 회전값 저장
        originalRotation = transform.rotation;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= lookAtDistance)
        {
            // 플레이어를 바라보도록 설정
            LookAtPlayer();
        }
        else
        {
            // 플레이어를 쳐다보지 않을 때 원래 회전값으로 복귀
            ReturnToOriginalRotation();
        }
    }

    void LookAtPlayer()
    {
        // NPC가 플레이어를 쳐다보는 로직
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        isLookingAtPlayer = true;
        lookTimer = 0.0f;  // 쳐다본 시간을 초기화
    }

    void ReturnToOriginalRotation()
    {
        if (isLookingAtPlayer)
        {
            lookTimer += Time.deltaTime;

            if (lookTimer >= lookDuration)
            {
                // 원래 회전값으로 천천히 돌아가게 함
                transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * rotationSpeed);

                // 원래 회전값에 도달하면 다시는 바라보지 않음
                if (Quaternion.Angle(transform.rotation, originalRotation) < 0.1f)
                {
                    isLookingAtPlayer = false;
                }
            }
        }
    }
}
