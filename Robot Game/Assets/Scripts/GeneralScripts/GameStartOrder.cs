using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartOrder : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        GeneralManager.instance.Initialize();
        UIManager.instance.Initialize();
        PlayerManager.instance.Initialize();
        QuestManager.instance.Initialize();
    }
}
