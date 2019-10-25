using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSelectionService : MonoBehaviour, ISelectionService {

    public bool DetectSelection() {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

}