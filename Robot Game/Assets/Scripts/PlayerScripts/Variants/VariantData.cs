using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariantData : ScriptableObject
{
    public enum Type
    {
        golem,sentinel,automaton
    }


    [Header("Base Variant Info")]
    public string variantName = "Default";
    public Type type;
    [Header("Base Variant Animations")]
    public RuntimeAnimatorController animController;
    [Header("Base Variant Base Stats")]
    public float moveSpeed = 1;
    public float jumpForce = 1;
}
