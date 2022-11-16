using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public Effect effect;
    private TextMeshProUGUI stackText;
    private TextMeshProUGUI durationText;

    private void Start()
    {
        stackText = transform.Find("Stacks").GetComponent<TextMeshProUGUI>();
        durationText = transform.Find("Duration").GetComponent<TextMeshProUGUI>();
    }

    public void Intitalize(Effect effect)
    {
        this.effect = effect;
    }

    private void Update()
    {
        stackText.text = effect.currentStacks + "";
        durationText.text = Math.Round(effect.currentDuration) + "";
    }
}
