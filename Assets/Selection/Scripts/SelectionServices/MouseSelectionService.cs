using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelectionService : MonoBehaviour, ISelectionService {

    public bool DetectSelection() {
        return Input.GetMouseButtonDown(0);
    }

}