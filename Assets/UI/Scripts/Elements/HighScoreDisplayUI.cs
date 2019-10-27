using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplayUI : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private TextMeshProUGUI userNameDisplay;

    [SerializeField]
    private IntDisplay scoreDisplay;

    public void Awake() {
        Deactivate();
    }

    public void ShowHighScoreEntry(string description, HighScoreEntry entry) {
        if (entry == null) {
            Deactivate();
        }
        else {
            Activate(description, entry);
        }
    }

    private void Activate(string description, HighScoreEntry entry) {
        gameObject.SetActive(true);

        descriptionText.text = description;
        userNameDisplay.text = entry.Name;
        scoreDisplay.SetValue(entry.Points);
    }

    private void Deactivate() {
        gameObject.SetActive(false);
    }

}