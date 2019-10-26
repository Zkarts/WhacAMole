using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMoveBehaviour : TargetMoveBehaviour {

    public override event Action OnDeactivated;

    [SerializeField]
    private float moveDistance;

    [SerializeField]
    [Range(0f, TargetMoveBehaviour.maxMoveTime)]
    private float showMoveTime = 0.3f, hideMoveTime = 0.3f;

    private float shownYPos, hiddenYPos;
    private float timeExpired = 0;
    private Coroutine moveRoutine;

    public override void MoveToStartPos() {
        Vector3 startPos = transform.position;
        shownYPos = startPos.y;

        startPos.y -= moveDistance;

        transform.position = startPos;
        hiddenYPos = startPos.y;
    }

    public override void Activate() {
        moveRoutine = StartCoroutine(MoveToWorldY(shownYPos, showMoveTime));
    }

    public override void Deactivate(bool deactivateFast = false) {
        if (moveRoutine != null) {
            StopCoroutine(moveRoutine);
        }
        moveRoutine = StartCoroutine(MoveToWorldY(hiddenYPos, deactivateFast ? hideMoveTime : fastDeactivateTime));
    }

    private IEnumerator MoveToWorldY(float targetYPos, float duration) {
        timeExpired = 0;
        float startYPos = transform.position.y;
        Vector3 newPos = transform.position;
        yield return null;

        while (timeExpired < duration) {
            timeExpired += Time.deltaTime;
            newPos.y = Mathf.Lerp(startYPos, targetYPos, timeExpired / duration);
            transform.position = newPos;
            yield return null;
        }

        OnDeactivated?.Invoke();
    }

}
