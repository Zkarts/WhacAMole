using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreNewUI : MonoBehaviour {

    public event Action<HighScoreEntry> OnNewHighScore;

    [SerializeField]
    private int characterLimit = 3;

    [SerializeField]
    private Button okButton;

    [SerializeField]
    private SettingsDisplayUI settingsDisplayUI;

    [SerializeField]
    private TMP_InputField nameField;

    [SerializeField]
    private IntDisplay scoreDisplay;

    public void Init() {
        okButton.onClick.AddListener(SubmitHighScore);
        nameField.characterLimit = characterLimit;
    }

    public void Activate(GameModeSetting setting, int newScore) {
        gameObject.SetActive(true);
        settingsDisplayUI.Activate(setting);
        nameField.text = "";
        scoreDisplay.SetValue(newScore);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

    private void SubmitHighScore() {
        OnNewHighScore?.Invoke(new HighScoreEntry(nameField.text, scoreDisplay.Value));
    }

}