using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;
using System.Drawing;

public class Pipe : MonoBehaviour, IPointerClickHandler
{
    public PipeType pipeType;  // 파이프 종류
    public bool isChecked = false;  // 파이프가 체크되었는지 여부
    private int currentRotation = 0;
    public RectTransform rectTransform;

    public Vector2Int[] connectionPoints;

    private Vector2Int[] defaultConnectionPoints;

    [SerializeField] Vector2Int coord;
    [SerializeField] List<Pipe> adjacencyPipes = new();
    public List<Pipe> AdjacencyPipes { get => adjacencyPipes; }
    List<Vector2Int> adjacencyPosList = new List<Vector2Int>();

    private void SetAdjacencyPipe()
    {
        adjacencyPipes.Clear();
        adjacencyPosList = new List<Vector2Int>()
        {
            coord + Vector2Int.right,  coord + Vector2Int.left, coord + Vector2Int.up, coord + Vector2Int.down
        };

        var allPipes = PipeManager.Instance.allPipes;

        foreach(var pos in adjacencyPosList)
        {
            var pipe = allPipes.Find(x => x.coord == pos);
            if(pipe != null) adjacencyPipes.Add(pipe);
        }
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SetAdjacencyPipe();
        currentRotation = Mathf.RoundToInt(rectTransform.eulerAngles.z);

        SetDefaultConnectionPoints();
        UpdateConnectionPoints();
        Debug.Log("Pipe initialized");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RotatePipe();
    }

    void RotatePipe()
    {
        currentRotation = (currentRotation + 90) % 360;
        rectTransform.DORotate(new Vector3(0, 0, currentRotation), 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(UpdateConnectionPoints);
    }

    void SetDefaultConnectionPoints()
    {
        if (pipeType == PipeType.Start)
        {
            defaultConnectionPoints = new Vector2Int[1] { new Vector2Int(1, 0) };
        }
        else if (pipeType == PipeType.End)
        {
            defaultConnectionPoints = new Vector2Int[1] { new Vector2Int(1, 0) };
        }
        else if (pipeType == PipeType.Straight)
        {
            defaultConnectionPoints = new Vector2Int[2] { new Vector2Int(0, 1), new Vector2Int(0, -1) };
        }
        else if (pipeType == PipeType.Corner)
        {
            defaultConnectionPoints = new Vector2Int[2] { new Vector2Int(1, 0), new Vector2Int(0, -1) };
        }
        else if (pipeType == PipeType.TShape)
        {
            defaultConnectionPoints = new Vector2Int[3] { new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1) };
        }
    }

    void UpdateConnectionPoints()
    {
        connectionPoints = new Vector2Int[defaultConnectionPoints.Length];

        for (int i = 0; i < connectionPoints.Length; i++)
        {
            connectionPoints[i] = RotateVector(defaultConnectionPoints[i], currentRotation);
        }

        // 업데이트된 연결 포인트를 출력하여 제대로 설정되었는지 확인
        //Debug.Log($"{gameObject.name}의 업데이트된 connectionPoints: {string.Join(", ", connectionPoints)}");
        PipeManager.Instance.CheckAllConnectedPipes();
    }



    Vector2Int RotateVector(Vector2Int vector, int angle)
    {
        switch (angle)
        {
            case 90:
                return new Vector2Int(-vector.y, vector.x);
            case 180:
                return new Vector2Int(-vector.x, -vector.y);
            case 270:
                return new Vector2Int(vector.y, -vector.x);
            default:
                return vector;
        }
    }

    public bool IsConnected(Pipe otherPipe)
    {
        //Debug.Log($"IsConnected: {gameObject.name}과 {otherPipe.name} 연결 확인 중");

        if (!adjacencyPipes.Contains(otherPipe)) return false;

        foreach (var myPoint in connectionPoints) // 연결된 포인트
        {
            if(otherPipe.coord == coord + myPoint)
            {
                foreach (var other in otherPipe.connectionPoints)
                {
                    if (coord == otherPipe.coord + other)
                    {
                        isChecked = true;
                        return true;
                    }
                }
            }

        }

        return false;
    }

    public void CheckConnection(Pipe otherPipe)
    {
        // 현재 파이프가 체크되지 않았고, 다른 파이프가 이미 체크된 경우 연결을 확인
        if (!isChecked && otherPipe.isChecked && IsConnected(otherPipe))
        {
            isChecked = true;
            //Debug.Log($"{gameObject.name}이(가) {otherPipe.gameObject.name}과 연결되어 isChecked = true로 설정됨");
        }
    }
}
