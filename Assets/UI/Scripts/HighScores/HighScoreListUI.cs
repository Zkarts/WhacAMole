using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreListUI : MonoBehaviour {

    public event Action OnClose;

    [SerializeField]
    private Transform highScoreList;

    [SerializeField]
    private SettingsDisplayUI settingsDisplayUI;

    [SerializeField]
    private Button okButton;

    [SerializeField]
    private HighScoreDisplayUI highScoreDisplayUIPrefab;

    private List<HighScoreDisplayUI> displayUIs = new List<HighScoreDisplayUI>();

    public void Init(int listLength) {
        for (int i = 0; i < listLength; i++) {
            HighScoreDisplayUI displayUI = GameObject.Instantiate<HighScoreDisplayUI>(highScoreDisplayUIPrefab);
            displayUIs.Add(displayUI);
        }

        okButton.onClick.AddListener(Close);
    }

    public void Activate(List<HighScoreEntry> entries, GameModeSetting setting) {
        gameObject.SetActive(true);

        settingsDisplayUI.Activate(setting);

        for (int i = 0; i < displayUIs.Count; i++) {
            HighScoreEntry entryToShow = null;
            if (entries != null && entries.Count > i) {
                entryToShow = entries[i];
            }
            displayUIs[i].ShowHighScoreEntry(i.ToString(), entryToShow);
        }
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

    private void Close() {
        OnClose?.Invoke();
    }

}