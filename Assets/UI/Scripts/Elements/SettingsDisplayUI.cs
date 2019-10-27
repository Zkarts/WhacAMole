using UnityEngine;
using System.Collections;

public class SettingsDisplayUI : MonoBehaviour {

    [SerializeField]
    private IntDisplay timeDisplay;

    [SerializeField]
    private IntDisplay holesDisplay;

    public void Activate(GameModeSetting settings) {
        gameObject.SetActive(true);
        timeDisplay.SetValue(settings.DurationInS);
        holesDisplay.SetValue(settings.Slots);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

}
