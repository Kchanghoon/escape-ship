using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.Playables;
using System.IO;
using Unity.VisualScripting;

public class PlayerController : Singleton<PlayerController>
{
    public Transform cameraTransform;
    public CharacterController characterController;

    public float moveSpeed = 7f;
    public float NormalSpeed = 7f;
    public float RunSpeed = 10f;
    public float jumpSpeed = 10f;
    public float gravity = -20f;
    public float yVelocity = 0;
    public float oxygenDepletedSpeed = 5f;  // 산소가 다 떨어졌을 때의 속도
    private bool isSitting = false;
    public float sitHeight = 0.7f;  // 앉았을 때 카메라가 이동할 높이
    public float sitDuration = 0.5f; // 앉는 동작이 진행될 시간

    public bool isMovingEnabled = true;
    bool isRun;

    private PlayerState playerState;

    private void Start()
    {
        playerState = FindObjectOfType<PlayerState>();  // PlayerState 스크립트 참조
        moveSpeed = NormalSpeed;  // 기본 속도 설정

        KeyManager.Instance.keyDic[KeyAction.Jump] += OnJump;
        KeyManager.Instance.keyDic[KeyAction.Run] += OnRun;
        KeyManager.Instance.keyDic[KeyAction.Sit] += OnSit;
    }

    private void OnJump()
    {
        // 산소가 3 이상이어야 점프 가능
        if (characterController.isGrounded && playerState.Oxygen >= 3f)
        {
            yVelocity = jumpSpeed;
        }
    }

    private void OnRun()
    {
        // 산소가 3 이상일 때만 달리기 가능
        if (playerState.Oxygen >= 3f)
        {
            moveSpeed = isRun ? NormalSpeed : RunSpeed;
            isRun = !isRun;
        }
    }

    private void OnSit()
    {
        // 앉고 일어서기 토글
        if (!isSitting)
        {
            // 앉는 동작: 카메라 높이를 낮춘다
            cameraTransform.DOLocalMoveY(cameraTransform.localPosition.y - sitHeight, sitDuration).SetEase(Ease.OutQuad);
        }
        else
        {
            // 일어서는 동작: 카메라 높이를 원래대로 되돌린다
            cameraTransform.DOLocalMoveY(cameraTransform.localPosition.y + sitHeight, sitDuration).SetEase(Ease.OutQuad);
        }

        isSitting = !isSitting;  // 상태 토글
    }


    void Update()
    {
        if (!isMovingEnabled)
            return;
        // 산소가 0이면 이동 속도를 5로 고정
        if (playerState.Oxygen <= 0)
        {
            moveSpeed = oxygenDepletedSpeed;
        }
        else if (!isRun) // 달리기 중이 아닐 때는 일반 속도
        {
            moveSpeed = NormalSpeed;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(h, 0, v);
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        if (!characterController.isGrounded) moveSpeed = NormalSpeed;

        yVelocity += (gravity * Time.deltaTime);
        moveDirection.y = yVelocity;

        characterController.Move(moveDirection * Time.deltaTime);

    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public async void SetPlayerPosition(Vector3 position)
    {
        isMovingEnabled = false;
        transform.position = position;

        Debug.Log("플레이어 위치가 설정되었습니다: " + transform.position);
        await UniTask.Delay(1000); // 플레이어 이동 후 딜레이
        isMovingEnabled = true;
    }
}
