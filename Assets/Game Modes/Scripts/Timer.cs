using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour {

    private event Action<int> onSecondPassed;
    private event Action onTimerEnd;

    private Coroutine timerCoroutine;
    private float duration;
    private float timeExpired = 0;

    public void StartTimer(float duration, Action onTimerEnd, Action<int> onSecondPassed = null) {
        this.duration = duration;
        //overwrite any old callbacks with the new ones
        this.onSecondPassed = onSecondPassed;
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
        int secondsPassed = 1;
        while (timeExpired < duration) {
            if (timeExpired > secondsPassed) {
                onSecondPassed?.Invoke(secondsPassed);
                secondsPassed++;
            }
            yield return null;

            timeExpired += Time.deltaTime;
        }

        onSecondPassed?.Invoke(secondsPassed);
        onTimerEnd?.Invoke();
    }

}
