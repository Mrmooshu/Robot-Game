using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLockMenu : MonoBehaviour
{
    private void OnEnable()
    {
        UIManager.instance.menuPreventingMovement = true;
    }

    private void OnDisable()
    {
        UIManager.instance.menuPreventingMovement = false;
    }
}
