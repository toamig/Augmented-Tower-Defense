using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeatherController : MonoBehaviour
{
    public GameObject lightClouds;
    public GameObject heavyClouds;
    public GameObject lightRain;
    public GameObject heavyRain;

    private WeatherNetwork weatherNetwork;
    public WeatherNetwork.WeatherTypes weather = WeatherNetwork.WeatherTypes.Clear;

    // Start is called before the first frame update
    void Awake()
    {
        WeatherNetwork.OnWeatherDataReceived += OnWeatherDataBroadcast;
        GameEvents.instance.OnMapDetected += setWeather;
        weatherNetwork = new WeatherNetwork();
        StartCoroutine(weatherNetwork.ProcessWeather());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnWeatherDataBroadcast(WeatherNetwork.WeatherTypes type)
    {
        weather = type;
    }

    private void setWeather()
    {
        if (weather == WeatherNetwork.WeatherTypes.Clear)
        {

        }
        else if (weather == WeatherNetwork.WeatherTypes.Clouds)
        {
            heavyClouds.SetActive(true);
        }
        else if (weather == WeatherNetwork.WeatherTypes.Drizzle)
        {
            lightClouds.SetActive(true);
            lightRain.SetActive(true);
        }
        else if (weather == WeatherNetwork.WeatherTypes.FewClouds)
        {
            lightClouds.SetActive(true);
        }
        else if (weather == WeatherNetwork.WeatherTypes.HeavyRain)
        {
            GameManager.instance.audioManager.Play("Rain");
            heavyClouds.SetActive(true);
            heavyRain.SetActive(true);
        }
        else if (weather == WeatherNetwork.WeatherTypes.LightRain)
        {
            GameManager.instance.audioManager.Play("Rain");
            lightClouds.SetActive(true);
            lightRain.SetActive(true);
        }
        else if (weather == WeatherNetwork.WeatherTypes.Rain)
        {
            GameManager.instance.audioManager.Play("Rain");
            heavyClouds.SetActive(true);
            heavyRain.SetActive(true);
        }
        else if (weather == WeatherNetwork.WeatherTypes.Snow)
        {
            //TODO snow
        }
        else if (weather == WeatherNetwork.WeatherTypes.Strom)
        {
            GameManager.instance.audioManager.Play("Wind");
            //TODO storm
        }
        else
        {

        }
    }
}
