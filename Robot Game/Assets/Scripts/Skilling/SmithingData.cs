using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SmithingData
{
    public SmithingStation currentStation;

    public SmithingData()
    {
        stations = new SmithingStation[] { new SmithingStation() };
    }

    [System.Serializable]
    public class SmithingStation
    {
        [System.Serializable]
        public class SmithTask
        {
            public static event Action smithTaskUpdated;

            [SerializeReference]public Item input;
            [SerializeReference]public Item output;
            public float progress;
            public int targetProgress;
            public IEnumerator task { get; private set; }

            public SmithTask()
            {
                input = null;
                output = null;
            }

            public void Initialize()
            {
                task = ProgressTask();
                PlayerManager.instance.StartCoroutine(task);
            }

            public void Complete()
            {
                progress = 0;
                var item = new Item(Database.GetSmeltingRecipe(input.itemID).output.itemID, 1);
                if (output == null)
                {
                    output = item;

                }
                else if (output.itemID == item.itemID)
                {
                    output.quanity++;
                }
                input.quanity--;
                smithTaskUpdated.Invoke();
                CheckToFinishTask();
            }

            ~SmithTask()
            {
                PlayerManager.instance.StopCoroutine(task);
            }

            private IEnumerator ProgressTask()
            {
                while (true)
                {
                    if (input.quanity > 0)
                    {
                        progress += .1f; //change later
                    }
                    if (progress >= targetProgress)
                    {
                        Complete();
                    }
                    yield return new WaitForSeconds(.1f);
                }
            }

            public void CheckToFinishTask()
            {
                if (input.quanity == 0 && output == null)
                {
                    PlayerManager.instance.smithing.currentStation.tasks.Remove(this);
                }
            }
        }

        [SerializeReference] public Item furnaceFuel;

        public List<SmithTask> tasks;

        public int capacityLimit;
        public int currentCapacity { get { var total = 0; tasks.ForEach(x => total += (int)x.input.quanity); return total; } }

        public SmithingStation()
        {
            furnaceFuel = null;
            tasks = new List<SmithTask>();
            capacityLimit = 15;
        }
    }

    public SmithingStation[] stations;
}
