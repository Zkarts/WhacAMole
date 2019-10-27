using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HighScoreContainer {

    private HighScoreManager highScoreManager;

    //Maximum size of the List<HighScoreEntry> is decided by the HighScoreManager
    private Dictionary<GameModeSetting, List<HighScoreEntry>> entryLists = new Dictionary<GameModeSetting, List<HighScoreEntry>>();

    public HighScoreContainer(HighScoreManager highScoreManager) {
        this.highScoreManager = highScoreManager;
    }

    public List<HighScoreEntry> TryGetEntries(GameModeSetting setting) {
        List<HighScoreEntry> entries;
        entryLists.TryGetValue(setting, out entries);
        return entries;
    }

    public void AddHighScore(GameModeSetting setting, HighScoreEntry entry) {
        //TODO:
        //entryLists.Add(setting, entry);
    }

    public void RemoveHighScore(GameModeSetting setting) {
        entryLists.Remove(setting);
    }

}