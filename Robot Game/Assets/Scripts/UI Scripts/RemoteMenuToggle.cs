using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RemoteMenuToggle : MonoBehaviour
{
    public Toggle[] toggles;

    public static RemoteMenuToggle instance;

    private  void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void ToggleThis(string togglename)
    {
        Toggle  toggle = instance.toggles.ToList().Find(x => x.name.ToLower() == togglename.ToLower());
        toggle.isOn = !toggle.isOn;
    }
}
