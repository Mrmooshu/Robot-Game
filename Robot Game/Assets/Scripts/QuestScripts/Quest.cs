using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Quest", fileName = "Quest")]
public class Quest : ScriptableObject
{
    public enum QuestState{
        inactive, active, completed
    }

    [System.Serializable]
    public struct QuestInfo
    {
        public string questName;
        public string questInfo;
    }

    [System.Serializable]
    public struct QuestReward
    {
        public ItemData reward;
        public int quanity;
    }

    public QuestInfo info;
    public QuestReward[] rewards;
    public List<QuestStep> steps;
    public int currentStep;
    public QuestState questState = QuestState.inactive;

    public virtual void BeginQuest(int currentStep = 0)
    {
        this.currentStep = currentStep;
        if (questState == QuestState.inactive)
        {
            QuestManager.ProgressQuestState(this);
        }
        else
        {
            Debug.Log("This is a bug quest should only begin from being inactive");
        }

        SetCurrentStep(currentStep);
    }

    private void SetCurrentStep(int stepIndex)
    {
        steps[stepIndex].Initialize(this);
    }

    private void CheckSteps()
    {
        if (steps[currentStep].completed)
        {
            currentStep++;
            if (currentStep >= steps.Count)
            {
                CompleteQuest();
            }
            else
            {
                SetCurrentStep(currentStep);
            }
        }
    }

    public QuestStep GetCurrentStep()
    {
        return steps[currentStep];
    }

    protected virtual void CompleteQuest()
    {
        foreach (QuestReward reward in rewards)
        {
            GameObject item = GeneralManager.SpawnItem(PlayerManager.instance.activeCore.bodyObject.transform, new Item(reward.reward.itemID, reward.quanity));
            item.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-80, 80), 120));
        }
        QuestManager.ProgressQuestState(this);
        Debug.Log(info.questName + " completed");
    }

    public abstract class QuestStep : ScriptableObject
    {
        protected Quest quest;
        public string dialogue;
        public bool completed { get; protected set; }

        public virtual void Initialize(Quest quest)
        {
            completed = false;
            this.quest = quest;
        }

        public virtual void CheckProgress()
        {

        }

        public virtual void Evaluate()
        {
            Complete();
        }

        private void Complete()
        {
            completed = true;
            CleanUp();
            quest.CheckSteps();
        }

        protected virtual void CleanUp()
        {
        }
    }
}