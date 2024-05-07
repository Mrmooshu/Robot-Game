using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassExpBar : PercentDisplayBar
{
    private LevelData level { get { return PlayerManager.instance.activeMinion.Level; } } 
    public override void Start()
    {
        base.Start();
        Inititalize(() => { return level.Exp - LevelData.GetExpForNextLevel(level.Level - 1); }, () => { return LevelData.GetExpForNextLevel(level.Level) - LevelData.GetExpForNextLevel(level.Level - 1); });
    }
}
