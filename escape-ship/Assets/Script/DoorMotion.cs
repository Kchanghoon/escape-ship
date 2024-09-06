using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorMotion : MonoBehaviour
{
    [SerializeField] private Transform doorLeft;
    [SerializeField] private Transform doorRight;
    [SerializeField] private Transform doorUp;    // ���� �����̴� ��
    [SerializeField] private Transform doorDown;  // �Ʒ��� �����̴� ��

    [Header("Motion Value")]
    [SerializeField] float leftStartPosZ;
    [SerializeField] float rightStartPosZ;
    [SerializeField] float endPosZ;
    [SerializeField] float duration;
    [SerializeField] Ease motionEase;

    [Header("Vertical Motion Value")]
    [SerializeField] float upStartPosY;
    [SerializeField] float downStartPosY;
    [SerializeField] float endPosY;   // ���� ���Ϸ� �̵��� �Ÿ�

    [ContextMenu("PlayDoorMotion")]
    private void PlayDoorMotion()
    {
        DOTween.Sequence()
            // �¿�� �����̴� ��
            .Append(doorLeft.DOLocalMoveZ(leftStartPosZ + endPosZ, duration).From(leftStartPosZ))
            .Join(doorRight.DOLocalMoveZ(rightStartPosZ - endPosZ, duration).From(rightStartPosZ))
            // ���Ϸ� �����̴� ��
            .Join(doorUp.DOLocalMoveY(upStartPosY + endPosY, duration).From(upStartPosY))
            .Join(doorDown.DOLocalMoveY(downStartPosY - endPosY, duration).From(downStartPosY))
            .SetEase(motionEase)
            .SetId($"Door Motion{GetInstanceID()}");
    }

    [ContextMenu("ResetMotion")]
    private void ResetMotion()
    {
        DOTween.Kill($"Door Motion{GetInstanceID()}");
    }
}
