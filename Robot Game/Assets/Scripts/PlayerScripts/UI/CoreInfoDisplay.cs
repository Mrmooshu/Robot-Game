using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoreInfoDisplay : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<RawImage>().texture = GameObject.Find("Second Camera").GetComponent<Camera>().targetTexture;
    }

    public static void UpdateInfo()
    {
        GameObject.Find("Info Area/Area").GetComponent<TextMeshProUGUI>().text = "default zone atm";
        GameObject.Find("Info Area/Activity").GetComponent<TextMeshProUGUI>().text = CoreSelectDisplay.selectedCore.core.activity.ToString();
        GameObject.Find("Info Area/Variant").GetComponent<TextMeshProUGUI>().text = CoreSelectDisplay.selectedCore.core.currentBody.variantName;
    }
}
