using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreUI : MonoBehaviour {

    public event Action OnClose;

    [SerializeField]
    private HighScoreManager highScoreManager;

    [SerializeField]
    private HighScoreListUI highScoreListUI;

    [SerializeField]
    private HighScoreNewUI highScoreNewUI;

    private GameModeSetting currentSetting;

    public void Init() {
        highScoreNewUI.Init();
        highScoreListUI.Init(highScoreManager.EntriesSavedPerSetting);

        highScoreNewUI.Deactivate();
        highScoreListUI.Deactivate();

        highScoreNewUI.OnNewHighScore += AddNewHighScore;
        highScoreListUI.OnClose += Close;
    }

    public void Activate(GameModeSetting setting, int newScore) {
        gameObject.SetActive(true);
        currentSetting = setting;

        List<HighScoreEntry> entries = highScoreManager.TryGetEntries(setting);

        bool addNewEntry = false;

        if (entries != null) {
            if (entries.Count <= 0) {
                addNewEntry = true;
            }
            else {
                HighScoreEntry lastEntry = entries[entries.Count - 1];

                if (lastEntry.Points < newScore) {
                    addNewEntry = true;
                }
            }
        }
        else {
            addNewEntry = true;
        }

        if (addNewEntry) {
            highScoreNewUI.Activate(currentSetting, newScore);
        }
        else {
            highScoreListUI.Activate(entries, currentSetting);
        }
    }

    private void AddNewHighScore(HighScoreEntry newEntry) {
        List<HighScoreEntry> entries = highScoreManager.TryGetEntries(currentSetting);

        highScoreNewUI.Deactivate();
        highScoreListUI.Activate(entries, currentSetting);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
        highScoreNewUI.Deactivate();
        highScoreListUI.Deactivate();
    }

    private void Close() {
        OnClose?.Invoke();
    }

}