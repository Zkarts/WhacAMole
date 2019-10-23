using System;
using System.Collections;
using UnityEngine;

public class WhackTarget : MonoBehaviour {

    public event Action OnDeactivated;

    [SerializeField]
    private int pointValue;

    [SerializeField]
    private float activeDuration;

    protected TargetMoveBehaviour targetMoveBehaviour;

    protected TargetPresenter presenter;

    public int PointValue { get { return pointValue; } }
    public float ActiveDuration { get { return activeDuration; } }

    public void Init(TargetPresenter presenter) {
        this.presenter = presenter;

        targetMoveBehaviour = GetComponent<TargetMoveBehaviour>();
        if (targetMoveBehaviour == null) {
            Debug.LogError("No ITargetMoveBehaviour found for WhackTarget");
        }
        else {
            targetMoveBehaviour.SetStartPos();
        }
    }

    public void Activate() {
        if (targetMoveBehaviour != null) {
            targetMoveBehaviour.Activate();
        }
    }

    public void Deactivate() {
        if (targetMoveBehaviour != null) {
            targetMoveBehaviour.Deactivate();
        }
    }

    public void GetHit() {
        presenter.HideTarget();
    }

}
