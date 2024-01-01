using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static DamageScript;

public class TitanEntity : MinionEntity
{
    new public MagmaTitanData data { get { return (MagmaTitanData)base.data; } private set { base.data = value; } }

    public override void MagicBasic()
    {
        throw new System.NotImplementedException();
    }

    public override void MeleeBasic()
    {
        throw new System.NotImplementedException();
    }

    public override void RangeBasic()
    {
        throw new System.NotImplementedException();
    }
}
