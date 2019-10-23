using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLoader : MonoBehaviour {

    [SerializeField]
    private Camera gameCamera;

    [SerializeField]
    private SelectionManager standaloneSelectionManagerPrefab, mobileSelectionManagerPrefab;

    private void Awake() {

        SelectionManager selectionManager;

#if UNITY_EDITOR || UNITY_STANDALONE
        selectionManager = GameObject.Instantiate<SelectionManager>(standaloneSelectionManagerPrefab);
#elif UNITY_ANDROID || UNITY_IOS
        selectionManager = GameObject.Instantiate<SelectionManager>(mobileSelectionManagerPrefab);
#endif

        selectionManager.Init(gameCamera);
    }

}