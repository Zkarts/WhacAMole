using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCameraRayProvider : MonoBehaviour, ICameraRayProvider {

    private Camera gameCamera;

    public void SetCamera(Camera cam) {
        gameCamera = cam;
    }

    public Ray GetRay(int id = 0) {
        if (Input.touchCount <= id) {
            Debug.LogError($"Touch ID {id} not found for TouchRayProvider");
            return new Ray(Vector3.zero, Vector3.zero);
        }
        return gameCamera.ScreenPointToRay(Input.GetTouch(id).position);
    }
}