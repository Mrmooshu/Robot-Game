using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPath
{
    public QuestEvent from;
    public QuestEvent to;

    public QuestPath(QuestEvent from, QuestEvent to)
    {
        this.from = from;
        this.to = to;
    }
}
