using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassExpBar : PercentDisplayBar
{
    private LevelData level { get { return PlayerManager.instance.activeMinion.Level; } } 
    public override void Start()
    {
        base.Start();
        Inititalize(() => { return level.exp - LevelData.GetExpForNextLevel(level.level - 1); }, () => { return LevelData.GetExpForNextLevel(level.level) - LevelData.GetExpForNextLevel(level.level - 1); });
    }
}
