using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

    [SerializeField]
    private List<TargetPresenter> presenters;

    int counter = 0;
    float duration = 3f;
    float expiredTime = 0;

    public void Start() {
        StartCoroutine(PresentRoutine());
    }

    public void Update() {
    }

    private IEnumerator PresentRoutine() {
        for (int i = 0; i < presenters.Count; i++) {
            expiredTime = 0;

            presenters[i].PresentTarget();

            while (expiredTime < duration) {
                expiredTime += Time.deltaTime;
                yield return null;
            }

            presenters[i].HideTarget();
        }
    }

}