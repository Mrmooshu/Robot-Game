using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [System.Serializable]
    public class TimeData
    {
        public int dayspassed = 0;
        public float currentTime = 0;
    }

    private TimeData data;
    public GameObject sun;
    public GameObject moon;
    private const int DAYLENGTH = 3600;

    // Start is called before the first frame update
    void Start()
    {
        data = PlayerManager.instance.time;
        MoonCheck();
    }

    // Update is called once per frame
    void Update()
    {
        data.currentTime += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, data.currentTime/DAYLENGTH*360);
        //transform.Rotate(0,0, Time.deltaTime/ DAYLENGTH * 360);
        if (data.currentTime > DAYLENGTH)
        {
            data.currentTime -= DAYLENGTH;
            data.dayspassed++;
            MoonCheck();
        }
    }

    private void MoonCheck()
    {
        moon.transform.rotation = Quaternion.Euler(0,180+ data.dayspassed%30 * 12,0);
    }
}
