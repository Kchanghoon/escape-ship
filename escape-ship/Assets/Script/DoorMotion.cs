using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorMotion : MonoBehaviour
{
    [SerializeField]  private Transform doorLeft;
    [SerializeField]  private Transform doorRight;
    
    [Header("Motion Value")]
    [SerializeField] float leftStartPosZ;
    [SerializeField] float rightStartPosZ;
    [SerializeField] float endPosZ;
    [SerializeField] float duration;
    [SerializeField] Ease motionEase;

    [ContextMenu("PlayDoorMotion")]
    private void PlayDoorMotion()
    {
        DOTween.Sequence().Append(doorLeft.DOLocalMoveZ(leftStartPosZ + endPosZ, duration)
                                            .From(leftStartPosZ))
                        .Join(doorRight.DOLocalMoveZ(rightStartPosZ - endPosZ, duration)
                                            .From(rightStartPosZ))
                        .SetEase(motionEase)
                        .SetId($"Door Motion{GetInstanceID()}");
        
    }

    [ContextMenu("ResetMotion")]
    private void ResetMotion()
    {
        DOTween.Kill($"Door Motion{GetInstanceID()}");
    }
}