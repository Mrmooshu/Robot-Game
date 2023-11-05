using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static DamageScript;

public class SentinelEntity : MinionEntity
{
    new public WoodSentinelData data { get { return (WoodSentinelData)base.data; } private set { base.data = value; } }
}
