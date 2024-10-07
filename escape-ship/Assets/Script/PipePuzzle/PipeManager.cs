using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public List<Pipe> allPipes;  // ���� ���Ե� ��� ������ ����Ʈ
    public Pipe startPipe;       // ���� ������
    public Pipe endPipe;         // �� ������
    private bool reachedEndPipe = false;  // End �������� �����ߴ��� ���� Ȯ�ο� ����

    // ������ �ذ�Ǿ����� üũ�ϴ� �Լ�
    public void CheckAllConnections()
    {
        // ��� �������� üũ���� ���� ���·� �ʱ�ȭ
        foreach (Pipe pipe in allPipes)
        {
            pipe.isChecked = false;
        }

        // End �������� ���� ���� �ʱ�ȭ
        reachedEndPipe = false;

        // ���� ���������� ����� ��� �������� üũ
        CheckConnectedPipes(startPipe);

        // ������ �ذ�Ǿ����� Ȯ��
        if (reachedEndPipe)
        {
            Debug.Log("���� �Ϸ�!");
            OnPuzzleComplete();
        }
        else
        {
            Debug.Log("������ ���� ������� �ʾҽ��ϴ�.");
        }
    }

    // ���� ���������� ����� ��� �������� ��������� üũ�ϴ� �Լ�
    void CheckConnectedPipes(Pipe currentPipe)
    {
        currentPipe.isChecked = true;  // ���� �������� üũ ���·� ǥ��

        // ����� �������� Ž��
        foreach (Pipe otherPipe in allPipes)
        {
            // ���� üũ���� �ʰ� ����� �������� Ž��
            if (!otherPipe.isChecked && currentPipe.IsConnected(otherPipe))
            {
                CheckConnectedPipes(otherPipe);  // ����� �������� ��������� üũ
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
