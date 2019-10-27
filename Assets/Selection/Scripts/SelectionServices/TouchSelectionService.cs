using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSelectionService : MonoBehaviour, ISelectionService {

    //keep track of all the touches already reported to support multi-taps
    private List<int> activatedIds = new List<int>();

    public void Refresh() {
        activatedIds.Clear();
    }

    public bool DetectSelection(out int id) {
        for (int i = 0; i < Input.touchCount; i++) {
            bool alreadyUsed = false;
            for (int j = 0; j < activatedIds.Count; j++) {
                if (i == j) {
                    alreadyUsed = true;
                    break;
                }
            }

            if (!alreadyUsed && Input.GetTouch(i).phase == TouchPhase.Began) {
                activatedIds.Add(i);
                id = i;
                return true;
            }
        }

        id = -1;
        return false;
    }

}