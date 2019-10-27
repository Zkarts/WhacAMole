using System;
using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour {

    public event Action<int, int> OnPlay;

    [SerializeField]
    private MenuUI menuUI;

    [SerializeField]
    private PlayUI playUI;

    [SerializeField]
    private HighScoreUI highScoreUI;

    public void Init(GameModesController gameModesController) {
        highScoreUI.Init();

        menuUI.OnPlay += (duration, slots) => OnPlay?.Invoke(duration, slots);
        highScoreUI.OnClose += ToMenu;

        gameModesController.OnGameStart += ToPlay;
        gameModesController.OnGameEnd += ToHighScores;

        menuUI.Deactivate();
        playUI.Deactivate();
        highScoreUI.Deactivate();

        ToMenu();
    }

    public void ToMenu() {
        highScoreUI.Deactivate();
        menuUI.Activate();
    }

    public void ToPlay(PlayModel playModel, int gameDurationInSeconds) {
        menuUI.Deactivate();

        playUI.Subscribe(playModel);
        playUI.Activate(gameDurationInSeconds);
    }

    public void ToHighScores(PlayModel playModel, GameModeSetting currentSetting) {
        playUI.Unsubscribe(playModel);
        playUI.Deactivate();

        highScoreUI.Activate(currentSetting, playModel.Score);
    }

}