using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedProgressHub : MonoBehaviour
{
    //get rid of this class

    static public List<Action> actions;

    private void Start()
    {
        actions = new List<Action>();
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        while (true)
        {
            actions.ForEach(x => x());
            yield return new WaitForSeconds(.1f);
        }
    }
}
