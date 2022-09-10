using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/QuestDatabase", fileName = "QuestDatabase")]
public class QuestDatabase : ScriptableObject
{
    public List<Quest> questList;
}
