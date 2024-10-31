using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public ItemDataExample itemData;  // 아이템 데이터
    public float pickUpDistance = 2f;  // 아이템을 가져갈 수 있는 거리
    private Transform playerTransform;  // 플레이어의 Transform
    private bool isMouseOverItem = false;  // 에임이 아이템에 맞춰졌는지 여부
    public AudioClip pickUpSound;  // 아이템 획득 시 재생할 사운드 클립
    private AudioSource audioSource;  // AudioSource 컴포넌트

    void Start()
    {
        playerTransform = PlayerController.Instance.transform;
        audioSource = GetComponent<AudioSource>();  // 오브젝트의 AudioSource 컴포넌트 가져오기
        KeyManager.Instance.keyDic[KeyAction.PickUp] += TryPickUpItem;
    }

    private void OnMouseEnter()
    {
        isMouseOverItem = true;
        HighlightItem(true);
    }

    private void OnMouseExit()
    {
        isMouseOverItem = false;
        HighlightItem(false);
    }

    private void TryPickUpItem()
    {
        if (isMouseOverItem)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            if (distance <= pickUpDistance)
            {
                PickUpItem();
            }
        }
    }

    private void PickUpItem()
    {
        KeyManager.Instance.keyDic[KeyAction.PickUp] -= TryPickUpItem;
        ItemController.Instance.AddItem(itemData.id);

        // 아이템 획득 사운드 재생
        if (pickUpSound != null)
        {
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
        }

        Destroy(gameObject);  // 사운드 재생과 동시에 오브젝트 파괴
        Debug.Log($"아이템 {itemData.id}을(를) 획득했습니다.");
    }

    private void HighlightItem(bool highlight)
    {
        var outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = highlight;
        }
    }
}
