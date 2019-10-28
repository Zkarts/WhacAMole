using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLoader : MonoBehaviour {

    [SerializeField]
    private Camera gameCamera;

    [SerializeField]
    private SelectionManager standaloneSelectionManagerPrefab, mobileSelectionManagerPrefab;

    private SelectionManager selectionManager;

    public SelectionManager SelectionManager {
        get { return selectionManager; }
    }

    public void Load() {

#if UNITY_EDITOR || UNITY_STANDALONE
        Debug.Log("Standalone platform");
        selectionManager = GameObject.Instantiate<SelectionManager>(standaloneSelectionManagerPrefab);
#elif UNITY_ANDROID || UNITY_IOS
        Debug.Log("Mobile platform");
        selectionManager = GameObject.Instantiate<SelectionManager>(mobileSelectionManagerPrefab);
#endif

        selectionManager.Init(gameCamera);
    }

}