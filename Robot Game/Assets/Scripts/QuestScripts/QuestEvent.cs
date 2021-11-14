using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent
{
    public enum EventStatus
    {
        waiting, current, done
    };
    public string name;
    public string description;
    public string id;
    public EventStatus status;

    public List<QuestPath> pathList = new List<QuestPath>();

    public QuestEvent(string n, string d)
    {
        name = n;
        description = d;
        status = EventStatus.waiting;
    }

    public void UpdateEvent(EventStatus status)
    {
        this.status = status;
    }
}
