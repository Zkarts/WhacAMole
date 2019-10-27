using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class GameModeSetting {

    public int DurationInS { get; }
    public int Slots { get; }

    public GameModeSetting(int durationInS, int slots) {
        DurationInS = durationInS;
        Slots = slots;
    }

}
