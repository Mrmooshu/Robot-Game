using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public PlayerCore[] cores;
    public PlayerBody[] bodyInventory;
    public ItemInventory bankInventory;
    public List<QuestData> quests;

    public void NewGameData()
    {
        bodyInventory = new PlayerBody[0];
        cores = new PlayerCore[2] { new PlayerCore(new PlayerBody("Clay Golem"), 10, 10) , new PlayerCore(new PlayerBody("Snow Golem"), 10, 10) };
        bankInventory = new ItemInventory(48);
        quests = new List<QuestData>();

        cores[0].currentBody.weapon = new Item(Database.GetItemID("Novium Pickaxe"),1);
        cores[1].currentBody.weapon = new Item(Database.GetItemID("Novium Pickaxe"), 1);
    }

    [System.Serializable]
    public struct QuestData
    {
        public string name;
        public int currentStep;
        public int questState;
    }

    public void NullDefaultItems()
    {
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
        foreach (PlayerBody body in bodyInventory)
        {
            if (body.weapon != null)
            {
                if (body.weapon.itemID == 0)
                {
                    body.weapon = null;
                }
            }
        }
    }
}
