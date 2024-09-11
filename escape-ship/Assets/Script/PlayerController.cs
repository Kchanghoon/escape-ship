using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public CharacterController characterController;

    public float moveSpeed = 10f;
    public float NormalSpeed = 10f;
    public float RunSpeed = 20f;
    public float jumpSpeed = 10f;
    public float gravity = -20f;
    public float yVelocity = 0;


    public bool isMovingEnabled = true;

    void Update()
    {
        if (!isMovingEnabled)
            return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(h, 0, v);
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        if (characterController.isGrounded)
        {
            yVelocity = 0;
            if (Input.GetKeyDown(KeyCode.Space)) yVelocity = jumpSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift)) moveSpeed = RunSpeed;
        else moveSpeed = NormalSpeed;

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
