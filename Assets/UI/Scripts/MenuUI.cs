using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour {

    public event Action<int, int> OnPlay;

    private const string highScoreText = "High score:";

    [SerializeField]
    private IntValuePicker timePicker, holesPicker;

    [SerializeField]
    private HighScoreDisplayUI highScoreDisplay;

    [SerializeField]
    private HighScoreManager highScoreManager;

    [SerializeField]
    private Button playButton;

    public void Activate() {
        gameObject.SetActive(true);
        timePicker.Activate();
        holesPicker.Activate();
        UpdateHighScoreDisplay();

        timePicker.OnValueChanged += UpdateHighScoreDisplay;
        holesPicker.OnValueChanged += UpdateHighScoreDisplay;
        playButton.onClick.AddListener(StartPlay);
    }

    public void Deactivate() {
        timePicker.Deactivate();
        holesPicker.Deactivate();

        timePicker.OnValueChanged -= UpdateHighScoreDisplay;
        holesPicker.OnValueChanged -= UpdateHighScoreDisplay;
        playButton.onClick.RemoveListener(StartPlay);

        gameObject.SetActive(false);
    }

    private void StartPlay() {
        OnPlay?.Invoke(timePicker.Value, holesPicker.Value);
    }

    private void UpdateHighScoreDisplay() {
        List<HighScoreEntry> entries = highScoreManager.TryGetEntries(new GameModeSetting(timePicker.Value, holesPicker.Value));
        HighScoreEntry entryToShow = null;
        if (entries != null && entries.Count > 0) {
            entryToShow = entries[0];
        }
        highScoreDisplay.ShowHighScoreEntry(highScoreText, entryToShow);
    }

}