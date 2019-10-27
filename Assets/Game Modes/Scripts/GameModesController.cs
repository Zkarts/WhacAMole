using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModesController : MonoBehaviour {

    public event Action<PlayModel, int> OnGameStart;
    public event Action<PlayModel, GameModeSetting> OnGameEnd;

    [SerializeField]
    private PlatformLoader platformLoader;

    [SerializeField]
    private RoundsGenerator roundsGenerator;

    [SerializeField]
    private SlotsManager targetGenerator;

    [SerializeField]
    private RoundsController roundsController;

    [SerializeField]
    private UIController uiController;

    private PlayModel playModel;
    private GameModeSetting currentGameModeSetting;

    public void Start() {
        uiController.Init(this);
        uiController.OnPlay += StartGame;

        platformLoader.Load();
        platformLoader.SelectionManager.IsBlocked = true;
    }

    public void StartGame(int gameDurationInSeconds, int slotCount) {
        playModel = new PlayModel();

        currentGameModeSetting = new GameModeSetting(gameDurationInSeconds, slotCount);

        targetGenerator.GenerateTargets(slotCount);
        List<RoundDefinition> roundDefinitions = roundsGenerator.GenerateRounds(gameDurationInSeconds, slotCount);
        roundsController.StartRounds(roundsGenerator.TargetTypeCollection, roundDefinitions, playModel);

        roundsController.OnRoundsFinished += EndGame;
        platformLoader.SelectionManager.IsBlocked = false;

        OnGameStart?.Invoke(playModel, gameDurationInSeconds);
    }

    private void EndGame() {
        roundsController.OnRoundsFinished -= EndGame;
        platformLoader.SelectionManager.IsBlocked = true;

        OnGameEnd?.Invoke(playModel, currentGameModeSetting);
    }

}
