using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField]private GameObject[] sceneLights;
    private LightShift currentLightShift;
    private float timeDifference = Common.lightChangeDuration;
    private GameObject sun;

    private void OnEnable()
    {

        EventHandler.LightShiftChangeEvent += OnLightShiftChangeEvent;

    }

    private void OnDisable()
    { 
        EventHandler.LightShiftChangeEvent -= OnLightShiftChangeEvent;
    }
    private void Start()
    {
        sun = GameObject.Find("Global Light 2D");
        currentLightShift = LightShift.Morning;
        //sceneLights = FindObjectOfType<Light2D>();
    }
    private void OnLightShiftChangeEvent(LightShift lightShift, float timeDifference)
    {
        this.timeDifference = timeDifference;
        if (currentLightShift != lightShift)
        {
            currentLightShift = lightShift;
            if (lightShift == LightShift.Night)
            {
                foreach (GameObject lantern in sceneLights)
                {
                    lantern.GetComponent<Light2D>().enabled = true;
                }
            }
            else
            {
                foreach(GameObject lantern in sceneLights)
                {
                    lantern.GetComponent<Light2D>().enabled = false;
                }
            }

            sun.GetComponent<LightControl>().ChangeLightShift(currentLightShift, timeDifference);
        }
    }
}
