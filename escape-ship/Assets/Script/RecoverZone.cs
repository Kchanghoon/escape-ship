using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverZone : MonoBehaviour
{
    private bool playerInRange = false;  // 플레이어가 범위 안에 있는지 여부
    private PlayerState playerState;

    // 플레이어가 범위에 들어왔을 때 호출
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerState = other.GetComponent<PlayerState>();  // PlayerState 스크립트 가져오기
            if (playerState != null)
            {
                playerInRange = true;  // 플레이어가 범위 안에 있음을 기록
                StartCoroutine(AdjustPlayerStats());
            }
        }
    }

    // 플레이어가 범위를 벗어났을 때 호출
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;  // 플레이어가 범위 밖으로 나갔음을 기록
            StopCoroutine(AdjustPlayerStats());
        }
    }

    // 산소와 스트레스 수치를 조정하는 코루틴
    private IEnumerator AdjustPlayerStats()
    {
        while (playerInRange && playerState != null)
        {
            // 초당 산소 3씩 증가
            playerState.IncreaseOxygen(3f * Time.deltaTime);

            // 초당 스트레스 1씩 감소
            playerState.DecreaseStress(1f * Time.deltaTime);

            yield return null;  // 한 프레임 대기
        }
    }
}
