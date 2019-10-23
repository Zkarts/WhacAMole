using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCameraRayProvider : MonoBehaviour, ICameraRayProvider {

    private Camera gameCamera;

    public void SetCamera(Camera cam) {
        gameCamera = cam;
    }

    public Ray GetRay() {
        if (Input.touchCount == 0) {
            Debug.LogError("No touches found for TouchRayProvider");
            return new Ray(Vector3.zero, Vector3.zero);
        }
        return gameCamera.ViewportPointToRay(Input.GetTouch(0).position);
    }
}