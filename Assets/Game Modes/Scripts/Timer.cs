using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour {

    private event Action onTimerEnd;

    private Coroutine timerCoroutine;
    private float duration;
    private float timeExpired = 0;

    public void StartTimer(float duration, Action onTimerEnd) {
        this.duration = duration;
        this.onTimerEnd = onTimerEnd;

        if (timerCoroutine != null) {
            StopTimer();
        }
        timerCoroutine = StartCoroutine(TimerRoutine());
    }

    public void StopTimer() {
        StopCoroutine(timerCoroutine);
    }

    private IEnumerator TimerRoutine() {
        timeExpired = 0;
        while (timeExpired < duration) {
            timeExpired += Time.deltaTime;
            yield return null;
        }

        onTimerEnd?.Invoke();
    }

}
