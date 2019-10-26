using UnityEngine;
using System.Collections;

public class PlayUI : MonoBehaviour {

    [SerializeField]
    private GameTimerUI gameTimerUI;

    [SerializeField]
    private IntDisplay scoreDisplay;

    private void Start() {
        Deactivate();
    }

    public void Subscribe(PlayModel playModel) {
        playModel.OnScoreChange += scoreDisplay.SetValue;
    }

    public void Unsubscribe(PlayModel playModel) {
        playModel.OnScoreChange -= scoreDisplay.SetValue;
    }

    public void Activate(float gameTime) {
        gameObject.SetActive(true);
        gameTimerUI.StartTimer(gameTime);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

}
