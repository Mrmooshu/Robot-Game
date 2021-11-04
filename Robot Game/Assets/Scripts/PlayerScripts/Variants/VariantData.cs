using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class VariantData : ScriptableObject
{
    [Header("Base Variant Info")]
    public string variantName = "Default";
    [Header("Base Variant Animations")]
    public AnimatorOverrideController animController;
    [Header("Base Variant Base Stats")]
    public float moveSpeed = 1;
    public float jumpForce = 1;
}
