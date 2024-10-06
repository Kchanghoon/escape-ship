using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public List<Pipe> allPipes;  // 퍼즐에 포함된 모든 파이프 리스트

    public void CheckAllConnections()
    {
        bool allConnected = true;

        foreach (Pipe pipe in allPipes)
        {
            // 파이프의 연결을 다른 파이프들과 확인
            foreach (Pipe otherPipe in allPipes)
            {
                if (pipe != otherPipe)
                {
                    if (!pipe.IsConnected(otherPipe))
                    {
                        allConnected = false;
                        break;
                    }
                }
            }
            if (!allConnected) break;
        }

        if (allConnected)
        {
            Debug.Log("퍼즐 완료!");
            // 퍼즐 완료 처리
            OnPuzzleComplete();
        }
        else
        {
            Debug.Log("퍼즐이 아직 연결되지 않았습니다.");
        }
    }

    void OnPuzzleComplete()
    {
        // 퍼즐이 완료되었을 때의 처리 (예: 문을 열거나, 게임을 진행시키는 로직)
    }
}
