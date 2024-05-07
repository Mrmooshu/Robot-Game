using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNight : MonoBehaviour
{
    [System.Serializable]
    public class TimeData
    {
        public int dayspassed = 0;
        public float currentTime = 0;
    }

    private TimeData data;
    public SpriteRenderer sky;
    public GameObject sun;
    public GameObject sunpos;
    public GameObject moon;
    public GameObject moonpos;
    public GameObject stars;
    public SpriteRenderer starfadecover;
    public Color fadeout;
    public Light2D outdoorLight;
    public Color dawn;
    public Color day;
    public Color dusk;
    public Color night;
    public Color inside;
    public bool outdoors = true;
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
        if (!outdoors)
        {
            outdoorLight.color = inside;
            outdoorLight.intensity = 1;
            return;
        }
        data.currentTime += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, -90 + data.currentTime/DAYLENGTH*360);
        moon.transform.position = moonpos.transform.position;
        sun.transform.position = sunpos.transform.position;
        if (data.currentTime > DAYLENGTH)
        {
            data.currentTime -= DAYLENGTH;
            data.dayspassed++;
            MoonCheck();
        }

        if (data.currentTime >= DAYLENGTH * .95f)
        {
            lightcontrol((data.currentTime - (DAYLENGTH * .95f)) / (DAYLENGTH * .10f), night, dawn, .5f, .8f);
        }
        else if (data.currentTime >= DAYLENGTH * .50f)
        {
            lightcontrol((data.currentTime - (DAYLENGTH * .50f)) / (DAYLENGTH * .10f), dusk, night, .7f, .5f);
        }
        else if (data.currentTime >= DAYLENGTH * .40f)
        {
            lightcontrol((data.currentTime - (DAYLENGTH * .40f)) / (DAYLENGTH * .10f), day, dusk, 1f, .7f);
            stars.gameObject.SetActive(true);
        }
        else if (data.currentTime >= DAYLENGTH * .05f)
        {
            lightcontrol((data.currentTime - (DAYLENGTH * .05f))  / (DAYLENGTH * .10f), dawn, day, .8f, 1f);
            stars.gameObject.SetActive(false);
        }
        else if (data.currentTime < DAYLENGTH * .05f)
        {
            lightcontrol((data.currentTime + (DAYLENGTH * .05f)) / (DAYLENGTH * .10f), night, dawn, .5f, .8f);
        }
        void lightcontrol(float time, Color colorFrom, Color colorTo, float brighnessFrom, float brightnessTo)
        {
            sky.color = Color.Lerp(colorFrom, colorTo, time);
            outdoorLight.color = Color.Lerp(colorFrom, colorTo, time);
            outdoorLight.intensity = Mathf.Lerp(brighnessFrom, brightnessTo, time);
            if (colorFrom == day)
            {
                starfadecover.color = Color.Lerp(sky.color, fadeout, time);
            }
            else if (colorFrom == night)
            {
                starfadecover.color = Color.Lerp(fadeout, sky.color, time);
            }
            else
            {
                starfadecover.color = fadeout;
            }

        }
    }

    private void MoonCheck()
    {
        moon.transform.rotation = Quaternion.Euler(0,180+ data.dayspassed%30 * 12,0);
    }
}
