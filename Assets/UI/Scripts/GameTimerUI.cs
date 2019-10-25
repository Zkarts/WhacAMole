using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Timer))]
public class GameTimerUI : MonoBehaviour {

    private Timer timer;

    [SerializeField]
    private IntDisplay intDisplay;

    private Coroutine timerCoroutine;

    private void Awake() {
        timer = GetComponent<Timer>();
    }

    public void StartTimer(float duration) {
        timerCoroutine = StartCoroutine(TimerRoutine(duration));
    }

    private IEnumerator TimerRoutine(float duration) {
        float timeExpired = 0;
        int displayDuration = (int)duration;
        intDisplay.SetValue(displayDuration);

        while (timeExpired < duration) {
            timeExpired += Time.deltaTime;

            if (timeExpired > 1.0f) {
                displayDuration--;
                timeExpired -= 1.0f;
                intDisplay.SetValue(displayDuration);
            }

            yield return null;
        }
    }

}
