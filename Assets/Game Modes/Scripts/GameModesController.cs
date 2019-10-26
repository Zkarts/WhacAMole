using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModesController : MonoBehaviour {

    [SerializeField]
    private RoundsGenerator roundsGenerator;

    [SerializeField]
    private SlotsManager targetGenerator;

    [SerializeField]
    private RoundsController roundsController;

    [SerializeField]
    private MenuUI menuUI;

    [SerializeField]
    private PlayUI playUI;

    private PlayModel playModel;

    public void Start() {
        menuUI.OnPlay += StartGame;
        menuUI.Activate();
    }

    public void StartGame(int gameDurationInSeconds, int slotCount) {
        playModel = new PlayModel();
        playUI.Subscribe(playModel);
        playUI.Activate(gameDurationInSeconds);

        targetGenerator.GenerateTargets(slotCount);
        List<RoundDefinition> roundDefinitions = roundsGenerator.GenerateRounds(gameDurationInSeconds, slotCount);
        roundsController.StartRounds(roundsGenerator.TargetTypeCollection, roundDefinitions, playModel);

        roundsController.OnRoundsFinished += EndGame;

        menuUI.Deactivate();
    }

    //TODO
    private void EndGame() {
        playUI.Unsubscribe(playModel);
        playUI.Deactivate();

        menuUI.Activate();
    }

}
