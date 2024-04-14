using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Seed", fileName = "Seed Item")]
public class Seed : ItemData
{
    [Header("Seed Properties")]
    public FarmingData.PlotData.PlotType seedtype = FarmingData.PlotData.PlotType.tree;
    public int numberofstages = 1;
    public float stageduration = 60;
    public GameObject treefab;
}
