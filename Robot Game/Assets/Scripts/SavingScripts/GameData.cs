using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public PlayerCore[] cores;
    public PlayerBody[] bodies;
    public ItemInventory bankInventory;
    public List<QuestData> quests;

    public GameData()
    {
        bodies = new PlayerBody[0];
        cores = new PlayerCore[2] { new PlayerCore(new PlayerBody("Clay Golem"), 10, 10) , new PlayerCore(new PlayerBody("Snow Golem"), 10, 10) };
        bankInventory = new ItemInventory(48);
        quests = new List<QuestData>();
    }

    [System.Serializable]
    public struct QuestData
    {
        public string name;
        public int currentStep;
        public int questState;
    }
}
