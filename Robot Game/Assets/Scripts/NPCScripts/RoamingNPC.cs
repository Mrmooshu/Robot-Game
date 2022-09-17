using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingNPC : CharacterEntity
{
    public WanderBehaviour wander;

    public override void Start()
    {
        base.Start();
        wander = new WanderBehaviour(this);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        wander.Wander(this);
    }
}
