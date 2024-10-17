using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeManager : Singleton<PipeManager>
{
    public List<Pipe> allPipes;  // ���� ���Ե� ��� ������ ����Ʈ
    public Pipe startPipe;       // ���� ������
    public Pipe endPipe;         // �� ������
    public GameObject panel;     // ���� �Ϸ� �� ��Ȱ��ȭ�� �г�
    public GameObject recoveryZone; // ���� �Ϸ� �� Ȱ��ȭ�� ȸ����
    public Canvas panelCanvas;  // �г��� Canvas ������Ʈ


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

    private List<Pipe> CheckAdjacencyPipesPipes(Pipe centerPipe)
    {
        List<Pipe> connectedPipes = new();
        foreach (Pipe pipe in centerPipe.AdjacencyPipes) //������ ������������ ��� ���������ΰ� �����ϰ� �ִ��� üũ
        {
            if (!pipe.isChecked && pipe.IsConnected(centerPipe)) connectedPipes.Add(pipe);
        }

        return connectedPipes;
    }

    public void CheckAllConnectedPipes()
    {
        allPipes.ForEach(x => x.isChecked = false);
        startPipe.isChecked = true;

        List<Pipe> beforeConnectedPipes = new List<Pipe>() { startPipe }; // ù ������ ���� ����������
        while (beforeConnectedPipes.Count != 0 || beforeConnectedPipes.Count > 10)
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
        // ȸ���� Ȱ��ȭ
        if (recoveryZone != null)
        {
            recoveryZone.SetActive(true);
            Debug.Log("ȸ������ Ȱ��ȭ�Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogWarning("ȸ������ �Ҵ���� �ʾҽ��ϴ�.");
        }
        // �г� ��Ȱ��ȭ
        if (panel != null)
        {
            panel.SetActive(false);
            Debug.Log("�г��� ��Ȱ��ȭ�Ǿ����ϴ�.");

            // Canvas �켱������ ������� ����
            if (panelCanvas != null)
            {
                panelCanvas.sortingOrder = 0;  // �⺻ ������ ����
                Debug.Log("�г� Canvas�� sortingOrder�� 0���� �����Ǿ����ϴ�.");
            }

            // Ŀ���� ����� ��� ����
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Debug.LogWarning("�г��� �Ҵ���� �ʾҽ��ϴ�.");
        }

      
    }


    // �г��� �� �� ȣ��Ǵ� �Լ� (�ʿ��� ��� ����)
    public void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Debug.Log("�г��� Ȱ��ȭ�Ǿ����ϴ�.");

            // Canvas �켱������ ���� ���� ����
            if (panelCanvas != null)
            {
                panelCanvas.sortingOrder = 9999;  
            }

            // Ŀ���� ���̰� ��� ����
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
