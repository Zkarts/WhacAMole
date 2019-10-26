using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class RoundsController : MonoBehaviour {

    public event Action OnRoundsFinished;

    [SerializeField]
    private TargetController targetController;

    private Timer timer;
    private TargetTypeCollection targetTypeCollection;
    private List<RoundDefinition> roundDefinitions;
    private int roundCounter = 0;

    private void Awake() {
        timer = GetComponent<Timer>();
    }

    public void StartRounds(TargetTypeCollection targetTypeCollection, List<RoundDefinition> roundDefinitions, PlayModel playModel) {
        this.targetTypeCollection = targetTypeCollection;
        this.roundDefinitions = roundDefinitions;

        targetController.Init(playModel);

        roundCounter = 0;
        ExecuteRound();
    }

    private void ExecuteRound() {
        timer.StartTimer(roundDefinitions[roundCounter].duration, EvaluateRound);
        targetController.ExecuteRound(roundDefinitions[roundCounter]);
    }

    private void EvaluateRound() {
        Debug.Log("Round " + roundCounter + " over");
        if (roundDefinitions[roundCounter].isFinalRound) {
            OnRoundsFinished?.Invoke();
        }
        else {
            roundCounter++;
            ExecuteRound();
        }
    }

}