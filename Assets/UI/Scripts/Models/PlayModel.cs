using System;
using System.Collections;
using UnityEngine;

public class PlayModel {

    public event Action<int> OnScoreChange;
    public event Action<int> OnTimeLeftChange;

    private int score = 0;
    private int timeLeft = 0;

    public int Score {
        get { return score; }
        set {
            score = value;
            OnScoreChange?.Invoke(score);
        }
    }

    public int TimeLeft {
        get { return timeLeft; }
        set {
            timeLeft = value;
            OnTimeLeftChange?.Invoke(timeLeft);
        }
    }
}
