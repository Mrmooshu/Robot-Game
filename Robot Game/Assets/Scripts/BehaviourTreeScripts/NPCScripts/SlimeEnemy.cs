using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageScript;

public class SlimeEnemy : CharacterEntity
{
    public GameObject coreGo;

    public override void Start()
    {
        base.Start();
        SpawnCore();
    }

    protected override void CreateBrain()
    {
        brain = new BehaviourSequence(new List<BehaviourNode>
        {
            new IsInIdleBehaviour(this),
            new HitstunnedBehaviour(this),
            new TargetPlayerBehaviour(this, 5),
            new FaceTargetBehaviour(this),
            new ActionCooldownBehaviour(this, (1.2f,1.6f), "primaryActions"),
            new RandomBehaviour(new List<BehaviourNode>
            {
                new BehaviourSequence(new List<BehaviourNode>
                {
                    new LineSightTargetBehaviour(this, 4),
                    new SlimeSlideBehaviour(this, 30, 4)
                }),

                new BehaviourSequence(new List<BehaviourNode>
                {
                    new RandomJumpBehaviour(this),
                    new JumpBehaviour(this, 1)
                }),
                new BehaviourSequence(new List<BehaviourNode>
                {
                    new BehaviourInverter(new List<BehaviourNode> {new IsIinPursuitBehaviour(this) }),
                    new TurnAroundBehaviour(this)
                })
            })
        });
        brain.SetData("JumpDisabled", false);
        brain.SetData("Target", null);
        brain.SetData("SlideDisabled", false);
        brain.SetData("primaryActions", false);
        base.CreateBrain();
    }

    private void SpawnCore()
    {
        //GameObject core = Instantiate(GeneralManager.instance.entityPrefabs.LoadAsset<GameObject>("Slime Core"), coreGo.transform.position, new Quaternion(), coreGo.transform);
        //core.GetComponent<Projectile>().Initialize(facingDirection, this, new attackData(this, new damageData(stats[EntityStatType.AttackDamage].Value * .1f + stats[EntityStatType.MagicDamage].Value, damageType.magic), false, (Vector2.zero, Vector2.zero), .5f, whatIsEnemy));
    }

    protected override void Die()
    {
        base.Die();
        Instantiate(GeneralManager.instance.entityPrefabs.LoadAsset<GameObject>("Slime Death"), transform.position, new Quaternion());
        Destroy(gameObject);
    }
}
