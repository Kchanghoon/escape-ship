using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public List<Pipe> allPipes;  // ���� ���Ե� ��� ������ ����Ʈ

    public void CheckAllConnections()
    {
        bool allConnected = true;

        foreach (Pipe pipe in allPipes)
        {
            // �������� ������ �ٸ� ��������� Ȯ��
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
            Debug.Log("���� �Ϸ�!");
            // ���� �Ϸ� ó��
            OnPuzzleComplete();
        }
        else
        {
            Debug.Log("������ ���� ������� �ʾҽ��ϴ�.");
        }
    }

    void OnPuzzleComplete()
    {
        // ������ �Ϸ�Ǿ��� ���� ó�� (��: ���� ���ų�, ������ �����Ű�� ����)
    }
}
