using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreInfoDisplay : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<RawImage>().texture = GameObject.Find("Second Camera").GetComponent<Camera>().targetTexture;
    }
}
