using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerState : MonoBehaviour
{
    // 플레이어 상태 스탯 변수
    [SerializeField] private float stress;  // 스트레스 수치
    [SerializeField] private float oxygen;  // 산소 수치

    // 최대값, 최소값을 지정해 줄 수 있다.
    [SerializeField] private float maxStress = 100f;
    [SerializeField] private float maxOxygen = 100f;

    private bool isInRecoverZone = false;  // 플레이어가 회복존 안에 있는지 여부
    private bool isOxygenDepleted = false;  // 산소가 다 떨어졌는지 여부

    // 외부에서 산소와 스트레스 값에 접근할 수 있도록 public 프로퍼티 추가
    public float Stress => stress;  // 스트레스 값 읽기
    public float Oxygen => oxygen;  // 산소 값 읽기
    public float MaxStress => maxStress;  // 최대 스트레스 값 읽기
    public float MaxOxygen => maxOxygen;  // 최대 산소 값 읽기

    // Start는 게임 시작 시 호출
    private void Start()
    {
        stress = 0f;
        oxygen = maxOxygen;

        KeyManager.Instance.keyDic[KeyAction.Jump] += OnJump;
        KeyManager.Instance.keyDic[KeyAction.Run] += OnRun;
        KeyManager.Instance.keyDic[KeyAction.Play] += OnAction;  // E키 할당 (가정: KeyAction.Action이 E키로 설정됨)
    }

    private void OnJump()
    {
        DecreaseOxygen(3f);  // 산소를 3만큼 감소
    }

    private void OnRun()
    {
        DecreaseOxygen(Time.deltaTime * 2f); // 산소 2초마다 감소.
    }

    private void OnAction()
    {
        IncreaseStress(5f);  // E 키를 누르면 스트레스가 5 증가
    }

    // Update는 매 프레임마다 호출
    void Update()
    {
        UpdateOxygen();
        UpdateStress();
    }

    // 스트레스를 업데이트하는 메서드
    void UpdateStress()
    {
        if (isOxygenDepleted && !isInRecoverZone)
        {
            // 산소가 다 떨어지고 회복존에 있지 않으면 초당 스트레스 5 증가
            IncreaseStress(5f * Time.deltaTime);
        }
    }

    // 산소를 업데이트하는 메서드
    void UpdateOxygen()
    {
        if (oxygen > 0)
        {
            // 산소가 남아있으면 자연 감소
            oxygen -= Time.deltaTime * 1f;  // 1f는 감소 속도
            isOxygenDepleted = false;
        }
        else
        {
            // 산소가 0 이하로 떨어지지 않도록 제한하고 산소가 소진된 상태로 기록
            oxygen = 0;
            isOxygenDepleted = true;
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

    // 회복존에 들어갔을 때 호출
    public void EnterRecoverZone()
    {
        isInRecoverZone = true;  // 회복존에 있는 상태로 기록
    }

    // 회복존에서 나왔을 때 호출
    public void ExitRecoverZone()
    {
        isInRecoverZone = false;  // 회복존에서 나간 상태로 기록
    }
}
