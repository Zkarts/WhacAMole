using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetMoveBehaviour : MonoBehaviour {

    protected const float fastDeactivateTime = 0.3f;

    public abstract event Action OnDeactivated;

    //Initialisation to start hidden
    public abstract void SetStartPos();
    //Regular showing up behaviour
    public abstract void Activate();
    //When the timer expires or deactivateFast if hit by the player
    public abstract void Deactivate(bool deactivateFast = false);

}
