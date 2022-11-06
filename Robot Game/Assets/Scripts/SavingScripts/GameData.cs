using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public PlayerCore[] cores;
    public BodyInventory bodyInventory;
    public ItemInventory bankInventory;
    public List<QuestData> quests;

    public void NewGameData()
    {
        bodyInventory = new BodyInventory(10);
        cores = new PlayerCore[2] { new PlayerCore(new PlayerBody("Clay Golem"), 10, 10) , new PlayerCore(new PlayerBody("Snow Golem"), 10, 10) };
        bankInventory = new ItemInventory(48);
        quests = new List<QuestData>();

        foreach (Quest quest in Database.instance.questDatabase.questList)
        {
            quest.currentStep = 0;
            quest.questState = Quest.QuestState.inactive;
        }

        cores[0].currentBody.tool = new Item(Database.GetItemID("Novium Pickaxe"),1);
        cores[1].currentBody.tool = new Item(Database.GetItemID("Iromite Pickaxe"), 1);

        cores[0].currentBody.weapon = new Item(Database.GetItemID("Novium Warhammer"), 1);
        cores[1].currentBody.weapon = new Item(Database.GetItemID("Iromite Warhammer"), 1);

        bodyInventory.inventory[0] = new PlayerBody("Clay Golem");
        bodyInventory.inventory[1] = new PlayerBody("Snow Golem");

        bankInventory.Add(new Item(Database.GetItemID("Slythril Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Myphrite Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Adzium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Pherium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Rhodisium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Lectonium Warhammer"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Red Glove"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Test Dart"), 1));
        bankInventory.Add(new Item(Database.GetItemID("Test Boomerang"), 1));
    }

    [System.Serializable]
    public struct QuestData
    {
        public string name;
        public int currentStep;
        public Quest.QuestState questState;
    }

    public void NullDefaultValues()
    {
        // check for default bodies in body inventories
        for(int i = 0; i < bodyInventory.inventory.Length; i++)
        {
            if (bodyInventory.inventory[i] != null)
            {
                if (bodyInventory.inventory[i].variantName == "")
                {
                    bodyInventory.inventory[i] = null;
                }
            }
        }

        // check for default items in core inventories
        foreach (PlayerCore core in cores)
        {
            for (int i = 0; i < core.inventory.inventory.Length; i++)
            {
                if (core.inventory.inventory[i] != null)
                {
                    if (core.inventory.inventory[i].itemID == 0)
                    {
                        core.inventory.inventory[i] = null;
                    }
                }
            }
            NullBodyItems(core.currentBody);
        }

        // check for default items in bank inventory
        for (int i = 0; i < bankInventory.inventory.Length; i++)
        {
            if (bankInventory.inventory[i] != null)
            {
                if (bankInventory.inventory[i].itemID == 0)
                {
                    bankInventory.inventory[i] = null;
                }
            }
        }

        // check for deafault items in body items
        foreach (PlayerBody body in bodyInventory.inventory)
        {
            NullBodyItems(body);
        }
    }

    private void NullBodyItems(PlayerBody body)
    {
        if (body != null)
        {
            if (body.weapon != null)
            {
                if (body.weapon.itemID == 0)
                {
                    body.weapon = null;
                }
            }
            if (body.tool != null)
            {
                if (body.tool.itemID == 0)
                {
                    body.tool = null;
                }
            }
        }
    }
}
