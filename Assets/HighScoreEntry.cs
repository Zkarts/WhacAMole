using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct HighScoreEntry {

    public string Name { get; }
    public int Points { get; }

    public HighScoreEntry(string name, int points) {
        Name = name;
        Points = points;
    }

}