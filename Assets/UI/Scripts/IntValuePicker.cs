using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntValuePicker : MonoBehaviour {

    [SerializeField]
    private int defaultValue, minValue, maxValue;

    [SerializeField]
    private int interval;

    [SerializeField]
    private Button addButton, subtractButton;

    [SerializeField]
    private IntDisplay display;

    private int value;

    public int Value {
        get { return value; }
        set {
            this.value = value;
            display.SetValue(value);
        }
    }

    public void Activate() {
        addButton.onClick.AddListener(Add);
        subtractButton.onClick.AddListener(Subtract);

        addButton.interactable = true;
        subtractButton.interactable = true;

        Value = Mathf.Clamp(defaultValue, minValue, maxValue);
        gameObject.SetActive(true);
    }

    public void Deactivate() {
        addButton.onClick.RemoveListener(Add);
        subtractButton.onClick.RemoveListener(Subtract);
        gameObject.SetActive(false);
    }

    private void Add() {
        value += interval;
        if (value >= maxValue) {
            value = maxValue;
            addButton.interactable = false;
        }
        display.SetValue(value);
        subtractButton.interactable = true;
    }

    private void Subtract() {
        value -= interval;
        if (value <= minValue) {
            value = minValue;
            subtractButton.interactable = false;
        }
        display.SetValue(value);
        addButton.interactable = true;
    }
}
