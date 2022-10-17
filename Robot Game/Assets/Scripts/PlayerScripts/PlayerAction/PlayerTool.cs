using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTool : MonoBehaviour
{
    public void CallToolAction()
    {
        transform.parent.GetComponent<PlayerEntity>().ToolAction();
    }
}
