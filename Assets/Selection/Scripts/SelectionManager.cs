using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

    public bool IsBlocked = false;

    private ISelectionService selectionService;
    private ICameraRayProvider cameraRayProvider;

    public void Init(Camera cam) {
        selectionService = GetComponent<ISelectionService>();
        cameraRayProvider = GetComponent<ICameraRayProvider>();
        if (selectionService == null) {
            Debug.LogError("No ISelectionService found for the SelectionManager");
        }
        if (cameraRayProvider == null) {
            Debug.LogError("No IRayProvider found for the SelectionManager");
        }
        else {
            cameraRayProvider.SetCamera(cam);
        }
    }

    private void Update() {
        if (IsBlocked || selectionService == null || cameraRayProvider == null) {
            return;
        }

        selectionService.Refresh();
        int id = -1;
        while (selectionService.DetectSelection(out id)) {
            Ray ray = cameraRayProvider.GetRay(id);

            Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue);

            //the ray can hit nothing, a target or the game board surface
            if (hitInfo.transform != null && hitInfo.transform.parent != null) {
                WhackTarget target = hitInfo.transform.parent.GetComponent<WhackTarget>();
                if (target != null) {
                    target.GetHit();
                }
            }
        }
    }
}