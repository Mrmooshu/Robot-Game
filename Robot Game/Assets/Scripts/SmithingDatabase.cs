using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skilling/SmithingDatabase", fileName = "SmithingDatabase")]
public class SmithingDatabase : ScriptableObject
{
    [System.Serializable]
    public struct smeltingRecipe
    {
        public ItemData input;
        public ItemData output;
        public int progress;
    }

    // input item, output item, progress needed
    public List<smeltingRecipe> smeltingList;
}