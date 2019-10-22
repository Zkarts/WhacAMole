using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HighScoreContainer : ScriptableObject {

    public List<HighScoreEntry> Entries;

    public void AddHighScore(HighScoreEntry entry) {
        //TODO: add in the right position
        Entries.Add(entry);
    }

    public void RemoveHighScore(HighScoreEntry entry) {
        //TODO: remove the right one
        Entries.Remove(entry);
    }

}