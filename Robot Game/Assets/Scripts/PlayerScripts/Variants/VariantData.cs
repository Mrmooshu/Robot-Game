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
    public GameObject skillTree;
    public GameObject blueprint;
    [Header("Base Variant Animations")]
    public RuntimeAnimatorController animController;
    public Sprite sprite;
    [Header("Base Variant Base Stats")]
    public float moveSpeed = 1;
    public float jumpForce = 1;
    public float gravity = 1;
}
