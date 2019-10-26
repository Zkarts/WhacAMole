using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class TargetController : MonoBehaviour {

    private List<TargetPresenter> presenters;

    private Timer timer;
    private PlayModel playModel;

    private void Awake() {
        timer = GetComponent<Timer>();
    }

    public void SetPresenters(List<TargetPresenter> presenters) {
        this.presenters = presenters;

        foreach (TargetPresenter presenter in presenters) {
            presenter.Init(playModel);
        }
    }

    public void Init(PlayModel playModel) {
        this.playModel = playModel;
    }




    //TODO: temp
    public void Start() {
        StartCoroutine(PresentRoutine());
    }

    private IEnumerator PresentRoutine() {
        yield return null;
        /*
        for (int i = 0; i < presenters.Count; i++) {
            expiredTime = 0;

            presenters[i].PresentTarget();

            //TODO: get the right round duration here
            while (expiredTime < 3f) {
                expiredTime += Time.deltaTime;
                yield return null;
            }

            presenters[i].HideTarget();
        }
        */
    }



    public void ExecuteRound(RoundDefinition roundDefinition) {
        ShowTargets();
        timer.StartTimer(roundDefinition.duration - TargetMoveBehaviour.maxMoveTime, HideTargets);
    }

    private void ShowTargets() {

    }

    private void HideTargets() {

    }

}
