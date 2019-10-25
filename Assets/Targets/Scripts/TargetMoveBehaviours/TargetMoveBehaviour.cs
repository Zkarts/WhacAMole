using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetMoveBehaviour : MonoBehaviour {

    public const float maxMoveTime = 0.5f;
    protected const float fastDeactivateTime = 0.1f;

    public abstract event Action OnDeactivated;

    //Initialisation to start target hidden
    public abstract void MoveToStartPos();
    //Regular showing up behaviour
    public abstract void Activate();
    //When the timer expires or deactivateFast if hit by the player
    public abstract void Deactivate(bool deactivateFast = false);

}
