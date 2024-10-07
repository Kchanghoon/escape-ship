using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeManager : Singleton<PipeManager>
{
    public List<Pipe> allPipes;  // ���� ���Ե� ��� ������ ����Ʈ
    public Pipe startPipe;       // ���� ������
    public Pipe endPipe;         // �� ������
    private bool reachedEndPipe = false;  // End �������� �����ߴ��� ���� Ȯ�ο� ����

    [ContextMenu("Test")]
    private void StartPipe()
    {
        // ������ ���۵� �� startPipe�� üũ ���·� ����
        if (startPipe != null)
        {
            startPipe.isChecked = true;
            Debug.Log($"{startPipe.name}��(��) �ʱ�ȭ�� (isChecked = true)");
        }
        else
        {
            Debug.LogError("startPipe�� �������� �ʾҽ��ϴ�!");
        }
        CheckAllConnectedPipes();
    }

    //public void CheckAllConnections()
    //{
    //    Debug.Log("CheckAllConnections ȣ���");

    //    // ��� �������� üũ���� ���� ���·� �ʱ�ȭ
    //    foreach (Pipe pipe in allPipes)
    //    {
    //        pipe.isChecked = false;
    //    }

    //    // ���� ���������� ����� ��� �������� üũ
    //    if (startPipe != null)
    //    {
    //        Debug.Log("���� ���������� ���� ���¸� Ȯ���մϴ�.");
    //        CheckAllConnectedPipes();
    //        if (endPipe.isChecked) OnPuzzleComplete();
    //    }
    //    else
    //    {
    //        Debug.LogError("startPipe�� �������� �ʾҽ��ϴ�!");
    //    }
    //}

    private List<Pipe> CheckAdjacencyPipesPipes(Pipe centerPipe)
    {
        List<Pipe> connectedPipes = new();
        foreach (Pipe pipe in centerPipe.AdjacencyPipes) //������ ������������ ��� ���������ΰ� �����ϰ� �ִ��� üũ
        {
            if (!pipe.isChecked && pipe.IsConnected(centerPipe)) connectedPipes.Add(pipe);
        }

        return connectedPipes;
    }

    public  void CheckAllConnectedPipes()
    {
        allPipes.ForEach(x => x.isChecked = false);
        startPipe.isChecked = true;
        
        List<Pipe> beforeConnectedPipes = new List<Pipe>() { startPipe }; // ù ������ ���� ����������
        while(beforeConnectedPipes.Count != 0 || beforeConnectedPipes.Count > 10)
        {
            if (beforeConnectedPipes.Count > 10) break;

            List<Pipe> connectPipeLine = new();
            foreach (Pipe pipe in beforeConnectedPipes)
            {
                connectPipeLine.AddRange(CheckAdjacencyPipesPipes(pipe));
            }

            beforeConnectedPipes = connectPipeLine.Except(beforeConnectedPipes).ToList(); //������ ����� �� ���� ����Ǿ��ִ� ����������
        }

        if (endPipe.isChecked) OnPuzzleComplete();

        //Debug.Log($"CheckConnectedPipes: {currentPipe.name}");
        //if (currentPipe.isChecked) return;

        //currentPipe.isChecked = true;
        //Debug.Log($"{currentPipe.name}��(��) üũ�Ǿ����ϴ�.");

        //foreach (Pipe otherPipe in allPipes)
        //{
        //    //Debug.Log($"�ٸ� ������ Ȯ�� ��: {otherPipe.name}");
        //    if (!otherPipe.isChecked && currentPipe.IsConnected(otherPipe))
        //    {
        //        CheckConnectedPipes(otherPipe);
        //    }
        //}
    }

    void UncheckDisconnectedPipes(Pipe currentPipe)
    {
        if (!currentPipe.isChecked) return;  // �̹� üũ ������ ��� ����

        currentPipe.isChecked = false;
        Debug.Log($"{currentPipe.name}��(��) üũ �����Ǿ����ϴ�.");

        foreach (Pipe otherPipe in allPipes)
        {
            // ����� �������� �� üũ ���¿��� ���������� ����
            if (otherPipe.isChecked && currentPipe.IsConnected(otherPipe))
            {
                UncheckDisconnectedPipes(otherPipe);  // ��������� ����
            }
        }
    }


    // ������ �Ϸ�Ǿ��� �� ó�� (��: ���� ���ų�, ���� �ܰ�� �����ϴ� ����)
    void OnPuzzleComplete()
    {
        Debug.Log("���� ���Ƚ��ϴ�!");
        // ���� �Ϸ� ���� �߰� ����
    }
}
