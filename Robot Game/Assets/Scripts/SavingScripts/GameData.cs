using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public UniversalPlayerData universal;
    [SerializeReference] public List<MinionData> activeMinions;
    public ItemInventory bankInventory;
    public List<QuestData> quests;

    public SmithingData smithing;
    public FarmingData farming;

    public DayNight.TimeData time;

    public void NewGameData()
    {
        universal = new UniversalPlayerData();
        activeMinions = new List<MinionData> { new ClayGolemData(), new ClayGolemData()};
        bankInventory = new ItemInventory(48);
        quests = new List<QuestData>();

        foreach (Quest quest in Database.instance.questDatabase.questList)
        {
            quest.currentStep = 0;
            quest.questState = Quest.QuestState.inactive;
        }

        smithing = new SmithingData();
        farming = new FarmingData();

        time = new DayNight.TimeData();

        //testing pusposes stuff

        bankInventory.Add(new Item(Database.GetItemID("Slythril Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Myphrite Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Adzium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Pherium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Rhodisium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Lectonium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Novium Pickaxe"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Coal"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Coal"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Coal"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Coal"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Coal"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Coppel Ore"), 100));
        bankInventory.Add(new Item(Database.GetItemID("Novium Ore"), 20));
        bankInventory.Add(new Item(Database.GetItemID("Iromite Ore"), 10));
        bankInventory.Add(new Item(Database.GetItemID("Slythril Ore"), 5));

        activeMinions[0].SkillPoints += 70;

        activeMinions[0].inventory.inventory[0] = new Item(Database.GetItemID("Novium Pickaxe"), 1);
        activeMinions[0].inventory.inventory[1] = new Item(Database.GetItemID("Test Hatchet"), 1);
        activeMinions[0].inventory.inventory[2] = new Item(Database.GetItemID("seed 1"), 1);

        activeMinions[1].inventory.inventory[0] = new Item(Database.GetItemID("Iromite Pickaxe"), 1);

        universal.abilities["Tornado"] = true;
        universal.abilities["RoamingCancel"] = true;

        activeMinions[0].functions[0] = new MiningFunction(activeMinions[0]);
        activeMinions[0].functions[1] = new MeleeFunction(activeMinions[0]);
        activeMinions[0].functions[2] = new WoodcuttingFunction(activeMinions[0]);
        activeMinions[0].functions[3] = new FarmingFunction(activeMinions[0]);
    }

    [System.Serializable]
    public struct QuestData
    {
        public string name;
        public int currentStep;
        public Quest.QuestState questState;
    }
}
