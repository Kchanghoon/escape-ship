using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPS : MonoBehaviour
{
    public float speed;

    void Update()
    {
        // -1 ~ 1의 값
        // 기본 맵핑: (A,D) (왼쪽 썸스틱의 오,왼)
        float horizontalInput = Input.GetAxis("Horizontal");
        // 기본 맵핑: (W,S) (왼쪽 썸스틱의 위,아래)
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        // 방향은 유지한채 크기를 1로 정규화
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime);
    }
}