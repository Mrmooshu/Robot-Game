using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Go Here Step", fileName = "GoHereStep")]
public class GoHereStep : Quest.QuestStep
{
    public Vector2 goalCord;
    public Vector2 boxDimensions;

    public override void Initialize(Quest quest)
    {
        base.Initialize(quest);
        GameObject goal = Instantiate(GeneralManager.instance.generalPrefabs.LoadAsset<GameObject>("GoHereQuestGoal"));
        goal.transform.position = goalCord;
        goal.GetComponent<BoxCollider2D>().size = boxDimensions;
        goal.GetComponent<GoHereTrigger>().step = this;
    }
}
