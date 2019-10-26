using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour {

    public event Action<int, int> OnPlay;

    [SerializeField]
    private IntValuePicker timePicker, holesPicker;

    [SerializeField]
    private Button playButton;

    public void Activate() {
        gameObject.SetActive(true);
        timePicker.Activate();
        holesPicker.Activate();
        playButton.onClick.AddListener(StartPlay);
    }

    public void Deactivate() {
        timePicker.Deactivate();
        holesPicker.Deactivate();
        gameObject.SetActive(false);
        playButton.onClick.RemoveListener(StartPlay);
    }

    private void StartPlay() {
        OnPlay?.Invoke(timePicker.Value, holesPicker.Value);
    }

}
