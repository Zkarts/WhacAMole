using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSettingComparer : IEqualityComparer<GameModeSetting> {

    public bool Equals(GameModeSetting x, GameModeSetting y) {
        return x.Slots == y.Slots && x.DurationInS == y.DurationInS;
    }

    public int GetHashCode(GameModeSetting obj) {
        int hash = 23;
        hash = hash * 31 + obj.DurationInS;
        hash = hash * 31 + obj.Slots;
        return hash;
    }

}
