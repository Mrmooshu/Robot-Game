using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeDisplay : MonoBehaviour
{
    public Transform view;

    private void Start()
    {
        Refresh();
    }

    public virtual void Refresh()
    {
        GameObject skillTree = Instantiate(Database.GetVariant(PlayerManager.instance.activeCore.currentBody.variantName).skillTree, view);
        GetComponent<ScrollRect>().content = skillTree.GetComponent<RectTransform>();
    }
}
