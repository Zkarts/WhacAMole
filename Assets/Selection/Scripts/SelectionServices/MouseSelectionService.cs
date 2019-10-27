using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelectionService : MonoBehaviour, ISelectionService {

    const int leftMouseButtonId = 0;

    bool alreadyDetected = false;

    public void Refresh() {
        alreadyDetected = false;
    }

    //Only report a selection once per frame
    public bool DetectSelection(out int id) {
        id = leftMouseButtonId;

        if (alreadyDetected) {
            return false;
        }

        alreadyDetected = true;
        return Input.GetMouseButtonDown(leftMouseButtonId);
    }

}