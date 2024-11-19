using TMPro;
using UnityEngine;

public class KeyBindingEntryUI : MonoBehaviour
{
    [SerializeField] private TMP_Text actionText;   // 액션 이름 표시
    [SerializeField] private TMP_Text keyText;      // 현재 키 이름 표시

    private KeyAction keyAction;                   // 변경 대상 KeyAction
    private System.Action<KeyAction, KeyCode> onKeyChanged; // 키 변경 콜백
    private bool isAwaitingKey = false;            // 키 입력 대기 상태

    // UI 초기화 메서드
    public void Initialize(KeyAction keyAction, KeyCode currentKey, System.Action<KeyAction, KeyCode> onKeyChangedCallback)
    {
        this.keyAction = keyAction;               // 현재 UI에 해당하는 KeyAction 저장
        actionText.text = keyAction.ToString();   // 액션 이름 표시
        keyText.text = currentKey.ToString();     // 현재 키 값 표시
        onKeyChanged = onKeyChangedCallback;      // KeyManager와 연결된 콜백 저장
    }

    // 버튼 클릭 시 호출
    public void OnClick()
    {
        if (!isAwaitingKey) // 현재 대기 상태가 아니면
        {
            isAwaitingKey = true; // 키 변경 대기 상태로 전환
            keyText.text = "Press any key..."; // 상태 표시
        }
    }

    private void Update()
    {
        if (isAwaitingKey && Input.anyKeyDown) // 대기 상태에서 키 입력 감지
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    isAwaitingKey = false; // 대기 상태 종료
                    keyText.text = keyCode.ToString(); // 새 키 이름 표시
                    onKeyChanged?.Invoke(keyAction, keyCode); // KeyManager에 변경 요청 전달
                    break;
                }
            }
        }
    }
}
