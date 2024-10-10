using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorMotion : MonoBehaviour
{
    [SerializeField] private Transform doorLeft;
    [SerializeField] private Transform doorRight;
    [SerializeField] private Transform doorUp;    // 위로 움직이는 문
    [SerializeField] private Transform doorDown;  // 아래로 움직이는 문

    [Header("Motion Value")]
    [SerializeField] float leftStartPosZ;
    [SerializeField] float rightStartPosZ;
    [SerializeField] float endPosZ;
    [SerializeField] float duration;
    [SerializeField] Ease motionEase;

    [Header("Vertical Motion Value")]
    [SerializeField] float upStartPosY;
    [SerializeField] float downStartPosY;
    [SerializeField] float endPosY;   // 문이 상하로 이동할 거리

    [ContextMenu("PlayDoorMotion")]
    private void PlayDoorMotion()
    {
        DOTween.Sequence()
            // 좌우로 움직이는 문
            .Append(doorLeft.DOLocalMoveZ(leftStartPosZ + endPosZ, duration).From(leftStartPosZ))
            .Join(doorRight.DOLocalMoveZ(rightStartPosZ - endPosZ, duration).From(rightStartPosZ))
            // 상하로 움직이는 문
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
