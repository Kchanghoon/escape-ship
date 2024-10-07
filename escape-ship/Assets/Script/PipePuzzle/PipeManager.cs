using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeManager : Singleton<PipeManager>
{
    public List<Pipe> allPipes;  // 퍼즐에 포함된 모든 파이프 리스트
    public Pipe startPipe;       // 시작 파이프
    public Pipe endPipe;         // 끝 파이프
    private bool reachedEndPipe = false;  // End 파이프에 도달했는지 여부 확인용 변수

    [ContextMenu("Test")]
    private void StartPipe()
    {
        // 게임이 시작될 때 startPipe를 체크 상태로 설정
        if (startPipe != null)
        {
            startPipe.isChecked = true;
            Debug.Log($"{startPipe.name}이(가) 초기화됨 (isChecked = true)");
        }
        else
        {
            Debug.LogError("startPipe가 설정되지 않았습니다!");
        }
        CheckAllConnectedPipes();
    }

    //public void CheckAllConnections()
    //{
    //    Debug.Log("CheckAllConnections 호출됨");

    //    // 모든 파이프를 체크되지 않은 상태로 초기화
    //    foreach (Pipe pipe in allPipes)
    //    {
    //        pipe.isChecked = false;
    //    }

    //    // 시작 파이프부터 연결된 모든 파이프를 체크
    //    if (startPipe != null)
    //    {
    //        Debug.Log("시작 파이프에서 연결 상태를 확인합니다.");
    //        CheckAllConnectedPipes();
    //        if (endPipe.isChecked) OnPuzzleComplete();
    //    }
    //    else
    //    {
    //        Debug.LogError("startPipe가 설정되지 않았습니다!");
    //    }
    //}

    private List<Pipe> CheckAdjacencyPipesPipes(Pipe centerPipe)
    {
        List<Pipe> connectedPipes = new();
        foreach (Pipe pipe in centerPipe.AdjacencyPipes) //인접한 파이프라인이 가운데 파이프라인과 인접하고 있는지 체크
        {
            if (!pipe.isChecked && pipe.IsConnected(centerPipe)) connectedPipes.Add(pipe);
        }

        return connectedPipes;
    }

    public  void CheckAllConnectedPipes()
    {
        allPipes.ForEach(x => x.isChecked = false);
        startPipe.isChecked = true;
        
        List<Pipe> beforeConnectedPipes = new List<Pipe>() { startPipe }; // 첫 연결점 시작 파이프라인
        while(beforeConnectedPipes.Count != 0 || beforeConnectedPipes.Count > 10)
        {
            if (beforeConnectedPipes.Count > 10) break;

            List<Pipe> connectPipeLine = new();
            foreach (Pipe pipe in beforeConnectedPipes)
            {
                connectPipeLine.AddRange(CheckAdjacencyPipesPipes(pipe));
            }

            beforeConnectedPipes = connectPipeLine.Except(beforeConnectedPipes).ToList(); //이전에 연결된 거 빼고 연결되어있는 파이프라인
        }

        if (endPipe.isChecked) OnPuzzleComplete();

        //Debug.Log($"CheckConnectedPipes: {currentPipe.name}");
        //if (currentPipe.isChecked) return;

        //currentPipe.isChecked = true;
        //Debug.Log($"{currentPipe.name}이(가) 체크되었습니다.");

        //foreach (Pipe otherPipe in allPipes)
        //{
        //    //Debug.Log($"다른 파이프 확인 중: {otherPipe.name}");
        //    if (!otherPipe.isChecked && currentPipe.IsConnected(otherPipe))
        //    {
        //        CheckConnectedPipes(otherPipe);
        //    }
        //}
    }

    void UncheckDisconnectedPipes(Pipe currentPipe)
    {
        if (!currentPipe.isChecked) return;  // 이미 체크 해제된 경우 무시

        currentPipe.isChecked = false;
        Debug.Log($"{currentPipe.name}이(가) 체크 해제되었습니다.");

        foreach (Pipe otherPipe in allPipes)
        {
            // 연결된 파이프들 중 체크 상태였던 파이프들을 해제
            if (otherPipe.isChecked && currentPipe.IsConnected(otherPipe))
            {
                UncheckDisconnectedPipes(otherPipe);  // 재귀적으로 해제
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
