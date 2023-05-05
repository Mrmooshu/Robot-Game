using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UniversalPlayerData : ISerializationCallbackReceiver
{
    public float ConnectionPowerCapacity { get { return 0; } }

    public Dictionary<string, int> upgrades;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {

    }
}
