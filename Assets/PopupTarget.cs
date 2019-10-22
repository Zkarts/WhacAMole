using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Timer), typeof(IPopupMoveBehaviour))]
public abstract class PopupTarget : MonoBehaviour {

    public int PointValue { get; }
    public float ActiveDuration { get; }

    private IPopupMoveBehaviour popupMoveBehaviour;

    private Timer timer;

    public PopupTarget(int pointValue, float activeDuration) {
        PointValue = pointValue;
        ActiveDuration = activeDuration;
    }

    public void Start() {
        timer = GetComponent<Timer>();
    }

    public virtual void Activate() {
        //TODO: this is doing more than just activating
        popupMoveBehaviour.Activate();
        timer.StartTimer(ActiveDuration, Deactivate);
    }

    public virtual void Deactivate() {
        popupMoveBehaviour.Deactivate();
    }

}
