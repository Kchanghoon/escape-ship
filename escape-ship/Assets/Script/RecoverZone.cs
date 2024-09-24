using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverZone : MonoBehaviour
{
    private bool playerInRange = false;  // �÷��̾ ���� �ȿ� �ִ��� ����
    private PlayerState playerState;

    // �÷��̾ ������ ������ �� ȣ��
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerState = other.GetComponent<PlayerState>();  // PlayerState ��ũ��Ʈ ��������
            if (playerState != null)
            {
                playerInRange = true;  // �÷��̾ ���� �ȿ� ������ ���
                StartCoroutine(AdjustPlayerStats());
            }
        }
    }

    // �÷��̾ ������ ����� �� ȣ��
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;  // �÷��̾ ���� ������ �������� ���
            StopCoroutine(AdjustPlayerStats());
        }
    }

    // ��ҿ� ��Ʈ���� ��ġ�� �����ϴ� �ڷ�ƾ
    private IEnumerator AdjustPlayerStats()
    {
        while (playerInRange && playerState != null)
        {
            // �ʴ� ��� 3�� ����
            playerState.IncreaseOxygen(3f * Time.deltaTime);

            // �ʴ� ��Ʈ���� 1�� ����
            playerState.DecreaseStress(1f * Time.deltaTime);

            yield return null;  // �� ������ ���
        }
    }
}
