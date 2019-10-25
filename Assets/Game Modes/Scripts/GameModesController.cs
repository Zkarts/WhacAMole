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
    private PlayUI playUI;

    private PlayModel playModel;

    public void Start() {
        //TODO: Temp
        StartGame(30, 15);



        playModel = new PlayModel();
        playUI.Init(playModel);
    }




    //TODO do buttons and stuff
    public void StartGame(int gameDurationInSeconds, int slotCount) {
        playModel = new PlayModel();

        targetGenerator.GenerateTargets(slotCount);
        List<RoundDefinition> roundDefinitions = roundsGenerator.GenerateRounds(gameDurationInSeconds, slotCount);
        roundsController.Init(roundsGenerator.TargetTypeCollection, roundDefinitions, playModel);

        roundsController.OnRoundsFinished += EndGame;
        playUI.Activate(gameDurationInSeconds);
    }

    //TODO
    private void EndGame() {

    }

}
