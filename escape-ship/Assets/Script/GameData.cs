using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public Vector3 playerPosition;                 // �÷��̾� ��ġ
    public PlayerStats playerStats;                // �÷��̾��� ���� (���, ��Ʈ���� ��)
    public List<ItemDataExample> inventoryItems;   // �κ��丮�� �ִ� ������
    public float bgmVolume;                        // ���� ���� (BGM ����)
    public float effectVolume;                     // ���� ���� (ȿ���� ����)
    public float masterVolume;                     // ���� ���� (������ ����)
}

[Serializable]
public class PlayerStats
{
    public float oxygen;
    public float stress;
    public float maxOxygen;
    public float maxStress;
}
