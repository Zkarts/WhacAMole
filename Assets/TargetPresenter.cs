﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class TargetPresenter : MonoBehaviour {

    private Timer timer;
    private WhackTarget target;

    public TargetPresenter(WhackTarget target) {
        this.target = target;
    }

    public void Awake() {
        timer = GetComponent<Timer>();
        //TODO: TEMP
        target = GetComponentInChildren<WhackTarget>();
        if (target != null) {
            target.Init(this);
        }
    }

    public void PresentTarget() {
        target.Activate();
        timer.StartTimer(target.ActiveDuration, HideTarget);
    }

    public void HideTarget() {
        target.Deactivate();
    }

}
