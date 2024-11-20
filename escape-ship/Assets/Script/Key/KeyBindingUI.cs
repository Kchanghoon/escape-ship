using System.Linq;
using UnityEngine;

public class KeyBindingUI : MonoBehaviour
{
    [SerializeField] private KeyManager keyManager;  // KeyManager 참조
    [SerializeField] private Transform contentContainer;  // Scroll View의 Content
    [SerializeField] private GameObject keyBindingPrefab; // Prefab
    private void Start()
    {
        InitializeKeyBindings();

    }

    private void InitializeKeyBindings()
    {
        foreach (var keySet in keyManager.InputDownKeySets.Concat(keyManager.InputKeySets))
        {
            // Prefab 생성
            var keyBindingObject = Instantiate(keyBindingPrefab, contentContainer);

            // KeyBindingEntryUI 초기화
            var keyBindingUI = keyBindingObject.GetComponent<KeyBindingEntryUI>();
            keyBindingUI.Initialize(
                keySet.keyAction,
                keySet.keyCode,
                keyManager.UpdateKeyBinding
            );
        }
    }


}
