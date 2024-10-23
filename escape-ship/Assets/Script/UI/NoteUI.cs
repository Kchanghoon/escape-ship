using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    [SerializeField] private GameObject item9Panel;
    [SerializeField] private GameObject item10Panel;
    Dictionary<string, GameObject> itemPanelDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        itemPanelDic.Add("9", item9Panel);
        itemPanelDic.Add("10", item10Panel);
    }

    private void Start()
    {
        // 키 이벤트에 패널 토글 메서드를 연결
        KeyManager.Instance.keyDic[KeyAction.Panel] += TryTogglePanel;
    }

    private void OnDestroy()
    {
        if (KeyManager.Instance != null) KeyManager.Instance.keyDic[KeyAction.Panel] -= TryTogglePanel;
    }

    // 선택된 아이템이 패널을 열 수 있는지 확인하는 메서드
    private void TryTogglePanel()
    {
        // 인벤토리에서 선택된 아이템 가져오기
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
        if (selectedItem == null) return;
        if (!itemPanelDic.ContainsKey(selectedItem.id)) return; // id 없는 경우 return

        // 선택된 아이템이 있으며, 그 아이템이 패널을 열 수 있는 아이템인지 확인
        TogglePanel(itemPanelDic[selectedItem.id]);
    }

    // 패널을 활성화 또는 비활성화하는 메서드
    private void TogglePanel(GameObject panel)
    {
        if (panel == null) return;
        bool isActive = !panel.gameObject.activeInHierarchy;  // 패널의 활성화 상태를 토글
        MouseCam.Instance.SetCursorState(!isActive);  // MouseCam 스크립트를 통해 커서 상태 변경
        panel.SetActive(isActive);  // 패널을 활성화 또는 비활성화
    }
}
