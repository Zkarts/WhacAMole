using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class TargetPresenter : MonoBehaviour {

    private Timer timer;
    private WhackTarget target;

    public void Awake() {
        timer = GetComponent<Timer>();
    }

    public void SetTarget(WhackTarget target) {
        this.target = target;
    }

    public void ClearTarget() {
        GameObject.Destroy(target?.gameObject);
        this.target = null;
    }

    public void PresentTarget() {
        if (target == null) {
            return;
        }
        target.Activate();
        timer.StartTimer(target.ActiveDuration, HideTarget);
    }

    public void HideTarget() {
        if (target == null) {
            return;
        }
        target.Deactivate();
    }

}
