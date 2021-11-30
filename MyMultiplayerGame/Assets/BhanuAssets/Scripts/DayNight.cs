using System;
using ScriptableObjects;
using UnityEngine;
using RenderSettings = UnityEngine.RenderSettings;

public class DayNight : MonoBehaviour
{
    //[SerializeField] private bool _bNightTime; //This is for Demo only
    
    private DateTime _dateAndTime;
    private Light _light;

    [SerializeField] private DayNightData dayNightData;
    [SerializeField] private GameObject directionalLightObj;

    private void Start()
    {
        _dateAndTime = DateTime.Now;
        InvokeRepeating(nameof(NightCheck) , 0.01f , 1.2f);
    }

    private void NightCheck()
    {
        _dateAndTime = DateTime.Now;
        
        if(_dateAndTime.Hour >= dayNightData.NightHour)
        {
            _light = directionalLightObj.GetComponent<Light>();
            _light.intensity = dayNightData.NightLightIntensityValue;
            RenderSettings.skybox = dayNightData.SkyBoxNightMaterial;
        }
        else
        {
            _light = directionalLightObj.GetComponent<Light>();
            _light.intensity = dayNightData.DayLightIntensityValue;
            RenderSettings.skybox = dayNightData.SkyBoxDayMaterial;
        }

        //This is for testing only
        // if(_bNightTime)
        // {
        //     _light = directionalLightObj.GetComponent<Light>();
        //     _light.intensity = dayNightData.NightLightIntensityValue;
        //     RenderSettings.skybox = dayNightData.SkyBoxNightMaterial;
        // }
        // else
        // {
        //     _light = directionalLightObj.GetComponent<Light>();
        //     _light.intensity = dayNightData.DayLightIntensityValue;
        //     RenderSettings.skybox = dayNightData.SkyBoxDayMaterial;
        // }
    }
}
