using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variant/VariantDatabase", fileName = "Variant Database")]
public class VariantDatabase : ScriptableObject
{
    public List<VariantData> variantList;
}
