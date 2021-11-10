using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore
{
    public PlayerBody currentBody;
    public bool activeCore;

    public PlayerCore(PlayerBody currentBody, bool activeCore)
    {
        this.currentBody = currentBody;
        this.activeCore = activeCore;
    }

}
