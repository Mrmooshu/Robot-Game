using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEyeLevel : MonoBehaviour
{
    [System.Serializable]
    public struct colorStruct
    {
        public Color color1;
        public Color color2;
    }

    public GolemEntity golem;
    public SpriteRenderer body;

    public List<colorStruct> levelColors;

    private void Start()
    {
        ((ClayGolemData)golem.data).ChargeLevelUpdated += EyeUpdate;
        EyeUpdate();
    }

    public void EyeUpdate()
    {
        body.material.SetColor("_FirstColor", levelColors[((ClayGolemData)golem.data).ChargeLevel].color1);
        body.material.SetColor("_SecondColor", levelColors[((ClayGolemData)golem.data).ChargeLevel].color2);
    }
}
