using System;
using System.Collections;
using UnityEngine;

public class PointMultiplyWhackTarget : WhackTarget {

    [SerializeField]
    private float multiplier;

    protected override void HandleHit() {
        isActive = false;
        presenter.HideTarget();
        playModel.Score = (int)((float)playModel.Score * multiplier);
    }

}