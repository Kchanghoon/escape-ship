using UnityEngine;

public class NPCLook : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform
    public float lookAtDistance = 5.0f;  // NPC�� �÷��̾ �Ĵٺ��� �Ÿ�
    public float rotationSpeed = 5.0f;  // ȸ�� �ӵ�
    public float lookDuration = 3.0f;   // NPC�� �÷��̾ �Ĵٺ��� �ð�

    private Quaternion originalRotation;  // NPC�� ���� ȸ����
    private bool isLookingAtPlayer = false;  // NPC�� �÷��̾ �Ĵٺ����� ����
    private float lookTimer = 0.0f;  // �÷��̾ �Ĵٺ� �� ����� �ð�

    void Start()
    {
        // �ʱ� ȸ���� ����
        originalRotation = transform.rotation;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= lookAtDistance)
        {
            // �÷��̾ �ٶ󺸵��� ����
            LookAtPlayer();
        }
        else
        {
            // �÷��̾ �Ĵٺ��� ���� �� ���� ȸ�������� ����
            ReturnToOriginalRotation();
        }
    }

    void LookAtPlayer()
    {
        // NPC�� �÷��̾ �Ĵٺ��� ����
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        isLookingAtPlayer = true;
        lookTimer = 0.0f;  // �Ĵٺ� �ð��� �ʱ�ȭ
    }

    void ReturnToOriginalRotation()
    {
        if (isLookingAtPlayer)
        {
            lookTimer += Time.deltaTime;

            if (lookTimer >= lookDuration)
            {
                // ���� ȸ�������� õõ�� ���ư��� ��
                transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * rotationSpeed);

                // ���� ȸ������ �����ϸ� �ٽô� �ٶ��� ����
                if (Quaternion.Angle(transform.rotation, originalRotation) < 0.1f)
                {
                    isLookingAtPlayer = false;
                }
            }
        }
    }
}
