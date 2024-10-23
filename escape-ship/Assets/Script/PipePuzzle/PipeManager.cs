using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeManager : Singleton<PipeManager>
{
    public List<Pipe> allPipes;  // 퍼즐에 포함된 모든 파이프 리스트
    public Pipe startPipe;       // 시작 파이프
    public Pipe endPipe;         // 끝 파이프
    public GameObject panel;     // 퍼즐 완료 시 비활성화할 패널
    public GameObject recoveryZone; // 퍼즐 완료 시 활성화할 회복존
    public Canvas panelCanvas;  // 패널의 Canvas 컴포넌트
    [SerializeField] float tiem;

    private bool reachedEndPipe = false;  // End 파이프에 도달했는지 여부 확인용 변수

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

    private List<Pipe> CheckAdjacencyPipesPipes(Pipe centerPipe)
    {
        List<Pipe> connectedPipes = new();
        foreach (Pipe pipe in centerPipe.AdjacencyPipes) //인접한 파이프라인이 가운데 파이프라인과 인접하고 있는지 체크
        {
            if (!pipe.isChecked && pipe.IsConnected(centerPipe)) connectedPipes.Add(pipe);
        }

        return connectedPipes;
    }

    public void CheckAllConnectedPipes()
    {
        allPipes.ForEach(x => x.isChecked = false);
        startPipe.isChecked = true;

        List<Pipe> beforeConnectedPipes = new List<Pipe>() { startPipe }; // 첫 연결점 시작 파이프라인
        while (beforeConnectedPipes.Count != 0 || beforeConnectedPipes.Count > 10)
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
    }

    async void OnPuzzleComplete()
    {
        // 패널 비활성화
        if (panel != null)
        {
            CloseBtn();
        }

        // 회복존 활성화
        if (recoveryZone != null)
        {
            recoveryZone.SetActive(true);
            Debug.Log("회복존이 활성화되었습니다.");
            await UniTask.Delay((int)(tiem * 1000));
            recoveryZone.SetActive(false);
            Debug.Log("회복존이 비활성화");
        }
        else
        {
            Debug.LogWarning("회복존이 할당되지 않았습니다.");
        }
    }

    // 패널을 열 때 호출되는 함수 (필요한 경우 구현)
    public void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Debug.Log("패널이 활성화되었습니다.");

            Time.timeScale = 0;  // 게임 일시정지
            // Canvas 우선순위를 가장 높게 설정
            if (panelCanvas != null)
            {
                panelCanvas.sortingOrder = 999;
            }
            // 커서 잠금 해제
            MouseCam.Instance.SetCursorState(false);
        }
    }

    public void CloseBtn()
    {
        panelCanvas.sortingOrder = 0; // 원래 순서로 복원
        Time.timeScale = 1;  // 게임 재개
        panel.SetActive(false);
        // 커서 잠금
        MouseCam.Instance.SetCursorState(true);
    }

    public void RandomPipe()
    {
        foreach (Pipe otherPipe in allPipes)
        {
            float randomRotaionZ = Random.Range(0, 4) * 90f;
            otherPipe.transform.localRotation = Quaternion.Euler(0, 0, randomRotaionZ);
        }

        CheckAllConnectedPipes();
    }
}
