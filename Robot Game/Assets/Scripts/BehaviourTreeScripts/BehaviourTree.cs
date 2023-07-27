using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    private BehaviourNode root = null;

    protected void Start()
    {
        root = GetComponent<CharacterEntity>().brain;
    }

    private void Update()
    {
        if (root != null)
        {
            root.Evaluate();
        }
    }
}