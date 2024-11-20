using TMPro;
using UnityEngine;

public class KeyBindingEntryUI : MonoBehaviour
{
    [SerializeField] private TMP_Text actionText;   // 액션 이름
    [SerializeField] private TMP_Text keyText;      // 현재 키 이름

    [SerializeField] private KeyAction keyAction;  // 이 UI가 담당하는 KeyAction (Inspector에 표시)

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

    public void OnClick()
    {
        if (!isAwaitingKey)
        {
            isAwaitingKey = true;
            keyText.text = "Press any key...";  // 대기 상태 표시
        }
    }

    private void Update()
    {
        if (isAwaitingKey && Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    isAwaitingKey = false; // 대기 상태 해제
                    keyText.text = keyCode.ToString(); // UI에 키 표시
                    onKeyChanged?.Invoke(keyAction, keyCode); // KeyManager에 변경 요청
                    break;
                }
            }
        }
    }
}
