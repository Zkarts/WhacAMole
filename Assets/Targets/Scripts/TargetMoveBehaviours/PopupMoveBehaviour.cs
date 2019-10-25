using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMoveBehaviour : TargetMoveBehaviour {

    public override event Action OnDeactivated;

    [SerializeField]
    private float shownYPos, hiddenYPos;

    [SerializeField]
    [Range(0f, TargetMoveBehaviour.maxMoveTime)]
    private float showMoveTime, hideMoveTime;

    private float timeExpired = 0;
    private Coroutine moveRoutine;

    public override void MoveToStartPos() {
        Vector3 startPos = transform.position;
        startPos.y = hiddenYPos;
        transform.position = startPos;
    }

    public override void Activate() {
        moveRoutine = StartCoroutine(MoveToY(shownYPos, showMoveTime));
    }

    public override void Deactivate(bool deactivateFast = false) {
        if (moveRoutine != null) {
            StopCoroutine(moveRoutine);
        }
        moveRoutine = StartCoroutine(MoveToY(hiddenYPos, deactivateFast ? hideMoveTime : fastDeactivateTime));
    }

    private IEnumerator MoveToY(float targetYPos, float duration) {
        timeExpired = 0;
        float startYPos = transform.localPosition.y;
        Vector3 newPos = transform.localPosition;
        yield return null;

        while (timeExpired < duration) {
            timeExpired += Time.deltaTime;
            newPos.y = Mathf.Lerp(startYPos, targetYPos, timeExpired / duration);
            transform.localPosition = newPos;
            yield return null;
        }

        OnDeactivated?.Invoke();
    }

}
