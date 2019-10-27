using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraRayProvider : MonoBehaviour, ICameraRayProvider {

    private Camera gameCamera;

    public void SetCamera(Camera cam) {
        gameCamera = cam;
    }

    public Ray GetRay(int id = 0) {
        return gameCamera.ScreenPointToRay(Input.mousePosition);
    }
}