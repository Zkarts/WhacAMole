using System;
using System.Collections;
using UnityEngine;

public abstract class WhackTarget : MonoBehaviour {

    //weight between all whackTargets to decide if this option gets chosen
    [SerializeField]
    protected float weight;

    [SerializeField]
    protected float activeDuration;

    protected TargetMoveBehaviour targetMoveBehaviour;
    protected TargetPresenter presenter;

    protected PlayModel playModel;
    protected bool isActive;

    public float Weight => weight;
    public float ActiveDuration => activeDuration;

    public virtual void Init(TargetPresenter presenter, PlayModel playModel) {
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

    public virtual void Activate() {
        isActive = true;

        if (targetMoveBehaviour != null) {
            targetMoveBehaviour.Activate();
        }
    }

    public virtual void Deactivate() {
        if (targetMoveBehaviour != null) {
            targetMoveBehaviour.Deactivate();
        }
    }

    public void GetHit() {
        if (!isActive) {
            return;
        }

        HandleHit();
    }

    //can be overridden for varying hit behaviour
    protected abstract void HandleHit();

}
