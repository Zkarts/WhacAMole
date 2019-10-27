using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntValuePicker : MonoBehaviour {

    public event Action OnValueChanged;

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
            OnValueChanged?.Invoke();
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
        int newValue = value + interval;
        if (newValue >= maxValue) {
            newValue = maxValue;
            addButton.interactable = false;
        }

        Value = newValue;
        subtractButton.interactable = true;
    }

    private void Subtract() {
        int newValue = value - interval;
        if (newValue <= minValue) {
            newValue = minValue;
            subtractButton.interactable = false;
        }

        Value = newValue;
        addButton.interactable = true;
    }
}
