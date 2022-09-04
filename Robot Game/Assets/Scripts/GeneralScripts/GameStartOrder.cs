using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartOrder : MonoBehaviour
{
    void Start()
    {
        UIManager.instance.Initialize();
        PlayerManager.instance.Initialize();
    }
}
