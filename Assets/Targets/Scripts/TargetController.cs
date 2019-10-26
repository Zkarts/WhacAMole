using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class TargetController : MonoBehaviour {

    private List<TargetPresenter> presenters;

    private Timer timer;
    private PlayModel playModel;
    private TargetTypeCollection targetTypeCollection;

    private void Awake() {
        timer = GetComponent<Timer>();
    }

    public void SetPresenters(List<TargetPresenter> presenters) {
        this.presenters = presenters;
    }

    public void Init(PlayModel playModel, TargetTypeCollection targetTypeCollection) {
        this.playModel = playModel;
        this.targetTypeCollection = targetTypeCollection;
    }

    public void ExecuteRound(RoundDefinition roundDefinition) {
        LoadTargets(roundDefinition.orderedSlotDefinitions);
        ShowTargets();
        timer.StartTimer(roundDefinition.duration - TargetMoveBehaviour.maxMoveTime, HideTargets);
    }

    private void LoadTargets(List<RoundDefinition.SlotDefinition> slotDefinitions) {
        for (int i = 0; i < presenters.Count; i++) {
            presenters[i].ClearTarget();

            RoundDefinition.SlotDefinition slotDefinition = slotDefinitions[i];
            if (slotDefinition == null) {
                continue;
            }

            WhackTarget targetPrefab = targetTypeCollection.targetTypes[slotDefinition.targetTypeIndex];
            WhackTarget newTarget = GameObject.Instantiate<WhackTarget>(targetPrefab, presenters[i].transform);
            newTarget.transform.eulerAngles = Vector3.zero;

            presenters[i].SetTarget(newTarget);
            newTarget.Init(presenters[i], playModel);
        }
    }

    private void ShowTargets() {
        for (int i = 0; i < presenters.Count; i++) {
            presenters[i].PresentTarget();
        }
    }

    private void HideTargets() {
        for (int i = 0; i < presenters.Count; i++) {
            presenters[i].HideTarget();
        }
    }

}