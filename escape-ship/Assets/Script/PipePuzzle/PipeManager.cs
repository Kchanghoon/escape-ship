using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public List<Pipe> allPipes;  // 퍼즐에 포함된 모든 파이프 리스트
    public Pipe startPipe;       // 시작 파이프
    public Pipe endPipe;         // 끝 파이프
    private bool reachedEndPipe = false;  // End 파이프에 도달했는지 여부 확인용 변수

    // 퍼즐이 해결되었는지 체크하는 함수
    public void CheckAllConnections()
    {
        // 모든 파이프를 체크하지 않은 상태로 초기화
        foreach (Pipe pipe in allPipes)
        {
            pipe.isChecked = false;
        }

        // End 파이프에 도달 여부 초기화
        reachedEndPipe = false;

        // 시작 파이프부터 연결된 모든 파이프를 체크
        CheckConnectedPipes(startPipe);

        // 퍼즐이 해결되었는지 확인
        if (reachedEndPipe)
        {
            Debug.Log("퍼즐 완료!");
            OnPuzzleComplete();
        }
        else
        {
            Debug.Log("퍼즐이 아직 연결되지 않았습니다.");
        }
    }

    // 시작 파이프에서 연결된 모든 파이프를 재귀적으로 체크하는 함수
    void CheckConnectedPipes(Pipe currentPipe)
    {
        currentPipe.isChecked = true;  // 현재 파이프를 체크 상태로 표시

        // 연결된 파이프들 탐색
        foreach (Pipe otherPipe in allPipes)
        {
            // 아직 체크되지 않고 연결된 파이프만 탐색
            if (!otherPipe.isChecked && currentPipe.IsConnected(otherPipe))
            {
                CheckConnectedPipes(otherPipe);  // 연결된 파이프도 재귀적으로 체크
            }
        }
    }




    // 퍼즐이 완료되었을 때 처리 (예: 문을 열거나, 다음 단계로 진행하는 로직)
    void OnPuzzleComplete()
    {
        Debug.Log("문이 열렸습니다!");
        // 퍼즐 완료 로직 추가 가능
    }
}
