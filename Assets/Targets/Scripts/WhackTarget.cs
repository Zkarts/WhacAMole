using System;
using System.Collections;
using UnityEngine;

public class WhackTarget : MonoBehaviour {

    //weight between all whackTargets to decide if this option gets chosen
    [SerializeField]
    private float weight;

    [SerializeField]
    private int pointValue;

    [SerializeField]
    private float activeDuration;

    protected TargetMoveBehaviour targetMoveBehaviour;

    protected TargetPresenter presenter;

    private PlayModel playModel;
    private bool isActive;

    public float Weight => weight;
    public int PointValue => pointValue;
    public float ActiveDuration => activeDuration;

    public void Init(TargetPresenter presenter, PlayModel playModel) {
        this.presenter = presenter;
        this.playModel = playModel;

        targetMoveBehaviour = GetComponent<TargetMoveBehaviour>();
        if (targetMoveBehaviour == null) {
            Debug.LogError("No ITargetMoveBehaviour found for WhackTarget");
        }
        else {
            targetMoveBehaviour.MoveToStartPos();
        }
    }

    public void Activate() {
        isActive = true;

        if (targetMoveBehaviour != null) {
            targetMoveBehaviour.Activate();
        }
    }

    public void Deactivate() {
        if (targetMoveBehaviour != null) {
            targetMoveBehaviour.Deactivate();
        }
    }

    //can be overridden for varying hit behaviour
    public virtual void GetHit() {
        if (!isActive) {
            return;
        }

        isActive = false;
        presenter.HideTarget();
        playModel.Score += pointValue;
    }

}
