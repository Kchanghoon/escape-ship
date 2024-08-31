using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPS : MonoBehaviour
{
    public float speed;

    void Update()
    {
        // -1 ~ 1�� ��
        // �⺻ ����: (A,D) (���� �潺ƽ�� ��,��)
        float horizontalInput = Input.GetAxis("Horizontal");
        // �⺻ ����: (W,S) (���� �潺ƽ�� ��,�Ʒ�)
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        // ������ ������ä ũ�⸦ 1�� ����ȭ
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime);
    }
}