using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMoveBehaviour : TargetMoveBehaviour {

    public override event Action OnDeactivated;

    [SerializeField]
    private float shownYPos, hiddenYPos;

    [SerializeField]
    private float showDuration, hideDuration;

    private float timeExpired = 0;
    private Coroutine moveRoutine;

    public override void SetStartPos() {
        Vector3 startPos = transform.position;
        startPos.y = hiddenYPos;
        transform.position = startPos;
    }

    public override void Activate() {
        moveRoutine = StartCoroutine(MoveToY(shownYPos, showDuration));
    }

    public override void Deactivate(bool deactivateFast = false) {
        if (moveRoutine != null) {
            StopCoroutine(moveRoutine);
        }
        moveRoutine = StartCoroutine(MoveToY(hiddenYPos, deactivateFast ? hideDuration : fastDeactivateTime));
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
