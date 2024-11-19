using System.Linq;
using UnityEngine;

public class KeyBindingUI : MonoBehaviour
{
    [SerializeField] private KeyManager keyManager;  // KeyManager 참조
    [SerializeField] private Transform keyBindingContainer;  // Prefab 컨테이너
    [SerializeField] private GameObject keyBindingPrefab;    // Prefab

    private void Start()
    {
        InitializeKeyBindings();
    }

    private void InitializeKeyBindings()
    {
        // KeyManager의 모든 KeySet에 대해 UI 생성
        foreach (var keySet in keyManager.InputDownKeySets.Concat(keyManager.InputKeySets))
        {
            var keyBindingObject = Instantiate(keyBindingPrefab, keyBindingContainer);
            var keyBindingUI = keyBindingObject.GetComponent<KeyBindingEntryUI>();

            // UI 항목 초기화
            keyBindingUI.Initialize(
                keySet.keyAction, // 현재 KeyAction 전달
                keySet.keyCode,   // 현재 KeyCode 전달
                keyManager.UpdateKeyBinding // 키 변경 요청 콜백
            );
        }
    }
}
