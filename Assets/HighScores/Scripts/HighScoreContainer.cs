using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HighScoreContainer {

    //Maximum size of the List<HighScoreEntry> is decided by the HighScoreManager
    private Dictionary<GameModeSetting, List<HighScoreEntry>> entryLists;

    public HighScoreContainer() {
        entryLists = CreateHighScoreDictionary();
    }

    private Dictionary<GameModeSetting, List<HighScoreEntry>> CreateHighScoreDictionary() {
        return new Dictionary<GameModeSetting, List<HighScoreEntry>>(new GameModeSettingComparer());
    }

    public List<HighScoreEntry> TryGetEntries(GameModeSetting setting) {
        List<HighScoreEntry> entries;
        entryLists.TryGetValue(setting, out entries);
        return entries;
    }

    public void AddHighScore(GameModeSetting setting, HighScoreEntry newEntry, int maxEntries) {
        List<HighScoreEntry> entries;

        if (entryLists.ContainsKey(setting)) {
            entries = entryLists[setting];

            int entryCount = entries.Count;

            if (entries[entryCount - 1].Points >= newEntry.Points) {
                entries.Add(newEntry);
                entryCount++;
            }
            else {
                for (int i = 0; i < entryCount; i++) {
                    if (entries[i].Points >= newEntry.Points) {
                        continue;
                    }
                    entries.Insert(i, newEntry);
                    entryCount++;
                    break;
                }
            }

            while (entryCount > maxEntries) {
                entries.RemoveAt(maxEntries);
                entryCount--;
            }
        }
        else {
            //no entries yet
            entries = new List<HighScoreEntry>();
            entries.Add(newEntry);
            entryLists[setting] = entries;
        }
    }

}