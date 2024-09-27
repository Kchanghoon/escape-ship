using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public Vector3 playerPosition;                 // 플레이어 위치
    public PlayerStats playerStats;                // 플레이어의 스탯 (산소, 스트레스 등)
    public List<ItemDataExample> inventoryItems;   // 인벤토리에 있는 아이템
    public float bgmVolume;                        // 사운드 설정 (BGM 볼륨)
    public float effectVolume;                     // 사운드 설정 (효과음 볼륨)
    public float masterVolume;                     // 사운드 설정 (마스터 볼륨)
}

[Serializable]
public class PlayerStats
{
    public float oxygen;
    public float stress;
    public float maxOxygen;
    public float maxStress;
}
