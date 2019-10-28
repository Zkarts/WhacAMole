using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreNewUI : MonoBehaviour {

    public event Action<HighScoreEntry> OnNewHighScore;

    [Tooltip("The maximum amount of characters for a player's name. 3 creates the typical arcade feeling")]
    [SerializeField]
    private int characterLimit = 3;

    [SerializeField]
    private bool playerNameAllCaps = true;

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
        if (playerNameAllCaps) {
            //this only sets the font style, the resulting text may still be lower case
            nameField.textComponent.fontStyle |= FontStyles.UpperCase;
        }
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
        string name = nameField.text;
        if (playerNameAllCaps) {
            name = name.ToUpper();
        }
        OnNewHighScore?.Invoke(new HighScoreEntry(name, scoreDisplay.Value));
    }

}