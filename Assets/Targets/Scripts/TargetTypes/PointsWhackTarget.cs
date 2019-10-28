using System;
using System.Collections;
using UnityEngine;

public class PointsWhackTarget : WhackTarget {

    [SerializeField]
    private int pointValue;

    protected override void HandleHit() {
        isActive = false;
        presenter.HideTarget();
        playModel.Score += pointValue;
    }

}