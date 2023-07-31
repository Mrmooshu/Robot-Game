using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public UniversalPlayerData universal;
    [SerializeReference] public List<MinionData> activeMinions;
    [SerializeReference] public MinionInventory minionInventory;
    public ItemInventory bankInventory;
    public List<QuestData> quests;

    public void NewGameData()
    {
        universal = new UniversalPlayerData();
        activeMinions = new List<MinionData> { new ClayGolemData("Clay Golem"), new ClayGolemData("Clay Golem") };
        minionInventory = new MinionInventory(10);
        bankInventory = new ItemInventory(48);
        quests = new List<QuestData>();

        foreach (Quest quest in Database.instance.questDatabase.questList)
        {
            quest.currentStep = 0;
            quest.questState = Quest.QuestState.inactive;
        }

        //testing pusposes stuff

        minionInventory.inventory[0] = new ClayGolemData("Clay Golem");
        minionInventory.inventory[1] = new ClayGolemData("Clay Golem");
        minionInventory.inventory[2] = new ClayGolemData("Clay Golem");

        minionInventory.inventory[0].tool = new Item(Database.GetItemID("Novium Pickaxe"), 1);
        minionInventory.inventory[1].tool = new Item(Database.GetItemID("Iromite Pickaxe"), 1);

        bankInventory.Add(new Item(Database.GetItemID("Slythril Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Myphrite Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Adzium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Pherium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Rhodisium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Lectonium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Novium Pickaxe"), 1));

        activeMinions[0].SkillPoints += 70;

        activeMinions[0].inventory.inventory[0] = new Item(Database.GetItemID("Novium Pickaxe"), 1);

        activeMinions[1].inventory.inventory[0] = new Item(Database.GetItemID("Iromite Pickaxe"), 1);

        universal.abilities["Tornado"] = true;
        universal.abilities["RoamingCancel"] = true;
    }

    [System.Serializable]
    public struct QuestData
    {
        public string name;
        public int currentStep;
        public Quest.QuestState questState;
    }
}
