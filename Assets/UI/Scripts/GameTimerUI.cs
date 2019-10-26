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
        intDisplay.SetValue((int)duration);
        timer.StartTimer(duration, null, (s) => intDisplay.SetValue((int)duration - s));
    }

}