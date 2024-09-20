using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // 플레이어 상태 스탯 변수
    public float stress;  // 스트레스 수치
    public float oxygen;  // 산소 수치

    // 최대값, 최소값을 지정해 줄 수 있다.
    public float maxStress = 100f;
    public float maxOxygen = 100f;

    // Start는 게임 시작 시 호출
    void Start()
    {
        // 초기 값 설정 (원하는 값으로 초기화할 수 있다)
        stress = 0f;
        oxygen = maxOxygen;
    }

    // Update는 매 프레임마다 호출
    void Update()
    {
        // 스트레스 또는 산소가 변하는 로직을 여기에 구현 가능
        UpdateStress();
        UpdateOxygen();
    }

    // 스트레스를 업데이트하는 메서드
    void UpdateStress()
    {
        // 임의로 스트레스가 증가하는 로직 (예시)
        if (stress < maxStress)
        {
            stress += Time.deltaTime * 2f;  // 2f는 증가 속도
        }
        else
        {
            stress = maxStress;  // 최대 스트레스 제한
        }
    }

    // 산소를 업데이트하는 메서드
    void UpdateOxygen()
    {
        // 임의로 산소가 감소하는 로직 (예시)
        if (oxygen > 0)
        {
            oxygen -= Time.deltaTime * 1f;  // 1f는 감소 속도
        }
        else
        {
            oxygen = 0;  // 산소가 0 이하로 떨어지지 않도록 제한
        }
    }

    // 외부에서 스트레스에 접근하는 메서드
    public void IncreaseStress(float amount)
    {
        stress = Mathf.Clamp(stress + amount, 0, maxStress);
    }

    public void DecreaseStress(float amount)
    {
        stress = Mathf.Clamp(stress - amount, 0, maxStress);
    }

    // 외부에서 산소에 접근하는 메서드
    public void IncreaseOxygen(float amount)
    {
        oxygen = Mathf.Clamp(oxygen + amount, 0, maxOxygen);
    }

    public void DecreaseOxygen(float amount)
    {
        oxygen = Mathf.Clamp(oxygen - amount, 0, maxOxygen);
    }
}
