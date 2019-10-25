﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class IntDisplay : MonoBehaviour {

    private TextMeshProUGUI textBox;

    private void Awake() {
        textBox = GetComponent<TextMeshProUGUI>();
    }

    public void SetValue(int newValue) {
        textBox.text = newValue.ToString();
    }

}
