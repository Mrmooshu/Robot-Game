using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteMenuToggle : MonoBehaviour
{
    public Toggle smithingToggle;
    public Toggle closeToggle;
    public Toggle safepointToggle;

    public static RemoteMenuToggle instance;

    private  void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void ToggleThis(Toggle toggle)
    {
        toggle.isOn = !toggle.isOn;
    }
}
